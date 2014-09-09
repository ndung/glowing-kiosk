//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              ConfigureATRDlg.cpp
//
//  Description:       This sample program outlines the steps on how to
//                     change the ATR of a smart card using the PC/SC platform.
//                     You can also change the Card Baud Rate and the Historical Bytes of the card.
//
//  NOTE:            Historical Bytes valid value must be 0 to 9 and A,B,C,D,E,F only. e.g.(11,99,AE,AA,FF etc)
//					 if historical byte is leave blank the profram will assume it as 00.
//					 After you update the ATR you have to initialize and connect to the device
//                   before you can see the updated ATR.
//
//Author:            Fernando G. Robles
//
//Date:              November 11, 2005
//
//Revision Trail:   (Date/Author/Description)

//=====================================================================

#include "stdafx.h"
#include "ConfigureATR.h"
#include "ConfigureATRDlg.h"

// ==========================================================================
// GetATR include file
#include "Winscard.h"

// GetATR GlobalVariables
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
							RecvLen;
	CmdBytes				apdu;
	BYTE					SendBuff[262],
							RecvBuff[262];
	SCARD_IO_REQUEST IO_REQ;
	unsigned char HByteArray[16];

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
// CConfigureATRDlg dialog

CConfigureATRDlg::CConfigureATRDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CConfigureATRDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CConfigureATRDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDI_ICON2);
}

void CConfigureATRDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CConfigureATRDlg)
	DDX_Control(pDX, IDC_EDIT17, edit17);
	DDX_Control(pDX, IDC_EDIT16, edit16);
	DDX_Control(pDX, IDC_EDIT15, edit15);
	DDX_Control(pDX, IDC_EDIT14, edit14);
	DDX_Control(pDX, IDC_EDIT13, edit13);
	DDX_Control(pDX, IDC_EDIT12, edit12);
	DDX_Control(pDX, IDC_EDIT11, edit11);
	DDX_Control(pDX, IDC_EDIT10, edit10);
	DDX_Control(pDX, IDC_EDIT9, edit9);
	DDX_Control(pDX, IDC_EDIT8, edit8);
	DDX_Control(pDX, IDC_EDIT7, edit7);
	DDX_Control(pDX, IDC_EDIT6, edit6);
	DDX_Control(pDX, IDC_EDIT5, edit5);
	DDX_Control(pDX, IDC_EDIT4, edit4);
	DDX_Control(pDX, IDC_EDIT3, edit3);
	DDX_Control(pDX, IDC_BUTTON5, bReset);
	DDX_Control(pDX, IDC_BUTTON4, bUpdate);
	DDX_Control(pDX, IDC_EDIT2, edit2);
	DDX_Control(pDX, IDC_EDIT1, edit1);
	DDX_Control(pDX, IDC_COMBO3, cbo_byte);
	DDX_Control(pDX, IDC_COMBO2, cbo_baud);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_LIST1, m_ListBox);
	DDX_Control(pDX, IDC_BUTTON3, bATR);
	DDX_Control(pDX, IDC_BUTTON2, bConnect);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CConfigureATRDlg, CDialog)
	//{{AFX_MSG_MAP(CConfigureATRDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	ON_EN_CHANGE(IDC_EDIT1, OnChangeEdit1)
	ON_EN_CHANGE(IDC_EDIT2, OnChangeEdit2)
	ON_CBN_EDITCHANGE(IDC_COMBO2, OnEditchangeCombo2)
	ON_CBN_SELCHANGE(IDC_COMBO2, OnSelchangeCombo2)
	ON_CBN_SELCHANGE(IDC_COMBO3, OnSelchangeCombo3)
	ON_CBN_EDITCHANGE(IDC_COMBO3, OnEditchangeCombo3)
	ON_BN_CLICKED(IDC_BUTTON5, OnButton5)
	ON_BN_CLICKED(IDC_BUTTON4, OnButton4)
	ON_EN_CHANGE(IDC_EDIT3, OnChangeEdit3)
	ON_EN_CHANGE(IDC_EDIT4, OnChangeEdit4)
	ON_EN_CHANGE(IDC_EDIT5, OnChangeEdit5)
	ON_EN_CHANGE(IDC_EDIT6, OnChangeEdit6)
	ON_EN_CHANGE(IDC_EDIT7, OnChangeEdit7)
	ON_EN_CHANGE(IDC_EDIT8, OnChangeEdit8)
	ON_EN_CHANGE(IDC_EDIT9, OnChangeEdit9)
	ON_EN_CHANGE(IDC_EDIT10, OnChangeEdit10)
	ON_EN_CHANGE(IDC_EDIT11, OnChangeEdit11)
	ON_EN_CHANGE(IDC_EDIT12, OnChangeEdit12)
	ON_EN_CHANGE(IDC_EDIT13, OnChangeEdit13)
	ON_EN_CHANGE(IDC_EDIT14, OnChangeEdit14)
	ON_EN_CHANGE(IDC_EDIT15, OnChangeEdit15)
	ON_EN_CHANGE(IDC_EDIT16, OnChangeEdit16)
	ON_EN_CHANGE(IDC_EDIT17, OnChangeEdit17)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CConfigureATRDlg message handlers


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


BOOL CConfigureATRDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	
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
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CConfigureATRDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CConfigureATRDlg::OnPaint() 
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
HCURSOR CConfigureATRDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CConfigureATRDlg::OnButton1() 
{
	int nLength = 64;
	int i;
	
  // 1. Initialize SC reader
  //  1.1. Establish context
 	retCode = SCardEstablishContext (SCARD_SCOPE_USER,
								NULL,
								NULL,
								&hContext);
	if (retCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString(GetScardErrMsg(retCode));
		m_ListBox.SetCurSel (i);
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
		i = m_ListBox.AddString(GetScardErrMsg(retCode));
		m_ListBox.SetCurSel (i);
        return;
	}
	if (readerName == NULL)
	{
		i = m_ListBox.AddString("No PC/SC reader is detected in your system.");
		m_ListBox.SetCurSel (i);

    	return;
	}
	
	cbReader.ResetContent();
	char *p = readerName;
	while (*p)
	{
    	for (int i=0;p[i];i++);
	      i++;
	    if (*p != 0)
		{
     		cbReader.AddString(p);
		}
		p = &p[i];
	}
	cbReader.SetCurSel(0);
	
	i = m_ListBox.AddString("Select reader, insert mcu card and connect.");
	m_ListBox.SetCurSel (i);

	bConnect.EnableWindow(TRUE);
	bInit.EnableWindow(FALSE);
	
}

void CConfigureATRDlg::OnButton2() 
{
	int i;
	DWORD Protocol = 1;
	char buff[100];
	char buff1[100];
	CString tmpStr;
	cbReader.GetLBText (cbReader.GetCurSel (), tmpStr);
	
	retCode = SCardConnect(hContext,
						tmpStr,
						SCARD_SHARE_SHARED,
						SCARD_PROTOCOL_T0|SCARD_PROTOCOL_T1,
						&hCard,
						&dwActProtocol);

	

	if (retCode != SCARD_S_SUCCESS)
	{	//Fail to Connect
		i = m_ListBox.AddString ("Unable to Connect to Card");
		m_ListBox.SetCurSel (i);

		return;
	}
	
	//Success in Connecting to Reader
//	IO_REQ.dwProtocol = Protocol;
//	IO_REQ.cbPciLength = sizeof (SCARD_IO_REQUEST);
	
	cbReader.GetLBText(cbReader.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Connection to ", buff1);
	
	i = m_ListBox.AddString(buff);
	m_ListBox.SetCurSel (i);
	bATR.EnableWindow(TRUE);	
}

void CConfigureATRDlg::OnButton3() 
{
	int indx, j, i;
	char ReaderName[100];
	DWORD ReaderLen = 100, ATRLen = 32, dwState;
    BYTE ATRVal[40];
	char tmpArray[150];
	char buff[100];
	char text2[1];

	//Get ATR Function

    retCode = SCardStatusA(hCard,
                            ReaderName,
                            &ReaderLen,
                            &dwState,
                            &dwActProtocol,
                            ATRVal,
                            &ATRLen);
	if (retCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Unable to Connect to Card");
		m_ListBox.SetCurSel (i);
		return;
	}
	for (indx = j = 0;indx < ATRLen; indx++)
	{
       sprintf(&tmpArray[j], "%02X ", ATRVal[indx]);
	   j += 3;
       
	}

	
	sprintf(buff,"%s %s", "Card ATR is: ", tmpArray);

	tmpArray[5] = 0;
	sprintf(text2,"%s",  &tmpArray[3]);
	edit2.SetWindowText(text2);
	
	tmpArray[8] = 0;
	sprintf(text2,"%s",  &tmpArray[6]);
	edit1.SetWindowText(text2);

	
	
	//edit1.SetWindowText(text1);
	i = m_ListBox.AddString(buff);
	m_ListBox.SetCurSel (i);
	cbo_baud.EnableWindow(true);
	cbo_byte.EnableWindow(true);
	bUpdate.EnableWindow(true);
	
	
}

void CConfigureATRDlg::OnChangeEdit1() 
{
	char buff[2];	
	
	edit1.GetWindowText(buff, 3);
	
	if (!memcmp(buff,"11",2))
	{
		cbo_baud.SetCurSel(0);
	}
	else
	
	if (!memcmp(buff,"92",2))
	{
		cbo_baud.SetCurSel(1);
	}
	else
	if (!memcmp(buff,"93",2))
	{
		cbo_baud.SetCurSel(2);
	}
	else
	if (!memcmp(buff,"94",2))
	{
		cbo_baud.SetCurSel(3);
	}
	else
	if (!memcmp(buff,"95",2))
	{
		cbo_baud.SetCurSel(4);
	}
	

}

void CConfigureATRDlg::OnChangeEdit2() 
{
	char buff[2];	
	
	edit2.GetWindowText(buff, 3);
	
	if (!memcmp(buff,"B0",2))
	{
		cbo_byte.SetCurSel(0);
	}
	else
	
	if (!memcmp(buff,"B1",2))
	{
		cbo_byte.SetCurSel(1);
	}
	else
	if (!memcmp(buff,"B2",2))
	{
		cbo_byte.SetCurSel(2);
	}
	else
	if (!memcmp(buff,"B3",2))
	{
		cbo_byte.SetCurSel(3);
	}
	else
	if (!memcmp(buff,"B4",2))
	{
		cbo_byte.SetCurSel(4);
	}
	else
	if (!memcmp(buff,"B5",2))
	{
		cbo_byte.SetCurSel(5);
	}
	else
	if (!memcmp(buff,"B6",2))
	{
		cbo_byte.SetCurSel(6);
	}
	else
	if (!memcmp(buff,"B7",2))
	{
		cbo_byte.SetCurSel(7);
	}
	else
	if (!memcmp(buff,"B8",2))
	{
		cbo_byte.SetCurSel(8);
	}
	else
	if (!memcmp(buff,"B9",2))
	{
		cbo_byte.SetCurSel(9);
	}
	else
	if (!memcmp(buff,"BA",2))
	{
		cbo_byte.SetCurSel(10);
	}
	else
	if (!memcmp(buff,"BB",2))
	{
		cbo_byte.SetCurSel(11);
	}
	else
	if (!memcmp(buff,"BC",2))
	{
		cbo_byte.SetCurSel(12);
	}
	else
	if (!memcmp(buff,"BD",2))
	{
		cbo_byte.SetCurSel(13);
	}
	else
	if (!memcmp(buff,"BF",2))
	{
		cbo_byte.SetCurSel(15);
	}
	else
	{
		if  (cbo_byte.GetCurSel() == 16)
			cbo_byte.SetCurSel(16);
		else
		if (!memcmp(buff,"BE",2))
			cbo_byte.SetCurSel(14);
	}


	edit3.SetWindowText("");
	edit4.SetWindowText("");
	edit5.SetWindowText("");
	edit6.SetWindowText("");
	edit7.SetWindowText("");
	edit8.SetWindowText("");
	edit9.SetWindowText("");
	edit10.SetWindowText("");
	edit11.SetWindowText("");
	edit12.SetWindowText("");
	edit13.SetWindowText("");
	edit14.SetWindowText("");
	edit15.SetWindowText("");
	edit16.SetWindowText("");
	edit17.SetWindowText("");

	edit3.EnableWindow(false);
	edit4.EnableWindow(false);
	edit5.EnableWindow(false);
	edit6.EnableWindow(false);
	edit7.EnableWindow(false);
	edit8.EnableWindow(false);
	edit9.EnableWindow(false);
	edit10.EnableWindow(false);
	edit11.EnableWindow(false);
	edit12.EnableWindow(false);
	edit13.EnableWindow(false);
	edit14.EnableWindow(false);
	edit15.EnableWindow(false);
	edit16.EnableWindow(false);
	edit17.EnableWindow(false);
	
	if (cbo_byte.GetCurSel() == 0)
	{
		edit3.EnableWindow(false);
		edit4.EnableWindow(false);
		edit5.EnableWindow(false);
		edit6.EnableWindow(false);
		edit7.EnableWindow(false);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 1)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(false);
		edit5.EnableWindow(false);
		edit6.EnableWindow(false);
		edit7.EnableWindow(false);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 2)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(false);
		edit6.EnableWindow(false);
		edit7.EnableWindow(false);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 3)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(false);
		edit7.EnableWindow(false);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 4)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(false);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 5)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 6)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 7)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 8)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 9)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 10)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(true);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 11)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(true);
		edit13.EnableWindow(true);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 12)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(true);
		edit13.EnableWindow(true);
		edit14.EnableWindow(true);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 13)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(true);
		edit13.EnableWindow(true);
		edit14.EnableWindow(true);
		edit15.EnableWindow(true);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 14)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(true);
		edit13.EnableWindow(true);
		edit14.EnableWindow(true);
		edit15.EnableWindow(true);
		edit16.EnableWindow(true);
		edit17.EnableWindow(false);
	}
	else
	if (cbo_byte.GetCurSel() == 15)
	{
		edit3.EnableWindow(true);
		edit4.EnableWindow(true);
		edit5.EnableWindow(true);
		edit6.EnableWindow(true);
		edit7.EnableWindow(true);
		edit8.EnableWindow(true);
		edit9.EnableWindow(true);
		edit10.EnableWindow(true);
		edit11.EnableWindow(true);
		edit12.EnableWindow(true);
		edit13.EnableWindow(true);
		edit14.EnableWindow(true);
		edit15.EnableWindow(true);
		edit16.EnableWindow(true);
		edit17.EnableWindow(true);
	}
	else
	if (cbo_byte.GetCurSel() == 16)
	{
		edit3.EnableWindow(false);
		edit4.EnableWindow(false);
		edit5.EnableWindow(false);
		edit6.EnableWindow(false);
		edit7.EnableWindow(false);
		edit8.EnableWindow(false);
		edit9.EnableWindow(false);
		edit10.EnableWindow(false);
		edit11.EnableWindow(false);
		edit12.EnableWindow(false);
		edit13.EnableWindow(false);
		edit14.EnableWindow(false);
		edit15.EnableWindow(false);
		edit16.EnableWindow(false);
		edit17.EnableWindow(false);
	}
	

	

}

void CConfigureATRDlg::OnEditchangeCombo2() 
{
	if (cbo_baud.GetCurSel() == 0)
		edit1.SetWindowText("11");
	else
		if (cbo_baud.GetCurSel() == 1)
		edit1.SetWindowText("92");
	else
		if (cbo_baud.GetCurSel() == 2)
		edit1.SetWindowText("93");
	else
		if (cbo_baud.GetCurSel() == 3)
		edit1.SetWindowText("94");
	else
		if (cbo_baud.GetCurSel() == 4)
		edit1.SetWindowText("95");
	
	
}

void CConfigureATRDlg::OnSelchangeCombo2() 
{
	if (cbo_baud.GetCurSel() == 0)
		edit1.SetWindowText("11");
	else
		if (cbo_baud.GetCurSel() == 1)
		edit1.SetWindowText("92");
	else
		if (cbo_baud.GetCurSel() == 2)
		edit1.SetWindowText("93");
	else
		if (cbo_baud.GetCurSel() == 3)
		edit1.SetWindowText("94");
	else
		if (cbo_baud.GetCurSel() == 4)
		edit1.SetWindowText("95");
	
}

void CConfigureATRDlg::OnSelchangeCombo3() 
{
	if (cbo_byte.GetCurSel() == 0)
		edit2.SetWindowText("B0");
	else
		if (cbo_byte.GetCurSel() == 1)
		edit2.SetWindowText("B1");
	else
		if (cbo_byte.GetCurSel() == 2)
		edit2.SetWindowText("B2");
	else
		if (cbo_byte.GetCurSel() == 3)
		edit2.SetWindowText("B3");
	else
		if (cbo_byte.GetCurSel() == 4)
		edit2.SetWindowText("B4");
	else
		if (cbo_byte.GetCurSel() == 5)
		edit2.SetWindowText("B5");
	else
		if (cbo_byte.GetCurSel() == 6)
		edit2.SetWindowText("B6");
	else
		if (cbo_byte.GetCurSel() == 7)
		edit2.SetWindowText("B7");
	else
		if (cbo_byte.GetCurSel() == 8)
		edit2.SetWindowText("B8");
	else
		if (cbo_byte.GetCurSel() == 9)
		edit2.SetWindowText("B9");
	else
		if (cbo_byte.GetCurSel() == 10)
		edit2.SetWindowText("BA");
	else
		if (cbo_byte.GetCurSel() == 11)
		edit2.SetWindowText("BB");
	else
		if (cbo_byte.GetCurSel() == 12)
		edit2.SetWindowText("BC");
	else
		if (cbo_byte.GetCurSel() == 13)
		edit2.SetWindowText("BD");
	else
		if (cbo_byte.GetCurSel() == 14)
		edit2.SetWindowText("BE");
	else
		if (cbo_byte.GetCurSel() == 15)
		edit2.SetWindowText("BF");
	else
		if (cbo_byte.GetCurSel() == 16)
		edit2.SetWindowText("BE");
	
	
}

void CConfigureATRDlg::OnEditchangeCombo3() 
{
	if (cbo_byte.GetCurSel() == 0)
		edit2.SetWindowText("B0");
	else
		if (cbo_byte.GetCurSel() == 1)
		edit2.SetWindowText("B1");
	else
		if (cbo_byte.GetCurSel() == 2)
		edit2.SetWindowText("B2");
	else
		if (cbo_byte.GetCurSel() == 3)
		edit2.SetWindowText("B3");
	else
		if (cbo_byte.GetCurSel() == 4)
		edit2.SetWindowText("B4");
	else
		if (cbo_byte.GetCurSel() == 5)
		edit2.SetWindowText("B5");
	else
		if (cbo_byte.GetCurSel() == 6)
		edit2.SetWindowText("B6");
	else
		if (cbo_byte.GetCurSel() == 7)
		edit2.SetWindowText("B7");
	else
		if (cbo_byte.GetCurSel() == 8)
		edit2.SetWindowText("B8");
	else
		if (cbo_byte.GetCurSel() == 9)
		edit2.SetWindowText("B9");
	else
		if (cbo_byte.GetCurSel() == 10)
		edit2.SetWindowText("BA");
	else
		if (cbo_byte.GetCurSel() == 11)
		edit2.SetWindowText("BB");
	else
		if (cbo_byte.GetCurSel() == 12)
		edit2.SetWindowText("BC");
	else
		if (cbo_byte.GetCurSel() == 13)
		edit2.SetWindowText("BD");
	else
		if (cbo_byte.GetCurSel() == 14)
		edit2.SetWindowText("BE");
	else
		if (cbo_byte.GetCurSel() == 15)
		edit2.SetWindowText("BF");
	else
		if (cbo_byte.GetCurSel() == 16)
		edit2.SetWindowText("BE");
}

void CConfigureATRDlg::OnButton5() 
{
	bConnect.EnableWindow(false);
	bInit.EnableWindow(true);
	bATR.EnableWindow(false);
    
	// Close SC reader
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);

	cbReader.ResetContent();
    cbReader.EnableWindow(true);
	cbo_baud.EnableWindow(false);
	cbo_byte.EnableWindow(false);
	bUpdate.EnableWindow(false);
	cbo_baud.SetCurSel(-1);
	cbo_byte.SetCurSel(-1);
	edit1.SetWindowText("");
	edit2.SetWindowText("");
}

void CConfigureATRDlg::OnButton4() 
{	
	char buff[100];
	int i,num_historical_byte,indx;
	char buff1[3];
	char buff2[3];
	DWORD RecvLength = 2;	
	DWORD Protocol = 1;
	

	ClearBuffers();
	//SELECT FILE FF07

	SendBuff[0] = 0x80; //CLA
	SendBuff[1] = 0xA4; //INS
	SendBuff[2] = 0x00; //P1
	SendBuff[3] = 0x00; //P2
	SendBuff[4] = 0x02; //P3

	SendBuff[5] = 0xFF; //File ID
	SendBuff[6] = 0x07; //File ID
	


	retCode = SCardTransmit(
		hCard,
		NULL,
		SendBuff,
		0x07,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (retCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Selecting FF07!");
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
		i = m_ListBox.AddString ("Error in Selecting FF07!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Selecting FF07!");
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


	retCode = SCardTransmit(
		hCard,
		NULL,
		SendBuff,
		0x0D,
		NULL,  
		RecvBuff,
		&RecvLength);

	if (retCode != SCARD_S_SUCCESS)
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

	num_historical_byte = cbo_byte.GetCurSel();
	
	
	edit1.GetWindowText(buff1,3);
	sscanf(buff1,"%02X",&buff2[0]);
	edit2.GetWindowText(buff1,3);
	sscanf(buff1,"%02X",&buff2[1]);

	

	//Updating the ATR (Speed, Historical bytes)

	SendBuff[0] = 0x80;        // CLA
    SendBuff[1] = 0xD2;        // INS
    SendBuff[2] = 0x00;        // Record No
    SendBuff[3] = 0x00;        // P2
    SendBuff[4] = 0x11;        // P3
	

	SendBuff[5] = buff2[0];
	SendBuff[6] = num_historical_byte;

	for (indx = 0;indx<=14;indx++)
    {
      
		SendBuff[indx+7] = HByteArray[indx];
      
	}
    
	retCode = SCardTransmit(
		hCard,
		NULL,
		SendBuff,
		0x16,
		NULL,  //   IN OUT LPSCARD_IO_REQUEST pioRecvPci,
		RecvBuff,
		&RecvLength);

	if (retCode != SCARD_S_SUCCESS)
	{
		i = m_ListBox.AddString ("Error in Updating FF07!");
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
		i = m_ListBox.AddString ("Error in Updating FF07!");
		m_ListBox.SetCurSel (i);
		
		sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
		i = m_ListBox.AddString (buff);
		m_ListBox.SetCurSel (i);

		return;
	}


	i = m_ListBox.AddString ("Success in Updating FF07!");
	m_ListBox.SetCurSel (i);
	sprintf(buff, "%02X %02X", RecvBuff[0],RecvBuff[1]);
	i = m_ListBox.AddString (buff);
	m_ListBox.SetCurSel (i);


	
}

void CConfigureATRDlg::OnChangeEdit3() 
{
	char buff[3];
	
	edit3.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[0]);
	
	
}

void CConfigureATRDlg::OnChangeEdit4() 
{
	char buff[3];
	
	edit4.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[1]);
	
}

void CConfigureATRDlg::OnChangeEdit5() 
{
	char buff[3];
	
	edit5.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[2]);	
}

void CConfigureATRDlg::OnChangeEdit6() 
{
	char buff[3];
	
	edit6.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[3]);
	
}

void CConfigureATRDlg::OnChangeEdit7() 
{
	char buff[3];
	
	edit7.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[4]);
	
}

void CConfigureATRDlg::OnChangeEdit8() 
{
	char buff[3];
	
	edit8.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[5]);
	
}

void CConfigureATRDlg::OnChangeEdit9() 
{
	char buff[3];
	
	edit9.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[6]);
	
}

void CConfigureATRDlg::OnChangeEdit10() 
{
	char buff[3];
	
	edit10.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[7]);
	
}

void CConfigureATRDlg::OnChangeEdit11() 
{
	char buff[3];
	
	edit11.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[8]);
	
}

void CConfigureATRDlg::OnChangeEdit12() 
{
	char buff[3];
	
	edit12.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[9]);
	
}

void CConfigureATRDlg::OnChangeEdit13() 
{
	char buff[3];
	
	edit13.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[10]);
	
}

void CConfigureATRDlg::OnChangeEdit14() 
{
	char buff[3];
	
	edit14.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[11]);	
}

void CConfigureATRDlg::OnChangeEdit15() 
{
	char buff[3];
	
	edit15.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[12]);	
}

void CConfigureATRDlg::OnChangeEdit16() 
{
	char buff[3];
	
	edit16.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[13]);
	
}

void CConfigureATRDlg::OnChangeEdit17() 
{
	char buff[3];
	
	edit17.GetWindowText(buff,3);
	sscanf(buff,"%02X",&HByteArray[14]);
	
}
