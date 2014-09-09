//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              DeviceProgrammingDlg.cpp
//
//  Description:       This sample program demonstrates how to set the polling options of the
//					   ACR128 reader. You can set if the reader can simultaenously detect a contact
//					   and a contactless card and set the delay between contactless card detection.
//
//  Author:            Wazer Emmanuel R. Benal
//
//	Date:              June 5, 2008
//
//	Revision Trail:   (Date/Author/Description)

//====================================================================================================

#include "stdafx.h"
#include "Polling Sample.h"
#include "Polling SampleDlg.h"

#define IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND SCARD_CTL_CODE(2079)
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 262

//Initializers for the status bar
static UINT BASED_CODE indicators[] =
{
    ID_INDICATOR_PANE1,
    ID_INDICATOR_PANE2,
	ID_INDICATOR_PANE3,
	ID_INDICATOR_PANE4
};


//Polling Sample Inlude File
#include "WINSCARD.h"

//Global Variables/////////////////////////////////////////
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	LPCBYTE					pbSend;
	DWORD					dwSend, dwRecv, size = 64;
	LPBYTE					pbRecv;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode, pollcase;
    char					readerName [256];
	DWORD					SendLen, RecvLen, ByteRet;;
	BYTE					SendBuff[MAX], RecvBuff[MAX];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	SCARD_READERSTATE		RdrState;
	bool					autodet = false;
	CPollingSampleDlg		*pThis = NULL;
///////////////////////////////////////////////////////////
#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//Functions used by the program////////////////////////////
void ClearBuffers();
static CString GetScardErrMsg( int code );
int CallCardControl();
void GetExMode( int reqType );
void ReadPoll( int reqType );
void CallCardConnect( int reqType );
int PICCpolling();
void DisplayOut( CString str, COLORREF color );
void Initialize();
///////////////////////////////////////////////////////////

//Timer function used for polling
void CALLBACK TimerFunc1 ( HWND, UINT, UINT_PTR, DWORD )
{

	switch( pollcase )
	{
		//Simultaenously detect a contact and contactless card
		case ( 1 || 3 ):
			pThis->OnICC();
			pThis->OnPICC();
			break;
		//Contactless card detection disabled
		case 2:
			pThis->OnICC();
			pThis->m_bar.SetPaneText( 3, "Automatic polling is disabled" );
			break;
	
	}
	

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

//This function displays messages in the rich edit box control.
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

//Card control function
int CallCardControl( )
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

//This is the separate polling function for contactless cards
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

//Inital states of the controls used
void Initialize()
{

	pThis->m_bar.SetPaneText( 1, "" );
	pThis->m_bar.SetPaneText( 3, "" );
	pThis->cbReader.SetWindowText( "" );
	pThis->btnInit.EnableWindow( true );
	pThis->btnConnect.EnableWindow( false );
	pThis->btnReadMode.EnableWindow( false );
	pThis->btnSetMode.EnableWindow( false );
	pThis->btnReadPoll.EnableWindow( false );
	pThis->btnSetPoll.EnableWindow( false );
	pThis->btnManual.EnableWindow( false );
	pThis->btnAuto.EnableWindow( false );
	pThis->btnReset.EnableWindow( false );
	pThis->btnQuit.EnableWindow( true );
	pThis->rInterBoth.EnableWindow( false );
	pThis->rInterEither.EnableWindow( false );
	pThis->rEXnot.EnableWindow( false );
	pThis->rEXactive.EnableWindow( false );
	pThis->check1.EnableWindow( false );
	pThis->check2.EnableWindow( false );
	pThis->check3.EnableWindow( false );
	pThis->check4.EnableWindow( false );
	pThis->check5.EnableWindow( false );
	pThis->check6.EnableWindow( false );
	pThis->rPoll250.EnableWindow( false );
	pThis->rPoll500.EnableWindow( false );
	pThis->rPoll1.EnableWindow( false );
	pThis->rPoll25.EnableWindow( false );
	pThis->rInterBoth.SetCheck( false );
	pThis->rInterEither.SetCheck( false );
	pThis->rEXnot.SetCheck( false );
	pThis->rEXactive.SetCheck( false );
	pThis->check1.SetCheck( false );
	pThis->check2.SetCheck( false );
	pThis->check3.SetCheck( false );
	pThis->check4.SetCheck( false );
	pThis->check5.SetCheck( false );
	pThis->check6.SetCheck( false );
	pThis->rPoll250.SetCheck( false );
	pThis->rPoll500.SetCheck( false );
	pThis->rPoll1.SetCheck( false );
	pThis->rPoll25.SetCheck( false );
	pThis->btnAuto.SetWindowText( "Start Auto Detection" );
	autodet = false;
	pThis->KillTimer( 1 );
	DisplayOut( "Program ready\n", GREEN );

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
// CPollingSampleDlg dialog

CPollingSampleDlg::CPollingSampleDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPollingSampleDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPollingSampleDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);

}

void CPollingSampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPollingSampleDlg)
	DDX_Control(pDX, IDC_BUTTON13, btnPICC);
	DDX_Control(pDX, IDC_BUTTON12, btnICC);
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_BUTTON11, btnQuit);
	DDX_Control(pDX, IDC_BUTTON10, btnReset);
	DDX_Control(pDX, IDC_BUTTON9, btnClear);
	DDX_Control(pDX, IDC_BUTTON8, btnAuto);
	DDX_Control(pDX, IDC_BUTTON7, btnManual);
	DDX_Control(pDX, IDC_BUTTON6, btnSetPoll);
	DDX_Control(pDX, IDC_BUTTON5, btnReadPoll);
	DDX_Control(pDX, IDC_RADIO8, rPoll25);
	DDX_Control(pDX, IDC_RADIO7, rPoll1);
	DDX_Control(pDX, IDC_RADIO6, rPoll500);
	DDX_Control(pDX, IDC_RADIO5, rPoll250);
	DDX_Control(pDX, IDC_CHECK6, check6);
	DDX_Control(pDX, IDC_CHECK5, check5);
	DDX_Control(pDX, IDC_CHECK4, check4);
	DDX_Control(pDX, IDC_CHECK3, check3);
	DDX_Control(pDX, IDC_CHECK2, check2);
	DDX_Control(pDX, IDC_CHECK1, check1);
	DDX_Control(pDX, IDC_BUTTON4, btnSetMode);
	DDX_Control(pDX, IDC_BUTTON3, btnReadMode);
	DDX_Control(pDX, IDC_RADIO4, rEXactive);
	DDX_Control(pDX, IDC_RADIO3, rEXnot);
	DDX_Control(pDX, IDC_RADIO2, rInterEither);
	DDX_Control(pDX, IDC_RADIO1, rInterBoth);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPollingSampleDlg, CDialog)
	//{{AFX_MSG_MAP(CPollingSampleDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_RADIO1, OnInterBoth)
	ON_BN_CLICKED(IDC_RADIO2, OnInterEither)
	ON_BN_CLICKED(IDC_RADIO3, OnExnot)
	ON_BN_CLICKED(IDC_RADIO4, OnEXactive)
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON3, OnReadMode)
	ON_BN_CLICKED(IDC_BUTTON4, OnSetMode)
	ON_BN_CLICKED(IDC_BUTTON5, OnReadPoll)
	ON_BN_CLICKED(IDC_BUTTON6, OnSetPoll)
	ON_BN_CLICKED(IDC_BUTTON7, OnManual)
	ON_BN_CLICKED(IDC_BUTTON8, OnAuto)
	ON_BN_CLICKED(IDC_BUTTON9, OnClear)
	ON_BN_CLICKED(IDC_BUTTON10, OnReset)
	ON_BN_CLICKED(IDC_BUTTON11, OnQuit)
	ON_BN_CLICKED(IDC_RADIO5, OnPoll250)
	ON_BN_CLICKED(IDC_RADIO6, OnPoll500)
	ON_BN_CLICKED(IDC_RADIO7, OnPoll1)
	ON_BN_CLICKED(IDC_RADIO8, OnPoll25)
	ON_BN_CLICKED(IDC_BUTTON12, OnICC)
	ON_BN_CLICKED(IDC_BUTTON13, OnPICC)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPollingSampleDlg message handlers

BOOL CPollingSampleDlg::OnInitDialog()
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
	//This creates the status bar in the dialog//////////////////////////
	pThis = this;
	m_bar.Create( this );
	m_bar.SetIndicators( indicators, 4 );
	CRect rect;
	GetClientRect( &rect );
	m_bar.SetPaneInfo( 0, ID_INDICATOR_PANE1, SBPS_NORMAL, 100 );
	m_bar.SetPaneInfo( 1, ID_INDICATOR_PANE2, SBPS_NORMAL, 240 );
	m_bar.SetPaneInfo( 2, ID_INDICATOR_PANE3, SBPS_NORMAL, 110 );
	m_bar.SetPaneInfo( 3, ID_INDICATOR_PANE4, SBPS_NORMAL, 235 );
	RepositionBars( AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST,
					ID_INDICATOR_PANE4 );
	//////////////////////////////////////////////////////////////////////
	
	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIconBig,	TRUE);		// Set big icon
	SetIcon(m_hIconSmall,	FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	//Setting up the text in a status bar pane////////////////////////////
	m_bar.SetPaneText( 0, "ICC Reader Status" );
	m_bar.SetPaneText( 2, "PICC Reader Status" );
	//////////////////////////////////////////////////////////////////////

	Initialize();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CPollingSampleDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CPollingSampleDlg::OnPaint() 
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
HCURSOR CPollingSampleDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CPollingSampleDlg::OnInterBoth() 
{

	rInterEither.SetCheck( false );

}

void CPollingSampleDlg::OnInterEither() 
{

	rInterBoth.SetCheck( false );

}

void CPollingSampleDlg::OnExnot() 
{

	rEXactive.SetCheck( false );
}

void CPollingSampleDlg::OnEXactive() 
{

	rEXnot.SetCheck( false );
	
}

void CPollingSampleDlg::OnInit() 
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
		int i;
    	for (i=0;p[i];i++);
	      i++;
	    if ( *p != 0 )
		{
     		cbReader.AddString( p );
		}
		p = &p[i];
	}
	
	//Set the SAM reader of the ACR128 as the default reader
	index = cbReader.FindStringExact( -1, "ACS ACR128U SAM Interface 0" );
	cbReader.SetCurSel( index );

	btnInit.EnableWindow( false );
	btnConnect.EnableWindow( true );
	btnReset.EnableWindow( true );
	
}

void CPollingSampleDlg::OnConnect() 
{
	
	DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];
	int index;

	cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
	//Connect to selected reader
	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_EXCLUSIVE,
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

	btnConnect.EnableWindow( false );
	btnReadMode.EnableWindow( true );
	btnSetMode.EnableWindow( true );
	btnReadPoll.EnableWindow( true );
	btnSetPoll.EnableWindow( true );
	btnManual.EnableWindow( true );
	btnAuto.EnableWindow( true );
	rInterBoth.EnableWindow( true );
	rInterEither.EnableWindow( true );
	rEXnot.EnableWindow( true );
	rEXactive.EnableWindow( true );
	check1.EnableWindow( true );
	check2.EnableWindow( true );
	check3.EnableWindow( true );
	check4.EnableWindow( true );
	check5.EnableWindow( true );
	check6.EnableWindow( true );
	rPoll250.EnableWindow( true );
	rPoll500.EnableWindow( true );
	rPoll1.EnableWindow( true );
	rPoll25.EnableWindow( true );
	
}

//Get the current mode and configuration setting of the reader
void CPollingSampleDlg::OnReadMode() 
{
	
	GetExMode( 1 );
	
}

//Set the mode of the reader
void CPollingSampleDlg::OnSetMode() 
{
	
	ClearBuffers();
	SendBuff[0] = 0x2B;
	SendBuff[1] = 0x01;

	if( rInterBoth.GetCheck() == true )
	{
	
		SendBuff[2] = 0x00;
	
	}
	else
	{
	
		SendBuff[2] = 0x01;
	
	}

	SendLen = 3;
	RecvLen = 7;
	
	//Call card control to set the configuration setting
	retCode = CallCardControl();

	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
}

void CPollingSampleDlg::OnReadPoll() 
{
	
	ReadPoll( 1 );
	
}

//Set polling options checked and the poll interval
void CPollingSampleDlg::OnSetPoll() 
{
	
	ClearBuffers();
	SendBuff[0] = 0x23;
	SendBuff[1] = 0x01;

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

	if( rPoll500.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x10 );
	
	}
	if( rPoll1.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x20 );
	
	}
	if( rPoll25.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x10 );
		SendBuff[2] = ( SendBuff[2] | 0x20 );
	
	}

	if( check5.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x40 );
	
	}
	if( check6.GetCheck() == true )
	{
	
		SendBuff[2] = ( SendBuff[2] | 0x80 );
	
	}

	SendLen = 3;
	RecvLen = 7;

	retCode = CallCardControl();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
}

//This is the manual card detection code for the ACR128 ICC reader
void CPollingSampleDlg::OnManual() 
{
	
	CallCardConnect( 1 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	ReadPoll( 0 );
	if( ( RecvBuff[5] & 0x01 ) != 0 )
	{
	
		DisplayOut( "Turn off automatic PICC polling in the device before using this function\n", RED );
		return;
	}
	else
	{
	
		ClearBuffers();
		SendBuff[0] = 0x22;
		SendBuff[1] = 0x01;
		SendBuff[2] = 0x0A;
		SendLen = 3;
		RecvLen = 6;

		retCode = CallCardControl();
		if( retCode != SCARD_S_SUCCESS )
		{
		
			return;
		
		}

		if( ( RecvBuff[5] & 0x01 ) != 0 )
		{
		
			m_bar.SetPaneText( 3, "No card within range" );
		
		}
		else
		{
		
			m_bar.SetPaneText( 3, "Card is detected" );
		
		}
	
	}
}

//This is the code for the automatic card detection
void CPollingSampleDlg::OnAuto() 
{
	
	if( autodet )
	{
	
		autodet = false;
		btnAuto.SetWindowText( "Start Auto Detection" );
		KillTimer(1);
		m_bar.SetPaneText( 1, "" );
		m_bar.SetPaneText( 3, "" );
		return;

	}

	CallCardConnect( 1 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		autodet = false;
		btnAuto.SetWindowText( "Start Auto Detection" );
		return;
	
	}

	GetExMode( 0 );
	if( RecvBuff[5] != 0 )
	{
	
		ReadPoll( 0 );
		if( ( RecvBuff[5] & 0x01 ) != 0 )
		{
		
			pollcase = 1;
		
		}
		else
		{
		
			pollcase = 2;
		
		}
	}
	else
	{
	
		ReadPoll( 0 );
		if( ( RecvBuff[5] & 0x01 ) != 0 )
		{
		
			pollcase = 3;
		
		}
		else
		{
		
			pollcase = 2;
		
		}
	
	}
	
	//Determines how the reader will poll the smart cards
	switch( pollcase )
	{
	
		case 1:
			DisplayOut( "Either reader can detect cards, but not both\n", GREEN );
			break;
		case 2:
			DisplayOut( "Automatic PICC polling is disabled, only ICC can detect card\n", GREEN );
			break;
		case 3:
			DisplayOut( "Both ICC and PICC readers can automatically detect card\n", GREEN );
			break;
	
	}

	autodet = true;
	btnAuto.SetWindowText( "End Auto Detection" );

	//Set the delay for the contactless card detection
	if( rPoll250.GetCheck() == true )
	{
	
		SetTimer(1, 250, TimerFunc1 );
	
	}
	if( rPoll500.GetCheck() == true )
	{
	
		SetTimer(1, 500, TimerFunc1 );
	
	}
	if( rPoll1.GetCheck() == true )
	{
	
		SetTimer(1, 1000, TimerFunc1 );
	
	}
	if( rPoll25.GetCheck() == true )
	{
	
		SetTimer(1, 2500, TimerFunc1 );
	
	}
	
}

void CPollingSampleDlg::OnClear() 
{
	
	rbResult.SetWindowText( "" );
	
}

void CPollingSampleDlg::OnReset() 
{

	rbResult.SetWindowText( "" );
	Initialize();
	
}

void CPollingSampleDlg::OnQuit() 
{
	
	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	CDialog::OnCancel();
	
}

void CPollingSampleDlg::OnPoll250() 
{

	rPoll500.SetCheck( false );
	rPoll1.SetCheck( false );
	rPoll25.SetCheck( false );

}

void CPollingSampleDlg::OnPoll500() 
{

	rPoll250.SetCheck( false );
	rPoll1.SetCheck( false );
	rPoll25.SetCheck( false );

}

void CPollingSampleDlg::OnPoll1() 
{

	rPoll250.SetCheck( false );
	rPoll500.SetCheck( false );
	rPoll25.SetCheck( false );

}

void CPollingSampleDlg::OnPoll25() 
{

	rPoll250.SetCheck( false );
	rPoll1.SetCheck( false );
	rPoll500.SetCheck( false );

}

//Gets the current configuration setting of the reader
void GetExMode( int reqType )
{
	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x2B;
	SendBuff[1] = 0x00;
	SendLen = 2;
	RecvLen = 7;

	sprintf( tempstr, "" );

	retCode = CallCardControl();

	for( index = 0; index < 5; index++ )
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "E100000002" ) == 0 )
	{
	
		if( reqType == 1 )
		{
		
			if( RecvBuff[5] == 0 )
			{
			
				pThis->rInterBoth.SetCheck( true );
				pThis->rInterEither.SetCheck( false );
			
			}
			else
			{

				pThis->rInterBoth.SetCheck( false );
				pThis->rInterEither.SetCheck( true );
			
			}

			if( RecvBuff[6] == 0 )
			{
			
				pThis->rEXnot.SetCheck( true );
				pThis->rEXactive.SetCheck( false );
			
			}
			else
			{
				
				pThis->rEXnot.SetCheck( false );
				pThis->rEXactive.SetCheck( true );
			
			}
		
		}
	
	}
	else
	{
	
		DisplayOut( "Wrong return Values from device\n", RED );
	
	}

}

//Gets the current polling options and poll interval of the reader
void ReadPoll( int reqType )
{

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

	if( reqType == 1 )
	{
	
		if( ( RecvBuff[5] & 0x01 ) != 0 )
		{
		
			DisplayOut( "> Automatic PICC polling is enabled\n", BLACK );
			pThis->check1.SetCheck( true );
		
		}
		else
		{
		
			DisplayOut( "> Automatic PICC polling is disabled\n", BLACK );
			pThis->check1.SetCheck( false );
		
		}

		if( ( RecvBuff[5] & 0x02 ) != 0 )
		{
		
			DisplayOut( "> Antenna off when no PICC found is enabled\n", BLACK );
			pThis->check2.SetCheck( true );
		
		}
		else
		{
		
			DisplayOut( "> Antenna off when no PICC found is disabled\n", BLACK );
			pThis->check2.SetCheck( false );		
		
		}

		if( ( RecvBuff[5] & 0x04 ) != 0 )
		{
		
			DisplayOut( "> Antenna off when PICC is inactive is enabled\n", BLACK );
			pThis->check3.SetCheck( true );

		}
		else
		{
		
			DisplayOut( "> Antenna off when PICC is inactive is disabled\n", BLACK );
			pThis->check3.SetCheck( false );
		
		}

		if( ( RecvBuff[5] & 0x08 ) != 0 )
		{
		
			DisplayOut( "> Activate PICC when detected is enabled\n", BLACK );
			pThis->check4.SetCheck( true );
		
		}
		else
		{
		
			DisplayOut( "> Activate PICC when detected is disabled\n", BLACK );
			pThis->check4.SetCheck( false );
		
		}

		if( ( ( RecvBuff[5] & 0x10 ) == 0 ) && ( ( RecvBuff[5] & 0x20 ) == 0 ) )
		{
		
			DisplayOut( "> Poll interval is 250 msec\n", BLACK );
			pThis->rPoll250.SetCheck( true );
			pThis->rPoll500.SetCheck( false );
			pThis->rPoll1.SetCheck( false );
			pThis->rPoll25.SetCheck( false );
		
		}
		if( ( ( RecvBuff[5] & 0x10 ) != 0 ) && ( ( RecvBuff[5] & 0x20 ) == 0 ) )
		{
		
			DisplayOut( "> Poll interval is 500 msec\n", BLACK );
			pThis->rPoll250.SetCheck( false );
			pThis->rPoll500.SetCheck( true );
			pThis->rPoll1.SetCheck( false );
			pThis->rPoll25.SetCheck( false );
		
		}
		if( ( ( RecvBuff[5] & 0x10 ) == 0 ) && ( ( RecvBuff[5] & 0x20 ) != 0 ) )
		{
		
			DisplayOut( "> Poll interval is 1 sec\n", BLACK );
			pThis->rPoll250.SetCheck( false );
			pThis->rPoll500.SetCheck( false );
			pThis->rPoll1.SetCheck( true );
			pThis->rPoll25.SetCheck( false );
		
		}
		if( ( ( RecvBuff[5] & 0x10 ) != 0 ) && ( ( RecvBuff[5] & 0x20 ) != 0 ) )
		{
		
			DisplayOut( "> Poll interval is 2.5 sec\n", BLACK );
			pThis->rPoll250.SetCheck( false );
			pThis->rPoll500.SetCheck( false );
			pThis->rPoll1.SetCheck( false );
			pThis->rPoll25.SetCheck( true );
		
		}

		if( ( RecvBuff[5] & 0x40 ) != 0 )
		{
		
			DisplayOut( "> Test Mode is enabled\n", BLACK );
			pThis->check5.SetCheck( true );
		
		}
		else
		{
		
			DisplayOut( "> Test Mode is disabled\n", BLACK );
			pThis->check5.SetCheck( false );
		
		}

		if( ( RecvBuff[5] & 0x80 ) != 0 )
		{
		
			DisplayOut( "> ISO14443A Part4 is enforced\n", BLACK );
			pThis->check6.SetCheck( true );
		
		}
		else
		{
		
			DisplayOut( "> ISO14443A Part4 is not enforced\n", BLACK );
			pThis->check6.SetCheck( false );

		
		}

	}

}

void CallCardConnect( int reqType )
{

	int index;
	char buffer[MAX];

	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );

	//Shared Connection
	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_SHARED,
							SCARD_PROTOCOL_T0 || SCARD_PROTOCOL_T1,
							&hCard,
							&dwActProtocol );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		if( reqType == 1 )
		{
		
			index = 0;
			pThis->cbReader.SetCurSel( index );
			index = pThis->cbReader.FindStringExact( -1, "ACS ACR128U SAM Interface 0" );
			
			if( index == CB_ERR )
			{
			
				DisplayOut( "Cannot find ACR128 SAM reader\n", RED );
				return;
			
			}
			index++;

			pThis->cbReader.SetCurSel( index );
			pThis->cbReader.GetLBText( pThis->cbReader.GetCurSel(), buffer );

			retCode = SCardConnect( hContext,
									buffer,
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
			
				sprintf( buffer, "Successful Connection to %s\n", buffer );
				DisplayOut( buffer, GREEN );

			}

		}
		else
		{
		
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;
		
		}
	
	}
	else
	{
	
		sprintf( buffer, "Successful to connection to %s\n", readerName );
		DisplayOut( buffer, GREEN );
	}

}


void CPollingSampleDlg::OnICC() 
{
	
	int tempnum = cbReader.FindStringExact( -1, "ACS ACR128U ICC Interface 0" );
	CString tempstr;
	
	cbReader.GetLBText( tempnum, tempstr );
	RdrState.szReader = tempstr;

	retCode = SCardGetStatusChangeA( hContext,
									 0,
									 &RdrState,
									 1 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return;
	
	}

    if (RdrState.dwEventState & SCARD_STATE_PRESENT)
	{

		m_bar.SetPaneText( 1, "Card is inserted" );

	}
    else
	{

        m_bar.SetPaneText( 1, "Card is removed" );

	}
	
}

void CPollingSampleDlg::OnPICC() 
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
		
		m_bar.SetPaneText( 3, "No card within range" );
		
	}
	else
	{
		
		m_bar.SetPaneText( 3, "Card is detected" );
		
	}
	
}
