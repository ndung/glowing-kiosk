// MyTabCtrl.cpp : implementation file
//

#include "stdafx.h"
#include "SAMSampleUsage.h"
#include "MyTabCtrl.h"
#include "ACCOUNT.h"
#include "SECURITY.h"
#include "SAMSampleUsageDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// MyTabCtrl

MyTabCtrl::MyTabCtrl()
{
   m_DialogID[0] =IDD_SECURITY_DIALOG;
   m_DialogID[1] =IDD_ACCOUNT_DIALOG;


   m_Dialog[0] = new SECURITY();
   m_Dialog[1] = new ACCOUNT();

   m_nPageCount = 2;
}

MyTabCtrl::~MyTabCtrl()
{
}

//This function creates the Dialog boxes once
void MyTabCtrl::InitDialogs()
{
	m_Dialog[0]->Create(m_DialogID[0],GetParent());
	m_Dialog[1]->Create(m_DialogID[1],GetParent());
}

void MyTabCtrl::ActivateTabDialogs()
{
  int nSel = GetCurSel();
  if(m_Dialog[nSel]->m_hWnd)
     m_Dialog[nSel]->ShowWindow(SW_HIDE);

  CRect l_rectClient;
  CRect l_rectWnd;

  GetClientRect(l_rectClient);
  AdjustRect(FALSE,l_rectClient);
  GetWindowRect(l_rectWnd);
  GetParent()->ScreenToClient(l_rectWnd);
  l_rectClient.OffsetRect(l_rectWnd.left,l_rectWnd.top);
  for(int nCount=0; nCount < m_nPageCount; nCount++){
     m_Dialog[nCount]->SetWindowPos(&wndTop, l_rectClient.left, l_rectClient.top, l_rectClient.Width(), l_rectClient.Height(), SWP_HIDEWINDOW);
  }
  m_Dialog[nSel]->SetWindowPos(&wndTop, l_rectClient.left, l_rectClient.top, l_rectClient.Width(), l_rectClient.Height(), SWP_SHOWWINDOW);

  m_Dialog[nSel]->ShowWindow(SW_SHOW);

}

BEGIN_MESSAGE_MAP(MyTabCtrl, CTabCtrl)
	//{{AFX_MSG_MAP(MyTabCtrl)
	ON_NOTIFY_REFLECT(TCN_SELCHANGE, OnSelchange)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// MyTabCtrl message handlers

void MyTabCtrl::OnSelchange(NMHDR* pNMHDR, LRESULT* pResult) 
{
	// TODO: Add your control notification handler code here
	ActivateTabDialogs();
	*pResult = 0;
}
