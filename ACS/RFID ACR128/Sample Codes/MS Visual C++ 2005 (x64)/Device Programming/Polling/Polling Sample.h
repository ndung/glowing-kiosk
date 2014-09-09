// Polling Sample.h : main header file for the POLLING SAMPLE application
//

#if !defined(AFX_POLLINGSAMPLE_H__43BC270D_581D_42F6_8909_C05CFD566F76__INCLUDED_)
#define AFX_POLLINGSAMPLE_H__43BC270D_581D_42F6_8909_C05CFD566F76__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CPollingSampleApp:
// See Polling Sample.cpp for the implementation of this class
//

class CPollingSampleApp : public CWinApp
{
public:
	CPollingSampleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPollingSampleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CPollingSampleApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_POLLINGSAMPLE_H__43BC270D_581D_42F6_8909_C05CFD566F76__INCLUDED_)
