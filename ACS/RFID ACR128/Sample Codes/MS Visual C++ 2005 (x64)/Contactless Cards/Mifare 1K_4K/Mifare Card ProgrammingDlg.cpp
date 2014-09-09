//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              MifareCardProgrammingDlg.cpp
//
//  Description:       This sample program demonstrates how to read data from and write data to
//					   PICC contactless card this also shows how to write to a value block and
//					   increment and decrement its value.
//
//  Author:            Wazer Emmanuel R. Benal
//
//	Date:              June 17, 2008
//
//	Revision Trail:   (Date/Author/Description)

//====================================================================================================

#include "stdafx.h"
#include "Mifare Card Programming.h"
#include "Mifare Card ProgrammingDlg.h"

//Define constants//////////////////////////////////////////////////
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 262
////////////////////////////////////////////////////////////////////

//Mifare Card Programming Inlude File
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
	BYTE					SendBuff[262], RecvBuff[262];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	int						reqtype;
	bool					autodet = false;
	CMifareCardProgrammingDlg	*pThis = NULL;

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void ClearBuffers();
static CString GetScardErrMsg( int code );
void DisplayOut( CString str, COLORREF color );
int SendAPDU( int SendType );
int HexCheck( char data1, char data2 );
void Initializer();

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

//Transmits APDU to Card
int SendAPDU( int SendType )
{

	char tempstr[MAX];
	int index;

	ioRequest.dwProtocol = dwActProtocol;
	ioRequest.cbPciLength = sizeof( SCARD_IO_REQUEST );
	
	//Display APDU In
	sprintf( tempstr, "< " );
	for( index = 0; index != SendLen; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, SendBuff[index] );
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );

	retCode = SCardTransmit( hCard,
							 &ioRequest,
							 SendBuff,
							 SendLen,
							 NULL,
							 RecvBuff,
							 &RecvLen );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return retCode;
	
	}
	else
	{
	
		sprintf( tempstr, "> " );
		switch( SendType )
		{
			
			case 0:	//Display SW1/SW2 value
				for( index = RecvLen - 2; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				if( strcmp( tempstr, "> 90 00 " ) != 0 )
				{
				
					DisplayOut( "Return bytes are not acceptable\n", RED );
				
				}
				break;
			case 1:	//Display ATR after checking SW1/SW2
				for( index = RecvLen - 2; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				if( strcmp( tempstr, "> 90 00 " ) != 0 )
				{
				
					DisplayOut( "Return bytes are not acceptable\n", RED );
				
				}
				else
				{
				
					sprintf( tempstr, "ATR : " );
					for( index = 0; index != RecvLen - 2; index++ )
					{
					
						sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
					
					}
				
				}
				break;
			case 2: //Display all data
				for( index = 0; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				break;
		}
		sprintf( tempstr, "%s\n", tempstr );
		DisplayOut( tempstr, BLACK );
	
	}

	return retCode;

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

//Initializes the buttons and edit boxes
void Initializer()
{

	DisplayOut( "Program Ready\n", GREEN );
	pThis->cbReader.SetWindowText( "" );

	pThis->btnInit.EnableWindow( true );
	pThis->btnClear.EnableWindow( true );
	pThis->btnQuit.EnableWindow( true );
	pThis->btnReset.EnableWindow( false );
	pThis->btnConnect.EnableWindow( false );
	pThis->btnLoadKey.EnableWindow( false );
	pThis->btnAuthen.EnableWindow( false );
	pThis->btnRead.EnableWindow( false );
	pThis->btnUpdate.EnableWindow( false );
	pThis->btnStoreVal.EnableWindow( false );
	pThis->btnInc.EnableWindow( false );
	pThis->btnDec.EnableWindow( false );
	pThis->btnReadVal.EnableWindow( false );
	pThis->btnRestoreVal.EnableWindow( false );

	pThis->tbKeyStore.EnableWindow( false );
	pThis->tbKeyVal1.EnableWindow( false );
	pThis->tbKeyVal2.EnableWindow( false );
	pThis->tbKeyVal3.EnableWindow( false );
	pThis->tbKeyVal4.EnableWindow( false );
	pThis->tbKeyVal5.EnableWindow( false );
	pThis->tbKeyVal6.EnableWindow( false );
	pThis->tbBlock.EnableWindow( false );
	pThis->tbKeyStoreNo.EnableWindow( false );
	pThis->tbKeyValIn1.EnableWindow( false );
	pThis->tbKeyValIn2.EnableWindow( false );
	pThis->tbKeyValIn3.EnableWindow( false );
	pThis->tbKeyValIn4.EnableWindow( false );
	pThis->tbKeyValIn5.EnableWindow( false );
	pThis->tbKeyValIn6.EnableWindow( false );
	pThis->tbStartBlock.EnableWindow( false );
	pThis->tbLen.EnableWindow( false );
	pThis->tbValue.EnableWindow( false );
	pThis->tbBlockNo.EnableWindow( false );
	pThis->tbSource.EnableWindow( false );
	pThis->tbTarget.EnableWindow( false );
	pThis->tbData.EnableWindow( false );

	pThis->rNonVol.EnableWindow( false );
	pThis->rVol.EnableWindow( false );
	pThis->rKeyA.EnableWindow( false );
	pThis->rKeyB.EnableWindow( false );
	pThis->rKeyMan.EnableWindow( false );
	pThis->rKeyVol.EnableWindow( false );
	pThis->rKeyNonVol.EnableWindow( false );

	pThis->tbKeyStore.SetWindowText( "" );
	pThis->tbKeyVal1.SetWindowText( "" );
	pThis->tbKeyVal2.SetWindowText( "" );
	pThis->tbKeyVal3.SetWindowText( "" );
	pThis->tbKeyVal4.SetWindowText( "" );
	pThis->tbKeyVal5.SetWindowText( "" );
	pThis->tbKeyVal6.SetWindowText( "" );
	pThis->tbBlock.SetWindowText( "" );
	pThis->tbKeyStoreNo.SetWindowText( "" );
	pThis->tbKeyValIn1.SetWindowText( "" );
	pThis->tbKeyValIn2.SetWindowText( "" );
	pThis->tbKeyValIn3.SetWindowText( "" );
	pThis->tbKeyValIn4.SetWindowText( "" );
	pThis->tbKeyValIn5.SetWindowText( "" );
	pThis->tbKeyValIn6.SetWindowText( "" );
	pThis->tbStartBlock.SetWindowText( "" );
	pThis->tbLen.SetWindowText( "" );
	pThis->tbValue.SetWindowText( "" );
	pThis->tbBlockNo.SetWindowText( "" );
	pThis->tbSource.SetWindowText( "" );
	pThis->tbTarget.SetWindowText( "" );
	pThis->tbData.SetWindowText( "" );

	pThis->rNonVol.SetCheck( false );
	pThis->rVol.SetCheck( false );
	pThis->rKeyA.SetCheck( false );
	pThis->rKeyB.SetCheck( false );
	pThis->rKeyMan.SetCheck( false );
	pThis->rKeyVol.SetCheck( false );
	pThis->rKeyNonVol.SetCheck( false );

	pThis->tbKeyStore.SetLimitText( 2 );
	pThis->tbKeyVal1.SetLimitText( 2 );
	pThis->tbKeyVal2.SetLimitText( 2 );
	pThis->tbKeyVal3.SetLimitText( 2 );
	pThis->tbKeyVal4.SetLimitText( 2 );
	pThis->tbKeyVal5.SetLimitText( 2 );
	pThis->tbKeyVal6.SetLimitText( 2 );
	pThis->tbBlock.SetLimitText( 3 );
	pThis->tbKeyStoreNo.SetLimitText( 2 );
	pThis->tbKeyValIn1.SetLimitText( 2 );
	pThis->tbKeyValIn2.SetLimitText( 2 );
	pThis->tbKeyValIn3.SetLimitText( 2 );
	pThis->tbKeyValIn4.SetLimitText( 2 );
	pThis->tbKeyValIn5.SetLimitText( 2 );
	pThis->tbKeyValIn6.SetLimitText( 2 );
	pThis->tbStartBlock.SetLimitText( 3 );
	pThis->tbLen.SetLimitText( 2 );
	pThis->tbBlockNo.SetLimitText( 3 );
	pThis->tbSource.SetLimitText( 3 );
	pThis->tbTarget.SetLimitText( 3 );

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
// CMifareCardProgrammingDlg dialog

CMifareCardProgrammingDlg::CMifareCardProgrammingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMifareCardProgrammingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMifareCardProgrammingDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CMifareCardProgrammingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMifareCardProgrammingDlg)
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_BUTTON14, btnQuit);
	DDX_Control(pDX, IDC_BUTTON13, btnReset);
	DDX_Control(pDX, IDC_BUTTON12, btnClear);
	DDX_Control(pDX, IDC_BUTTON11, btnRestoreVal);
	DDX_Control(pDX, IDC_BUTTON10, btnReadVal);
	DDX_Control(pDX, IDC_BUTTON9, btnDec);
	DDX_Control(pDX, IDC_BUTTON8, btnInc);
	DDX_Control(pDX, IDC_BUTTON5, btnStoreVal);
	DDX_Control(pDX, IDC_EDIT22, tbTarget);
	DDX_Control(pDX, IDC_EDIT21, tbSource);
	DDX_Control(pDX, IDC_EDIT20, tbBlock);
	DDX_Control(pDX, IDC_EDIT19, tbValue);
	DDX_Control(pDX, IDC_BUTTON7, btnUpdate);
	DDX_Control(pDX, IDC_BUTTON6, btnRead);
	DDX_Control(pDX, IDC_EDIT18, tbData);
	DDX_Control(pDX, IDC_EDIT17, tbLen);
	DDX_Control(pDX, IDC_EDIT16, tbStartBlock);
	DDX_Control(pDX, IDC_BUTTON4, btnAuthen);
	DDX_Control(pDX, IDC_EDIT15, tbKeyValIn6);
	DDX_Control(pDX, IDC_EDIT14, tbKeyValIn5);
	DDX_Control(pDX, IDC_EDIT13, tbKeyValIn4);
	DDX_Control(pDX, IDC_EDIT12, tbKeyValIn3);
	DDX_Control(pDX, IDC_EDIT11, tbKeyValIn2);
	DDX_Control(pDX, IDC_EDIT10, tbKeyValIn1);
	DDX_Control(pDX, IDC_EDIT9, tbKeyStoreNo);
	DDX_Control(pDX, IDC_EDIT8, tbBlockNo);
	DDX_Control(pDX, IDC_RADIO7, rKeyB);
	DDX_Control(pDX, IDC_RADIO6, rKeyA);
	DDX_Control(pDX, IDC_RADIO5, rKeyNonVol);
	DDX_Control(pDX, IDC_RADIO4, rKeyVol);
	DDX_Control(pDX, IDC_RADIO3, rKeyMan);
	DDX_Control(pDX, IDC_RADIO2, rVol);
	DDX_Control(pDX, IDC_RADIO1, rNonVol);
	DDX_Control(pDX, IDC_EDIT7, tbKeyVal6);
	DDX_Control(pDX, IDC_EDIT6, tbKeyVal5);
	DDX_Control(pDX, IDC_EDIT5, tbKeyVal4);
	DDX_Control(pDX, IDC_EDIT4, tbKeyVal3);
	DDX_Control(pDX, IDC_EDIT2, tbKeyVal1);
	DDX_Control(pDX, IDC_EDIT3, tbKeyVal2);
	DDX_Control(pDX, IDC_EDIT1, tbKeyStore);
	DDX_Control(pDX, IDC_BUTTON3, btnLoadKey);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMifareCardProgrammingDlg, CDialog)
	//{{AFX_MSG_MAP(CMifareCardProgrammingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON12, OnClear)
	ON_BN_CLICKED(IDC_BUTTON13, OnReset)
	ON_BN_CLICKED(IDC_BUTTON14, OnQuit)
	ON_BN_CLICKED(IDC_RADIO1, OnNonVol)
	ON_BN_CLICKED(IDC_RADIO2, OnVol)
	ON_BN_CLICKED(IDC_RADIO3, OnKeyMan)
	ON_BN_CLICKED(IDC_RADIO4, OnKeyVol)
	ON_BN_CLICKED(IDC_RADIO5, OnKeyNonVol)
	ON_BN_CLICKED(IDC_RADIO6, OnKeyA)
	ON_BN_CLICKED(IDC_RADIO7, OnKeyB)
	ON_BN_CLICKED(IDC_BUTTON3, OnLoadKey)
	ON_BN_CLICKED(IDC_BUTTON4, OnAuthen)
	ON_BN_CLICKED(IDC_BUTTON6, OnReadBlock)
	ON_BN_CLICKED(IDC_BUTTON7, OnUpdateBlock)
	ON_BN_CLICKED(IDC_BUTTON5, OnStoreVal)
	ON_BN_CLICKED(IDC_BUTTON8, OnInc)
	ON_BN_CLICKED(IDC_BUTTON9, OnDec)
	ON_BN_CLICKED(IDC_BUTTON10, OnReadVal)
	ON_BN_CLICKED(IDC_BUTTON11, OnRestore)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMifareCardProgrammingDlg message handlers

BOOL CMifareCardProgrammingDlg::OnInitDialog()
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
	Initializer();
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMifareCardProgrammingDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CMifareCardProgrammingDlg::OnPaint() 
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
HCURSOR CMifareCardProgrammingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CMifareCardProgrammingDlg::OnInit() 
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
		int i;
    	for (i=0;p[i];i++);
	      i++;
	    if ( *p != 0 )
		{
     		cbReader.AddString( p );
		}
		p = &p[i];
	}
	
	index = cbReader.FindStringExact( -1, "ACS ACR128U PICC Interface 0" );
	cbReader.SetCurSel( index );

	btnInit.EnableWindow( false );
	btnConnect.EnableWindow( true );
	
}

void CMifareCardProgrammingDlg::OnConnect() 
{
	
	DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];


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
	
		//Failed to connect to reader
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return;
		
	}

	//Successful connection to reader
	IO_REQ.dwProtocol = Protocol;
	IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

	cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
	sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
	DisplayOut( buffer1, GREEN );

	pThis->btnReset.EnableWindow( true );
	pThis->btnLoadKey.EnableWindow( true );
	pThis->btnAuthen.EnableWindow( true );
	pThis->btnRead.EnableWindow( true );
	pThis->btnUpdate.EnableWindow( true );
	pThis->btnStoreVal.EnableWindow( true );
	pThis->btnInc.EnableWindow( true );
	pThis->btnDec.EnableWindow( true );
	pThis->btnReadVal.EnableWindow( true );
	pThis->btnRestoreVal.EnableWindow( true );

	pThis->tbKeyStore.EnableWindow( true );
	pThis->tbKeyVal1.EnableWindow( true );
	pThis->tbKeyVal2.EnableWindow( true );
	pThis->tbKeyVal3.EnableWindow( true );
	pThis->tbKeyVal4.EnableWindow( true );
	pThis->tbKeyVal5.EnableWindow( true );
	pThis->tbKeyVal6.EnableWindow( true );
	pThis->tbBlock.EnableWindow( true );
	pThis->tbKeyStoreNo.EnableWindow( true );
	pThis->tbKeyValIn1.EnableWindow( true );
	pThis->tbKeyValIn2.EnableWindow( true );
	pThis->tbKeyValIn3.EnableWindow( true );
	pThis->tbKeyValIn4.EnableWindow( true );
	pThis->tbKeyValIn5.EnableWindow( true );
	pThis->tbKeyValIn6.EnableWindow( true );
	pThis->tbStartBlock.EnableWindow( true );
	pThis->tbLen.EnableWindow( true );
	pThis->tbValue.EnableWindow( true );
	pThis->tbBlockNo.EnableWindow( true );
	pThis->tbSource.EnableWindow( true );
	pThis->tbTarget.EnableWindow( true );
	pThis->tbData.EnableWindow( true );

	pThis->rNonVol.EnableWindow( true );
	pThis->rVol.EnableWindow( true );
	pThis->rKeyA.EnableWindow( true );
	pThis->rKeyA.SetCheck( true );
	pThis->rKeyB.EnableWindow( true );
	pThis->rKeyMan.EnableWindow( true );
	pThis->rKeyMan.SetCheck( true );
	pThis->rKeyVol.EnableWindow( true );
	pThis->rKeyNonVol.EnableWindow( true );
	
}

void CMifareCardProgrammingDlg::OnClear() 
{

	rbResult.SetWindowText( "" );

}

void CMifareCardProgrammingDlg::OnReset() 
{

	rbResult.SetWindowText( "" );
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	Initializer();

}

void CMifareCardProgrammingDlg::OnQuit() 
{

	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	CDialog::OnCancel();
	
}

void CMifareCardProgrammingDlg::OnNonVol() 
{

	rNonVol.SetCheck( true );
	rVol.SetCheck( false );

}

void CMifareCardProgrammingDlg::OnVol() 
{

	rVol.SetCheck( true );
	rNonVol.SetCheck( false );

}

void CMifareCardProgrammingDlg::OnKeyMan() 
{

	rKeyMan.SetCheck( true );
	rKeyVol.SetCheck( false );
	rKeyNonVol.SetCheck( false );
	tbKeyStoreNo.EnableWindow( false );
	tbKeyValIn1.EnableWindow( true );
	tbKeyValIn2.EnableWindow( true );
	tbKeyValIn3.EnableWindow( true );
	tbKeyValIn4.EnableWindow( true );
	tbKeyValIn5.EnableWindow( true );
	tbKeyValIn6.EnableWindow( true );

}

void CMifareCardProgrammingDlg::OnKeyVol() 
{
	
	rKeyMan.SetCheck( false );
	rKeyVol.SetCheck( true );
	rKeyNonVol.SetCheck( false );
	tbKeyStoreNo.EnableWindow( false );
	tbKeyValIn1.EnableWindow( false );
	tbKeyValIn2.EnableWindow( false );
	tbKeyValIn3.EnableWindow( false );
	tbKeyValIn4.EnableWindow( false );
	tbKeyValIn5.EnableWindow( false );
	tbKeyValIn6.EnableWindow( false );
	

}

void CMifareCardProgrammingDlg::OnKeyNonVol() 
{

	rKeyMan.SetCheck( false );
	rKeyVol.SetCheck( false );
	rKeyNonVol.SetCheck( true );
	tbKeyStoreNo.EnableWindow( true );
	tbKeyValIn1.EnableWindow( false );
	tbKeyValIn2.EnableWindow( false );
	tbKeyValIn3.EnableWindow( false );
	tbKeyValIn4.EnableWindow( false );
	tbKeyValIn5.EnableWindow( false );
	tbKeyValIn6.EnableWindow( false );
	
}

void CMifareCardProgrammingDlg::OnKeyA() 
{

	rKeyA.SetCheck( true );
	rKeyB.SetCheck( false );

}

void CMifareCardProgrammingDlg::OnKeyB() 
{

	rKeyA.SetCheck( false );
	rKeyB.SetCheck( true );

}

void CMifareCardProgrammingDlg::OnLoadKey() 
{
	
	char holder[4];
	int tempval;
	
	//Check for valid inputs
	if( rNonVol.GetCheck() == false && rVol.GetCheck() == false )
	{
	
		rNonVol.SetCheck( true );
		rNonVol.SetFocus();
	
	}

	tbKeyStore.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyStore.SetFocus();
		return;
	
	}

	tbKeyVal1.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyVal1.SetFocus();
		return;
	
	}
	tbKeyVal2.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyVal2.SetFocus();
		return;
	
	}
	tbKeyVal3.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyVal3.SetFocus();
		return;
	
	}
	tbKeyVal4.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyVal4.SetFocus();
		return;
	
	}
	tbKeyVal5.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyVal5.SetFocus();
		return;
	
	}
	tbKeyVal6.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbKeyVal6.SetFocus();
		return;
	
	}

	ClearBuffers();
	SendBuff[0] = 0xFF;							//CLA
	SendBuff[1] = 0x82;							//INS

	if( rNonVol.GetCheck() == true )
	{
	
		SendBuff[2] = 0x20;						//P1 : Non volatile memory

		tbKeyStore.GetWindowText( holder, 3 );
		sscanf( holder, "%X", &tempval );
		SendBuff[3] = tempval;					//P2 : Memory location
	
	}
	else if( rVol.GetCheck() == true )
	{
	
		SendBuff[2] = 0x00;						//P1 : Volatile memory
		SendBuff[3] = 0x20;						//P2 : Session Key
	
	}

	SendBuff[4] = 0x06;							//P3
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbKeyVal1.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[5] = tempval;						//Key 1 Value

	tbKeyVal2.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[6] = tempval;						//Key 2 Value

	tbKeyVal3.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[7] = tempval;						//Key 3 Value

	tbKeyVal4.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[8] = tempval;						//Key 4 Value

	tbKeyVal5.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[9] = tempval;						//Key 5 Value

	tbKeyVal6.GetWindowText( holder, 3 );
	sscanf( holder, "%X", &tempval );
	SendBuff[10] = tempval;						//Key 6 Value

	SendLen = 0x0B;
	RecvLen = 0x02;

	retCode = SendAPDU( 0 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CMifareCardProgrammingDlg::OnAuthen() 
{

	char holder[4];
	int tempval;

	//Validate input
	tbBlockNo.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbBlockNo.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tbBlockNo.SetWindowText( "319" );
		tbBlockNo.SetFocus();
		return;
	
	}

	if( rKeyMan.GetCheck() == true )
	{
	
		tbKeyValIn1.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbKeyValIn1.SetFocus();
			return;
		
		}
		tbKeyValIn2.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbKeyValIn2.SetFocus();
			return;
		
		}
		tbKeyValIn3.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbKeyValIn3.SetFocus();
			return;
		
		}
		tbKeyValIn4.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbKeyValIn4.SetFocus();
			return;
		
		}
		tbKeyValIn5.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbKeyValIn5.SetFocus();
			return;
		
		}
		tbKeyValIn6.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbKeyValIn6.SetFocus();
			return;
		
		}
	
	}
	else
	{
	
		if( rKeyNonVol.GetCheck() == true )
		{
		
			tbKeyStoreNo.GetWindowText( holder, 4 );
			sscanf( holder, "%X", &tempval );
			if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
			{
			
				tbKeyStoreNo.SetFocus();
				return;
			
			}
			else if( tempval > 0x1F )
			{
			
				tbKeyStoreNo.SetWindowText( "1F" );
				tbKeyStoreNo.SetFocus();
				return;
			
			}
		
		}
	
	}
	
	if( rKeyMan.GetCheck() == true )
	{
		//Store value in volatile memory
		ClearBuffers();
		SendBuff[0] = 0xFF;						//CLA
		SendBuff[2] = 0x00;						//P1 : Volatile memory
		SendBuff[1] = 0x82;						//INS
		SendBuff[3] = 0x20;						//P2  : session key
		SendBuff[4] = 0x06;						//P3
				
		//First put the input in a char array
		//then put it into a int variable using
		//sscanf
		tbKeyValIn1.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[5] = tempval;					//Key 1
		tbKeyValIn2.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[6] = tempval;					//Key 2
		tbKeyValIn3.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[7] = tempval;					//Key 3
		tbKeyValIn4.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[8] = tempval;					//Key 4
		tbKeyValIn5.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[9] = tempval;					//Key 5
		tbKeyValIn6.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[10] = tempval;					//Key 6

		SendLen = 0x0B;
		RecvLen = 0x02;

		retCode = SendAPDU( 0 );
		if( retCode != SCARD_S_SUCCESS )
		{
		
			return;
		
		}

		//Use volatile memory to authenticate
		ClearBuffers();
		SendBuff[0] = 0xFF;						//CLA
		SendBuff[2] = 0x00;						//P1 : Same for all source type
		SendBuff[1] = 0x86;						//INS : for stored key input
		SendBuff[3] = 0x00;						//P2 : for stored key input
		SendBuff[4] = 0x05;						//P3 : for stored key input
		SendBuff[5] = 0x01;						//Byte 1 : Version number
		SendBuff[6] = 0x00;						//Byte 2
		
		tbBlockNo.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		SendBuff[7] = tempval;					//Byte 3 : sectore no. for stored key input

		if( rKeyA.GetCheck() == true )
		{
		
			SendBuff[8] = 0x60;					//Byte 4 : Key A for stored input
		
		}
		else
		{
		
			SendBuff[8] = 0x61;					//Byte 4 : Key B for stored input
		
		}
		
		SendBuff[9] = 0x20;						//Byte 5 : Session key for volatile memory
	
	}
	else
	{
		
		ClearBuffers();
		SendBuff[0] = 0xFF;						//CLA
		SendBuff[2] = 0x00;						//P1 : Same for all source type
		SendBuff[1] = 0x86;						//INS : for stored key input
		SendBuff[3] = 0x00;						//P2  : for stored key input
		SendBuff[4] = 0x05;						//P3  : for stored key input
		SendBuff[5] = 0x01;						//Byte 1 : Version no.
		SendBuff[6] = 0x00;						//Byte 2
		
		tbBlock.GetWindowText( holder, 4 );
		sscanf( holder, "%d", &tempval );
		SendBuff[7] = tempval;					//Byte 3 : Sector No. for stored key input
		
		if( rKeyA.GetCheck() == true )
		{
		
			SendBuff[8] = 0x60;					//Byte 4 : Key A for stored key input
		
		}
		else
		{
		
			SendBuff[8] = 0x61;					//Byte 4 : Key B for stored key input
		
		}

		if( rKeyVol.GetCheck() == true )
		{
		
			SendBuff[9] = 0x20;					//Byte 5 : Session key for volatile memory
		
		}
		else
		{
		
			tbKeyStoreNo.GetWindowText( holder, 4 );
			sscanf( holder, "%X", &tempval );
			SendBuff[9] = tempval;				//Byte 5 : Key No. for non-volatile memory
		
		}
	
	}

	SendLen = 0x0A;
	RecvLen = 0x02;

	retCode = SendAPDU( 0 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CMifareCardProgrammingDlg::OnReadBlock() 
{

	char tempstr[MAX], holder[4];
	int index, tempval;

	tbData.SetWindowText( "" );
	
	//Validate inputs
	tbStartBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbStartBlock.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tbStartBlock.SetWindowText( "319" );
		tbStartBlock.SetFocus();
		return;
	
	}

	tbLen.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbLen.SetFocus();
		return;
	
	}
	
	ClearBuffers();							
	SendBuff[0] = 0xFF;						//CLA
	SendBuff[1] = 0xB0;						//INS
	SendBuff[2] = 0x00;						//P1
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbStartBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	SendBuff[3] = tempval;					//P2 : Starting Block No.

	tbLen.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	SendBuff[4] = tempval;					//P3 : Data length

	SendLen = 0x05;
	RecvLen = SendBuff[4] + 2;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "" );
	for( index = 0; index != RecvLen; index++ )
	{
	
		sprintf( tempstr, "%s%c", tempstr, RecvBuff[index] );
	
	}

	tbData.SetWindowText( tempstr );

}

void CMifareCardProgrammingDlg::OnUpdateBlock() 
{
	
	char tempstr[MAX], holder[50];
	int index, tempval;
	
	//Validate inputs
	tbStartBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbStartBlock.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tbStartBlock.SetWindowText( "319" );
		tbStartBlock.SetFocus();
		return;
	
	}

	tbData.GetWindowText( holder, 50 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbData.SetFocus();
		return;
	
	}

	tbLen.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbLen.SetFocus();
		return;
	
	}

	tbData.GetWindowText( tempstr, 50 );
	ClearBuffers();
	SendBuff[0] = 0xFF;						//CLA
	SendBuff[1] = 0xD6;						//INS
	SendBuff[2] = 0x00;						//P1
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbStartBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	SendBuff[3] = tempval;					//P2 : Starting Block No.

	tbLen.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	SendBuff[4] = tempval;					//P3 : Data length

	for( index = 0; index < strlen( tempstr ) - 1; index++ )
	{
	
		SendBuff[index + 5] = int( tempstr[index + 1] );		
	}

	SendLen = SendBuff[4] + 5;
	RecvLen = 0x02;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CMifareCardProgrammingDlg::OnStoreVal() 
{

	char holder[12];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;

	//Validate Inputs
	tbValue.GetWindowText( holder, 12 );
	sscanf( holder, "%lu", &tempval1 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbValue.SetFocus();
		return;
	
	}
	else if( tempval1 > 4294967294 )
	{
	
		tbValue.SetWindowText( "4294967294" );
		tbValue.SetFocus();
		return;
	
	}

	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbBlock.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tbBlock.SetWindowText( "319" );
		tbBlock.SetFocus();
		return;
	
	}

	tbSource.SetWindowText( "" );
	tbTarget.SetWindowText( "" );
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbValue.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval1 );
	Amount = tempval1;

	ClearBuffers();
	SendBuff[0] = 0xFF;							//CLA
	SendBuff[1] = 0xD7;							//INS
	SendBuff[2] = 0x00;							//P1

	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;						//P2 : Block No.

	SendBuff[4] = 0x05;							//Lc : Data length
	SendBuff[5] = 0x00;							//VB_OP Value
	
	SendBuff[6] = ( ( Amount >> 24 ) & 0xFF );	//Amount MSByte
	SendBuff[7] = ( ( Amount >> 16 ) & 0xFF );	//Amount middle byte
	SendBuff[8] = ( ( Amount >> 8 ) & 0xFF );	//Amount middle byte
	SendBuff[9] = ( Amount & 0xFF );			//Amount LSByte

	SendLen = SendBuff[4] + 5;
	RecvLen = 0x02;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CMifareCardProgrammingDlg::OnInc() 
{

	char holder[12];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;
	
	//Validate inputs
	tbValue.GetWindowText( holder, 12 );
	sscanf( holder, "%lu", &tempval1 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbValue.SetFocus();
		return;
	
	}
	else if( tempval1 > 4294967294 )
	{
	
		tbValue.SetWindowText( "4294967294" );
		tbValue.SetFocus();
		return;
	
	}

	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbBlock.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tbBlock.SetWindowText( "319" );
		tbBlock.SetFocus();
		return;
	
	}

	tbSource.SetWindowText( "" );
	tbTarget.SetWindowText( "" );
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbValue.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval1 );
	Amount = tempval1;

	ClearBuffers();
	SendBuff[0] = 0xFF;							//CLA
	SendBuff[1] = 0xD7;							//INS
	SendBuff[2] = 0x00;							//P1

	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;						//P2 : Block No.

	SendBuff[4] = 0x05;							//Lc : Data length
	SendBuff[5] = 0x01;							//VB_OP Value
	
	SendBuff[6] = ( ( Amount >> 24 ) & 0xFF );	//Amount MSByte
	SendBuff[7] = ( ( Amount >> 16 ) & 0xFF );	//Amount middle byte
	SendBuff[8] = ( ( Amount >> 8 ) & 0xFF );	//Amount middle byte
	SendBuff[9] = ( Amount & 0xFF );			//Amount LSByte

	SendLen = SendBuff[4] + 5;
	RecvLen = 0x02;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CMifareCardProgrammingDlg::OnDec() 
{

	char holder[12];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;
	
	//Validate inputs
	tbValue.GetWindowText( holder, 12 );
	sscanf( holder, "%lu", &tempval1 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbValue.SetFocus();
		return;
	
	}
	else if( tempval1 > 4294967294 )
	{
	
		tbValue.SetWindowText( "4294967294" );
		tbValue.SetFocus();
		return;
	
	}

	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbBlock.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tbBlock.SetWindowText( "319" );
		tbBlock.SetFocus();
		return;
	
	}

	tbSource.SetWindowText( "" );
	tbTarget.SetWindowText( "" );
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbValue.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval1 );
	Amount = tempval1;

	ClearBuffers();
	SendBuff[0] = 0xFF;							//CLA
	SendBuff[1] = 0xD7;							//INS
	SendBuff[2] = 0x00;							//P1

	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;						//P2 : Block No.

	SendBuff[4] = 0x05;							//Lc : Data length
	SendBuff[5] = 0x02;							//VB_OP Value
	
	SendBuff[6] = ( ( Amount >> 24 ) & 0xFF );	//Amount MSByte
	SendBuff[7] = ( ( Amount >> 16 ) & 0xFF );	//Amount middle byte
	SendBuff[8] = ( ( Amount >> 8 ) & 0xFF );	//Amount middle byte
	SendBuff[9] = ( Amount & 0xFF );			//Amount LSByte

	SendLen = SendBuff[4] + 5;
	RecvLen = 0x02;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}

void CMifareCardProgrammingDlg::OnReadVal() 
{

	char tempstr[MAX], holder[12];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;
	
	//Validate inputs
	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbBlock.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tbBlock.SetWindowText( "319" );
		tbBlock.SetFocus();
		return;
	
	}
	
	tbValue.SetWindowText( "" );
	tbSource.SetWindowText( "" );
	tbTarget.SetWindowText( "" );

	ClearBuffers();
	SendBuff[0] = 0xFF;						//CLA
	SendBuff[1] = 0xB1;						//INS
	SendBuff[2] = 0x00;						//P1
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbBlock.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;					//P2 : Block No.

	SendBuff[4] = 0x00;						//Le

	SendLen = 0x05;
	RecvLen = 0x06;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	Amount = RecvBuff[3];
	Amount = Amount + ( RecvBuff[2] * 256 );
	Amount = Amount + ( RecvBuff[1] * 256 * 256 );
	Amount = Amount + ( RecvBuff[0] * 256 * 256 * 256 );

	sprintf( tempstr, "%lu", Amount );
	tbValue.SetWindowText( tempstr );

}

void CMifareCardProgrammingDlg::OnRestore() 
{

	char holder[4];
	int tempval;
	
	//Validate inputs
	tbSource.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbSource.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tbSource.SetWindowText( "319" );
		tbSource.SetFocus();
		return;
	
	}

	tbTarget.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbTarget.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tbTarget.SetWindowText( "319" );
		tbTarget.SetFocus();
		return;
	
	}

	tbValue.SetWindowText( "" );
	tbBlock.SetWindowText( "" );

	ClearBuffers();	
	SendBuff[0] = 0xFF;						//CLA
	SendBuff[1] = 0xD7;						//INS
	SendBuff[2] = 0x00;						//P1
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tbSource.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	SendBuff[3] = tempval;					//P2 : Source Block No.

	SendBuff[4] = 0x02;						//Lc
	SendBuff[5] = 0x03;						//Data In Byte 1

	tbTarget.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	SendBuff[6] = tempval;					//P2 : Target Block No.
	
	SendLen = 0x07;
	RecvLen = 0x02;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}
