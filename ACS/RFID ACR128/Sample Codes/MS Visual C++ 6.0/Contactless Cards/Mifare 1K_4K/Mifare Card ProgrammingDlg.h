// Mifare Card ProgrammingDlg.h : header file
//

#if !defined(AFX_MIFARECARDPROGRAMMINGDLG_H__325CCA34_7013_41E3_B561_872715E8499A__INCLUDED_)
#define AFX_MIFARECARDPROGRAMMINGDLG_H__325CCA34_7013_41E3_B561_872715E8499A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMifareCardProgrammingDlg dialog

class CMifareCardProgrammingDlg : public CDialog
{
// Construction
public:
	CMifareCardProgrammingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMifareCardProgrammingDlg)
	enum { IDD = IDD_MIFARECARDPROGRAMMING_DIALOG };
	CRichEditCtrl	rbResult;
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CButton	btnRestoreVal;
	CButton	btnReadVal;
	CButton	btnDec;
	CButton	btnInc;
	CButton	btnStoreVal;
	CEdit	tbTarget;
	CEdit	tbSource;
	CEdit	tbBlock;
	CEdit	tbValue;
	CButton	btnUpdate;
	CButton	btnRead;
	CEdit	tbData;
	CEdit	tbLen;
	CEdit	tbStartBlock;
	CButton	btnAuthen;
	CEdit	tbKeyValIn6;
	CEdit	tbKeyValIn5;
	CEdit	tbKeyValIn4;
	CEdit	tbKeyValIn3;
	CEdit	tbKeyValIn2;
	CEdit	tbKeyValIn1;
	CEdit	tbKeyStoreNo;
	CEdit	tbBlockNo;
	CButton	rKeyB;
	CButton	rKeyA;
	CButton	rKeyNonVol;
	CButton	rKeyVol;
	CButton	rKeyMan;
	CButton	rVol;
	CButton	rNonVol;
	CEdit	tbKeyVal6;
	CEdit	tbKeyVal5;
	CEdit	tbKeyVal4;
	CEdit	tbKeyVal3;
	CEdit	tbKeyVal1;
	CEdit	tbKeyVal2;
	CEdit	tbKeyStore;
	CButton	btnLoadKey;
	CComboBox	cbReader;
	CButton	btnConnect;
	CButton	btnInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMifareCardProgrammingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CMifareCardProgrammingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnNonVol();
	afx_msg void OnVol();
	afx_msg void OnKeyMan();
	afx_msg void OnKeyVol();
	afx_msg void OnKeyNonVol();
	afx_msg void OnKeyA();
	afx_msg void OnKeyB();
	afx_msg void OnLoadKey();
	afx_msg void OnAuthen();
	afx_msg void OnReadBlock();
	afx_msg void OnUpdateBlock();
	afx_msg void OnStoreVal();
	afx_msg void OnInc();
	afx_msg void OnDec();
	afx_msg void OnReadVal();
	afx_msg void OnRestore();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MIFARECARDPROGRAMMINGDLG_H__325CCA34_7013_41E3_B561_872715E8499A__INCLUDED_)
