#if !defined(AFX_SECURITY_H__700D2BE0_7C96_47B8_9D14_21FAA271B32B__INCLUDED_)
#define AFX_SECURITY_H__700D2BE0_7C96_47B8_9D14_21FAA271B32B__INCLUDED_
#include "SAMSampleUsageDlg.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SECURITY.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// SECURITY dialog

class SECURITY : public CDialog
{
// Construction
public:
	SECURITY(CWnd* pParent = NULL);   // standard constructor
	void LoadReaderNames ();
	
	LONG RetCode;
	char ReaderName [128];
	CSAMSampleUsageDlg *frmParent; //Pointer to Main Window
	
	unsigned char L_SendBuff [256];
		
	LONG L_SendLen;
	LONG L_RecvLen;

	SECURITY *p;
// Dialog Data
	//{{AFX_DATA(SECURITY)
	enum { IDD = IDD_SECURITY_DIALOG };
	CComboBox	m_combo;
	CComboBox	m_combo2;
	CButton	m_ChangePIN;
	CButton	m_SubmitPIN;
	CButton	m_MA;
	CEdit	m_ACOSNewPIN;
	CEdit	m_ACOSPIN;
	CEdit	m_SAMGPIN;
	CButton	m_rdo2;
	CButton	m_rdo1;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(SECURITY)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(SECURITY)
	virtual BOOL OnInitDialog();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	afx_msg void OnButton4();
	afx_msg void OnButton5();
	afx_msg void OnRadio1();
	afx_msg void OnRadio2();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SECURITY_H__700D2BE0_7C96_47B8_9D14_21FAA271B32B__INCLUDED_)
