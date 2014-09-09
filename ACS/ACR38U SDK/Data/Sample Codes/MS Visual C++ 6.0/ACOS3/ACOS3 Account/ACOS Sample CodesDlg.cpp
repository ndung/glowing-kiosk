//  Copyright(C):      Advanced Card Systems Ltd
//
//
//  Description:       This sample program outlines the steps on how to
//                     use the Account File functionalities of ACOS
//                     using the PC/SC platform.
//
//  Author:            Fernando G. Robles
//
//  Date:              Aug. 16, 2005
//
//  Revision Trail:   (Date/Author/Description)
//
//   11/10/2005      Fernando G. Robles      Added Debit Certification
//
//======================================================================

#include "stdafx.h"
#include "winscard.h"
#include "ACOS Sample Codes.h"
#include "ACOS Sample CodesDlg.h"
#include "des.cp"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif



// GLOBAL VARIABLES

unsigned char *CipherKey; // this variable is needed by DES.CP for DES encryption
unsigned char SessionKey [16]; // this variable holds the Session Key as computed by Mut. Auth'n
unsigned char cKey[16];
unsigned char tKey[16];

SCARDCONTEXT hContext = 0;
SCARDHANDLE hCard;
LONG RetCode;
SCARD_IO_REQUEST IO_REQ;
char ReaderName [128];
unsigned char SendBuff [256];
unsigned char RecvBuff [256];

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
// CACOSSampleCodesDlg dialog

CACOSSampleCodesDlg::CACOSSampleCodesDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CACOSSampleCodesDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CACOSSampleCodesDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDI_ICON2);

	SCardDisconnect(
		hCard,
		SCARD_UNPOWER_CARD);

	SCardReleaseContext (hContext);
}


void LoadReaderNames (CACOSSampleCodesDlg *);
void ClearBuffers();

// this function encrypts 8-byte Data with 8-byte Key
// Data holds the plain text
// Key holds the Key
// Result will be stoed in Data
// if bEncrypt == TRUE, function will perform single DES encryption, else single DES decryption
void DES (BYTE *Data, BYTE *Key, int bEncrypt = 1)
{
	CipherKey = Key;
	bEncrypt ? Encrypt (Data) : Decrypt (Data);
}

// this function encrypts 8-byte Data with 16-byte Key
// Data holds the plain text
// Key holds the Key
// Result will be stoed in Data
// if bEncrypt == TRUE, function will perform triple DES encryption, else triple DES decryption
void DES3 (BYTE *Data, BYTE *Key, int bEncrypt = 1)
{
	::CipherKey = Key;
	bEncrypt ? Encrypt (Data) : Decrypt (Data);
	::CipherKey = &Key[8];
	bEncrypt ? Decrypt (Data) : Encrypt (Data);
	::CipherKey = Key;
	bEncrypt ? Encrypt (Data) : Decrypt (Data);
}

void CACOSSampleCodesDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CACOSSampleCodesDlg)
	DDX_Control(pDX, IDC_CHECK2, m_check2);
	DDX_Control(pDX, IDC_BUTTON9, m_button9);
	DDX_Control(pDX, IDC_BUTTON8, m_button8);
	DDX_Control(pDX, IDC_BUTTON10, m_button10);
	DDX_Control(pDX, IDC_EDIT5, m_edit5);
	DDX_Control(pDX, IDC_EDIT4, m_edit4);
	DDX_Control(pDX, IDC_EDIT3, m_edit3);
	DDX_Control(pDX, IDC_EDIT2, m_edit2);
	DDX_Control(pDX, IDC_RADIO4, m_radio4);
	DDX_Control(pDX, IDC_BUTTON5, m_button5);
	DDX_Control(pDX, IDC_BUTTON6, m_button6);
	DDX_Control(pDX, IDC_EDIT1, m_edit1);
	DDX_Control(pDX, IDC_RADIO3, m_radio3);
	DDX_Control(pDX, IDC_BUTTON4, m_button4);
	DDX_Control(pDX, IDC_BUTTON2, m_button2);
	DDX_Control(pDX, IDC_BUTTON1, m_button1);
	DDX_Control(pDX, IDC_COMBO3, m_Combo);
	DDX_Control(pDX, IDC_LIST1, m_ListBox);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CACOSSampleCodesDlg, CDialog)
	//{{AFX_MSG_MAP(CACOSSampleCodesDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_RADIO3, OnRadio3)
	ON_BN_CLICKED(IDC_BUTTON4, OnButton4)
	ON_BN_CLICKED(IDC_BUTTON6, OnButton6)
	ON_BN_CLICKED(IDC_BUTTON5, OnButton5)
	ON_BN_CLICKED(IDC_RADIO4, OnRadio4)
	ON_BN_CLICKED(IDC_BUTTON8, OnButton8)
	ON_BN_CLICKED(IDC_BUTTON9, OnButton9)
	ON_BN_CLICKED(IDC_BUTTON10, OnButton10)
	ON_BN_CLICKED(IDC_CHECK2, OnCheck2)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CACOSSampleCodesDlg message handlers

BOOL CACOSSampleCodesDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	/*ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
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
	}*/

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	LoadReaderNames (this);
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CACOSSampleCodesDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CACOSSampleCodesDlg::OnPaint() 
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
HCURSOR CACOSSampleCodesDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void ClearBuffers()
{
	////Initialize the SendBuff and RecvBuff variables

	int indx;
    for (indx = 0;indx<263;indx++)
    {
      SendBuff[indx] = 0x00;
      RecvBuff[indx] = 0x00;
	}
}


void LoadReaderNames (CACOSSampleCodesDlg *p)
{
	//Load all the PCSC Readers connected
	
	char ReaderList [128];
	DWORD ReaderListSize = 128;
	int i;
	char *pch;

	if (hContext != 0)
		SCardReleaseContext (hContext);

	RetCode = SCardEstablishContext(
		SCARD_SCOPE_USER,
		NULL,
		NULL,
		&hContext);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = p->m_ListBox.AddString ("Unable to Establish Context");
		p->m_ListBox.SetCurSel (i);
		hContext = 0;

		return;
	}

	RetCode = SCardListReadersA(
		hContext,
		NULL,
		ReaderList,
		&ReaderListSize);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = p->m_ListBox.AddString ("Unable to List Readers");
		p->m_ListBox.SetCurSel (i);

		return;
	}

	pch = ReaderList;

	while (*pch != 0)
	{
		p->m_Combo.AddString (pch);
		pch += strlen (pch) + 1;
	}
	p->m_Combo.SetCurSel (0);
}

void CACOSSampleCodesDlg::OnButton1() 
{
	//Module where in connection to the reader is done

	int i;
	DWORD Protocol = 1;
	char buff[100];
	char buff1[100];

	m_Combo.GetLBText (m_Combo.GetCurSel (), ReaderName);

	RetCode = SCardConnectA(
		hContext,
		ReaderName,
		SCARD_SHARE_EXCLUSIVE,
		Protocol,
        &hCard,
		&Protocol);

	if (RetCode != SCARD_S_SUCCESS)
	{	//Fail to Connect
		i = m_ListBox.AddString ("Unable to Connect to Card");
		m_ListBox.SetCurSel (i);

		return;
	}
	
	//Success in Connecting to Reader
	IO_REQ.dwProtocol = Protocol;
	IO_REQ.cbPciLength = sizeof (SCARD_IO_REQUEST);
	
	m_Combo.GetLBText(m_Combo.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Connection to ", buff1);
	
	i = m_ListBox.AddString(buff);
	m_ListBox.SetCurSel (i);

	m_button2.EnableWindow(true);
	m_button4.EnableWindow(true);
	m_button8.EnableWindow(true);
	m_button9.EnableWindow(true);
	m_button10.EnableWindow(true);
	
	
	
	m_edit1.EnableWindow(true);
	m_edit2.EnableWindow(true);
	m_edit3.EnableWindow(true);
	m_edit4.EnableWindow(true);
	m_edit5.EnableWindow(true);
	
	m_radio3.EnableWindow(true);
	m_radio4.EnableWindow(true);
	m_radio3.SetCheck(1);
	m_radio4.SetCheck(0);

	m_edit1.SetLimitText(8);
	m_edit2.SetLimitText(8);
	m_edit3.SetLimitText(8);
	m_edit4.SetLimitText(8);
	m_edit5.SetLimitText(4);
	
	m_check2.EnableWindow(true);

}

void CACOSSampleCodesDlg::OnButton2() 
{	
	char buff[100];
	int i,ctr;
	DWORD RecvLength = 2;	
	DWORD Protocol = 1;
	//The whole function simply format and set files.

	if (m_radio3.GetCheck() == 1)
	{
		m_edit1.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit1.SetFocus();
			return;
		}
		m_edit2.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit2.SetFocus();
			return;
		}
		m_edit3.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit3.SetFocus();
			return;
		}
		m_edit4.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit4.SetFocus();
			return;
		}

	}
	else
	{
		m_edit1.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit1.SetFocus();
			return;
		}
		m_edit2.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit2.SetFocus();
			return;
		}
		m_edit3.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit3.SetFocus();
			return;
		}
		m_edit4.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit4.SetFocus();
			return;
		}
	}




	

	ClearBuffers();
	
	//SUBMIT IC CODE

	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0x20; //INS
	SendBuff[2] = 0x07; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x08; //P3

	SendBuff[5] = 0x41; //A
	SendBuff[6] = 0x43; //C
	SendBuff[7] = 0x4F; //O
	SendBuff[8] = 0x53; //S
	SendBuff[9] = 0x54; //T
	SendBuff[10] = 0x45; //E
	SendBuff[11] = 0x53; //S
	SendBuff[12] = 0x54; //T


	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x0D,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Submitting IC!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Submitting IC!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Submitting IC!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	ClearBuffers();
	//SELECT FILE FF02

	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0xA4; //INS
	SendBuff[2] = 0x00; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x02; //P3

	SendBuff[5] = 0xFF; //File ID
	SendBuff[6] = 0x02; //File ID
	


	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x07,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Selecting FF02!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Selecting FF02!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Selecting FF02!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);




	ClearBuffers();

	// Write to FF 02
    //  This will create 3 User files, no Option registers and
    //  Security Option registers defined, Personalization bit is not set.



	SendBuff[0] = 0x80;        // CLA
    SendBuff[1] = 0xD2;        // INS
    SendBuff[2] = 0x00;        // Record No
    SendBuff[3] = 0x00;        // P2
    SendBuff[4] = 0x04;        // P3
	
	if (m_radio3.GetCheck() == 1)
		SendBuff[5] = 0x29; //Only REV_DEB, DEB_MAC and Account bits are set
	else
		SendBuff[5] = 0x2B; //REV_DEB, DEB_MAC, 3-DES and Account bits are set

	SendBuff[6] = 0x00;  // Security option register 
	SendBuff[7] = 0x03; //No of user files
	SendBuff[8] = 0x00; //Personalization bit
    
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Updating FF02!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Updating FF02!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Updating FF02!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);

	//Perform a reset for changes in the ACOS to take effect
	m_Combo.GetLBText (m_Combo.GetCurSel (), ReaderName);

	RetCode = SCardDisconnect(
		hCard,
		SCARD_UNPOWER_CARD);

	RetCode = SCardConnectA(
		hContext,
		ReaderName,
		SCARD_SHARE_EXCLUSIVE,
		Protocol,
        &hCard,
		&Protocol);

	if (RetCode != SCARD_S_SUCCESS)
	{	//Fail to Connect
		i = m_ListBox.AddString ("Card reset is failed!");
		m_ListBox.SetCurSel (i);

		return;
	}

	i = m_ListBox.AddString ("Account files are enabled!");
	m_ListBox.SetCurSel (i);

	
	ClearBuffers();
	
	//SUBMIT IC CODE

	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0x20; //INS
	SendBuff[2] = 0x07; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x08; //P3

	SendBuff[5] = 0x41; //A
	SendBuff[6] = 0x43; //C
	SendBuff[7] = 0x4F; //O
	SendBuff[8] = 0x53; //S
	SendBuff[9] = 0x54; //T
	SendBuff[10] = 0x45; //E
	SendBuff[11] = 0x53; //S
	SendBuff[12] = 0x54; //T


	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x0D,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Submitting IC!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Submitting IC!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Submitting IC!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	ClearBuffers();
	//Select FF05
	
	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0xA4; //INS
	SendBuff[2] = 0x00; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x02; //P3

	SendBuff[5] = 0xFF; //File ID
	SendBuff[6] = 0x05; //File ID
	


	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x07,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Selecting FF05!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Selecting FF05!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Selecting FF05!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);



	ClearBuffers();
	

	//Record 00
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x00;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//TRANSTYP 0		    
	SendBuff[6] = 0x00;		//(3 bytes
	SendBuff[7] = 0x00;		// reserved for
	SendBuff[8] = 0x00;		// BALANCE 0)

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 00!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 00!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Writing Record 00!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	//Record 01
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x01;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//(2 bytes reserved		    
	SendBuff[6] = 0x00;		// for ATC 0)
	SendBuff[7] = 0x01;		// Set CHECKSUM 0
	SendBuff[8] = 0x00;		// 0x00

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 01!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 01!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Writing Record 01!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	//Record 02
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x02;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//TRANSTYP 0		    
	SendBuff[6] = 0x00;		//(3 bytes
	SendBuff[7] = 0x00;		// reserved for
	SendBuff[8] = 0x00;		// BALANCE 1)


	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 02!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 02!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Writing Record 02!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	//Record 03
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x03;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//(2 bytes reserved		    
	SendBuff[6] = 0x00;		// for ATC 1)
	SendBuff[7] = 0x01;		// Set CHECKSUM 1
	SendBuff[8] = 0x00;		// 0x00

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 03!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 02!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Writing Record 02!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);

	//Record 04
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x04;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0xFF;		//(3 bytes 		    
	SendBuff[6] = 0xFF;		// initialized for
	SendBuff[7] = 0xFF;		// MAX Balance)
	SendBuff[8] = 0x00;		// 0x00

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 04!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 04!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Writing Record 04!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	//Record 05
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x05;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//(4 bytes 		    
	SendBuff[6] = 0x00;		// Reserved
	SendBuff[7] = 0x00;		// for
	SendBuff[8] = 0x00;		// AID)

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 05!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 05!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Writing Record 05!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);



	//Record 06
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x06;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//(4 bytes 		    
	SendBuff[6] = 0x00;		// reserved
	SendBuff[7] = 0x00;		// for
	SendBuff[8] = 0x00;		// TTREF_C)

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 06!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 06!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}

	i = m_ListBox.AddString ("Success in Writing Record 06!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	//Record 07
	
	SendBuff[0] = 0x80;        // CLA
	SendBuff[1] = 0xD2;        // INS
	SendBuff[2] = 0x07;        // Record No
	SendBuff[3] = 0x00;        // P2
	SendBuff[4] = 0x04;        // P3
	
	SendBuff[5] = 0x00;		//(4 bytes 		    
	SendBuff[6] = 0x00;		// reserved
	SendBuff[7] = 0x00;		// for
	SendBuff[8] = 0x00;		// TTREF_D)

	RecvLength = 0x02;
	
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Writing Record 07!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Writing Record 07!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}

	i = m_ListBox.AddString ("Success in Writing Record 07!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	i = m_ListBox.AddString ("Success in Updating FF05!");
	m_ListBox.SetCurSel (i);



	//Select FF06
	
	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0xA4; //INS
	SendBuff[2] = 0x00; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x02; //P3

	SendBuff[5] = 0xFF; //File ID
	SendBuff[6] = 0x06; //File ID
	


	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x07,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Selecting FF06!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_ListBox.AddString ("Error in Selecting FF06!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Selecting FF06!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	
	//WRITE TO FF06

	if (m_radio3.GetCheck() == 1) //DES option uses 8-byte key
	{
		// Record 00 for Debit key
		m_edit2.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x00;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Debit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Debit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Debit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);


		// Record 01 for Credit key

		m_edit1.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x01;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Credit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Credit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Credit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);




		// Record 02 for Certify key

		m_edit3.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x02;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Certify Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Certify Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Certify Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);




		// Record 03 for Revoke Debit key
		m_edit4.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x03;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Revoke Debit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Revoke Debit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Revoke Debit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);


		



	}
	else //3DES option uses 16-byte key
	{

		// Record 04 for Left half of Debit key
		m_edit2.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff)/2) - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x04;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Debit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Debit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Left Half of Debit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);



		// Write on Record 00 the Right half of Debit key
		m_edit2.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff)/2) - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr + 8];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x00;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Debit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Debit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Right Half of Card Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);




		// Record 05 for Left half of Credit key
		m_edit1.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x05;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Credit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Credit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Left Half of Credit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
		

		// Record 01 for Right half of Credit key
		m_edit1.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr + 8];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x01;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Credit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Credit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Right Half of Credit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);



		// Record 06 for Left half of Certify key
		m_edit3.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x06;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Certify Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Certify Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Left Half of Certify Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
		

		// Record 02 for Right half of Certify key
		m_edit3.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr + 8];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x02;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Certify Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Certify Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Right Half of Certify Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);



		// Record 07 for Left half of Revoke Debit key
		m_edit4.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x07;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Revoke Debit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Left Half of Revoke Debit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Left Half of Revoke Debit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
		

		// Record 03 for Right half of Revoke Debit key
		m_edit4.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr + 8];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x03;        // Record No
		SendBuff[3] = 0x00;        // P2
		SendBuff[4] = 0x08;        // P3
		

		
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x0D,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Revoke Debit Key!");
			m_ListBox.SetCurSel (i);

			return;
		}

		if (RecvLength != 2)
		{
			i = m_ListBox.AddString ("Unable to Transmit Command to Card");
			m_ListBox.SetCurSel (i);
			return;
		}

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Writing Right Half of Revoke Debit Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Right Half of Revoke Debit Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

	}


	i = m_ListBox.AddString ("Success in Updating FF06!");
	m_ListBox.SetCurSel (i);
	
	m_edit1.SetWindowText("");
	m_edit2.SetWindowText("");
	m_edit3.SetWindowText("");
	m_edit4.SetWindowText("");
	m_edit5.SetWindowText("");
	i = m_ListBox.AddString ("OK!");
	m_ListBox.SetCurSel (i);
	return;	




	




}



void CACOSSampleCodesDlg::OnRadio3() 
{
	m_radio4.SetCheck(0);	
	m_radio3.SetCheck(1);
	m_edit1.SetLimitText(8);
	m_edit2.SetLimitText(8);
	m_edit3.SetLimitText(8);
	m_edit4.SetLimitText(8);
	
}



void CACOSSampleCodesDlg::OnButton4() 
{
	//THIS IS THE CREDIT MODULE

	int i,indx;
	char buff[100];
	unsigned char TTREFc[4];           
	unsigned char ATREF[6];         
	unsigned char tmpArray[32];
	unsigned long Amount;
	unsigned char tmpKey[16];    //credit key to verify MAC     
	unsigned char CreditKey3DES [16];
	unsigned char CreditKeyDES [8];

	DWORD RecvLength;
	

	if (m_radio3.GetCheck() == 1)
	{	
		m_edit1.GetWindowText(buff, 100);
		for (indx = 0;indx<8;indx++)
		{
			CreditKeyDES[indx] = buff[indx];
		}
		
	}
	else
	{
		m_edit1.GetWindowText(buff, 100);
		for (indx = 0;indx<16;indx++)
		{
			CreditKey3DES[indx] = buff[indx];
		}
	}

	if (m_radio3.GetCheck() == 1)
	{
		
		m_edit1.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit1.SetFocus();
			return;
		}
		
	}
	else
	{
		
		m_edit1.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit1.SetFocus();
			return;
		}
		
	}


	m_edit5.GetWindowText(buff, 100);
	if (strlen(buff) <= 0) 
	{
		i = m_ListBox.AddString ("Invalid Input!");
		m_ListBox.SetCurSel (i);
		this->m_edit5.SetFocus();
		return;
	}


	ClearBuffers();

	//Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
    //    Arbitrary data is 01 01 01 01	

    RecvLength = 2;
        
    SendBuff[0] = 0x80; //CLA
    SendBuff[1] = 0xE4; //INS
    SendBuff[2] = 0x02;  //P1
    SendBuff[3] = 0x00;  //P2
    SendBuff[4] = 0x04;  //P3	
	SendBuff[5] = 0x01;  //Arbitrary Data	
	SendBuff[6] = 0x01;  //Arbitrary Data	
	SendBuff[7] = 0x01;  //Arbitrary Data	
	SendBuff[8] = 0x01;  //Arbitrary Data

		RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x61) | (RecvBuff[1] != 0x19))
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}



	//Issue GET RESPONSE command with Le = 19h or 25 bytes.

	ClearBuffers();
    RecvLength = 0x00;
	SendBuff[0] = 0x80;         // CLA
	SendBuff[1] = 0xC0;        // INS
	SendBuff[2] = 0x00;         //P1
	SendBuff[3] = 0x00;         // P2
	SendBuff[4] = 0x19;        //P3
	RecvLength = SendBuff[4] + 2;
			
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);

			return;
		}
			
		if ((RecvBuff[0x19] != 0x90) | (RecvBuff[0x1A] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);
		
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		//Store ACOS3 card values for TTREFc and ATREF

		for (indx = 0;indx<4;indx++)
		{
			TTREFc[indx] = RecvBuff[indx + 17];
		}
		for (indx = 0;indx<6;indx++)
		{
			ATREF[indx] = RecvBuff[indx + 8];
		}
		m_edit5.GetWindowText(buff, 100);
		Amount = atol(buff);

		//Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
		//    use tmpArray as the data block
		tmpArray[0] = 0xE2;
		tmpArray[1] = (unsigned char)(Amount >> 16);                  // Amount MSByte
		tmpArray[2] = (unsigned char)(Amount >> 8) % 256;           // Amount Middle Byte
		tmpArray[3] = (unsigned char)Amount % 256;                  // Amount LSByte

		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 4] = TTREFc[indx];
		}

		for (indx = 0;indx<6;indx++)
		{
			tmpArray[indx + 8] = ATREF[indx];
		}

		tmpArray[13] = tmpArray[13] + 1;              //increment last byte of ATREF
		tmpArray[14] = 0x00;
		tmpArray[15] = 0x00;
		
		//Generate applicable MAC values, MAC result will be stored in tmpArray
		if ((m_radio3.GetCheck() == 1) )
		{
			for (indx = 0;indx<8;indx++)
			{
				tmpKey[indx] = CreditKeyDES[indx];
				
			}
			//MAC PROCESS

			DES (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES (tmpArray, tmpKey); //Encryption Process
			
			
		}
		else
		{
			for (indx = 0;indx<16;indx++)
			{
				tmpKey[indx] = CreditKey3DES[indx];
				
			}
			//3MAC PROCESS

			DES3 (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES3 (tmpArray, tmpKey); //Encryption Process
		}

		tmpArray[4] = (unsigned char)Amount >> 16;                  // Amount MSByte
		tmpArray[5] = (unsigned char)(Amount >> 8) % 256;           // Amount Middle Byte
		tmpArray[6] = (unsigned char)Amount % 256;                  // Amount LSByte

		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 7] = TTREFc[indx];
		}

		
		ClearBuffers();
		RecvLength =  0x00;

		SendBuff[0] = 0x80;         // CLA
		SendBuff[1] = 0xE2;        // INS
		SendBuff[2] = 0x00;         //P1
		SendBuff[3] = 0x00;         // P2
		SendBuff[4] = 0x0B;        //P3
		RecvLength = 2;
		
		for (indx = 0;indx<12;indx++)
		{
			SendBuff[indx + 5] = tmpArray[indx];
		}

		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x05 + 0x0B,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("CREDIT AMOUNT command failed!");
			m_ListBox.SetCurSel (i);

			return;
		}
			
		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("CREDIT AMOUNT command failed!");
			m_ListBox.SetCurSel (i);
		
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		
		i = m_ListBox.AddString ("Credit transaction completed!");
		m_ListBox.SetCurSel (i);

		m_edit1.SetWindowText("");
		m_edit2.SetWindowText("");
		m_edit3.SetWindowText("");
		m_edit4.SetWindowText("");
		m_edit5.SetWindowText("");
	




}

void CACOSSampleCodesDlg::OnButton6() 
{
	SCardDisconnect(
		hCard,
		SCARD_UNPOWER_CARD);

	SCardReleaseContext (hContext);

	this->EndDialog(0); 
	
}

void CACOSSampleCodesDlg::OnButton5() 
{
	int i;

	SCardDisconnect(
		hCard,
		SCARD_UNPOWER_CARD);

	
	i = m_ListBox.AddString("Reader is Disconnected Successfully!");
	m_ListBox.SetCurSel (i);

	m_radio3.EnableWindow(false);
	m_radio4.EnableWindow(false);

	m_radio3.SetCheck(0);
	m_radio4.SetCheck(0);
	
	m_edit1.EnableWindow(false);
	m_edit2.EnableWindow(false);
	m_edit3.EnableWindow(false);
	m_edit4.EnableWindow(false);
	m_edit5.EnableWindow(false);
	
	
	m_edit1.SetWindowText("");
	m_edit2.SetWindowText("");
	m_edit3.SetWindowText("");
	m_edit4.SetWindowText("");
	m_edit5.SetWindowText("");
	
	
	m_button2.EnableWindow(false);
	m_button4.EnableWindow(false);
	m_button8.EnableWindow(false);
	m_button9.EnableWindow(false);
	m_button10.EnableWindow(false);

	m_check2.EnableWindow(false);
	
}

void CACOSSampleCodesDlg::OnRadio4() 
{
	m_radio3.SetCheck(0);	
	m_radio4.SetCheck(1);
	m_edit1.SetLimitText(16);
	m_edit2.SetLimitText(16);
	m_edit3.SetLimitText(16);
	m_edit4.SetLimitText(16);
}


void CACOSSampleCodesDlg::OnButton8() 
{

	//THIS IS THE INQUIRE BALANCE MODULE

	int i,indx;
	char buff[100];
	char tmpStr[100];
	unsigned char TTREFc[4];           
	unsigned char TTREFd[4];         
	unsigned char ATREF[6];         
	unsigned char tmpArray[32];
	unsigned char LastTran;
	unsigned long tmpBalance;         
	unsigned char tmpKey[16];    //certify key to verify MAC     
	unsigned char CertifyKeyDES [8];
	unsigned char CertifyKey3DES [16];


	

	DWORD RecvLength;
	
	if (m_radio3.GetCheck() == 1)
	{	
		m_edit3.GetWindowText(buff, 100);
		for (indx = 0;indx<8;indx++)
		{
			CertifyKeyDES[indx] = buff[indx];
		}
		
	}
	else
	{
		m_edit3.GetWindowText(buff, 100);
		for (indx = 0;indx<16;indx++)
		{
			CertifyKey3DES[indx] = buff[indx];
		}
	}

	if (m_radio3.GetCheck() == 1)
	{
		
		m_edit3.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit3.SetFocus();
			return;
		}
		
	}
	else
	{
		
		m_edit3.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit3.SetFocus();
			return;
		}
		
	}



	//Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
    //    Arbitrary data is 01 01 01 01	

	ClearBuffers();
    RecvLength = 2;
        
    SendBuff[0] = 0x80; //CLA
    SendBuff[1] = 0xE4; //INS
    SendBuff[2] = 0x02;  //P1
    SendBuff[3] = 0x00;  //P2
    SendBuff[4] = 0x04;  //P3	
	SendBuff[5] = 0x01;  //Arbitrary Data	
	SendBuff[6] = 0x01;  //Arbitrary Data	
	SendBuff[7] = 0x01;  //Arbitrary Data	
	SendBuff[8] = 0x01;  //Arbitrary Data

		RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x61) | (RecvBuff[1] != 0x19))
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}



	//Issue GET RESPONSE command with Le = 19h or 25 bytes.

	ClearBuffers();
    RecvLength = 0x00;
	SendBuff[0] = 0x80;         // CLA
	SendBuff[1] = 0xC0;        // INS
	SendBuff[2] = 0x00;         //P1
	SendBuff[3] = 0x00;         // P2
	SendBuff[4] = 0x19;        //P3
	RecvLength = SendBuff[4] + 2;
			
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);

			return;
		}
			
		if ((RecvBuff[0x19] != 0x90) | (RecvBuff[0x1A] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);
		
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		//Check integrity of data returned by card.
		//Build MAC input data. Extract the info from ACOS3 card in Dataout.

		LastTran = RecvBuff[4];
		tmpBalance = (RecvBuff[5] * 256 * 256) + (RecvBuff[6] * 256) + (RecvBuff[7]);
		
		
		for (indx = 0;indx<4;indx++)
		{
			TTREFc[indx] = RecvBuff[indx + 17];
		}
		for (indx = 0;indx<4;indx++)
		{
			TTREFd[indx] = RecvBuff[indx + 21];
		}
		for (indx = 0;indx<6;indx++)
		{
			ATREF[indx] = RecvBuff[indx + 8];
		}

		//Move data from ACOS card as input to MAC calculations

		//Arbitrary Data
		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx] = 0x01;
		}

		tmpArray[4] = RecvBuff[4];          // 4 BYTE MAC + LAST TRANS TYPE
		
		for (indx = 0;indx<3;indx++)
		{
			tmpArray[indx + 5] = RecvBuff[indx + 5];
		}
		
		for (indx = 0;indx<6;indx++)
		{
			tmpArray[indx + 8] = RecvBuff[indx + 8];
		}

		//Pad 2 bytes of zero value according to formula.
		tmpArray[14] = 0x00;
		tmpArray[15] = 0x00;

		//Copy TTREFc
		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 16] = TTREFc[indx];
		}

		//Copy TTREFd
		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 20] = TTREFd[indx];
		}


		//Generate applicable MAC values

		

		if ((m_radio3.GetCheck() == 1) )
		{
			for (indx = 0;indx<8;indx++)
			{
				tmpKey[indx] = CertifyKeyDES[indx];
				
			}
			//MAC PROCESS

			DES (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES (tmpArray, tmpKey); //Encryption Process
			
			
		}
		else
		{
			for (indx = 0;indx<16;indx++)
			{
				tmpKey[indx] = CertifyKey3DES[indx];
				
			}
			//MAC PROCESS

			DES3 (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES3 (tmpArray, tmpKey); //Encryption Process
		}

		//Compare MAC values

		for (indx = 0;indx<4;indx++)
			{
				if (tmpArray[indx] != RecvBuff[indx])
				{
					
					i = m_ListBox.AddString ("MAC is incorrect, data integrity is jeopardized!");
					m_ListBox.SetCurSel (i);
					return;

				}
            
			}

		switch (LastTran)
		{
			case 0x01:
				strcpy(tmpStr,"DEBIT");
				break;
			case 0x03:
				strcpy(tmpStr,"CREDIT");
				break;
			default:
				strcpy(tmpStr,"NOT DEFINED");
				break;
		}

		m_edit1.SetWindowText("") ;

		sprintf(buff, "%s %s", "Last transaction is ", tmpStr);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
		
		sprintf(buff,"%s %ld ","Amount is :",tmpBalance);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		m_edit1.SetWindowText("");
		m_edit2.SetWindowText("");
		m_edit3.SetWindowText("");
		m_edit4.SetWindowText("");
		m_edit5.SetWindowText("");
				
		
	
}

void CACOSSampleCodesDlg::OnButton9() 
{	
	//THIS IS THE DEBIT MODULE

	int i,indx;
	char buff[100];
	unsigned char TTREFd[4];           
	unsigned char ATREF[6];         
	unsigned char tmpArray[32];
	unsigned long Amount;
	unsigned char tmpKey[16];    //debit key to verify MAC     
	unsigned char DebitKey3DES [16];
	unsigned char DebitKeyDES [8];
	unsigned char tmpBalance[3];
	unsigned long new_balance;
	DWORD RecvLength;
	

	if (m_radio3.GetCheck() == 1)
	{	
		m_edit2.GetWindowText(buff, 100);
		for (indx = 0;indx<8;indx++)
		{
			DebitKeyDES[indx] = buff[indx];
		}
		
	}
	else
	{
		m_edit2.GetWindowText(buff, 100);
		for (indx = 0;indx<16;indx++)
		{
			DebitKey3DES[indx] = buff[indx];
		}
	}

	if (m_radio3.GetCheck() == 1)
	{
		
		m_edit2.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit2.SetFocus();
			return;
		}
		
	}
	else
	{
		
		m_edit2.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit2.SetFocus();
			return;
		}
		
	}


	m_edit5.GetWindowText(buff, 100);
	if (strlen(buff) <= 0) 
	{
		i = m_ListBox.AddString ("Invalid Input!");
		m_ListBox.SetCurSel (i);
		this->m_edit5.SetFocus();
		return;
	}

	ClearBuffers();

	//Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
    //    Arbitrary data is 01 01 01 01	

    RecvLength = 2;
        
    SendBuff[0] = 0x80; //CLA
    SendBuff[1] = 0xE4; //INS
    SendBuff[2] = 0x02;  //P1
    SendBuff[3] = 0x00;  //P2
    SendBuff[4] = 0x04;  //P3	
	SendBuff[5] = 0x01;  //Arbitrary Data	
	SendBuff[6] = 0x01;  //Arbitrary Data	
	SendBuff[7] = 0x01;  //Arbitrary Data	
	SendBuff[8] = 0x01;  //Arbitrary Data

		RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x61) | (RecvBuff[1] != 0x19))
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}



	//Issue GET RESPONSE command with Le = 19h or 25 bytes.

	ClearBuffers();
    RecvLength = 0x00;
	SendBuff[0] = 0x80;         // CLA
	SendBuff[1] = 0xC0;        // INS
	SendBuff[2] = 0x00;         //P1
	SendBuff[3] = 0x00;         // P2
	SendBuff[4] = 0x19;        //P3
	RecvLength = SendBuff[4] + 2;
			
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);

			return;
		}
			
		if ((RecvBuff[0x19] != 0x90) | (RecvBuff[0x1A] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);
		
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		tmpBalance[1] = RecvBuff[7];
		tmpBalance[2] = RecvBuff[6];
		tmpBalance[2] = tmpBalance[2] * 256;
		tmpBalance[3] = RecvBuff[5];
		tmpBalance[3] = tmpBalance[3] * 256;
		tmpBalance[3] = tmpBalance[3] * 256;
		tmpBalance[0] = tmpBalance[1] + tmpBalance[2] + tmpBalance[3];
		


		//Store ACOS card values for TTREFd and ATREF

		for (indx = 0;indx<4;indx++)
		{
			TTREFd[indx] = RecvBuff[indx + 21];
		}
		for (indx = 0;indx<6;indx++)
		{
			ATREF[indx] = RecvBuff[indx + 8];
		}
		m_edit5.GetWindowText(buff, 100);
		Amount = atol(buff);

		//Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
		//    use tmpArray as the data block
		tmpArray[0] = 0xE6;
		tmpArray[1] = (unsigned char)Amount >> 16;                  // Amount MSByte
		tmpArray[2] = (unsigned char)(Amount >> 8) % 256;           // Amount Middle Byte
		tmpArray[3] = (unsigned char)Amount % 256;                  // Amount LSByte

		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 4] = TTREFd[indx];
		}

		for (indx = 0;indx<6;indx++)
		{
			tmpArray[indx + 8] = ATREF[indx];
		}

		tmpArray[13] = tmpArray[13] + 1;              //increment last byte of ATREF
		tmpArray[14] = 0x00;
		tmpArray[15] = 0x00;
		
		//Generate applicable MAC values, MAC result will be stored in tmpArray
		if ((m_radio3.GetCheck() == 1) )
		{
			for (indx = 0;indx<8;indx++)
			{
				tmpKey[indx] = DebitKeyDES[indx];
				
			}
			//MAC PROCESS

			DES (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES (tmpArray, tmpKey); //Encryption Process
			
			
		}
		else
		{
			for (indx = 0;indx<16;indx++)
			{
				tmpKey[indx] = DebitKey3DES[indx];
				
			}
			//3MAC PROCESS

			DES3 (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES3 (tmpArray, tmpKey); //Encryption Process
		}

		tmpArray[4] = (unsigned char)Amount >> 16;                  // Amount MSByte
		tmpArray[5] = (unsigned char)(Amount >> 8) % 256;           // Amount Middle Byte
		tmpArray[6] = (unsigned char)Amount % 256;                  // Amount LSByte

		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 7] = TTREFd[indx];
		}

		
		

		
		if (m_check2.GetCheck() == 0) //Without Debit Certificate
		{
			
			ClearBuffers();
			RecvLength =  0x00;

			SendBuff[0] = 0x80;         // CLA
			SendBuff[1] = 0xE6;        // INS
			SendBuff[2] = 0x00;         //P1
			SendBuff[3] = 0x00;         // P2
			SendBuff[4] = 0x0B;        //P3
			RecvLength = 2;
			
			for (indx = 0;indx<12;indx++)
			{
				SendBuff[indx + 5] = tmpArray[indx];
			}

			RetCode = SCardTransmit(
				hCard,
				&IO_REQ,
				SendBuff,
				0x05 + 0x0B,
				NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
				RecvBuff,
				&RecvLength);

			if (RetCode != SCARD_S_SUCCESS)
			{
				i = m_ListBox.AddString ("DEBIT AMOUNT command failed!");
				m_ListBox.SetCurSel (i);

				return;
			}
				
			if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
			{
				i = m_ListBox.AddString ("DEBIT AMOUNT command failed!");
				m_ListBox.SetCurSel (i);
			
				sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
				i = m_ListBox.AddString (buff);
				m_ListBox.SetCurSel (i);

				return;
			}



		}
		else						//With Debit Certificate	
		{


			ClearBuffers();
			RecvLength =  0x00;

			//Debit Command
			SendBuff[0] = 0x80;         // CLA
			SendBuff[1] = 0xE6;        // INS
			SendBuff[2] = 0x01;         //P1
			SendBuff[3] = 0x00;         // P2
			SendBuff[4] = 0x0B;        //P3
			RecvLength = 2;
			
			for (indx = 0;indx<12;indx++)
			{
				SendBuff[indx + 5] = tmpArray[indx];
			}

			RetCode = SCardTransmit(
				hCard,
				&IO_REQ,
				SendBuff,
				0x05 + 0x0B,
				NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
				RecvBuff,
				&RecvLength);

			if (RetCode != SCARD_S_SUCCESS)
			{
				i = m_ListBox.AddString ("DEBIT AMOUNT command failed!");
				m_ListBox.SetCurSel (i);

				return;
			}
				
			if ((RecvBuff[0] != 0x61) | (RecvBuff[1] != 0x04))
			{
				i = m_ListBox.AddString ("DEBIT AMOUNT command failed!");
				m_ListBox.SetCurSel (i);
			
				sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
				i = m_ListBox.AddString (buff);
				m_ListBox.SetCurSel (i);

				return;
			}


			//Issue GET RESPONSE command with Le = 19h or 25 bytes.

			ClearBuffers();
			RecvLength = 0x00;
			SendBuff[0] = 0x80;         // CLA
			SendBuff[1] = 0xC0;        // INS
			SendBuff[2] = 0x00;         //P1
			SendBuff[3] = 0x00;         // P2
			SendBuff[4] = 0x04;        //P3
			RecvLength = SendBuff[4] + 2;
					
				RetCode = SCardTransmit(
					hCard,
					&IO_REQ,
					SendBuff,
					0x05,
					NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
					RecvBuff,
					&RecvLength);

				if (RetCode != SCARD_S_SUCCESS)
				{
					i = m_ListBox.AddString ("Error in Get Response!");
					m_ListBox.SetCurSel (i);

					return;
				}
					
				if ((RecvBuff[0x04] != 0x90) | (RecvBuff[0x05] != 0x00))
				{
					i = m_ListBox.AddString ("Error in Get Response!");
					m_ListBox.SetCurSel (i);
				
					sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
					i = m_ListBox.AddString (buff);
					m_ListBox.SetCurSel (i);

					return;
				}			

			//Prepare MAC data block: 01 + New Balance + ATC + TTREFD + 00 + 00 + 00
			//    use tmpArray as the data block
			
			m_edit5.GetWindowText(buff, 100);
			Amount = atol(buff);

			new_balance = (tmpBalance[0] - Amount);
			tmpArray[0] = 0x01;

			tmpArray[0] = 0x01;
			tmpArray[1] = (unsigned char)new_balance >> 16;                  // New Balance MSByte
			tmpArray[2] = (unsigned char)(new_balance >> 8) % 256;           // New Balance Middle Byte
			tmpArray[3] = (unsigned char)new_balance % 256;                  // New Balance LSByte

			tmpArray[4] = (unsigned char)Amount >> 16;						// Amount MSByte
			tmpArray[5] = (unsigned char)(Amount >> 8) % 256;				// Amount Middle Byte
			tmpArray[6] = (unsigned char)Amount % 256;						// Amount LSByte

			tmpArray[7] = ATREF[4];
			tmpArray[8] = (ATREF[5] + 1);										// Increment ATC after every transaction
			
			for (indx = 0;indx<3;indx++)
				{
					tmpArray[indx + 9] = TTREFd[indx];
				}
			
			tmpArray[13] = 0x00;
			tmpArray[14] = 0x00;
			tmpArray[15] = 0x00;


			//Generate applicable MAC values, MAC result will be stored in tmpArray
			if ((m_radio3.GetCheck() == 1) )
			{
				for (indx = 0;indx<8;indx++)
				{
					tmpKey[indx] = DebitKeyDES[indx];
					
				}
				//MAC PROCESS

				DES (tmpArray, tmpKey); //Encryption Process
				for (indx = 0;indx<8;indx++)
				{
					tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
				}
				DES (tmpArray, tmpKey); //Encryption Process
				
				
			}
			else
			{
				for (indx = 0;indx<16;indx++)
				{
					tmpKey[indx] = DebitKey3DES[indx];
					
				}
				//3MAC PROCESS

				DES3 (tmpArray, tmpKey); //Encryption Process
				for (indx = 0;indx<8;indx++)
				{
					tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
				}
				DES3 (tmpArray, tmpKey); //Encryption Process
			}


			for (indx = 0;indx<3;indx++)
				{
					if (tmpArray[indx] != RecvBuff[indx]) 
					{
						i = m_ListBox.AddString ("Debit Certificate Failed.");
						m_ListBox.SetCurSel (i);
					}
					
				}

			i = m_ListBox.AddString ("Debit Certificate Verified.");
			m_ListBox.SetCurSel (i);


		}

		i = m_ListBox.AddString ("Debit transaction completed!");
		m_ListBox.SetCurSel (i);

		m_edit1.SetWindowText("");
		m_edit2.SetWindowText("");
		m_edit3.SetWindowText("");
		m_edit4.SetWindowText("");
		m_edit5.SetWindowText("");
	
}

void CACOSSampleCodesDlg::OnButton10() 
{	
	//THIS IS THE REVOKE DEBIT MODULE

	int i,indx;
	char buff[100];
	unsigned char TTREFd[4];           
	unsigned char ATREF[6];         
	unsigned char tmpArray[32];
	unsigned long Amount;
	unsigned char tmpKey[16];    //revoke debit key to verify MAC     
	unsigned char RevokeDebitKey3DES [16];
	unsigned char RevokeDebitKeyDES [8];
	DWORD RecvLength;
	

	if (m_radio3.GetCheck() == 1)
	{	
		m_edit4.GetWindowText(buff, 100);
		for (indx = 0;indx<8;indx++)
		{
			RevokeDebitKeyDES[indx] = buff[indx];
		}
		
	}
	else
	{
		m_edit4.GetWindowText(buff, 100);
		for (indx = 0;indx<16;indx++)
		{
			RevokeDebitKey3DES[indx] = buff[indx];
		}
	}

	if (m_radio3.GetCheck() == 1)
	{
		
		m_edit4.GetWindowText(buff, 100);
		if (strlen(buff) < 8) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit4.SetFocus();
			return;
		}
		
	}
	else
	{
		
		m_edit4.GetWindowText(buff, 100);
		if (strlen(buff) < 16) 
		{
			i = m_ListBox.AddString ("Invalid Input!");
			m_ListBox.SetCurSel (i);
			this->m_edit4.SetFocus();
			return;
		}
		
	}

	m_edit5.GetWindowText(buff, 100);
	if (strlen(buff) <= 0) 
	{
		i = m_ListBox.AddString ("Invalid Input!");
		m_ListBox.SetCurSel (i);
		this->m_edit5.SetFocus();
		return;
	}

	ClearBuffers();

	//Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
    //    Arbitrary data is 01 01 01 01	

    RecvLength = 2;
        
    SendBuff[0] = 0x80; //CLA
    SendBuff[1] = 0xE4; //INS
    SendBuff[2] = 0x02;  //P1
    SendBuff[3] = 0x00;  //P2
    SendBuff[4] = 0x04;  //P3	
	SendBuff[5] = 0x01;  //Arbitrary Data	
	SendBuff[6] = 0x01;  //Arbitrary Data	
	SendBuff[7] = 0x01;  //Arbitrary Data	
	SendBuff[8] = 0x01;  //Arbitrary Data

		RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x09,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);

		return;
	}

	if (RecvLength != 2)
	{
		i = m_ListBox.AddString ("Unable to Transmit Command to Card");
		m_ListBox.SetCurSel (i);
		return;
	}

	//Retrieving the return code is it is a success or not

	if ((RecvBuff[0] != 0x61) | (RecvBuff[1] != 0x19))
	{
		i = m_ListBox.AddString ("INQUIRE ACCOUNT command failed!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}



	//Issue GET RESPONSE command with Le = 19h or 25 bytes.

	ClearBuffers();
    RecvLength = 0x00;
	SendBuff[0] = 0x80;         // CLA
	SendBuff[1] = 0xC0;        // INS
	SendBuff[2] = 0x00;         //P1
	SendBuff[3] = 0x00;         // P2
	SendBuff[4] = 0x19;        //P3
	RecvLength = SendBuff[4] + 2;
			
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);

			return;
		}
			
		if ((RecvBuff[0x19] != 0x90) | (RecvBuff[0x1A] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);
		
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		//Store ACOS3 card values for TTREFd and ATREF

		for (indx = 0;indx<4;indx++)
		{
			TTREFd[indx] = RecvBuff[indx + 21];
		}
		for (indx = 0;indx<6;indx++)
		{
			ATREF[indx] = RecvBuff[indx + 8];
		}
		m_edit5.GetWindowText(buff, 100);
		Amount = atol(buff);

		//Prepare MAC data block: E8 + AMT + TTREFd + ATREF + 00 + 00
		//    use tmpArray as the data block
		tmpArray[0] = 0xE8;
		tmpArray[1] = (unsigned char)Amount >> 16;                  // Amount MSByte
		tmpArray[2] = (unsigned char)(Amount >> 8) % 256;           // Amount Middle Byte
		tmpArray[3] = (unsigned char)Amount % 256;                  // Amount LSByte

		for (indx = 0;indx<4;indx++)
		{
			tmpArray[indx + 4] = TTREFd[indx];
		}

		for (indx = 0;indx<6;indx++)
		{
			tmpArray[indx + 8] = ATREF[indx];
		}

		tmpArray[13] = tmpArray[13] + 1;              //increment last byte of ATREF
		tmpArray[14] = 0x00;
		tmpArray[15] = 0x00;
		
		//Generate applicable MAC values, MAC result will be stored in tmpArray
		if ((m_radio3.GetCheck() == 1) )
		{
			for (indx = 0;indx<8;indx++)
			{
				tmpKey[indx] = RevokeDebitKeyDES[indx];
				
			}
			//MAC PROCESS

			DES (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES (tmpArray, tmpKey); //Encryption Process
			
			
		}
		else
		{
			for (indx = 0;indx<16;indx++)
			{
				tmpKey[indx] = RevokeDebitKey3DES[indx];
				
			}
			//3MAC PROCESS

			DES3 (tmpArray, tmpKey); //Encryption Process
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= tmpArray[indx + 8];
			}
			DES3 (tmpArray, tmpKey); //Encryption Process
		}

		
		ClearBuffers();
		RecvLength =  0x00;

		SendBuff[0] = 0x80;         // CLA
		SendBuff[1] = 0xE8;        // INS
		SendBuff[2] = 0x00;         //P1
		SendBuff[3] = 0x00;         // P2
		SendBuff[4] = 0x04;        //P3
		RecvLength = 2;
		
		for (indx = 0;indx<5;indx++)
		{
			SendBuff[indx + 5] = tmpArray[indx];
		}

		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x04 + 0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("REVOKE DEBIT command failed!");
			m_ListBox.SetCurSel (i);

			return;
		}
			
		if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
		{
			i = m_ListBox.AddString ("REVOKE DEBIT command failed!");
			m_ListBox.SetCurSel (i);
		
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		
		i = m_ListBox.AddString ("Revoke Debit transaction completed!");
		m_ListBox.SetCurSel (i);

		m_edit1.SetWindowText("");
		m_edit2.SetWindowText("");
		m_edit3.SetWindowText("");
		m_edit4.SetWindowText("");
		m_edit5.SetWindowText("");

		

}

void CACOSSampleCodesDlg::OnCheck2() 
{
	
}
