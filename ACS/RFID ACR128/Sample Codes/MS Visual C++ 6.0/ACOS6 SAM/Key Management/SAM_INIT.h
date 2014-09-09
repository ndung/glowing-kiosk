#if !defined(AFX_SAM_INIT_H__88A00072_04A2_4CF1_A352_B72F2CE5B643__INCLUDED_)
#define AFX_SAM_INIT_H__88A00072_04A2_4CF1_A352_B72F2CE5B643__INCLUDED_
#include "KeyManagementDlg.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SAM_INIT.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// SAM_INIT dialog

class SAM_INIT : public CDialog
{
// Construction
public:
	SAM_INIT(CWnd* pParent = NULL);   // standard constructor
	void LoadReaderNames ();

	CKeyManagementDlg *frmParent; //Pointer to Main Window
	
	LONG RetCode;
	char ReaderName [128];
	unsigned char SendBuff [256];
	unsigned char RecvBuff [256];

	SAM_INIT *p; 
	// Dialog Data
	//{{AFX_DATA(SAM_INIT)
	enum { IDD = IDD_SAM_INIT_DIALOG };
	CButton	m_button2;
	CButton	m_button1;
	CComboBox	m_combo;
	CEdit	m_SAMKrd;
	CEdit	m_SAMKcf;
	CEdit	m_SAMKcr;
	CEdit	m_SAMKd;
	CEdit	m_SAMKt;
	CEdit	m_SAMKc;
	CEdit	m_SAMIC;
	CEdit	m_SAMGPIN;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(SAM_INIT)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(SAM_INIT)
	virtual BOOL OnInitDialog();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAM_INIT_H__88A00072_04A2_4CF1_A352_B72F2CE5B643__INCLUDED_)
