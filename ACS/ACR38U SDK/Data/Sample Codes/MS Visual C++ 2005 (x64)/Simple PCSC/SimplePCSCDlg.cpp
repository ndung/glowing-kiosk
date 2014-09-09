/////////////////////////////////////////////////////////////////////////////
//
// Company	: ADVANCED CARD SYSTEMS, LTD.
//
// Name		: SimplePCSCDlg.cpp : implementation file
//
// Author	: Alcendor Lorzano Chan
//
//	Date	: 14 / 09 / 2001 
//
// Revision Trail: Date/Author/Description
// 06September2005/J.I.R. Mission/ Allows APDU input
///////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "SimplePCSC.h"
#include "SimplePCSCDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

// Global variables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	LPCBYTE					pbSend;
	DWORD					dwSend, dwRecv, size = 64;
	LPBYTE					pbRecv;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode;
    char					readerName [256];
	DWORD					SendLen,
							RecvLen,
							nBytesRet;
	BYTE					SendBuff[262],
							RecvBuff[262];
	char					rName[256];
	CSimplePCSCDlg	    *pThis=NULL;

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
// CSimplePCSCDlg dialog

CSimplePCSCDlg::CSimplePCSCDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSimplePCSCDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSimplePCSCDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CSimplePCSCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSimplePCSCDlg)
	DDX_Control(pDX, IDC_BTNTransmit, bTransmit);
	DDX_Control(pDX, IDC_BTNStatus, bStatus);
	DDX_Control(pDX, IDC_BTNReleaseContext, bReleaseContext);
	DDX_Control(pDX, IDC_BTNListReaders, bListReaders);
	DDX_Control(pDX, IDC_BTNEstablishContext, bEstContext);
	DDX_Control(pDX, IDC_BTNEndTransaction, bEndTransaction);
	DDX_Control(pDX, IDC_BTNDisconnect, bDisconnect);
	DDX_Control(pDX, IDC_BTNConnect, bConnect);
	DDX_Control(pDX, IDC_BTNBeginTransaction, bBeginTransaction);
	DDX_Control(pDX, IDC_COMBOREADER, cbReader);
	DDX_Control(pDX, IDC_EDIT1, tDataIn);
	DDX_Control(pDX, IDC_LIST1, m_List);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CSimplePCSCDlg, CDialog)
	//{{AFX_MSG_MAP(CSimplePCSCDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BTNExit, OnBTNExit)
	ON_BN_CLICKED(IDC_BTNEstablishContext, OnBTNEstablishContext)
	ON_BN_CLICKED(IDC_BTNReleaseContext, OnBTNReleaseContext)
	ON_BN_CLICKED(IDC_BTNListReaders, OnBTNListReaders)
	ON_BN_CLICKED(IDC_BTNConnect, OnBTNConnect)
	ON_BN_CLICKED(IDC_BTNDisconnect, OnBTNDisconnect)
	ON_BN_CLICKED(IDC_BTNStatus, OnBTNStatus)
	ON_BN_CLICKED(IDC_BTNBeginTransaction, OnBTNBeginTransaction)
	ON_BN_CLICKED(IDC_BTNEndTransaction, OnBTNEndTransaction)
	ON_BN_CLICKED(IDC_BTNTransmit, OnBTNTransmit)
	ON_BN_CLICKED(IDC_BTNClear, OnBTNClear)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

// ==========================================================================
// SimplePCSCDlg internal routines

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
	}
	return ("Error is not documented.");
}

void InitButtons()
{
	pThis->bListReaders.EnableWindow(FALSE);
	pThis->bConnect.EnableWindow(FALSE);
	pThis->bBeginTransaction.EnableWindow(FALSE);
	pThis->bStatus.EnableWindow(FALSE);
	pThis->bTransmit.EnableWindow(FALSE);
	pThis->bEndTransaction.EnableWindow(FALSE);
	pThis->bDisconnect.EnableWindow(FALSE);
	pThis->bReleaseContext.EnableWindow(FALSE);
}
void DisableAPDUInput()
{

	pThis->tDataIn.SetWindowText("");
	pThis->tDataIn.EnableWindow(FALSE);
}

void DisplayOut(int errType, int retVal, CString prtText)
{
  char buffer[300];

  switch (errType){
  case 0:                          // Notifications
	  pThis->m_List.AddString(prtText);
	  break;
  case 1:                          // Error Messages
	  pThis->m_List.AddString(GetScardErrMsg(retVal));
	  break;
  case 2:						   // Input data
	  sprintf(buffer, "< %s", prtText);
	  pThis->m_List.AddString(buffer);
	  break;
  case 3:                          // Output data
	  sprintf(buffer, "> %s", prtText);
	  pThis->m_List.AddString(buffer);
	  break;
  case 4:                          // Output data
	  sprintf(buffer, "ATR Value: %s", prtText);
	  pThis->m_List.AddString(buffer);
	  break;
  }
}

int SendAPDU()
{

	ioRequest.dwProtocol = dwActProtocol;
	ioRequest.cbPciLength = sizeof(SCARD_IO_REQUEST);
	RecvLen = 262;
	retCode = SCardTransmit( hCard,
						&ioRequest,
						SendBuff,
						SendLen,
						NULL,
						RecvBuff,
						&RecvLen);
 	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return retCode;
	}

	return retCode;

}


/////////////////////////////////////////////////////////////////////////////
// CSimplePCSCDlg message handlers

BOOL CSimplePCSCDlg::OnInitDialog()
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
	DisableAPDUInput();
	InitButtons();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CSimplePCSCDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CSimplePCSCDlg::OnPaint() 
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
HCURSOR CSimplePCSCDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CSimplePCSCDlg::OnBTNExit() 
{
	// TODO: Add your control notification handler code here
	retCode = SCardEndTransaction(hCard,SCARD_LEAVE_CARD);
	retCode = SCardDisconnect(hCard,SCARD_RESET_CARD);
	retCode = SCardReleaseContext(hContext);
	CDialog::OnOK();
}


void CSimplePCSCDlg::OnBTNEstablishContext() 
{
	// TODO: Add your control notification handler code here
	retCode = SCardEstablishContext(SCARD_SCOPE_USER,
						  NULL,
						  NULL,
						  &hContext);
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	DisplayOut(0, 0, "SCardEstablishContext...OK");
	pThis->bEstContext.EnableWindow(FALSE);
	pThis->bListReaders.EnableWindow(TRUE);
	pThis->bReleaseContext.EnableWindow(TRUE);
	Scrolldown();

}

void CSimplePCSCDlg::OnBTNReleaseContext() 
{
	// TODO: Add your control notification handler code here
	
	retCode = SCardReleaseContext(hContext);
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	DisplayOut(0, 0, "SCardReleaseContext...OK");
	Scrolldown();
	pThis->bEstContext.EnableWindow(TRUE);
	pThis->bListReaders.EnableWindow(FALSE);
	pThis->bConnect.EnableWindow(FALSE);
	pThis->bReleaseContext.EnableWindow(FALSE);
	pThis->cbReader.ResetContent();
}

void CSimplePCSCDlg::OnBTNListReaders() 
{
	// TODO: Add your control notification handler code here
	StringList ReaderList;
	cbReader.ResetContent(); // clear contents of the combobox;
	
 	retCode = SCardEstablishContext (SCARD_SCOPE_USER,
								NULL,
								NULL,
								&hContext);

	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	retCode = SCardListReaders (hContext,
							NULL,
							readerName,
							&size);

	
	
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}
	if (readerName == NULL)
	{
        DisplayOut(0, retCode, "No PC/SC reader is detected in your system.");
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
    DisplayOut(0, retCode, "SCardListReaders...OK");
	pThis->bConnect.EnableWindow(TRUE);

}


/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNConnect() 
{
	// TODO: Add your control notification handler code here
	int tmpInt = cbReader.GetCurSel();


	cbReader.GetLBText(tmpInt, rName);

	retCode = SCardConnect(hContext,
						rName,
						SCARD_SHARE_SHARED,
						SCARD_PROTOCOL_T0,
						&hCard,
						&dwActProtocol);
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}
    DisplayOut(0, 0, "SCardConnect...OK");
	pThis->bListReaders.EnableWindow(FALSE);
	pThis->bConnect.EnableWindow(FALSE);
	pThis->bBeginTransaction.EnableWindow(TRUE);
	pThis->bDisconnect.EnableWindow(TRUE);
	pThis->bReleaseContext.EnableWindow(FALSE);
	Scrolldown();
}


/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNDisconnect() 
{
	// TODO: Add your control notification handler code here
	retCode = SCardDisconnect(hCard,SCARD_RESET_CARD);

	
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	DisplayOut(0, 0, "SCardDisconnect...OK");

	Scrolldown();
	pThis->bListReaders.EnableWindow(TRUE);
	pThis->bConnect.EnableWindow(TRUE);
	pThis->bBeginTransaction.EnableWindow(FALSE);
	pThis->bDisconnect.EnableWindow(FALSE);
	pThis->bReleaseContext.EnableWindow(TRUE);
}


/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNStatus() 
{
	// TODO: Add your control notification handler code here
	char rName[100];
	DWORD ReaderLen = 100, ATRLen = 32, dwState;
    BYTE ATRVal[40];
	char tmpArray[150];
	int i,j;

    retCode = SCardStatusA(hCard,
                            rName,
                            &ReaderLen,
                            &dwState,
                            &dwActProtocol,
                            ATRVal,
                            &ATRLen);
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	for (i = j = 0;i < ATRLen; i++)
	{
       sprintf(&tmpArray[j], "%02X ", ATRVal[i]);
	   j += 3;
	}
	DisplayOut(4, 0, tmpArray);

	
	switch(dwState)
	{
		case SCARD_UNKNOWN     : m_List.AddString("State : SCARD_UNKNOWN"); break;
		case SCARD_ABSENT      : m_List.AddString("State : SCARD_ABSENT");break;
		case SCARD_PRESENT     : m_List.AddString("State : SCARD_PRESENT");break;
		case SCARD_SWALLOWED   : m_List.AddString("State : SCARD_SWALLOWED");break;
		case SCARD_POWERED     : m_List.AddString("State : SCARD_POWERED");break;
		case SCARD_NEGOTIABLE  : m_List.AddString("State : SCARD_NEGOTIABLE");break;
		case SCARD_SPECIFIC    : m_List.AddString("State : SCARD_SPECIFIC");break;
	}; // switch case 

		
	switch(dwActProtocol)
	{
		case SCARD_PROTOCOL_UNDEFINED :m_List.AddString("Protocol : SCARD_PROTOCOL_UNDEFINED"); break;
		case SCARD_PROTOCOL_T0        :m_List.AddString("Protocol : SCARD_PROTOCOL_T0"); break;		
		case SCARD_PROTOCOL_T1        :m_List.AddString("Protocol :SCARD_PROTOCOL_T1"); break;			
		case SCARD_PROTOCOL_RAW       :m_List.AddString("Protocol :SCARD_PROTOCOL_RAW"); break;			
		case SCARD_PROTOCOL_DEFAULT   :m_List.AddString("Protocol :SCARD_PROTOCOL_DEFAULT"); break;			
	}; // switch case
	
	Scrolldown();
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNBeginTransaction() 
{
	// TODO: Add your control notification handler code here
	retCode = SCardBeginTransaction(hCard);

	
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	DisplayOut(0, 0, "SCardBeginTransaction...OK");

	Scrolldown();
	tDataIn.EnableWindow(TRUE);
	pThis->bBeginTransaction.EnableWindow(FALSE);
	pThis->bStatus.EnableWindow(TRUE);
	pThis->bTransmit.EnableWindow(TRUE);
	pThis->bEndTransaction.EnableWindow(TRUE);
	pThis->bDisconnect.EnableWindow(FALSE);

}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNEndTransaction() 
{
	// TODO: Add your control notification handler code here
	retCode = SCardEndTransaction(hCard,SCARD_LEAVE_CARD);

	
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
        return;
	}

	DisplayOut(0, 0, "SCardEndTransaction...OK");

	Scrolldown();
	pThis->bBeginTransaction.EnableWindow(TRUE);
	pThis->bStatus.EnableWindow(FALSE);
	pThis->bTransmit.EnableWindow(FALSE);
	pThis->bEndTransaction.EnableWindow(FALSE);
	pThis->bDisconnect.EnableWindow(TRUE);
	DisableAPDUInput();
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNTransmit() 
{
	// TODO: Add your control notification handler code here
	char tmpString[257], tmpStr[257], tmpData[257], buffer[257];
	int tmpLen, i, j, k;

	tmpLen = tDataIn.GetWindowText(tmpStr, 257);
	if  (tmpLen < 1){
		pThis->tDataIn.SetFocus();
		m_List.AddString("No input data");
		return;
	}
    j = 0;
	for (i=0;tmpStr[i] != 0; i++){
		if (tmpStr[i] != 32) { 
			tmpData[j] = tmpStr[i];
			j++;
		}
	}
	if (j%2 != 0){
		pThis->tDataIn.SetFocus();
		m_List.AddString("Invalid input data, uneven number of characters");
		return;
	}

	if (j/2 < 5){
		pThis->tDataIn.SetFocus();
		m_List.AddString("Insufficient input data");
		return;
	}

	if (j/2 < 6){
	    strcpy(tmpString, "");
		for (k=i=0; i < 5; i++){
			sscanf(&tmpData[k], "%02X", &SendBuff[i]);
			k += 2;
		}
		for(i=0; i<5; i++){
			sprintf(buffer, "%02X ", SendBuff[i] & 0x00FF);
			strcat(tmpString, buffer);
		}
		SendLen = 5;
	}
	else
	{
	    strcpy(tmpString, "");
		for (k=i=0; i < 5; i++){
			sscanf(&tmpData[k], "%02X", &SendBuff[i]);
			k += 2;
		}
		SendLen = SendBuff[4] + 5;
		if (j/2 < SendLen){
			pThis->tDataIn.SetFocus();
			m_List.AddString("Insufficient input data");
			return;
		}
		for (k=i=0; i < SendBuff[4]; i++){
			sscanf(&tmpData[k+10], "%02X", &SendBuff[i+5]);
			k += 2;
		}
		
		for(i=0; i<SendLen; i++){
			sprintf(buffer, "%02X ", SendBuff[i] & 0x00FF);
			strcat(tmpString, buffer);
		}
	}

	DisplayOut(2, 0, tmpString);
	retCode = SendAPDU();
	
	if (retCode != SCARD_S_SUCCESS)
	{
        return;
	}

	strcpy(tmpString, "");
	for(i=0; i<RecvLen; i++){
		sprintf(buffer, "%02X ", RecvBuff[i] & 0x00FF);
		strcat(tmpString, buffer);
	}

	DisplayOut(3, 0, tmpString);
	Scrolldown();
}



/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::OnBTNClear() 
{
	// TODO: Add your control notification handler code here
	m_List.ResetContent();
}


/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
void CSimplePCSCDlg::Scrolldown()
{
	m_List.SetCurSel(m_List.GetCount()-1);
}

