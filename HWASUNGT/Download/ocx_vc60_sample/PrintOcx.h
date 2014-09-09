// PrintOcx.h : main header file for the PRINTOCX application
//

#if !defined(AFX_PRINTOCX_H__E4A3C434_47F3_4B2F_B8E1_7A2485DBD2B4__INCLUDED_)
#define AFX_PRINTOCX_H__E4A3C434_47F3_4B2F_B8E1_7A2485DBD2B4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CPrintOcxApp:
// See PrintOcx.cpp for the implementation of this class
//

class CPrintOcxApp : public CWinApp
{
public:
	CPrintOcxApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPrintOcxApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CPrintOcxApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PRINTOCX_H__E4A3C434_47F3_4B2F_B8E1_7A2485DBD2B4__INCLUDED_)
