///////////////////////////////////////////////////////////////////////////////
//
// FORM NAME : Key Management Sample
//
// COMPANY : ADVANDCED CARD SYSTEMS, LTD
//
// AUTHOR : MALCOLM BERNARD U. SOLAÑA
//
// DATE :  01 / 30 / 2007
//
//
// Description : This program tests the SAM PIN and ACOS PIN set by
//					the Key Management program.
//
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////
// SECURITY.cpp : implementation file
//

#include "stdafx.h"
#include "SAMSampleUsage.h"
#include "SECURITY.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

// GLOBAL VARIABLES
LONG L_MAX_BUFFER_LEN  = 256;
LONG L_INVALID_SW1SW2  = -450;

/////////////////////////////////////////////////////////////////////////////
// SECURITY dialog


SECURITY::SECURITY(CWnd* pParent /*=NULL*/)
	: CDialog(SECURITY::IDD, pParent)
{
	//{{AFX_DATA_INIT(SECURITY)
	//}}AFX_DATA_INIT
}


void SECURITY::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(SECURITY)
	DDX_Control(pDX, IDC_COMBO1, m_combo);
	DDX_Control(pDX, IDC_COMBO2, m_combo2);
	DDX_Control(pDX, IDC_BUTTON5, m_ChangePIN);
	DDX_Control(pDX, IDC_BUTTON4, m_SubmitPIN);
	DDX_Control(pDX, IDC_BUTTON3, m_MA);
	DDX_Control(pDX, IDC_EDIT3, m_ACOSNewPIN);
	DDX_Control(pDX, IDC_EDIT2, m_ACOSPIN);
	DDX_Control(pDX, IDC_EDIT1, m_SAMGPIN);
	DDX_Control(pDX, IDC_RADIO2, m_rdo2);
	DDX_Control(pDX, IDC_RADIO1, m_rdo1);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(SECURITY, CDialog)
	//{{AFX_MSG_MAP(SECURITY)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	ON_BN_CLICKED(IDC_BUTTON4, OnButton4)
	ON_BN_CLICKED(IDC_BUTTON5, OnButton5)
	ON_BN_CLICKED(IDC_RADIO1, OnRadio1)
	ON_BN_CLICKED(IDC_RADIO2, OnRadio2)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// SECURITY message handlers
/////////////////////////////////////////////////////////////////////////////
// SECURITY message handlers
//Automatically Getting the context and 
//Listing down the readers.
//This function is called on the InitDialog portion (FORM_LOAD) for VB
void SECURITY::LoadReaderNames ()
{

	//This function list the SMART Card Reader/s Available
	m_combo.Clear();
	m_combo2.Clear();

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

	pch = ReaderList;

	while (*pch != 0)
	{

		m_combo2.AddString(pch);
		pch += strlen (pch) + 1;

	}

	m_combo2.SetCurSel (1);
	
}

BOOL SECURITY::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	// TODO: Add extra initialization here
		// TODO: Add extra initialization here
	this->frmParent =  (CSAMSampleUsageDlg*) this->GetParent();
	// TODO: Add extra initialization here
	
	this->m_SAMGPIN.SetWindowText("");
	this->m_ACOSNewPIN.SetWindowText("");
	this->m_ACOSPIN.SetWindowText("");

	this->m_rdo1.EnableWindow(false);
	this->m_rdo2.EnableWindow(false);

	this->m_SAMGPIN.EnableWindow(false);
	this->m_ACOSNewPIN.EnableWindow(false);
	this->m_ACOSPIN.EnableWindow(false);

	this->m_MA.EnableWindow(false);
	this->m_ChangePIN.EnableWindow(false);
	this->m_SubmitPIN.EnableWindow(false);

	this->m_SAMGPIN.SetLimitText(16);
	this->m_ACOSNewPIN.SetLimitText(16);
	this->m_ACOSPIN.SetLimitText(16);

	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void SECURITY::OnButton1() 
{
	// TODO: Add your control notification handler code here
	this->LoadReaderNames();
	
}

void SECURITY::OnButton2() 
{
// TODO: Add your control notification handler code here
	//Variables
	DWORD Protocol = 1;
	char buff[100];
	char buff1[100];
	
	//COnnecting to SAM *************************************************
	m_combo.GetLBText (m_combo2.GetCurSel (), ReaderName);
	
	if (frmParent->G_ConnActive)
	{	
		frmParent->DisplayOut(0, 0, "Already Connected", "SAM");
		return;
	}
	// 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
	RetCode = SCardConnectA(
		frmParent->G_hContext,
		ReaderName,
		SCARD_SHARE_EXCLUSIVE,
		Protocol,
        &frmParent->G_hSAMCard,
		&Protocol);

	if (RetCode != SCARD_S_SUCCESS)
	{	//Fail to Connect
		
		
		frmParent->DisplayOut(1, RetCode, "Unable to Connect to Card", "SAM");

		//i = frmParent->m_listbox.AddString ("Unable to Connect to Card");
		//frmParent->m_listbox.SetCurSel (i);
		
		return;

	}
	
	//Success in Connecting to Reader
	frmParent->IO_REQ.dwProtocol = Protocol;
	frmParent->IO_REQ.cbPciLength = sizeof (SCARD_IO_REQUEST);
	
	m_combo2.GetLBText(m_combo2.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Connection to ", buff1);
	
	frmParent->G_ConnActive = true;
	frmParent->DisplayOut(0, 0, buff, "");
	
	//Connecting to ACOS *****************************************************
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

		//i = frmParent->m_listbox.AddString ("Unable to Connect to Card");
		//frmParent->m_listbox.SetCurSel (i);

		return;

	}
	
	//Success in Connecting to Reader
	frmParent->IO_REQ.dwProtocol = Protocol;
	frmParent->IO_REQ.cbPciLength = sizeof (SCARD_IO_REQUEST);
	
	m_combo.GetLBText(m_combo.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Connection to ", buff1);
	
	
	frmParent->DisplayOut(0, 0, buff, "");
	frmParent->G_ConnActiveMCU = true;
	
	frmParent->G_AlgoRef = 1;
	this->m_rdo1.EnableWindow(true);
	this->m_rdo2.EnableWindow(true);
	this->m_rdo1.SetCheck(1);

	this->m_SAMGPIN.EnableWindow(true);

	this->m_MA.EnableWindow(true);	
}

void SECURITY::OnButton3() 
{
int i, j, ctr, ArrCnt;
	char buff[100];
	CString SN, tmpstr, tmpstr2, Challenge, RNDt;
	CString Response;

	m_SAMGPIN.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMGPIN.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Global PIN Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMGPIN.SetFocus();
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
		j+=2;
	}
	SN = buff;

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
	
	m_SAMGPIN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1)+5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	
	i = (int)(((strlen(tmpstr)/2) - 1)+5);
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");
	L_RecvLen = 2;
    L_SendLen = (((strlen(tmpstr)/2))+5);
	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
 	if (i != 0) 
	{	
		return;
	}
	

	//Diversify Kc ************************************************
    frmParent->ClearBuffers();
	L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x72;
    L_SendBuff[2] = 0x04;
    L_SendBuff[3] = 0x82;
    L_SendBuff[4] = 0x08;
    
	tmpstr = SN;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) +5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = (int)(((strlen(SN)/2) - 1) +5);
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

	//Diversify Kt ************************************************
    frmParent->ClearBuffers();
	L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x72;
    L_SendBuff[2] = 0x03;
    L_SendBuff[3] = 0x83;
    L_SendBuff[4] = 0x08;
    
	tmpstr = SN;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) +5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = (int)(((strlen(tmpstr)/2) - 1) +5);
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

	//Get Challenge ************************************************
    frmParent->ClearBuffers();
	frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0x84;
    frmParent->G_SendBuff[2] = 0x00;
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x08;

	i = 4;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0,buff, "MCU");
	frmParent->G_RecvLen = 10;
    frmParent->G_SendLen = 5;

	i = frmParent->SendAPDUandDisplay(2, buff);
	
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x90) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x00))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = L_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;

	}
	else
	{	
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		frmParent->DisplayOut(3, 0, buff, "MCU");
		
		i = 7;
		j = 0;
		for (ctr = 0; ctr <= i; ctr++)
		{
			sprintf(&buff[j], "%02X ", frmParent->G_RecvBuff[ctr]);
			j+=2;
		}

		Challenge = buff;
		
	}

	//'Prepare ACOS authentication *************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x78;
	
	if (frmParent->G_AlgoRef == 0) 
	{
		L_SendBuff[2] = 0x00;
    }
	else
	{
		L_SendBuff[2] = 0x01;
	}
	L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x08;
	
	tmpstr = Challenge;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = (int)(((strlen(tmpstr)/2) - 1) + 5);
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, i, buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 13;
	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}

	//'Get Response to get result + RNDt ********************************************
    L_SendBuff[0] = 0x00;
    L_SendBuff[1] = 0xC0;
	L_SendBuff[2] = 0x00;
    L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x10;

	i = 4;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, i, buff, "SAM");
    L_RecvLen = 0x12;
    L_SendLen = 0x05;
	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}
	else
	{
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		frmParent->DisplayOut(3, 0, buff, "SAM");

		i = (int)frmParent->G_RecvLen - 3;
		j = 0;
		for (ctr = 0; ctr <= i; ctr++)
		{
			sprintf(&buff[j], "%02X ", frmParent->G_RecvBuff[ctr]);
			j+=2;
		}

		RNDt = buff;
	}

	//'Authenticate ************************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0x82;
    frmParent->G_SendBuff[2] = 0x00;
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x10;

	tmpstr = RNDt;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &frmParent->G_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = ctr;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 10;
    frmParent->G_SendLen = 21;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x61) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x08))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = L_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;

	}

	//'Get Response to get result **********************************************
    Response = frmParent->GetMCUResponse(0x08);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Verify ACOS Authentication **********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7A;
    L_SendBuff[2] = 0x00;
    L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x08;
	
	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = (((strlen(tmpstr)/2) - 1) + 5);
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, i, buff, "SAM");
    L_RecvLen = 2;
    L_SendLen = 13;
	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}
	else
	{
		if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x90) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x00))
		{
		
			sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
			i = L_INVALID_SW1SW2;
			frmParent->DisplayOut(1, i, buff, "SAM");
		
			return;

		}
	}

	this->m_ACOSPIN.EnableWindow(true);
	this->m_SubmitPIN.EnableWindow(true);	
}

void SECURITY::OnButton4() 
{
CString tmpstr, tmpstr2, Response;
	int ArrCnt, ctr, i, j;
	char buff[100];

	m_ACOSPIN.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSPIN.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid ACOS PIN Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSPIN.SetFocus();
		return;
	}
	// TODO: Add your control notification handler code here
	//'Encrypt PIN****************************************
	L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x74;
	
	if (frmParent->G_AlgoRef == 0) 
	{
		L_SendBuff[2] = 0x00;
    }
	else
	{
		L_SendBuff[2] = 0x01;
	}
	L_SendBuff[3] = 0x01;
    L_SendBuff[4] = 0x08;

	m_ACOSPIN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0, buff, "SAM");
    L_SendLen = ctr;
	L_RecvLen = 2;
	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}

	//'Get Response to get encrypted PIN ****************************************
	Response = frmParent->GetSAMResponse(10, 0x08);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Submit Encrypted PIN ************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0x20;
    frmParent->G_SendBuff[2] = 0x06;  //'PIN
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x08;

	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &frmParent->G_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = ctr;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 2;
    frmParent->G_SendLen = i;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x90) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x00))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = L_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;
	}
	
	
	this->m_ChangePIN.EnableWindow(true);
	this->m_ACOSNewPIN.EnableWindow(true);	
}

void SECURITY::OnButton5() 
{
CString tmpstr, tmpstr2, Response;
	int ArrCnt, ctr, i, j;
	char buff[100];

	m_ACOSNewPIN.GetWindowText(buff, 100); 
	if (strlen(buff) < m_ACOSNewPIN.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid ACOS New PIN Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_ACOSNewPIN.SetFocus();
		return;
	}
	// TODO: Add your control notification handler code here
	//'Decrypt PIN *************************************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x76;
	
	if (frmParent->G_AlgoRef == 0) 
	{
		L_SendBuff[2] = 0x00;
    }
	else
	{
		L_SendBuff[2] = 0x01;
	}
	L_SendBuff[3] = 0x01;
    L_SendBuff[4] = 0x08;

	m_ACOSNewPIN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0, buff, "SAM");
    L_SendLen = ctr;
	L_RecvLen = 2;
	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}
	
	//'Get Response to get decrypted PIN ***********************************
    Response = frmParent->GetSAMResponse(10, 0x08);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Change PIN ***********************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0x24;
    frmParent->G_SendBuff[2] = 0x00;  //'PIN
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x08;

	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 5; ctr <= (int)(((strlen(tmpstr)/2) - 1) + 5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &frmParent->G_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;   
	}

	i = ctr;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 2;
    frmParent->G_SendLen = i;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x90) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x00))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = L_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;
	}	
}

void SECURITY::OnRadio1() 
{
	frmParent->G_AlgoRef = 1;	
}

void SECURITY::OnRadio2() 
{
	frmParent->G_AlgoRef = 0;	
}
