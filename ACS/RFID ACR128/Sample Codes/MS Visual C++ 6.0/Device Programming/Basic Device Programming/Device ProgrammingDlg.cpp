//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              DeviceProgrammingDlg.cpp
//
//  Description:       This sample program demonstrates how to set the buzzer and LED states
//					   of the ACR128 reader. You can also customize the settings on how the
//					   buzzer and LED of the reader will behave.
//
//  Author:            Wazer Emmanuel R. Benal
//
//	Date:              June 3, 2008
//
//	Revision Trail:   (Date/Author/Description)

//=====================================================================


#include "stdafx.h"
#include "Device Programming.h"
#include "Device ProgrammingDlg.h"

#define INVALID_SW1SW2 -450
#define IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND SCARD_CTL_CODE(2079)
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)

//Device Programming Inlude File
#include "WINSCARD.h"

//Global Variables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	LPCBYTE					pbSend;
	DWORD					dwSend, dwRecv, size = 64;
	LPBYTE					pbRecv;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode;
    char					readerName [256];
	DWORD					SendLen, RecvLen, ByteRet;;
	BYTE					SendBuff[262], RecvBuff[262];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	CDeviceProgrammingDlg	*pThis = NULL;

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void ClearBuffers();
static CString GetScardErrMsg( int code );
int CallCardControl();
void DisplayOut( CString str, COLORREF color );

//Clears the buffers of any data
void ClearBuffers()
{

	int index;
	
	for( index = 0; index <= 262; index++ )
	{
	
		SendBuff[index] = 0x00;
		RecvBuff[index] = 0x00;
	
	}

}

//Displays the message in the rich edit box with the respective color
void DisplayOut( CString str, COLORREF color )
{

	int nOldLines = 0,
		nNewLines = 0,
		nScroll = 0;
	long nInsertPoint = 0;
	CHARFORMAT cf;

	//Save number of lines before insertion of new text
	nOldLines = pThis->rbResult.GetLineCount();

	//Initialize character format structure
	cf.cbSize		= sizeof( CHARFORMAT );
	cf.dwMask		= CFM_COLOR;
	cf.dwEffects	= 0;	// To disable CFE_AUTOCOLOR
	cf.crTextColor	= color;

	//Set insertion point to end of text
	nInsertPoint = pThis->rbResult.GetWindowTextLength();
	pThis->rbResult.SetSel( nInsertPoint, -1 );
	
	//Set the character format
	pThis->rbResult.SetSelectionCharFormat( cf );

	//Insert string at the current caret poisiton
	pThis->rbResult.ReplaceSel( str );

	nNewLines = pThis->rbResult.GetLineCount();
	nScroll	= nNewLines - nOldLines;
	pThis->rbResult.LineScroll( 1 );
	
}

int CallCardControl()
{

	char tempstr[262];
	char tempstr2[262];
	int index;

	sprintf( tempstr,"< SCardControl: " );
	for( index = 0; index <= SendLen - 1; index++ )
	{
	
		sprintf( tempstr, "%s %02X", tempstr, SendBuff[index] );
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );
		
	retCode = SCardControl( hCard,
							IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND,
							&SendBuff,
							SendLen,
							&RecvBuff,
							RecvLen,
							&ByteRet );

	if( retCode != SCARD_S_SUCCESS )
	{
		
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return retCode;
	
	}

	sprintf( tempstr2, ">" );
	for( index = 0; index <= RecvLen - 1; index++ )
	{
	
		sprintf( tempstr2, "%s %02X",tempstr2, RecvBuff[index] );
	
	}
	sprintf( tempstr2, "%s \n", tempstr2 );
	DisplayOut( tempstr2, BLACK );

	return retCode;

}

//SmartCard Error Handler
static CString GetScardErrMsg(int code)
{
	switch( code )
	{
	// Smartcard Reader interface errors
	case SCARD_E_CANCELLED:
		return ("The action was canceled by an SCardCancel request.\n");
		break;
	case SCARD_E_CANT_DISPOSE:
		return ("The system could not dispose of the media in the requested manner.\n");
		break;
	case SCARD_E_CARD_UNSUPPORTED:
		return ("The smart card does not meet minimal requirements for support.\n");
		break;
	case SCARD_E_DUPLICATE_READER:
		return ("The reader driver didn't produce a unique reader name.\n");
		break;
	case SCARD_E_INSUFFICIENT_BUFFER:
		return ("The data buffer for returned data is too small for the returned data.\n");
		break;
	case SCARD_E_INVALID_ATR:
		return ("An ATR string obtained from the registry is not a valid ATR string.\n");
		break;
	case SCARD_E_INVALID_HANDLE:
		return ("The supplied handle was invalid.\n");
		break;
	case SCARD_E_INVALID_PARAMETER:
		return ("One or more of the supplied parameters could not be properly interpreted.\n");
		break;
	case SCARD_E_INVALID_TARGET:
		return ("Registry startup information is missing or invalid.\n");
		break;
	case SCARD_E_INVALID_VALUE:
		return ("One or more of the supplied parameter values could not be properly interpreted.\n");
		break;
	case SCARD_E_NOT_READY:
		return ("The reader or card is not ready to accept commands.\n");
		break;
	case SCARD_E_NOT_TRANSACTED:
		return ("An attempt was made to end a non-existent transaction.\n");
		break;
	case SCARD_E_NO_MEMORY:
		return ("Not enough memory available to complete this command.\n");
		break;
	case SCARD_E_NO_SERVICE:
		return ("The smart card resource manager is not running.\n");
		break;
	case SCARD_E_NO_SMARTCARD:
		return ("The operation requires a smart card, but no smart card is currently in the device.\n");
		break;
	case SCARD_E_PCI_TOO_SMALL:
		return ("The PCI receive buffer was too small.\n");
		break;
	case SCARD_E_PROTO_MISMATCH:
		return ("The requested protocols are incompatible with the protocol currently in use with the card.\n");
		break;
	case SCARD_E_READER_UNAVAILABLE:
		return ("The specified reader is not currently available for use.\n");
		break;
	case SCARD_E_READER_UNSUPPORTED:
		return ("The reader driver does not meet minimal requirements for support.\n");
		break;
	case SCARD_E_SERVICE_STOPPED:
		return ("The smart card resource manager has shut down.\n");
		break;
	case SCARD_E_SHARING_VIOLATION:
		return ("The smart card cannot be accessed because of other outstanding connections.\n");
		break;
	case SCARD_E_SYSTEM_CANCELLED:
		return ("The action was canceled by the system, presumably to log off or shut down.\n");
		break;
	case SCARD_E_TIMEOUT:
		return ("The user-specified timeout value has expired.\n");
		break;
	case SCARD_E_UNKNOWN_CARD:
		return ("The specified smart card name is not recognized.\n");
		break;
	case SCARD_E_UNKNOWN_READER:
		return ("The specified reader name is not recognized.\n");
		break;
	case SCARD_F_COMM_ERROR:
		return ("An internal communications error has been detected.\n");
		break;
	case SCARD_F_INTERNAL_ERROR:
		return ("An internal consistency check failed.\n");
		break;
	case SCARD_F_UNKNOWN_ERROR:
		return ("An internal error has been detected, but the source is unknown.\n");
		break;
	case SCARD_F_WAITED_TOO_LONG:
		return ("An internal consistency timer has expired.\n");
		break;
	case SCARD_W_REMOVED_CARD:
		return ("The smart card has been removed and no further communication is possible.\n");
		break;
	case SCARD_W_RESET_CARD:
		return ("The smart card has been reset, so any shared state information is invalid.\n");
		break;
	case SCARD_W_UNPOWERED_CARD:
		return ("Power has been removed from the smart card and no further communication is possible.\n");
		break;
	case SCARD_W_UNRESPONSIVE_CARD:
		return ("The smart card is not responding to a reset.\n");
		break;
	case SCARD_W_UNSUPPORTED_CARD:
		return ("The reader cannot communicate with the card due to ATR string configuration conflicts.\n");
		break;
	}
	return ("Error is not documented.\n");
}

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingDlg dialog

CDeviceProgrammingDlg::CDeviceProgrammingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDeviceProgrammingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDeviceProgrammingDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CDeviceProgrammingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDeviceProgrammingDlg)
	DDX_Control(pDX, IDC_CHECK10, checkGREEN);
	DDX_Control(pDX, IDC_CHECK9, checkRED);
	DDX_Control(pDX, IDC_CHECK8, check8);
	DDX_Control(pDX, IDC_CHECK7, check7);
	DDX_Control(pDX, IDC_CHECK6, check6);
	DDX_Control(pDX, IDC_CHECK5, check5);
	DDX_Control(pDX, IDC_CHECK4, check4);
	DDX_Control(pDX, IDC_CHECK3, check3);
	DDX_Control(pDX, IDC_CHECK2, check2);
	DDX_Control(pDX, IDC_CHECK1, check1);
	DDX_Control(pDX, IDC_BUTTON11, btnQuit);
	DDX_Control(pDX, IDC_BUTTON10, btnReset);
	DDX_Control(pDX, IDC_BUTTON9, btnClear);
	DDX_Control(pDX, IDC_BUTTON8, btnSetStates);
	DDX_Control(pDX, IDC_BUTTON7, btnGetStates);
	DDX_Control(pDX, IDC_EDIT1, tbValue);
	DDX_Control(pDX, IDC_BUTTON6, btnBuzzDur);
	DDX_Control(pDX, IDC_BUTTON5, btnSetLED);
	DDX_Control(pDX, IDC_BUTTON4, btnGetLED);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_BUTTON3, btnGetFW);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CDeviceProgrammingDlg, CDialog)
	//{{AFX_MSG_MAP(CDeviceProgrammingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON3, OnGetFW)
	ON_BN_CLICKED(IDC_BUTTON4, OnGetLED)
	ON_BN_CLICKED(IDC_BUTTON5, OnSetLED)
	ON_BN_CLICKED(IDC_BUTTON6, OnBuzzDur)
	ON_BN_CLICKED(IDC_BUTTON7, OnGetStates)
	ON_BN_CLICKED(IDC_BUTTON8, OnSetStates)
	ON_BN_CLICKED(IDC_BUTTON9, OnClear)
	ON_BN_CLICKED(IDC_BUTTON10, OnReset)
	ON_BN_CLICKED(IDC_BUTTON11, OnQuit)
	ON_BN_CLICKED(IDC_BUTTON12, OnStartBuzz)
	ON_BN_CLICKED(IDC_BUTTON13, OnStopBuzz)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingDlg message handlers

BOOL CDeviceProgrammingDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIconBig,	TRUE);		// Set big icon
	SetIcon(m_hIconSmall,	FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	pThis = this;
	DisplayOut( "Program Ready\n", GREEN );
	cbReader.SetWindowText( "" );
	btnConnect.EnableWindow( false );
	btnGetFW.EnableWindow( false );
	btnGetLED.EnableWindow( false );
	btnSetLED.EnableWindow( false );
	btnGetStates.EnableWindow( false );
	btnSetStates.EnableWindow( false );
	btnReset.EnableWindow( false );
	btnBuzzDur.EnableWindow( false );
	tbValue.EnableWindow( false );
	check1.EnableWindow( false );
	check2.EnableWindow( false );
	check3.EnableWindow( false );
	check4.EnableWindow( false );
	check5.EnableWindow( false );
	check6.EnableWindow( false );
	check7.EnableWindow( false );
	check8.EnableWindow( false );
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CDeviceProgrammingDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CDeviceProgrammingDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIconSmall);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CDeviceProgrammingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CDeviceProgrammingDlg::OnInit() 
{

	int len = 64;
	int index;

	//Establish Context
	retCode = SCardEstablishContext( SCARD_SCOPE_USER,
									 NULL,
									 NULL,
									 &hContext );
	
	if( retCode != SCARD_S_SUCCESS )
	{
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	//List PC/SC Card Readers
	size = 256;
	retCode = SCardListReaders( hContext,
								NULL,
								readerName,
								&size );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	if( readerName == NULL )
	{
	
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	cbReader.ResetContent();
	char *p = readerName;
	while ( *p )
	{
    	for (int i=0;p[i];i++);
	      i++;
	    if ( *p != 0 )
		{
     		cbReader.AddString( p );
		}
		p = &p[i];
	}

	index = cbReader.FindStringExact( -1, "ACS ACR128U SAM Interface 0" );
	cbReader.SetCurSel( index );

	btnConnect.EnableWindow( true );
	btnReset.EnableWindow( true );

}

void CDeviceProgrammingDlg::OnConnect() 
{
	
	//int i;
	DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];
	int index;

	cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
	//Connect to selected reader
	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_SHARED,
							Protocol,
							&hCard,
							&Protocol );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		index = cbReader.FindStringExact( -1, "ACS ACR128U SAM Interface 0" );
		cbReader.SetCurSel( index );
		
		retCode = SCardConnect( hContext,
								readerName,
								SCARD_SHARE_DIRECT,
								0,
								&hCard,
								&Protocol );

		if( retCode != SCARD_S_SUCCESS )
		{
			
			//Failed to connect to reader
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;

		}
		
	}

	//Successful connection to reader
	IO_REQ.dwProtocol = Protocol;
	IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

	cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
	sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
	DisplayOut( buffer1, GREEN );
	OnGetLED();
	OnGetStates();

	btnGetFW.EnableWindow( true );
	btnGetLED.EnableWindow( true );
	btnSetLED.EnableWindow( true );
	btnGetStates.EnableWindow( true );
	btnSetStates.EnableWindow( true );
	btnBuzzDur.EnableWindow( true );
	tbValue.EnableWindow( true );
	check1.EnableWindow( true );
	check2.EnableWindow( true );
	check3.EnableWindow( true );
	check4.EnableWindow( true );
	check5.EnableWindow( true );
	check6.EnableWindow( true );
	check7.EnableWindow( true );
	check8.EnableWindow( true );

}

//Displays the firmware version of the ACR128 reader
void CDeviceProgrammingDlg::OnGetFW() 
{

	char tempstr[262];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x18;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 35;
	retCode = CallCardControl();

	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;

	}
	
	sprintf( tempstr, "> Firmware Version: " );
	for( index = 5; index <= 24; index++ )
	{
	
		if( RecvBuff[index] != 0x00 )
		{
		
			sprintf( tempstr, "%s%c", tempstr, RecvBuff[index] );
		
		}
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );

}

//Get the current state of the LED
void CDeviceProgrammingDlg::OnGetLED() 
{
	
	ClearBuffers();
	SendBuff[0] = 0x29;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 6;
	retCode = CallCardControl();
	
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;

	}

	switch( RecvBuff[5] )
	{
	
		case 0:
			DisplayOut( "> Currently connect to SAM reader interface\n", BLACK );
			checkRED.SetCheck( true );
			break;
		case 1:
			DisplayOut( "> No PICC found\n", BLACK );
			checkRED.SetCheck( true );
			break;
		case 2:
			DisplayOut( "> PICC is present but not activated\n", BLACK );
			checkRED.SetCheck( true );
			break;
		case 3:
			DisplayOut( "> ICC is present and activated\n", BLACK );
			checkRED.SetCheck( true );
			break;
		case 4:
			DisplayOut( "> ICC is absent or not activated\n", BLACK );
			checkRED.SetCheck( true );
			break;
		case 5:
			DisplayOut( "> ICC is present and activated\n", BLACK );
			checkGREEN.SetCheck( true );
			break;
		case 6:
			DisplayOut( "> ICC is absent or not activated\n", BLACK );
			checkGREEN.SetCheck( true );
			break;
		case 7:
			DisplayOut( "> ICC is operating\n", BLACK );
			checkGREEN.SetCheck( true );
			break;
	}

	if( ( RecvBuff[5] & 0x02 ) != 0 )
	{
	
		checkGREEN.SetCheck( true );
	
	}
	else
	{
	
		checkGREEN.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x01 ) != 0 )
	{
	
		checkRED.SetCheck( true );
	
	}
	else
	{
	
		checkRED.SetCheck( false );
	
	}
	
}

//Sets the LED state of the reader
void CDeviceProgrammingDlg::OnSetLED() 
{
	ClearBuffers();
	SendBuff[0] = 0x29;
	SendBuff[1] = 0x01;
	
	if( checkRED.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x01 );
	
	}
	
	if( checkGREEN.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x02 );
	
	}

	SendLen = 3;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;

	}

}

//Sets the buzzer duration of the ACR128 reader
void CDeviceProgrammingDlg::OnBuzzDur() 
{
	
	char buffer[50];

	tbValue.GetWindowText( buffer, 50 );
	
	//checks if the input is an integer from 1 to 255
	if( atoi( buffer ) == 0 )
	{
	
		tbValue.SetWindowText( "1" );
		tbValue.SetFocus();
	
	}
	else if( atoi( buffer ) < 1 )
	{
	
		tbValue.SetWindowText( "1" );
		tbValue.SetFocus();
	
	}
	else if( atoi( buffer ) > 255 )
	{
	
		tbValue.SetWindowText( "255" );
		tbValue.SetFocus();
	
	}

	ClearBuffers();
	SendBuff[0] = 0x28;
	SendBuff[1] = 0x01;
	SendBuff[2] = atoi( buffer );
	SendLen = 3;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;

	}
	
}

//Reads the current state of the reader
void CDeviceProgrammingDlg::OnGetStates() 
{
	
	ClearBuffers();
	SendBuff[0] = 0x21;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 6;
	
	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	if( ( RecvBuff[5] & 0x01 ) != 0 )
	{
	
		DisplayOut( "> ICC Activation Status LED is enabled\n", BLACK );
		check1.SetCheck( true );
	
	}
	else
	{
	
		DisplayOut( "> ICC Activation Status LED is disabled\n", BLACK );
		check1.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x02 ) != 0)
	{
	
		DisplayOut( "> PICC Polling Status LED is enabled\n", BLACK );
		check2.SetCheck( true );
	}
	else
	{
	
		DisplayOut( "> PICC Polling Status LED is disabled\n", BLACK );
		check2.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x04 ) != 0 )
	{
	
		DisplayOut( "> PICC Activation Status Buzzer is enabled\n", BLACK );
		check3.SetCheck( true );

	}
	else
	{
	
		DisplayOut( "> PICC Activation Status Buzzer is disabled\n", BLACK );
		check3.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x08 ) != 0 )
	{
	
		DisplayOut( "> PICC PPS Status Buzzer is enabled\n", BLACK );
		check4.SetCheck( true );
	
	}
	else
	{
	
		DisplayOut( "> PICC PPS Status Buzzer is disabled\n", BLACK );
		check4.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x10 ) != 0 )
	{
	
		DisplayOut( "> Card Insertion and Removal Events Buzzer is enabled\n", BLACK );
		check5.SetCheck( true );
	
	}
	else
	{
	
		DisplayOut( "> Card Insertion and Removal Events Buzzer is disabled\n", BLACK );
		check5.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x20 ) != 0 )
	{
	
		DisplayOut( "> RC531 Reset Indication Buzzer is enabled\n", BLACK );
		check6.SetCheck( true );
	
	}
	else
	{
	
		DisplayOut( "> RC531 Reset Indication Buzzer is disabled\n", BLACK );
		check6.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x40 ) != 0 )
	{
	
		DisplayOut( "> Exclusive Mode Status Buzzer is enabled\n", BLACK );
		check7.SetCheck( true );
	
	}
	else
	{
	
		DisplayOut( "> Exclusive Mode Status Buzzer is disabled\n", BLACK );
		check7.SetCheck( false );
	
	}

	if( ( RecvBuff[5] & 0x80 ) != 0 )
	{
	
		DisplayOut( "> Card Operation Blinking LED is enabled\n", BLACK );
		check8.SetCheck( true );
	
	}
	else
	{
	
		DisplayOut( "> Card Operation Blinking LED is disabled\n", BLACK );
		check8.SetCheck( false );
	
	}

}

//This function sets the states marked to the reader
void CDeviceProgrammingDlg::OnSetStates() 
{
	
	ClearBuffers();
	SendBuff[0] = 0x21;
	SendBuff[1] = 0x01;
	SendBuff[2] = 0x00;

	if( check1.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x01 );
	
	}

	if( check2.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x02 );
	
	}

	if( check3.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x04 );
	
	}

	if( check4.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x08 );
	
	}

	if( check5.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x10 );
	
	}

	if( check6.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x20 );
	
	}

	if( check7.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x40 );
	
	}

	if( check8.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x80 );
	
	}

	SendLen = 3;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
}

void CDeviceProgrammingDlg::OnClear() 
{

	rbResult.SetWindowText( "" );
	
}

void CDeviceProgrammingDlg::OnReset() 
{

	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	rbResult.SetWindowText( "" );
	OnInitDialog();
	
}

void CDeviceProgrammingDlg::OnQuit() 
{
	
	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	CDialog::OnCancel();
	
}

void CDeviceProgrammingDlg::OnStartBuzz() 
{

	ClearBuffers();
	SendBuff[0] = 0x28;
	SendBuff[1] = 0x01;
	SendBuff[2] = 0xFF;
	SendLen = 3;
	RecvLen = 6;
	
	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CDeviceProgrammingDlg::OnStopBuzz() 
{

	ClearBuffers();
	SendBuff[0] = 0x28;
	SendBuff[1] = 0x01;
	SendBuff[2] = 0x00;
	SendLen = 3;
	RecvLen = 6;
	
	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}
