/*'=======================================================================================================
'
'   Project Name :  KeyManagement
' 
'   Company      :  Advanced Card System LTD.
'
'   Author       :  Aileen Grace L. Sarte
'
'   Date         :  February 1, 2007
'
'   Description  :  This sample shows how to initialize an ACOS SAM and use it to generate keys
'                   for ACOS card, based from the card serial number.
'
'   Initial Step :  1.  Press List Readers (SAM Initialization).
'                   2.  Choose the SAM reader where you insert your SAM card.
'                   3.  Press Connect.
'                   4.  Enter 8 bytes global PIN (hex format).
'                   5.  If you haven't initialize the SAM card yet, select algorithm to use (DES/3DES).
'                          otherwise proceed to step 8 to initialize keys of new ACOS card.
'                   6.  Enter 16 bytes Master Key to be use for key generation of
'                          each type of ACOS Keys (e.g IC, PIN, Card Key....).
'                   7.  Press Initialize SAM button.
'                   8.  Press ACOS Card Initialization Tab.
'                   9.  Press List Readers (ACOS Card Initialization).
'                   10. Choose the slot reader where you insert your ACOS card.
'                   11. Press Connect.
'                   12. Select Algorithm Reference to use (DES/3DES)
'                   13. Press Generate Keys.
'                   14. Press Save Keys.
'
'   NOTE:
'                   Please note that once the ACOS card have been initialize or the IC was changed
'                   from it's default key (0x41 0x43 0x4F 0x53 0x54 0x45 0x53 0x54) it is not possible to
'                   save keys unless you change it's IC key back to default value.
'
'   Revision     :
'
'               Name                    Date            Brief Description of Revision
'
'=======================================================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace KeyManagement
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		//Variable Declaration
		int G_retCode, G_hContext, G_hCardSAM, G_hCard, G_Protocol;
		ModWinsCard.SCARD_IO_REQUEST G_ioRequest = new ModWinsCard.SCARD_IO_REQUEST();
		byte G_AlgoRef; 
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		int RecvLen;
		
		internal System.Windows.Forms.ListBox lst_Log;
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox txtKrd;
		internal System.Windows.Forms.TextBox txtKcf;
		internal System.Windows.Forms.TextBox txtKcr;
		internal System.Windows.Forms.TextBox txtKd;
		internal System.Windows.Forms.TextBox txtKt;
		internal System.Windows.Forms.TextBox txtKc;
		internal System.Windows.Forms.TextBox txt_IC;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox txtSAMGPIN;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button cmd_InitSAM;
		internal System.Windows.Forms.Button cmd_Connect_SAM;
		internal System.Windows.Forms.Button cmd_ListReaders_SAM;
		internal System.Windows.Forms.ComboBox cmbSAM;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.TextBox txt_PIN;
		internal System.Windows.Forms.Label Label16;
		internal System.Windows.Forms.Label lbl_r_Kt;
		internal System.Windows.Forms.Label lbl_Kt;
		internal System.Windows.Forms.Label lbl_r_kc;
		internal System.Windows.Forms.RadioButton rb_3DES;
		internal System.Windows.Forms.RadioButton rb_DES;
		internal System.Windows.Forms.Label lbl_Krd;
		internal System.Windows.Forms.Label lbl_Kcf;
		internal System.Windows.Forms.Label lbl_Kcr;
		internal System.Windows.Forms.Label lbl_Kd;
		internal System.Windows.Forms.Label lbl_Kc;
		internal System.Windows.Forms.Label lbl_IC;
		internal System.Windows.Forms.Label lbl_CardSN;
		internal System.Windows.Forms.Button cmd_GenerateKey;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.Label Label13;
		internal System.Windows.Forms.Label Label14;
		internal System.Windows.Forms.Label Label15;
		internal System.Windows.Forms.Label Label17;
		internal System.Windows.Forms.Label Label18;
		internal System.Windows.Forms.Button cmd_SaveKeys;
		internal System.Windows.Forms.Button cmd_Connect_SLT;
		internal System.Windows.Forms.Button cmd_ListReaders_SLT;
		internal System.Windows.Forms.ComboBox cmbSLT;
		internal System.Windows.Forms.Label lbl_r_Kd;
		internal System.Windows.Forms.Label lbl_r_Kcr;
		internal System.Windows.Forms.Label lbl_r_Kcf;
		internal System.Windows.Forms.Label lbl_r_Krd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lst_Log = new System.Windows.Forms.ListBox();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtKrd = new System.Windows.Forms.TextBox();
            this.txtKcf = new System.Windows.Forms.TextBox();
            this.txtKcr = new System.Windows.Forms.TextBox();
            this.txtKd = new System.Windows.Forms.TextBox();
            this.txtKt = new System.Windows.Forms.TextBox();
            this.txtKc = new System.Windows.Forms.TextBox();
            this.txt_IC = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtSAMGPIN = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmd_InitSAM = new System.Windows.Forms.Button();
            this.cmd_Connect_SAM = new System.Windows.Forms.Button();
            this.cmd_ListReaders_SAM = new System.Windows.Forms.Button();
            this.cmbSAM = new System.Windows.Forms.ComboBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_r_Krd = new System.Windows.Forms.Label();
            this.lbl_r_Kcf = new System.Windows.Forms.Label();
            this.lbl_r_Kcr = new System.Windows.Forms.Label();
            this.lbl_r_Kd = new System.Windows.Forms.Label();
            this.txt_PIN = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.lbl_r_Kt = new System.Windows.Forms.Label();
            this.lbl_Kt = new System.Windows.Forms.Label();
            this.lbl_r_kc = new System.Windows.Forms.Label();
            this.rb_3DES = new System.Windows.Forms.RadioButton();
            this.rb_DES = new System.Windows.Forms.RadioButton();
            this.lbl_Krd = new System.Windows.Forms.Label();
            this.lbl_Kcf = new System.Windows.Forms.Label();
            this.lbl_Kcr = new System.Windows.Forms.Label();
            this.lbl_Kd = new System.Windows.Forms.Label();
            this.lbl_Kc = new System.Windows.Forms.Label();
            this.lbl_IC = new System.Windows.Forms.Label();
            this.lbl_CardSN = new System.Windows.Forms.Label();
            this.cmd_GenerateKey = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.cmd_SaveKeys = new System.Windows.Forms.Button();
            this.cmd_Connect_SLT = new System.Windows.Forms.Button();
            this.cmd_ListReaders_SLT = new System.Windows.Forms.Button();
            this.cmbSLT = new System.Windows.Forms.ComboBox();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst_Log
            // 
            this.lst_Log.Location = new System.Drawing.Point(385, 15);
            this.lst_Log.Name = "lst_Log";
            this.lst_Log.Size = new System.Drawing.Size(369, 498);
            this.lst_Log.TabIndex = 5;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Location = new System.Drawing.Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(372, 507);
            this.TabControl1.TabIndex = 4;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(364, 481);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "SAM Initialization";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.txtKrd);
            this.GroupBox1.Controls.Add(this.txtKcf);
            this.GroupBox1.Controls.Add(this.txtKcr);
            this.GroupBox1.Controls.Add(this.txtKd);
            this.GroupBox1.Controls.Add(this.txtKt);
            this.GroupBox1.Controls.Add(this.txtKc);
            this.GroupBox1.Controls.Add(this.txt_IC);
            this.GroupBox1.Controls.Add(this.Label10);
            this.GroupBox1.Controls.Add(this.Label9);
            this.GroupBox1.Controls.Add(this.Label8);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.txtSAMGPIN);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.cmd_InitSAM);
            this.GroupBox1.Controls.Add(this.cmd_Connect_SAM);
            this.GroupBox1.Controls.Add(this.cmd_ListReaders_SAM);
            this.GroupBox1.Controls.Add(this.cmbSAM);
            this.GroupBox1.Location = new System.Drawing.Point(6, 8);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(352, 464);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(29, 169);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(312, 24);
            this.Label3.TabIndex = 26;
            this.Label3.Text = "---------------------------SAM Master Keys---------------------------";
            // 
            // txtKrd
            // 
            this.txtKrd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKrd.Enabled = false;
            this.txtKrd.Location = new System.Drawing.Point(129, 386);
            this.txtKrd.MaxLength = 32;
            this.txtKrd.Name = "txtKrd";
            this.txtKrd.Size = new System.Drawing.Size(204, 21);
            this.txtKrd.TabIndex = 11;
            this.txtKrd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKrd_KeyPress);
            // 
            // txtKcf
            // 
            this.txtKcf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKcf.Enabled = false;
            this.txtKcf.Location = new System.Drawing.Point(129, 353);
            this.txtKcf.MaxLength = 32;
            this.txtKcf.Name = "txtKcf";
            this.txtKcf.Size = new System.Drawing.Size(204, 21);
            this.txtKcf.TabIndex = 10;
            this.txtKcf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKcf_KeyPress);
            // 
            // txtKcr
            // 
            this.txtKcr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKcr.Enabled = false;
            this.txtKcr.Location = new System.Drawing.Point(129, 324);
            this.txtKcr.MaxLength = 32;
            this.txtKcr.Name = "txtKcr";
            this.txtKcr.Size = new System.Drawing.Size(204, 21);
            this.txtKcr.TabIndex = 9;
            this.txtKcr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKcr_KeyPress);
            // 
            // txtKd
            // 
            this.txtKd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKd.Enabled = false;
            this.txtKd.Location = new System.Drawing.Point(129, 295);
            this.txtKd.MaxLength = 32;
            this.txtKd.Name = "txtKd";
            this.txtKd.Size = new System.Drawing.Size(204, 21);
            this.txtKd.TabIndex = 8;
            this.txtKd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKd_KeyPress);
            // 
            // txtKt
            // 
            this.txtKt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKt.Enabled = false;
            this.txtKt.Location = new System.Drawing.Point(129, 264);
            this.txtKt.MaxLength = 32;
            this.txtKt.Name = "txtKt";
            this.txtKt.Size = new System.Drawing.Size(204, 21);
            this.txtKt.TabIndex = 7;
            this.txtKt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKt_KeyPress);
            // 
            // txtKc
            // 
            this.txtKc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKc.Enabled = false;
            this.txtKc.Location = new System.Drawing.Point(129, 233);
            this.txtKc.MaxLength = 32;
            this.txtKc.Name = "txtKc";
            this.txtKc.Size = new System.Drawing.Size(204, 21);
            this.txtKc.TabIndex = 6;
            this.txtKc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKc_KeyPress);
            // 
            // txt_IC
            // 
            this.txt_IC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_IC.Enabled = false;
            this.txt_IC.Location = new System.Drawing.Point(129, 201);
            this.txt_IC.MaxLength = 32;
            this.txt_IC.Name = "txt_IC";
            this.txt_IC.Size = new System.Drawing.Size(204, 21);
            this.txt_IC.TabIndex = 5;
            this.txt_IC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_IC_KeyPress);
            // 
            // Label10
            // 
            this.Label10.Location = new System.Drawing.Point(22, 358);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(100, 18);
            this.Label10.TabIndex = 17;
            this.Label10.Text = "Certify Key (Kcf)";
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(22, 389);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(100, 27);
            this.Label9.TabIndex = 16;
            this.Label9.Text = "Revoke Debit Key (Krd)";
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(22, 329);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(100, 15);
            this.Label8.TabIndex = 15;
            this.Label8.Text = "Credit Key (Kcr)";
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(22, 301);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(100, 19);
            this.Label7.TabIndex = 14;
            this.Label7.Text = "Debit Key (Kd)";
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(22, 238);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(100, 18);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "Card Key (Kc)";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(22, 270);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(100, 18);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Terminal Key (Kt)";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(22, 205);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(100, 23);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "Issuer Code (IC)";
            // 
            // txtSAMGPIN
            // 
            this.txtSAMGPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSAMGPIN.Enabled = false;
            this.txtSAMGPIN.Location = new System.Drawing.Point(132, 125);
            this.txtSAMGPIN.MaxLength = 16;
            this.txtSAMGPIN.Name = "txtSAMGPIN";
            this.txtSAMGPIN.Size = new System.Drawing.Size(112, 21);
            this.txtSAMGPIN.TabIndex = 4;
            this.txtSAMGPIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSAMGPIN_KeyPress);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(24, 125);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 23);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "SAM GLOBAL PIN";
            // 
            // cmd_InitSAM
            // 
            this.cmd_InitSAM.Enabled = false;
            this.cmd_InitSAM.Location = new System.Drawing.Point(240, 424);
            this.cmd_InitSAM.Name = "cmd_InitSAM";
            this.cmd_InitSAM.Size = new System.Drawing.Size(88, 23);
            this.cmd_InitSAM.TabIndex = 12;
            this.cmd_InitSAM.Text = "Initialize SAM";
            this.cmd_InitSAM.Click += new System.EventHandler(this.cmd_InitSAM_Click);
            // 
            // cmd_Connect_SAM
            // 
            this.cmd_Connect_SAM.Enabled = false;
            this.cmd_Connect_SAM.Location = new System.Drawing.Point(235, 72);
            this.cmd_Connect_SAM.Name = "cmd_Connect_SAM";
            this.cmd_Connect_SAM.Size = new System.Drawing.Size(96, 23);
            this.cmd_Connect_SAM.TabIndex = 3;
            this.cmd_Connect_SAM.Text = "Connect";
            this.cmd_Connect_SAM.Click += new System.EventHandler(this.cmd_Connect_SAM_Click);
            // 
            // cmd_ListReaders_SAM
            // 
            this.cmd_ListReaders_SAM.Location = new System.Drawing.Point(235, 40);
            this.cmd_ListReaders_SAM.Name = "cmd_ListReaders_SAM";
            this.cmd_ListReaders_SAM.Size = new System.Drawing.Size(97, 23);
            this.cmd_ListReaders_SAM.TabIndex = 1;
            this.cmd_ListReaders_SAM.Text = "List Readers";
            this.cmd_ListReaders_SAM.Click += new System.EventHandler(this.cmd_ListReaders_SAM_Click);
            // 
            // cmbSAM
            // 
            this.cmbSAM.Location = new System.Drawing.Point(24, 40);
            this.cmbSAM.Name = "cmbSAM";
            this.cmbSAM.Size = new System.Drawing.Size(184, 21);
            this.cmbSAM.TabIndex = 2;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.GroupBox2);
            this.TabPage2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(364, 481);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "ACOS Card Initialization";
            this.TabPage2.Visible = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.lbl_r_Krd);
            this.GroupBox2.Controls.Add(this.lbl_r_Kcf);
            this.GroupBox2.Controls.Add(this.lbl_r_Kcr);
            this.GroupBox2.Controls.Add(this.lbl_r_Kd);
            this.GroupBox2.Controls.Add(this.txt_PIN);
            this.GroupBox2.Controls.Add(this.Label16);
            this.GroupBox2.Controls.Add(this.lbl_r_Kt);
            this.GroupBox2.Controls.Add(this.lbl_Kt);
            this.GroupBox2.Controls.Add(this.lbl_r_kc);
            this.GroupBox2.Controls.Add(this.rb_3DES);
            this.GroupBox2.Controls.Add(this.rb_DES);
            this.GroupBox2.Controls.Add(this.lbl_Krd);
            this.GroupBox2.Controls.Add(this.lbl_Kcf);
            this.GroupBox2.Controls.Add(this.lbl_Kcr);
            this.GroupBox2.Controls.Add(this.lbl_Kd);
            this.GroupBox2.Controls.Add(this.lbl_Kc);
            this.GroupBox2.Controls.Add(this.lbl_IC);
            this.GroupBox2.Controls.Add(this.lbl_CardSN);
            this.GroupBox2.Controls.Add(this.cmd_GenerateKey);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Controls.Add(this.Label11);
            this.GroupBox2.Controls.Add(this.Label12);
            this.GroupBox2.Controls.Add(this.Label13);
            this.GroupBox2.Controls.Add(this.Label14);
            this.GroupBox2.Controls.Add(this.Label15);
            this.GroupBox2.Controls.Add(this.Label17);
            this.GroupBox2.Controls.Add(this.Label18);
            this.GroupBox2.Controls.Add(this.cmd_SaveKeys);
            this.GroupBox2.Controls.Add(this.cmd_Connect_SLT);
            this.GroupBox2.Controls.Add(this.cmd_ListReaders_SLT);
            this.GroupBox2.Controls.Add(this.cmbSLT);
            this.GroupBox2.Location = new System.Drawing.Point(7, 9);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(352, 464);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Generated Keys";
            // 
            // lbl_r_Krd
            // 
            this.lbl_r_Krd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_r_Krd.Location = new System.Drawing.Point(226, 384);
            this.lbl_r_Krd.Name = "lbl_r_Krd";
            this.lbl_r_Krd.Size = new System.Drawing.Size(112, 23);
            this.lbl_r_Krd.TabIndex = 33;
            this.lbl_r_Krd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_r_Kcf
            // 
            this.lbl_r_Kcf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_r_Kcf.Location = new System.Drawing.Point(226, 352);
            this.lbl_r_Kcf.Name = "lbl_r_Kcf";
            this.lbl_r_Kcf.Size = new System.Drawing.Size(112, 23);
            this.lbl_r_Kcf.TabIndex = 32;
            this.lbl_r_Kcf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_r_Kcr
            // 
            this.lbl_r_Kcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_r_Kcr.Location = new System.Drawing.Point(226, 320);
            this.lbl_r_Kcr.Name = "lbl_r_Kcr";
            this.lbl_r_Kcr.Size = new System.Drawing.Size(112, 23);
            this.lbl_r_Kcr.TabIndex = 31;
            this.lbl_r_Kcr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_r_Kd
            // 
            this.lbl_r_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_r_Kd.Location = new System.Drawing.Point(226, 288);
            this.lbl_r_Kd.Name = "lbl_r_Kd";
            this.lbl_r_Kd.Size = new System.Drawing.Size(112, 23);
            this.lbl_r_Kd.TabIndex = 30;
            this.lbl_r_Kd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_PIN
            // 
            this.txt_PIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_PIN.Location = new System.Drawing.Point(113, 133);
            this.txt_PIN.MaxLength = 16;
            this.txt_PIN.Name = "txt_PIN";
            this.txt_PIN.Size = new System.Drawing.Size(112, 21);
            this.txt_PIN.TabIndex = 16;
            // 
            // Label16
            // 
            this.Label16.Location = new System.Drawing.Point(16, 141);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(88, 14);
            this.Label16.TabIndex = 66;
            this.Label16.Text = "ACOS Card PIN";
            // 
            // lbl_r_Kt
            // 
            this.lbl_r_Kt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_r_Kt.Location = new System.Drawing.Point(226, 256);
            this.lbl_r_Kt.Name = "lbl_r_Kt";
            this.lbl_r_Kt.Size = new System.Drawing.Size(112, 23);
            this.lbl_r_Kt.TabIndex = 29;
            this.lbl_r_Kt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kt
            // 
            this.lbl_Kt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kt.Location = new System.Drawing.Point(114, 256);
            this.lbl_Kt.Name = "lbl_Kt";
            this.lbl_Kt.Size = new System.Drawing.Size(112, 23);
            this.lbl_Kt.TabIndex = 23;
            this.lbl_Kt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_r_kc
            // 
            this.lbl_r_kc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_r_kc.Location = new System.Drawing.Point(226, 222);
            this.lbl_r_kc.Name = "lbl_r_kc";
            this.lbl_r_kc.Size = new System.Drawing.Size(112, 23);
            this.lbl_r_kc.TabIndex = 28;
            this.lbl_r_kc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rb_3DES
            // 
            this.rb_3DES.Location = new System.Drawing.Point(219, 160);
            this.rb_3DES.Name = "rb_3DES";
            this.rb_3DES.Size = new System.Drawing.Size(104, 24);
            this.rb_3DES.TabIndex = 18;
            this.rb_3DES.Text = "3 DES";
            this.rb_3DES.CheckedChanged += new System.EventHandler(this.rb_3DES_CheckedChanged);
            // 
            // rb_DES
            // 
            this.rb_DES.Location = new System.Drawing.Point(115, 160);
            this.rb_DES.Name = "rb_DES";
            this.rb_DES.Size = new System.Drawing.Size(104, 24);
            this.rb_DES.TabIndex = 17;
            this.rb_DES.Text = "DES";
            this.rb_DES.CheckedChanged += new System.EventHandler(this.rb_DES_CheckedChanged);
            // 
            // lbl_Krd
            // 
            this.lbl_Krd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Krd.Location = new System.Drawing.Point(114, 384);
            this.lbl_Krd.Name = "lbl_Krd";
            this.lbl_Krd.Size = new System.Drawing.Size(112, 23);
            this.lbl_Krd.TabIndex = 27;
            this.lbl_Krd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kcf
            // 
            this.lbl_Kcf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kcf.Location = new System.Drawing.Point(114, 352);
            this.lbl_Kcf.Name = "lbl_Kcf";
            this.lbl_Kcf.Size = new System.Drawing.Size(112, 23);
            this.lbl_Kcf.TabIndex = 26;
            this.lbl_Kcf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kcr
            // 
            this.lbl_Kcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kcr.Location = new System.Drawing.Point(114, 320);
            this.lbl_Kcr.Name = "lbl_Kcr";
            this.lbl_Kcr.Size = new System.Drawing.Size(112, 23);
            this.lbl_Kcr.TabIndex = 25;
            this.lbl_Kcr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kd
            // 
            this.lbl_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kd.Location = new System.Drawing.Point(114, 288);
            this.lbl_Kd.Name = "lbl_Kd";
            this.lbl_Kd.Size = new System.Drawing.Size(112, 23);
            this.lbl_Kd.TabIndex = 24;
            this.lbl_Kd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kc
            // 
            this.lbl_Kc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kc.Location = new System.Drawing.Point(114, 222);
            this.lbl_Kc.Name = "lbl_Kc";
            this.lbl_Kc.Size = new System.Drawing.Size(112, 23);
            this.lbl_Kc.TabIndex = 22;
            this.lbl_Kc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IC
            // 
            this.lbl_IC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_IC.Location = new System.Drawing.Point(114, 188);
            this.lbl_IC.Name = "lbl_IC";
            this.lbl_IC.Size = new System.Drawing.Size(223, 23);
            this.lbl_IC.TabIndex = 21;
            this.lbl_IC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CardSN
            // 
            this.lbl_CardSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CardSN.Location = new System.Drawing.Point(114, 100);
            this.lbl_CardSN.Name = "lbl_CardSN";
            this.lbl_CardSN.Size = new System.Drawing.Size(223, 23);
            this.lbl_CardSN.TabIndex = 52;
            this.lbl_CardSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmd_GenerateKey
            // 
            this.cmd_GenerateKey.Enabled = false;
            this.cmd_GenerateKey.Location = new System.Drawing.Point(152, 427);
            this.cmd_GenerateKey.Name = "cmd_GenerateKey";
            this.cmd_GenerateKey.Size = new System.Drawing.Size(88, 23);
            this.cmd_GenerateKey.TabIndex = 19;
            this.cmd_GenerateKey.Text = "Generate Key";
            this.cmd_GenerateKey.Click += new System.EventHandler(this.cmd_GenerateKey_Click);
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(15, 356);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(93, 18);
            this.Label6.TabIndex = 41;
            this.Label6.Text = "Certify Key (Kcf)";
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(15, 386);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(93, 27);
            this.Label11.TabIndex = 40;
            this.Label11.Text = "Revoke Debit Key (Krd)";
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(15, 324);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(85, 15);
            this.Label12.TabIndex = 39;
            this.Label12.Text = "Credit Key (Kcr)";
            // 
            // Label13
            // 
            this.Label13.Location = new System.Drawing.Point(15, 291);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(77, 19);
            this.Label13.TabIndex = 38;
            this.Label13.Text = "Debit Key (Kd)";
            // 
            // Label14
            // 
            this.Label14.Location = new System.Drawing.Point(15, 226);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(77, 17);
            this.Label14.TabIndex = 37;
            this.Label14.Text = "Card Key (Kc)";
            // 
            // Label15
            // 
            this.Label15.Location = new System.Drawing.Point(15, 257);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(93, 23);
            this.Label15.TabIndex = 36;
            this.Label15.Text = "Terminal Key (Kt)";
            // 
            // Label17
            // 
            this.Label17.Location = new System.Drawing.Point(15, 196);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(93, 23);
            this.Label17.TabIndex = 34;
            this.Label17.Text = "Issuer Code (IC)";
            // 
            // Label18
            // 
            this.Label18.Location = new System.Drawing.Point(15, 103);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(108, 23);
            this.Label18.TabIndex = 30;
            this.Label18.Text = "Card Serial Number";
            // 
            // cmd_SaveKeys
            // 
            this.cmd_SaveKeys.Enabled = false;
            this.cmd_SaveKeys.Location = new System.Drawing.Point(248, 427);
            this.cmd_SaveKeys.Name = "cmd_SaveKeys";
            this.cmd_SaveKeys.Size = new System.Drawing.Size(88, 23);
            this.cmd_SaveKeys.TabIndex = 20;
            this.cmd_SaveKeys.Text = "Save Keys";
            this.cmd_SaveKeys.Click += new System.EventHandler(this.cmd_SaveKeys_Click);
            // 
            // cmd_Connect_SLT
            // 
            this.cmd_Connect_SLT.Location = new System.Drawing.Point(240, 61);
            this.cmd_Connect_SLT.Name = "cmd_Connect_SLT";
            this.cmd_Connect_SLT.Size = new System.Drawing.Size(96, 23);
            this.cmd_Connect_SLT.TabIndex = 15;
            this.cmd_Connect_SLT.Text = "Connect";
            this.cmd_Connect_SLT.Click += new System.EventHandler(this.cmd_Connect_SLT_Click);
            // 
            // cmd_ListReaders_SLT
            // 
            this.cmd_ListReaders_SLT.Location = new System.Drawing.Point(240, 29);
            this.cmd_ListReaders_SLT.Name = "cmd_ListReaders_SLT";
            this.cmd_ListReaders_SLT.Size = new System.Drawing.Size(97, 23);
            this.cmd_ListReaders_SLT.TabIndex = 13;
            this.cmd_ListReaders_SLT.Text = "List Readers";
            this.cmd_ListReaders_SLT.Click += new System.EventHandler(this.cmd_ListReaders_SLT_Click);
            // 
            // cmbSLT
            // 
            this.cmbSLT.Location = new System.Drawing.Point(29, 29);
            this.cmbSLT.Name = "cmbSLT";
            this.cmbSLT.Size = new System.Drawing.Size(184, 21);
            this.cmbSLT.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(760, 525);
            this.Controls.Add(this.lst_Log);
            this.Controls.Add(this.TabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Key Management Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void cmd_ListReaders_SAM_Click(object sender, System.EventArgs e)
		{
			//Initialize List of Available Readers
			int pcchReaders = 0;
			int indx = 0;
			string rName = "";

			//Establish Context
			G_retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER,0,0,ref G_hContext);
			
			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SAM SCardEstablishContext Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SAM SCardEstablishContext Success");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}

			//Get readers buffer len
			G_retCode = ModWinsCard.SCardListReaders(G_hContext,null,null,ref pcchReaders);

			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SAM SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}

			//Set reader buffer size
			byte[] ReadersList = new byte[pcchReaders];

			// Fill reader list
			G_retCode = ModWinsCard.SCardListReaders(G_hContext,null ,ReadersList, ref pcchReaders);
				
			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SAM SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SAM SCardListReaders Success");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}

            rName = "";
            //Convert reader buffer to string
			while(ReadersList[indx] != 0)
			{
				
				while(ReadersList[indx] != 0)
				{
					rName = rName + (char)ReadersList[indx];		
					indx = indx + 1;
				}
				
				//Add reader name to list
				cmbSAM.Items.Add(rName);
				rName = "";
				indx = indx + 1;
				
			}

			if( cmbSAM.Items.Count > 0)
			{
				cmbSAM.SelectedIndex = 0;
				cmd_Connect_SAM.Enabled = true;
			}
	
		}

		private void cmd_Connect_SAM_Click(object sender, System.EventArgs e)
		{
			//Establish SAM Reader Connection

			//Disconnect
			G_retCode = ModWinsCard.SCardDisconnect(G_hCardSAM, ModWinsCard.SCARD_UNPOWER_CARD);

			//Connect
			G_retCode = ModWinsCard.SCardConnect(G_hContext, cmbSAM.Text, ModWinsCard.SCARD_SHARE_EXCLUSIVE,
				ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1,
				ref G_hCardSAM, ref G_Protocol);

			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SAM SCardConnect Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SAM SCardConnect Success");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}
	        

			// Enable PIN and Master Keys Inputbox
			txtSAMGPIN.Enabled = true;

			rb_DES.Enabled = true;
			rb_3DES.Enabled = true;

			txt_IC.Enabled = true;
			txtKc.Enabled = true;
			txtKcf.Enabled = true;
			txtKcr.Enabled = true;
			txtKd.Enabled = true;
			txtKrd.Enabled = true;
			txtKt.Enabled = true;

			cmd_InitSAM.Enabled = true;
		}

		private void cmd_ListReaders_SLT_Click(object sender, System.EventArgs e)
		{
			//Initialize List of Available Readers
			int pcchReaders = 0;
			int indx = 0;
			string rName = "";

			//Establish Context
			G_retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER,0,0,ref G_hContext);
			
			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SCardEstablishContext Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SCardEstablishContext Success");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}

			//Get readers buffer len
			G_retCode = ModWinsCard.SCardListReaders(G_hContext,null,null,ref pcchReaders);

			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}

			//Set reader buffer size
			byte[] ReadersList = new byte[pcchReaders];

			// Fill reader list
			G_retCode = ModWinsCard.SCardListReaders(G_hContext,null ,ReadersList, ref pcchReaders);
				
			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SCardListReaders Success");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}

			//Convert reader buffer to string
			while(ReadersList[indx] != 0)
			{
				
				while(ReadersList[indx] != 0)
				{
					rName = rName + (char)ReadersList[indx];		
					indx = indx + 1;
				}
				
				//Add reader name to list
				cmbSLT.Items.Add(rName);
				rName = "";
				indx = indx + 1;
				
			}

			if( cmbSLT.Items.Count > 0)
			{
				cmbSLT.SelectedIndex = 0;
				cmd_Connect_SLT.Enabled = true;
			}
		}

		private void cmd_Connect_SLT_Click(object sender, System.EventArgs e)
		{
			//Establish Reader Connection

			//Disconnect
			G_retCode = ModWinsCard.SCardDisconnect(G_hCard, ModWinsCard.SCARD_UNPOWER_CARD);

			//Connect
			G_retCode = ModWinsCard.SCardConnect(G_hContext, cmbSLT.Text, ModWinsCard.SCARD_SHARE_EXCLUSIVE,
				ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1,
				ref G_hCard, ref G_Protocol);

			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SCardConnect Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SCardConnect Success");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}
	        
			//Enable buttons
			cmd_GenerateKey.Enabled = true;
			cmd_SaveKeys.Enabled = true;
		}


		public bool Send_APDU_SAM(ref byte[] SendBuff, int SendLen, ref int RecvLen, ref byte[] RecvBuff)
		{
			//Send APDU to SAM Reader

			G_ioRequest.dwProtocol = G_Protocol;
			G_ioRequest.cbPciLength = 8; 

			G_retCode = ModWinsCard.SCardTransmit(G_hCardSAM, ref G_ioRequest, ref SendBuff[0], SendLen, ref G_ioRequest, 
				ref RecvBuff[0], ref RecvLen);

			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SAM SCardTransmit Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
											
			}
			return(true);
		}

		private void cmd_InitSAM_Click(object sender, System.EventArgs e)
		{
			// Check if null and check length if correct for txtSAMGPIN
			if( txtSAMGPIN.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtSAMGPIN.Focus();
				return;
			}
        
			if (txtSAMGPIN.Text.Length != 16)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtSAMGPIN.Focus();
				return;
			}

			// Check if null and check length if correct for txt_IC
			if( txt_IC.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txt_IC.Focus();
				return;
			}
        
			if (txt_IC.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txt_IC.Focus();
				return;
			}
				
			// Check if null and check length if correct for txtKc
			if( txtKc.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKc.Focus();
				return;
			}
        
			if (txtKc.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKc.Focus();
				return;
			}
			
			// Check if null and check length if correct for txtKt
			if( txtKt.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKt.Focus();
				return;
			}
        
			if (txtKt.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKt.Focus();
				return;
			}

			// Check if null and check length if correct for txtKd
			if( txtKd.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKd.Focus();
				return;
			}
        
			if (txtKd.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKd.Focus();
				return;
			}

			// Check if null and check length if correct for txtKcr
			if( txtKcr.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKcr.Focus();
				return;
			}
        
			if (txtKcr.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKcr.Focus();
				return;
			}

			// Check if null and check length if correct for txtKcf
			if( txtKcf.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKcf.Focus();
				return;
			}
        
			if (txtKcf.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKcf.Focus();
				return;
			}

			// Check if null and check length if correct for txtKrd
			if( txtKrd.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKrd.Focus();
				return;
			}
        
			if (txtKrd.Text.Length != 32)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txtKrd.Focus();
				return;
			}


			// Clear Card's EEPROM
					
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x30;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x0;

			lst_Log.Items.Add("SAM < 80 30 00 00 00");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == false)
			{
				lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				return;
			}
			else
			{
				lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}

			 
			// Reset
			cmd_Connect_SAM_Click(sender, e);

			//Create MF
            
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0xE;
			SendBuff[5] = 0x62;
			SendBuff[6] = 0xC;
			SendBuff[7] = 0x80;
			SendBuff[8] = 0x2;
			SendBuff[9] = 0x2C;
			SendBuff[10] = 0x0;
			SendBuff[11] = 0x82;
			SendBuff[12] = 0x2;
			SendBuff[13] = 0x3F;
			SendBuff[14] = 0xFF;
			SendBuff[15] = 0x83;
			SendBuff[16] = 0x2;
			SendBuff[17] = 0x3F;
			SendBuff[18] = 0x0;
	
			lst_Log.Items.Add("SAM < 00 E0 00 00 0E");
			lst_Log.Items.Add("    <<62 0C 80 02 2C 00 82 02 02 3F FF 83 02 3F 00");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
	
			RecvLen = 2;
	
			if (Send_APDU_SAM(ref SendBuff, 19, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
			
			
			// Create EF1 to store PIN's
			// FDB=0C MRL=0A NOR=01 READ=NONE WRITE=IC
				
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x1B;
			SendBuff[5] = 0x62;
			SendBuff[6] = 0x19;
			SendBuff[7] = 0x83;
			SendBuff[8] = 0x2;
			SendBuff[9] = 0xFF;
			SendBuff[10] = 0xA;
			SendBuff[11] = 0x88;
			SendBuff[12] = 0x1;
			SendBuff[13] = 0x1;
			SendBuff[14] = 0x82;
			SendBuff[15] = 0x6;
			SendBuff[16] = 0xC;
			SendBuff[17] = 0x0;
			SendBuff[18] = 0x0;
			SendBuff[19] = 0xA;
			SendBuff[20] = 0x0;
			SendBuff[21] = 0x1;
			SendBuff[22] = 0x8C;
			SendBuff[23] = 0x8;
			SendBuff[24] = 0x7F;
			SendBuff[25] = 0xFF;
			SendBuff[26] = 0xFF;
			SendBuff[27] = 0xFF;
			SendBuff[28] = 0xFF;
			SendBuff[29] = 0x27;
			SendBuff[30] = 0x27;
			SendBuff[31] = 0xFF;
	
			lst_Log.Items.Add("SAM < 00 E0 00 00 1B");
			lst_Log.Items.Add("    <<62 19 83 02 FF 0A 88 01 01 82 06 0C 00 00 0A 00");
			lst_Log.Items.Add("    <<01 8C 08 7F FF FF FF FF 27 27 FF");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
	
			RecvLen = 2;
	
			if(Send_APDU_SAM(ref SendBuff, 32, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}				
	
	
			// Set Global PIN's
				
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xDC;
			SendBuff[2] = 0x1;
			SendBuff[3] = 0x4;
			SendBuff[4] = 0xA;
			SendBuff[5] = 0x1;
			SendBuff[6] = 0x88;
			SendBuff[7] = Convert.ToByte(txtSAMGPIN.Text.Substring(0,2),16); 
			SendBuff[8] = Convert.ToByte(txtSAMGPIN.Text.Substring(2, 2),16);
			SendBuff[9] = Convert.ToByte(txtSAMGPIN.Text.Substring(4, 2),16);
			SendBuff[10] = Convert.ToByte(txtSAMGPIN.Text.Substring(6, 2),16);
			SendBuff[11] = Convert.ToByte(txtSAMGPIN.Text.Substring(8, 2),16);
			SendBuff[12] = Convert.ToByte(txtSAMGPIN.Text.Substring(10, 2),16);
			SendBuff[13] = Convert.ToByte(txtSAMGPIN.Text.Substring(12, 2),16);
			SendBuff[14] = Convert.ToByte(txtSAMGPIN.Text.Substring(14, 2),16);
	
			lst_Log.Items.Add("SAM < 00 DC 01 04 0A");
			lst_Log.Items.Add("    <<01 88 " + txtSAMGPIN.Text.Substring(0, 2) + " "
											+ txtSAMGPIN.Text.Substring(2, 2) + " " 
											+ txtSAMGPIN.Text.Substring(4, 2) + " " 
											+ txtSAMGPIN.Text.Substring(6, 2) + " " 
											+ txtSAMGPIN.Text.Substring(8, 2) + " " 
											+ txtSAMGPIN.Text.Substring(10, 2) + " "
											+ txtSAMGPIN.Text.Substring(12, 2) + " "
											+ txtSAMGPIN.Text.Substring(14, 2));
	
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
	
			RecvLen = 2;
	
			if( Send_APDU_SAM(ref SendBuff, 15, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			
			//Create Next DF DRT01: 1100

			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2B;
			SendBuff[5] = 0x62;
			SendBuff[6] = 0x29;
			SendBuff[7] = 0x82;
			SendBuff[8] = 0x1;
			SendBuff[9] = 0x38;
			SendBuff[10] = 0x83;
			SendBuff[11] = 0x2;
			SendBuff[12] = 0x11;
			SendBuff[13] = 0x0;
			SendBuff[14] = 0x8A;
			SendBuff[15] = 0x1;
			SendBuff[16] = 0x1;
			SendBuff[17] = 0x8C;
			SendBuff[18] = 0x8;
			SendBuff[19] = 0x7F;
			SendBuff[20] = 0x3;
			SendBuff[21] = 0x3;
			SendBuff[22] = 0x3;
			SendBuff[23] = 0x3;
			SendBuff[24] = 0x3;
			SendBuff[25] = 0x3;
			SendBuff[26] = 0x3;
			SendBuff[27] = 0x8D;
			SendBuff[28] = 0x2;
			SendBuff[29] = 0x41;
			SendBuff[30] = 0x3;
			SendBuff[31] = 0x80;
			SendBuff[32] = 0x2;
			SendBuff[33] = 0x3;
			SendBuff[34] = 0x20;
			SendBuff[35] = 0xAB;
			SendBuff[36] = 0xB;
			SendBuff[37] = 0x84;
			SendBuff[38] = 0x1;
			SendBuff[39] = 0x88;
			SendBuff[40] = 0xA4;
			SendBuff[41] = 0x6;
			SendBuff[42] = 0x83;
			SendBuff[43] = 0x1;
			SendBuff[44] = 0x81;
			SendBuff[45] = 0x95;
			SendBuff[46] = 0x1;
			SendBuff[47] = 0xFF;

			lst_Log.Items.Add("SAM < 00 E0 00 00 2B");
			lst_Log.Items.Add("    <<62 29 82 01 38 83 02 11 00 8A 01 01 8C 08 7F 03");
			lst_Log.Items.Add("    <<03 03 03 03 03 03 8D 02 41 03 20 AB 0B 84 01 88");
			lst_Log.Items.Add("    <<A4 06 83 01 81 95 01 FF");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
	
			RecvLen = 2;
	
			if (Send_APDU_SAM(ref SendBuff, 48, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
	
	
			//Create Key File EF2 1101
			//MRL=16 NOR=08

			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x1D;
			SendBuff[5] = 0x62;
			SendBuff[6] = 0x1B;
			SendBuff[7] = 0x82;
			SendBuff[8] = 0x5;
			SendBuff[9] = 0xC;;
			SendBuff[10] = 0x41;
			SendBuff[11] = 0x0;
			SendBuff[12] = 0x16;
			SendBuff[13] = 0x8;
			SendBuff[14] = 0x83;
			SendBuff[15] = 0x2;
			SendBuff[16] = 0x11;
			SendBuff[17] = 0x1;
			SendBuff[18] = 0x88;
			SendBuff[19] = 0x1;
			SendBuff[20] = 0x2;
			SendBuff[21] = 0x8A;
			SendBuff[22] = 0x1;
			SendBuff[23] = 0x1;
			SendBuff[24] = 0x8C;
			SendBuff[25] = 0x8;
			SendBuff[26] = 0x7F;
			SendBuff[27] = 0x3;
			SendBuff[28] = 0x3;
			SendBuff[29] = 0x3;
			SendBuff[30] = 0x3;
			SendBuff[31] = 0x3;
			SendBuff[32] = 0x3;
			SendBuff[33] = 0x3;

			lst_Log.Items.Add("SAM < 00 E0 00 00 1D");
			lst_Log.Items.Add("    <<62 1B 82 05 0C 41 00 16 08 83 02 11 01 88 01 02");
			lst_Log.Items.Add("    <<8A 01 01 8C 08 7F 03 03 03 03 03 03 03");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 34, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}

			
			//Append Record To EF2, Define 8 Key Records in EF2 - Master Keys
			//1st Master key, key ID=81, key type=03, int/ext authenticate, usage counter = FF FF
	        
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x81; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;  
			SendBuff[11] = Convert.ToByte(txt_IC.Text.Substring(0, 2),16); 
			SendBuff[12] = Convert.ToByte(txt_IC.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txt_IC.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txt_IC.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txt_IC.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txt_IC.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txt_IC.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txt_IC.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txt_IC.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txt_IC.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txt_IC.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txt_IC.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txt_IC.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txt_IC.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txt_IC.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txt_IC.Text.Substring(30, 2),16);


			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<81 03 FF FF 88 00 " 
									+ txt_IC.Text.Substring(0, 2) + " " 
									+ txt_IC.Text.Substring(2, 2) + " " 
									+ txt_IC.Text.Substring(4, 2) + " " 
									+ txt_IC.Text.Substring(6, 2) + " " 
									+ txt_IC.Text.Substring(8, 2) + " " 
									+ txt_IC.Text.Substring(10, 2) + " "
									+ txt_IC.Text.Substring(12, 2) + " " 
									+ txt_IC.Text.Substring(14, 2) + " " 
									+ txt_IC.Text.Substring(16, 2) + " " 
									+ txt_IC.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txt_IC.Text.Substring(20, 2) + " " 
									+ txt_IC.Text.Substring(22, 2) + " " 
									+ txt_IC.Text.Substring(24, 2) + " " 
									+ txt_IC.Text.Substring(26, 2) + " " 
									+ txt_IC.Text.Substring(28, 2) + " " 
									+ txt_IC.Text.Substring(30, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}  

			//2nd Master key, key ID=82, key type=03, int/ext authenticate, usage counter = FF FF
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x82; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;
			SendBuff[11] = Convert.ToByte(txtKc.Text.Substring(0, 2),16);
			SendBuff[12] = Convert.ToByte(txtKc.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txtKc.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txtKc.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txtKc.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txtKc.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txtKc.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txtKc.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txtKc.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txtKc.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txtKc.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txtKc.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txtKc.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txtKc.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txtKc.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txtKc.Text.Substring(30, 2),16);

			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<82 03 FF FF 88 00 " 
									+ txtKc.Text.Substring(0, 2) + " "
									+ txtKc.Text.Substring(2, 2) + " "
									+ txtKc.Text.Substring(4, 2) + " " 
									+ txtKc.Text.Substring(6, 2) + " " 
									+ txtKc.Text.Substring(8, 2) + " " 
									+ txtKc.Text.Substring(10, 2) + " "
									+ txtKc.Text.Substring(12, 2) + " " 
									+ txtKc.Text.Substring(14, 2) + " " 
									+ txtKc.Text.Substring(16, 2) + " " 
									+ txtKc.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txtKc.Text.Substring(20, 2) + " " 
									+ txtKc.Text.Substring(22, 2) + " " 
									+ txtKc.Text.Substring(24, 2) + " " 
									+ txtKc.Text.Substring(26, 2) + " " 
									+ txtKc.Text.Substring(28, 2) + " " 
									+ txtKc.Text.Substring(30, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			
			//3rd Master key, key ID=83, key type=03, int/ext authenticate, usage counter = FF FF
	        
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x83; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;
			SendBuff[11] = Convert.ToByte(txtKt.Text.Substring(0, 2),16);
			SendBuff[12] = Convert.ToByte(txtKt.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txtKt.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txtKt.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txtKt.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txtKt.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txtKt.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txtKt.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txtKt.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txtKt.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txtKt.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txtKt.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txtKt.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txtKt.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txtKt.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txtKt.Text.Substring(30, 2),16);

			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<83 03 FF FF 88 00 " 
									+ txtKt.Text.Substring(0, 2) + " "
									+ txtKt.Text.Substring(2, 2) + " " 
									+ txtKt.Text.Substring(4, 2) + " " 
									+ txtKt.Text.Substring(6, 2) + " " 
									+ txtKt.Text.Substring(8, 2) + " " 
									+ txtKt.Text.Substring(10, 2) + " " 
									+ txtKt.Text.Substring(12, 2) + " " 
									+ txtKt.Text.Substring(14, 2) + " " 
									+ txtKt.Text.Substring(16, 2) + " " 
									+ txtKt.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txtKt.Text.Substring(20, 2) + " " 
									+ txtKt.Text.Substring(22, 2) + " " 
									+ txtKt.Text.Substring(24, 2) + " " 
									+ txtKt.Text.Substring(26, 2) + " " 
									+ txtKt.Text.Substring(28, 2) + " " 
									+ txtKt.Text.Substring(30, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			// 4th Master key, key ID=84, key type=03, int/ext authenticate, usage counter = FF FF
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x84; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;
			SendBuff[11] = Convert.ToByte(txtKd.Text.Substring(0, 2),16);
			SendBuff[12] = Convert.ToByte(txtKd.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txtKd.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txtKd.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txtKd.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txtKd.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txtKd.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txtKd.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txtKd.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txtKd.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txtKd.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txtKd.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txtKd.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txtKd.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txtKd.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txtKd.Text.Substring(30, 2),16);

			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<84 03 FF FF 88 00 " 
									+ txtKd.Text.Substring(0, 2) + " " 
									+ txtKd.Text.Substring(2, 2) + " " 
									+ txtKd.Text.Substring(4, 2) + " " 
									+ txtKd.Text.Substring(6, 2) + " " 
									+ txtKd.Text.Substring(8, 2) + " " 
									+ txtKd.Text.Substring(10, 2) + " " 
									+ txtKd.Text.Substring(12, 2) + " " 
									+ txtKd.Text.Substring(14, 2) + " " 
									+ txtKd.Text.Substring(16, 2) + " " 
									+ txtKd.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txtKd.Text.Substring(20, 2) + " " 
									+ txtKd.Text.Substring(22, 2) + " " 
									+ txtKd.Text.Substring(24, 2) + " " 
									+ txtKd.Text.Substring(26, 2) + " " 
									+ txtKd.Text.Substring(28, 2) + " " 
									+ txtKd.Text.Substring(30, 2));


			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//5th Master key, key ID=85, key type=03, int/ext authenticate, usage counter = FF FF
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x85; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;
			SendBuff[11] = Convert.ToByte(txtKcr.Text.Substring(0, 2),16);
			SendBuff[12] = Convert.ToByte(txtKcr.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txtKcr.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txtKcr.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txtKcr.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txtKcr.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txtKcr.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txtKcr.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txtKcr.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txtKcr.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txtKcr.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txtKcr.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txtKcr.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txtKcr.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txtKcr.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txtKcr.Text.Substring(30, 2),16);

			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<85 03 FF FF 88 00 " 
									+ txtKcr.Text.Substring(0, 2) + " " 
									+ txtKcr.Text.Substring(2, 2) + " " 
									+ txtKcr.Text.Substring(4, 2) + " " 
									+ txtKcr.Text.Substring(6, 2) + " " 
									+ txtKcr.Text.Substring(8, 2) + " " 
									+ txtKcr.Text.Substring(10, 2) + " "
									+ txtKcr.Text.Substring(12, 2) + " " 
									+ txtKcr.Text.Substring(14, 2) + " " 
									+ txtKcr.Text.Substring(16, 2) + " " 
									+ txtKcr.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txtKcr.Text.Substring(20, 2) + " " 
									+ txtKcr.Text.Substring(22, 2) + " " 
									+ txtKcr.Text.Substring(24, 2) + " " 
									+ txtKcr.Text.Substring(26, 2) + " " 
									+ txtKcr.Text.Substring(28, 2) + " " 
									+ txtKcr.Text.Substring(30, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

		
			//6th Master key, key ID=86, key type=03, int/ext authenticate, usage counter = FF FF
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x86; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;
			SendBuff[11] = Convert.ToByte(txtKcf.Text.Substring(0, 2),16);
			SendBuff[12] = Convert.ToByte(txtKcf.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txtKcf.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txtKcf.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txtKcf.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txtKcf.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txtKcf.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txtKcf.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txtKcf.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txtKcf.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txtKcf.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txtKcf.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txtKcf.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txtKcf.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txtKcf.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txtKcf.Text.Substring(30, 2),16);

			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<86 03 FF FF 88 00 " 
									+ txtKcf.Text.Substring(0, 2) + " " 
									+ txtKcf.Text.Substring(2, 2) + " " 
									+ txtKcf.Text.Substring(4, 2) + " " 
									+ txtKcf.Text.Substring(6, 2) + " " 
									+ txtKcf.Text.Substring(8, 2) + " " 
									+ txtKcf.Text.Substring(10, 2) + " " 
									+ txtKcf.Text.Substring(12, 2) + " " 
									+ txtKcf.Text.Substring(14, 2) + " " 
									+ txtKcf.Text.Substring(16, 2) + " " 
									+ txtKcf.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txtKcf.Text.Substring(20, 2) + " " 
									+ txtKcf.Text.Substring(22, 2) + " " 
									+ txtKcf.Text.Substring(24, 2) + " " 
									+ txtKcf.Text.Substring(26, 2) + " " 
									+ txtKcf.Text.Substring(28, 2) + " " 
									+ txtKcf.Text.Substring(30, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//7th Master key, key ID=87, key type=03, int/ext authenticate, usage counter = FF FF
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x16;
			SendBuff[5] = 0x87; //Key ID
			SendBuff[6] = 0x3;
			SendBuff[7] = 0xFF;
			SendBuff[8] = 0xFF;
			SendBuff[9] = 0x88;
			SendBuff[10] = 0x0;
			SendBuff[11] = Convert.ToByte(txtKrd.Text.Substring(0, 2),16);
			SendBuff[12] = Convert.ToByte(txtKrd.Text.Substring(2, 2),16);
			SendBuff[13] = Convert.ToByte(txtKrd.Text.Substring(4, 2),16);
			SendBuff[14] = Convert.ToByte(txtKrd.Text.Substring(6, 2),16);
			SendBuff[15] = Convert.ToByte(txtKrd.Text.Substring(8, 2),16);
			SendBuff[16] = Convert.ToByte(txtKrd.Text.Substring(10, 2),16);
			SendBuff[17] = Convert.ToByte(txtKrd.Text.Substring(12, 2),16);
			SendBuff[18] = Convert.ToByte(txtKrd.Text.Substring(14, 2),16);
			SendBuff[19] = Convert.ToByte(txtKrd.Text.Substring(16, 2),16);
			SendBuff[20] = Convert.ToByte(txtKrd.Text.Substring(18, 2),16);
			SendBuff[21] = Convert.ToByte(txtKrd.Text.Substring(20, 2),16);
			SendBuff[22] = Convert.ToByte(txtKrd.Text.Substring(22, 2),16);
			SendBuff[23] = Convert.ToByte(txtKrd.Text.Substring(24, 2),16);
			SendBuff[24] = Convert.ToByte(txtKrd.Text.Substring(26, 2),16);
			SendBuff[25] = Convert.ToByte(txtKrd.Text.Substring(28, 2),16);
			SendBuff[26] = Convert.ToByte(txtKrd.Text.Substring(30, 2),16);

			lst_Log.Items.Add("SAM < 00 E2 00 00 16");
			lst_Log.Items.Add("    <<87 03 FF FF 88 00 " 
									+ txtKrd.Text.Substring(0, 2) + " " 
									+ txtKrd.Text.Substring(2, 2) + " " 
									+ txtKrd.Text.Substring(4, 2) + " " 
									+ txtKrd.Text.Substring(6, 2) + " " 
									+ txtKrd.Text.Substring(8, 2) + " " 
									+ txtKrd.Text.Substring(10, 2) + " " 
									+ txtKrd.Text.Substring(12, 2) + " " 
									+ txtKrd.Text.Substring(14, 2) + " " 
									+ txtKrd.Text.Substring(16, 2) + " " 
									+ txtKrd.Text.Substring(18, 2));

			lst_Log.Items.Add("    <<" + txtKrd.Text.Substring(20, 2) + " " 
									+ txtKrd.Text.Substring(22, 2) + " " 
									+ txtKrd.Text.Substring(24, 2) + " " 
									+ txtKrd.Text.Substring(26, 2) + " " 
									+ txtKrd.Text.Substring(28, 2) + " " 
									+ txtKrd.Text.Substring(30, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 27, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + String.Format("{0:x2}", RecvBuff[0]).ToUpper() + String.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
		}


		public bool Send_APDU_SLT(ref byte[] SendBuff, int SendLen, ref int RecvLen, ref byte[] RecvBuff)
		{
			//Send APDU to Slot Reader

			G_ioRequest.dwProtocol = G_Protocol;
			G_ioRequest.cbPciLength = 8;
			
			G_retCode = ModWinsCard.SCardTransmit(G_hCard, ref G_ioRequest, ref SendBuff[0], SendLen,
				ref G_ioRequest, ref RecvBuff[0], ref RecvLen);

			if (G_retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lst_Log.Items.Add("SCardTransmit Error: " + ModWinsCard.GetScardErrMsg(G_retCode));
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
			}
		
			return(true);

		}

		private void cmd_GenerateKey_Click(object sender, System.EventArgs e)
		{
			// Variable Declaration
			string SN, GeneratedKey;
			int i;
			
			//Select Issuer DF
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xA4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2;
			SendBuff[5] = 0x11;
			SendBuff[6] = 0x0;

			lst_Log.Items.Add("SAM < 00 A4 00 00 02");
			lst_Log.Items.Add("    <<11 00");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 7, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "612D")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
			


			//Submit Issuer PIN
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0x20;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x1;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(txtSAMGPIN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(txtSAMGPIN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(txtSAMGPIN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(txtSAMGPIN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(txtSAMGPIN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(txtSAMGPIN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(txtSAMGPIN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(txtSAMGPIN.Text.Substring(14, 2),16);

						
			lst_Log.Items.Add("SAM < 00 20 00 01 08");
			lst_Log.Items.Add("    <<" + txtSAMGPIN.Text.Substring(0, 2) 
										+ txtSAMGPIN.Text.Substring(2, 2) 
										+ txtSAMGPIN.Text.Substring(4, 2) 
										+ txtSAMGPIN.Text.Substring(6, 2) 
										+ txtSAMGPIN.Text.Substring(8, 2) 
										+ txtSAMGPIN.Text.Substring(10, 2) 
										+ txtSAMGPIN.Text.Substring(12, 2) 
										+ txtSAMGPIN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			
			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
			
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{ 
				return;
			}

			
			//Get Card Serial Number
			//Select FF00
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xA4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2;
			SendBuff[5] = 0xFF;
			SendBuff[6] = 0x0;

			lst_Log.Items.Add("MCU < 80 A4 00 00 02");
			lst_Log.Items.Add("    <<FF 00");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 7, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
			
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}


			//Read FF 00 to retrieve card serial number
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xB2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("MCU < 80 B2 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper()+ string.Format("{0:x2}", RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[8]).ToUpper() + string.Format("{0:x2}", RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Card Serial Number
					SN = "";

					for(i = 0; i<=7; i++)
					{
						SN = SN + string.Format("{0:x2}", RecvBuff[i]).ToUpper() + " ";
					}
					lst_Log.Items.Add("MCU >> " + SN);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[8]).ToUpper() + string.Format("{0:x2}", RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_CardSN.Text = SN.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			
			//Generate Key
			//Generate IC Using 1st SAM Master Key (KeyID=81)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x81; //KeyID
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 81 08");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2) 
										+ lbl_CardSN.Text.Substring(2, 2) 
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2) 
										+ lbl_CardSN.Text.Substring(8, 2) 
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_IC.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			
			//Generate Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x82; //KeyID
			SendBuff[4] = 0x8; 
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 82 08");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2)
										+ lbl_CardSN.Text.Substring(2, 2)
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2)
										+ lbl_CardSN.Text.Substring(8, 2)
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
			
			
			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_Kc.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}


			//If Algorithm Reference = 3DES then Generate Right Half of Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
			if (G_AlgoRef == 0)
			{

				SendBuff[0] = 0x80;
				SendBuff[1] = 0x88;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x82; //KeyID
				SendBuff[4] = 0x8;

				//compliment the card serial number to generate right half key for 3DES algorithm
				SendBuff[5] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16) ^ 0xFF);
				SendBuff[6] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16) ^ 0xFF);
				SendBuff[7] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16) ^ 0xFF);
				SendBuff[8] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16) ^ 0xFF);
				SendBuff[9] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16) ^ 0xFF);
				SendBuff[10] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16) ^ 0xFF);
				SendBuff[11] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16) ^ 0xFF);
				SendBuff[12] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16) ^ 0xFF);

				lst_Log.Items.Add("SAM < 80 88 00 82 08");
				lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[12]).ToUpper());

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}


				//Get Response to Retrieve Generated Key
		
				SendBuff[0] = 0x0;
				SendBuff[1] = 0xC0;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;

				lst_Log.Items.Add("SAM < 00 C0 00 00 08");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 10;

				if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						//Retrieve Generated Key
						GeneratedKey = "";

						for(i = 0; i <=7; i++)
						{
							GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
						}
				
						lst_Log.Items.Add("SAM >> " + GeneratedKey);
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

						lbl_r_kc.Text = GeneratedKey.Replace(" ", "");

					}
				}
				else
				{
					return;
				}
				
			}
			else
			{
				lbl_r_kc.Text = "";
			}
		
			//Generate Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x83; //KeyID
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 83 08");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2)
										+ lbl_CardSN.Text.Substring(2, 2)
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2)
										+ lbl_CardSN.Text.Substring(8, 2)
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
			
			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_Kt.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}
			

			//If Algorithm Reference = 3DES then Generate Right Half of Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
		
			if (G_AlgoRef == 0)
			{

				SendBuff[0] = 0x80;
				SendBuff[1] = 0x88;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x83; //KeyID
				SendBuff[4] = 0x8;

				//compliment the card serial number to generate right half key for 3DES algorithm
				SendBuff[5] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16) ^ 0xFF);
				SendBuff[6] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16) ^ 0xFF);
				SendBuff[7] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16) ^ 0xFF);
				SendBuff[8] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16) ^ 0xFF);
				SendBuff[9] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16) ^ 0xFF);
				SendBuff[10] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16) ^ 0xFF);
				SendBuff[11] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16) ^ 0xFF);
				SendBuff[12] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16) ^ 0xFF);

				lst_Log.Items.Add("SAM < 80 88 00 83 08");
				lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[12]).ToUpper());

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}


				//Get Response to Retrieve Generated Key
		
				SendBuff[0] = 0x0;
				SendBuff[1] = 0xC0;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;

				lst_Log.Items.Add("SAM < 00 C0 00 00 08");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 10;

				if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						//Retrieve Generated Key
						GeneratedKey = "";

						for(i = 0; i <=7; i++)
						{
							GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
						}
				
						lst_Log.Items.Add("SAM >> " + GeneratedKey);
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

						lbl_r_Kt.Text = GeneratedKey.Replace(" ", "");
					}
				}
				else
				{
					return;
				}
			}
			else
			{
				lbl_r_Kt.Text = "";
			}

			
			//Generate Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x84; //KeyID
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 84 08");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2) 
										+ lbl_CardSN.Text.Substring(2, 2) 
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2) 
										+ lbl_CardSN.Text.Substring(8, 2) 
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_Kd.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}


			//If Algorithm Reference = 3DES then Generate Right Half of Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)

			if (G_AlgoRef == 0)
			{

				SendBuff[0] = 0x80;
				SendBuff[1] = 0x88;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x84; //KeyID
				SendBuff[4] = 0x8;

				//compliment the card serial number to generate right half key for 3DES algorithm
				SendBuff[5] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16) ^ 0xFF);
				SendBuff[6] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16) ^ 0xFF);
				SendBuff[7] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16) ^ 0xFF);
				SendBuff[8] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16) ^ 0xFF);
				SendBuff[9] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16) ^ 0xFF);
				SendBuff[10] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16) ^ 0xFF);
				SendBuff[11] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16) ^ 0xFF);
				SendBuff[12] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16) ^ 0xFF);

				lst_Log.Items.Add("SAM < 80 88 00 84 08");
				lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
					+ string.Format("{0:x2}", SendBuff[12]).ToUpper());

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}

				//Get Response to Retrieve Generated Key
		
				SendBuff[0] = 0x0;
				SendBuff[1] = 0xC0;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;

				lst_Log.Items.Add("SAM < 00 C0 00 00 08");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 10;

				if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						//Retrieve Generated Key
						GeneratedKey = "";

						for(i = 0; i <=7; i++)
						{
							GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
						}
				
						lst_Log.Items.Add("SAM >> " + GeneratedKey);
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

						lbl_r_Kd.Text = GeneratedKey.Replace(" ", "");
					}
				}
				else
				{
					return;
				}
		 	
			}
			else
			{
				lbl_r_Kd.Text = "";
			}


			//Generate Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x85; //KeyID
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 85 08");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2)
										+ lbl_CardSN.Text.Substring(2, 2)
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2)
										+ lbl_CardSN.Text.Substring(8, 2)
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_Kcr.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}


			//If Algorithm Reference = 3DES then Generate Right Half of Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)

			if (G_AlgoRef == 0)
			{

				SendBuff[0] = 0x80;
				SendBuff[1] = 0x88;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x85; //KeyID
				SendBuff[4] = 0x8;

				//compliment the card serial number to generate right half key for 3DES algorithm
				SendBuff[5] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16) ^ 0xFF);
				SendBuff[6] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16) ^ 0xFF);
				SendBuff[7] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16) ^ 0xFF);
				SendBuff[8] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16) ^ 0xFF);
				SendBuff[9] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16) ^ 0xFF);
				SendBuff[10] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16) ^ 0xFF);
				SendBuff[11] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16) ^ 0xFF);
				SendBuff[12] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16) ^ 0xFF);

				lst_Log.Items.Add("SAM < 80 88 00 85 08");
				lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[12]).ToUpper());

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}

				//Get Response to Retrieve Generated Key
		
				SendBuff[0] = 0x0;
				SendBuff[1] = 0xC0;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;

				lst_Log.Items.Add("SAM < 00 C0 00 00 08");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 10;

				if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						//Retrieve Generated Key
						GeneratedKey = "";

						for(i = 0; i <=7; i++)
						{
							GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
						}
				
						lst_Log.Items.Add("SAM >> " + GeneratedKey);
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

						lbl_r_Kcr.Text = GeneratedKey.Replace(" ", "");
					}
				}
				else
				{
					return;
				}
		
			}
			else
			{
				lbl_r_Kcr.Text = "";
			}


			//Generate Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x86; //KeyID
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 86 08 ");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2)
										+ lbl_CardSN.Text.Substring(2, 2)
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2)
										+ lbl_CardSN.Text.Substring(8, 2)
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}
			
			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_Kcf.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}


			//If Algorithm Reference = 3DES then Generate Right Half of Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)

			if (G_AlgoRef == 0)
			{

				SendBuff[0] = 0x80;
				SendBuff[1] = 0x88;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x86; //KeyID
				SendBuff[4] = 0x8;

				//compliment the card serial number to generate right half key for 3DES algorithm
				SendBuff[5] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16) ^ 0xFF);
				SendBuff[6] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16) ^ 0xFF);
				SendBuff[7] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16) ^ 0xFF);
				SendBuff[8] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16) ^ 0xFF);
				SendBuff[9] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16) ^ 0xFF);
				SendBuff[10] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16) ^ 0xFF);
				SendBuff[11] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16) ^ 0xFF);
				SendBuff[12] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16) ^ 0xFF);

				lst_Log.Items.Add("SAM < 80 88 00 86 08");
				lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[12]).ToUpper());

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}

				//Get Response to Retrieve Generated Key
		
				SendBuff[0] = 0x0;
				SendBuff[1] = 0xC0;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;

				lst_Log.Items.Add("SAM < 00 C0 00 00 08");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 10;

				if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						//Retrieve Generated Key
						GeneratedKey = "";

						for(i = 0; i <=7; i++)
						{
							GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
						}
				
						lst_Log.Items.Add("SAM >> " + GeneratedKey);
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

						lbl_r_Kcf.Text = GeneratedKey.Replace(" ", "");
					}
				}
				else
				{
					return;
				}
			}
			else
			{
				lbl_r_Kcf.Text = "";
			}
		

			//Generate Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x88;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x87; //KeyID
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 88 00 87 08");
			lst_Log.Items.Add("    <<" + lbl_CardSN.Text.Substring(0, 2)
										+ lbl_CardSN.Text.Substring(2, 2)
										+ lbl_CardSN.Text.Substring(4, 2) 
										+ lbl_CardSN.Text.Substring(6, 2)
										+ lbl_CardSN.Text.Substring(8, 2)
										+ lbl_CardSN.Text.Substring(10, 2) 
										+ lbl_CardSN.Text.Substring(12, 2) 
										+ lbl_CardSN.Text.Substring(14, 2));
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//Get Response to Retrieve Generated Key
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("SAM < 00 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Generated Key
					GeneratedKey = "";

					for(i = 0; i <=7; i++)
					{
						GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + GeneratedKey);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					lbl_Krd.Text = GeneratedKey.Replace(" ", "");

				}
			}
			else
			{
				return;
			}


			//If Algorithm Reference = 3DES then Generate Right Half of Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)

			if (G_AlgoRef == 0)
			{

				SendBuff[0] = 0x80;
				SendBuff[1] = 0x88;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x87; //KeyID
				SendBuff[4] = 0x8;

				//compliment the card serial number to generate right half key for 3DES algorithm
				SendBuff[5] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(0, 2),16) ^ 0xFF);
				SendBuff[6] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(2, 2),16) ^ 0xFF);
				SendBuff[7] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(4, 2),16) ^ 0xFF);
				SendBuff[8] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(6, 2),16) ^ 0xFF);
				SendBuff[9] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(8, 2),16) ^ 0xFF);
				SendBuff[10] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(10, 2),16) ^ 0xFF);
				SendBuff[11] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(12, 2),16) ^ 0xFF);
				SendBuff[12] = Convert.ToByte(Convert.ToByte(lbl_CardSN.Text.Substring(14, 2),16) ^ 0xFF);

				lst_Log.Items.Add("SAM < 80 88 00 87 08");
				lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
											+ string.Format("{0:x2}", SendBuff[12]).ToUpper());

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}

				//Get Response to Retrieve Generated Key
		
				SendBuff[0] = 0x0;
				SendBuff[1] = 0xC0;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;

				lst_Log.Items.Add("SAM < 00 C0 00 00 08");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 10;

				if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						//Retrieve Generated Key
						GeneratedKey = "";

						for(i = 0; i <=7; i++)
						{
							GeneratedKey = GeneratedKey + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
						}
				
						lst_Log.Items.Add("SAM >> " + GeneratedKey);
						lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

						lbl_r_Krd.Text = GeneratedKey.Replace(" ", "");
					}
				}
				else
				{
					return;
				}
			}
			else
			{
				lbl_r_Krd.Text = "";
			}

		}

		private void rb_DES_CheckedChanged(object sender, System.EventArgs e)
		{
			//Set Algo Ref to DES
			G_AlgoRef = 1;
		}

		private void rb_3DES_CheckedChanged(object sender, System.EventArgs e)
		{
			//Set Algo Ref to 3DES
			G_AlgoRef = 0;
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Initialization
			rb_DES.Checked = true;
			G_AlgoRef = 1;
		}

		private void cmd_SaveKeys_Click(object sender, System.EventArgs e)
		{
			// Check User PIN
			// Check if null and check length if correct
			if(txt_PIN.Text == "")
			{
				lst_Log.Items.Add("Input Required");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txt_PIN.Focus();
				return;
			}
        
			if (txt_PIN.Text.Length != 16)
			{
				lst_Log.Items.Add("Invalid Input Length");
				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				txt_PIN.Focus();
				return;
			}

			//Update Personalization File (FF02)

			//Select FF02
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xA4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2;
			SendBuff[5] = 0xFF;
			SendBuff[6] = 0x2;

			lst_Log.Items.Add("MCU < 80 A4 00 00 02");
			lst_Log.Items.Add("    <<FF 02");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 7, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}
        
			//Submit Default IC
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x20;
			SendBuff[2] = 0x7;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = 0x41;
			SendBuff[6] = 0x43;
			SendBuff[7] = 0x4F;
			SendBuff[8] = 0x53;
			SendBuff[9] = 0x54;
			SendBuff[10] = 0x45;
			SendBuff[11] = 0x53;
			SendBuff[12] = 0x54;

			lst_Log.Items.Add("MCU < 80 20 07 00 08");
			lst_Log.Items.Add("    <<41 43 4F 53 54 45 53 54");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			//Update FF02 record 0 
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;

			if(G_AlgoRef == 0)
				SendBuff[5] = 0xFF; // INQ_AUT, TRNS_AUT, REV_DEB, DEB_PIN, DEB_MAC, Account, 3-DES, PIN_ALT Enabled
			else
                SendBuff[5] = 0xFD; // INQ_AUT, TRNS_AUT, REV_DEB, DEB_PIN, DEB_MAC, Account, 3-DES, PIN_ALT Enabled
			
			SendBuff[6] = 0x40; //PIN was encrypted and the PIN should be submitted in the submit code command must be encrypted with the current session key.
			SendBuff[7] = 0x0;  //No User File Created
			SendBuff[8] = 0x0;

			lst_Log.Items.Add("MCU < 80 D2 00 00 04");
			lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " 40 00 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//Reset
			cmd_Connect_SLT_Click(sender, e);

			//Update Card Keys (FF03)

			//Select FF03
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xA4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2;
			SendBuff[5] = 0xFF;
			SendBuff[6] = 0x3;

			lst_Log.Items.Add("MCU < 80 A4 00 00 02");
			lst_Log.Items.Add("    <<FF 03");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 7, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//Submit Default IC
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x20;
			SendBuff[2] = 0x7;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = 0x41;
			SendBuff[6] = 0x43;
			SendBuff[7] = 0x4F;
			SendBuff[8] = 0x53;
			SendBuff[9] = 0x54;
			SendBuff[10] = 0x45;
			SendBuff[11] = 0x53;
			SendBuff[12] = 0x54;

			lst_Log.Items.Add("MCU < 80 20 07 00 08");
			lst_Log.Items.Add("    <<41 43 4F 53 54 45 53 54");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			//Update FF03 record 0 (IC)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_IC.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_IC.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_IC.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_IC.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_IC.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_IC.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_IC.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_IC.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 00 00 08");
			lst_Log.Items.Add("    <<" + lbl_IC.Text.Substring(0, 2) + " " 
										+ lbl_IC.Text.Substring(2, 2) + " " 
										+ lbl_IC.Text.Substring(4, 2) + " " 
										+ lbl_IC.Text.Substring(6, 2) + " " 
										+ lbl_IC.Text.Substring(8, 2) + " " 
										+ lbl_IC.Text.Substring(10, 2) + " " 
										+ lbl_IC.Text.Substring(12, 2) + " " 
										+ lbl_IC.Text.Substring(14, 2) + " ");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			//Update FF03 record 1 (PIN)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x1;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(txt_PIN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(txt_PIN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(txt_PIN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(txt_PIN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(txt_PIN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(txt_PIN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(txt_PIN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(txt_PIN.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 01 00 08");
			lst_Log.Items.Add("    <<" + txt_PIN.Text.Substring(0, 2) + " " 
										+ txt_PIN.Text.Substring(2, 2) + " " 
										+ txt_PIN.Text.Substring(4, 2) + " " 
										+ txt_PIN.Text.Substring(6, 2) + " " 
										+ txt_PIN.Text.Substring(8, 2) + " " 
										+ txt_PIN.Text.Substring(10, 2) + " " 
										+ txt_PIN.Text.Substring(12, 2) + " " 
										+ txt_PIN.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

				
			//Update FF03 record 2 (Kc)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x2;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_Kc.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_Kc.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_Kc.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_Kc.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_Kc.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_Kc.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_Kc.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_Kc.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 02 00 08");
			lst_Log.Items.Add("    <<" + lbl_Kc.Text.Substring(0, 2) + " "
										+ lbl_Kc.Text.Substring(2, 2) + " "
										+ lbl_Kc.Text.Substring(4, 2) + " "
										+ lbl_Kc.Text.Substring(6, 2) + " "
										+ lbl_Kc.Text.Substring(8, 2) + " "
										+ lbl_Kc.Text.Substring(10, 2) + " "
										+ lbl_Kc.Text.Substring(12, 2) + " "
										+ lbl_Kc.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}


			if (G_AlgoRef == 0)
				//If Algorithm Reference = 3DES Update FF03 record 0x0C Right Half (Kc)
			{
				SendBuff[0] = 0x80;
				SendBuff[1] = 0xD2;
				SendBuff[2] = 0xC;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;
				SendBuff[5] = Convert.ToByte(lbl_r_kc.Text.Substring(0, 2),16);
				SendBuff[6] = Convert.ToByte(lbl_r_kc.Text.Substring(2, 2),16);
				SendBuff[7] = Convert.ToByte(lbl_r_kc.Text.Substring(4, 2),16);
				SendBuff[8] = Convert.ToByte(lbl_r_kc.Text.Substring(6, 2),16);
				SendBuff[9] = Convert.ToByte(lbl_r_kc.Text.Substring(8, 2),16);
				SendBuff[10] = Convert.ToByte(lbl_r_kc.Text.Substring(10, 2),16);
				SendBuff[11] = Convert.ToByte(lbl_r_kc.Text.Substring(12, 2),16);
				SendBuff[12] = Convert.ToByte(lbl_r_kc.Text.Substring(14, 2),16);

				lst_Log.Items.Add("MCU < 80 D2 0C 00 08");
				lst_Log.Items.Add("    <<" + lbl_r_kc.Text.Substring(0, 2) + " " 
											+ lbl_r_kc.Text.Substring(2, 2) + " "
											+ lbl_r_kc.Text.Substring(4, 2) + " "
											+ lbl_r_kc.Text.Substring(6, 2) + " "
											+ lbl_r_kc.Text.Substring(8, 2) + " "
											+ lbl_r_kc.Text.Substring(10, 2) + " "
											+ lbl_r_kc.Text.Substring(12, 2) + " "
											+ lbl_r_kc.Text.Substring(14, 2));

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}
			}

			//Update FF03 record 3 (Kt)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x3;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_Kt.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_Kt.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_Kt.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_Kt.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_Kt.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_Kt.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_Kt.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_Kt.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 03 00 08");
			lst_Log.Items.Add("    <<" + lbl_Kt.Text.Substring(0, 2) + " "
										+ lbl_Kt.Text.Substring(2, 2) + " "
										+ lbl_Kt.Text.Substring(4, 2) + " "
										+ lbl_Kt.Text.Substring(6, 2) + " "
										+ lbl_Kt.Text.Substring(8, 2) + " "
										+ lbl_Kt.Text.Substring(10, 2) + " "
										+ lbl_Kt.Text.Substring(12, 2) + " "
										+ lbl_Kt.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}
			else
			{
				return;
			}

			if (G_AlgoRef == 0)
				//If Algorithm Reference = 3DES Update FF03 record 0x0D Right Half (Kt)
			{
				SendBuff[0] = 0x80;
				SendBuff[1] = 0xD2;
				SendBuff[2] = 0xD;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;
				SendBuff[5] = Convert.ToByte(lbl_r_Kt.Text.Substring(0, 2),16);
				SendBuff[6] = Convert.ToByte(lbl_r_Kt.Text.Substring(2, 2),16);
				SendBuff[7] = Convert.ToByte(lbl_r_Kt.Text.Substring(4, 2),16);
				SendBuff[8] = Convert.ToByte(lbl_r_Kt.Text.Substring(6, 2),16);
				SendBuff[9] = Convert.ToByte(lbl_r_Kt.Text.Substring(8, 2),16);
				SendBuff[10] = Convert.ToByte(lbl_r_Kt.Text.Substring(10, 2),16);
				SendBuff[11] = Convert.ToByte(lbl_r_Kt.Text.Substring(12, 2),16);
				SendBuff[12] = Convert.ToByte(lbl_r_Kt.Text.Substring(14, 2),16);

				lst_Log.Items.Add("MCU < 80 D2 0D 00 08");
				lst_Log.Items.Add("    <<" + lbl_r_Kt.Text.Substring(0, 2) + " "
											+ lbl_r_Kt.Text.Substring(2, 2) + " "
											+ lbl_r_Kt.Text.Substring(4, 2) + " "
											+ lbl_r_Kt.Text.Substring(6, 2) + " "
											+ lbl_r_Kt.Text.Substring(8, 2) + " "
											+ lbl_r_Kt.Text.Substring(10, 2) + " "
											+ lbl_r_Kt.Text.Substring(12, 2) + " "
											+ lbl_r_Kt.Text.Substring(14, 2));

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}
				else
				{
					return;
				}
			}

			//Select FF06
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xA4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2;
			SendBuff[5] = 0xFF;
			SendBuff[6] = 0x6;

			lst_Log.Items.Add("MCU < 80 A4 00 00 02");
			lst_Log.Items.Add("    <<FF 06");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 7, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}

			//Update FF06 record 0 (Kd)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
		
			if (G_AlgoRef == 0) 
			    SendBuff[2] = 0x4;
            else
                SendBuff[2] = 0x0;
        
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_Kd.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_Kd.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_Kd.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_Kd.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_Kd.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_Kd.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_Kd.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_Kd.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 08");
			lst_Log.Items.Add("    <<" + lbl_Kd.Text.Substring(0, 2) + " "
										+ lbl_Kd.Text.Substring(2, 2) + " "
										+ lbl_Kd.Text.Substring(4, 2) + " "
										+ lbl_Kd.Text.Substring(6, 2) + " "
										+ lbl_Kd.Text.Substring(8, 2) + " "
										+ lbl_Kd.Text.Substring(10, 2) + " "
										+ lbl_Kd.Text.Substring(12, 2) + " "
										+ lbl_Kd.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			//Update FF06 record 1 (Kcr)
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
		
			if (G_AlgoRef == 0)
				SendBuff[2] = 0x5;
            else
                SendBuff[2] = 0x1;
        
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_Kcr.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_Kcr.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_Kcr.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_Kcr.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_Kcr.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_Kcr.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_Kcr.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_Kcr.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 08");
			lst_Log.Items.Add("    <<" + lbl_Kcr.Text.Substring(0, 2) + " " 
										+ lbl_Kcr.Text.Substring(2, 2) + " " 
										+ lbl_Kcr.Text.Substring(4, 2) + " " 
										+ lbl_Kcr.Text.Substring(6, 2) + " " 
										+ lbl_Kcr.Text.Substring(8, 2) + " " 
										+ lbl_Kcr.Text.Substring(10, 2) + " " 
										+ lbl_Kcr.Text.Substring(12, 2) + " " 
										+ lbl_Kcr.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			//Update FF06 record 2 (Kcf)
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			
			if (G_AlgoRef == 0)
				SendBuff[2] = 0x6;
			else
				SendBuff[2] = 0x2;
        
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_Kcf.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_Kcf.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_Kcf.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_Kcf.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_Kcf.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_Kcf.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_Kcf.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_Kcf.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 08");
			lst_Log.Items.Add("    <<" + lbl_Kcf.Text.Substring(0, 2) + " " 
										+ lbl_Kcf.Text.Substring(2, 2) + " " 
										+ lbl_Kcf.Text.Substring(4, 2) + " " 
										+ lbl_Kcf.Text.Substring(6, 2) + " " 
										+ lbl_Kcf.Text.Substring(8, 2) + " " 
										+ lbl_Kcf.Text.Substring(10, 2) + " " 
										+ lbl_Kcf.Text.Substring(12, 2) + " " 
										+ lbl_Kcf.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			//Update FF06 record 3 (Krd)
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			
			if (G_AlgoRef == 0)
				SendBuff[2] = 0x7;
            else
                SendBuff[2] = 0x3;

			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(lbl_Krd.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(lbl_Krd.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(lbl_Krd.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(lbl_Krd.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(lbl_Krd.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(lbl_Krd.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(lbl_Krd.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(lbl_Krd.Text.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 08");
			lst_Log.Items.Add("    <<" + lbl_Krd.Text.Substring(0, 2) + " " 
										+ lbl_Krd.Text.Substring(2, 2) + " " 
										+ lbl_Krd.Text.Substring(4, 2) + " " 
										+ lbl_Krd.Text.Substring(6, 2) + " " 
										+ lbl_Krd.Text.Substring(8, 2) + " " 
										+ lbl_Krd.Text.Substring(10, 2) + " " 
										+ lbl_Krd.Text.Substring(12, 2) + " " 
										+ lbl_Krd.Text.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			//If Algorithm Reference = 3DES then update Right Half of the Keys
			if(G_AlgoRef == 0) 
			{

				//Update FF06 record 0 (Kd) right half
				SendBuff[0] = 0x80;
				SendBuff[1] = 0xD2;
				SendBuff[2] = 0x0;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;
				SendBuff[5] = Convert.ToByte(lbl_r_Kd.Text.Substring(0, 2),16);
				SendBuff[6] = Convert.ToByte(lbl_r_Kd.Text.Substring(2, 2),16);
				SendBuff[7] = Convert.ToByte(lbl_r_Kd.Text.Substring(4, 2),16);
				SendBuff[8] = Convert.ToByte(lbl_r_Kd.Text.Substring(6, 2),16);
				SendBuff[9] = Convert.ToByte(lbl_r_Kd.Text.Substring(8, 2),16);
				SendBuff[10] = Convert.ToByte(lbl_r_Kd.Text.Substring(10, 2),16);
				SendBuff[11] = Convert.ToByte(lbl_r_Kd.Text.Substring(12, 2),16);
				SendBuff[12] = Convert.ToByte(lbl_r_Kd.Text.Substring(14, 2),16);


				lst_Log.Items.Add("MCU < 80 D2 00 00 08");
				lst_Log.Items.Add("    <<" + lbl_r_Kd.Text.Substring(0, 2) + " " 
											+ lbl_r_Kd.Text.Substring(2, 2) + " " 
											+ lbl_r_Kd.Text.Substring(4, 2) + " " 
											+ lbl_r_Kd.Text.Substring(6, 2) + " " 
											+ lbl_r_Kd.Text.Substring(8, 2) + " " 
											+ lbl_r_Kd.Text.Substring(10, 2) + " " 
											+ lbl_r_Kd.Text.Substring(12, 2) + " " 
											+ lbl_r_Kd.Text.Substring(14, 2));

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}	
				else
				{
					return;
				}

				//Update FF06 record 1 (Kcr) right half
				SendBuff[0] = 0x80;
				SendBuff[1] = 0xD2;
				SendBuff[2] = 0x1;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;
				SendBuff[5] = Convert.ToByte(lbl_r_Kcr.Text.Substring(0, 2),16);
				SendBuff[6] = Convert.ToByte(lbl_r_Kcr.Text.Substring(2, 2),16);
				SendBuff[7] = Convert.ToByte(lbl_r_Kcr.Text.Substring(4, 2),16);
				SendBuff[8] = Convert.ToByte(lbl_r_Kcr.Text.Substring(6, 2),16);
				SendBuff[9] = Convert.ToByte(lbl_r_Kcr.Text.Substring(8, 2),16);
				SendBuff[10] = Convert.ToByte(lbl_r_Kcr.Text.Substring(10, 2),16);
				SendBuff[11] = Convert.ToByte(lbl_r_Kcr.Text.Substring(12, 2),16);
				SendBuff[12] = Convert.ToByte(lbl_r_Kcr.Text.Substring(14, 2),16);


				lst_Log.Items.Add("MCU < 80 D2 01 00 08");
				lst_Log.Items.Add("    <<" + lbl_r_Kcr.Text.Substring(0, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(2, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(4, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(6, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(8, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(10, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(12, 2) + " " 
											+ lbl_r_Kcr.Text.Substring(14, 2));

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}	
				else
				{
					return;
				}


				//Update FF06 record 2 (Kcf) right half
				SendBuff[0] = 0x80;
				SendBuff[1] = 0xD2;
				SendBuff[2] = 0x2;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;
				SendBuff[5] = Convert.ToByte(lbl_r_Kcf.Text.Substring(0, 2),16);
				SendBuff[6] = Convert.ToByte(lbl_r_Kcf.Text.Substring(2, 2),16);
				SendBuff[7] = Convert.ToByte(lbl_r_Kcf.Text.Substring(4, 2),16);
				SendBuff[8] = Convert.ToByte(lbl_r_Kcf.Text.Substring(6, 2),16);
				SendBuff[9] = Convert.ToByte(lbl_r_Kcf.Text.Substring(8, 2),16);
				SendBuff[10] = Convert.ToByte(lbl_r_Kcf.Text.Substring(10, 2),16);
				SendBuff[11] = Convert.ToByte(lbl_r_Kcf.Text.Substring(12, 2),16);
				SendBuff[12] = Convert.ToByte(lbl_r_Kcf.Text.Substring(14, 2),16);


				lst_Log.Items.Add("MCU < 80 D2 02 00 08");
				lst_Log.Items.Add("    <<" + lbl_r_Kcf.Text.Substring(0, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(2, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(4, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(6, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(8, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(10, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(12, 2) + " " 
											+ lbl_r_Kcf.Text.Substring(14, 2));

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}	
				else
				{
					return;
				}


				//Update FF06 record 3 (Krd) right half
				SendBuff[0] = 0x80;
				SendBuff[1] = 0xD2;
				SendBuff[2] = 0x3;
				SendBuff[3] = 0x0;
				SendBuff[4] = 0x8;
				SendBuff[5] = Convert.ToByte(lbl_r_Krd.Text.Substring(0, 2),16);
				SendBuff[6] = Convert.ToByte(lbl_r_Krd.Text.Substring(2, 2),16);
				SendBuff[7] = Convert.ToByte(lbl_r_Krd.Text.Substring(4, 2),16);
				SendBuff[8] = Convert.ToByte(lbl_r_Krd.Text.Substring(6, 2),16);
				SendBuff[9] = Convert.ToByte(lbl_r_Krd.Text.Substring(8, 2),16);
				SendBuff[10] = Convert.ToByte(lbl_r_Krd.Text.Substring(10, 2),16);
				SendBuff[11] = Convert.ToByte(lbl_r_Krd.Text.Substring(12, 2),16);
				SendBuff[12] = Convert.ToByte(lbl_r_Krd.Text.Substring(14, 2),16);


				lst_Log.Items.Add("MCU < 80 D2 03 00 08");
				lst_Log.Items.Add("    <<" + lbl_r_Krd.Text.Substring(0, 2) + " " 
											+ lbl_r_Krd.Text.Substring(2, 2) + " " 
											+ lbl_r_Krd.Text.Substring(4, 2) + " " 
											+ lbl_r_Krd.Text.Substring(6, 2) + " " 
											+ lbl_r_Krd.Text.Substring(8, 2) + " " 
											+ lbl_r_Krd.Text.Substring(10, 2) + " " 
											+ lbl_r_Krd.Text.Substring(12, 2) + " " 
											+ lbl_r_Krd.Text.Substring(14, 2));

				lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

				RecvLen = 2;

				if(Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
				{
					if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						return;
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
				}	
				else
				{
					return;
				}

			}

			//Select FF05
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xA4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x2;
			SendBuff[5] = 0xFF;
			SendBuff[6] = 0x5;

			lst_Log.Items.Add("MCU < 80 A4 00 00 05");
			lst_Log.Items.Add("    <<FF 02");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SLT(ref SendBuff, 7, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			//Initialize FF05 Account File
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x0;
			SendBuff[8] = 0x0;


			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 00 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}

			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x1;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x1;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 01 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x2;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x0;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 00 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x3;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x1;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 01 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			//Set Max Balance to 98 96 7F = 9,999,999
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x4;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x98;
			SendBuff[6] = 0x96;
			SendBuff[7] = 0x7F;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<98 96 7F 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}

			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x5;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x0;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 00 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x6;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x0;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 00 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


			SendBuff[0] = 0x80;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x7;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;
			SendBuff[5] = 0x0;
			SendBuff[6] = 0x0;
			SendBuff[7] = 0x0;
			SendBuff[8] = 0x0;					

			lst_Log.Items.Add("MCU < 80 D2 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 04");
			lst_Log.Items.Add("    <<00 00 00 00");

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
				}
			}	
			else
			{
				return;
			}


		}
		
		private void txtSAMGPIN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
            //Character 0-9 and A-F
        
			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		
		private void txt_IC_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}
		
		private void txtKc_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}
	
		private void txtKt_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		private void txtKd_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		private void txtKcr_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		private void txtKcf_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		private void txtKrd_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

	}
}



