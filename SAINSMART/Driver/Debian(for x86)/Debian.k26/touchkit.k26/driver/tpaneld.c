 /* tab size: 4 */

/*
 * user-level driver for touch panel (mainly acts as a data repeater)
 */

#include <sys/types.h>
#include <sys/stat.h>
#include <sys/wait.h>
#include <sys/time.h>
#include <sys/ioctl.h>
#include <termios.h>
#include <unistd.h>
#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>
#include <signal.h>
#include <paths.h>
#include <ctype.h>
#include <string.h>
#include <errno.h>
#include <syslog.h>

#define BUTTON_DEBUG
#define CMD_DEBUG
//#define INPUT_DEBUG
//#define OUTPUT_DEBUG

//#ifdef BUTTON_DEBUG
 #define WhichOne(b)  ((b)==_RightButton?'R':'L')
#ifdef _DEBUG_BUTTON
 #define DumpButton              \
 { printf("%d: %c this: %c, next: %c\n",   \
    __LINE__,       \
    TouchSetting.touched ? 'D' : 'U', \
    WhichOne(TouchSetting.thisButton), \
    WhichOne(TouchSetting.nextButton)); \
 }
#else
 #define DumpButton
#endif


#include "tpanel.h"
#include "configSTR.h"
#include "configINT.h"
#include "filter.h"

#include "device.c"
#include "correct.c"
#include "eeprom.c"
#include "filter.c"

int optNotDaemon;
int optForce;

int RClickInited = 0;
int RClickPid = 0;
int MsgPipe[2];
#define FD_ToRClick  (MsgPipe[1])
#define FD_FromRClick (MsgPipe[0])


#define DEFAULT_CONST_TOUCH  1
#define DEFAULT_CONST_AREA  20
#define DEFAULT_AR_BUTTON  1
#define DEFAULT_AR_BUTTON_TIME  2 /*2 seconds */

typedef int TouchMode;

typedef struct tagConstTouch
{
 int bConstTouchEnabled;  /* Enable/Disable constant touch */
 int iConstTouchArea;    /* Constant Touch Area */
 int bAutoRightButtonEnabled;
 int iAutoRightButtonTime;
 long lEventDelay;
 //int bCurState;
 //int bLastState;
 //int bTracking;
    struct timeval TrackingTime;
 int cx; /* Right button position*/
 int cy; /* Right button position */
 int bTrackingNow;
}CONST_TOUCH;



struct
{ TouchMode mode;
 int soundMode;
 int dblClickSpeed;
 int dblClickArea;
 int thisButton;
 int nextButton;
 int commFdin;
 int commFdout;
 int rbuttonTag;
 int rClickTool;
 int oldx;
 int oldy;
 int touched;
 int count;
 FILTERCOEFF filter;
 struct timeval prevTime, thisTime;
 CONST_TOUCH    CTouch;

 int bIdleReset;
 int bHoldReset;
 int IdleTime;
 int HoldTime;
} TouchSetting = {0};





int bitValid = 11;

#define MaxMeasure  ((1<<bitValid)-1)
TouchPanel *panelList = NULL;  /* list of all panels */
/*
** input channels: One for control and Some for panels
*/
int  fdShrIn = -1;
int  fdCmdIn = -1;
int  fdMax = -1;  /* parameters for select() */
fd_set fdSet;   /* parameters for select() */

/*
** output channels: One for control and some for converted X-Y packets
*/
int  fdShrOut = -1;
int  fdCmdOut = -1;
#define NumOfOutput 2
int  fdOuts[NumOfOutput]; /* one for gpm, one for X, how about the 3rd? */

char  PortShared[500] = "";  /* name of port shared (designed for USB pnp :~) */

#define tp_perrorf(arg...)  printf(arg)
#define tp_message(arg...)  printf(arg)

void DumpSetting()
{ printf("Mode: %s\n", TouchSetting.mode == TM_DRAWING ?
       sTM_DRAWING : sTM_DESKTOP );
}

void DumpPanel()
{
#if 0
 TouchPanel *p;
 for (p=panelList; p; p=p->next)
 { printf("PANEL=%s (%d,%d)-(%d,%d) [%d]",
   p->device, p->xmin, p->ymin, p->xmax, p->ymax, p->rotate);
  printf(" %s\n", p->sharing ? "shared" : "");
 }
#endif
}

/*=========================================================================*/
/* Do Reset Panel 							   */
/*=========================================================================*/
void DoResetPanel( TouchPanel *Panel )
{
	char szCmd[] = { _SOP, 1, H2D_FIRMWARERESET };
	if( Panel->fd != -1 )
	{
		syslog( LOG_DEBUG|LOG_USER," Reset Panel now...\n " );
		write( Panel->fd, szCmd, sizeof( szCmd ) );
	}
	else
	{
		syslog( LOG_DEBUG|LOG_USER," Reset Panel Failure due to file = -1...\n " );
	}
	Panel->LastResetTime = time( NULL );

}

/*=========================================================================*/
/* Function for constant touch algorithm*/
/*=========================================================================*/


void SET_PANEL_MAPPING_FR_EEPROM(TouchPanel *p, EEPROM eeprom)
{ p->mapping.uiULX = eeprom[EPR_ULX];
 p->mapping.uiULY = eeprom[EPR_ULY];
 p->mapping.uiURX = eeprom[EPR_URX];
 p->mapping.uiURY = eeprom[EPR_URY];
 p->mapping.uiLLX = eeprom[EPR_LLX];
 p->mapping.uiLLY = eeprom[EPR_LLY];
 p->mapping.uiLRX = eeprom[EPR_LRX];
 p->mapping.uiLRY = eeprom[EPR_LRY];
 p->_25Mode = eeprom[EPR_Flags] & EPR_F_25CoefOn;

 p->coef.mapping.uiULX = eeprom[EPR_25ULX];
 p->coef.mapping.uiULY = eeprom[EPR_25ULY];
 p->coef.mapping.uiURX = eeprom[EPR_25URX];
 p->coef.mapping.uiURY = eeprom[EPR_25URY];
 p->coef.mapping.uiLLX = eeprom[EPR_25LLX];
 p->coef.mapping.uiLLY = eeprom[EPR_25LLY];
 p->coef.mapping.uiLRX = eeprom[EPR_25LRX];
 p->coef.mapping.uiLRY = eeprom[EPR_25LRY];

 { int col, row;

  for ( col=0; col<5; col++)
  { for ( row=0; row<5; row++)
   { short dat = eeprom[EPR_25(col + 5*row + 1)];
    p->coef.delta[0][col][row] = EPR_25X(dat);
    p->coef.delta[1][col][row] = EPR_25Y(dat);
   }
  }
 }
}

char *GetRomFilename(char *name)
{ static char buf[100];
 char *slash;
 
 if ( (slash = strrchr(name, '/')) )
 { name = slash + 1;
 }
 sprintf(buf, IDcat(PRJNAME, _ROMHOME) ROM_FORMAT, name);
 return buf;
}
                        

TouchPanel *NewPanel(char *device, int baud)
{ TouchPanel *p;
 EEPROM eeprom;
 int fd;

 if ( !device )
  return NULL;

 printf("Test %s....", device);
 fflush(stdout);
 
 if ( (fd=open(device, O_RDWR )) < 0 )
 { printf("open failed\n");
   syslog( LOG_DEBUG|LOG_USER," Device on %s failure..\n", device );
   return NULL;
 }
 Dev_SaveState(fd);
 Dev_SetBaud(fd, baud);

 if (!LoopBackOK(fd))
 { printf("no responce\n");
   syslog( LOG_DEBUG|LOG_USER," Loop back No Response...\n " );
   Dev_RestoreState(fd);
   close(fd);
   return NULL;
 }

 { char *filename = GetRomFilename(device);
   int retry = 5;
  while (CachedReadEEPROM(filename, fd, eeprom))
  { printf("bad eeprom\n");
    syslog( LOG_DEBUG|LOG_USER," EEPROM ERROR -- try %d again now... in %s..\n", retry,  device );
    if ( retry == 0 )
    {
        syslog( LOG_DEBUG|LOG_USER," EEPROM ERROR -- FAILURE!!! in %s..\n", device );
   	Dev_RestoreState(fd);
   	close(fd);
   	return NULL;
    }
    retry--;
  }
  printf("OK\n");
  syslog( LOG_DEBUG|LOG_USER," Touch devices on %s Ok\n", device );
 }
 
 p = (TouchPanel*)malloc(sizeof(TouchPanel)+strlen(device)+1);
 if ( !p )
 { tp_perrorf("malloc %s", device);
  close(fd);
  return NULL;
 }else
 { p->device  = (char*)(p+1); strcpy(p->device, device);
  p->fd  = fd;
  p->len  = 0;
  p->state = Normal;
  p->next  = NULL;
  p->sharing = 0;
  p->present = 1;
  p->shrlen = 0;
  p->touched = 0;
  p->LastResetTime = time( NULL );
  p->IdleResetInterval = 60 * 1; // Reset the controller every 3 min
  p->DownResetInterval = 60 * 1; // If always point the same point. Reset

  p->iCmdBytesRecved = 0; 
  p->iCmdLen = 0;	// Command Length
  p->bCmdPacket = 0;	// Currently is Cmd packet;
  p->bPtPacket = 0;
  
  SET_PANEL_MAPPING_FR_EEPROM(p, eeprom);
  
  p->chksum  = eeprom[EPR_CheckSum];
  
   return p;
 }
}  

inline void LeaveList(TouchPanel **headp, TouchPanel *target, TouchPanel *pre)
{ if (pre == NULL)
 { *headp = target->next;
 }else
 { pre->next = target->next;
 }
}

inline void JoinList(TouchPanel **headp, TouchPanel *newp)
{ newp->next = *headp;
 *headp  = newp;
}

inline void EmptyPipe(int fd)
{ char buf[5000];
 read(fd, buf, 5000);
}

/*
** Send "Rescan" to touchcfg
*/
void SendRescanCmd()
{ struct stat filestat;
 int fd;
 
 puts("Send rescan to " TOUCHCFG_CMD);
 if ( !stat(TOUCHCFG_CMD, &filestat)
  && S_ISFIFO(filestat.st_mode)
  && (fd = open(TOUCHCFG_CMD, O_RDWR|O_NDELAY)) >= 0 )
 { char msg[] = "rescan\n"; // !! sync this msg with touchcfg.tcl
  write(fd, msg, sizeof msg - 1);
  close(fd);
 }
}

inline int DoRead(TouchPanel *panel)
{ int rval = read(panel->fd, panel->buf+panel->len, TP_BUFSZ-panel->len);

 #ifdef INPUT_DEBUG
  printf("data from %s: (%d)\n", panel->device, rval);
  { int i;
   for (i=0; i<rval; i++)
   { printf("%02X ", (unsigned char)(panel->buf+panel->len)[i]);
   }
   puts("");
  }
 #endif
 if (rval == 0)
 { static int cnt = 0;
  if (cnt++ > 5)
  { cnt = 0;
   kill(getpid(), SIGHUP);
   return 0;
  }
 }else if ( rval > 0 )
 { if (panel->sharing)
  {
   #ifdef OUTPUT_DEBUG
    printf("forward data\n");
   #endif
   if ( panel->buf[panel->len+rval-1] == cCR
    || (    (panel->len+rval) >= FrameSize 
     && !Touched(panel->buf[panel->len+rval-FrameSize]) )
    || panel->shrlen + rval > TP_SHRBUFSZ )
   { if (panel->shrlen)
    { write(fdShrOut, panel->shrbuf, panel->shrlen);
     panel->shrlen = 0;
    }
    if (write(fdShrOut, panel->buf+panel->len, rval) == -1)
    { panel->sharing = 0;
     EmptyPipe(fdShrOut);
    }
   }else
   { memcpy(panel->shrbuf+panel->shrlen, panel->buf+panel->len, rval);
    panel->shrlen += rval;
   }
   #if 0 /* forward without buffer */
   if (write(fdShrOut, panel->buf+panel->len, rval) == -1)
   { panel->sharing = 0;
    EmptyPipe(fdShrOut);
   }
   #endif 
  }
  panel->len += rval;
 } else
 { if (errno == ENODEV)
  { panel->present = 0;
   FD_CLR(panel->fd, &fdSet);
   printf("Device removed: %s\n", panel->device);
   SendRescanCmd();
  }
 }
 return rval;
}

char *DataFilter(TouchPanel *panel)
{ static char buf[FrameSize];
 int i;
 
 for (i=0; i<panel->len; i++)
 { if (IsFirstByteOfFrame(panel->buf[i]))
  { if (i+FrameSize <= panel->len) /* Frame complete */
   { int j;
    for (j=1; j<FrameSize; j++)
    { if (IsFirstByteOfFrame(panel->buf[i+j]))
     { break;
     }
    }
    if (j<FrameSize)
    { // frame truncated
     continue;
    }else
    { memcpy(buf, panel->buf+i, FrameSize);
     memcpy(panel->buf, panel->buf+i+FrameSize, panel->len -= FrameSize + i);
     return buf;
    }
   }else
   { memcpy(panel->buf, panel->buf+i, panel->len -= i);
    return NULL;
   }
  }
 }
 panel->len = 0;
 return NULL;
}


void RotateAndShift(double x, double y, double *xp, double *yp, int angle)
{ switch (angle)
 {
 case 0:
  *xp = x;
  *yp = y;
  break;
 case 1:
  *xp = -y + MaxMeasure;
  *yp = x;
  break;
 case 2:
  *xp = -x + MaxMeasure;
  *yp = -y + MaxMeasure;
  break;
 case 3:
  *xp = y;
  *yp = -x + MaxMeasure;
  break;
 }
}

#define WithinArea(cx, cy, x, y, dis)  \
( (cx) - (x) <= (dis)      \
&& (x) - (cx) <= (dis)      \
&& (cy) - (y) <= (dis)      \
&& (y) - (cy) <= (dis))

#define FastTimeBound 500000   /* 0.5 sec */
#define SlowTimeBound 1800000   /* 1.8 sec */

#define WithinTimeBound(past, cur)    \
({ int long _dsec;        \
 _dsec = (cur)->tv_sec - (past)->tv_sec;  \
 ( (_dsec < 3)        \
  && 1000000 * _dsec + (cur)->tv_usec - (past)->tv_usec    \
   < FastTimeBound +             \
    (TouchSetting.dblClickSpeed-MaxDblClickSpeed) *    \
     ((SlowTimeBound-FastTimeBound)/       \
     (MinDblClickSpeed-MaxDblClickSpeed)));     \
})


int IsTimeToRightButton(int x, int y)
{
    struct timeval timex;
    long time0, time1;
/*
    if( TouchSetting.CTouch.cx == x &&
        TouchSetting.CTouch.cy == y )
    {
        return 0; */ /* Same position, no need to click right button */
/*    }*/
    if( TouchSetting.CTouch.bTrackingNow )
    {
     return 0;
    }
    time0 = ( TouchSetting.CTouch.TrackingTime.tv_sec * 1000000 ) +
            ( TouchSetting.CTouch.TrackingTime.tv_usec );
    gettimeofday( &timex, NULL );
    time1 = ( timex.tv_sec * 1000000 ) + timex.tv_usec;
    if( ( time1 - time0 ) > ( TouchSetting.CTouch.iAutoRightButtonTime * 1000 ) )
    {
        TouchSetting.CTouch.cx = x;
        TouchSetting.CTouch.cy = y;
        return 1;
    }
    else
    {
        return 0;
    }


}

void GenFrameWithMouseMode (
 TouchPanel *panel,
 unsigned char head,
 int x, int y, char *dest, int *len )
{ 
 
// printf("m(%d) c(%d)\n", TouchSetting.mode, TouchSetting.count);
 
    int bConstTouch  = 0;
    int bToggleRClick = 0;
    if (TouchSetting.mode == TM_DRAWING)
 {
 }else // TM_DESKTOP, enhanced for double-click
 { 
	if ( 0 < TouchSetting.count )
  	{ if ( TouchSetting.count <= TouchSetting.dblClickSpeed )
	   { if ( WithinArea(TouchSetting.oldx, TouchSetting.oldy,
      				  x, y, TouchSetting.dblClickArea) )
		    { x = TouchSetting.oldx;
		     y = TouchSetting.oldy;
		     TouchSetting.count++;
		    }
		else
	    	{	 
			TouchSetting.count = TouchSetting.dblClickSpeed + 1;
    	     	}
   	     }
   	    if ( !Touched(head) )
	     { 
	      TouchSetting.count = 0;
	      TouchSetting.prevTime = TouchSetting.thisTime;
	  }
        else
          { 
	      }
	    TouchSetting.count = 1;
          gettimeofday(&TouchSetting.thisTime, NULL);
          if ( WithinTimeBound(&TouchSetting.prevTime, &TouchSetting.thisTime) &&
               WithinArea(TouchSetting.oldx, TouchSetting.oldy, x,
               y,TouchSetting.dblClickArea<<1) )
	   { 
		x = TouchSetting.oldx;
	      y = TouchSetting.oldy;
   	   }
          }
 }
 /*
 */
 if( ( TouchSetting.touched != Touched( head ) ) &&  Touched(head) )
    {
        gettimeofday( &TouchSetting.CTouch.TrackingTime, NULL );
        TouchSetting.oldx = x;
        TouchSetting.oldy = y;
        panel->LastResetTime = time( NULL );
    }
if( TouchSetting.CTouch.bConstTouchEnabled )
    {
        if(  ( abs( x - TouchSetting.oldx ) < TouchSetting.CTouch.iConstTouchArea ) &&
             ( abs( y - TouchSetting.oldy ) < TouchSetting.CTouch.iConstTouchArea ) )
        {
            x = TouchSetting.oldx;
            y = TouchSetting.oldy;
            bConstTouch = 1;
        }
    }
    /*TouchSetting.thisButton = _RightButton;*/
    if( bConstTouch && TouchSetting.CTouch.bAutoRightButtonEnabled )
    {
       if( IsTimeToRightButton( x, y ) )
        {
            //TouchSetting.thisButton = _RightButton;
            bToggleRClick = 1;
            gettimeofday( &TouchSetting.CTouch.TrackingTime, NULL );
        }
        else
        {

        }
    }
    else if( !bConstTouch )
    {
        gettimeofday( &TouchSetting.CTouch.TrackingTime, NULL );
        panel->LastResetTime = time( NULL );
    }
    if( bConstTouch )
    {
    	time_t CurrentTime = time( NULL );
    	if( ( CurrentTime - panel->LastResetTime ) > TouchSetting.HoldTime && TouchSetting.bHoldReset  )
    	{
    		// If time out, just reset the controller.
		syslog( LOG_DEBUG|LOG_USER," Start Hold Reset Now ... \n" );
    		DoResetPanel( panel );
    	}
    }
	 TouchSetting.oldx = x;
	 TouchSetting.oldy = y;
	 setXY(dest, x, y);
	 dest[0] = head;
	 *len = FrameSize;
	 panel->touched = TouchSetting.touched = Touched(head);
	 if( bToggleRClick )
	 {
	  memcpy( dest+FrameSize, dest, FrameSize );  // generate a l button up
	  memcpy( dest+2*FrameSize, dest, FrameSize );
	  *len = ( 3 * FrameSize );
	  dest[0] &= ~0x01;   				// Generate L Button Up
	  setRButton(dest+FrameSize);
	  dest[FrameSize ] |= ( 0x20 | 0x01 );   	// RButton down
	  dest[2*FrameSize] &= ~0x01;
	  dest[2*FrameSize ] |= 0x20;        		// RButton up 

	  TouchSetting.CTouch.bTrackingNow = 1;
	  return;	
	 }
	 else if (TouchSetting.thisButton == _RightButton)
	 { int i;

	  for (i=0; i<*len; i+=FrameSize)
	  { 
	    setRButton(dest+i);
	  }
 	}
	 DumpButton;

#if 0
 printf("panel status %d, global touched %d",
  Touched(head), TouchSetting.touched);
 puts("");
#endif

 if ( !Touched(head) )
 { /*
   if( TouchSetting.CTouch.bTrackingNow )
   {
	*len = 0;
   }	*/
  TouchSetting.thisButton = TouchSetting.nextButton;
  TouchSetting.nextButton = _LeftButton;
  TouchSetting.CTouch.bTrackingNow = 0;
 }
 else
 {
	 if( TouchSetting.CTouch.bTrackingNow )
	{
			dest[0] &= ~0x01;
	}
  }
}

inline void XBEEP()
{ if (RClickPid)
 { char buf[] = "BEEP\n";
  write(FD_ToRClick, buf, sizeof buf - 1);
 }
}


void CheckFrameForBeep(char *frame, int dlen)
{ static int OldTouched = 0;
 int i;

 for (i=0; i<dlen; i+=FrameSize)
 { int touched = Touched(frame[i]);
		if ( OldTouched != touched )
		{	if (touched)
			{	if (TouchSetting.soundMode == SM_DOWN)
				{	XBEEP();
					/* delay 0.001 sec */
					{	struct timeval delay = { 0, 1000}; // 350 ms
						select(0, NULL, NULL, NULL, &delay);
					}
				}
			}else
			{	if (TouchSetting.soundMode == SM_UP)
				{	XBEEP();
				}
			}
			OldTouched = touched;
		}
	}
}


void ReportFrame(TouchPanel *panel, char *src)
{	int i;
	int orgX, orgY;

	if (!src)
	{	return;
	}
	bitValid = BitinXY(src[0]);
	orgX = X(src);
	orgY = Y(src);

	OnZTransfer(&orgX, &orgY, Touched(src[0]), &TouchSetting.filter);
	
	if ( panel->_25Mode )
	{	CorrectPoint(&panel->coef.mapping, &orgX, &orgY);
		Calibration25Pt(&panel->coef, &orgX, &orgY);	
	}
	CorrectPoint(&panel->mapping, &orgX, &orgY);

	if( orgX < 0 )
		orgX = 0;
	else if( orgX > MAX_DIMENSION )
		orgX = MAX_DIMENSION;
	if( orgY < 0 )
		orgY = 0;
	else if( orgY > MAX_DIMENSION )
		orgY = MAX_DIMENSION;


	#ifdef OUTPUT_DEBUG
		printf("about to report %02X (%d, %d)\n", (unsigned char)*src, orgX, orgY);
	#endif
	{	char frame[30];
		int len;

		GenFrameWithMouseMode(panel, *src, orgX, orgY, frame, &len);

		CheckFrameForBeep(frame, len);
		/* duplicate X-Y packet */
		if( len )
		{
		for (i=0; i<NumOfOutput; i++)
		{
			#ifdef OUTPUT_DEBUG
				printf("output to dat[%d]: (dat from %s)\n", i, panel->device);
			#endif
			
			if( len > 5 )
			{
				if( write(fdOuts[i], frame, 5) == -1 )	
				{
					EmptyPipe(fdOuts[i]);	
				}
				{
                           struct timeval delay = { 0, 300000};
				 delay.tv_usec = TouchSetting.CTouch.lEventDelay * 1000;
				 select(0, NULL, NULL, NULL, &delay);
				}
		             if( write(fdOuts[i], frame+5, len-5) == -1 )
				{
					EmptyPipe(fdOuts[i]);
				}
			}
			else
			{
				if( write(fdOuts[i], frame, len) == -1 ) /* channel is full */
				{	
					EmptyPipe(fdOuts[i]);
				}

			}	
			//if( write(fdOuts[i], frame, len) == -1 ) /* channel is full */
			//{	
			//	EmptyPipe(fdOuts[i]);
			//}
		}
		}
	}
}

void SetLock()
{	char tmp_file[] = _PATH_VARRUN "tpdXXXXXX";
	FILE *fp;

	mkstemp(tmp_file); //+ strlen(_PATH_VARRUN));
	if ( (fp=fopen(tmp_file, "w")) )
	{	fprintf(fp, "%d\n", getpid());
		fclose(fp);
	}else if (getuid())
	{	tp_message("You must have root permission to execute this program.");
		exit(1);
	}else
	{	tp_message("Cannot open file %s\n", tmp_file);
		exit(1);
	}
	
	unlink(IDcat(PRJNAME, _LOCK));
	link(tmp_file, IDcat(PRJNAME, _LOCK));
	unlink(tmp_file);
	return;
}

int IsUnique(int *pid)
{	FILE *fp;
	int rval = -1;

	if ( (fp=fopen(IDcat(PRJNAME, _LOCK), "r")) )
	{	if ( fscanf(fp, "%d", pid) )
		{	if ( !kill(*pid, 0) )
			{	rval = 0;
			}else if ( errno == ESRCH )   // no such process
			{	rval = 1;
			}else
			{	tp_message("You must have root permission to execute this program.");
				exit(1);
			}
		}
		fclose(fp);
	}
	return rval;
}

int CreatePipe(char *name, int mode)
{	struct stat filestat;
	static int retry = 0;
	int fd;
	
	while ( stat(name, &filestat) == -1 )
	{	if ( mkfifo(name, FIFO_MODE) == -1 )
		{	return -1;
		}
		chmod(name, mode);
	}
	if ( S_ISFIFO(filestat.st_mode) )
	{	fd = open(name, O_RDWR|O_NDELAY);
		if ( fd != -1 )
		{	retry = 0;
			return fd;
		}else
		{	tp_perrorf("open %s", name);
		}
	}else
	{	if ( !unlink(name) )
		{	tp_message("%s should be a pipe.\nDelete it and try again...\n", name);
			if (retry > 5)
			{	retry = 0;
				return -1;
			}else
			{	retry++;
				return CreatePipe(name, mode);
			}
		}
	}
	return -1;
}


int CreateSharePipe()
{	int fd;

	if ( (fd=CreatePipe(IDcat(PRJNAME, _SHRIN), 0622)) == -1 )
	{	return 1;
	}else
	{	fdShrIn = fd;
	}
	if ( (fd=CreatePipe(IDcat(PRJNAME, _SHROUT), 0644)) == -1 )
	{	return 1;
	}else
	{	fdShrOut = fd;
	}
	return 0;
}


int CreateCmdPipe()
{	int fd;

	if ( (fd=CreatePipe(IDcat(PRJNAME, _CMDIN), 0622)) == -1 )
	{	return 1;
	}else
	{	fdCmdIn = fd;
	}
	if ( (fd=CreatePipe(IDcat(PRJNAME, _CMDOUT), 0644)) == -1 )
	{	return 1;
	}else
	{	fdCmdOut = fd;
	}
	return 0;
}

#define STRLEN(constStr)	(sizeof(constStr)-1)

int IsComment(char *line)
{	
	for (; *line && isspace(*line); line++)
	{;}
	return (*line == '#');
}

/*
*	Close a port temporarily, for Cal. or Drawtest
*/
void ClosePort(char *name)
{	TouchPanel *p, *pre;

	for (pre=NULL, p=panelList; p; pre=p, p=p->next)
	{	if ( !strcmp(name, p->device) )
		{	close(p->fd);
			FD_CLR(p->fd, &fdSet);
			if ( fdMax == p->fd )
			{	fdMax--;
			}
			LeaveList(&panelList, p, pre);
		}
	}
}

TouchPanel *UpdatePanel(TouchPanel *p)
{	EEPROM_UNIT dat;

	printf("Update %s....", p->device);
	fflush(stdout);

	if ( _ReadEEPROM(p->fd, EPR_CheckSum, &dat) )
	{	if ( strncmp(p->device, USBPORT, STRLEN(USBPORT)) )
		{	ClosePort(p->device);
			free(p);
			printf("device removed\n");
		}else
		{	printf("USB unplugged\n");
		}
		return NULL;
	}
	if ( dat == p->chksum )
	{	if ( ! p->present )	// for un-plugged USB device
		{	p->present = 1;
			fdMax = MAX(fdMax, p->fd);
			FD_SET(p->fd, &fdSet);
			printf("USB plugged\n");
			SendRescanCmd();
		}else
		{	printf("existing device\n");
		}
		return NULL;
	}else
	{	TouchPanel *ptr;
		printf("new panel\n");
		ClosePort(p->device);
		ptr = NewPanel(p->device, 9600);
		free(p);
		SendRescanCmd();
		return ptr;
	}
}
				
void ReadDataFromCfgFile(FILE *fp)
{	char line[200], parm[100], value[100];
	char *equAddr;

	TouchSetting.mode				= DefaultMouseMode;
	TouchSetting.dblClickSpeed		= DefaultDblClickSpeed;
	TouchSetting.dblClickArea			= DefaultDblClickArea;
	TouchSetting.rClickTool			= DefaultRClickTool;
	TouchSetting.soundMode			= DefaultSoundMode;
	TouchSetting.CTouch.bAutoRightButtonEnabled = 1;
	TouchSetting.CTouch.bConstTouchEnabled = 1;
	TouchSetting.CTouch.lEventDelay = 300;
    	TouchSetting.CTouch.cx = 0;
    	TouchSetting.CTouch.cy = 0;
    	TouchSetting.CTouch.iAutoRightButtonTime = 1000; /* 2000 ms */
    	TouchSetting.CTouch.iConstTouchArea = 25;
    	TouchSetting.CTouch.TrackingTime.tv_sec =0;
    	TouchSetting.CTouch.TrackingTime.tv_usec = 0;
	TouchSetting.bHoldReset = 0;
	TouchSetting.bIdleReset = 0;
	TouchSetting.IdleTime = 300;
	TouchSetting.HoldTime = 30;


	for ( ; fgets(line, sizeof line, fp); )
	{	if (IsComment(line))
		{	continue;
		}
		equAddr = strchr(line, '=');
		if (!equAddr)
		{	continue;
		}
		*(equAddr++) = parm[0] = '\0';
		sscanf(line, "%s", parm);

		#define StoreParm(storage, name, src)	\
		{	/*(storage) = Default##name;*/		\
			sscanf(src, "%d", &(storage));		\
		}

		if ( !strcasecmp(parm, _MouseMode) )
		{	if ( sscanf(equAddr, "%s", value) )
			{	if ( !strcasecmp(value, sTM_DRAWING) )
				{	TouchSetting.mode = TM_DRAWING;
				}else if ( !strcasecmp(value, sTM_DESKTOP) )
				{	TouchSetting.mode = TM_DESKTOP;
				}
			}
			printf("Mouse Mode: %s\n", TouchSetting.mode == TM_DRAWING ?
										sTM_DRAWING : sTM_DESKTOP );
		}else if ( !strcasecmp(parm, _Sound) )
		{	if ( sscanf(equAddr, "%s", value) )
			{	if ( !strcasecmp(value, sSM_NONE) )
				{	TouchSetting.soundMode = SM_NONE;
				}else if ( !strcasecmp(value, sSM_DOWN) )
				{	TouchSetting.soundMode = SM_DOWN;
				}else if ( !strcasecmp(value, sSM_UP) )
				{	TouchSetting.soundMode = SM_UP;
				}
			}
			printf("Sound Mode: %s\n",
					TouchSetting.soundMode == SM_NONE ? sSM_NONE :
					(TouchSetting.soundMode == SM_DOWN ? sSM_DOWN : sSM_UP));
		}else if ( !strcasecmp(parm, _DblClickSpeed) )
		{	StoreParm(TouchSetting.dblClickSpeed, DblClickSpeed, equAddr);
			printf("Dbl-Click Speed: %d\n", TouchSetting.dblClickSpeed);
		}else if ( !strcasecmp(parm, _DblClickArea) )
		{	StoreParm(TouchSetting.dblClickArea, DblClickArea, equAddr);
			printf("Dbl-Click Area: %d\n", TouchSetting.dblClickArea);
		}else if ( !strcasecmp(parm, _RClickTool) )
		{	StoreParm(TouchSetting.rClickTool, RClickTool, equAddr);
			printf("R-Click Tool: %d\n", TouchSetting.rClickTool);
		}else if( !strcasecmp(parm,"ConstTouch" ) )
		{
			if ( sscanf(equAddr, "%s", value) )
			{
				if( !strcasecmp( value,"Disabled"))
				{
					 TouchSetting.CTouch.bConstTouchEnabled = 0;
				}
			}
		}else if( !strcasecmp(parm,"AutoRightClick" ) )
		{
			if ( sscanf(equAddr, "%s", value) )
			{
				if( !strcasecmp( value,"Disabled") )
				{
					 TouchSetting.CTouch.bAutoRightButtonEnabled = 0;
				}
			}
		}
		else if( !strcasecmp(parm,"AutoRightClickTime"))
		{
			int Value;
			if( sscanf( equAddr,"%d",&Value) )
			{
				TouchSetting.CTouch.iAutoRightButtonTime = Value;
			}
		}else if( !strcasecmp(parm,"ConstTouchArea"))
		{
			int Value;
			if( sscanf( equAddr,"%d",&Value))
			{
				TouchSetting.CTouch.iConstTouchArea = Value;
			}
		} else if( !strcasecmp(parm,"XEventDelay"))
		{
			int Value;
			if( sscanf( equAddr,"%d",&Value))
			{
				TouchSetting.CTouch.lEventDelay = Value;
			}
		}
		else if( !strcasecmp( parm,"IdleReset") )
		{
                        if ( sscanf(equAddr,"%s", value ) )
                        {
				if( !strcasecmp( value,"Enabled") )
				{	
					TouchSetting.bIdleReset = 1;
				}
			}
		}
		else if( !strcasecmp( parm,"HoldReset") )
		{
                        if ( sscanf(equAddr,"%s", value ) )
                        {
				if( !strcasecmp( value,"Enabled") )
				{	
					TouchSetting.bHoldReset = 1;
				}
			}
		}
		else if( !strcasecmp( parm,"IdleTime") )
		{
			int Value;
			if( sscanf( equAddr,"%d",&Value))
			{
				TouchSetting.IdleTime = Value;
			}
		}
		else if( !strcasecmp( parm,"HoldTime") )
		{
			int Value;
			if( sscanf( equAddr,"%d",&Value))
			{
				TouchSetting.HoldTime = Value;
			}
		}
		else if ( !strcasecmp(parm, _Port) )
		{	char name[300];

			if ( sscanf(equAddr, "%s", name) )
			{	TouchPanel *p;

				for (p=panelList; p; p = p->next)
				{	if ( !strcmp(p->device, name) )
					{	break;
					}
				}
				if (p)
				{	p = UpdatePanel(p);
				} else
				{	p = NewPanel(name, 9600);
				}
				if( p )
				{	JoinList(&panelList, p);
			
					fdMax = MAX(fdMax, p->fd);
					FD_SET(p->fd, &fdSet);
				}
			}
		}
	}
}

void RClickToolKillOld()
{	FILE *fp;
	int pid;

	if ( (fp = fopen(RCLICK_LOCK, "r+")) )
	{	if ( fscanf(fp, "%d", &pid) )
		{	kill(pid, SIGKILL);
		}
		fclose(fp);
	}
}

void undertaker(int dummy)
{	int status;
	
	wait(&status);
	
	RClickPid = 0;
	close(FD_ToRClick);
}


void RClickToolNew(int On)
{	int pid;

	signal(SIGCHLD, undertaker);
	
	pipe(MsgPipe);
	pid = fork();
	if (pid == 0)
	{	char ppidbuf[20];
		char modebuf[20];
		TouchPanel *p;
		for (p=panelList; p; p=p->next)
		{	close(p->fd);
			free(p);
		}
		
		#define CloseIfValid(fd)	\
		{if ( (fd) > 0 ) close(fd);}
		
		CloseIfValid(fdCmdIn);
		CloseIfValid(fdCmdOut);
		CloseIfValid(fdShrIn);
		CloseIfValid(fdShrOut);
		{	int i;
			for (i=0; i<NumOfOutput; i++)
			{	CloseIfValid(fdOuts[i]);
			}
		}
		dup2(FD_FromRClick, 0);
		close(FD_FromRClick);
		close(FD_ToRClick);
		sprintf(ppidbuf, "%d", getppid());
		sprintf(modebuf, "%d", TouchSetting.mode);
		execl(  "/usr/bin/env", "env", "DISPLAY=:0.0",
				IDcat(PRJNAME, _HOME) "diag/rbutton.tcl", 
				IDcat(PRJNAME, _HOME) "image/lbutton.gif",
				IDcat(PRJNAME, _HOME) "image/rbutton.gif",
				ppidbuf,
				On ? "1" : "0",
				modebuf, NULL);
		perror("exec");
		exit(0);
	}else if (pid > 0)
	{	close(FD_FromRClick);
		RClickPid = pid;
	}else
	{	perror("fork");
	}
}

int RClickToolExist()
{	
	if ( RClickPid )
	{	if ( !kill(RClickPid, 0) )
		{	return 1;
		}else
		{	RClickPid = 0;
		}
	}else
	{	RClickToolKillOld();
	}
	return 0;
}

void RClickToolOff()
{	
	if (RClickPid)
	{	char buf[] = "OFF\n";
		write(FD_ToRClick, buf, sizeof buf - 1);
	}
}

void RClickToolOn()
{	if (RClickToolExist())
	{	char buf[] = "ON\n";
		write(FD_ToRClick, buf, sizeof buf - 1);
	}else
	{	RClickToolNew(1);
	}
}



int GetPanelSetting()
{	FILE *file;
	FILE *pAutoRClick;
	int tmpMode;
	
	if ( (file=fopen(IDcat(PRJNAME, _CONF), "r+")) == NULL )
	{	if ( (file=fopen(IDcat(PRJNAME, _CONF), "w+")) == NULL )
		{	perror("reopen");
			return 1;
		}else
		{	fprintf(file, "%s\n", DefaultConfData);
			rewind(file);
		}
	}
	pAutoRClick = fopen( "/etc/tpaneld.ini","r+");

/*	TouchSetting.thisButton			= _LeftButton;
	TouchSetting.nextButton			= _LeftButton;
	TouchSetting.touched			= 0;
*/	TouchSetting.count				= 0;

	#define __INVALID_MODE 	1234
	if (RClickToolExist() && TouchSetting.rClickTool)
	{	tmpMode = TouchSetting.mode;
	}else
	{	tmpMode = __INVALID_MODE;
	}
	ReadDataFromCfgFile(file);
	if( pAutoRClick )
	{
		ReadDataFromCfgFile(pAutoRClick);
	}

	if (RClickToolExist())
	{	if (TouchSetting.rClickTool)
		{	if ( tmpMode != __INVALID_MODE )
				TouchSetting.mode = tmpMode;
			RClickToolOn();
		}else
		{	RClickToolOff();
		}
	}else
	{	RClickToolNew(TouchSetting.rClickTool);
	}
	printf("Real Mouse Mode: %s\n", TouchSetting.mode == TM_DRAWING ?
										sTM_DRAWING : sTM_DESKTOP );

	if ( panelList == NULL )	/* there is NO PANEL setting read */
	{	puts("warning: no panel found");
	}
	fclose(file);
	if( pAutoRClick ) 
	{
		fclose( pAutoRClick );
	}
	return 0;
}

void DaemonReset()
{	
	#ifdef CMD_DEBUG
		puts("SIGHUP RESET");
	#endif
	if ( GetPanelSetting() )
	{	exit(1);
	}
	if (PortShared[0])
	{	char buf[500];
		void SharePort(char *name);
		strncpy(buf, PortShared, sizeof buf);
		SharePort(buf);
	}
}

void tpd_killed(int signo)
{	switch (signo)
	{
	case SIGHUP:
		DaemonReset();
		syslog( LOG_DEBUG|LOG_USER," DaemonReset() called...\n " );
		break;
	case SIGUSR1:
		puts("killed by new one");
		syslog( LOG_DEBUG|LOG_USER," killed by new one...\n " );
		exit(0);
	}
}


int CreateDatPipe()
{	int i;
	char name[100];

	for (i=0; i<NumOfOutput; i++)
	{	sprintf(name, IDcat(PRJNAME, _DAT) "%d", i);
		fdOuts[i] = CreatePipe(name, 0622);
		if ( fdOuts[i] == -1 )
		{	return 1;	
		}
	}
	return 0;
}




void SharePort(char *name)
{	TouchPanel *p, *pre;
	int changed = 0, shareOK = 0;

	for (pre=NULL, p=panelList; p; pre=p, p=p->next)
	{	int sharing;
		
		sharing = !strcmp(name, p->device) && p->present;
		if (sharing)
		{	char buf[] = "SHROK\n";
			strncpy(PortShared, p->device, sizeof PortShared);
			shareOK = 1;
			write(fdCmdOut, buf, (sizeof buf)-1);
		}
		if (p->sharing != sharing)
		{	changed++;
		}
		p->sharing = sharing;
		p->shrlen = p->sharing ? p->shrlen : 0;
	}
	if (changed)
	{	char c = 0;
		write(fdShrOut, &c, sizeof c);
		EmptyPipe(fdShrOut);
	}
	if (!shareOK)
	{	PortShared[0] = '\0';
	}
}



void FlushOutput()
{	int i;
	for (i=0; i<NumOfOutput; i++)
	{	write(fdOuts[i], &i, sizeof i);
		EmptyPipe(fdOuts[i]);
	}
}

void SetEEPROM(char *cmd)
{	EEPROM eeprom;
	TouchPanel *p;
	int i, len;
	char *wp;
	
	if ( !(wp=strrchr(cmd, ':')) )
	{	// cmd format error
		return;
	}
	*(wp++) = '\0';
	for (p=panelList; p; p=p->next)
	{	if (!strcmp(p->device, cmd))
		{	break;
		}
	}
	if (!p)
	{	// no panel match
		return;
	}
	len = strlen(wp);
	if (len<4*EEPROM_SIZE)
	{	// data len error
		return;
	}	
	for (i=0; i<EEPROM_SIZE; i++, wp+=4)
	{	char tmp = *(wp+4);
		int rval;
		
		*(wp+4) = '\0';
		if (!sscanf(wp, "%X", &rval))
		{	break;
		}
		*(wp+4) = tmp;
		eeprom[i] = rval;
	}
	if (i<EEPROM_SIZE)
	{	// data format error
		return;
	}
	if (CheckEEPROM(eeprom))
	{	// checksum error
		return;
	}

	SET_PANEL_MAPPING_FR_EEPROM(p, eeprom);
	
	p->chksum = eeprom[EPR_CheckSum];
}

void ProcessCommand()
{	char buf[500];
	char cmd[500];
	int id;
	int rval;
	
	rval = read(fdCmdIn, buf, sizeof buf);
	if ( rval >= 0 )
	{	buf[rval] = '\0';
	}
	if ( rval > 0 && sscanf(buf, "[%d]%s", &id, cmd) == 2 )
	{	switch (id)
		{
		case AP2D_CLOSEPORT:
			#ifdef CMD_DEBUG
				printf("CMD CLOSEPORT : %s\n", cmd);
			#endif
			ClosePort(cmd);
			break;	
		case AP2D_RESET:
			#ifdef CMD_DEBUG
				printf("CMD RESET\n");
			#endif
			DaemonReset();
			break;
		case AP2D_SUICIDE:
			#ifdef CMD_DEBUG
				printf("CMD KILL\n");
			#endif
			exit(0);
		case AP2D_SHOWSTATUS:
			#ifdef CMD_DEBUG
				printf("CMD SHOWPANEL\n");
			#endif
			DumpSetting();
			DumpPanel();
			break;
		case AP2D_SHAREPORT:
			#ifdef CMD_DEBUG
				printf("CMD SHAREPORT: %s\n", cmd);
			#endif
			SharePort(cmd);
			break;
		case AP2D_SETMODE:
			#ifdef CMD_DEBUG
				printf("CMD SETMODE: %s\n", cmd);
			#endif
			TouchSetting.mode = atoi(cmd);
			if (RClickPid)
			{	char buf[] = "?\n";
				buf[0] = '0' + TouchSetting.mode;
				write(FD_ToRClick, buf, sizeof buf - 1);
			}
			break;
		case AP2D_SETSOUNDMODE:
			#ifdef CMD_DEBUG
				printf("CMD SETSOUNDMODE: %s\n", cmd);
			#endif
			TouchSetting.soundMode = atoi(cmd);
			break;
		case AP2D_SETDCSPEED:
			#ifdef CMD_DEBUG
				printf("CMD SETDCSPEED: %s\n", cmd);
			#endif
			TouchSetting.dblClickSpeed = atoi(cmd);
			break;
		case AP2D_SETDCAREA:
			#ifdef CMD_DEBUG
				printf("CMD SETDCAREA: %s\n", cmd);
			#endif
			TouchSetting.dblClickArea = atoi(cmd);
			break;
		case AP2D_RCLICK:
			#ifdef CMD_DEBUG
				printf("CMD RCLICK: %s\n", cmd);
			#endif
			DumpButton;
			if (TouchSetting.touched)
			{	TouchSetting.nextButton = _RightButton;
			}else
			{	TouchSetting.thisButton = _RightButton;
			}
			DumpButton;
			break;
		case AP2D_RCLICKCANCEL:
			#ifdef CMD_DEBUG
				printf("CMD RCLICKCANCEL: %s\n", cmd);
			#endif
			DumpButton;
			if (TouchSetting.touched)
			{	TouchSetting.nextButton = _LeftButton;
			}else
			{	TouchSetting.thisButton = _LeftButton;
			}
			DumpButton;
			break;
		case AP2D_RCLICKON:
			#ifdef CMD_DEBUG
				printf("CMD RCLICKON: %s\n", cmd);
			#endif
			RClickToolOn();
			break;
		case AP2D_RCLICKOFF:
			#ifdef CMD_DEBUG
				printf("CMD RCLICKOFF: %s\n", cmd);
			#endif
			RClickToolOff();
			break;
		case AP2D_RCLICKDELAYON:
			#ifdef CMD_DEBUG
				printf("CMD RCLICKDELAYON: %s\n", cmd);
			#endif
			sleep(RCLICK_DELAY);
			RClickToolOn();
			break;
		case AP2D_XMODULEREADY:
			#ifdef CMD_DEBUG
				printf("CMD XMODULEREADY: %s\n", cmd);
			#endif
			RClickToolNew(0);
			break;
		case AP2D_FLUSHOUTPUT:
			#ifdef CMD_DEBUG
				printf("CMD FLUSHOUTPUT: %s\n", cmd);
			#endif
			FlushOutput();
			break;
		case AP2D_SETEEPROM:
			#ifdef CMD_DEBUG
				printf("CMD SETEEPROM: %s\n", cmd);
			#endif
			SetEEPROM(cmd);
			break;
		case AP2D_GETMODE:
			#ifdef CMD_DEBUG
				printf("CMD GETMODE: %s\n", cmd);
			#endif
			{	char buf[30];
				sprintf(buf, "M%d\n", TouchSetting.mode);
				write(fdCmdOut, buf, strlen(buf));
			}
			break;
		}
	}
}

void LogCmdEvent( TouchPanel *panel )
{
	short *p;
	unsigned char chCmdBase = 'a' + 2;
	p = ( short *)&panel->chCmdBuffer[1];
//	 syslog( LOG_DEBUG|LOG_USER," CmdLen = %d\n", panel->iCmdLen );
	if( panel->chCmdBuffer[0] == chCmdBase )
	{
	  syslog( LOG_DEBUG|LOG_USER," Firmware Reset Ok 1 \n" );
	  syslog( LOG_DEBUG|LOG_USER," Stray = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 1 )
	{
	  syslog( LOG_DEBUG|LOG_USER," Invalid Stray when update stray \n" );
	  syslog( LOG_DEBUG|LOG_USER," StrayValue = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 2 )
	{
	  syslog( LOG_DEBUG|LOG_USER," Invalid ADC \n" );
	  syslog( LOG_DEBUG|LOG_USER," Invalid ADC = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 3 )
	{
	  syslog( LOG_DEBUG|LOG_USER," AD Deviation too much \n" );
	  syslog( LOG_DEBUG|LOG_USER," ADC = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 4 )
	{
	  syslog( LOG_DEBUG|LOG_USER," No Error \n" );
	  syslog( LOG_DEBUG|LOG_USER," ADC = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 5 )
	{
	  syslog( LOG_DEBUG|LOG_USER," Stray Error !!! Reset automatically!!!\n" );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 6 )
	{
	  syslog( LOG_DEBUG|LOG_USER," ADC Seems Error like LCD OFF \n" );
	  syslog( LOG_DEBUG|LOG_USER," ADC = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}
	if( panel->chCmdBuffer[0] == chCmdBase + 7 )
	{
	  syslog( LOG_DEBUG|LOG_USER," ADC Seems Error like LCD ON or ESD!\n" );
	  syslog( LOG_DEBUG|LOG_USER," ADC = %d, %d, %d, %d\n", p[0],p[1],p[2],p[3] );
	}


}

int CommandFilter( TouchPanel *panel )
{
	int i;
	for( i = 0; i < panel->len; i++ )
	{
		if( !panel->bPtPacket && !panel->bCmdPacket )
		{
			if( panel->buf[ i ] == _SOP )
			{
				panel->bCmdPacket = 1;
				panel->iCmdBytesRecved = 0; 
				panel->iCmdLen = 0;
			}
			else if( panel->buf[ i ] &0x80 )
			{
				panel->bPtPacket = 1;
				panel->bPtRecved = 1;
			} 
		}
		else if( panel->bCmdPacket )
		{
			if( 0 == panel->iCmdLen )
			{
				panel->iCmdLen = panel->buf[i];
				panel->iCmdBytesRecved = 0;
			}
			else
			{
				panel->chCmdBuffer[ panel->iCmdBytesRecved ] = panel->buf[ i ];
				panel->iCmdBytesRecved++; 
				if( panel->iCmdBytesRecved == panel->iCmdLen || panel->iCmdBytesRecved > 16 )
				{
					LogCmdEvent( panel );
					panel->bCmdPacket = 0;
					panel->iCmdBytesRecved = 0; 
					panel->iCmdLen = 0;
					panel->bPtPacket = 0;
				}
			}
		}
		else if( panel->bPtPacket )
		{
			panel->bPtRecved++;
			if( panel->bPtRecved >= 5)
			{
					panel->bCmdPacket = 0;
					panel->iCmdBytesRecved = 0; 
					panel->iCmdLen = 0;
					panel->bPtPacket = 0;

			}
		}
	}
	return 0;

}

void ProcessPanel(TouchPanel *panel)
{	if ( DoRead(panel) > 0 )
	{	panel->buf[panel->len] = '\0';

	#ifdef OUTPUT_DEBUG
		{	int i;
			printf("All dat in %s's buf\n", panel->device);
					for (i=0; i<panel->len; i++)
					{	printf("%02X ", (unsigned char)panel->buf[i]);
					}
			puts("");
		}
	#endif
	
		CommandFilter( panel );
		while ( panel->len >= FrameSize )
		{	char *frame = DataFilter(panel);
			if (frame)
			{	ReportFrame(panel, frame);
			}
		}
	}
	else
	{
		time_t CurrentTime = time( NULL );
		if( ( CurrentTime - panel->LastResetTime ) > TouchSetting.IdleTime && TouchSetting.bIdleReset )
		{
			//If Idle enough, just restart it!!!
			syslog( LOG_DEBUG|LOG_USER," Start Idle Reset Now ... \n" );
			  DoResetPanel( panel );
		}
		
	}
}

void ProcessShare(void)
{	char buf[1000];
	int rval;
	TouchPanel *p, *pre;
	
	rval = read(fdShrIn, buf, sizeof buf);

	if (rval > 0)
	{	for (pre=NULL, p=panelList; p; pre=p, p=p->next)
		{	if (p->sharing)
			{	write(p->fd, buf, rval);
				break;
			}
		}
	}
}

void EndlessServiceLoop()
{	fd_set fds;
	TouchPanel *p;
	struct timeval timeout;
	timeout.tv_sec = 30;
	timeout.tv_usec = 0;

	while(1)
	{	fds = fdSet;
		timeout.tv_sec = 30;
		timeout.tv_usec = 0;
		if ( select( fdMax+1, &fds, NULL, NULL, NULL) > 0 )
		{	if ( FD_ISSET(fdCmdIn, &fds) )	// here comes a command?
			{	ProcessCommand();		
			}
			if ( FD_ISSET(fdShrIn, &fds) )	// data from ap, forward it
			{	ProcessShare();		
			}
			for (p=panelList; p; p=p->next)	// data from some panels?
			{	if ( FD_ISSET(p->fd, &fds) )
				{	ProcessPanel(p);
				}
				else
				{
					time_t CurrentTime = time( NULL );
					if(( CurrentTime - p->LastResetTime ) >   
	                                     TouchSetting.IdleTime && TouchSetting.bIdleReset )
					{
					//If Idle enough, just restart it!!!
					  syslog( LOG_DEBUG|LOG_USER," Start Idle Reset Now ... \n" );
					  DoResetPanel( p );
					}
				}
			}
		}
		else
		{
			for (p=panelList; p; p=p->next)	// data from some panels?
			{	
				time_t CurrentTime = time( NULL );
				if(( CurrentTime - p->LastResetTime ) > TouchSetting.IdleTime && TouchSetting.bIdleReset  )
				{
				//If Idle enough, just restart it!!!
				syslog( LOG_DEBUG|LOG_USER," Start Idle Reset Now ... \n" );
				  DoResetPanel( p );
				}
			}
		}
	}
}

void InitMyFilter(FILTERCOEFF *fp, char **argv)
{	InitFilterDefault(fp);
	
	for ( ; *argv; argv++ )
	{	int c[10];
		/* Coef0~3, C3Min, C3Max, Thold */
		if ( sscanf(*argv, "%d,%d,%d,%d,%d,%d,%d",
					c+0, c+1, c+2, c+3, c+4, c+5, c+6) == 7 )
		{	InitFilter(fp, c[0], c[1], c[2], c[3], c[4], c[5]);
   SetFilterThreshold(fp, c[6]);
  }
 }
}


void GetOpt(int argc, char **argv)
{	char c;

	for ( ; (c=getopt(argc, argv, "dfr")) != -1; )
	{	switch (c)
		{
		case 'd': optNotDaemon = 1; break;
		case 'f': optForce = 1; break;
		}
	}
}

int main(int argc, char **argv)
{	int oldpid = 0;

	GetOpt(argc, argv);
	syslog( LOG_DEBUG|LOG_USER," TPaneld is Running...\n " );

	if ( !optNotDaemon ) daemon(0, 0);

	if ( !IsUnique(&oldpid) && !optForce )
	{	char buf[100];
		sprintf(buf, "echo '[%d]x' >> %s", AP2D_RESET, IDcat(PRJNAME, _CMDIN));
		system(buf);
		printf("Reset Sent\n");
		return 0;		/* another tpaneld is running */
	}
	if ( oldpid )
	{	kill(oldpid, SIGKILL);
		waitpid(oldpid, NULL, 0);
		syslog( LOG_DEBUG|LOG_USER," Kill old PID now...\n " );
	}
	SetLock();
	
	InitMyFilter(&TouchSetting.filter, argv);

	signal(SIGHUP,	tpd_killed);	/* register HUP signal handler */

	FD_ZERO(&fdSet);
	if ( GetPanelSetting() )	/* read conf file and create Panels */
	{	return 1;
	}
	if ( CreateCmdPipe() )
	{	printf("Failed to CreateCmdPipe\n");
		return 1;
	}
	fdMax = fdCmdIn;
	FD_SET(fdCmdIn, &fdSet);
	if ( CreateSharePipe() )
	{	printf("Failed to CreateSharePipe\n");
		return 1;
	}
	fdMax = MAX(fdShrIn, fdCmdIn);
	FD_SET(fdShrIn, &fdSet);
	if ( CreateDatPipe() )
	{	printf("Failed to CreateDatPipe\n");
		return 1;
	}
	syslog( LOG_DEBUG|LOG_USER," EndlessServiceLoop() is running now...\n " );
	EndlessServiceLoop();
	return 0;
}

