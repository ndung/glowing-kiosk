#if !defined(AFX_MYTABCTRL_H__F2BE49C1_1B83_49E0_B759_BDC1910A5DF7__INCLUDED_)
#define AFX_MYTABCTRL_H__F2BE49C1_1B83_49E0_B759_BDC1910A5DF7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// MyTabCtrl.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// MyTabCtrl window

class MyTabCtrl : public CTabCtrl
{
// Construction
public:
	MyTabCtrl();

// Attributes
public:
	//Array to hold the list of dialog boxes/tab pages for CTabCtrl
	int m_DialogID[2];
	int m_nPageCount;


	//CDialog Array Variable to hold the dialogs 
	CDialog *m_Dialog[2];
// Operations
public:
	//Function to Create the dialog boxes during startup
	void InitDialogs();

	//Function to activate the tab dialog boxes
	void ActivateTabDialogs();
// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(MyTabCtrl)
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~MyTabCtrl();

	// Generated message map functions
protected:
	//{{AFX_MSG(MyTabCtrl)
	afx_msg void OnSelchange(NMHDR* pNMHDR, LRESULT* pResult);
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MYTABCTRL_H__F2BE49C1_1B83_49E0_B759_BDC1910A5DF7__INCLUDED_)
