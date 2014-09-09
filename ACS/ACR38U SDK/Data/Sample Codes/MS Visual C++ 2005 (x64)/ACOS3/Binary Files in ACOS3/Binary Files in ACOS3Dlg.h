// Binary Files in ACOS3Dlg.h : header file
//

#if !defined(AFX_BINARYFILESINACOS3DLG_H__B2D4B333_ED89_41A3_989F_58046F3880BB__INCLUDED_)
#define AFX_BINARYFILESINACOS3DLG_H__B2D4B333_ED89_41A3_989F_58046F3880BB__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CBinaryFilesinACOS3Dlg dialog

class CBinaryFilesinACOS3Dlg : public CDialog
{
// Construction
public:
	CBinaryFilesinACOS3Dlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CBinaryFilesinACOS3Dlg)
	enum { IDD = IDD_BINARYFILESINACOS3_DIALOG };
	CEdit	tbLength;
	CButton	btnWrite;
	CButton	btnRead;
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CRichEditCtrl	rbResult;
	CEdit	tbData;
	CEdit	tbOffset1;
	CEdit	tbOffset2;
	CEdit	tbID2;
	CEdit	tbID1;
	CEdit	tbLen2;
	CEdit	tbLen1;
	CEdit	tbFileID2;
	CEdit	tbFileID1;
	CComboBox	cbReader;
	CButton	btnFormat;
	CButton	btnConnect;
	CButton	btnInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CBinaryFilesinACOS3Dlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CBinaryFilesinACOS3Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnFormat();
	afx_msg void OnRead();
	afx_msg void OnWrite();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_BINARYFILESINACOS3DLG_H__B2D4B333_ED89_41A3_989F_58046F3880BB__INCLUDED_)
