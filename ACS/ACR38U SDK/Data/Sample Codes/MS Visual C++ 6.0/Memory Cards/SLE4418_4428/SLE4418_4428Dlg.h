// SLE4418_4428Dlg.h : header file
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


// SLE4418_4428Dlg-defined errors
#define INVALID_SW1SW2 -450
#define	NO_READER_INSTALLED	-451
#define IOCTL_SMARTCARD_SET_CARD_TYPE		SCARD_CTL_CODE(2060)

#if !defined(AFX_SLE4418_4428DLG_H__8FEE5036_E530_4931_92D6_0F0C1C515D11__INCLUDED_)
#define AFX_SLE4418_4428DLG_H__8FEE5036_E530_4931_92D6_0F0C1C515D11__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CSLE4418_4428Dlg dialog

class CSLE4418_4428Dlg : public CDialog
{
// Construction
public:
	CSLE4418_4428Dlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CSLE4418_4428Dlg)
	enum { IDD = IDD_SLE4418_4428_DIALOG };
	CButton	rbSLE4418;
	CListBox	mMsg;
	CEdit	tData;
	CEdit	tLen;
	CEdit	tLoAdd;
	CEdit	tHiAdd;
	CComboBox	cbReader;
	CButton	bErrCtr;
	CButton	bSubmit;
	CButton	bWriteProt;
	CButton	bWrite;
	CButton	bReadProt;
	CButton	bRead;
	CButton	bQuit;
	CButton	bReset;
	CButton	bConnect;
	CButton	bInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSLE4418_4428Dlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
		m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CSLE4418_4428Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnQuit();
	afx_msg void OnEditchangeCombo1();
	afx_msg void OnRadio1();
	afx_msg void OnRadio2();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnReset();
	afx_msg void OnRead();
	afx_msg void OnReadProt();
	afx_msg void OnWrite();
	afx_msg void OnWriteProt();
	afx_msg void OnSubmit();
	afx_msg void OnErrCtr();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SLE4418_4428DLG_H__8FEE5036_E530_4931_92D6_0F0C1C515D11__INCLUDED_)
