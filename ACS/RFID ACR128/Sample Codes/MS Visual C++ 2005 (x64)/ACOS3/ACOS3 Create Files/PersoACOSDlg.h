// PersoACOSDlg.h : header file
//

#if !defined(AFX_PERSOACOSDLG_H__20BAD9E8_D392_11D5_923A_00010283AE0D__INCLUDED_)
#define AFX_PERSOACOSDLG_H__20BAD9E8_D392_11D5_923A_00010283AE0D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include "winscard.h"



/////////////////////////////////////////////////////////////////////////////
// CPersoACOSDlg dialog

class CPersoACOSDlg : public CDialog
{
// Construction
public:
	CPersoACOSDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPersoACOSDlg)
	enum { IDD = IDD_PERSOACOS_DIALOG };
	CButton	m_button4;
	CButton	m_button3;
	CButton	m_button2;
	CButton	m_button1;
	CListBox	m_Output;
	CComboBox	m_Port;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPersoACOSDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CPersoACOSDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnDestroy();
	afx_msg void OnBTNPersonalize();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

    SCARDHANDLE    m_hCard;
	SCARDCONTEXT   m_hContext;
	char		   m_sBuffer[MAX_PATH];

public:
	void ExchangeData(BYTE *SendBuff,DWORD nSend,BYTE *RecvBuff,DWORD &nRecv);

};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PERSOACOSDLG_H__20BAD9E8_D392_11D5_923A_00010283AE0D__INCLUDED_)
