///////////////////////////////////////////////////////////////////////////////
//
// FORM NAME : Key Management Sample
//
// COMPANY : ADVANDCED CARD SYSTEMS, LTD
//
// AUTHOR : MALCOLM BERNARD U. SOLAÑA
//
// DATE :  01 / 25 / 2007
//
//
// Description : This dialog implements the ACOS commands on
//
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////
// ACOS_INIT.cpp : implementation file
//

#include "stdafx.h"
#include "KeyManagement.h"
#include "ACOS_INIT.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// ACOS_INIT dialog


ACOS_INIT::ACOS_INIT(CWnd* pParent /*=NULL*/)
	: CDialog(ACOS_INIT::IDD, pParent)
{
	//{{AFX_DATA_INIT(ACOS_INIT)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
}


void ACOS_INIT::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(ACOS_INIT)
	DDX_Control(pDX, IDC_RADIO2, m_rdo2);
	DDX_Control(pDX, IDC_RADIO1, m_rdo1);
	DDX_Control(pDX, IDC_COMBO1, m_combo);
	DDX_Control(pDX, IDC_EDIT16, m_ACOSKrdRyt);
	DDX_Control(pDX, IDC_EDIT15, m_ACOSKcfRyt);
	DDX_Control(pDX, IDC_EDIT14, m_ACOSKcrRyt);
	DDX_Control(pDX, IDC_EDIT13, m_ACOSKdRyt);
	DDX_Control(pDX, IDC_EDIT12, m_ACOSKtRyt);
	DDX_Control(pDX, IDC_EDIT11, m_ACOSKcRyt);
	DDX_Control(pDX, IDC_EDIT9, m_ACOSKrd);
	DDX_Control(pDX, IDC_EDIT8, m_ACOSKcf);
	DDX_Control(pDX, IDC_EDIT7, m_ACOSKcr);
	DDX_Control(pDX, IDC_EDIT6, m_ACOSKd);
	DDX_Control(pDX, IDC_EDIT5, m_ACOSKt);
	DDX_Control(pDX, IDC_EDIT4, m_ACOSKc);
	DDX_Control(pDX, IDC_EDIT3, m_ACOSIC);
	DDX_Control(pDX, IDC_EDIT2, m_ACOSPIN);
	DDX_Control(pDX, IDC_EDIT1, m_ACOSSN);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(ACOS_INIT, CDialog)
	//{{AFX_MSG_MAP(ACOS_INIT)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	ON_BN_CLICKED(IDC_BUTTON4, OnButton4)
	ON_BN_CLICKED(IDC_RADIO1, OnRadio1)
	ON_BN_CLICKED(IDC_RADIO2, OnRadio2)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// ACOS_INIT message handlers

BOOL ACOS_INIT::OnInitDialog() 
{
	CDialog::OnInitDialog();
	this->frmParent =  (CKeyManagementDlg*) this->GetParent();


	CDialog::OnInitDialog();
	
	this->m_ACOSIC.SetLimitText(16);
	this->m_ACOSPIN.SetLimitText(16);
	this->m_ACOSKc.SetLimitText(16);
	this->m_ACOSKcRyt.SetLimitText(16);
	this->m_ACOSKt.SetLimitText(16);
	this->m_ACOSKtRyt.SetLimitText(16);
	this->m_ACOSKd.SetLimitText(16);
	this->m_ACOSKdRyt.SetLimitText(16);
	this->m_ACOSKcr.SetLimitText(16);
	this->m_ACOSKcrRyt.SetLimitText(16);
	this->m_ACOSKcf.SetLimitText(16);
	this->m_ACOSKcfRyt.SetLimitText(16);
	this->m_ACOSKrd.SetLimitText(16);
	this->m_ACOSKrdRyt.SetLimitText(16);

	
	frmParent->G_AlgoRef = 1;
	LoadReaderNames();
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

//Listing down the readers.
//This function is called on the InitDialog portion (FORM_LOAD) for VB
void ACOS_INIT::LoadReaderNames ()
{

	//This function list the SMART Card Reader/s Available

	//Variables
	char ReaderList [128];
	DWORD ReaderListSize = 128;
	int i;
	char *pch;

	if (frmParent->G_hContext != 0)
		SCardReleaseContext (frmParent->G_hContext);
	
	//Establish Card context
	RetCode = SCardEstablishContext(
		SCARD_SCOPE_USER,
		NULL,
		NULL,
		&frmParent->G_hContext);

	if (RetCode != SCARD_S_SUCCESS)
	{

		i = frmParent->m_listbox.AddString ("Load Reader Success");
		frmParent->m_listbox.SetCurSel (i);
		frmParent->G_hContext = 0;

		return;

	}

	//Listing the readers 
	RetCode = SCardListReadersA(
		frmParent->G_hContext,
		NULL,
		ReaderList,
		&ReaderListSize);

	if (RetCode != SCARD_S_SUCCESS)
	{

		i = frmParent->m_listbox.AddString ("Unable to List Readers");
		frmParent->m_listbox.SetCurSel (i);

		return;

	}

	pch = ReaderList;

	while (*pch != 0)
	{
		m_combo.AddString(pch);
		pch += strlen (pch) + 1;
	}
	m_combo.SetCurSel (0);

	m_ACOSPIN.EnableWindow(false);
	m_ACOSPIN.SetLimitText(16);
	
}

void ACOS_INIT::OnButton1() 
{
	// TODO: Add your control notification handler code here
	// This procedure connects the reader to the Smart Card inserted

	//Variables
	DWORD Protocol = 1;
	char buff[100];
	char buff1[100];
	m_combo.GetLBText (m_combo.GetCurSel (), ReaderName);
	
	if (frmParent->G_ConnActiveMCU)
	{	
		frmParent->DisplayOut(0, 0, "Already Connected", "MCU");
		return;
	}

	// 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
	RetCode = SCardConnectA(
		frmParent->G_hContext,
		ReaderName,
		SCARD_SHARE_EXCLUSIVE,
		Protocol,
        &frmParent->G_hCard,
		&Protocol);

	if (RetCode != SCARD_S_SUCCESS)
	{	//Fail to Connect
		
		frmParent->DisplayOut(1, 0, "Unable to Connect to Card", "MCU");

		return;

	}
	
	//Success in Connecting to Reader
	frmParent->IO_REQ.dwProtocol = Protocol;
	frmParent->IO_REQ.cbPciLength = sizeof (SCARD_IO_REQUEST);
	
	m_combo.GetLBText(m_combo.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Connection to ", buff1);
	
	this->m_ACOSPIN.EnableWindow(true);
	

	frmParent->DisplayOut(0, 0, buff, "");
	frmParent->G_ConnActiveMCU = true;
	
	this->m_rdo1.SetCheck(1);	
}

void ACOS_INIT::OnButton3() 
{
int i, j, ctr, ArrCnt;
	char buff[100];
	CString strTemp;
	CString tmpstr, tmpstr2;
	
	//Select Issuer DF ************************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0xA4;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;
	L_SendBuff[4] = 0x2;
	L_SendBuff[5] = 0x11;
	L_SendBuff[6] = 0x00;

	i = 6;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 7;

	
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x61) | (frmParent->G_RecvBuff[1] != 0x2D))
	{	
		return;
	}

	//Submit Issuer PIN *************************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x20;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x01;
	L_SendBuff[4] = 0x08;
	
	for (ctr = 0; ctr <=  7  ; ctr++)
	{
        L_SendBuff[ctr + 5] = frmParent->G_GSAMGPIN[ctr]  ;  	 
	}
	
	i = 12;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");
	L_RecvLen = 2;
    L_SendLen = 13;

	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}

	//Get Card Serial Number *******************************************
    //Select FF00 ******************************************************
    i = frmParent->selectfile(0xFF, 0x00);
	if (i != 0)
	{
		return;
	}

	i = frmParent->readRecord(0x00, 0x08);
	if (i != 0)
	{
		return;
	}
	
	i = 7;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_RecvBuff[ctr]);
		j+=3;
	}
	this->m_ACOSSN.SetWindowText(buff);

	
	//Send buff
	//Normal Sendbuff (L_SendBuff) ********
	m_ACOSSN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 4); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 3;   
	}

	//Xor Sendbuff (L_SendBuff2) ***********
	m_ACOSSN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 4); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X ", &L_SendBuff2[ctr]);
		L_SendBuff2[ctr] = L_SendBuff2[ctr] ^ 0xFF;
		ArrCnt = ArrCnt + 3;   
	}
	
	//Generate Key ***********************************************************
    //Generate IC Using 1st SAM Master Key (KeyID=81) ************************   
	if (frmParent->GenerateSAMKey(0x81, L_SendBuff, 8) != 0)
	{
		return;
	}
	

	strTemp = frmParent->GetSAMResponse();
	m_ACOSIC.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}

	//'Generate Card Key Using 2nd SAM Master Key (KeyID=82)
	if (frmParent->GenerateSAMKey(0x82, L_SendBuff, 8) != 0)
	{
		return;
	}
	
	//Get Response to Retrieve Generated Key (Card Key)**********************
	strTemp = frmParent->GetSAMResponse();
	m_ACOSKc.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}

	//'If Algorithm Reference = 3DES
    //then Generate Right Half of Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
	if (frmParent->G_AlgoRef == 0) 
	{	
		if (frmParent->GenerateSAMKey(0x82, L_SendBuff2, 8) != 0)
		{
			return;
		}
		
		//Get Response to Retrieve 3DES
		//Get Response to Retrieve Generated Key (Card Key)**********************
		strTemp = frmParent->GetSAMResponse();
		m_ACOSKcRyt.SetWindowText(strTemp);

		if (strTemp.IsEmpty())
		{
			return;
		}
		
	}
	else
	{
		m_ACOSKcRyt.SetWindowText(NULL);
	}

	 // 'Generate Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)**********
	if (frmParent->GenerateSAMKey(0x83, L_SendBuff, 8) != 0)
	{
		return;
	}
	
	//Get Response to Retrieve Generated Key (Terminal Key)**********************
	strTemp = frmParent->GetSAMResponse();
	m_ACOSKt.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}

	//'If Algorithm Reference = 3DES
    //then Generate Right Half of Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
	if (frmParent->G_AlgoRef == 0) 
	{	
		if (frmParent->GenerateSAMKey(0x83, L_SendBuff2, 8) != 0)
		{
			return;
		}
		
		//Get Response to Retrieve 3DES
        //Generated Key (Terminal Key)**********************
		strTemp = frmParent->GetSAMResponse();
		m_ACOSKtRyt.SetWindowText(strTemp);

		if (strTemp.IsEmpty())
		{
			return;
		}
		
	}
	else
	{
		m_ACOSKtRyt.SetWindowText(NULL);
	}

	//Generate Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
	if (frmParent->GenerateSAMKey(0x84, L_SendBuff, 8) != 0)
	{
		return;
	}

	//Get Response to Retrieve Generated Key (Debit Key)**********************
	strTemp = frmParent->GetSAMResponse();
	m_ACOSKd.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}
	

	//'If Algorithm Reference = 3DES
    //then Generate Right Half of Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
	if (frmParent->G_AlgoRef == 0) 
	{	
		if (frmParent->GenerateSAMKey(0x84, L_SendBuff2, 8) != 0)
		{
			return;
		}
		
		//Get Response to Retrieve 3DES
        //Generated Key (Debit Key)**********************
		strTemp = frmParent->GetSAMResponse();
		m_ACOSKdRyt.SetWindowText(strTemp);

		if (strTemp.IsEmpty())
		{
			return;
		}
		
	}
	else
	{
		m_ACOSKdRyt.SetWindowText(NULL);
	}


	//'Generate Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
	if (frmParent->GenerateSAMKey(0x85, L_SendBuff, 8) != 0)
	{
		return;
	}

	//Get Response to Retrieve Generated Key (Credit Key)**********************
	strTemp = frmParent->GetSAMResponse();
	m_ACOSKcr.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}
	
	//'If Algorithm Reference = 3DES
    //then Generate Right Half of Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
	if (frmParent->G_AlgoRef == 0) 
	{	
		if (frmParent->GenerateSAMKey(0x85, L_SendBuff2, 8) != 0)
		{
			return;
		}
		
		//Get Response to Retrieve 3DES
        //Generated Key (Credit Key)**********************
		strTemp = frmParent->GetSAMResponse();
		m_ACOSKcrRyt.SetWindowText(strTemp);

		if (strTemp.IsEmpty())
		{
			return;
		}
		
	}
	else
	{
		m_ACOSKcrRyt.SetWindowText(NULL);
	}

	//'Generate Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
	if (frmParent->GenerateSAMKey(0x86, L_SendBuff, 8) != 0)
	{
		return;
	}

	//Get Response to Retrieve Generated Key (Certify Key)**********************
	strTemp = frmParent->GetSAMResponse();
	m_ACOSKcf.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}
	
	//'If Algorithm Reference = 3DES
    //then Generate Right Half of Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
	if (frmParent->G_AlgoRef == 0) 
	{	
		if (frmParent->GenerateSAMKey(0x86, L_SendBuff2, 8) != 0)
		{
			return;
		}
		
		//Get Response to Retrieve 3DES
        //Generated Key (Certify Key)**********************
		strTemp = frmParent->GetSAMResponse();
		m_ACOSKcfRyt.SetWindowText(strTemp);

		if (strTemp.IsEmpty())
		{
			return;
		}
		
	}
	else
	{
		m_ACOSKcfRyt.SetWindowText(NULL);
	}

	//'Generate Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
	if (frmParent->GenerateSAMKey(0x87, L_SendBuff, 8) != 0)
	{
		return;
	}

	//Get Response to Retrieve Generated Key (Revoke Debit Key)**********************
	strTemp = frmParent->GetSAMResponse();
	m_ACOSKrd.SetWindowText(strTemp);

	if (strTemp.IsEmpty())
	{
		return;
	}

	//'If Algorithm Reference = 3DES
    //then Generate Right Half of Revoke Debit (Krd) Using 7th SAM Master Key (KeyID=87)
	if (frmParent->G_AlgoRef == 0) 
	{	
		if (frmParent->GenerateSAMKey(0x87, L_SendBuff2, 8) != 0)
		{
			return;
		}
		
		//Get Response to Retrieve 3DES
        //Generated Key (Revoke Debit Key)**********************
		strTemp = frmParent->GetSAMResponse();
		m_ACOSKrdRyt.SetWindowText(strTemp);

		if (strTemp.IsEmpty())
		{
			return;
		}
		
	}
	else
	{
		m_ACOSKrdRyt.SetWindowText(NULL);
	}	
}

void ACOS_INIT::OnButton4() 
{
char buff[100], buff1[100];
	int i, retcode, ctr, ArrCnt;
	CString tmpstr, tmpstr2;
	DWORD Protocol = 1;
	BYTE AccLoc;
	
	// TODO: Add your control notification handler code here
	m_ACOSPIN.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSPIN.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM PIN Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSPIN.SetFocus();
		return;
	}

	m_ACOSIC.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSIC.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM IC generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSIC.SetFocus();
		return;
	}

	m_ACOSKc.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSKc.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM Kc generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSKc.SetFocus();
		return;
	}
	
	m_ACOSKt.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSKt.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM Kt generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSKt.SetFocus();
		return;
	}

	m_ACOSKd.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSKd.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM Kd generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSKd.SetFocus();
		return;
	}
	
	m_ACOSKcr.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSKcr.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM Kcr generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSKcr.SetFocus();
		return;
	}

	m_ACOSKcf.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSKcf.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM Kcf generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSKcf.SetFocus();
		return;
	}

	m_ACOSKrd.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSKrd.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("No SAM Krd generated!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSKrd.SetFocus();
		return;
	}

	if (frmParent->G_AlgoRef == 0)
	{
		m_ACOSKcRyt.GetWindowText(buff, 100); 
		if (strlen(buff) < m_ACOSKcRyt.GetLimitText()) 
		{
			i = frmParent->m_listbox.AddString ("No SAM Kc Right generated!");
			frmParent->m_listbox.SetCurSel (i);
			m_ACOSKcRyt.SetFocus();
			return;
		}
		
		m_ACOSKtRyt.GetWindowText(buff, 100); 
		if (strlen(buff) < m_ACOSKtRyt.GetLimitText()) 
		{
			i = frmParent->m_listbox.AddString ("No SAM Kt Right generated!");
			frmParent->m_listbox.SetCurSel (i);
			m_ACOSKtRyt.SetFocus();
			return;
		}

		m_ACOSKdRyt.GetWindowText(buff, 100); 
		if (strlen(buff) < m_ACOSKdRyt.GetLimitText()) 
		{
			i = frmParent->m_listbox.AddString ("No SAM Kd Right generated!");
			frmParent->m_listbox.SetCurSel (i);
			m_ACOSKdRyt.SetFocus();
			return;
		}
		
		m_ACOSKcrRyt.GetWindowText(buff, 100); 
		if (strlen(buff) < m_ACOSKcrRyt.GetLimitText()) 
		{
			i = frmParent->m_listbox.AddString ("No SAM Kcr Right generated!");
			frmParent->m_listbox.SetCurSel (i);
			m_ACOSKcrRyt.SetFocus();
			return;
		}

		m_ACOSKcfRyt.GetWindowText(buff, 100); 
		if (strlen(buff) < m_ACOSKcfRyt.GetLimitText()) 
		{
			i = frmParent->m_listbox.AddString ("No SAM Kcf Right generated!");
			frmParent->m_listbox.SetCurSel (i);
			m_ACOSKcfRyt.SetFocus();
			return;
		}

		m_ACOSKrdRyt.GetWindowText(buff, 100); 
		if (strlen(buff) < m_ACOSKrdRyt.GetLimitText()) 
		{
			i = frmParent->m_listbox.AddString ("No SAM Krd Right generated!");
			frmParent->m_listbox.SetCurSel (i);
			m_ACOSKrdRyt.SetFocus();
			return;
		}
	}
	//'Update Personalization File (FF02)*****************************

	//'Select FF02****************************************************
	retcode = frmParent->selectfile(0xFF, 0x02);
	if (retcode != 0)
	{
		return;
	}
	
	//Submit Default IC************************************************
	frmParent->SubmitIC() ;
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		sprintf(buff, "%02X %02X",frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		frmParent->DisplayOut(1, 0, buff, "MCU");

		return;
	}

	//'Update FF02 record 0 *******************************************
	if (frmParent->G_AlgoRef == 0) 
	{
      L_SendBuff[0] = 0xFF;
	}
	else
	{
      L_SendBuff[0] = 0xFD;	
	}
	
	L_SendBuff[1] = 0x40;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x00, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Reset
	frmParent->ResetMCU();

	// 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
	RetCode = SCardConnectA(
		frmParent->G_hContext,
		ReaderName,
		SCARD_SHARE_EXCLUSIVE,
		Protocol,
        &frmParent->G_hCard,
		&Protocol);

	if (RetCode != SCARD_S_SUCCESS)
	{	//Fail to Connect
		
		frmParent->DisplayOut(1, 0, "Unable to Connect to Card", "MCU");

		return;

	}
	
	//Success in Connecting to Reader
	frmParent->IO_REQ.dwProtocol = Protocol;
	frmParent->IO_REQ.cbPciLength = sizeof (SCARD_IO_REQUEST);
	
	m_combo.GetLBText(m_combo.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Re-Connection to ", buff1);
	frmParent->DisplayOut(0, 0, buff, "");
	

	//'Update Card Keys (FF03) Security File **************************

   //'Select FF03  ***************************************************
	retcode = frmParent->selectfile(0xFF, 0x03);
	if (retcode != 0)
	{
		return;
	}
	
	//Submit Default IC************************************************
	frmParent->SubmitIC() ;
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		sprintf(buff, "%02X %02X",frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		frmParent->DisplayOut(1, 0, buff, "MCU");

		return;
	}
	

	//'Update FF03 record 0 (IC) *******************************************
	m_ACOSIC.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	retcode = frmParent->writeRecord(0, 0x00, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}


	//'Update FF03 record 1 (PIN) ******************************************
	m_ACOSPIN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	retcode = frmParent->writeRecord(0, 0x01, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//'Update FF03 record 2 (Kc) ******************************************
	m_ACOSKc.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}

	retcode = frmParent->writeRecord(0, 0x02, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//'If Algorithm Reference = 3DES
   //Update FF03 record 0x0C Right Half (Kc)
	if (frmParent->G_AlgoRef == 0)
	{	
		m_ACOSKcRyt.GetWindowText(tmpstr);
		ArrCnt = 0;
		for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
		{	
			tmpstr2 = tmpstr.Mid(ArrCnt, 2);
			tmpstr2 = "0x" + tmpstr2;

			sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
			ArrCnt = ArrCnt + 2;    
		}
	
		retcode = frmParent->writeRecord(0, 0x0C, 0x08, 0x08, L_SendBuff);
		if (retcode != 0) 
		{
			return;
		}

	}

	// 'Update FF03 record 3 (Kt)
	m_ACOSKt.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	retcode = frmParent->writeRecord(0, 0x03, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//'If Algorithm Reference = 3DES
   //Update FF03 record 0x0D Right Half (Kt)
	if (frmParent->G_AlgoRef == 0)
	{	
		m_ACOSKtRyt.GetWindowText(tmpstr);
		ArrCnt = 0;
		for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
		{	
			tmpstr2 = tmpstr.Mid(ArrCnt, 2);
			tmpstr2 = "0x" + tmpstr2;

			sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
			ArrCnt = ArrCnt + 2;    
		}	

		retcode = frmParent->writeRecord(0, 0x0D, 0x08, 0x08, L_SendBuff);
		if (retcode != 0) 
		{
			return;
		}

	}

	//'Select FF06 (Account Security File) ******************************
	retcode = frmParent->selectfile(0xFF, 0x06);
	if (retcode != 0)
	{
		return;
	}

	//'Update FF06 record 0 (Kd) ***************************************
	m_ACOSKd.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	if (frmParent->G_AlgoRef == 0) 
	{
		AccLoc = 0x4;
	}				
	else
	{
		AccLoc = 0x0;
	}

	retcode = frmParent->writeRecord(0, AccLoc, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//'Update FF06 record 1 (Kcr) ***************************************
	m_ACOSKcr.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	if (frmParent->G_AlgoRef == 0) 
	{
		AccLoc = 0x5;
	}				
	else
	{
		AccLoc = 0x1;
	}

	retcode = frmParent->writeRecord(0, AccLoc, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//'Update FF06 record 2 (Kcf)*****************************************
	m_ACOSKcf.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	if (frmParent->G_AlgoRef == 0) 
	{
		AccLoc = 0x6;
	}				
	else
	{
		AccLoc = 0x2;
	}

	retcode = frmParent->writeRecord(0, AccLoc, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}
	
	//'Update FF06 record 3 (Krd)*****************************************
	m_ACOSKrd.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;    
	}
	
	if (frmParent->G_AlgoRef == 0) 
	{
		AccLoc = 0x7;
	}				
	else
	{
		AccLoc = 0x3;
	}

	retcode = frmParent->writeRecord(0, AccLoc, 0x08, 0x08, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	 //'If Algorithm Reference = 3DES
	//then update Right Half of the Keys
	if (frmParent->G_AlgoRef == 0)
	{	
		//'Update FF06 record 0 (Kd) *****************************************
		m_ACOSKdRyt.GetWindowText(tmpstr);
		ArrCnt = 0;
		for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
		{	
			tmpstr2 = tmpstr.Mid(ArrCnt, 2);
			tmpstr2 = "0x" + tmpstr2;

			sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
			ArrCnt = ArrCnt + 2;    
		}	

		retcode = frmParent->writeRecord(0, 0x0, 0x08, 0x08, L_SendBuff);
		if (retcode != 0) 
		{
			return;
		}

		//'Update FF06 record 1 (Kcr) *****************************************
		m_ACOSKcrRyt.GetWindowText(tmpstr);
		ArrCnt = 0;
		for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
		{	
			tmpstr2 = tmpstr.Mid(ArrCnt, 2);
			tmpstr2 = "0x" + tmpstr2;

			sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
			ArrCnt = ArrCnt + 2;    
		}	

		retcode = frmParent->writeRecord(0, 0x1, 0x08, 0x08, L_SendBuff);
		if (retcode != 0) 
		{
			return;
		}

		//'Update FF06 record 2 (Kcf) *****************************************
		m_ACOSKcfRyt.GetWindowText(tmpstr);
		ArrCnt = 0;
		for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
		{	
			tmpstr2 = tmpstr.Mid(ArrCnt, 2);
			tmpstr2 = "0x" + tmpstr2;

			sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
			ArrCnt = ArrCnt + 2;    
		}	

		retcode = frmParent->writeRecord(0, 0x2, 0x08, 0x08, L_SendBuff);
		if (retcode != 0) 
		{
			return;
		}

		//'Update FF06 record 3 (Krd) *****************************************
		m_ACOSKrdRyt.GetWindowText(tmpstr);
		ArrCnt = 0;
		for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
		{	
			tmpstr2 = tmpstr.Mid(ArrCnt, 2);
			tmpstr2 = "0x" + tmpstr2;

			sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
			ArrCnt = ArrCnt + 2;    
		}	

		retcode = frmParent->writeRecord(0, 0x3, 0x08, 0x08, L_SendBuff);
		if (retcode != 0) 
		{
			return;
		}

	}

	
	//'Select FF05 (Account File)****************************************************
	retcode = frmParent->selectfile(0xFF, 0x05);
	if (retcode != 0)
	{
		return;
	}

	//'Initialize FF05 Account File*******************************************
	//Initialize Record 0 of Account File **************************************
   	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x00, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 1 of Account File **************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x01;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x01, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 2 of Account File **************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x02, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 3 of Account File **************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x01;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x03, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 4 of Account File **************************************
	//'Set Max Balance to 98 96 7F = 9,999,999
	L_SendBuff[0] = 0x98;
	L_SendBuff[1] = 0x96;
	L_SendBuff[2] = 0x7F;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x04, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 5 of Account File **************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x05, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 6 of Account File **************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x06, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}

	//Initialize Record 7 of Account File **************************************
	L_SendBuff[0] = 0x00;
	L_SendBuff[1] = 0x00;
	L_SendBuff[2] = 0x00;
	L_SendBuff[3] = 0x00;

	retcode = frmParent->writeRecord(0, 0x07, 0x04, 0x04, L_SendBuff);
	if (retcode != 0) 
	{
		return;
	}	
}

void ACOS_INIT::OnRadio1() 
{
	frmParent->G_AlgoRef = 1;	
}

void ACOS_INIT::OnRadio2() 
{
	frmParent->G_AlgoRef = 0;	
}
