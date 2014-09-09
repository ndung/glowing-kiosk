// Advanced Device ProgrammingDlg.h : header file
//

#if !defined(AFX_ADVANCEDDEVICEPROGRAMMINGDLG_H__4F7824B3_7D0D_4F60_B657_87E37125C1ED__INCLUDED_)
#define AFX_ADVANCEDDEVICEPROGRAMMINGDLG_H__4F7824B3_7D0D_4F60_B657_87E37125C1ED__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CAdvancedDeviceProgrammingDlg dialog

class CAdvancedDeviceProgrammingDlg : public CDialog
{
// Construction
public:
	CAdvancedDeviceProgrammingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CAdvancedDeviceProgrammingDlg)
	enum { IDD = IDD_ADVANCEDDEVICEPROGRAMMING_DIALOG };
	CButton	rInterSAM;
	CButton	rInterPICC;
	CButton	rInterICC;
	CButton	btnRefresh;
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CRichEditCtrl	rbResult;
	CButton	btnReg;
	CButton	btnGetReg;
	CEdit	tbNewReg;
	CEdit	tbCurrReg;
	CEdit	tbMessage;
	CButton	btnGetPPS;
	CButton	btnPPS;
	CButton	rCurrNO;
	CButton	rCurr848;
	CButton	rCurr424;
	CButton	rCurr212;
	CButton	rCurr106;
	CButton	rMaxNO;
	CButton	rMax848;
	CButton	rMax424;
	CButton	rMax212;
	CButton	rMax106;
	CButton	rISOAB;
	CButton	rISOB;
	CButton	rISOA;
	CButton	rANTOff;
	CButton	rANTOn;
	CButton	btnAuto;
	CButton	btnPICCType;
	CButton	btnGetPICCType;
	CButton	btnPICC;
	CButton	btnGetPICC;
	CButton	btnError;
	CButton	btnGetError;
	CEdit	tbRXA2;
	CEdit	tbACONDA2;
	CEdit	tbMODA2;
	CEdit	tbRXA1;
	CEdit	tbCONDA1;
	CEdit	tbMODA1;
	CEdit	tbRXB1;
	CEdit	tbRXB2;
	CEdit	tbCONDB2;
	CEdit	tbMODB2;
	CEdit	tbCONDB1;
	CEdit	tbMODB1;
	CEdit	tbPICC;
	CEdit	tbPCD;
	CButton	check1;
	CButton	btnGetTransOpt;
	CButton	btnTransOpt;
	CEdit	tbTXMode;
	CEdit	tbRecvGain;
	CEdit	tbSetup;
	CEdit	tbFieldStop;
	CButton	btnGetAntenna;
	CButton	btnAntenna;
	CEdit	tbTrans;
	CEdit	tbPoll;
	CEdit	tbFWI;
	CComboBox	cbReader;
	CButton	btnGetOptions;
	CButton	btnOptions;
	CButton	btnConnect;
	CButton	btnInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAdvancedDeviceProgrammingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CAdvancedDeviceProgrammingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnGetOpt();
	afx_msg void OnOpt();
	afx_msg void OnGetAntenna();
	afx_msg void OnAntenna();
	afx_msg void OnAntennaON();
	afx_msg void OnAntennaOFF();
	afx_msg void OnGetTrans();
	afx_msg void OnTrans();
	afx_msg void OnGetError();
	afx_msg void OnSetError();
	afx_msg void OnGetPICC();
	afx_msg void OnPICC();
	afx_msg void OnGetPICCType();
	afx_msg void OnPICCType();
	afx_msg void OnISOA();
	afx_msg void OnISOB();
	afx_msg void OnISOAB();
	afx_msg void OnAuto();
	afx_msg void OnMax106();
	afx_msg void OnMax212();
	afx_msg void OnMax424();
	afx_msg void OnMax848();
	afx_msg void OnMaxNO();
	afx_msg void OnCurr106();
	afx_msg void OnCurr212();
	afx_msg void OnCurr424();
	afx_msg void OnCurr848();
	afx_msg void OnCurrNo();
	afx_msg void OnGetCurrent();
	afx_msg void OnSetPPS();
	afx_msg void OnGetReg();
	afx_msg void OnSetReg();
	afx_msg void OnRefresh();
	afx_msg void OnInterICC();
	afx_msg void OnInterPICC();
	afx_msg void OnInterSAM();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ADVANCEDDEVICEPROGRAMMINGDLG_H__4F7824B3_7D0D_4F60_B657_87E37125C1ED__INCLUDED_)
