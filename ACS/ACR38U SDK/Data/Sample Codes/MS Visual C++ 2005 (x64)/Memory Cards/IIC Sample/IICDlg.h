// IICDlg.h : header file
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


// IIC-defined errors
#define INVALID_SW1SW2 -450
#define	NO_READER_INSTALLED	-451
#define IOCTL_SMARTCARD_SET_CARD_TYPE		SCARD_CTL_CODE(2060)

#if !defined(AFX_IICDLG_H__9BCE3407_6CA1_4BA3_8346_F8189F3333D7__INCLUDED_)
#define AFX_IICDLG_H__9BCE3407_6CA1_4BA3_8346_F8189F3333D7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CIICDlg dialog

class CIICDlg : public CDialog
{
// Construction
public:
	CIICDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CIICDlg)
	enum { IDD = IDD_IIC_DIALOG };
	CListBox	mMsg;
	CEdit	tData;
	CEdit	tLen;
	CEdit	tLoAdd;
	CEdit	tHiAdd;
	CEdit	tBitAdd;
	CComboBox	cbReader;
	CComboBox	cbCardType;
	CComboBox	cbPageSize;
	CButton	bReset;
	CButton	bQuit;
	CButton	bSet;
	CButton	bRead;
	CButton	bWrite;
	CButton	bConnect;
	CButton	bInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CIICDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//HICON m_hIcon;
	HICON 	m_hIconBig,
		m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CIICDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnQuit();
	afx_msg void OnReset();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnEditchangecbReader();
	afx_msg void OnEditchangeCombo2();
	afx_msg void OnSetPageSize();
	afx_msg void OnRead();
	afx_msg void OnWrite();
	afx_msg void OnSelchangeCombo1();
	afx_msg void OnSelchangeCombo2();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_IICDLG_H__9BCE3407_6CA1_4BA3_8346_F8189F3333D7__INCLUDED_)
