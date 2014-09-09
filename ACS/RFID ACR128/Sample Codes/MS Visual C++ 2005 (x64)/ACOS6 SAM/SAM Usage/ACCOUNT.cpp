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
// Description : This program tests the Account commands for the ACOS
//					using the SAM keys set on the Key Management program.
//
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////
// ACCOUNT.cpp : implementation file
//

#include "stdafx.h"
#include "SAMSampleUsage.h"
#include "ACCOUNT.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


// GLOBAL VARIABLES
LONG A_MAX_BUFFER_LEN  = 256;
LONG A_INVALID_SW1SW2  = -450;

/////////////////////////////////////////////////////////////////////////////
// ACCOUNT dialog


ACCOUNT::ACCOUNT(CWnd* pParent /*=NULL*/)
	: CDialog(ACCOUNT::IDD, pParent)
{
	//{{AFX_DATA_INIT(ACCOUNT)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
}


void ACCOUNT::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(ACCOUNT)
	DDX_Control(pDX, IDC_EDIT4, m_Debit);
	DDX_Control(pDX, IDC_EDIT3, m_Credit);
	DDX_Control(pDX, IDC_EDIT2, m_Balance);
	DDX_Control(pDX, IDC_EDIT1, m_MaxBalance);
	//}}AFX_DATA_MAP
}


BOOL ACCOUNT::OnInitDialog() 
{
	CDialog::OnInitDialog();
	// TODO: Add extra initialization here
	this->frmParent =  (CSAMSampleUsageDlg *) this->GetParent();

	this->m_Credit.SetLimitText(16);
	this->m_Debit.SetLimitText(16);
	
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

BEGIN_MESSAGE_MAP(ACCOUNT, CDialog)
	//{{AFX_MSG_MAP(ACCOUNT)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// ACCOUNT message handlers

CString ACCOUNT::GetBalance(BYTE Data1, BYTE Data2, BYTE Data3)
{	int Balance;
	char buff[100];
	//Get Total Balance ***************************************************
    Balance = Data1 * 65536;

	Balance = Balance + (Data2 * 256);
	
	Balance = Balance + Data3;

	sprintf(buff, "%d ", Balance);

	return buff;
}

void ACCOUNT::OnButton1() 
{
int i, j, ctr, ArrCnt;
	char buff[100];
	CString SN, tmpstr, tmpstr2, Response, MaxBal, Bal;
	// TODO: Add your control notification handler code here
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

	//Diversify Kcf ************************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x72;
    L_SendBuff[2] = 0x02;
    L_SendBuff[3] = 0x86;
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

	//'Inquire Account *****************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0xE4;
    frmParent->G_SendBuff[2] = 0x02;  //'PIN
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x04;
	//'4 bytes reference
	frmParent->G_SendBuff[5] = 0xAA;
    frmParent->G_SendBuff[6] = 0xBB;
    frmParent->G_SendBuff[7] = 0xCC;
    frmParent->G_SendBuff[8] = 0xDD;
	

	i = 8;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 2;
    frmParent->G_SendLen = ctr;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x61) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x19))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = A_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;
	}

	//'Get Response to get result*******************************************
    Response = frmParent->GetMCUResponse(0x19);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Verify Inquire Account***********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7C;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x1D;
	//'4 bytes reference
	L_SendBuff[5] = 0xAA;
    L_SendBuff[6] = 0xBB;
    L_SendBuff[7] = 0xCC;
    L_SendBuff[8] = 0xDD;

	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 9; ctr <= (int)(((strlen(tmpstr)/2) - 1) +5); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  MAC = L_SendBuff[9...12]
		//        Transaction Type = L_SendBuff[13]
		//        Balance = L_SendBuff[14...16]
		//        ATREF = L_SendBuff[17...22]
		//        Max Balance = L_SendBuff[23...25]
		//        TTREFc = L_SendBuff[26...29]
		//        TTREFd = L_SendBuff[30...33]
	}
	
	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 34;

	
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		return;
	}
	char *p;
	CString Val1, Val2, Val3;
	Val1 = "0x" + Response.Mid(28,2);
	Val2 = "0x" + Response.Mid(30,2);
	Val3 = "0x" + Response.Mid(32,2);
	MaxBal = GetBalance((BYTE) strtol(Val1,&p,16),(BYTE) strtol(Val2,&p,16),(BYTE) strtol(Val3,&p,16));
	
	Val1 = "0x" + Response.Mid(10,2);
	Val2 = "0x" + Response.Mid(12,2);
	Val3 = "0x" + Response.Mid(14,2);
	Bal = GetBalance( (BYTE) strtol(Val1,&p,16),(BYTE) strtol(Val2,&p,16),(BYTE) strtol(Val3,&p,16));
	
	this->m_MaxBalance.SetWindowText(MaxBal);
	this->m_Balance.SetWindowText(Bal);	
}

void ACCOUNT::OnButton2() 
{
int i, j, ctr, ArrCnt;
	char buff[100];
	CString tmpstr, tmpstr2, SN, Response, Bal, MaxBal;

	
	// TODO: Add your control notification handler code here
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

	//Diversify Kcr ************************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x72;
    L_SendBuff[2] = 0x02;
    L_SendBuff[3] = 0x85;
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

	//'Inquire Account *****************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0xE4;
    frmParent->G_SendBuff[2] = 0x01;  
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x04;
	//'4 bytes reference
	frmParent->G_SendBuff[5] = 0xAA;
    frmParent->G_SendBuff[6] = 0xBB;
    frmParent->G_SendBuff[7] = 0xCC;
    frmParent->G_SendBuff[8] = 0xDD;
	

	i = 8;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 2;
    frmParent->G_SendLen = ctr;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x61) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x19))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = A_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;
	}


	//'Get Response to get result*******************************************
    Response = frmParent->GetMCUResponse(0x19);
	
	if (Response.IsEmpty())
	{
		return;
	}
	

		//'Verify Inquire Account***********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7C;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x1D;
	//'4 bytes reference
	L_SendBuff[5] = 0xAA;
    L_SendBuff[6] = 0xBB;
    L_SendBuff[7] = 0xCC;
    L_SendBuff[8] = 0xDD;

	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 9; ctr <= (int)(((strlen(tmpstr)/2) - 1)+9); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  MAC = L_SendBuff[9...12]
		//        Transaction Type = L_SendBuff[13]
		//        Balance = L_SendBuff[14...16]
		//        ATREF = L_SendBuff[17...22]
		//        Max Balance = L_SendBuff[23...25]
		//        TTREFc = L_SendBuff[26...29]
		//        TTREFd = L_SendBuff[30...33]
	}
	
	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 34;

	
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		return;
	}
	
	//'Prepare ACOS Transaction ***********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7E;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0xE2;
    L_SendBuff[4] = 0x0D;

	//'Amount to Credit
	m_Credit.GetWindowText(tmpstr);
    L_SendBuff[5] = (atoi(tmpstr)) / 65536;
	L_SendBuff[6] = (atoi(tmpstr) / 256) % 65536 % 256;
    L_SendBuff[7] = (atoi(tmpstr)) % 256;

	tmpstr = Response;
	ArrCnt = 42;
	for (ctr = 8; ctr <= 11; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  TTREFd = L_SendBuff[8...11]
	}

	ArrCnt = 16;
	for (ctr = 12; ctr <= 17; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  ATREF = L_SendBuff[12...17]
	}

	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 18;
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x61) | (frmParent->G_RecvBuff[1] != 0x0B))
	{	
		return;
	}

	//'Get Response to get Result ****************************************
	Response = frmParent->GetSAMResponse(0x0D, 0x0B);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Credit*******************************************************************
    tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)(((strlen(tmpstr)/2) - 1)); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	i = frmParent->CreditDebit(L_SendBuff,((strlen(tmpstr)/2)), 0xE2, 0x00, ""); 
	if (i!=0)
	{
		return;
	}

	//'Perform Verify Inquire Account w/ Credit Key and new ammount***************
    //'Inquire Account *****************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0xE4;
    frmParent->G_SendBuff[2] = 0x01;  //'PIN
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x04;
	//'4 bytes reference
	frmParent->G_SendBuff[5] = 0xAA;
    frmParent->G_SendBuff[6] = 0xBB;
    frmParent->G_SendBuff[7] = 0xCC;
    frmParent->G_SendBuff[8] = 0xDD;
	

	i = 8;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 2;
    frmParent->G_SendLen = ctr;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x61) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x19))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = A_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;
	}
	

	//'Get Response to get result*******************************************
    Response = frmParent->GetMCUResponse(0x19);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Verify Inquire Account***********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7C;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x1D;
	//'4 bytes reference
	L_SendBuff[5] = 0xAA;
    L_SendBuff[6] = 0xBB;
    L_SendBuff[7] = 0xCC;
    L_SendBuff[8] = 0xDD;

	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 9; ctr <= (int)(((strlen(tmpstr)/2) - 1) +9); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  MAC = L_SendBuff[9...12]
		//        Transaction Type = L_SendBuff[13]
		//        Balance = L_SendBuff[14...16]
		//        ATREF = L_SendBuff[17...22]
		//        Max Balance = L_SendBuff[23...25]
		//        TTREFc = L_SendBuff[26...29]
		//        TTREFd = L_SendBuff[30...33]
	}
	
	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 34;

	
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		return;
	}

	char *p;
	CString Val1, Val2, Val3;
	Val1 = "0x" + Response.Mid(28,2);
	Val2 = "0x" + Response.Mid(30,2);
	Val3 = "0x" + Response.Mid(32,2);
	MaxBal = GetBalance((BYTE) strtol(Val1,&p,16),(BYTE) strtol(Val2,&p,16),(BYTE) strtol(Val3,&p,16));
	
	Val1 = "0x" + Response.Mid(10,2);
	Val2 = "0x" + Response.Mid(12,2);
	Val3 = "0x" + Response.Mid(14,2);
	Bal = GetBalance( (BYTE) strtol(Val1,&p,16),(BYTE) strtol(Val2,&p,16),(BYTE) strtol(Val3,&p,16));
	
	this->m_MaxBalance.SetWindowText(MaxBal);
	this->m_Balance.SetWindowText(Bal);	
}

void ACCOUNT::OnButton3() 
{
int i, j, ctr, ArrCnt, RmnBal, DebBal;
	char buff[100];
	CString tmpstr, tmpstr2, SN, Response;
	CString Response2, Bal, MaxBal, NewBal;
	char *p;
	CString Val1, Val2, Val3;
	

	// TODO: Add your control notification handler code here
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

	//Diversify Kd ************************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x72;
    L_SendBuff[2] = 0x02;
    L_SendBuff[3] = 0x84;
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

	//'Inquire Account *****************************************************
    frmParent->ClearBuffers();
    frmParent->G_SendBuff[0] = 0x80;
    frmParent->G_SendBuff[1] = 0xE4;
    frmParent->G_SendBuff[2] = 0x00;  
    frmParent->G_SendBuff[3] = 0x00;
    frmParent->G_SendBuff[4] = 0x04;
	//'4 bytes reference
	frmParent->G_SendBuff[5] = 0xAA;
    frmParent->G_SendBuff[6] = 0xBB;
    frmParent->G_SendBuff[7] = 0xCC;
    frmParent->G_SendBuff[8] = 0xDD;
	

	i = 8;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}
	
	frmParent->DisplayOut(2, 0, buff, "MCU");
	frmParent->G_RecvLen = 2;
    frmParent->G_SendLen = ctr;

	i = frmParent->SendAPDUandDisplay(0, buff);
	if (i != SCARD_S_SUCCESS)
	{	
		frmParent->DisplayOut(1, i, "", "MCU");
		return;
	}
				
	if ((frmParent->G_RecvBuff[frmParent->G_RecvLen -2] != 0x61) | (frmParent->G_RecvBuff[frmParent->G_RecvLen -1] != 0x19))
	{
		
		sprintf(buff, "%02X %02X", frmParent->G_RecvBuff[frmParent->G_RecvLen -2],frmParent->G_RecvBuff[frmParent->G_RecvLen -1]);
		i = A_INVALID_SW1SW2;
		frmParent->DisplayOut(1, i, buff, "MCU");
		
		return;
	}

	//'Get Response to get result*******************************************
    Response2 = frmParent->GetMCUResponse(0x19);
	
	if (Response2.IsEmpty())
	{
		return;
	}
	
	//'Verify Inquire Account***********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7C;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x1D;
	//'4 bytes reference
	L_SendBuff[5] = 0xAA;
    L_SendBuff[6] = 0xBB;
    L_SendBuff[7] = 0xCC;
    L_SendBuff[8] = 0xDD;

	tmpstr = Response2;
	ArrCnt = 0;
	for (ctr = 9; ctr <= (int)(((strlen(tmpstr)/2) - 1)+9); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  MAC = L_SendBuff[9...12]
		//        Transaction Type = L_SendBuff[13]
		//        Balance = L_SendBuff[14...16]
		//        ATREF = L_SendBuff[17...22]
		//        Max Balance = L_SendBuff[23...25]
		//        TTREFc = L_SendBuff[26...29]
		//        TTREFd = L_SendBuff[30...33]
	}
	
	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 34;

	
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		return;
	}
	
	Val1 = "0x" + Response2.Mid(10,2);
	Val2 = "0x" + Response2.Mid(12,2);
	Val3 = "0x" + Response2.Mid(14,2);

	RmnBal = atoi(GetBalance((BYTE) strtol(Val1,&p,16),(BYTE) strtol(Val2,&p,16),(BYTE) strtol(Val3,&p,16)));
	
	//'Prepare ACOS Transaction ***********************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x7E;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0xE6;
    L_SendBuff[4] = 0x0D;

	//'Amount to Debit
	m_Debit.GetWindowText(tmpstr);
    L_SendBuff[5] = (atoi(tmpstr)) / 65536;
	L_SendBuff[6] = (atoi(tmpstr) / 256) % 65536 % 256;
    L_SendBuff[7] = (atoi(tmpstr)) % 256;

	tmpstr = Response2;
	ArrCnt = 42;
	for (ctr = 8; ctr <= 11; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  TTREFd = L_SendBuff[8...11]
	}

	ArrCnt = 16;
	for (ctr = 12; ctr <= 17; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  ATREF = L_SendBuff[12...17]
	}

	i = ctr-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");

	L_RecvLen = 2;
    L_SendLen = 18;
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x61) | (frmParent->G_RecvBuff[1] != 0x0B))
	{	
		return;
	}

	//'Get Response to get Result ****************************************
	Response = frmParent->GetSAMResponse(0x0D, 0x0B);
	
	if (Response.IsEmpty())
	{
		return;
	}

	//'Debit and return Debit Certificate****************************************
    tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2) - 1); ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;	
	}
	
	i = frmParent->CreditDebit(L_SendBuff, ((strlen(tmpstr)/2)), 0xE6, 0x01, Response);
	if (i!=0)
	{
		return;
	}
	
	Response = frmParent->GetMCUResponse(0x4);

	if (Response.IsEmpty())
	{
		return;
	}
	
	//'Verify Debit Certificate ************************************************
    L_SendBuff[0] = 0x80;
    L_SendBuff[1] = 0x70;
	
	if (frmParent->G_AlgoRef == 1)
	{
		L_SendBuff[2] = 0x03;  
    }
	else
	{
		L_SendBuff[2] = 0x02;
	}

	L_SendBuff[3] = 0x00;
    L_SendBuff[4] = 0x14;;
	
	tmpstr = Response;
	ArrCnt = 0;
	for (ctr = 5; ctr <= 8; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  TTREFd = L_SendBuff[5...8]
	}
	
	//'Amount last Debited from card
	m_Debit.GetWindowText(tmpstr);
    L_SendBuff[9] = (atoi(tmpstr)) / 65536;
	L_SendBuff[10] = (atoi(tmpstr) / 256) % 65536 % 256;
    L_SendBuff[11] = (atoi(tmpstr)) % 256;
	
	//'Expected New Balance after the Debit
    DebBal = RmnBal - (atoi(tmpstr));
	sprintf(buff, "%d", DebBal);

	L_SendBuff[12] = (atoi(buff)) / 65536;
	L_SendBuff[13] = (atoi(buff) / 256) % 65536 % 256;
    L_SendBuff[14] = (atoi(buff)) % 256;

	tmpstr = Response2;
	ArrCnt = 16;
	for (ctr = 15; ctr <= 20; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  ATREF = L_SendBuff[15...20]
	}

	ArrCnt = 42;
	for (ctr = 21; ctr <= 24; ctr++)
	{	
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
		
		//Note :  TTREFd = L_SendBuff[15...20]
	}

	L_RecvLen = 2;
    L_SendLen = 25;

	i = L_SendLen-1;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");
	frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, frmParent->G_RecvBuff);
	if ((frmParent->G_RecvBuff[0] != 0x90) | (frmParent->G_RecvBuff[1] != 0x00))
	{	
		return;
	}
	
	sprintf(buff, "%d", DebBal);
	this->m_Balance.SetWindowText(buff);	
}

