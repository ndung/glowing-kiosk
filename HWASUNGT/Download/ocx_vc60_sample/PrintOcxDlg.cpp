// PrintOcxDlg.cpp : implementation file
//
#include <windows.h>


#include "stdafx.h"
#include "PrintOcx.h"
#include "PrintOcxDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CPrintOcxDlg dialog

CPrintOcxDlg::CPrintOcxDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPrintOcxDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPrintOcxDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CPrintOcxDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPrintOcxDlg)
	DDX_Control(pDX, IDC_HWAUSBCTRL1, m_test1);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPrintOcxDlg, CDialog)
	//{{AFX_MSG_MAP(CPrintOcxDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPrintOcxDlg message handlers

BOOL CPrintOcxDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CPrintOcxDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CPrintOcxDlg::OnOK() 
{
	long ret;
	LPCTSTR str;

// ¸ðµ¨¸í, printer model
//	str = "HMK-825";
	str = "HMK-060";
//	str = "HMK-054";


// OCX open
	ret=m_test1.Open(str);

	CString t;
	t.Format("%s", "OPEN SUCCESS!");
	if(ret==0) AfxMessageBox(t);
	else if(ret==-3){
		t.Format("%s", "OPEN FAILED!");
		AfxMessageBox(t);
	 }
}


void CPrintOcxDlg::OnButton1() 
{
	long ret;


// output string
	ret=m_test1.PrintStr("This is printer font!");

// output feed command
	ret=m_test1.PrintCmd(0x1b);
	ret=m_test1.PrintCmd(0x4a);
	ret=m_test1.PrintCmd(0xff);

// output cutting command
	ret=m_test1.PrintCmd(0x1b);
	ret=m_test1.PrintCmd(0x69);
	
}

void CPrintOcxDlg::OnButton2() 
{
	long ret;
	

// read 1byte
	ret=m_test1.RealRead();

	CString t;
	t.Format("%s", "READ FAILED!");

	// read failed	
	if(ret==-1){
		AfxMessageBox(t);
	}

    SetDlgItemInt(IDC_EDIT1, ret, TRUE);	

}

void CPrintOcxDlg::OnCancel() 
{
	
	m_test1.Close();
	CDialog::OnCancel();

}
