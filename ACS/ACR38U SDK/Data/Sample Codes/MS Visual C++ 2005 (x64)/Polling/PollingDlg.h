// PollingDlg.h : header file
//

// Polling-defined errors
#define	NO_READER_INSTALLED	-451

#if !defined(AFX_POLLINGDLG_H__B9D602AB_255F_4146_BC1E_A2E48C90207B__INCLUDED_)
#define AFX_POLLINGDLG_H__B9D602AB_255F_4146_BC1E_A2E48C90207B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CPollingDlg dialog

class CPollingDlg : public CDialog
{
// Construction
public:
	CPollingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPollingDlg)
	enum { IDD = IDD_POLLING_DIALOG };
	CEdit	tMsg;
	CComboBox	cbReader;
	CButton	bQuit;
	CButton	bReset;
	CButton	bEnd;
	CButton	bStart;
	CButton	bInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPollingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
		m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CPollingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnStartPolling();
	afx_msg void OnEndPolling();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_POLLINGDLG_H__B9D602AB_255F_4146_BC1E_A2E48C90207B__INCLUDED_)
