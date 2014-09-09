// Advanced Device Programming.h : main header file for the ADVANCED DEVICE PROGRAMMING application
//

#if !defined(AFX_ADVANCEDDEVICEPROGRAMMING_H__30B75487_0F1D_4C07_A58F_CA8AEDD1FE34__INCLUDED_)
#define AFX_ADVANCEDDEVICEPROGRAMMING_H__30B75487_0F1D_4C07_A58F_CA8AEDD1FE34__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CAdvancedDeviceProgrammingApp:
// See Advanced Device Programming.cpp for the implementation of this class
//

class CAdvancedDeviceProgrammingApp : public CWinApp
{
public:
	CAdvancedDeviceProgrammingApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAdvancedDeviceProgrammingApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CAdvancedDeviceProgrammingApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ADVANCEDDEVICEPROGRAMMING_H__30B75487_0F1D_4C07_A58F_CA8AEDD1FE34__INCLUDED_)
