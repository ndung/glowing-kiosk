// SAMSampleUsageDlg.h : header file
//

#if !defined(AFX_SAMSAMPLEUSAGEDLG_H__BBF622F1_3508_4D8C_8F0B_DD57F4F09B7B__INCLUDED_)
#define AFX_SAMSAMPLEUSAGEDLG_H__BBF622F1_3508_4D8C_8F0B_DD57F4F09B7B__INCLUDED_
#include "WINSCARD.H"
#include "MyTabCtrl.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CSAMSampleUsageDlg dialog

class CSAMSampleUsageDlg : public CDialog
{
// Construction
public:
	CSAMSampleUsageDlg(CWnd* pParent = NULL);	// standard constructor
	
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
	BYTE G_SendBuff [256];
	BYTE G_RecvBuff [256];
	BYTE G_AlgoRef;

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
	int CreditDebit(BYTE* DataIn, BYTE MaxDataLen, BYTE Buff1, BYTE Buff2, CString Response);
	int Debit_ACOS2(CString Response);

	CString GetSAMResponse(BYTE RecvLen, BYTE Buff4);
	CString GetMCUResponse(BYTE SLen);
// Dialog Data
	//{{AFX_DATA(CSAMSampleUsageDlg)
	enum { IDD = IDD_SAMSAMPLEUSAGE_DIALOG };
	MyTabCtrl	m_tabCtrl;
	CListBox	m_listbox;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSAMSampleUsageDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSAMSampleUsageDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAMSAMPLEUSAGEDLG_H__BBF622F1_3508_4D8C_8F0B_DD57F4F09B7B__INCLUDED_)
