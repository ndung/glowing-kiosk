#if !defined(AFX_ACOS_INIT_H__25D97CF3_F455_4416_ACF5_BD8646D043D3__INCLUDED_)
#define AFX_ACOS_INIT_H__25D97CF3_F455_4416_ACF5_BD8646D043D3__INCLUDED_
#include "KeyManagementDlg.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ACOS_INIT.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// ACOS_INIT dialog

class ACOS_INIT : public CDialog
{
// Construction
public:
	ACOS_INIT(CWnd* pParent = NULL);   // standard constructor
		
	void LoadReaderNames ();

	LONG RetCode;
	char ReaderName [128];

	unsigned char L_SendBuff [256];
	unsigned char L_SendBuff2 [256];
	
	LONG L_SendLen;
	LONG L_RecvLen;

	ACOS_INIT *p;
	CKeyManagementDlg *frmParent; //Pointer to Main Window
	// Dialog Data
	//{{AFX_DATA(ACOS_INIT)
	enum { IDD = IDD_ACOS_INIT_DIALOG };
	CButton	m_rdo2;
	CButton	m_rdo1;
	CComboBox	m_combo;
	CEdit	m_ACOSKrdRyt;
	CEdit	m_ACOSKcfRyt;
	CEdit	m_ACOSKcrRyt;
	CEdit	m_ACOSKdRyt;
	CEdit	m_ACOSKtRyt;
	CEdit	m_ACOSKcRyt;
	CEdit	m_ACOSKrd;
	CEdit	m_ACOSKcf;
	CEdit	m_ACOSKcr;
	CEdit	m_ACOSKd;
	CEdit	m_ACOSKt;
	CEdit	m_ACOSKc;
	CEdit	m_ACOSIC;
	CEdit	m_ACOSPIN;
	CEdit	m_ACOSSN;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(ACOS_INIT)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(ACOS_INIT)
	virtual BOOL OnInitDialog();
	afx_msg void OnButton1();
	afx_msg void OnButton3();
	afx_msg void OnButton4();
	afx_msg void OnRadio1();
	afx_msg void OnRadio2();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ACOS_INIT_H__25D97CF3_F455_4416_ACF5_BD8646D043D3__INCLUDED_)
