// IIC.h : main header file for the IIC application
//

#if !defined(AFX_IIC_H__63A091FA_E1F1_4B41_96B0_447C91BA226D__INCLUDED_)
#define AFX_IIC_H__63A091FA_E1F1_4B41_96B0_447C91BA226D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CIICApp:
// See IIC.cpp for the implementation of this class
//

class CIICApp : public CWinApp
{
public:
	CIICApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CIICApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CIICApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_IIC_H__63A091FA_E1F1_4B41_96B0_447C91BA226D__INCLUDED_)
