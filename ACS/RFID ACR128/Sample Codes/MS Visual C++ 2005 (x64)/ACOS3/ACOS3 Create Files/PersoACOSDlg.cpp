/////////////////////////////////////////////////////////////////////////////
//
//	COMPANY : Advanced Card Systems, Ltd.
//
//  AUTHOR  : Alcendor Lorzano Chan
//
//	CREATED	: 07 / 11 / 2001
//
// NOTE : PCSC components and the reader driver for PCSC must be installed 
//		  first before this program will run successfully. If program does 
//		  not run the first time, pull out the card and run the program again.
//		  A similar display is shown in the output window when debugging.	
//
//	REVISION TRAIL
//
//	June 23, 2008	Wazer Emmanuel R. Benal  -	Added the file access byte in writing to FF 04
/////////////////////////////////////////////////////////////////////////////


#include "stdafx.h"
#include "PersoACOS.h"
#include "PersoACOSDlg.h"

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
// CPersoACOSDlg dialog

CPersoACOSDlg::CPersoACOSDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPersoACOSDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPersoACOSDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CPersoACOSDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPersoACOSDlg)
	DDX_Control(pDX, IDC_BUTTON3, m_button4);
	DDX_Control(pDX, IDC_BTNPersonalize, m_button3);
	DDX_Control(pDX, IDC_BUTTON2, m_button2);
	DDX_Control(pDX, IDC_BUTTON1, m_button1);
	DDX_Control(pDX, IDC_LIST2, m_Output);
	DDX_Control(pDX, IDC_COMBO1, m_Port);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPersoACOSDlg, CDialog)
	//{{AFX_MSG_MAP(CPersoACOSDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BTNPersonalize, OnBTNPersonalize)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPersoACOSDlg message handlers

BOOL CPersoACOSDlg::OnInitDialog()
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

/*	LONG lReturn=0;

	lReturn = SCardEstablishContext(SCARD_SCOPE_USER,
							  NULL,
							  NULL,
							  &m_hContext);
		
	if (lReturn != SCARD_S_SUCCESS)
	{
		m_Output.AddString("SCardEstablishContext Failed!");
	}	
	else 
	{
		DWORD	  dwReadersLen=MAX_PATH;
		memset(m_sBuffer,'\0',MAX_PATH);
		SCardListReaders(m_hContext,NULL,(LPTSTR)&m_sBuffer,&dwReadersLen);

		if (dwReadersLen)
		{
			CString		sReader;
			char	    *pChar = NULL;	
			pChar =	 m_sBuffer;
			while (*pChar != NULL)
			{
				sReader = "";
				while(*pChar != NULL)
				{
					sReader = sReader + *pChar;
					pChar++;
				} // while do
				pChar++;
				m_Port.AddString(sReader);
			} // while do
			m_Port.SetCurSel(0);
		}
		else 
		{
			m_Output.AddString("No readers found. Possible that PCSC is not installed.");
		}
		SCardReleaseContext(m_hContext);
	}

	return TRUE;  // return TRUE  unless you set the focus to a control */
	//m_Output.AddString("Program Ready.");
	
	return TRUE;
}

void CPersoACOSDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CPersoACOSDlg::OnPaint() 
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
HCURSOR CPersoACOSDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CPersoACOSDlg::OnOK() 
{
	// TODO: Add extra validation here

	CDialog::OnOK();
}

void CPersoACOSDlg::OnDestroy() 
{
	CDialog::OnDestroy();
	// TODO: Add your message handler code here
	m_Port.ResetContent();   // Clear contents of the port ddlistbox
	SCardReleaseContext(m_hContext);  // Release context
}

void CPersoACOSDlg::OnBTNPersonalize() 
{

	int i;
	CString		sReader;
	LONG		lReturn = 0;
	BYTE		SendBuff[256 + 5];
	BYTE		RecvBuff[256 + 2];
	DWORD		dwSend = 0;
	DWORD		dwRecv = 0;
	
	DWORD		dwProtocol = SCARD_PROTOCOL_T0;
	DWORD		dwState = SCARD_RESET_CARD;
	DWORD		dwReaderLen = MAX_PATH;
	DWORD		ATRLen = SCARD_ATR_LENGTH;
    
	char		sReaderBuff[MAX_PATH];		
	BYTE		AppCODE5[8] = {0,0,0,0,0,0,0,0};
	BYTE		AppCODE4[8] = {0,0,0,0,0,0,0,0};
	BYTE		bATR[SCARD_ATR_LENGTH] = {0,0,0,0,0,0,0,0,0,0,
										  0,0,0,0,0,0,0,0,0,0,
										  0,0,0,0,0,0,0,0,0,0,0,0,0};


	

	m_Output.AddString("=======START=======");
	m_Output.AddString("");



	// Submit IC code
	SendBuff[0] = 0x80;
	SendBuff[1] = 0x20;
	SendBuff[2] = 0x07;	// Index for IC code "ACOSTEST" (41 43 4F 53 54 45 53 54)
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x08;

	SendBuff[5] = 0x41; // 'A'
	SendBuff[6]	= 0x43; // 'C'
	SendBuff[7]	= 0x4F; // 'O'
	SendBuff[8]	= 0x53; // 'S'
	SendBuff[9]	= 0x54; // 'T'
	SendBuff[10] = 0x45;// 'E'
	SendBuff[11] = 0x53;// 'S'
	SendBuff[12] = 0x54;// 'T'
	dwSend = 5 + 8; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;


	// Select Personalization File FF 02
	SendBuff[0] = 0x80;
	SendBuff[1] = 0xA4;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x02;

	SendBuff[5] = 0xFF;
	SendBuff[6] = 0x02;
	dwSend = 5 + 2; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;


	//   Write to FF 02
    //   This will create 3 User files, no Option registers and
    //   Security Option registers defined, Personalization bit
    //   is not set

	SendBuff[0] = 0x80;
	SendBuff[1] = 0xD2;
	SendBuff[2] = 0x00;	
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x04;
	
	SendBuff[5] = 0x00;
	SendBuff[6] = 0x00;
	SendBuff[7] = 0x03;
	SendBuff[8] = 0x00;

	dwSend = 9; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;
	m_Output.AddString("FF 02 is updated!");


	//Perform a reset for changes in the ACOS to take effect
	
	lReturn = SCardDisconnect(m_hCard,SCARD_RESET_CARD);

	m_Port.GetLBText(m_Port.GetCurSel(),sReader);

	memset(sReaderBuff,NULL,MAX_PATH);
	memcpy(sReaderBuff,sReader,sReader.GetLength()+1);

	lReturn = SCardConnect(m_hContext,
						   sReaderBuff,
						   SCARD_SHARE_EXCLUSIVE,
						   SCARD_PROTOCOL_T0,
						   &m_hCard,
						   &dwProtocol);


	if (lReturn != SCARD_S_SUCCESS)
	{	//Fail to Connect
		i = m_Output.AddString ("Card reset is failed!");
		m_Output.SetCurSel (i);

		return;
	}

	i = m_Output.AddString ("Card reset is successful!");
	m_Output.SetCurSel (i);

	//Select FF 04
	SendBuff[0] = 0x80;
	SendBuff[1] = 0xA4;
	SendBuff[2] = 0x00;	
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x02;

	SendBuff[5] = 0xFF; 
	SendBuff[6] = 0x04;
	dwSend = 5 + 2; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;


	// Submit IC code
	SendBuff[0] = 0x80;
	SendBuff[1] = 0x20;
	SendBuff[2] = 0x07;	// Index for IC code "ACOSTEST" (41 43 4F 53 54 45 53 54)
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x08;

	SendBuff[5] = 0x41; // 'A'
	SendBuff[6]	= 0x43; // 'C'
	SendBuff[7]	= 0x4F; // 'O'
	SendBuff[8]	= 0x53; // 'S'
	SendBuff[9]	= 0x54; // 'T'
	SendBuff[10] = 0x45;// 'E'
	SendBuff[11] = 0x53;// 'S'
	SendBuff[12] = 0x54;// 'T'
	dwSend = 5 + 8; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;

	// Write to FF 04
	// Write to first record of FF 04
	SendBuff[0] = 0x80;
	SendBuff[1] = 0xD2;
	SendBuff[2] = 0x00;	
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x06;

	SendBuff[5] = 0x05; 
	SendBuff[6] = 0x03; 
	SendBuff[7] = 0x00; 
	SendBuff[8] = 0x00; 
	SendBuff[9] = 0xAA; 
	SendBuff[10] = 0x11; 
	SendBuff[11] = 0x00;
	dwSend = 11; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_Output.AddString ("Error in Defining User File AA 11!");
		m_Output.SetCurSel (i);
		return;
	}
	else
	{
		i = m_Output.AddString ("User File AA 11 is defined!");
		m_Output.SetCurSel (i);
		
	}


	//Write to second record of FF 04

	SendBuff[0] = 0x80;
	SendBuff[1] = 0xD2;
	SendBuff[2] = 0x01;	
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x06;

	SendBuff[5] = 0x0A; 
	SendBuff[6] = 0x02; 
	SendBuff[7] = 0x00; 
	SendBuff[8] = 0x00; 
	SendBuff[9] = 0xBB; 
	SendBuff[10] = 0x22; 
	SendBuff[11] = 0x00;
	dwSend = 11; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_Output.AddString ("Error in Defining User File BB 22!");
		m_Output.SetCurSel (i);
		return;
	}
	else
	{
		i = m_Output.AddString ("User File BB 22 is defined!");
		m_Output.SetCurSel (i);
		
	}

	
	// Write to third record of FF 04
	SendBuff[0] = 0x80;
	SendBuff[1] = 0xD2;
	SendBuff[2] = 0x02;	
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x06;

	SendBuff[5] = 0x06; 
	SendBuff[6] = 0x04; 
	SendBuff[7] = 0x00; 
	SendBuff[8] = 0x00; 
	SendBuff[9] = 0xCC; 
	SendBuff[10] = 0x33; 
	SendBuff[11] = 0x00;
	dwSend = 11; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;

	if ((RecvBuff[0] != 0x90) | (RecvBuff[1] != 0x00))
	{
		i = m_Output.AddString ("Error in Defining User File CC 33!");
		m_Output.SetCurSel (i);
		return;
	}
	else
	{
		i = m_Output.AddString ("User File CC 33 is defined!");
		m_Output.SetCurSel (i);
		
	}

	// Select 3 User Files created previously for validation
  
	
	//Select User File AA 11

	SendBuff[0] = 0x80;
	SendBuff[1] = 0xA4;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x02;

	SendBuff[5] = 0xAA;
	SendBuff[6] = 0x11;
	dwSend = 5 + 2; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;
	if ((RecvBuff[0] != 0x91) | (RecvBuff[1] != 0x00))
	{
		i = m_Output.AddString ("Fail in Selecting AA 11!");
		m_Output.SetCurSel (i);
		return;
	}
	else
	{
		i = m_Output.AddString ("User File AA 11 is selected!");
		m_Output.SetCurSel (i);
		
	}


	//Select User File BB 22

	SendBuff[0] = 0x80;
	SendBuff[1] = 0xA4;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x02;

	SendBuff[5] = 0xBB;
	SendBuff[6] = 0x22;
	dwSend = 5 + 2; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;
	if ((RecvBuff[0] != 0x91) | (RecvBuff[1] != 0x01))
	{
		i = m_Output.AddString ("Fail in Selecting BB 22!");
		m_Output.SetCurSel (i);
		return;
	}
	else
	{
		i = m_Output.AddString ("User File BB 22 is selected!");
		m_Output.SetCurSel (i);
		
	}


	//Select User File CC 33

	SendBuff[0] = 0x80;
	SendBuff[1] = 0xA4;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x02;

	SendBuff[5] = 0xCC;
	SendBuff[6] = 0x33;
	dwSend = 5 + 2; 
	dwRecv = 2; 
	
	ExchangeData(SendBuff,dwSend,RecvBuff,dwRecv); // Send to Card;
	if ((RecvBuff[0] != 0x91) | (RecvBuff[1] != 0x02))
	{
		i = m_Output.AddString ("Fail in Selecting CC 33!");
		m_Output.SetCurSel (i);
		return;
	}
	else
	{
		i = m_Output.AddString ("User File CC 33 is selected!");
		m_Output.SetCurSel (i);
		
	}


	m_Output.AddString("=======END=======");

}




void CPersoACOSDlg::ExchangeData(BYTE *SendBuff,DWORD nSend,BYTE *RecvBuff,DWORD &nRecv)
{

	SCARD_IO_REQUEST	 SendIo = {SCARD_PROTOCOL_T0,sizeof(SCARD_IO_REQUEST)};
	SCARD_IO_REQUEST	 RecvIo = {SCARD_PROTOCOL_T0,sizeof(SCARD_IO_REQUEST)};
	LONG	lReturn =0;
	DWORD	dwIndex=0;


	lReturn = SCardTransmit(m_hCard,&SendIo,SendBuff,nSend,&RecvIo,RecvBuff,&nRecv);
	
	CString  sTemp;
	CString  sAccm;
	if (lReturn != SCARD_S_SUCCESS)
	{
		
		sTemp.Format("SCardTransmit Error ( %08X )",lReturn);
		m_Output.AddString(sTemp);
	}
	else
	{
		sAccm = "";
		for (dwIndex = 0; dwIndex < nSend; dwIndex++)
		{
			sTemp = "";
			sTemp.Format("%02X ",SendBuff[dwIndex]);
			sAccm = sAccm + sTemp; 
		}// for loop

		m_Output.AddString("< " + sAccm);
		TRACE ("< %s\n",sAccm);

		sAccm = "";
		for (dwIndex = 0; dwIndex < nRecv - 2; dwIndex++)
		{
			sTemp = "";
			sTemp.Format("%02X ",RecvBuff[dwIndex]);
			sAccm = sAccm + sTemp; 
		}// for loop
		if (nRecv - 2) m_Output.AddString("> "+ sAccm);
		sAccm.Format("%02X %02X (status)",RecvBuff[nRecv-2],RecvBuff[nRecv-1]);
		m_Output.AddString("> "+ sAccm );
		TRACE ("< %s\n",sAccm);
		//m_Output.AddString("");
		TRACE ("\n",sAccm);
	}
}


void CPersoACOSDlg::OnButton1() 
{
	LONG lReturn=0;
	int i;

	lReturn = SCardEstablishContext(SCARD_SCOPE_USER,
							  NULL,
							  NULL,
							  &m_hContext);
		
	if (lReturn != SCARD_S_SUCCESS)
	{
		m_Output.AddString("SCardEstablishContext Failed!");
	}	
	else 
	{
		DWORD	  dwReadersLen=MAX_PATH;
		memset(m_sBuffer,'\0',MAX_PATH);
		SCardListReaders(m_hContext,NULL,(LPTSTR)&m_sBuffer,&dwReadersLen);

		if (dwReadersLen)
		{
			CString		sReader;
			char	    *pChar = NULL;	
			pChar =	 m_sBuffer;
			while (*pChar != NULL)
			{
				sReader = "";
				while(*pChar != NULL)
				{
					sReader = sReader + *pChar;
					pChar++;
				} // while do
				pChar++;
				m_Port.AddString(sReader);
			} // while do
			m_Port.SetCurSel(0);
		}
		else 
		{
			m_Output.AddString("No readers found. Possible that PCSC is not installed.");
		}
		SCardReleaseContext(m_hContext);
		i = m_Output.AddString ("Program Ready");
		m_Output.SetCurSel (i);
		m_button2.EnableWindow(true);
	}

	// return TRUE  unless you set the focus to a control	
}

void CPersoACOSDlg::OnButton2() 
{	
	CString		sReader;
	DWORD		dwProtocol = SCARD_PROTOCOL_T0;
	char		sReaderBuff[MAX_PATH];	
	LONG		lReturn = 0;

	int i;
	char buff[100];
	char buff1[100];

	// Reset Output buffer incase user presses Personalize again.


	lReturn = SCardEstablishContext(SCARD_SCOPE_USER,
								  NULL,
								  NULL,
								  &m_hContext);
	
	m_Port.GetLBText(m_Port.GetCurSel(),sReader);

	memset(sReaderBuff,NULL,MAX_PATH);
	memcpy(sReaderBuff,sReader,sReader.GetLength()+1);

	lReturn = SCardConnect(m_hContext,
						   sReaderBuff,
						   SCARD_SHARE_EXCLUSIVE,
						   SCARD_PROTOCOL_T0,
						   &m_hCard,
						   &dwProtocol);
	if (lReturn != SCARD_S_SUCCESS)
	{
	
		sReader.Format("Connect Failed! (%08X) Terminating program.",lReturn);
		m_Output.AddString(sReader);
		m_Output.AddString("Check if the correct port was selected.");
		return;
	}
	else
	{
		m_Port.GetLBText(m_Port.GetCurSel(),buff1);
		sprintf(buff,"%s %s", "Successful Connection to ", buff1);
		i = m_Output.AddString(buff);
		m_Output.SetCurSel (i);
		m_button3.EnableWindow(true);

	}	
}

void CPersoACOSDlg::OnButton3() 
{
	long lReturn;
	int i;

	lReturn = SCardDisconnect(m_hCard,SCARD_UNPOWER_CARD);
	
	i = m_Output.AddString("Reader is Disconnected Successfully!");
	m_Output.SetCurSel (i);

	m_button2.EnableWindow(false);
	m_button3.EnableWindow(false);
		
}
