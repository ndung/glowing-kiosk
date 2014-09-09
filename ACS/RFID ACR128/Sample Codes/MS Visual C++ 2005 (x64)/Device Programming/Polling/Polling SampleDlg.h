// Polling SampleDlg.h : header file
//

#if !defined(AFX_POLLINGSAMPLEDLG_H__57351089_48B9_4358_83F7_11F560887D97__INCLUDED_)
#define AFX_POLLINGSAMPLEDLG_H__57351089_48B9_4358_83F7_11F560887D97__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CPollingSampleDlg dialog

class CPollingSampleDlg : public CDialog
{
// Construction
public:
	CPollingSampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPollingSampleDlg)
	enum { IDD = IDD_POLLINGSAMPLE_DIALOG };
	CButton	btnPICC;
	CButton	btnICC;
	CRichEditCtrl	rbResult;
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CButton	btnAuto;
	CButton	btnManual;
	CButton	btnSetPoll;
	CButton	btnReadPoll;
	CButton	rPoll25;
	CButton	rPoll1;
	CButton	rPoll500;
	CButton	rPoll250;
	CButton	check6;
	CButton	check5;
	CButton	check4;
	CButton	check3;
	CButton	check2;
	CButton	check1;
	CButton	btnSetMode;
	CButton	btnReadMode;
	CButton	rEXactive;
	CButton	rEXnot;
	CButton	rInterEither;
	CButton	rInterBoth;
	CComboBox	cbReader;
	CButton	btnConnect;
	CButton	btnInit;
	CStatusBar m_bar;
	afx_msg void OnICC();
	afx_msg void OnPICC();

	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPollingSampleDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CPollingSampleDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInterBoth();
	afx_msg void OnInterEither();
	afx_msg void OnExnot();
	afx_msg void OnEXactive();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnReadMode();
	afx_msg void OnSetMode();
	afx_msg void OnReadPoll();
	afx_msg void OnSetPoll();
	afx_msg void OnManual();
	afx_msg void OnAuto();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnPoll250();
	afx_msg void OnPoll500();
	afx_msg void OnPoll1();
	afx_msg void OnPoll25();
	//afx_msg void OnICC();
	//afx_msg void OnPICC();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_POLLINGSAMPLEDLG_H__57351089_48B9_4358_83F7_11F560887D97__INCLUDED_)
