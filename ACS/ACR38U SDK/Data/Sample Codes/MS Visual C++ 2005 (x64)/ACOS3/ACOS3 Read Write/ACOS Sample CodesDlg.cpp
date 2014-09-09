//  Copyright(C):      Advanced Card Systems Ltd
//
//
//  Description:       This sample program mainly outlines the steps on how to perform
//                     Read, Write using an ACOS smart card under the PC/SC platform.,
//
//  Author:            Fernando G. Robles
//
//  Date:              Aug. 9, 2005
//
//  Revision Trail:   (Date/Author/Description)
//
//	June 23, 2008	Wazer Emmanuel R. Benal		Added the file access byte in writing to FF 04
//======================================================================

#include "stdafx.h"
#include "winscard.h"
#include "ACOS Sample Codes.h"
#include "ACOS Sample CodesDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif




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
void CACOSSampleCodesDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CACOSSampleCodesDlg)
	DDX_Control(pDX, IDC_BUTTON5, m_button5);
	DDX_Control(pDX, IDC_BUTTON6, m_button6);
	DDX_Control(pDX, IDC_EDIT1, m_edit1);
	DDX_Control(pDX, IDC_RADIO3, m_radio3);
	DDX_Control(pDX, IDC_RADIO2, m_radio2);
	DDX_Control(pDX, IDC_RADIO1, m_radio1);
	DDX_Control(pDX, IDC_BUTTON4, m_button4);
	DDX_Control(pDX, IDC_BUTTON3, m_button3);
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
	ON_BN_CLICKED(IDC_RADIO1, OnRadio1)
	ON_BN_CLICKED(IDC_RADIO2, OnRadio2)
	ON_BN_CLICKED(IDC_RADIO3, OnRadio3)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	ON_BN_CLICKED(IDC_BUTTON4, OnButton4)
	ON_BN_CLICKED(IDC_BUTTON6, OnButton6)
	ON_BN_CLICKED(IDC_BUTTON5, OnButton5)
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
	int indx;
    for (indx = 0;indx<263;indx++)
    {
      SendBuff[indx] = 0x00;
      RecvBuff[indx] = 0x00;
	}
}


void LoadReaderNames (CACOSSampleCodesDlg *p)
{
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

}

void CACOSSampleCodesDlg::OnButton2() 
{
	//The whole function simply format and set files to be written and read.

	char buff[100];
	int i;
	DWORD RecvLength = 2;	
	DWORD Protocol = 1;

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
	SendBuff[5] = 0x00;
	SendBuff[6] = 0x00;
	SendBuff[7] = 0x03;
	SendBuff[8] = 0x00;
    
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

	i = m_ListBox.AddString ("Card reset is successful!");
	m_ListBox.SetCurSel (i);

	

	ClearBuffers();
	//SELECT FILE FF04

	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0xA4; //INS
	SendBuff[2] = 0x00; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x02; //P3

	SendBuff[5] = 0xFF; //File ID
	SendBuff[6] = 0x04; //File ID
	SendBuff[7] = 0x00;
	


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
		i = m_ListBox.AddString ("Error in Selecting FF04!");
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
		i = m_ListBox.AddString ("Error in Selecting FF04!");
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

	// Write to FF 04
    //  Write to first record of FF 04.
    


	SendBuff[0] = 0x80;        // CLA
    SendBuff[1] = 0xD2;        // INS
    SendBuff[2] = 0x00;        // Record No
    SendBuff[3] = 0x00;        // P2
    SendBuff[4] = 0x06;        // P3
	SendBuff[5] = 0x0A;			//Record Length
	SendBuff[6] = 0x03;			//No. of Records
	SendBuff[7] = 0x00;			//Read security attribute
	SendBuff[8] = 0x00;			//Write security attribute
	SendBuff[9] = 0xAA;			//File ID
	SendBuff[10] = 0x11;		//File ID
	SendBuff[11] = 0x00;		//File Access Byte

	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x0B,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Defining User File AA 11!");
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
		i = m_ListBox.AddString ("Error in Defining User File AA 11!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Defining User File AA 11!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	//  Write to second record of FF 04.
    
	SendBuff[0] = 0x80;        // CLA
    SendBuff[1] = 0xD2;        // INS
    SendBuff[2] = 0x01;       // Record No
    SendBuff[3] = 0x00;        // P2
    SendBuff[4] = 0x06;        // P3
	SendBuff[5] = 0x10;			//Record Length
	SendBuff[6] = 0x02;			//No. of Records
	SendBuff[7] = 0x00;			//Read security attribute
	SendBuff[8] = 0x00;			//Write security attribute
	SendBuff[9] = 0xBB;			//File ID
	SendBuff[10] = 0x22;			//File ID
	SendBuff[11] = 0x00;		//File Access Byte
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x0B,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Defining User File BB 22!");
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
		i = m_ListBox.AddString ("Error in Defining User File BB 22!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Defining User File BB 22!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);



		//  Write to third  record of FF 04.
    
	SendBuff[0] = 0x80;        // CLA
    SendBuff[1] = 0xD2;        // INS
    SendBuff[2] = 0x02;      // Record No
    SendBuff[3] = 0x00;        // P2
    SendBuff[4] = 0x06;        // P3
	SendBuff[5] = 0x20;			//Record Length
	SendBuff[6] = 0x04;			//No. of Records
	SendBuff[7] = 0x00;			//Read security attribute
	SendBuff[8] = 0x00;			//Write security attribute
	SendBuff[9] = 0xCC;			//File ID
	SendBuff[10] = 0x33;			//File ID
	SendBuff[11] = 0x00;		//File Access Byte
	
	RetCode = SCardTransmit(
		hCard,
		&IO_REQ,
		SendBuff,
		0x0B,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (RetCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Defining User File CC 33!");
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
		i = m_ListBox.AddString ("Error in Defining User File CC 33!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Defining User File CC 33!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);

	m_radio1.EnableWindow(true);	
	m_radio2.EnableWindow(true);
	m_radio3.EnableWindow(true);

	m_radio1.SetCheck(1);
	m_radio2.SetCheck(0);
	m_radio3.SetCheck(0);
	
	m_edit1.EnableWindow(true);
	m_edit1.SetLimitText(10);

	m_button3.EnableWindow(true);
	m_button4.EnableWindow(true);





}

void CACOSSampleCodesDlg::OnRadio1() 
{
	m_radio1.SetCheck(1);
	m_radio2.SetCheck(0);	
	m_radio3.SetCheck(0);	
	m_edit1.SetWindowText("");
	m_edit1.SetLimitText(10);

}

void CACOSSampleCodesDlg::OnRadio2() 
{
	m_radio1.SetCheck(0);
	m_radio2.SetCheck(1);	
	m_radio3.SetCheck(0);	
	m_edit1.SetWindowText("");
	m_edit1.SetLimitText(16);
}

void CACOSSampleCodesDlg::OnRadio3() 
{
	m_radio1.SetCheck(0);
	m_radio2.SetCheck(0);	
	m_radio3.SetCheck(1);
	m_edit1.SetWindowText("");
	m_edit1.SetLimitText(32);
}

void CACOSSampleCodesDlg::OnButton3() 
{
	char buff[100];
	char buff1[100];
	int i;
	DWORD RecvLength = 2;	

	if (m_radio1.GetCheck() == 1)	
	{

		//Select AA 11 first before Reading into it.
		ClearBuffers();

		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xA4; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x02; //P3
		
		SendBuff[5] = 0xAA; //DF File ID
		SendBuff[6] = 0x11; //DF File ID


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
			i = m_ListBox.AddString ("Error in Selecting AA 11!");
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

		
		if ((RecvBuff[0] != 0x91) )
		{
			i = m_ListBox.AddString ("Error in Selecting AA 11!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		//Read Record on File AA 11
		ClearBuffers();
		RecvLength = 0x00;
		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xB2; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x0A; //P3
		
		RecvLength = SendBuff[4] + 0x02;

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
			i = m_ListBox.AddString ("Error in Reading data from AA 11!");
			m_ListBox.SetCurSel (i);

			return;
		}

	
		//Retrieving the return code is it is a success or not

		
		if ((RecvBuff[0x0A] != 0x90) | (RecvBuff[0x0B] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Reading data from AA 11!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		RecvBuff[0x0A]=0x00;
		strcpy(buff,(char*)RecvBuff); 

	
		sprintf(buff1, "%s %s", "Read Data from AA 11: ", buff);
		i = m_ListBox.AddString (buff1);
		m_ListBox.SetCurSel (i);





		
	}
	else
	if (m_radio2.GetCheck() == 1)
	{

		ClearBuffers();
		
		//Select BB 22 first before Reading into it.

		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xA4; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x02; //P3
		
		SendBuff[5] = 0xBB; //DF File ID
		SendBuff[6] = 0x22; //DF File ID


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
			i = m_ListBox.AddString ("Error in Selecting BB 22!");
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

		
		if ((RecvBuff[0] != 0x91) )
		{
			i = m_ListBox.AddString ("Error in Selecting BB 22!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		//Read Record on File BB 22
		ClearBuffers();
		RecvLength = 0x00;
		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xB2; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x10; //P3
		
		RecvLength = SendBuff[4] + 0x02;

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
			i = m_ListBox.AddString ("Error in Reading data from BB 22!");
			m_ListBox.SetCurSel (i);

			return;
		}

	
		//Retrieving the return code is it is a success or not

		
		if ((RecvBuff[0x10] != 0x90) | (RecvBuff[0x11] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Reading data from BB 22!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		RecvBuff[0x10]=0x00;
		strcpy(buff,(char*)RecvBuff); 

	
		sprintf(buff1, "%s %s", "Read Data from BB 22: ", buff);
		i = m_ListBox.AddString (buff1);
		m_ListBox.SetCurSel (i);


	}
	else
	if (m_radio3.GetCheck() == 1)
	{
		ClearBuffers();
		
		//Select CC 33 first before Reading into it.

		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xA4; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x02; //P3
		
		SendBuff[5] = 0xCC; //DF File ID
		SendBuff[6] = 0x33; //DF File ID


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
			i = m_ListBox.AddString ("Error in Selecting CC 33!");
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

		
		if ((RecvBuff[0] != 0x91) )
		{
			i = m_ListBox.AddString ("Error in Selecting CC 33!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}


		//Read Record on File CC 33
		ClearBuffers();
		RecvLength = 0x00;
		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xB2; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x20; //P3
		
		RecvLength = SendBuff[4] + 0x02;

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
			i = m_ListBox.AddString ("Error in Reading data from CC 33!");
			m_ListBox.SetCurSel (i);

			return;
		}

	
		//Retrieving the return code is it is a success or not

		
		if ((RecvBuff[0x20] != 0x90) | (RecvBuff[0x21] != 0x00))
		{
			i = m_ListBox.AddString ("Error in Reading data from CC 33!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		RecvBuff[0x20]=0x00;
		strcpy(buff,(char*)RecvBuff); 

	
		sprintf(buff1, "%s %s", "Read Data from CC 33: ", buff);
		i = m_ListBox.AddString (buff1);
		m_ListBox.SetCurSel (i);

	}

}

void CACOSSampleCodesDlg::OnButton4() 
{
	char buff[100];
	char buff1[100];
	int i, ctr;
	DWORD RecvLength = 2;	
	

	m_edit1.GetWindowText(buff, 100);
	
	if (buff[0]==0 )
	{
		i = m_ListBox.AddString ("Invalid Input!");
		m_ListBox.SetCurSel (i);
		return;
	}

	if (m_radio1.GetCheck() == 1)	
	{	
		ClearBuffers();

		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xA4; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x02; //P3
		
		SendBuff[5] = 0xAA; //DF File ID
		SendBuff[6] = 0x11; //DF File ID


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
			i = m_ListBox.AddString ("Error in Selecting AA 11!");
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

		
		if ((RecvBuff[0] != 0x91) )
		{
			i = m_ListBox.AddString ("Error in Selecting AA 11!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}

		m_edit1.GetWindowText(buff, 100);
		
		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            i = i + 1;

		}

		SendBuff[0] = 0x80; //CLA
		SendBuff[1] = 0xD2; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = (strlen(buff) ); //P3

		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			SendBuff[4] + 0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);
		

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error on Writing data to AA 11!");
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
			i = m_ListBox.AddString ("Error on Writing data to AA 11!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff1, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff1);
			m_ListBox.SetCurSel (i);

			return;
		}

		sprintf(buff1,"%s %s", "Write Data: ", buff);
		i = m_ListBox.AddString(buff1);
		m_ListBox.SetCurSel (i);

		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);



	}
	else
	if (m_radio2.GetCheck() == 1)	
	{
		ClearBuffers();

		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xA4; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x02; //P3
		
		SendBuff[5] = 0xBB; //DF File ID
		SendBuff[6] = 0x22; //DF File ID


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
			i = m_ListBox.AddString ("Error in Selecting BB 22!");
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

		
		if ((RecvBuff[0] != 0x91) )
		{
			i = m_ListBox.AddString ("Error in Selecting BB 22!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}
		m_edit1.GetWindowText(buff, 100);

		
		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            i = i + 1;

		}

		SendBuff[0] = 0x80; //CLA
		SendBuff[1] = 0xD2; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = (strlen(buff) ); //P3

		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			SendBuff[4] + 0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);
		

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error on Writing data to BB 22!");
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
			i = m_ListBox.AddString ("Error on Writing data to BB 22!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff1, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff1);
			m_ListBox.SetCurSel (i);

			return;
		}

		sprintf(buff1,"%s %s", "Write Data: ", buff);
		i = m_ListBox.AddString(buff1);
		m_ListBox.SetCurSel (i);

		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

	}
	else
	if (m_radio3.GetCheck() == 1)	
	{
		ClearBuffers();

		SendBuff[0] = 0x80; //CLA 
		SendBuff[1] = 0xA4; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = 0x02; //P3
		
		SendBuff[5] = 0xCC; //DF File ID
		SendBuff[6] = 0x33; //DF File ID


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
			i = m_ListBox.AddString ("Error in Selecting CC 33!");
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

		
		if ((RecvBuff[0] != 0x91) )
		{
			i = m_ListBox.AddString ("Error in Selecting CC 33!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff);
			m_ListBox.SetCurSel (i);

			return;
		}
		m_edit1.GetWindowText(buff, 100);

		i = 5;
		for (ctr = 0; ctr <= (int)strlen((char*)buff) - 1; ctr++)
		{
        
            SendBuff[i] = buff[ctr];
            i = i + 1;

		}

		SendBuff[0] = 0x80; //CLA
		SendBuff[1] = 0xD2; //INS
		SendBuff[2] = 0x00; //P1
		SendBuff[3] = 0x00; //P2
		SendBuff[4] = (strlen(buff) ); //P3

		RetCode = SCardTransmit(
			hCard,
			&IO_REQ,
			SendBuff,
			SendBuff[4] + 0x05,
			NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
			RecvBuff,
			&RecvLength);
		

		if (RetCode != SCARD_S_SUCCESS)
		{
			i = m_ListBox.AddString ("Error on Writing data to CC 33!");
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
			i = m_ListBox.AddString ("Error on Writing data to CC 33!");
			m_ListBox.SetCurSel (i);
			
			sprintf(buff1, "%02X %02X", RecvBuff[0],RecvBuff[1]);
			i = m_ListBox.AddString (buff1);
			m_ListBox.SetCurSel (i);

			return;
		}

		sprintf(buff1,"%s %s", "Write Data: ", buff);
		i = m_ListBox.AddString(buff1);
		m_ListBox.SetCurSel (i);

		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);
		
	}
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

	m_radio1.EnableWindow(false);	
	m_radio2.EnableWindow(false);
	m_radio3.EnableWindow(false);

	m_radio1.SetCheck(0);
	m_radio2.SetCheck(0);
	m_radio3.SetCheck(0);
	
	m_edit1.EnableWindow(false);
	
	m_button2.EnableWindow(false);
	m_button3.EnableWindow(false);
	m_button4.EnableWindow(false);
	m_edit1.SetWindowText("");
}
