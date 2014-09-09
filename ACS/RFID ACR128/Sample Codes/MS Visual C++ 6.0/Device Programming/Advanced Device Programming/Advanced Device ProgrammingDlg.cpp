//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              AdvancedDeviceProgrammingDlg.cpp
//
//  Description:       This sample program outlines the steps on how to
//                     execute advanced device-specific functions of ACR128
//
//  Author:            Wazer Emmanuel R. Benal
//
//	Date:              June 12, 2008
//
//	Revision Trail:   (Date/Author/Description)

//====================================================================================================

#include "stdafx.h"
#include "Advanced Device Programming.h"
#include "Advanced Device ProgrammingDlg.h"

//Define constants//////////////////////////////////////////////////
#define IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND SCARD_CTL_CODE(2079)
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 262
////////////////////////////////////////////////////////////////////

//Advanced Device Programming Inlude File
#include "WINSCARD.h"

//Global Variables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	DWORD					dwSend, dwRecv, size = 64;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode;
    char					readerName [256];
	DWORD					SendLen, RecvLen, ByteRet;
	BYTE					SendBuff[MAX], RecvBuff[MAX];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	int						reqtype;
	bool					autodet = false;
	CAdvancedDeviceProgrammingDlg	*pThis = NULL;

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void ClearBuffers();
static CString GetScardErrMsg( int code );
void DisplayOut( CString str, COLORREF color );
int CallCardControl();
void ReadPollingOption();
int PICCpolling();
int HexCheck( char data1, char data2 );
void Initialize();

//This is the timer used for the automatic polling
void CALLBACK TimerFunc ( HWND, UINT, UINT, DWORD )
{

	ClearBuffers();
	SendBuff[0] = 0x22;
	SendBuff[1] = 0x01;
	SendBuff[2] = 0x0A;
	SendLen = 3;
	RecvLen = 6;

	retCode = PICCpolling();
	if( retCode != SCARD_S_SUCCESS )
	{
		
		return;
		
	}

	if( ( RecvBuff[5] & 0x01 ) != 0 )
	{
		
		pThis->tbMessage.SetWindowText( "No card within range" );
		
	}
	else
	{
		
		pThis->tbMessage.SetWindowText( "Card is detected" );
	}

}

//Used by the timer to poll the contactless card
int PICCpolling()
{

	char tempstr[MAX];
	char tempstr2[MAX];
	int index;

	sprintf( tempstr,"< SCardControl: " );
	for( index = 0; index <= SendLen - 1; index++ )
	{
	
		sprintf( tempstr, "%s %02X", tempstr, SendBuff[index] );
	
	}
	sprintf( tempstr, "%s\n", tempstr );
		
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

	return retCode;

}

//Clears the buffers of any data
void ClearBuffers()
{

	int index;
	
	for( index = 0; index <= MAX; index++ )
	{
	
		SendBuff[index] = 0x00;
		RecvBuff[index] = 0x00;
	
	}

}

//Displays the message with the corresponding color
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

//Error checking for inputs that needs to be in hex format
int HexCheck( char data1, char data2 )
{

	int retval = 1;
	bool state1, state2;

	if( data1 == '0' ||
		data1 == '1' ||
		data1 == '2' ||
		data1 == '3' ||
		data1 == '4' ||
		data1 == '5' ||
		data1 == '6' ||
		data1 == '7' ||
		data1 == '8' ||
		data1 == '9' ||
		data1 == 'A' ||
		data1 == 'B' ||
		data1 == 'C' ||
		data1 == 'D' ||
		data1 == 'E' ||
		data1 == 'F' ||
		data1 == 'a' ||
		data1 == 'b' ||
		data1 == 'c' ||
		data1 == 'd' ||
		data1 == 'e' ||
		data1 == 'f' )
	{
	
		state1 = true;
	
	}
	else
	{
	
		state1 = false;
	
	}

	if( data2 == '0' ||
		data2 == '1' ||
		data2 == '2' ||
		data2 == '3' ||
		data2 == '4' ||
		data2 == '5' ||
		data2 == '6' ||
		data2 == '7' ||
		data2 == '8' ||
		data2 == '9' ||
		data2 == 'A' ||
		data2 == 'B' ||
		data2 == 'C' ||
		data2 == 'D' ||
		data2 == 'E' ||
		data2 == 'F' ||
		data1 == 'a' ||
		data1 == 'b' ||
		data1 == 'c' ||
		data1 == 'd' ||
		data1 == 'e' ||
		data1 == 'f' ||
		data2 == NULL )
	{
	
		state2 = true;
	
	}
	else
	{
	
		state2 = false;
	
	}

	if( state1 == true && state2 == true )
	{
	
		retval = 0;
	
	}
	else
	{
	
		retval = 1;
	
	}
				
	return retval;
}

void Initialize()
{
	DisplayOut( "Program Ready\n", GREEN );

	pThis->btnInit.EnableWindow( true );
	pThis->btnReset.EnableWindow( true );
	pThis->btnQuit.EnableWindow( true );
	pThis->rbResult.EnableWindow( true );
	pThis->btnConnect.EnableWindow( false );
	pThis->btnGetOptions.EnableWindow( false );
	pThis->btnOptions.EnableWindow( false );
	pThis->btnAntenna.EnableWindow( false );
	pThis->btnGetAntenna.EnableWindow( false );
	pThis->btnTransOpt.EnableWindow( false );
	pThis->btnGetTransOpt.EnableWindow( false );
	pThis->btnError.EnableWindow( false );
	pThis->btnGetError.EnableWindow( false );
	pThis->btnPICC.EnableWindow( false );
	pThis->btnGetPICC.EnableWindow( false );
	pThis->btnGetPICCType.EnableWindow( false );
	pThis->btnPICCType.EnableWindow( false );
	pThis->btnAuto.EnableWindow( false );
	pThis->btnPPS.EnableWindow( false );
	pThis->btnGetPPS.EnableWindow( false );
	pThis->btnReg.EnableWindow( false );
	pThis->btnGetReg.EnableWindow( false );
	pThis->btnRefresh.EnableWindow( false );
	pThis->btnReset.EnableWindow( false );
	pThis->cbReader.SetWindowText( "" );
	pThis->tbFWI.EnableWindow( false );
	pThis->tbPoll.EnableWindow( false );
	pThis->tbTrans.EnableWindow( false );
	pThis->tbFieldStop.EnableWindow( false );
	pThis->tbRecvGain.EnableWindow( false );
	pThis->tbTXMode.EnableWindow( false );
	pThis->tbPCD.EnableWindow( false );
	pThis->tbPICC.EnableWindow( false );
	pThis->tbMODA1.EnableWindow( false );
	pThis->tbMODA2.EnableWindow( false );
	pThis->tbMODB1.EnableWindow( false );
	pThis->tbMODB2.EnableWindow( false );
	pThis->tbCONDA1.EnableWindow( false );
	pThis->tbCONDB1.EnableWindow( false );
	pThis->tbCONDB2.EnableWindow( false );
	pThis->tbACONDA2.EnableWindow( false );
	pThis->tbRXA1.EnableWindow( false );
	pThis->tbRXA2.EnableWindow( false );
	pThis->tbRXB1.EnableWindow( false );
	pThis->tbRXB2.EnableWindow( false );
	pThis->tbMessage.EnableWindow( false );
	pThis->tbCurrReg.EnableWindow( false );
	pThis->tbNewReg.EnableWindow( false );
	pThis->tbSetup.EnableWindow( false );
	pThis->rANTOff.EnableWindow( false );
	pThis->rANTOn.EnableWindow( false );
	pThis->rCurr106.EnableWindow( false );
	pThis->rCurr212.EnableWindow( false );
	pThis->rCurr424.EnableWindow( false );
	pThis->rCurr848.EnableWindow( false );
	pThis->rCurrNO.EnableWindow( false );
	pThis->rInterICC.EnableWindow( false );
	pThis->rInterPICC.EnableWindow( false );
	pThis->rInterSAM.EnableWindow( false );
	pThis->rISOA.EnableWindow( false );
	pThis->rISOB.EnableWindow( false );
	pThis->rISOAB.EnableWindow( false );
	pThis->rMax106.EnableWindow( false );
	pThis->rMax212.EnableWindow( false );
	pThis->rMax424.EnableWindow( false );
	pThis->rMax848.EnableWindow( false );
	pThis->rMaxNO.EnableWindow( false );
	pThis->check1.EnableWindow( false );
	
	pThis->rANTOff.SetCheck( false );
	pThis->rANTOn.SetCheck( false );
	pThis->rCurr106.SetCheck( false );
	pThis->rCurr212.SetCheck( false );
	pThis->rCurr424.SetCheck( false );
	pThis->rCurr848.SetCheck( false );
	pThis->rCurrNO.SetCheck( false );
	pThis->rInterICC.SetCheck( false );
	pThis->rInterPICC.SetCheck( false );
	pThis->rInterSAM.SetCheck( false );
	pThis->rISOA.SetCheck( false );
	pThis->rISOB.SetCheck( false );
	pThis->rISOAB.SetCheck( false );
	pThis->rMax106.SetCheck( false );
	pThis->rMax212.SetCheck( false );
	pThis->rMax424.SetCheck( false );
	pThis->rMax848.SetCheck( false );
	pThis->rMaxNO.SetCheck( false );
	pThis->check1.SetCheck( false );

	pThis->tbFWI.SetWindowText( "" );
	pThis->tbPoll.SetWindowText( "" );
	pThis->tbTrans.SetWindowText( "" );
	pThis->tbFieldStop.SetWindowText( "" );
	pThis->tbRecvGain.SetWindowText( "" );
	pThis->tbTXMode.SetWindowText( "" );
	pThis->tbPCD.SetWindowText( "" );
	pThis->tbPICC.SetWindowText( "" );
	pThis->tbMODA1.SetWindowText( "" );
	pThis->tbMODA2.SetWindowText( "" );
	pThis->tbMODB1.SetWindowText( "" );
	pThis->tbMODB2.SetWindowText( "" );
	pThis->tbCONDA1.SetWindowText( "" );
	pThis->tbCONDB1.SetWindowText( "" );
	pThis->tbCONDB2.SetWindowText( "" );
	pThis->tbACONDA2.SetWindowText( "" );
	pThis->tbRXA1.SetWindowText( "" );
	pThis->tbRXA2.SetWindowText( "" );
	pThis->tbRXB1.SetWindowText( "" );
	pThis->tbRXB2.SetWindowText( "" );
	pThis->tbMessage.SetWindowText( "" );
	pThis->tbCurrReg.SetWindowText( "" );
	pThis->tbNewReg.SetWindowText( "" );
	pThis->tbSetup.SetWindowText( "" );

	pThis->tbFWI.SetLimitText( 2 );
	pThis->tbPoll.SetLimitText( 2 );
	pThis->tbTrans.SetLimitText( 2 );
	pThis->tbFieldStop.SetLimitText( 2 );
	pThis->tbRecvGain.SetLimitText( 1 );
	pThis->tbTXMode.SetLimitText( 2 );
	pThis->tbPCD.SetLimitText( 1 );
	pThis->tbPICC.SetLimitText( 1 );
	pThis->tbMODA1.SetLimitText( 2 );
	pThis->tbMODA2.SetLimitText( 2 );
	pThis->tbMODB1.SetLimitText( 2 );
	pThis->tbMODB2.SetLimitText( 2 );
	pThis->tbCONDA1.SetLimitText( 2 );
	pThis->tbCONDB1.SetLimitText( 2 );
	pThis->tbCONDB2.SetLimitText( 2 );
	pThis->tbACONDA2.SetLimitText( 2 );
	pThis->tbRXA1.SetLimitText( 2 );
	pThis->tbRXA2.SetLimitText( 2 );
	pThis->tbRXB1.SetLimitText( 2 );
	pThis->tbRXB2.SetLimitText( 2 );
	pThis->tbCurrReg.SetLimitText( 2 );
	pThis->tbNewReg.SetLimitText( 2 );
	pThis->tbSetup.SetLimitText( 2 );
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
// CAdvancedDeviceProgrammingDlg dialog

CAdvancedDeviceProgrammingDlg::CAdvancedDeviceProgrammingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CAdvancedDeviceProgrammingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CAdvancedDeviceProgrammingDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CAdvancedDeviceProgrammingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAdvancedDeviceProgrammingDlg)
	DDX_Control(pDX, IDC_RADIO18, rInterSAM);
	DDX_Control(pDX, IDC_RADIO17, rInterPICC);
	DDX_Control(pDX, IDC_RADIO16, rInterICC);
	DDX_Control(pDX, IDC_BUTTON23, btnRefresh);
	DDX_Control(pDX, IDC_BUTTON22, btnQuit);
	DDX_Control(pDX, IDC_BUTTON21, btnReset);
	DDX_Control(pDX, IDC_BUTTON20, btnClear);
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_BUTTON19, btnReg);
	DDX_Control(pDX, IDC_BUTTON18, btnGetReg);
	DDX_Control(pDX, IDC_EDIT25, tbNewReg);
	DDX_Control(pDX, IDC_EDIT24, tbCurrReg);
	DDX_Control(pDX, IDC_EDIT23, tbMessage);
	DDX_Control(pDX, IDC_BUTTON16, btnGetPPS);
	DDX_Control(pDX, IDC_BUTTON17, btnPPS);
	DDX_Control(pDX, IDC_RADIO15, rCurrNO);
	DDX_Control(pDX, IDC_RADIO14, rCurr848);
	DDX_Control(pDX, IDC_RADIO13, rCurr424);
	DDX_Control(pDX, IDC_RADIO12, rCurr212);
	DDX_Control(pDX, IDC_RADIO11, rCurr106);
	DDX_Control(pDX, IDC_RADIO10, rMaxNO);
	DDX_Control(pDX, IDC_RADIO9, rMax848);
	DDX_Control(pDX, IDC_RADIO8, rMax424);
	DDX_Control(pDX, IDC_RADIO7, rMax212);
	DDX_Control(pDX, IDC_RADIO6, rMax106);
	DDX_Control(pDX, IDC_RADIO5, rISOAB);
	DDX_Control(pDX, IDC_RADIO4, rISOB);
	DDX_Control(pDX, IDC_RADIO3, rISOA);
	DDX_Control(pDX, IDC_RADIO2, rANTOff);
	DDX_Control(pDX, IDC_RADIO1, rANTOn);
	DDX_Control(pDX, IDC_BUTTON15, btnAuto);
	DDX_Control(pDX, IDC_BUTTON14, btnPICCType);
	DDX_Control(pDX, IDC_BUTTON13, btnGetPICCType);
	DDX_Control(pDX, IDC_BUTTON12, btnPICC);
	DDX_Control(pDX, IDC_BUTTON11, btnGetPICC);
	DDX_Control(pDX, IDC_BUTTON10, btnError);
	DDX_Control(pDX, IDC_BUTTON9, btnGetError);
	DDX_Control(pDX, IDC_EDIT22, tbRXA2);
	DDX_Control(pDX, IDC_EDIT21, tbACONDA2);
	DDX_Control(pDX, IDC_EDIT20, tbMODA2);
	DDX_Control(pDX, IDC_EDIT19, tbRXA1);
	DDX_Control(pDX, IDC_EDIT18, tbCONDA1);
	DDX_Control(pDX, IDC_EDIT17, tbMODA1);
	DDX_Control(pDX, IDC_EDIT13, tbRXB1);
	DDX_Control(pDX, IDC_EDIT16, tbRXB2);
	DDX_Control(pDX, IDC_EDIT15, tbCONDB2);
	DDX_Control(pDX, IDC_EDIT14, tbMODB2);
	DDX_Control(pDX, IDC_EDIT12, tbCONDB1);
	DDX_Control(pDX, IDC_EDIT11, tbMODB1);
	DDX_Control(pDX, IDC_EDIT9, tbPICC);
	DDX_Control(pDX, IDC_EDIT8, tbPCD);
	DDX_Control(pDX, IDC_CHECK1, check1);
	DDX_Control(pDX, IDC_BUTTON7, btnGetTransOpt);
	DDX_Control(pDX, IDC_BUTTON8, btnTransOpt);
	DDX_Control(pDX, IDC_EDIT7, tbTXMode);
	DDX_Control(pDX, IDC_EDIT6, tbRecvGain);
	DDX_Control(pDX, IDC_EDIT5, tbSetup);
	DDX_Control(pDX, IDC_EDIT4, tbFieldStop);
	DDX_Control(pDX, IDC_BUTTON5, btnGetAntenna);
	DDX_Control(pDX, IDC_BUTTON6, btnAntenna);
	DDX_Control(pDX, IDC_EDIT3, tbTrans);
	DDX_Control(pDX, IDC_EDIT2, tbPoll);
	DDX_Control(pDX, IDC_EDIT1, tbFWI);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON3, btnGetOptions);
	DDX_Control(pDX, IDC_BUTTON4, btnOptions);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAdvancedDeviceProgrammingDlg, CDialog)
	//{{AFX_MSG_MAP(CAdvancedDeviceProgrammingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON3, OnGetOpt)
	ON_BN_CLICKED(IDC_BUTTON4, OnOpt)
	ON_BN_CLICKED(IDC_BUTTON5, OnGetAntenna)
	ON_BN_CLICKED(IDC_BUTTON6, OnAntenna)
	ON_BN_CLICKED(IDC_RADIO1, OnAntennaON)
	ON_BN_CLICKED(IDC_RADIO2, OnAntennaOFF)
	ON_BN_CLICKED(IDC_BUTTON7, OnGetTrans)
	ON_BN_CLICKED(IDC_BUTTON8, OnTrans)
	ON_BN_CLICKED(IDC_BUTTON9, OnGetError)
	ON_BN_CLICKED(IDC_BUTTON10, OnSetError)
	ON_BN_CLICKED(IDC_BUTTON11, OnGetPICC)
	ON_BN_CLICKED(IDC_BUTTON12, OnPICC)
	ON_BN_CLICKED(IDC_BUTTON13, OnGetPICCType)
	ON_BN_CLICKED(IDC_BUTTON14, OnPICCType)
	ON_BN_CLICKED(IDC_RADIO3, OnISOA)
	ON_BN_CLICKED(IDC_RADIO4, OnISOB)
	ON_BN_CLICKED(IDC_RADIO5, OnISOAB)
	ON_BN_CLICKED(IDC_BUTTON15, OnAuto)
	ON_BN_CLICKED(IDC_RADIO6, OnMax106)
	ON_BN_CLICKED(IDC_RADIO7, OnMax212)
	ON_BN_CLICKED(IDC_RADIO8, OnMax424)
	ON_BN_CLICKED(IDC_RADIO9, OnMax848)
	ON_BN_CLICKED(IDC_RADIO10, OnMaxNO)
	ON_BN_CLICKED(IDC_RADIO11, OnCurr106)
	ON_BN_CLICKED(IDC_RADIO12, OnCurr212)
	ON_BN_CLICKED(IDC_RADIO13, OnCurr424)
	ON_BN_CLICKED(IDC_RADIO14, OnCurr848)
	ON_BN_CLICKED(IDC_RADIO15, OnCurrNo)
	ON_BN_CLICKED(IDC_BUTTON16, OnGetCurrent)
	ON_BN_CLICKED(IDC_BUTTON17, OnSetPPS)
	ON_BN_CLICKED(IDC_BUTTON18, OnGetReg)
	ON_BN_CLICKED(IDC_BUTTON19, OnSetReg)
	ON_BN_CLICKED(IDC_BUTTON23, OnRefresh)
	ON_BN_CLICKED(IDC_RADIO16, OnInterICC)
	ON_BN_CLICKED(IDC_RADIO17, OnInterPICC)
	ON_BN_CLICKED(IDC_RADIO18, OnInterSAM)
	ON_BN_CLICKED(IDC_BUTTON20, OnClear)
	ON_BN_CLICKED(IDC_BUTTON21, OnReset)
	ON_BN_CLICKED(IDC_BUTTON22, OnQuit)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CAdvancedDeviceProgrammingDlg message handlers

BOOL CAdvancedDeviceProgrammingDlg::OnInitDialog()
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
	Initialize();
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CAdvancedDeviceProgrammingDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CAdvancedDeviceProgrammingDlg::OnPaint() 
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
HCURSOR CAdvancedDeviceProgrammingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CAdvancedDeviceProgrammingDlg::OnInit() 
{

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
	//Put readernames inside the combobox
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
	btnInit.EnableWindow( false );
	
}

void CAdvancedDeviceProgrammingDlg::OnConnect() 
{

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

	pThis->btnConnect.EnableWindow( true );
	pThis->btnGetOptions.EnableWindow( true );
	pThis->btnOptions.EnableWindow( true );
	pThis->btnAntenna.EnableWindow( true );
	pThis->btnGetAntenna.EnableWindow( true );
	pThis->btnTransOpt.EnableWindow( true );
	pThis->btnGetTransOpt.EnableWindow( true );
	pThis->btnError.EnableWindow( true );
	pThis->btnGetError.EnableWindow( true );
	pThis->btnPICC.EnableWindow( true );
	pThis->btnGetPICC.EnableWindow( true );
	pThis->btnGetPICCType.EnableWindow( true );
	pThis->btnPICCType.EnableWindow( true );
	pThis->btnAuto.EnableWindow( true );
	pThis->btnPPS.EnableWindow( true );
	pThis->btnGetPPS.EnableWindow( true );
	pThis->btnReg.EnableWindow( true );
	pThis->btnGetReg.EnableWindow( true );
	pThis->btnRefresh.EnableWindow( true );
	pThis->btnReset.EnableWindow( true );
	pThis->tbFWI.EnableWindow( true );
	pThis->tbPoll.EnableWindow( true );
	pThis->tbTrans.EnableWindow( true );
	pThis->tbFieldStop.EnableWindow( true );
	pThis->tbRecvGain.EnableWindow( true );
	pThis->tbTXMode.EnableWindow( true );
	pThis->tbPCD.EnableWindow( true );
	pThis->tbPICC.EnableWindow( true );
	pThis->tbMODA1.EnableWindow( true );
	pThis->tbMODA2.EnableWindow( true );
	pThis->tbMODB1.EnableWindow( true );
	pThis->tbMODB2.EnableWindow( true );
	pThis->tbCONDA1.EnableWindow( true );
	pThis->tbCONDB1.EnableWindow( true );
	pThis->tbCONDB2.EnableWindow( true );
	pThis->tbACONDA2.EnableWindow( true );
	pThis->tbRXA1.EnableWindow( true );
	pThis->tbRXA2.EnableWindow( true );
	pThis->tbRXB1.EnableWindow( true );
	pThis->tbRXB2.EnableWindow( true );
	pThis->tbMessage.EnableWindow( true );
	pThis->tbCurrReg.EnableWindow( true );
	pThis->tbNewReg.EnableWindow( true );
	pThis->tbSetup.EnableWindow( true );
	pThis->rANTOff.EnableWindow( true );
	pThis->rANTOn.EnableWindow( true );
	pThis->rCurr106.EnableWindow( true );
	pThis->rCurr212.EnableWindow( true );
	pThis->rCurr424.EnableWindow( true );
	pThis->rCurr848.EnableWindow( true );
	pThis->rCurrNO.EnableWindow( true );
	pThis->rInterICC.EnableWindow( true );
	pThis->rInterPICC.EnableWindow( true );
	pThis->rInterSAM.EnableWindow( true );
	pThis->rISOA.EnableWindow( true );
	pThis->rISOB.EnableWindow( true );
	pThis->rISOAB.EnableWindow( true );
	pThis->rMax106.EnableWindow( true );
	pThis->rMax212.EnableWindow( true );
	pThis->rMax424.EnableWindow( true );
	pThis->rMax848.EnableWindow( true );
	pThis->rMaxNO.EnableWindow( true );
	pThis->check1.EnableWindow( true );
	pThis->rInterSAM.SetCheck( true );

}

void CAdvancedDeviceProgrammingDlg::OnGetOpt() 
{
	
	char tempstr[MAX];
	char tempstr2[MAX];
	int index;
	
	ClearBuffers();
	SendBuff[0] = 0x1F;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 8;
	
	retCode = CallCardControl();

	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	sprintf( tempstr2, "" );

	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000003" ) == 0 )
	{
		//Interpret response data
		sprintf( tempstr2, "%02X", RecvBuff[5] );
		tbFWI.SetWindowText( tempstr2 );
		sprintf( tempstr2, "%02X", RecvBuff[6] );
		tbPoll.SetWindowText( tempstr2 );
		sprintf( tempstr2, "%02X", RecvBuff[7] );
		tbTrans.SetWindowText( tempstr2 );
	
	}
	else
	{
	
		tbFWI.SetWindowText( "" );
		tbPoll.SetWindowText( "" );
		tbTrans.SetWindowText( "" );
		DisplayOut( "Invalid Response", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnOpt() 
{

	char tempstr[MAX];
	char holder[4];
	int index, hold;

	ClearBuffers();
	SendBuff[0] = 0x1F;
	SendBuff[1] = 0x03;

	tbFWI.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &hold );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbFWI.SetWindowText( "0B" );
		tbFWI.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[2] = hold;

	}

	tbPoll.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &hold );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbPoll.SetWindowText( "" );
		tbPoll.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[3] = hold;

	}

	tbTrans.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &hold );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbTrans.SetWindowText( "40" );
		tbTrans.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[4] = hold;

	}

	SendLen = 5;
	RecvLen = 8;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000003" ) != 0 )
	{
	
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnGetAntenna() 
{

	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x25;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000001" ) == 0 )
	{
		//Interpret response data
		if( RecvBuff[5] == 0x00 )
		{
		
			rANTOff.SetCheck( true );
			rANTOn.SetCheck( false );
		
		}
		else
		{
		
			rANTOn.SetCheck( true );
			rANTOff.SetCheck( false );

		}
	
	}
	else
	{
	
		rANTOn.SetCheck( false );
		rANTOff.SetCheck( false );
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnAntenna() 
{

	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x23;
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
	
		DisplayOut( "Turn off automatic PICC polling in the device before using this function\n", RED );
		return;

	}

	ClearBuffers();
	SendBuff[0] = 0x25;
	SendBuff[1] = 0x01;

	if( rANTOn.GetCheck() == true )
	{
	
		SendBuff[2] = 0x01;
	
	}
	else
	{
	
		if( rANTOff.GetCheck() == true )
		{
		
			SendBuff[2] = 0x00;
		
		}
		else
		{
		
			rANTOn.SetFocus();
			return;
		
		}
	
	}

	SendLen = 3;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000001" ) != 0 )
	{
	
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnAntennaON() 
{

	rANTOn.SetCheck( true );
	rANTOff.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnAntennaOFF() 
{
	
	rANTOn.SetCheck( false );
	rANTOff.SetCheck( true );
	
}

void CAdvancedDeviceProgrammingDlg::OnGetTrans() 
{
	
	char tempstr[MAX], conv[10];
	int index, tempval;

	ClearBuffers();
	SendBuff[0] = 0x20;
	SendBuff[1] = 0x01;
	SendLen = 2;
	RecvLen = 9;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 6; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E10000000406" ) == 0 )
	{
		//Interpret response data
		tempval = RecvBuff[6] >> 4;
		sprintf( conv, "%d", tempval );
		tbFieldStop.SetWindowText( conv );
		tempval = ( RecvBuff[6] & 0x0F );
		sprintf( conv, "%d", tempval );
		tbSetup.SetWindowText( conv );

		if( ( RecvBuff[7] & 0x04 ) != 0 )
		{
		
			check1.SetCheck( true );
		
		}
		else
		{
		
			check1.SetCheck( false );
		
		}
		
		tempval = ( RecvBuff[7] & 0x03 );
		sprintf( conv, "%d", tempval );
		tbRecvGain.SetWindowText( conv );
		sprintf( conv, "%02X", RecvBuff[8] );
		tbTXMode.SetWindowText( conv );

	}
	else
	{
	
		DisplayOut( "Invalid Response\n", RED );
		
	}

}

void CAdvancedDeviceProgrammingDlg::OnTrans() 
{

	char temp[5];
	int tempval;

	tbFieldStop.GetWindowText( temp, 4 );
	if( strcmp( temp, "" ) == 0 )
	{
	
		tbFieldStop.SetFocus();
		return;

	}

	tbSetup.GetWindowText( temp, 4 );
	if( strcmp( temp, "" ) == 0 )
	{
	
		tbSetup.SetFocus();
		return;

	}

	tbRecvGain.GetWindowText( temp, 4 );
	if( strcmp( temp, "" ) == 0 )
	{
	
		tbRecvGain.SetFocus();
		return;

	}

	tbTXMode.GetWindowText( temp, 4 );
	if( strcmp( temp, "" ) == 0 || HexCheck( temp[0], temp[1] ) != 0 )
	{

		tbTXMode.SetWindowText( "" );
		tbTXMode.SetFocus();
		return;

	}

	tbFieldStop.GetWindowText( temp, 4 );
	if( atoi( temp ) > 15 )
	{
	
		tbFieldStop.SetWindowText( "15" );
		tbFieldStop.SetFocus();
		return;
	
	}

	tbSetup.GetWindowText( temp, 4 );
	if( atoi( temp ) > 15 )
	{
	
		tbSetup.SetWindowText( "15" );
		tbSetup.SetFocus();
		return;
	
	}

	tbRecvGain.GetWindowText( temp, 4 );
	if( atoi( temp ) > 4 )
	{
	
		tbRecvGain.SetWindowText( "4" );
		tbRecvGain.SetFocus();
		return;
	
	}
	
	ClearBuffers();
	SendBuff[0] = 0x20;
	SendBuff[1] = 0x04;
	SendBuff[2] = 0x06;

	tbFieldStop.GetWindowText( temp, 4 );
	SendBuff[3] = SendBuff[3] + atoi( temp );

	tbSetup.GetWindowText( temp,4 );
	SendBuff[3] = SendBuff[3] + atoi( temp );
	
	if( check1.GetCheck() == true )
	{
	
		SendBuff[4] = 0x04;
	
	}

	tbRecvGain.GetWindowText( temp, 4 );
	SendBuff[3] = SendBuff[4] + atoi( temp );
	
	tbTXMode.GetWindowText( temp, 4 );
	sscanf( temp, "%X", &tempval );
	SendBuff[5] = tempval;

	SendLen = 6;
	RecvLen = 5;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	if( RecvBuff[0] != 0xE1 )
	{
	
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnGetError() 
{

	char tempstr[MAX], holder[MAX];
	int index, tempval;

	ClearBuffers();
	SendBuff[0] = 0x2C;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 7;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000002" ) == 0 && RecvBuff[6] == 0x7F )
	{
		//Interpret response data
		tempval = ( RecvBuff[5] >> 4 );
		sprintf( holder, "%d", tempval );
		tbPCD.SetWindowText( holder );
		tempval = ( RecvBuff[5] & 0x03 );
		sprintf( holder, "%d", tempval );
		tbPICC.SetWindowText( holder );

	}
	else
	{
	
		tbPCD.SetWindowText( "" );
		tbPICC.SetWindowText( "" );
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnSetError() 
{
	
	char tempstr[MAX], holder[2];
	int index;

	tbPCD.GetWindowText( holder, 2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbPCD.SetFocus();
		return;
	
	}

	if( atoi( holder ) > 3 )
	{
	
		tbPCD.SetWindowText( "3" );
		tbPCD.SetFocus();
		return;
			
	}
	
	tbPICC.GetWindowText( holder, 2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbPICC.SetFocus();
		return;
	
	}

	if( atoi( holder ) > 3 )
	{
	
		tbPICC.SetWindowText( "3" );
		tbPICC.SetFocus();
		return;
			
	}
	
	ClearBuffers();
	SendBuff[0] = 0x2C;
	SendBuff[1] = 0x02;

	tbPCD.GetWindowText( holder, 2 );
	SendBuff[2] = ( atoi( holder ) << 4 );
	tbPICC.GetWindowText( holder, 2 );
	SendBuff[2] = SendBuff[2] + atoi( holder );
	SendBuff[3] = 0x7F;
	SendLen = 4;
	RecvLen = 7;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000002" ) != 0 )
	{
	
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnGetPICC() 
{

	char tempstr[MAX], holder[2];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x2A;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 17;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E10000000C" ) == 0 )
	{
		//Interpret response data
		sprintf( holder, "%02X", RecvBuff[5] );
		tbMODB1.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[6] );
		tbCONDB1.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[7] );
		tbRXB1.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[8] );
		tbMODB2.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[9] );
		tbCONDB2.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[10] );
		tbRXB2.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[11] );
		tbMODA1.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[12] );
		tbCONDA1.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[13] );
		tbRXA1.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[14] );
		tbMODA2.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[15] );
		tbACONDA2.SetWindowText( holder );
		sprintf( holder, "%02X", RecvBuff[16] );
		tbRXA2.SetWindowText( holder );
		
	}
	else
	{
	
		tbMODB1.SetWindowText( "" );
		tbCONDB1.SetWindowText( "" );
		tbRXB1.SetWindowText( "" );
		tbMODB2.SetWindowText( "" );
		tbCONDB2.SetWindowText( "" );
		tbRXB2.SetWindowText( "" );
		tbMODA1.SetWindowText( "" );
		tbCONDA1.SetWindowText( "" );
		tbRXA1.SetWindowText( "" );
		tbMODA2.SetWindowText( "" );
		tbACONDA2.SetWindowText( "" );
		tbRXA2.SetWindowText( "" );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnPICC() 
{

	char tempstr[MAX], holder[4];
	int index, tempval;

	ClearBuffers();
	SendBuff[0] = 0x2A;
	SendBuff[1] = 0x0C;

	tbMODB1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbMODB1.SetWindowText( "" );
		tbMODB1.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[2] = tempval;

	}
	tbCONDB1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbCONDB1.SetWindowText( "" );
		tbCONDB1.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[3] = tempval;

	}
	tbRXB1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbRXB1.SetWindowText( "" );
		tbRXB1.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[4] = tempval;

	}

	tbMODB2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbMODB2.SetWindowText( "" );
		tbMODB2.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[5] = tempval;

	}
	tbCONDB2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbCONDB2.SetWindowText( "" );
		tbCONDB2.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[6] = tempval;

	}
	tbRXB2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbRXB2.SetWindowText( "" );
		tbRXB2.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[7] = tempval;

	}

	tbMODA1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbMODA1.SetWindowText( "" );
		tbMODA1.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[8] = tempval;

	}
	tbCONDA1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbCONDA1.SetWindowText( "" );
		tbCONDA1.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[9] = tempval;

	}
	tbRXA1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbRXA1.SetWindowText( "" );
		tbRXA1.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[10] = tempval;

	}

	tbMODA2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbMODA2.SetWindowText( "" );
		tbMODA2.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[11] = tempval;

	}
	tbACONDA2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbACONDA2.SetWindowText( "" );
		tbACONDA2.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[12] = tempval;

	}
	tbRXA2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbRXA2.SetWindowText( "" );
		tbRXA2.SetFocus();
		return;
	
	}
	else
	{

		SendBuff[13] = tempval;

	}
	
	SendLen = 14;
	RecvLen = 17;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E10000000C" ) != 0 )
	{
	
		DisplayOut( "Invalid response\n", RED );
	
	}
}

void CAdvancedDeviceProgrammingDlg::OnGetPICCType() 
{
	
	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x20;
	SendBuff[1] = 0x00;
	SendBuff[3] = 0xFF;
	SendLen = 4;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000001" ) != 0 )
	{
	
		tbMessage.SetWindowText( "Invalid Card Detected" );
		return;
	
	}
	//Interpret status
	switch( RecvBuff[5] )
	{
	
		case 1:
			rISOA.SetCheck( true );
			rISOB.SetCheck( false );
			rISOAB.SetCheck( false );
			break;
		case 2:
			rISOA.SetCheck( false );
			rISOB.SetCheck( true );
			rISOAB.SetCheck( false );
			break;
		default:
			rISOA.SetCheck( false );
			rISOB.SetCheck( false );
			rISOAB.SetCheck( true );	
			break;

	}

}

void CAdvancedDeviceProgrammingDlg::OnPICCType() 
{

	if( rISOA.GetCheck() == true )
	{
	
		reqtype = 1;
	
	}
	else if( rISOB.GetCheck() == true )
	{
	
		reqtype = 2;
	
	}
	else if( rISOAB.GetCheck() == true )
	{
	
		reqtype = 3;
	
	}
	else
	{
	
		rISOA.SetFocus();
		return;
	
	}

	ClearBuffers();
	SendBuff[0] = 0x20;
	SendBuff[1] = 0x02;

	switch( reqtype )
	{
	
		case 1:
			SendBuff[2] = 0x01;
			break;
		case 2:
			SendBuff[2] = 0x02;
			break;
		case 3:
			SendBuff[2] = 0x03;
			break;
		default:
			break;
	
	}

	SendBuff[3] = 0xFF;
	SendLen = 4;
	RecvLen = 5;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnISOA() 
{

	rISOA.SetCheck( true );
	rISOB.SetCheck( false );
	rISOAB.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnISOB() 
{
	
	rISOA.SetCheck( false );
	rISOB.SetCheck( true );
	rISOAB.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnISOAB() 
{

	rISOA.SetCheck( false );
	rISOB.SetCheck( false );
	rISOAB.SetCheck( true );	

}

void CAdvancedDeviceProgrammingDlg::OnAuto() 
{
	int index;
	DWORD Protocol = 1;

	if( autodet == true )
	{
	
		autodet = false;
		btnAuto.SetWindowText( "Start Auto Detection" );
		KillTimer( 1 );
		tbMessage.SetWindowText( "Polling Stopped..." );
	
	}
	else
	{
	
		autodet = true;
		btnAuto.SetWindowText( "End Auto Detection" );
		
		index = cbReader.FindStringExact( -1, "ACS ACR128U PICC Interface 0" );
		cbReader.SetCurSel( index );
		
		tbMessage.SetWindowText( "Polling Started..." );
		SetTimer( 1, 250, TimerFunc );

	}

}

void CAdvancedDeviceProgrammingDlg::OnMax106() 
{

	rMax106.SetCheck( true );
	rMax212.SetCheck( false );
	rMax424.SetCheck( false );
	rMax848.SetCheck( false );
	rMaxNO.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnMax212() 
{

	rMax106.SetCheck( false );
	rMax212.SetCheck( true );
	rMax424.SetCheck( false );
	rMax848.SetCheck( false );
	rMaxNO.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnMax424() 
{
	
	rMax106.SetCheck( false );
	rMax212.SetCheck( false );
	rMax424.SetCheck( true );
	rMax848.SetCheck( false );
	rMaxNO.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnMax848() 
{

	rMax106.SetCheck( false );
	rMax212.SetCheck( false );
	rMax424.SetCheck( false );
	rMax848.SetCheck( true );
	rMaxNO.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnMaxNO() 
{

	rMax106.SetCheck( false );
	rMax212.SetCheck( false );
	rMax424.SetCheck( false );
	rMax848.SetCheck( false );
	rMaxNO.SetCheck( true );

}

void CAdvancedDeviceProgrammingDlg::OnCurr106() 
{
	
	rCurr106.SetCheck( true );
	rCurr212.SetCheck( false );
	rCurr424.SetCheck( false );
	rCurr848.SetCheck( false );
	rCurrNO.SetCheck( false );
	
}

void CAdvancedDeviceProgrammingDlg::OnCurr212() 
{

	rCurr106.SetCheck( false );
	rCurr212.SetCheck( true );
	rCurr424.SetCheck( false );
	rCurr848.SetCheck( false );
	rCurrNO.SetCheck( false );
	
}

void CAdvancedDeviceProgrammingDlg::OnCurr424() 
{
	
	rCurr106.SetCheck( false );
	rCurr212.SetCheck( false );
	rCurr424.SetCheck( true );
	rCurr848.SetCheck( false );
	rCurrNO.SetCheck( false );
	
}

void CAdvancedDeviceProgrammingDlg::OnCurr848() 
{
	
	rCurr106.SetCheck( false );
	rCurr212.SetCheck( false );
	rCurr424.SetCheck( false );
	rCurr848.SetCheck( true );
	rCurrNO.SetCheck( false );
	
}

void CAdvancedDeviceProgrammingDlg::OnCurrNo() 
{
	
	rCurr106.SetCheck( false );
	rCurr212.SetCheck( false );
	rCurr424.SetCheck( false );
	rCurr848.SetCheck( false );
	rCurrNO.SetCheck( true );
		
}

void CAdvancedDeviceProgrammingDlg::OnGetCurrent() 
{

	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x24;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 7;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000002" ) == 0 )
	{
		//Interpret response data
		switch( RecvBuff[5] )
		{
		
			case 0:
				rMax106.SetCheck( true );
				rMax212.SetCheck( false );
				rMax424.SetCheck( false );
				rMax848.SetCheck( false );
				rMaxNO.SetCheck( false );
				break;
			case 1:
				rMax106.SetCheck( false );
				rMax212.SetCheck( true );
				rMax424.SetCheck( false );
				rMax848.SetCheck( false );
				rMaxNO.SetCheck( false );
				break;
			case 2:
				rMax106.SetCheck( false );
				rMax212.SetCheck( false );
				rMax424.SetCheck( true );
				rMax848.SetCheck( false );
				rMaxNO.SetCheck( false );
				break;
			case 3:
				rMax106.SetCheck( false );
				rMax212.SetCheck( false );
				rMax424.SetCheck( false );
				rMax848.SetCheck( true );
				rMaxNO.SetCheck( false );
				break;
			case 4:
				rMax106.SetCheck( false );
				rMax212.SetCheck( false );
				rMax424.SetCheck( false );
				rMax848.SetCheck( false );
				rMaxNO.SetCheck( true );
				break;
		}

		switch( RecvBuff[6] )
		{
		
			case 0:
				rCurr106.SetCheck( true );
				rCurr212.SetCheck( false );
				rCurr424.SetCheck( false );
				rCurr848.SetCheck( false );
				rCurrNO.SetCheck( false );
				break;
			case 1:
				rCurr106.SetCheck( false );
				rCurr212.SetCheck( true );
				rCurr424.SetCheck( false );
				rCurr848.SetCheck( false );
				rCurrNO.SetCheck( false );
				break;
			case 2:
				rCurr106.SetCheck( false );
				rCurr212.SetCheck( false );
				rCurr424.SetCheck( true );
				rCurr848.SetCheck( false );
				rCurrNO.SetCheck( false );
				break;
			case 3:
				rCurr106.SetCheck( false );
				rCurr212.SetCheck( false );
				rCurr424.SetCheck( false );
				rCurr848.SetCheck( true );
				rCurrNO.SetCheck( false );
				break;
			case 4:
				rCurr106.SetCheck( false );
				rCurr212.SetCheck( false );
				rCurr424.SetCheck( false );
				rCurr848.SetCheck( false );
				rCurrNO.SetCheck( true );
				break;

		}
	
	}
	else
	{
		DisplayOut( tempstr, GREEN );
		rCurr106.SetCheck( false );
		rCurr212.SetCheck( false );
		rCurr424.SetCheck( false );
		rCurr848.SetCheck( false );
		rCurrNO.SetCheck( false );
		rMax106.SetCheck( false );
		rMax212.SetCheck( false );
		rMax424.SetCheck( false );
		rMax848.SetCheck( false );
		rMaxNO.SetCheck( false );
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnSetPPS() 
{
	
	char tempstr[MAX];
	int index;

	if( rMax106.GetCheck() == false &&
		rMax212.GetCheck() == false &&
		rMax424.GetCheck() == false &&
		rMax848.GetCheck() == false &&
		rMaxNO.GetCheck() == false )
	{
	
		rMaxNO.SetCheck( true );
	
	}

	if( rCurr106.GetCheck() == false &&
		rCurr212.GetCheck() == false &&
		rCurr424.GetCheck() == false &&
		rCurr848.GetCheck() == false &&
		rCurrNO.GetCheck() == false )
	{
	
		rCurrNO.SetCheck( true );
	
	}

	ClearBuffers();
	SendBuff[0] = 0x24;
	SendBuff[1] = 0x01;

	if( rMax106.GetCheck() == true )
	{
	
		SendBuff[2] = 0x00;
	
	}
	else if( rMax212.GetCheck() == true )
	{
	
		SendBuff[2] = 0x01;
	
	}
	else if( rMax424.GetCheck() == true )
	{
	
		SendBuff[2] = 0x02;
	
	}
	else if( rMax848.GetCheck() == true )
	{
	
		SendBuff[2] = 0x03;
	
	}
	else if( rMaxNO.GetCheck() == true )
	{
	
		SendBuff[2] = 0xFF;
	
	}

	SendLen = 3;
	RecvLen = 7;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000002" ) != 0 )
	{
	
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnGetReg() 
{

	char tempstr[MAX], holder[3];
	int index, tempval;

	tbCurrReg.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbCurrReg.SetWindowText( "" );
		tbCurrReg.SetFocus();
		return;
	
	}

	ClearBuffers();
	SendBuff[0] = 0x19;
	SendBuff[1] = 0x01;

	tbCurrReg.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[2] = tempval;

	SendLen = 3;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000001" ) == 0 )
	{
	
		sprintf( holder, "%02X", RecvBuff[5] );
		//tbCurrReg.SetWindowText( holder );
		tbNewReg.SetWindowText( holder );
			
	}
	else
	{
	
		tbCurrReg.SetWindowText( "" );
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnSetReg() 
{

	char tempstr[MAX], holder[3];
	int index, tempval;

	tbCurrReg.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbCurrReg.SetWindowText( "" );
		tbCurrReg.SetFocus();
		return;
	
	}

	tbNewReg.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tbNewReg.SetWindowText( "07" );
		tbNewReg.SetFocus();
		return;
	
	}

	ClearBuffers();
	SendBuff[0] = 0x1A;
	SendBuff[1] = 0x02;

	tbCurrReg.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[2] = tempval;

	tbNewReg.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[3] = tempval;

	SendLen = 4;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000001" ) == 0 )
	{
	
		sprintf( holder, "%02X", RecvBuff[5] );
		//tbCurrReg.SetWindowText( holder );
		tbNewReg.SetWindowText( holder );
	
	}
	else
	{
	
		tbCurrReg.SetWindowText( "" );
		tbNewReg.SetWindowText( "" );
		DisplayOut( "Invalid Response\n", RED );
	
	}

}

void CAdvancedDeviceProgrammingDlg::OnRefresh() 
{

	char tempstr[MAX], holder[50];
	int index;

	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return;
	
	}

	cbReader.SetCurSel( 0 );
	index = cbReader.FindStringExact( -1, "ACS ACR128U SAM Interface 0" );
	cbReader.SetCurSel( index );
	cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
	if( index == -1 )
	{
	
		DisplayOut( "Cannot find ACR128 SAM reader\n", RED );
		return;
	
	}
	//1. For SAM Refresh, connect to SAM Interface in direct mode
	if( rInterSAM.GetCheck() == true )
	{
	
		retCode = SCardConnect( hContext,
								readerName,
								SCARD_SHARE_DIRECT,
								0,
								&hCard,
								&dwActProtocol );

		if( retCode != SCARD_S_SUCCESS )
		{
		
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;
		
		}
		else
		{
			cbReader.GetLBText( cbReader.GetCurSel(), holder );
			sprintf( tempstr, "Successful connection to %s\n", holder );
			DisplayOut( tempstr, GREEN );

		}
	
	}
	//2. For other interfaces, connect to SAM Interface in direct mode
	else
	{
	
		retCode = SCardConnect( hContext,
								readerName,
								SCARD_SHARE_DIRECT,
								0,
								&hCard,
								&dwActProtocol );

		if( retCode != SCARD_S_SUCCESS )
		{
		
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;
		
		}
		else
		{
		
			cbReader.GetLBText( cbReader.GetCurSel(), holder );
			sprintf( tempstr, "Successful connection to %s\n", holder );
			DisplayOut( tempstr, GREEN );
		
		}

	}

	ClearBuffers();
	SendBuff[0] = 0x2D;
	SendBuff[1] = 0x01;

	if( rInterICC.GetCheck() == true )
	{
	
		SendBuff[2] == 0x01;
	
	}
	else 
	{

		if( rInterPICC.GetCheck() == true )
		{

			SendBuff[2] == 0x02;

		}
		else
		{
		
			SendBuff[2] == 0x04;
		
		}

	}
	
	SendLen = 3;
	RecvLen = 6;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000001" ) != 0 )
	{
	
		DisplayOut( "Invalid Response\n", RED );
		return;
	
	}
	//3. For SAM interface, disconnect and connect to SAM Interface in direct mode
	if( rInterSAM.GetCheck() == true )
	{

		retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
		if( retCode != SCARD_S_SUCCESS )
		{
	
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;
	
		}

		cbReader.SetCurSel( 0 );
		index = cbReader.FindStringExact( -1, "ACS ACR128U SAM Interface 0" );
		cbReader.SetCurSel( index );
		cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
		if( index == -1 )
		{
	
			DisplayOut( "Cannot find ACR128 SAM reader\n", RED );
			return;
	
		}
	
		retCode = SCardConnect( hContext,
								readerName,
								SCARD_SHARE_DIRECT,
								0,
								&hCard,
								&dwActProtocol );

		if( retCode != SCARD_S_SUCCESS )
		{
	
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;
	
		}
		else
		{
			cbReader.GetLBText( cbReader.GetCurSel(), holder );
			sprintf( tempstr, "Successful connection to %s\n", holder );
			DisplayOut( tempstr, GREEN );

		}

	}

}

void CAdvancedDeviceProgrammingDlg::OnInterICC() 
{

	rInterICC.SetCheck( true );
	rInterPICC.SetCheck( false );
	rInterSAM.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnInterPICC() 
{

	rInterICC.SetCheck( false );
	rInterPICC.SetCheck( true );
	rInterSAM.SetCheck( false );

}

void CAdvancedDeviceProgrammingDlg::OnInterSAM() 
{

	rInterICC.SetCheck( false );
	rInterPICC.SetCheck( false );
	rInterSAM.SetCheck( true );

}

void CAdvancedDeviceProgrammingDlg::OnClear() 
{

	rbResult.SetWindowText( "" );

}

void CAdvancedDeviceProgrammingDlg::OnReset() 
{

	rbResult.SetWindowText( "" );
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	Initialize();

}

void CAdvancedDeviceProgrammingDlg::OnQuit() 
{
	
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	CDialog::OnCancel();
	
}
