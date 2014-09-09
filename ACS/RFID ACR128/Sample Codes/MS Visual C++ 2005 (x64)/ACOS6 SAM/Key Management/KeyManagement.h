// KeyManagement.h : main header file for the KEYMANAGEMENT application
//

#if !defined(AFX_KEYMANAGEMENT_H__58501646_4428_4515_AF8B_5C88A1AFF388__INCLUDED_)
#define AFX_KEYMANAGEMENT_H__58501646_4428_4515_AF8B_5C88A1AFF388__INCLUDED_
#include "winscard.h"
#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CKeyManagementApp:
// See KeyManagement.cpp for the implementation of this class
//

class CKeyManagementApp : public CWinApp
{
public:
	CKeyManagementApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CKeyManagementApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CKeyManagementApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_KEYMANAGEMENT_H__58501646_4428_4515_AF8B_5C88A1AFF388__INCLUDED_)
