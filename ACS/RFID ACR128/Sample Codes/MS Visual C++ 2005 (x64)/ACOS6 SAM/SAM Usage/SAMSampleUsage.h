// SAMSampleUsage.h : main header file for the SAMSAMPLEUSAGE application
//

#if !defined(AFX_SAMSAMPLEUSAGE_H__1A08EA15_355B_40D5_B3A0_BB324B5BF42F__INCLUDED_)
#define AFX_SAMSAMPLEUSAGE_H__1A08EA15_355B_40D5_B3A0_BB324B5BF42F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CSAMSampleUsageApp:
// See SAMSampleUsage.cpp for the implementation of this class
//

class CSAMSampleUsageApp : public CWinApp
{
public:
	CSAMSampleUsageApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSAMSampleUsageApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CSAMSampleUsageApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAMSAMPLEUSAGE_H__1A08EA15_355B_40D5_B3A0_BB324B5BF42F__INCLUDED_)
