// ConfigureATRDlg.h : header file
//


typedef struct _CmdBytes{
    BYTE
        bCla,   // The instruction class
        bIns,   // The instruction code 
        bP1,    // Parameter to the instruction
        bP2,    // Parameter to the instruction
        bP3;    // Size of I/O Transfer
	char
		bDataIn [256],		// Data in
		bDataOut [256];		// Data out
	char
		Status [2];			// (SW1/SW2)
} CmdBytes;


// GetATR-defined errors
#define INVALID_SW1SW2 -450
#define	NO_READER_INSTALLED	-451


#if !defined(AFX_CONFIGUREATRDLG_H__147B31DB_3430_41A5_AF13_594B279EE764__INCLUDED_)
#define AFX_CONFIGUREATRDLG_H__147B31DB_3430_41A5_AF13_594B279EE764__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CConfigureATRDlg dialog

class CConfigureATRDlg : public CDialog
{
// Construction
public:
	CConfigureATRDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CConfigureATRDlg)
	enum { IDD = IDD_CONFIGUREATR_DIALOG };
	CEdit	edit17;
	CEdit	edit16;
	CEdit	edit15;
	CEdit	edit14;
	CEdit	edit13;
	CEdit	edit12;
	CEdit	edit11;
	CEdit	edit10;
	CEdit	edit9;
	CEdit	edit8;
	CEdit	edit7;
	CEdit	edit6;
	CEdit	edit5;
	CEdit	edit4;
	CEdit	edit3;
	CButton	bReset;
	CButton	bUpdate;
	CEdit	edit2;
	CEdit	edit1;
	CComboBox	cbo_byte;
	CComboBox	cbo_baud;
	CComboBox	cbReader;
	CListBox	m_ListBox;
	CButton	bATR;
	CButton	bConnect;
	CButton	bInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CConfigureATRDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CConfigureATRDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	afx_msg void OnChangeEdit1();
	afx_msg void OnChangeEdit2();
	afx_msg void OnEditchangeCombo2();
	afx_msg void OnSelchangeCombo2();
	afx_msg void OnSelchangeCombo3();
	afx_msg void OnEditchangeCombo3();
	afx_msg void OnButton5();
	afx_msg void OnButton4();
	afx_msg void OnChangeEdit3();
	afx_msg void OnChangeEdit4();
	afx_msg void OnChangeEdit5();
	afx_msg void OnChangeEdit6();
	afx_msg void OnChangeEdit7();
	afx_msg void OnChangeEdit8();
	afx_msg void OnChangeEdit9();
	afx_msg void OnChangeEdit10();
	afx_msg void OnChangeEdit11();
	afx_msg void OnChangeEdit12();
	afx_msg void OnChangeEdit13();
	afx_msg void OnChangeEdit14();
	afx_msg void OnChangeEdit15();
	afx_msg void OnChangeEdit16();
	afx_msg void OnChangeEdit17();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CONFIGUREATRDLG_H__147B31DB_3430_41A5_AF13_594B279EE764__INCLUDED_)
