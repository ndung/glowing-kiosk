// ConfigureATR.h : main header file for the CONFIGUREATR application
//

#if !defined(AFX_CONFIGUREATR_H__04431A67_8243_46F5_9D01_F757395E91B2__INCLUDED_)
#define AFX_CONFIGUREATR_H__04431A67_8243_46F5_9D01_F757395E91B2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CConfigureATRApp:
// See ConfigureATR.cpp for the implementation of this class
//

class CConfigureATRApp : public CWinApp
{
public:
	CConfigureATRApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CConfigureATRApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CConfigureATRApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CONFIGUREATR_H__04431A67_8243_46F5_9D01_F757395E91B2__INCLUDED_)
