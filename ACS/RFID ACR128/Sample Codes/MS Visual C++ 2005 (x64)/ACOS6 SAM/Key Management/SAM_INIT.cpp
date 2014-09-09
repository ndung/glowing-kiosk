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
// Description : This dialog implements the SAM commands
//
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////
// SAM_INIT.cpp : implementation file
//

#include "stdafx.h"
#include "KeyManagement.h"
#include "SAM_INIT.h"
#include <shlwapi.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


// GLOBAL VARIABLES
unsigned char L_SendBuff [256];
unsigned char L_RecvBuff [256];
LONG L_SendLen;
LONG L_RecvLen;

/////////////////////////////////////////////////////////////////////////////
// SAM_INIT dialog


SAM_INIT::SAM_INIT(CWnd* pParent /*=NULL*/)
	: CDialog(SAM_INIT::IDD, pParent)
{
	//{{AFX_DATA_INIT(SAM_INIT)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
}


void SAM_INIT::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(SAM_INIT)
	DDX_Control(pDX, IDC_BUTTON1, m_button2);
	DDX_Control(pDX, IDC_BUTTON2, m_button1);
	DDX_Control(pDX, IDC_COMBO1, m_combo);
	DDX_Control(pDX, IDC_EDIT8, m_SAMKrd);
	DDX_Control(pDX, IDC_EDIT7, m_SAMKcf);
	DDX_Control(pDX, IDC_EDIT6, m_SAMKcr);
	DDX_Control(pDX, IDC_EDIT5, m_SAMKd);
	DDX_Control(pDX, IDC_EDIT4, m_SAMKt);
	DDX_Control(pDX, IDC_EDIT3, m_SAMKc);
	DDX_Control(pDX, IDC_EDIT2, m_SAMIC);
	DDX_Control(pDX, IDC_EDIT1, m_SAMGPIN);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(SAM_INIT, CDialog)
	//{{AFX_MSG_MAP(SAM_INIT)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// SAM_INIT message handlers

BOOL SAM_INIT::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	// TODO: Add extra initialization here
	this->frmParent =  (CKeyManagementDlg*) this->GetParent();
	// TODO: Add extra initialization here
	//pParent->m_listbox.AddString ("Unable to Establish Context");
	LoadReaderNames();
	
	this->m_SAMGPIN.LimitText(16);
	this->m_SAMIC.LimitText(32);
	this->m_SAMKc.LimitText(32);
	this->m_SAMKcf.LimitText(32);
	this->m_SAMKcr.LimitText(32);
	this->m_SAMKd.LimitText(32);
	this->m_SAMKrd.LimitText(32);
	this->m_SAMKt.LimitText(32);

	this->m_SAMGPIN.EnableWindow(false);
	this->m_SAMIC.EnableWindow(false);
	this->m_SAMKc.EnableWindow(false);
	this->m_SAMKcf.EnableWindow(false);
	this->m_SAMKcr.EnableWindow(false);
	this->m_SAMKd.EnableWindow(false);
	this->m_SAMKrd.EnableWindow(false);
	this->m_SAMKt.EnableWindow(false);
	
	this->m_SAMGPIN.SetTabStops(0);
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

/////////////////////////////////////////////////////////////////////////////
// SAM_INIT message handlers
//Automatically Getting the context and 
//Listing down the readers.
//This function is called on the InitDialog portion (FORM_LOAD) for VB
void SAM_INIT::LoadReaderNames ()
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

	m_combo.SetCurSel (1);
	
}

void SAM_INIT::OnButton1() 
{
// TODO: Add your control notification handler code here
	// This procedure connects the reader to the Smart Card inserted

	//Variables
	DWORD Protocol = 1;
	char buff[100];
	char buff1[100];
	m_combo.GetLBText (m_combo.GetCurSel (), ReaderName);
	
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
	
	m_combo.GetLBText(m_combo.GetCurSel(),buff1);
	sprintf(buff,"%s %s", "Successful Connection to ", buff1);
	
	frmParent->G_ConnActive = true;
	frmParent->DisplayOut(0, 0, buff, "");


	this->m_SAMGPIN.EnableWindow(true);
	this->m_SAMIC.EnableWindow(true);
	this->m_SAMKc.EnableWindow(true);
	this->m_SAMKcf.EnableWindow(true);
	this->m_SAMKcr.EnableWindow(true);
	this->m_SAMKd.EnableWindow(true);
	this->m_SAMKrd.EnableWindow(true);
	this->m_SAMKt.EnableWindow(true);	
}

void SAM_INIT::OnButton2() 
{
	char buff[100];
	DWORD Protocol = 1;
	CString tmpstr, tmpstr2;
	int i, j;
	int ctr, ArrCnt;

	// TODO: Add your control notification handler code here
	

	m_SAMGPIN.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMGPIN.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Global PIN Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMGPIN.SetFocus();
		return;
	}

	m_SAMIC.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMIC.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM IC Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMIC.SetFocus();
		return;
	}


	m_SAMKc.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMKc.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Card Key Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMKc.SetFocus();
		return;
	}

	m_SAMKt.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMKt.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Terminal Key Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMKt.SetFocus();
		return;
	}

	m_SAMKd.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMKd.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Debit Key Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMKd.SetFocus();
		return;
	}

	m_SAMKcr.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMKcr.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Credit Key Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMKcr.SetFocus();
		return;
	}

	m_SAMKcf.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMKcf.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Certify Key Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMKcf.SetFocus();
		return;
	}

	m_SAMKrd.GetWindowText(buff, 100); 
	if (strlen(buff) < m_SAMKrd.GetLimitText()) 
	{
		i = frmParent->m_listbox.AddString ("Invalid SAM Revoke Debit Key Input!");
		frmParent->m_listbox.SetCurSel (i);
		m_SAMKrd.SetFocus();
		return;
	}
	
	//Clear Card's EEPROM ***************************************************************
	frmParent->ClearBuffers();
	frmParent->G_SendBuff[0] = 0x80;
	frmParent->G_SendBuff[1] = 0x30;
	frmParent->G_SendBuff[2] = 0x00;
	frmParent->G_SendBuff[3] = 0x00;
	frmParent->G_SendBuff[4] = 0x00;
	
	i = 4;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", frmParent->G_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");
	
	frmParent->G_SendLen = 5;
	frmParent->G_RecvLen = 2;

	i = frmParent->SendAPDUSAM(frmParent->G_SendBuff, frmParent->G_SendLen, frmParent->G_RecvLen, frmParent->G_RecvBuff);
	if (i != 0) 
	{	
		return;
	}
	
	frmParent->ResetSAM();
	frmParent->DisplayOut(0, 0,"Reader is Disconnected Successfully!", "");

	//Create Master File ***************************************************************
	frmParent->DisplayOut(0, 0,"Invoke SCardConnect", "");
	
	OnButton1();

	//1. Create Master File *****************************
	L_SendBuff[0] = 0x62;
	L_SendBuff[1] = 0xC;
	L_SendBuff[2] = 0x80;
	L_SendBuff[3] = 0x2;
	L_SendBuff[4] = 0x2C;
	L_SendBuff[5] = 0x00;
	L_SendBuff[6] = 0x82;
	L_SendBuff[7] = 0x2;
	L_SendBuff[8] = 0x3F;
	L_SendBuff[9] = 0xFF;
	L_SendBuff[10] = 0x83;
	L_SendBuff[11] = 0x2;
	L_SendBuff[12] = 0x3F;
	L_SendBuff[13] = 0x00;

	if (frmParent->CreateSAMFile(0xE, L_SendBuff, 14) != 0)
	{
		return;
	}
	
	//Create EF1 to store PIN's******************************
    //FDB=0C MRL=0A NOR=01 READ=NONE WRITE=IC*********************
	L_SendBuff[0] = 0x62;
	L_SendBuff[1] = 0x19;
	L_SendBuff[2] = 0x83;
	L_SendBuff[3] = 0x2;
	L_SendBuff[4] = 0xFF;
	L_SendBuff[5] = 0xA;
	L_SendBuff[6] = 0x88;
	L_SendBuff[7] = 0x1;
	L_SendBuff[8] = 0x1;
	L_SendBuff[9] = 0x82;
	L_SendBuff[10] = 0x6;
	L_SendBuff[11] = 0xC;
	L_SendBuff[12] = 0x00;
	L_SendBuff[13] = 0x00;
	L_SendBuff[14] = 0xA;
	L_SendBuff[15] = 0x00;
	L_SendBuff[16] = 0x1;
	L_SendBuff[17] = 0x8C;
	L_SendBuff[18] = 0x8;
	L_SendBuff[19] = 0x7F;
	L_SendBuff[20] = 0xFF;
	L_SendBuff[21] = 0xFF;
	L_SendBuff[22] = 0xFF;
	L_SendBuff[23] = 0xFF;
	L_SendBuff[24] = 0x27;
	L_SendBuff[25] = 0x27;
	L_SendBuff[26] = 0xFF;

	if (frmParent->CreateSAMFile(0x1B, L_SendBuff, 27) != 0)
	{
		return;
	}

	//Set Global PIN's************************************
	L_SendBuff[0] = 0x0;
	L_SendBuff[1] = 0xDC;
	L_SendBuff[2] = 0x1;
	L_SendBuff[3] = 0x4;
	L_SendBuff[4] = 0xA;
	L_SendBuff[5] = 0x1;
	L_SendBuff[6] = 0x88;

	m_SAMGPIN.GetWindowText(tmpstr);
	ArrCnt = 0;
	i = 7;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{	
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[i]);
		ArrCnt = ArrCnt + 2;
        i = i + 1;
	}

	i = 13;
	j = 0;
	for (ctr = 0; ctr <= i; ctr++)
	{
		sprintf(&buff[j], "%02X ", L_SendBuff[ctr]);
		j+=3;
	}

	frmParent->DisplayOut(2, 0,buff, "SAM");
	
	L_SendLen = 15;
	L_RecvLen = 2;

	i = frmParent->SendAPDUSAM(L_SendBuff, L_SendLen, L_RecvLen, L_RecvBuff);
	if (i != 0) 
	{	
		return;
	}

	//Create Next DF DRT01: 1100 ***************************
	L_SendBuff[0] = 0x62;
	L_SendBuff[1] = 0x29;
	L_SendBuff[2] = 0x82;
	L_SendBuff[3] = 0x1;
	L_SendBuff[4] = 0x38;
	L_SendBuff[5] = 0x83;
	L_SendBuff[6] = 0x2;
	L_SendBuff[7] = 0x11;
	L_SendBuff[8] = 0x00;
	L_SendBuff[9] = 0x8A;
	L_SendBuff[10] = 0x1;
	L_SendBuff[11] = 0x1;
	L_SendBuff[12] = 0x8C;
	L_SendBuff[13] = 0x8;
	L_SendBuff[14] = 0x7F;
	L_SendBuff[15] = 0x3;
	L_SendBuff[16] = 0x3;
	L_SendBuff[17] = 0x3;
	L_SendBuff[18] = 0x3;
	L_SendBuff[19] = 0x3;
	L_SendBuff[20] = 0x3;
	L_SendBuff[21] = 0x3;
	L_SendBuff[22] = 0x8D;
	L_SendBuff[23] = 0x2;
	L_SendBuff[24] = 0x41;
	L_SendBuff[25] = 0x3;
	L_SendBuff[26] = 0x80;
	L_SendBuff[27] = 0x2;
	L_SendBuff[28] = 0x3;
	L_SendBuff[29] = 0x20;
	L_SendBuff[30] = 0xAB;
	L_SendBuff[31] = 0xB;
	L_SendBuff[32] = 0x84;
	L_SendBuff[33] = 0x1;
	L_SendBuff[34] = 0x88;
	L_SendBuff[35] = 0xA4;
	L_SendBuff[36] = 0x6;
	L_SendBuff[37] = 0x83;
	L_SendBuff[38] = 0x1;
	L_SendBuff[39] = 0x81;
	L_SendBuff[40] = 0x95;
	L_SendBuff[41] = 0x1;
	L_SendBuff[42] = 0xFF;

	if (frmParent->CreateSAMFile(0x2B, L_SendBuff, 43) != 0)
	{
		return;
	}

	//Create Key File EF2 1101 **************************************
    //MRL=16 NOR=08 **************************************************
	L_SendBuff[0] = 0x62;
	L_SendBuff[1] = 0x1B;
	L_SendBuff[2] = 0x82;
	L_SendBuff[3] = 0x5;
	L_SendBuff[4] = 0xC;
	L_SendBuff[5] = 0x41;
	L_SendBuff[6] = 0x00;
	L_SendBuff[7] = 0x16;
	L_SendBuff[8] = 0x8;
	L_SendBuff[9] = 0x83;
	L_SendBuff[10] = 0x2;
	L_SendBuff[11] = 0x11;
	L_SendBuff[12] = 0x1;
	L_SendBuff[13] = 0x88;
	L_SendBuff[14] = 0x1;
	L_SendBuff[15] = 0x2;
	L_SendBuff[16] = 0x8A;
	L_SendBuff[17] = 0x1;
	L_SendBuff[18] = 0x1;
	L_SendBuff[19] = 0x8C;
	L_SendBuff[20] = 0x8;
	L_SendBuff[21] = 0x7F;
	L_SendBuff[22] = 0x3;
	L_SendBuff[23] = 0x3;
	L_SendBuff[24] = 0x3;
	L_SendBuff[25] = 0x3;
	L_SendBuff[26] = 0x3;
	L_SendBuff[27] = 0x3;
	L_SendBuff[28] = 0x3;
		
	if (frmParent->CreateSAMFile(0x1D, L_SendBuff, 29) != 0)
	{
		return;
	}

	//Acquires the Global SAM PIN and assigns to Global array
	m_SAMGPIN.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
		tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &frmParent->G_GSAMGPIN[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	//Append Record To EF2, Define 8 Key Records in EF2 - Master Keys
    //1st Master key, key ID=81, key type=03, int/ext authenticate, usage counter = FF FF
	m_SAMIC.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	if (frmParent->AppendSamFile(0x81, L_SendBuff, 16))
	{
		return;
	}


	//2nd Master key, key ID=82, key type=03, int/ext authenticate, usage counter = FF FF
	m_SAMKc.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	if (frmParent->AppendSamFile(0x82, L_SendBuff, 16))
	{
		return;
	}

	//3rd Master key, key ID=83, key type=03, int/ext authenticate, usage counter = FF FF
    m_SAMKt.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	if (frmParent->AppendSamFile(0x83, L_SendBuff, 16))
	{
		return;
	}

	//4th Master key, key ID=84, key type=03, int/ext authenticate, usage counter = FF FF
    m_SAMKd.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	if (frmParent->AppendSamFile(0x84, L_SendBuff, 16))
	{
		return;
	}

	 //5th Master key, key ID=85, key type=03, int/ext authenticate, usage counter = FF FF
    m_SAMKcr.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	if (frmParent->AppendSamFile(0x85, L_SendBuff, 16))
	{
		return;
	}
	
	//'6th Master key, key ID=86, key type=03, int/ext authenticate, usage counter = FF FF
    m_SAMKcf.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}
	if (frmParent->AppendSamFile(0x86, L_SendBuff, 16))
	{
		return;
	}

	//'7th Master key, key ID=87, key type=03, int/ext authenticate, usage counter = FF FF
    m_SAMKrd.GetWindowText(tmpstr);
	ArrCnt = 0;
	for (ctr = 0; ctr <= (int)((strlen(tmpstr)/2)-1); ctr++)
	{
        tmpstr2 = tmpstr.Mid(ArrCnt, 2);
		tmpstr2 = "0x" + tmpstr2;

		sscanf(tmpstr2, "%02X", &L_SendBuff[ctr]);
		ArrCnt = ArrCnt + 2;
	}

	if (frmParent->AppendSamFile(0x87, L_SendBuff, 16))
	{
		return;
	}	
}
