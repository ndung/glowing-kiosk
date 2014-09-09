//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              IICDlg.cpp
//
//  Description:       This sample program outlines the steps on how to
//                     program IIC memory cards using ACS readers
//                     in PC/SC platform.
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	           June 22, 2004
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
// IICDlg.cpp : implementation file
//

#include "stdafx.h"
#include "IIC.h"
#include "IICDlg.h"

// ==========================================================================
// IIC include file
#include "Winscard.h"

// IIC GlobalVariables
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
	CmdBytes				apdu;
	BYTE					SendBuff[262],
							RecvBuff[262];
	BOOL					connActive;
	CIICDlg			    *pThis=NULL;
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
// CIICDlg dialog

CIICDlg::CIICDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CIICDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CIICDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CIICDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CIICDlg)
	DDX_Control(pDX, IDC_LIST1, mMsg);
	DDX_Control(pDX, IDC_EDIT6, tData);
	DDX_Control(pDX, IDC_EDIT4, tLen);
	DDX_Control(pDX, IDC_EDIT3, tLoAdd);
	DDX_Control(pDX, IDC_EDIT2, tHiAdd);
	DDX_Control(pDX, IDC_EDIT1, tBitAdd);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_COMBO2, cbCardType);
	DDX_Control(pDX, IDC_COMBO3, cbPageSize);
	DDX_Control(pDX, IDC_BUTTON7, bReset);
	DDX_Control(pDX, IDC_BUTTON6, bQuit);
	DDX_Control(pDX, IDC_BUTTON5, bSet);
	DDX_Control(pDX, IDC_BUTTON4, bRead);
	DDX_Control(pDX, IDC_BUTTON3, bWrite);
	DDX_Control(pDX, IDC_BUTTON2, bConnect);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CIICDlg, CDialog)
	//{{AFX_MSG_MAP(CIICDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON6, OnQuit)
	ON_BN_CLICKED(IDC_BUTTON7, OnReset)
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_CBN_EDITCHANGE(IDC_COMBO1, OnEditchangecbReader)
	ON_CBN_EDITCHANGE(IDC_COMBO2, OnEditchangeCombo2)
	ON_BN_CLICKED(IDC_BUTTON5, OnSetPageSize)
	ON_BN_CLICKED(IDC_BUTTON4, OnRead)
	ON_BN_CLICKED(IDC_BUTTON3, OnWrite)
	ON_CBN_SELCHANGE(IDC_COMBO1, OnSelchangeCombo1)
	ON_CBN_SELCHANGE(IDC_COMBO2, OnSelchangeCombo2)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

// ==========================================================================
// IIC internal routines

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

void ClearBuffers()
{
	int indx;
    for (indx = 0;indx<263;indx++)
    {
      SendBuff[indx] = 0x00;
      RecvBuff[indx] = 0x00;
	}
}

void ClearFields()
{
	pThis->cbPageSize.SetCurSel(-1);
	pThis->tBitAdd.SetWindowText("");
	pThis->tHiAdd.SetWindowText("");
	pThis->tLoAdd.SetWindowText("");
	pThis->tLen.SetWindowText("");
	pThis->tData.SetWindowText("");

}

void EnableFields()
{
	pThis->cbPageSize.EnableWindow(TRUE);
	pThis->tBitAdd.EnableWindow(FALSE);
	pThis->tBitAdd.SetLimitText(1);
	pThis->tHiAdd.EnableWindow(TRUE);
	pThis->tHiAdd.SetLimitText(2);
	pThis->tLoAdd.EnableWindow(TRUE);
	pThis->tLoAdd.SetLimitText(2);
	pThis->tLen.EnableWindow(TRUE);
	pThis->tLen.SetLimitText(2);
	pThis->tData.EnableWindow(TRUE);
	pThis->cbPageSize.AddString("8-byte");
	pThis->cbPageSize.AddString("16-byte");
	pThis->cbPageSize.AddString("32-byte");
	pThis->cbPageSize.AddString("64-byte");
	pThis->cbPageSize.AddString("128-byte");
	pThis->cbPageSize.SetCurSel(0);
}

void DisableFields()
{
	pThis->cbPageSize.ResetContent();
	pThis->cbPageSize.EnableWindow(FALSE);
	pThis->tBitAdd.EnableWindow(FALSE);
	pThis->tHiAdd.EnableWindow(FALSE);
	pThis->tLoAdd.EnableWindow(FALSE);
	pThis->tLen.EnableWindow(FALSE);
	pThis->tData.EnableWindow(FALSE);
}

void InitMenu()
{
	pThis->cbReader.ResetContent();
	pThis->cbCardType.ResetContent();
    pThis->mMsg.ResetContent();
	pThis->cbReader.EnableWindow(FALSE);
	pThis->cbCardType.EnableWindow(FALSE);
	pThis->bInit.EnableWindow(TRUE);
	pThis->bConnect.EnableWindow(FALSE);
	pThis->bRead.EnableWindow(FALSE);
	pThis->bWrite.EnableWindow(FALSE);
	pThis->bReset.EnableWindow(FALSE);
	pThis->bSet.EnableWindow(FALSE);
	ClearFields();
	DisableFields();
}

void DisplayOut(int errType, int retVal, CString prtText)
{
  char buffer[300];

  switch (errType){
  case 0:                          // Notifications
	  pThis->mMsg.AddString(prtText);
	  break;
  case 1:                          // Error Messages
	  pThis->mMsg.AddString(GetScardErrMsg(retVal));
	  break;
  case 2:						   // Input data
	  sprintf(buffer, "< %s", prtText);
	  pThis->mMsg.AddString(buffer);
	  break;
  case 3:                          // Output data
	  sprintf(buffer, "> %s", prtText);
	  pThis->mMsg.AddString(buffer);
	  break;
  }

}

void AddButtons()
{
	pThis->bInit.EnableWindow(FALSE);
	pThis->bConnect.EnableWindow(TRUE);
	pThis->bRead.EnableWindow(TRUE);
	pThis->bWrite.EnableWindow(TRUE);
	pThis->bReset.EnableWindow(TRUE);
	pThis->bSet.EnableWindow(TRUE);
	
}

int SendAPDUandDisplay(int sendType, CString apduIn)
{
    char buffer[300], tmpStr[300];
	int  indx;

	ioRequest.dwProtocol = dwActProtocol;
	ioRequest.cbPciLength = sizeof(SCARD_IO_REQUEST);
	DisplayOut(2, 0, apduIn);
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

	switch(sendType){
	case 0:						// Display SW1/SW2 value
		if (!((RecvBuff[RecvLen-2] == 0x90) && (RecvBuff[RecvLen-1] == 0x00)))
			DisplayOut(1, 0, "Return bytes are not acceptable.");
		else{
			strcpy(tmpStr, "");
			for (indx=RecvLen-2; indx<(RecvLen); indx++){
				sprintf(buffer, "%02X ", RecvBuff[indx] & 0x00FF);
				strcat(tmpStr, buffer);
			}
		}
		break;
	case 1:						// Display ATR after checking SW1/SW2
	    if (!((RecvBuff[RecvLen-2] == 0x90) && (RecvBuff[RecvLen-1] == 0x00)))
			DisplayOut(1, 0, "Return bytes are not acceptable.");
		else{
			strcpy(tmpStr, "");
			for (indx=0; indx<(RecvLen-2); indx++){
				sprintf(buffer, "%02X ", RecvBuff[indx] & 0x00FF);
				strcat(tmpStr, buffer);
			}
			sprintf(tmpStr, "ATR : %s", tmpStr);
		}
		break;
	case 2:						// Display all data after checking SW1/SW2
	    if (!((RecvBuff[RecvLen-2] == 0x90) && (RecvBuff[RecvLen-1] == 0x00)))
			DisplayOut(1, 0, "Return bytes are not acceptable.");
		else{
			strcpy(tmpStr, "");
			for (indx=0; indx<(RecvLen-2); indx++){
				sprintf(buffer, "%02X ", RecvBuff[indx] & 0x00FF);
				strcat(tmpStr, buffer);
			}
		}
		break;
	}
	DisplayOut(3, 0, tmpStr);
	return retCode;

}

BOOL InputOK(int checkType, int opType)
{
	char tmpStr[257];
	int tmpLen;

    if (checkType == 1){         // For 17-bit address input
		pThis->tBitAdd.GetWindowText(tmpStr, 2);
		if  (tmpStr == ""){
			pThis->tBitAdd.SetFocus();
			return 0;
		}
	}
    
	tmpLen = pThis->tHiAdd.GetWindowText(tmpStr, 3);
	if  ((tmpStr == "")|(tmpLen != 2)){
		pThis->tHiAdd.SetFocus();
		return 0;
	}
	tmpLen = pThis->tLoAdd.GetWindowText(tmpStr, 3);
	if  ((tmpStr == "")|(tmpLen != 2)){
		pThis->tLoAdd.SetFocus();
		return 0;
	}
	tmpLen = pThis->tLen.GetWindowText(tmpStr, 3);
	if  ((tmpStr == "")|(tmpLen != 2)){
		pThis->tLen.SetFocus();
		return 0;
	}
	if (opType == 1){            // For Write Operation
		pThis->tData.GetWindowText(tmpStr, 257);
		if  (tmpStr == ""){
			pThis->tData.SetFocus();
			return 0;
		}
    }

	return 1;

}


/////////////////////////////////////////////////////////////////////////////
// CIICDlg message handlers

BOOL CIICDlg::OnInitDialog()
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
	InitMenu();
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CIICDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CIICDlg::OnPaint() 
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
HCURSOR CIICDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CIICDlg::OnQuit() 
{

    int tmpResult = 0;

    // Close SC reader
	if (connActive)
		retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
    this->EndDialog(tmpResult);
	
}

void CIICDlg::OnReset() 
{

	ClearFields();

    // Close SC reader
	if (connActive)
		retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    retCode = SCardReleaseContext(hContext);
    connActive = FALSE;
	InitMenu();
	
}

void CIICDlg::OnInit() 
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
        DisplayOut(1, retCode, "");
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
    DisplayOut(0, retCode, "Select Reader and Card to connect.");
    cbReader.EnableWindow(TRUE);
	cbCardType.EnableWindow(TRUE);
	cbCardType.AddString("Auto Detect");
	cbCardType.AddString("1 Kbit");
	cbCardType.AddString("2 Kbit");
	cbCardType.AddString("4 Kbit");
	cbCardType.AddString("8 Kbit");
	cbCardType.AddString("16 Kbit");
	cbCardType.AddString("32 Kbit");
	cbCardType.AddString("64 Kbit");
	cbCardType.AddString("128 Kbit");
	cbCardType.AddString("256 Kbit");
	cbCardType.AddString("512 Kbit");
	cbCardType.AddString("1024 Kbit");
	cbCardType.SetCurSel(0);
	bConnect.EnableWindow(TRUE);
	bInit.EnableWindow(FALSE);
	bReset.EnableWindow(TRUE);
	
}

void CIICDlg::OnConnect() 
{
	int tmpInt = cbReader.GetCurSel();
	CString tmpStr, cardType;
	char buffer[200];


	if (connActive){
		DisplayOut(0, 0, "Connection is already active.");
		return;
	}
	cbReader.GetLBText(tmpInt, tmpStr);

  	retCode = SCardConnect(hContext,
						tmpStr,
						SCARD_SHARE_SHARED,
						SCARD_PROTOCOL_T0|SCARD_PROTOCOL_T1,
						&hCard,
						&dwActProtocol);
	if (retCode != SCARD_S_SUCCESS)
	{
        DisplayOut(1, retCode, "");
		connActive = FALSE;
        return;
	}

	connActive = TRUE;
    cbPageSize.SetCurSel(0);
	AddButtons();
	EnableFields();
    if (cbCardType.GetCurSel() == 11) 
		tBitAdd.EnableWindow(TRUE);
    else
		tBitAdd.EnableWindow(FALSE);
	sprintf(buffer, "Connected to %s.", tmpStr);
	DisplayOut(0, 0, buffer);


}

void CIICDlg::OnEditchangecbReader() 
{

	cbCardType.SetCurSel(0);
	ClearFields();
	DisableFields();
	bRead.EnableWindow(FALSE);
	bWrite.EnableWindow(FALSE);
	bSet.EnableWindow(FALSE);
	
	if (connActive)
		retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	connActive = FALSE;
	
}

void CIICDlg::OnEditchangeCombo2() 
{
	ClearFields();
	DisableFields();
	bRead.EnableWindow(FALSE);
	bWrite.EnableWindow(FALSE);
	bSet.EnableWindow(FALSE);
	
	if (connActive)
		retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	connActive = FALSE;

}

void CIICDlg::OnSetPageSize() 
{
	char buffer[300], tmpStr[300];

	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x01;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x01;
	switch(cbPageSize.GetCurSel()){
		case 0: SendBuff[5] = 0x03;
			break;
		case 1: SendBuff[5] = 0x04;
			break;
		case 2: SendBuff[5] = 0x05;
			break;
		case 3: SendBuff[5] = 0x06;
			break;
		case 4: SendBuff[5] = 0x07;
			break;
	}
	SendLen = 6;
	RecvLen = 2;
    strcpy(tmpStr, "");
	for(int i=0; i<6; i++){
		sprintf(buffer, "%02X ", SendBuff[i] & 0x00FF);
		strcat(tmpStr, buffer);
	}
	retCode = SendAPDUandDisplay(0, tmpStr);
	if (retCode != SCARD_S_SUCCESS)
        return;
	
}

void CIICDlg::OnRead() 
{
	int checkType, tmpInt, indx;
	char buffer[256], tmpStr[256];

	// 1. Check for all input fields
    if (cbCardType.GetCurSel() == 11) 
		checkType = 1;
    else
		checkType = 0;
	if (!InputOK(checkType,0))
		return;
   
	// 2. Read input fields and pass data to card
	tData.Clear();
	ClearBuffers();
	tmpInt = 0;
	SendBuff[0] = 0xFF;
	if (cbCardType.GetCurSel() == 11){
		tBitAdd.GetWindowText(buffer, 2);
		sscanf(buffer, "%01X", &tmpInt);
	}
	if ((cbCardType.GetCurSel() == 11) & (tmpInt == 1))
		SendBuff[1] = 0xB1;
	else
		SendBuff[1] = 0xB0;

	tmpInt = 0;
	tHiAdd.GetWindowText(buffer, 3);
	sscanf(buffer, "%02X", &tmpInt);
	SendBuff[2] = tmpInt;

	tmpInt = 0;
	tLoAdd.GetWindowText(buffer, 3);
	sscanf(buffer, "%02X", &tmpInt);
	SendBuff[3] = tmpInt;

	tmpInt = 0;
	tLen.GetWindowText(buffer, 3);
	sscanf(buffer, "%02X", &tmpInt);
	SendBuff[4] = tmpInt;
	
	SendLen = 5;
	RecvLen = SendBuff[4]+2;
    strcpy(tmpStr, "");
	for(indx=0; indx<5; indx++){
		sprintf(buffer, "%02X ", SendBuff[indx] & 0x00FF);
		strcat(tmpStr, buffer);
	}
	retCode = SendAPDUandDisplay(2, tmpStr);
	if (retCode != SCARD_S_SUCCESS)
        return;

	// 3. Display data read from card into Data textbox
    strcpy(tmpStr, "");
	for(indx=0; indx<RecvLen-2; indx++){
 		sprintf(buffer, "%c", RecvBuff[indx] & 0xFF);
		strcat(tmpStr, buffer);
	}
	tData.SetWindowText(tmpStr);
}

void CIICDlg::OnWrite() 
{
	int checkType, tmpInt, indx;
	char buffer[256], tmpStr[256];

	// 1. Check for all input fields
    if (cbCardType.GetCurSel() == 11) 
		checkType = 1;
    else
		checkType = 0;
	if (!InputOK(checkType,1))
		return;
   
	// 2. Read input fields and pass data to card
	ClearBuffers();
	tmpInt = 0;
	SendBuff[0] = 0xFF;
	if (cbCardType.GetCurSel() == 11){
		tBitAdd.GetWindowText(buffer, 2);
		sscanf(buffer, "%01X", &tmpInt);
	}
	if ((cbCardType.GetCurSel() == 11) & (tmpInt == 1))
		SendBuff[1] = 0xD1;
	else
		SendBuff[1] = 0xD0;

	tmpInt = 0;
	tHiAdd.GetWindowText(buffer, 3);
	sscanf(buffer, "%02X", &tmpInt);
	SendBuff[2] = tmpInt;

	tmpInt = 0;
	tLoAdd.GetWindowText(buffer, 3);
	sscanf(buffer, "%02X", &tmpInt);
	SendBuff[3] = tmpInt;

	tmpInt = 0;
	tLen.GetWindowText(buffer, 3);
	sscanf(buffer, "%02X", &tmpInt);
	SendBuff[4] = tmpInt;
	
	tData.GetWindowText(buffer, SendBuff[4]+1);
	for (indx =0;indx<SendBuff[4];indx++)
		SendBuff[indx + 5] = buffer[indx];
	SendLen = 5 + SendBuff[4];
	RecvLen = 2;
    strcpy(tmpStr, "");
	for(indx=0; indx<SendLen; indx++){
		sprintf(buffer, "%02X ", SendBuff[indx] & 0x00FF);
		strcat(tmpStr, buffer);
	}
	retCode = SendAPDUandDisplay(0, tmpStr);
	if (retCode != SCARD_S_SUCCESS)
        return;

	tData.Clear();
	
}

void CIICDlg::OnSelchangeCombo1() 
{

	cbCardType.SetCurSel(0);
	ClearFields();
	DisableFields();
	bRead.EnableWindow(FALSE);
	bWrite.EnableWindow(FALSE);
	bSet.EnableWindow(FALSE);
	
	if (connActive)
		retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	connActive = FALSE;
	
}

void CIICDlg::OnSelchangeCombo2() 
{
	ClearFields();
	DisableFields();
	bRead.EnableWindow(FALSE);
	bWrite.EnableWindow(FALSE);
	bSet.EnableWindow(FALSE);
	
	if (connActive)
		retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	connActive = FALSE;

}
