//  Copyright(C):      Advanced Card Systems Ltd
//
//
//  Description:       This sample program outlines the steps on how to
//                     use the ACOS card for the Mutual Authentication
//                     process using the PC/SC platform.
//
//  Author:            Fernando G. Robles
//
//  Date:              Aug. 12, 2005
//
//  Revision Trail:   (Date/Author/Description)
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


// GLOBAL VARIABLES
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
	//Initialize the SendBuff and RecvBuff variables

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
	
	
	m_edit1.EnableWindow(true);
	m_edit2.EnableWindow(true);
	
	m_radio3.EnableWindow(true);
	m_radio4.EnableWindow(true);
	m_radio3.SetCheck(1);
	m_radio4.SetCheck(0);

	m_edit1.SetLimitText(8);
	m_edit2.SetLimitText(8);
	

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
		SendBuff[5] = 0x00; //3-DES is not set
	else
		SendBuff[5] = 0x02; //3-DES is enabled

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
	//Select FF03
	
	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0xA4; //INS
	SendBuff[2] = 0x00; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x02; //P3

	SendBuff[5] = 0xFF; //File ID
	SendBuff[6] = 0x03; //File ID
	


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
		i = m_ListBox.AddString ("Error in Selecting FF03!");
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
		i = m_ListBox.AddString ("Error in Selecting FF03!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Selecting FF03!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);



	ClearBuffers();
	

	

	if (m_radio3.GetCheck() == 1) //DES option uses 8-byte key
	{
		// Record 02 for Card key
		m_edit1.GetWindowText(buff, 100);
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
			i = m_ListBox.AddString ("Error in Writing Card Key!");
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
			i = m_ListBox.AddString ("Error in Writing Card Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Card Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		// Record 03 for Terminal key
		m_edit2.GetWindowText(buff, 100);
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
			i = m_ListBox.AddString ("Error in Writing Terminal Key!");
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
			i = m_ListBox.AddString ("Error in Writing Terminal Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Terminal Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);




	}
	else //3DES option uses 16-byte key
	{

		// Write Record 02 for Left half of Card key
		m_edit1.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff)/2) - 1); ctr++)
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
			i = m_ListBox.AddString ("Error in Writing Left Half of Card Key!");
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
			i = m_ListBox.AddString ("Error in Writing Left Half of Card Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Left Half of Card Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);



		// Write on Record 12 the Right half of Card key
		m_edit1.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff)/2) - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr + 8];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x0C;        // Record No
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
			i = m_ListBox.AddString ("Error in Writing Right Half of Card Key!");
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
			i = m_ListBox.AddString ("Error in Writing Right Half of Card Key!");
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




		// Write Record 03 for Left half of Terminal key
		m_edit2.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
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
			i = m_ListBox.AddString ("Error in Writing Left Half of Terminal Key!");
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
			i = m_ListBox.AddString ("Error in Writing Left Half of Terminal Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Left Half of Terminal Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
		

		//Write Record 13 of Right half of Terminal key
		m_edit2.GetWindowText(buff, 100);
		i = 5;
		for (ctr = 0; ctr <= (int)((strlen((char*)buff))/2 - 1); ctr++)
		{
        
            SendBuff[i] = buff[ctr + 8];
            
            i = i + 1;
		}

		SendBuff[0] = 0x80;        // CLA
		SendBuff[1] = 0xD2;        // INS
		SendBuff[2] = 0x0D;        // Record No
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
			i = m_ListBox.AddString ("Error in Writing Right Half of Terminal Key!");
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
			i = m_ListBox.AddString ("Error in Writing Right Half of Terminal Key!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Writing Right Half of Terminal Key!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
	}
	
	m_edit1.SetWindowText("");
	m_edit2.SetWindowText("");
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
	
}



void CACOSSampleCodesDlg::OnButton4() 
{
	//MUTUAL AUTHENTICATION FUNCTION

	char buff[100];
	char tmpStr[100];
	int i,indx;
	DWORD RecvLength = 2;	
	DWORD Protocol = 1;
	unsigned char tmpArray[32];
	unsigned char tmpResult[32];
	unsigned char CRnd[8];		//Card random number
	unsigned char TRnd[8];		//Terminal random number
	unsigned char ReverseKey[32]; //Reverse of Terminal Key

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
	}

	
	
		
		

		

		//Issue Get Challenge Command
		RecvLength = 0x00;
		SendBuff[0] = 0x80; //CLA
		SendBuff[1] = 0x84; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x08; //P3
		RecvLength = 0x0A;
		
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
			i = m_ListBox.AddString ("Error in Get Challenge!");
			m_ListBox.SetCurSel (i);

			return;
		}

	

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0x08] != 0x90) | (RecvBuff[0x09] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Get Challenge!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Get Challenge!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0x08],RecvBuff[0x09]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		//Store the random number generated by the card to Crnd

		for (indx = 0;indx<8;indx++)
		{
			CRnd[indx] = RecvBuff[indx];
		}
		
		//Retrieve Terminal Key from Input Template
		m_edit2.GetWindowText(tmpStr, 100);
		for (indx = 0;indx <= (int)strlen((char*)tmpStr) - 1;indx++)
		{
			tKey[indx] = tmpStr[indx];
		}

		// Encrypt Random No (CRnd) with Terminal Key (tKey)
		//    tmpArray1 will hold the 8-byte Enrypted number
		for (indx = 0;indx<8;indx++)
		{
			tmpArray[indx] = CRnd[indx];
		}

		if (m_radio3.GetCheck() == 1)
		{
			DES (tmpArray,tKey);
		}
		else
		{
			DES3 (tmpArray,tKey);
		}


		// Issue Authenticate command using 8-byte Encrypted No (tmpArray)
		//    and Random Terminal number (TRnd)
		for (indx = 0;indx<8;indx++)
		{
			tmpArray[indx+8] = TRnd[indx];
		}


		RecvLength = 0x00;
		//Authenticate Command
		SendBuff[0] = 0x80; //CLA
		SendBuff[1] = 0x82; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x10; //P3
		SendBuff[5] = tmpArray[0];
		SendBuff[6] = tmpArray[1];
		SendBuff[7] = tmpArray[2];
		SendBuff[8] = tmpArray[3];
		SendBuff[9] = tmpArray[4];
		SendBuff[10] = tmpArray[5];
		SendBuff[11] = tmpArray[6];
		SendBuff[12] = tmpArray[7];
		SendBuff[13] = tmpArray[8];
		SendBuff[14] = tmpArray[9];
		SendBuff[15] = tmpArray[10];
		SendBuff[16] = tmpArray[11];
		SendBuff[17] = tmpArray[12];
		SendBuff[18] = tmpArray[13];
		SendBuff[19] = tmpArray[14];
		SendBuff[20] = tmpArray[15];


		RecvLength = 0x0A;
		
		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			0x05 + 0x10,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error in Authentication!");
			m_ListBox.SetCurSel (i);

			return;
		}

	

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0] != 0x61) | (RecvBuff[1] != 0x08))
		{
			i = m_ListBox.AddString ("Error in Authentication!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Authentication!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0x08],RecvBuff[0x09]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);



		//Issue a GET RESPONSE COMMAND

		RecvLength = 0x00;
		SendBuff[0] = 0x80; //CLA
		SendBuff[1] = 0xC0; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x08; //P3
		RecvLength = 0x0A;
		
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

	

		//Retrieving the return code is it is a success or not

		if ((RecvBuff[0x08] != 0x90) | (RecvBuff[0x09] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Get Response!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		i = m_ListBox.AddString ("Success in Get Response!");
		m_ListBox.SetCurSel (i);
		sprintf(buff, "%02X %02X", RecvBuff[0x08],RecvBuff[0x09]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		for (indx = 0;indx<8;indx++)
		{
			tmpResult[indx] = RecvBuff[indx];
		}	
		
		//Get Card Key from Key Template
		m_edit1.GetWindowText(tmpStr, 100);
		for (indx = 0;indx <= (int)strlen((char*)tmpStr) - 1;indx++)
		{
			cKey[indx] = tmpStr[indx];
		}

		//Compute for Session Key

		if (m_radio3.GetCheck() == 1)
		{
			//for single DES
			// prepare SessionKey
			// SessionKey = DES (DES(RNDc, KC) XOR RNDt, KT)
			


			// calculate DES(cRnd,cKey)
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = CRnd[indx];
			}	
			DES (tmpArray, cKey);

			// XOR the result with tRnd
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = tmpArray[indx] ^= TRnd[indx];
			}	
			// DES the result with tKey
			DES (tmpArray, tKey);

			for (indx = 0;indx<8;indx++)
			{
				SessionKey[indx] = tmpArray[indx]; 
			}	

		}
		else
		{
			// 4.2b. for triple DES
			// prepare SessionKey
			// Left half SessionKey =  3DES (3DES (CRnd, cKey), tKey)
			// Right half SessionKey = 3DES (TRnd, REV (tKey))

		    // tmpArray1 = 3DES (CRnd, cKey)
			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = CRnd[indx];
			}	
			DES3 (tmpArray, cKey);

			DES3 (tmpArray, tKey);
			
			//Left Half of Session Key is done
			for (indx = 0;indx<8;indx++)
			{
				SessionKey[indx] = tmpArray[indx];
			}	

			// compute ReverseKey of tKey
			// just swap its left side with right side
			// ReverseKey = right half of tKey + left half of tKey

			for (indx = 0;indx<8;indx++)
			{
				ReverseKey[indx] = tKey[8 + indx];
			}	
			for (indx = 0;indx<8;indx++)
			{
				ReverseKey[8 + indx] = tKey[indx];
			}	

			// compute tmpArray = 3DES (TRnd, ReverseKey)

			for (indx = 0;indx<8;indx++)
			{
				tmpArray[indx] = TRnd[indx];
			}	
			DES3 (tmpArray, ReverseKey);
			
			//Right Half of Session Key is done
			for (indx = 0;indx<8;indx++)
			{
				SessionKey[indx + 8] = tmpArray[indx];
			}	

		}

		i = m_ListBox.AddString ("Success in Session Key!");
		m_ListBox.SetCurSel (i);

		for (indx = 0;indx<8;indx++)
		{
			tmpArray[indx] = TRnd[indx];
		}	
		
		if (m_radio3.GetCheck() == 1)
		{
			DES (tmpArray, SessionKey);
		}
		else
		{
			DES3 (tmpArray, SessionKey);
		}

		for (indx = 0;indx<8;indx++)
		{
			if (tmpResult[indx] != tmpArray[indx])
			{
				i = m_ListBox.AddString ("Mutual Authentication failed!");
				m_ListBox.SetCurSel (i);
				return;
			}
		}	

		i = m_ListBox.AddString ("Mutual Authentication is successful!");
		m_ListBox.SetCurSel (i);
		




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
	
	m_edit1.SetWindowText("");
	m_edit2.SetWindowText("");
	
	m_button2.EnableWindow(false);
	m_button4.EnableWindow(false);
	
}

void CACOSSampleCodesDlg::OnRadio4() 
{
	m_radio3.SetCheck(0);	
	m_radio4.SetCheck(1);
	m_edit1.SetLimitText(16);
	m_edit2.SetLimitText(16);
}

