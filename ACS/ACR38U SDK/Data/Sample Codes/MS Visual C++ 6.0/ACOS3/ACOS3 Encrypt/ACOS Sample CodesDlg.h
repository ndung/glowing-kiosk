// ACOS Sample CodesDlg.h : header file
//

#if !defined(AFX_ACOSSAMPLECODESDLG_H__2A68B151_D788_40D9_968F_39D05779568D__INCLUDED_)
#define AFX_ACOSSAMPLECODESDLG_H__2A68B151_D788_40D9_968F_39D05779568D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CACOSSampleCodesDlg dialog

class CACOSSampleCodesDlg : public CDialog
{
// Construction
public:
	CACOSSampleCodesDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CACOSSampleCodesDlg)
	enum { IDD = IDD_ACOSSAMPLECODES_DIALOG };
	CButton	m_button7;
	CEdit	m_edit3;
	CComboBox	m_combo4;
	CEdit	m_edit2;
	CButton	m_radio4;
	CButton	m_button5;
	CButton	m_button6;
	CEdit	m_edit1;
	CButton	m_radio3;
	CButton	m_radio2;
	CButton	m_radio1;
	CButton	m_button4;
	CButton	m_button2;
	CButton	m_button1;
	CComboBox	m_Combo;
	CListBox	m_ListBox;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CACOSSampleCodesDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CACOSSampleCodesDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnRadio1();
	afx_msg void OnRadio2();
	afx_msg void OnRadio3();
	afx_msg void OnButton3();
	afx_msg void OnButton4();
	afx_msg void OnButton6();
	afx_msg void OnButton5();
	afx_msg void OnRadio4();
	afx_msg void OnButton7();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ACOSSAMPLECODESDLG_H__2A68B151_D788_40D9_968F_39D05779568D__INCLUDED_)
