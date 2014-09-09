#if !defined(AFX_ACCOUNT_H__695DCF89_68CB_4BF5_9AB4_63A136175D08__INCLUDED_)
#define AFX_ACCOUNT_H__695DCF89_68CB_4BF5_9AB4_63A136175D08__INCLUDED_
#include "SAMSampleUsageDlg.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ACCOUNT.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// ACCOUNT dialog

class ACCOUNT : public CDialog
{
// Construction
public:
	ACCOUNT(CWnd* pParent = NULL);   // standard constructor
	CSAMSampleUsageDlg *frmParent; //Pointer to Main Window
	CString GetBalance(BYTE Data1, BYTE Data2, BYTE Data3);

	LONG RetCode;
	char ReaderName [128];
	
	unsigned char L_SendBuff [256];
		
	LONG L_SendLen;
	LONG L_RecvLen;
// Dialog Data
	//{{AFX_DATA(ACCOUNT)
	enum { IDD = IDD_ACCOUNT_DIALOG };
	CEdit	m_Debit;
	CEdit	m_Credit;
	CEdit	m_Balance;
	CEdit	m_MaxBalance;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(ACCOUNT)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(ACCOUNT)
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ACCOUNT_H__695DCF89_68CB_4BF5_9AB4_63A136175D08__INCLUDED_)
