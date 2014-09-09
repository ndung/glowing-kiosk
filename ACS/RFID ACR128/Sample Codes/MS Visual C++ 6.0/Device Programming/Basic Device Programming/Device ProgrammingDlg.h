// Device ProgrammingDlg.h : header file
//

#if !defined(AFX_DEVICEPROGRAMMINGDLG_H__50C7DEC3_B420_4DA1_939B_B3F2FD7001B2__INCLUDED_)
#define AFX_DEVICEPROGRAMMINGDLG_H__50C7DEC3_B420_4DA1_939B_B3F2FD7001B2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingDlg dialog

class CDeviceProgrammingDlg : public CDialog
{
// Construction
public:
	CDeviceProgrammingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CDeviceProgrammingDlg)
	enum { IDD = IDD_DEVICEPROGRAMMING_DIALOG };
	CButton	checkGREEN;
	CButton	checkRED;
	CButton	check8;
	CButton	check7;
	CButton	check6;
	CButton	check5;
	CButton	check4;
	CButton	check3;
	CButton	check2;
	CButton	check1;
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CButton	btnSetStates;
	CButton	btnGetStates;
	CEdit	tbValue;
	CButton	btnBuzzDur;
	CButton	btnSetLED;
	CButton	btnGetLED;
	CComboBox	cbReader;
	CRichEditCtrl	rbResult;
	CButton	btnGetFW;
	CButton	btnConnect;
	CButton	btnInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDeviceProgrammingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CDeviceProgrammingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnGetFW();
	afx_msg void OnGetLED();
	afx_msg void OnSetLED();
	afx_msg void OnBuzzDur();
	afx_msg void OnGetStates();
	afx_msg void OnSetStates();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnStartBuzz();
	afx_msg void OnStopBuzz();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEVICEPROGRAMMINGDLG_H__50C7DEC3_B420_4DA1_939B_B3F2FD7001B2__INCLUDED_)
