///////////////////////////////////////////////////////////////////////////////
//
// FORM NAME : SAM Sample Usage
//
// COMPANY : ADVANDCED CARD SYSTEMS, LTD
//
// AUTHOR : MALCOLM BERNARD U. SOLAÑA
//
// DATE :  01 / 30 / 2007
//
//
// Description : This program test the SAM commands created on KeyManagement program
//				 All general functions and variables are all declared on
//					on SAMSampleUsageDlg.cpp and SAMSampleUsageDlg.h
//					for public usage.
//
//'   Initial Step :  1.  Press List Readers.
//'                   2.  Choose the SAM reader and ACOS card reader.
//'                   3.  Press Connect.
//'                   4.  Select Algorithm Reference to use (DES/3DES)
//'                   5.  Enter SAM Global PIN. (PIN used in KeyManagement sample, SAM Initialization)
//'                   6.  Press Mutual Authentication.
//'                   7.  Enter ACOS Card PIN. (PIN used in KeyManagement sample, ACOS Card Initialization)
//'                   8.  Press Submit PIN.
//'                   9.  If you don't want to change your current PIN go to step 10.
//'                       To changed current PIN, enter the desired new PIN and press Change PIN
//'                   10. To check current card balance (e-purse) press Inquire Account.
//'                   11. To credit amount to the card e-purse enter the amount to credit and press Credit.
//'                   12. To dedit amount to the card e-purse enter the amount to dedit and press Dedit.
//'
//'   NOTE:
//'                   Please note that this sample program assumes that the SAM and ACOS card were already
//'                   initialized using KeyManagement Sample program.
//
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////
// SAMSampleUsageDlg.cpp : implementation file
//

#include "stdafx.h"
#include "SAMSampleUsage.h"
#include "SAMSampleUsageDlg.h"
#include "MyTabCtrl.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


// GLOBAL VARIABLES
LONG MAX_BUFFER_LEN  = 256;
LONG INVALID_SW1SW2  = -450;
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

/////////////////////////////////////////////////////////////////////////////
// CSAMSampleUsageDlg dialog

CSAMSampleUsageDlg::CSAMSampleUsageDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSAMSampleUsageDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSAMSampleUsageDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDI_ICON1);
	G_hContext = 0;
	G_Protocol = 1;
	G_ConnActive = false;
	G_ConnActiveMCU = false;
}

void CSAMSampleUsageDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSAMSampleUsageDlg)
	DDX_Control(pDX, IDC_TAB1, m_tabCtrl);
	DDX_Control(pDX, IDC_LIST1, m_listbox);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CSAMSampleUsageDlg, CDialog)
	//{{AFX_MSG_MAP(CSAMSampleUsageDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSAMSampleUsageDlg message handlers

BOOL CSAMSampleUsageDlg::OnInitDialog()
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
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	TCITEM tcItem1;
	TCITEM tcItem2;

	tcItem1.mask = TCIF_TEXT;
	tcItem1.pszText = _T("         SAM Initialization          ");
	tcItem2.mask = TCIF_TEXT;
	tcItem2.pszText = _T("        ACOS Card Initialization      ");
	   
	m_tabCtrl.InitDialogs();
	m_tabCtrl.InsertItem(0, &tcItem1);
	m_tabCtrl.InsertItem(1, &tcItem2);
	m_tabCtrl.SetCurFocus(0); //Sets focus to first tab.
		
	m_tabCtrl.ActivateTabDialogs();

	this->SetWindowText("SAM Sample Usage");
	this->m_listbox.SetHorizontalExtent(WS_HSCROLL);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CSAMSampleUsageDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CSAMSampleUsageDlg::OnPaint() 
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
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CSAMSampleUsageDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CSAMSampleUsageDlg::ClearBuffers()
{	
	//Clears the send and receive buffer for the PCSC Commands

	int i=0;
    for (i = 0;i<255;i++)
    {
    
		this->G_SendBuff[i] = 0x00;
		this->G_RecvBuff[i] = 0x00;
	
	}

}


int CSAMSampleUsageDlg::DisplayOut(int errType, int retVal, CString PrintText, CString AppText)
{	
	//Displays the APDU sent and recieved by the SAM and MCU card..
	//returns 1 if erronous and 0 if successful

	int i;
	
	switch (errType) {
		
		case 0	:	i = m_listbox.AddString(">" + PrintText);
					m_listbox.SetCurSel (i);
					break;
					//Information
		
		case 1	:	i = m_listbox.AddString(AppText + " Error>" + PrintText + " : " + GetScardErrMsg(retVal));
					m_listbox.SetCurSel (i);
					return 1;
					//Error
		
		case 2	:	i = m_listbox.AddString(AppText + "<" + PrintText);
					m_listbox.SetCurSel (i);
					break;
					//Into Card command
		
		case 3	:	i = m_listbox.AddString(AppText + ">" + PrintText);
					m_listbox.SetCurSel (i);
					//Out from Card command
					break ;
		
		default : ;
	}
	
	m_listbox.SetCurSel (i);
	return 0;

}


int CSAMSampleUsageDlg::AppendSamFile(BYTE KeyId, BYTE* DataArr, int MaxDataLen)
{	
	//Appends the SAM file when creating a DF
	//returns 1 if erronous and 0 if successful

	int i, ctr, j;
	char buff[100];	
	
	ClearBuffers();
	G_SendBuff[0] = 0x00;
	G_SendBuff[1] = 0xE2;
	G_SendBuff[2] = 0x00;
	G_SendBuff[3] = 0x00;
	G_SendBuff[4] = 0x16;
	G_SendBuff[5] = KeyId;
	G_SendBuff[6] = 0x3;
	G_SendBuff[7] = 0xFF;
	G_SendBuff[8] = 0xFF;
	G_SendBuff[9] = 0x88;
	G_SendBuff[10] = 0x00;

	i = 11;
	for (ctr = 0; ctr <= MaxDataLen - 1; ctr++)
	{
        
		G_SendBuff[i] = DataArr[ctr];
        i = i + 1;
	
	}
	
	j = 0;
	for (ctr = 0; ctr <= (MaxDataLen + 11) -1; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}
	
	DisplayOut(2, 0, buff, "SAM");

	G_SendLen = MaxDataLen + 11;
	G_RecvLen = 2;

	if (SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) != 0)
	{
		return 1;
	}

	return 0;
}

int CSAMSampleUsageDlg::CreateSAMFile(BYTE FileLen, BYTE* DataArr, int MaxDataLen)
{	
	//Creates/Defines a SAM file
	//returns 1 if erronous and 0 if successful
	
	int i, ctr;
	char buff[100];	

	ClearBuffers();
	G_SendBuff[0] = 0x00;
	G_SendBuff[1] = 0xE0;
	G_SendBuff[2] = 0x00;
	G_SendBuff[3] = 0x00;
	G_SendBuff[4] = FileLen;

	i = 5;
	for (ctr = 0; ctr <= MaxDataLen - 1; ctr++)
	{
        G_SendBuff[i] = DataArr[ctr];
        i = i + 1;
	}


	int j = 0;
	for (ctr = 0; ctr <= (MaxDataLen + 5) -1; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0, buff, "SAM");
	
	G_SendLen = MaxDataLen + 5;
	G_RecvLen = 2;
	if (SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) != 0)
	{
		return 1;
	}

	return 0;
}

int CSAMSampleUsageDlg::writeRecord(int caseType, BYTE RecNo, BYTE maxDataLen, BYTE DataLen, BYTE* DataIn)
{	//Writes the data needed to the ACOS Card..
	//Note : Please select the file currently needed first
	//      before writing to card.
	
	int i, ctr, j;
	char buff[100];

	switch (caseType)	{
	case	1	: // If card data is to be erased before writing new data	
					// 1. Re-initialize card values to $00
				ClearBuffers();
				G_SendBuff[0] =  0x80;          // CLA
				G_SendBuff[1] =  0xD2;          // INS
				G_SendBuff[2] =  RecNo;        // P1    Record to be written
				G_SendBuff[3] =  0x00;          // P2
				G_SendBuff[4] =  maxDataLen;   // P3    Length
				
				i = 5;
				for (ctr = 0; ctr <= maxDataLen - 1; ctr++)
				{
					G_SendBuff[i] = 0x00;
					i = i + 1;
				}
				
				j = 0;
				for (ctr = 0; ctr <= (maxDataLen + 5) -1; ctr++)
				{
					sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
					j+=3;
				}

				DisplayOut(2, 0, buff, "SAM");
				
				G_SendLen = maxDataLen + 5;
				G_RecvLen = 0x02;
				
				G_RetCode = SendAPDUandDisplay(0, buff);
	
				if (G_RetCode != SCARD_S_SUCCESS)
				{	
					DisplayOut(1, G_RetCode, "", "MCU");
					return G_RetCode;
				}
				
				if ((G_RecvBuff[G_RecvLen -2] != 0x90) | (G_RecvBuff[G_RecvLen -1] != 0x00))
				{
					
					DisplayOut(1, G_RetCode, buff, "MCU");
					G_RetCode = INVALID_SW1SW2;

					return G_RetCode;

				}	
				break;

	default		: // 2. Write data to card
				ClearBuffers();
				G_SendBuff[0] =  0x80;          // CLA
				G_SendBuff[1] =  0xD2;          // INS
				G_SendBuff[2] =  RecNo;        // P1    Record to be written
				G_SendBuff[3] =  0x00;          // P2
				G_SendBuff[4] =  DataLen;   // P3    Length
				
				i = 5;
				for (ctr = 0; ctr <= maxDataLen - 1; ctr++)
				{
					G_SendBuff[i] = DataIn[ctr];
					i = i + 1;
				}
				
				j = 0;
				for (ctr = 0; ctr <= (maxDataLen + 5) -1; ctr++)
				{
					sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
					j+=3;
				}

				DisplayOut(2, 0, buff, "MCU");
				
				G_SendLen = maxDataLen + 5;
				G_RecvLen = 2;
				
				G_RetCode = SendAPDUandDisplay(0, buff);

				if (G_RetCode != SCARD_S_SUCCESS)
				{
					return G_RetCode;
				}
				
				sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
					

				if ((G_RecvBuff[G_RecvLen -2] != 0x90) | (G_RecvBuff[G_RecvLen -1] != 0x00))
				{
					
					DisplayOut(1, G_RetCode, buff, "MCU");
					G_RetCode = INVALID_SW1SW2;

					return G_RetCode;

				}
			
				break;
		;
		}

	return 0;
}

int CSAMSampleUsageDlg::SendAPDUandDisplay(int SendType, CString ApduIN) 
{	
	//Sends the APDU command to the SAM Card...
	//returns 1 if erronous and 0 if successful
	
	char buff[100];
	int i, j, ctr;

	IO_REQ.dwProtocol = G_Protocol;
	IO_REQ.cbPciLength = sizeof(SCARD_IO_REQUEST);
	
	G_RecvLen = 262;
	G_RetCode = SCardTransmit(G_hCard,
                           &IO_REQ,
                           G_SendBuff,
                           G_SendLen,
                           &IO_REQ,
                           G_RecvBuff,
                           &G_RecvLen);

	//Retrieving the return code is it is a success or not
	switch (SendType) {
		case 0 : sprintf(buff, "%02X %02X", G_RecvBuff[0],G_RecvBuff[1]);
				 break;
					
		case 1	:	// Read ATR after checking SW1/SW2
					if ((G_RecvBuff[G_RecvLen -2] != 0x90) | (G_RecvBuff[G_RecvLen -1] != 0x00))
					{
						DisplayOut(1, G_RetCode, buff, "MCU");

						return 1;
					}
					
					sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
					

					i = (int)G_RecvLen - 3;
					j = 0;
					for (ctr = 0; ctr <= i; ctr++)
					{
						sprintf(&buff[j], "%02X ", G_RecvBuff[ctr]);
						j+=3;
					}
					break;

		case 2 :	// Read SW1/SW2
					sprintf(buff, "%02X %02X", G_RecvBuff[0],G_RecvBuff[1]);
					if ((G_RecvBuff[G_RecvLen -2] != 0x90) | (G_RecvBuff[G_RecvLen -1] != 0x00))
					{
						DisplayOut(1, G_RetCode, buff, "MCU");

						return 1;
					}
					
					sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
					

					i = (int)G_RecvLen - 3;
					j = 0;
					for (ctr = 0; ctr <= i; ctr++)
					{
						sprintf(&buff[j], "%02X ", G_RecvBuff[ctr]);
						j+=3;
					}
					break;
		default	:	;
	}
	
	if (G_RetCode != SCARD_S_SUCCESS)
	{	
		
		return 1;
	}

	DisplayOut(3, 0, buff, "MCU");

	return 0;
}

int CSAMSampleUsageDlg::Debit_ACOS2(CString Response)
{	CString tmpstr, tmpstr2;
	int ArrCnt, ctr, retCode;
	char buff[100];

	ClearBuffers();
	G_SendBuff[0] = 0x80;
    G_SendBuff[1] = 0xE6;
    G_SendBuff[2] = 0x00;
    G_SendBuff[3] = 0x00;
    G_SendBuff[4] = 0x0B;
	
	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) +5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &G_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}
	
	int i = ctr-1;
	int j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0, buff, "MCU");
	
	G_SendLen = 16;
	G_RecvLen = 2;
	retCode = this->SendAPDUandDisplay(0, buff);
	if (retCode != 0) 
	{
		return 1;
	}
	
	sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);

	if ((G_RecvBuff[G_RecvLen -2] != 0x90) | (G_RecvBuff[G_RecvLen -1] != 0x00))
	{	
		DisplayOut(1, retCode, "Invalid Return String", "MCU");
		return retCode;
	}

	return 0;
}

int CSAMSampleUsageDlg::CreditDebit(BYTE* DataIn, BYTE MaxDataLen, BYTE Buff1, BYTE Buff2, CString Response)
{	int i, ctr, retCode;
	char buff[100];
	ClearBuffers();
	G_SendBuff[0] = 0x80;
    G_SendBuff[1] = Buff1;
    G_SendBuff[2] = Buff2;
    G_SendBuff[3] = 0x00;
    G_SendBuff[4] = 0x0B;

	i = 5;
	for (ctr = 0; ctr <= MaxDataLen - 1; ctr++)
	{
        G_SendBuff[i] = DataIn[ctr];
        i = i + 1;
	}
	
	int j = 0;
	for (ctr = 0; ctr <= (MaxDataLen + 5) -1; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0, buff, "MCU");
	
	G_SendLen = MaxDataLen + 5;
	G_RecvLen = 2;
	retCode = this->SendAPDUandDisplay(0, buff);
	if (retCode != 0) 
	{
		return 1;
	}
	
	sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
	
	//Credit
	if (Buff1 == 0xE2)
	{
		if ((G_RecvBuff[G_RecvLen -2] != 0x90) | (G_RecvBuff[G_RecvLen -1] != 0x00))
		{	
			DisplayOut(1, retCode, "Invalid Return String", "MCU");
			return 1;
		}

	}

	//Debit
	if (Buff1 == 0xE6)
	{	
		if ((G_RecvBuff[G_RecvLen -2] != 0x61) | (G_RecvBuff[G_RecvLen -1] != 0x04))
		{
			if ((G_RecvBuff[G_RecvLen -2] == 0x6A) | (G_RecvBuff[G_RecvLen -1] == 0x86))
			{
				DisplayOut(0, 0, "Debit Certificate Not Supported By ACOS2 card or lower", "MCU");
				DisplayOut(0, 0, "Change P1 = 0 to perform Debit without returning debit certificate", "MCU");
				i = this->Debit_ACOS2(Response);
				return 1;
			}
			else
			{
				DisplayOut(1, retCode, "Invalid Return String", "MCU");
				return 1;
			}

			return 1;
		}

	}

	return 0;
}

CString CSAMSampleUsageDlg::GetSAMResponse(BYTE RecvLen, BYTE Buff4)
{	//Acquires the SAM Key generated..
	//Function returns the hex value of generated key..

	char buff[100];
	int j, ctr, i;

	ClearBuffers();
	G_SendBuff[0] = 0x00;
    G_SendBuff[1] = 0xC0;
    G_SendBuff[2] = 0x00;
    G_SendBuff[3] = 0x00;
    G_SendBuff[4] = Buff4;
	
	j = 0;
	for (ctr = 0; ctr <= 4; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0, buff, "SAM");
	G_SendLen = 5;
	G_RecvLen = RecvLen;
	
	SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff);

	sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
	DisplayOut(3, 0, buff, "SAM");

	if ((G_RecvBuff[G_RecvLen -2] == 0x90) | (G_RecvBuff[G_RecvLen -1] == 0x00))
	{
		i = (int)G_RecvLen - 3;
		j = 0;
		for (ctr = 0; ctr <= i; ctr++)
		{
			sprintf(&buff[j], "%02X ", G_RecvBuff[ctr]);
			j+=2;
		}
	}
	else
	{
		sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
		DisplayOut(1, G_RetCode, buff, "SAM");

		for(ctr = 0; ctr <= 100; ctr++)
		{
			sprintf(&buff[j], "%c ", "");
		}
	}

	return buff;
}

CString CSAMSampleUsageDlg::GetMCUResponse(BYTE Slen)
{	//Acquires the SAM Key generated..
	//Function returns the hex value of generated key..

	char buff[100];
	int j, ctr, i;

	ClearBuffers();
	G_SendBuff[0] = 0x80;
    G_SendBuff[1] = 0xC0;
    G_SendBuff[2] = 0x00;
    G_SendBuff[3] = 0x00;
    G_SendBuff[4] = Slen;
	
	j = 0;
	for (ctr = 0; ctr <= 4; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0, buff, "MCU");
	G_SendLen = 5;
	G_RecvLen = 27;
	
	G_RetCode = SendAPDUandDisplay(2, G_SendBuff);
	
	sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
	DisplayOut(3, 0, buff, "MCU");

	if ((G_RecvBuff[G_RecvLen -2] == 0x90) | (G_RecvBuff[G_RecvLen -1] == 0x00))
	{
		i = (int)G_RecvLen - 3;
		j = 0;
		for (ctr = 0; ctr <= i; ctr++)
		{
			sprintf(&buff[j], "%02X ", G_RecvBuff[ctr]);
			j+=2;
		}
	}
	else
	{
		return "";
		
	}

	return buff;
}

int CSAMSampleUsageDlg::GenerateSAMKey(BYTE KeyId, BYTE* DataArr, int MaxDataLen) 
{	//Generates the SAM key base from the User Input..


	char buff[100];
	int i, ctr, j;
	ClearBuffers();
	G_SendBuff[0] = 0x80;
    G_SendBuff[1] = 0x88;
    G_SendBuff[2] = 0x00;
    G_SendBuff[3] = KeyId; //KeyID
    G_SendBuff[4] = 0x08;
	
	i = 5;
	for (ctr = 0; ctr <= MaxDataLen - 1; ctr++)
	{
        G_SendBuff[i] = DataArr[ctr];
        i = i + 1;
	}


	j = 0;
	for (ctr = 0; ctr <= (MaxDataLen + 5) -1; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0, buff, "SAM");
	
	G_SendLen = MaxDataLen + 5;
	G_RecvLen = 2;
	if (SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) != 0)
	{	
		sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
					
		DisplayOut(1, G_RetCode, buff, "SAM");
		return 1;
	}
	else
	{	

		sprintf(buff, "%02X %02X", G_RecvBuff[G_RecvLen -2],G_RecvBuff[G_RecvLen -1]);
			
		if ((G_RecvBuff[G_RecvLen -2] != 0x61) | (G_RecvBuff[G_RecvLen -1] != 0x08))
		{	
			DisplayOut(1, G_RetCode, buff, "SAM");

			return 1;
		}
		
		
		return 0;
	}
}

int CSAMSampleUsageDlg::SendAPDUSAM(BYTE* SendBuff, int SendLen, int RecvLen, BYTE* RecvBuff)
{	
	//Sends the APDU command to the SAM Card...
	//returns 1 if erronous and 0 if successful
	
	char buff[100];
	int i, j, ctr;
	G_RecvLen = RecvLen;

	IO_REQ.dwProtocol = G_Protocol;
	IO_REQ.cbPciLength = sizeof(SCARD_IO_REQUEST);

	G_RetCode = SCardTransmit(G_hSAMCard,
							&IO_REQ,
							SendBuff, 
							SendLen,
							&IO_REQ,
							RecvBuff,
							&G_RecvLen);
	
	//Retrieving the return code is it is a success or not
	if (RecvLen == 2) 
	{
		sprintf(buff, "%02X %02X", G_RecvBuff[0],G_RecvBuff[1]);
	}
	else
	{
		i = (int)G_RecvLen - 3;
		j = 0;
		for (ctr = 0; ctr <= i; ctr++)
		{
			sprintf(&buff[j], "%02X ", G_RecvBuff[ctr]);
			j+=3;
		}
	}

	if (G_RetCode != SCARD_S_SUCCESS)
	{	
		DisplayOut(1, G_RetCode, buff, "SAM");
		
		return 1;
	}

							
	DisplayOut(3, 0, buff, "SAM");

	return 0;
}

void CSAMSampleUsageDlg::ResetSAM()
{
	//Resets the SAM Card..

	if (G_ConnActive)
	{
		SCardDisconnect(
		G_hSAMCard,
		SCARD_UNPOWER_CARD);

		this->G_ConnActive = false;
	}

	G_RetCode = SCardReleaseContext(G_hSAMCard);
}


void CSAMSampleUsageDlg::ResetMCU()
{
	//Resets the MCU Card..

	if (G_ConnActiveMCU)
	{
		SCardDisconnect(
		G_hCard,
		SCARD_UNPOWER_CARD);

		this->G_ConnActiveMCU = false;
	}

	G_RetCode = SCardReleaseContext(G_hCard);
}


int CSAMSampleUsageDlg::readRecord(BYTE RecNo, BYTE DataLen)
{	
	//Reads the record on a Specified file on the MCU card.
	//Return 1 if erroneous and 0 if successful

	int i, j, ctr;
	char buff[100];

	ClearBuffers();
	G_SendBuff[0] = 0x80;        // CLA
	G_SendBuff[1] = 0xB2;        // INS
	G_SendBuff[2] = RecNo;      // P1    Record No
	G_SendBuff[3] = 0x00;        // P2
	G_SendBuff[4] = DataLen;    // P3    Length of data to be read
	
	i = 4;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}
	
	DisplayOut(2, 0,buff, "MCU");
	G_SendLen = 0x05;
	G_RecvLen = G_SendBuff[4] + 2;

	G_RetCode = SendAPDUandDisplay(2, buff);
	
	if (G_RetCode != SCARD_S_SUCCESS)
	{
		return 1;
	}

	return 0;
}

int CSAMSampleUsageDlg::selectfile(BYTE HiAddr, BYTE LoAddr) 
{	
	//Selects the file on the MCU that needs to be accessed.
	//Return 1 if erroneous and 0 if successful
	
	int i, j, ctr;
	char buff[100];

	ClearBuffers();
	G_SendBuff[0] = 0x80;     // CLA
	G_SendBuff[1] = 0xA4;     // INS
	G_SendBuff[2] = 0x00;     // P1
	G_SendBuff[3] = 0x00;     // P2
	G_SendBuff[4] = 0x02;     // P3
	G_SendBuff[5] = HiAddr;     // Value of High Byte
	G_SendBuff[6] = LoAddr;     // Value of Low Byte
	
	i = 6;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}
	
	DisplayOut(2, 0,buff, "MCU");

	G_SendLen = 0x07;
	G_RecvLen = 0x02;

	G_RetCode = SendAPDUandDisplay(0, buff);
	
	if (G_RetCode != SCARD_S_SUCCESS)
	{
		return 1;
	}

	return 0;
}

void CSAMSampleUsageDlg::SubmitIC() 
{
	//Submits the default IC on the MCU card..

	char buff[100];
	int i, j,  ctr;
	DWORD RecvLength = 2;	
	DWORD Protocol = 1;

	ClearBuffers();

	G_SendBuff[0] = 0x80; //CLA
	G_SendBuff[1] = 0x20; //INS
	G_SendBuff[2] = 0x07; //P1
	G_SendBuff[3] = 0x00; //P2
	G_SendBuff[4] = 0x08; //P3

	G_SendBuff[5] = 0x41; //A
	G_SendBuff[6] = 0x43; //C
	G_SendBuff[7] = 0x4F; //O
	G_SendBuff[8] = 0x53; //S
	G_SendBuff[9] = 0x54; //T
	G_SendBuff[10] = 0x45; //E
	G_SendBuff[11] = 0x53; //S
	G_SendBuff[12] = 0x54; //T
	G_SendLen = 0x0D;
	G_RecvLen = 0x02;
	
	i = 12;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", G_SendBuff[ctr]);
		j+=3;
	}

	DisplayOut(2, 0,buff, "MCU");


	G_RetCode = SendAPDUandDisplay(0, G_SendBuff);

	if (G_RetCode != SCARD_S_SUCCESS)
	{	
		DisplayOut(1, G_RetCode, "Error in Submitting IC!", "MCU");	
		 //Error in Submitting IC
	}

	if (RecvLength != 2)
	{
		DisplayOut(1, G_RetCode, "Unable to Transmit Command to Card", "MCU");
		 //Unable to Transmit Command to Card
	}

	//Retrieving the return code is it is a success or not

	if ((G_RecvBuff[0] != 0x90) | (G_RecvBuff[1] != 0x00))
	{
		DisplayOut(1, G_RetCode, "Error in Submitting IC!", "MCU");
		
		sprintf(buff, "%02X %02X", G_RecvBuff[0],G_RecvBuff[1]);
		DisplayOut(1, G_RetCode, buff, "MCU");
		 //Error in Submitting IC!
	}

	DisplayOut(0, 0, "Success in Submitting IC!", "MCU");

}