//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              PollingDlg.cpp
//
//  Description:       This sample program outlines the steps on how to
//                     automatically detect the insertion and removal
//                     of the smart card from the smartcard reader using
//                     the PC/SC platform.
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	           June 02, 2004
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
// PollingDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Polling.h"
#include "PollingDlg.h"

// ==========================================================================
// Polling include file
#include "Winscard.h"

// Polling GlobalVariables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	int						retCode;
    char					readerName [256];
	DWORD					size = 64;
	DWORD					SendLen,
							RecvLen;
    SCARD_READERSTATE		RdrState;
	CPollingDlg			    *pThis=NULL;
// ==========================================================================
#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

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
// CPollingDlg dialog

CPollingDlg::CPollingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPollingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPollingDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CPollingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPollingDlg)
	DDX_Control(pDX, IDC_EDIT1, tMsg);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON5, bQuit);
	DDX_Control(pDX, IDC_BUTTON4, bReset);
	DDX_Control(pDX, IDC_BUTTON3, bEnd);
	DDX_Control(pDX, IDC_BUTTON2, bStart);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPollingDlg, CDialog)
	//{{AFX_MSG_MAP(CPollingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnStartPolling)
	ON_BN_CLICKED(IDC_BUTTON3, OnEndPolling)
	ON_BN_CLICKED(IDC_BUTTON4, OnReset)
	ON_BN_CLICKED(IDC_BUTTON5, OnQuit)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

// ==========================================================================
// Polling internal routines

static CString GetScardErrMsg(int code)
{
	switch(code)
	{
	// Smartcard Reader interface errors
	case SCARD_E_CANCELLED:
		return ("The action was canceled by an SCardCancel request.");
		break;
	case SCARD_E_CANT_DISPOSE:
		return ("The system could not dispose of the media in the requested manner.");
		break;
	case SCARD_E_CARD_UNSUPPORTED:
		return ("The smart card does not meet minimal requirements for support.");
		break;
	case SCARD_E_DUPLICATE_READER:
		return ("The reader driver didn't produce a unique reader name.");
		break;
	case SCARD_E_INSUFFICIENT_BUFFER:
		return ("The data buffer for returned data is too small for the returned data.");
		break;
	case SCARD_E_INVALID_ATR:
		return ("An ATR string obtained from the registry is not a valid ATR string.");
		break;
	case SCARD_E_INVALID_HANDLE:
		return ("The supplied handle was invalid.");
		break;
	case SCARD_E_INVALID_PARAMETER:
		return ("One or more of the supplied parameters could not be properly interpreted.");
		break;
	case SCARD_E_INVALID_TARGET:
		return ("Registry startup information is missing or invalid.");
		break;
	case SCARD_E_INVALID_VALUE:
		return ("One or more of the supplied parameter values could not be properly interpreted.");
		break;
	case SCARD_E_NOT_READY:
		return ("The reader or card is not ready to accept commands.");
		break;
	case SCARD_E_NOT_TRANSACTED:
		return ("An attempt was made to end a non-existent transaction.");
		break;
	case SCARD_E_NO_MEMORY:
		return ("Not enough memory available to complete this command.");
		break;
	case SCARD_E_NO_SERVICE:
		return ("The smart card resource manager is not running.");
		break;
	case SCARD_E_NO_SMARTCARD:
		return ("The operation requires a smart card, but no smart card is currently in the device.");
		break;
	case SCARD_E_PCI_TOO_SMALL:
		return ("The PCI receive buffer was too small.");
		break;
	case SCARD_E_PROTO_MISMATCH:
		return ("The requested protocols are incompatible with the protocol currently in use with the card.");
		break;
	case SCARD_E_READER_UNAVAILABLE:
		return ("The specified reader is not currently available for use.");
		break;
	case SCARD_E_READER_UNSUPPORTED:
		return ("The reader driver does not meet minimal requirements for support.");
		break;
	case SCARD_E_SERVICE_STOPPED:
		return ("The smart card resource manager has shut down.");
		break;
	case SCARD_E_SHARING_VIOLATION:
		return ("The smart card cannot be accessed because of other outstanding connections.");
		break;
	case SCARD_E_SYSTEM_CANCELLED:
		return ("The action was canceled by the system, presumably to log off or shut down.");
		break;
	case SCARD_E_TIMEOUT:
		return ("The user-specified timeout value has expired.");
		break;
	case SCARD_E_UNKNOWN_CARD:
		return ("The specified smart card name is not recognized.");
		break;
	case SCARD_E_UNKNOWN_READER:
		return ("The specified reader name is not recognized.");
		break;
	case SCARD_F_COMM_ERROR:
		return ("An internal communications error has been detected.");
		break;
	case SCARD_F_INTERNAL_ERROR:
		return ("An internal consistency check failed.");
		break;
	case SCARD_F_UNKNOWN_ERROR:
		return ("An internal error has been detected, but the source is unknown.");
		break;
	case SCARD_F_WAITED_TOO_LONG:
		return ("An internal consistency timer has expired.");
		break;
	case SCARD_W_REMOVED_CARD:
		return ("The smart card has been removed and no further communication is possible.");
		break;
	case SCARD_W_RESET_CARD:
		return ("The smart card has been reset, so any shared state information is invalid.");
		break;
	case SCARD_W_UNPOWERED_CARD:
		return ("Power has been removed from the smart card and no further communication is possible.");
		break;
	case SCARD_W_UNRESPONSIVE_CARD:
		return ("The smart card is not responding to a reset.");
		break;
	case SCARD_W_UNSUPPORTED_CARD:
		return ("The reader cannot communicate with the card due to ATR string configuration conflicts.");
		break;
	case NO_READER_INSTALLED:
		return ("The smartcard reader is not installed in your system.");
		break;
	}
	return ("Error is not documented.");
}

/////////////////////////////////////////////////////////////////////////////
// CPollingDlg message handlers

BOOL CPollingDlg::OnInitDialog()
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
	pThis->tMsg.SetWindowText("");
	pThis->bStart.EnableWindow(FALSE);
	pThis->bEnd.EnableWindow(FALSE);
	pThis->bReset.EnableWindow(FALSE);
    pThis->tMsg.SetWindowText("Initialize Smart Card resources.");

	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CPollingDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CPollingDlg::OnPaint() 
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

void CALLBACK MyTimerFunc (HWND, UINT, UINT_PTR, DWORD)
{
	// this is the timer routine
	int tmpInt = pThis->cbReader.GetCurSel();
	CString tmpStr; 

    pThis->tMsg.SetWindowText("");

	pThis->cbReader.GetLBText(tmpInt, tmpStr);
	RdrState.szReader = tmpStr;
    retCode = SCardGetStatusChangeA(hContext,
                                    0,
                                    &RdrState,
                                    1);
	if (retCode != SCARD_S_SUCCESS)
	{
        pThis->tMsg.SetWindowText(GetScardErrMsg(retCode));
		pThis->KillTimer(1);
		return;
	}
    if (RdrState.dwEventState & SCARD_STATE_PRESENT)
        pThis->tMsg.SetWindowText("Card is inserted.");
    else
        pThis->tMsg.SetWindowText("Card is removed.");
	pThis->tMsg.RedrawWindow();
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CPollingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CPollingDlg::OnInit() 
{
	int nLength = 64;
	
  // 1. Initialize SC reader
  //  1.1. Establish context
 	retCode = SCardEstablishContext (SCARD_SCOPE_USER,
								NULL,
								NULL,
								&hContext);
	if (retCode != SCARD_S_SUCCESS)
	{
        tMsg.SetWindowText(GetScardErrMsg(retCode));
        return;
	}

  //  1.2. List PC/SC readers
	size = 256;
	retCode = SCardListReaders (hContext,
							NULL,
							readerName,
							&size);
	if (retCode != SCARD_S_SUCCESS)
	{
        tMsg.SetWindowText(GetScardErrMsg(retCode));
        return;
	}
	if (readerName == NULL)
	{
        tMsg.SetWindowText("No PC/SC reader is detected in your system.");
    	return;
	}
	
	cbReader.ResetContent();
	char *p = readerName;
	while (*p)
	{
		int i;
    	for (int i=0;p[i];i++);
	      i++;
	    if (*p != 0)
		{
     		cbReader.AddString(p);
		}
		p = &p[i];
	}
	cbReader.SetCurSel(0);
    tMsg.SetWindowText("Start polling to detect card in reader.");
	pThis->bInit.EnableWindow(FALSE);
	pThis->bReset.EnableWindow(TRUE);
	bStart.EnableWindow(TRUE);
	
}

void CPollingDlg::OnStartPolling() 
{
	pThis->bStart.EnableWindow(FALSE);
	pThis->bEnd.EnableWindow(TRUE);
    tMsg.SetWindowText("");

	// launch timer routine to check status of reader
	this->SetTimer (1, 100, MyTimerFunc);
	
}

void CPollingDlg::OnEndPolling() 
{
	pThis->bStart.EnableWindow(TRUE);
	pThis->bEnd.EnableWindow(FALSE);
    tMsg.SetWindowText("Polling routine ended.");
	
	// stop the timer rotine
	pThis->KillTimer(1);
	
}

void CPollingDlg::OnReset() 
{
	pThis->bInit.EnableWindow(TRUE);
	pThis->bStart.EnableWindow(FALSE);
	pThis->bEnd.EnableWindow(FALSE);
	pThis->KillTimer(1);
    // Close SC reader
	retCode = SCardReleaseContext(hContext);

	cbReader.ResetContent();
    cbReader.EnableWindow(TRUE);
    tMsg.SetWindowText("Initialize Smart Card resources.");
	
}

void CPollingDlg::OnQuit() 
{
    int tmpResult = 0;

    // Close SC reader
	retCode = SCardReleaseContext(hContext);
    this->EndDialog(tmpResult);
	
}
