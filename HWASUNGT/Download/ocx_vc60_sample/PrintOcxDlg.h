// PrintOcxDlg.h : header file
//
//{{AFX_INCLUDES()
#include "hwausb.h"
//}}AFX_INCLUDES

#if !defined(AFX_PRINTOCXDLG_H__C15BB86D_1578_4EC9_A436_8819A4A18D7F__INCLUDED_)
#define AFX_PRINTOCXDLG_H__C15BB86D_1578_4EC9_A436_8819A4A18D7F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CPrintOcxDlg dialog

class CPrintOcxDlg : public CDialog
{
// Construction
public:
	CPrintOcxDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPrintOcxDlg)
	enum { IDD = IDD_PRINTOCX_DIALOG };
	CHwaUSB	m_test1;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPrintOcxDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CPrintOcxDlg)
	virtual BOOL OnInitDialog();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	virtual void OnCancel();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PRINTOCXDLG_H__C15BB86D_1578_4EC9_A436_8819A4A18D7F__INCLUDED_)
