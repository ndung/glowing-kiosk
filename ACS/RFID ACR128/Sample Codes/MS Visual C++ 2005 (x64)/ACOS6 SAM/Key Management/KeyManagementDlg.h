// KeyManagementDlg.h : header file
//

#if !defined(AFX_KEYMANAGEMENTDLG_H__69E71DB2_ABBA_415E_8D3B_11508D8EFBDD__INCLUDED_)
#define AFX_KEYMANAGEMENTDLG_H__69E71DB2_ABBA_415E_8D3B_11508D8EFBDD__INCLUDED_
#include "MyTabCtrl.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CKeyManagementDlg dialog

class CKeyManagementDlg : public CDialog
{
// Construction
public:
	CKeyManagementDlg(CWnd* pParent = NULL);	// standard constructor
	void SubmitIC();
	void ClearBuffers();
	void ResetSAM();
	void ResetMCU();

	LONG G_RetCode, G_Protocol;
	SCARDCONTEXT G_hContext;
	SCARDHANDLE G_hSAMCard;
	SCARDHANDLE G_hCard;
	SCARD_IO_REQUEST IO_REQ;

	char G_ReaderName [128];
	LONG G_SendLen;
	
	DWORD G_RecvLen;
	BOOLEAN G_ConnActive;
	BOOLEAN G_ConnActiveMCU;
	unsigned char G_SendBuff [256];
	unsigned char G_RecvBuff [256];
	unsigned char G_AlgoRef;

	//SAM Keys
	unsigned char G_GSAMGPIN [256];

	int DisplayOut(int errType, int retVal, CString PrintText, CString AppText);
	int SendAPDUSAM(BYTE* SendBuff, int SendLen, int RecvLen, BYTE* RecvBuff);
	int CreateSAMFile(BYTE FileLen, BYTE* DataArr, int MaxDataLen);
	int AppendSamFile(BYTE KeyId, BYTE* DataArr, int MaxDataLen);
	int selectfile(BYTE HiAddr, BYTE LoAddr);
	int SendAPDUandDisplay(int SendType, CString ApduIN) ;
	int readRecord(BYTE RecNo, BYTE DataLen);
	int writeRecord(int caseType, BYTE Recno, BYTE maxDataLen, BYTE DataLen, BYTE* DataIn);
	int GenerateSAMKey(BYTE KeyId, BYTE* DataArr, int maxDataLen);
	CString GetSAMResponse();

	// Dialog Data
	//{{AFX_DATA(CKeyManagementDlg)
	enum { IDD = IDD_KEYMANAGEMENT_DIALOG };
	MyTabCtrl	m_tabCtrl;
	CListBox	m_listbox;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CKeyManagementDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CKeyManagementDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnSelchangeTab2(NMHDR* pNMHDR, LRESULT* pResult);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_KEYMANAGEMENTDLG_H__69E71DB2_ABBA_415E_8D3B_11508D8EFBDD__INCLUDED_)
