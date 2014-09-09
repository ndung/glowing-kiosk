/*===================================================================================================
'
'   Project Name :  SAM_Sample_Usage
'
'   Company      :  Advanced Card Systems LTD.
'
'   Author       :  Aileen Grace L. Sarte
'
'   Date         :  February 7, 2007
'
'   Description  :  This sample shows how ACOS SAM is use for Mutual Authentication and to perform
'                   encrypted PIN submission. And secure E-Purse Function.
'
'   Initial Step :  1.  Press List Readers.
'                   2.  Choose the SAM reader and ACOS card reader.
'                   3.  Press Connect.
'                   4.  Select Algorithm Reference to use (DES/3DES)
'                   5.  Enter SAM Global PIN. (PIN used in KeyManagement sample, SAM Initialization)
'                   6.  Press Mutual Authentication.
'                   7.  Enter ACOS Card PIN. (PIN used in KeyManagement sample, ACOS Card Initialization)
'                   8.  Press Submit PIN.
'                   9.  If you don't want to change your current PIN go to step 10.
'                       To changed current PIN, enter the desired new PIN and press Change PIN
'                   10. To check current card balance (e-purse) press Inquire Account.
'                   11. To credit amount to the card e-purse enter the amount to credit and press Credit.
'                   12. To dedit amount to the card e-purse enter the amount to dedit and press Dedit.
'
'   NOTE:
'                   Please note that this sample program assumes that the SAM and ACOS card were already
'                   initialized using KeyManagement Sample program.
'
'   Revision     :
'
'               Name                    Date            Brief Description of Revision
'
'=====================================================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SAM_Sample_Usage
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		// Variable Declaraton
		int G_retCode, G_hContext, G_hCard, G_hCardSAM, G_Protocol;
		ModWinsCard.SCARD_IO_REQUEST G_ioRequest = new ModWinsCard.SCARD_IO_REQUEST();
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		int RecvLen;
				   
		internal System.Windows.Forms.ListBox lst_Log;
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.TextBox txt_New_PIN;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Button cmd_Change_PIN;
		internal System.Windows.Forms.Button cmd_SubmitPIN;
		internal System.Windows.Forms.TextBox txt_PIN;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Button cmd_MutualAuth;
		internal System.Windows.Forms.TextBox txtSAMGPIN;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.RadioButton rb_3DES;
		internal System.Windows.Forms.RadioButton rb_DES;
		internal System.Windows.Forms.Button cmd_Connect;
		internal System.Windows.Forms.ComboBox cmbSAM;
		internal System.Windows.Forms.ComboBox cmbSLT;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button cmd_ListReaders;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.Button cmdDebit;
		internal System.Windows.Forms.TextBox txtDebitAmount;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Button cmdCredit;
		internal System.Windows.Forms.TextBox txtCreditAmount;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.Button cmdInqAccount;
		internal System.Windows.Forms.TextBox txtInqAmount;
		internal System.Windows.Forms.TextBox txtMaxBalance;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Label Label6;
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
            this.txt_New_PIN = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmd_Change_PIN = new System.Windows.Forms.Button();
            this.cmd_SubmitPIN = new System.Windows.Forms.Button();
            this.txt_PIN = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmd_MutualAuth = new System.Windows.Forms.Button();
            this.txtSAMGPIN = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.rb_3DES = new System.Windows.Forms.RadioButton();
            this.rb_DES = new System.Windows.Forms.RadioButton();
            this.cmd_Connect = new System.Windows.Forms.Button();
            this.cmbSAM = new System.Windows.Forms.ComboBox();
            this.cmbSLT = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmd_ListReaders = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDebit = new System.Windows.Forms.Button();
            this.txtDebitAmount = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.cmdCredit = new System.Windows.Forms.Button();
            this.txtCreditAmount = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.cmdInqAccount = new System.Windows.Forms.Button();
            this.txtInqAmount = new System.Windows.Forms.TextBox();
            this.txtMaxBalance = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst_Log
            // 
            this.lst_Log.Location = new System.Drawing.Point(272, 24);
            this.lst_Log.Name = "lst_Log";
            this.lst_Log.Size = new System.Drawing.Size(473, 485);
            this.lst_Log.TabIndex = 15;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Location = new System.Drawing.Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(254, 503);
            this.TabControl1.TabIndex = 14;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(246, 477);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Security";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txt_New_PIN);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.cmd_Change_PIN);
            this.GroupBox1.Controls.Add(this.cmd_SubmitPIN);
            this.GroupBox1.Controls.Add(this.txt_PIN);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.cmd_MutualAuth);
            this.GroupBox1.Controls.Add(this.txtSAMGPIN);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.rb_3DES);
            this.GroupBox1.Controls.Add(this.rb_DES);
            this.GroupBox1.Controls.Add(this.cmd_Connect);
            this.GroupBox1.Controls.Add(this.cmbSAM);
            this.GroupBox1.Controls.Add(this.cmbSLT);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.cmd_ListReaders);
            this.GroupBox1.Location = new System.Drawing.Point(7, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(232, 464);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            // 
            // txt_New_PIN
            // 
            this.txt_New_PIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_New_PIN.Location = new System.Drawing.Point(110, 386);
            this.txt_New_PIN.MaxLength = 16;
            this.txt_New_PIN.Name = "txt_New_PIN";
            this.txt_New_PIN.Size = new System.Drawing.Size(109, 20);
            this.txt_New_PIN.TabIndex = 11;
            this.txt_New_PIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_New_PIN_KeyPress);
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(15, 391);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(88, 16);
            this.Label5.TabIndex = 15;
            this.Label5.Text = "ACOS New PIN";
            // 
            // cmd_Change_PIN
            // 
            this.cmd_Change_PIN.Enabled = false;
            this.cmd_Change_PIN.Location = new System.Drawing.Point(20, 414);
            this.cmd_Change_PIN.Name = "cmd_Change_PIN";
            this.cmd_Change_PIN.Size = new System.Drawing.Size(128, 32);
            this.cmd_Change_PIN.TabIndex = 12;
            this.cmd_Change_PIN.Text = "Change PIN (ciphered)";
            this.cmd_Change_PIN.Click += new System.EventHandler(this.cmd_Change_PIN_Click);
            // 
            // cmd_SubmitPIN
            // 
            this.cmd_SubmitPIN.Enabled = false;
            this.cmd_SubmitPIN.Location = new System.Drawing.Point(19, 340);
            this.cmd_SubmitPIN.Name = "cmd_SubmitPIN";
            this.cmd_SubmitPIN.Size = new System.Drawing.Size(128, 32);
            this.cmd_SubmitPIN.TabIndex = 10;
            this.cmd_SubmitPIN.Text = "Submit PIN (ciphered)";
            this.cmd_SubmitPIN.Click += new System.EventHandler(this.cmd_SubmitPIN_Click);
            // 
            // txt_PIN
            // 
            this.txt_PIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_PIN.Location = new System.Drawing.Point(110, 313);
            this.txt_PIN.MaxLength = 16;
            this.txt_PIN.Name = "txt_PIN";
            this.txt_PIN.Size = new System.Drawing.Size(109, 20);
            this.txt_PIN.TabIndex = 9;
            this.txt_PIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_PIN_KeyPress);
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(15, 316);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(88, 16);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "ACOS Card PIN";
            // 
            // cmd_MutualAuth
            // 
            this.cmd_MutualAuth.Enabled = false;
            this.cmd_MutualAuth.Location = new System.Drawing.Point(18, 268);
            this.cmd_MutualAuth.Name = "cmd_MutualAuth";
            this.cmd_MutualAuth.Size = new System.Drawing.Size(128, 32);
            this.cmd_MutualAuth.TabIndex = 8;
            this.cmd_MutualAuth.Text = "Mutual Authentication";
            this.cmd_MutualAuth.Click += new System.EventHandler(this.cmd_MutualAuth_Click);
            // 
            // txtSAMGPIN
            // 
            this.txtSAMGPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSAMGPIN.Location = new System.Drawing.Point(109, 239);
            this.txtSAMGPIN.MaxLength = 16;
            this.txtSAMGPIN.Name = "txtSAMGPIN";
            this.txtSAMGPIN.Size = new System.Drawing.Size(109, 20);
            this.txtSAMGPIN.TabIndex = 7;
            this.txtSAMGPIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSAMGPIN_KeyPress);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(11, 243);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(104, 17);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "SAM GLOBAL PIN";
            // 
            // rb_3DES
            // 
            this.rb_3DES.Location = new System.Drawing.Point(87, 202);
            this.rb_3DES.Name = "rb_3DES";
            this.rb_3DES.Size = new System.Drawing.Size(57, 24);
            this.rb_3DES.TabIndex = 6;
            this.rb_3DES.Text = "3 DES";
            // 
            // rb_DES
            // 
            this.rb_DES.Location = new System.Drawing.Point(22, 202);
            this.rb_DES.Name = "rb_DES";
            this.rb_DES.Size = new System.Drawing.Size(57, 24);
            this.rb_DES.TabIndex = 5;
            this.rb_DES.Text = "DES";
            // 
            // cmd_Connect
            // 
            this.cmd_Connect.Enabled = false;
            this.cmd_Connect.Location = new System.Drawing.Point(19, 159);
            this.cmd_Connect.Name = "cmd_Connect";
            this.cmd_Connect.Size = new System.Drawing.Size(128, 32);
            this.cmd_Connect.TabIndex = 4;
            this.cmd_Connect.Text = "Connect";
            this.cmd_Connect.Click += new System.EventHandler(this.cmd_Connect_Click);
            // 
            // cmbSAM
            // 
            this.cmbSAM.Location = new System.Drawing.Point(12, 128);
            this.cmbSAM.Name = "cmbSAM";
            this.cmbSAM.Size = new System.Drawing.Size(208, 21);
            this.cmbSAM.TabIndex = 3;
            // 
            // cmbSLT
            // 
            this.cmbSLT.Location = new System.Drawing.Point(12, 76);
            this.cmbSLT.Name = "cmbSLT";
            this.cmbSLT.Size = new System.Drawing.Size(208, 21);
            this.cmbSLT.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(12, 110);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(72, 16);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "SAM Reader";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 57);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(72, 16);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Card Reader";
            // 
            // cmd_ListReaders
            // 
            this.cmd_ListReaders.Location = new System.Drawing.Point(20, 16);
            this.cmd_ListReaders.Name = "cmd_ListReaders";
            this.cmd_ListReaders.Size = new System.Drawing.Size(128, 32);
            this.cmd_ListReaders.TabIndex = 1;
            this.cmd_ListReaders.Text = "List Readers";
            this.cmd_ListReaders.Click += new System.EventHandler(this.cmd_ListReaders_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.GroupBox2);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(246, 477);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Account";
            this.TabPage2.Visible = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.cmdDebit);
            this.GroupBox2.Controls.Add(this.txtDebitAmount);
            this.GroupBox2.Controls.Add(this.Label9);
            this.GroupBox2.Controls.Add(this.cmdCredit);
            this.GroupBox2.Controls.Add(this.txtCreditAmount);
            this.GroupBox2.Controls.Add(this.Label8);
            this.GroupBox2.Controls.Add(this.cmdInqAccount);
            this.GroupBox2.Controls.Add(this.txtInqAmount);
            this.GroupBox2.Controls.Add(this.txtMaxBalance);
            this.GroupBox2.Controls.Add(this.Label7);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Location = new System.Drawing.Point(8, 7);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(232, 464);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            // 
            // cmdDebit
            // 
            this.cmdDebit.Location = new System.Drawing.Point(17, 402);
            this.cmdDebit.Name = "cmdDebit";
            this.cmdDebit.Size = new System.Drawing.Size(128, 32);
            this.cmdDebit.TabIndex = 18;
            this.cmdDebit.Text = "Debit";
            this.cmdDebit.Click += new System.EventHandler(this.cmdDebit_Click);
            // 
            // txtDebitAmount
            // 
            this.txtDebitAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDebitAmount.Location = new System.Drawing.Point(16, 371);
            this.txtDebitAmount.MaxLength = 7;
            this.txtDebitAmount.Name = "txtDebitAmount";
            this.txtDebitAmount.Size = new System.Drawing.Size(200, 20);
            this.txtDebitAmount.TabIndex = 17;
            this.txtDebitAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDebitAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDebitAmount_KeyPress);
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(16, 352);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(100, 16);
            this.Label9.TabIndex = 8;
            this.Label9.Text = "Debit Amount";
            // 
            // cmdCredit
            // 
            this.cmdCredit.Location = new System.Drawing.Point(17, 296);
            this.cmdCredit.Name = "cmdCredit";
            this.cmdCredit.Size = new System.Drawing.Size(128, 32);
            this.cmdCredit.TabIndex = 16;
            this.cmdCredit.Text = "Credit";
            this.cmdCredit.Click += new System.EventHandler(this.cmdCredit_Click);
            // 
            // txtCreditAmount
            // 
            this.txtCreditAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCreditAmount.Location = new System.Drawing.Point(16, 264);
            this.txtCreditAmount.MaxLength = 7;
            this.txtCreditAmount.Name = "txtCreditAmount";
            this.txtCreditAmount.Size = new System.Drawing.Size(200, 20);
            this.txtCreditAmount.TabIndex = 15;
            this.txtCreditAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCreditAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditAmount_KeyPress);
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(16, 243);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(100, 16);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "Credit Amount";
            // 
            // cmdInqAccount
            // 
            this.cmdInqAccount.Location = new System.Drawing.Point(16, 154);
            this.cmdInqAccount.Name = "cmdInqAccount";
            this.cmdInqAccount.Size = new System.Drawing.Size(128, 32);
            this.cmdInqAccount.TabIndex = 14;
            this.cmdInqAccount.Text = "Inquire Account";
            this.cmdInqAccount.Click += new System.EventHandler(this.cmdInqAccount_Click);
            // 
            // txtInqAmount
            // 
            this.txtInqAmount.Enabled = false;
            this.txtInqAmount.Location = new System.Drawing.Point(16, 123);
            this.txtInqAmount.Name = "txtInqAmount";
            this.txtInqAmount.Size = new System.Drawing.Size(200, 20);
            this.txtInqAmount.TabIndex = 3;
            this.txtInqAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxBalance
            // 
            this.txtMaxBalance.Enabled = false;
            this.txtMaxBalance.Location = new System.Drawing.Point(16, 58);
            this.txtMaxBalance.Name = "txtMaxBalance";
            this.txtMaxBalance.Size = new System.Drawing.Size(200, 20);
            this.txtMaxBalance.TabIndex = 2;
            this.txtMaxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(16, 103);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(100, 16);
            this.Label7.TabIndex = 1;
            this.Label7.Text = "Balance";
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(16, 36);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(100, 16);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Max Balance";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(752, 519);
            this.Controls.Add(this.lst_Log);
            this.Controls.Add(this.TabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAM Sample Usage";
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Initialize object
			rb_DES.Checked = true;
		}

		private void cmd_ListReaders_Click(object sender, System.EventArgs e)
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

			//Convert reader buffer to string
			while(ReadersList[indx] != 0)
			{
				
				while(ReadersList[indx] != 0)
				{
					rName = rName + (char)ReadersList[indx];
					indx = indx + 1;
				}
				
				//Add available reader name to list
				cmbSLT.Items.Add(rName);
				cmbSAM.Items.Add(rName);
				rName = "";
				indx = indx + 1;
							
			}

			if(cmbSLT.Items.Count > 0 & cmbSAM.Items.Count > 0)
			{
				cmbSLT.SelectedIndex = 0;
				cmbSAM.SelectedIndex = 0;
				cmd_Connect.Enabled = true;
			}
		
		}

		private void cmd_Connect_Click(object sender, System.EventArgs e)
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
		
			cmd_MutualAuth.Enabled = true;
		}

		private void cmd_MutualAuth_Click(object sender, System.EventArgs e)
		{
			//Variable Declaration
			string SN, tempStr;
			int i;

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

					SN = SN.Replace(" ", "");

				}
			}
			else
			{
				return;
			}


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

		
			//Submit Issuer PIN (SAM Global PIN)
			
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


			//Diversify Kc
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x72;
			SendBuff[2] = 0x4;
			SendBuff[3] = 0x82;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(SN.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(SN.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(SN.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(SN.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(SN.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(SN.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(SN.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(SN.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 72 04 82 08");
			lst_Log.Items.Add("    <<" + SN.Substring(0, 2) + " "
									+ SN.Substring(2, 2) + " "
									+ SN.Substring(4, 2) + " "
									+ SN.Substring(6, 2) + " "
									+ SN.Substring(8, 2) + " "
									+ SN.Substring(10, 2) + " "
									+ SN.Substring(12, 2) + " "
									+ SN.Substring(14, 2));

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


			//Diversify Kt
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x72;
			SendBuff[2] = 0x3;
			SendBuff[3] = 0x83;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(SN.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(SN.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(SN.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(SN.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(SN.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(SN.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(SN.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(SN.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 72 03 83 08");
			lst_Log.Items.Add("    <<" + SN.Substring(0, 2) + " "
										+ SN.Substring(2, 2) + " "
										+ SN.Substring(4, 2) + " "
										+ SN.Substring(6, 2) + " "
										+ SN.Substring(8, 2) + " "
										+ SN.Substring(10, 2) + " "
										+ SN.Substring(12, 2) + " "
										+ SN.Substring(14, 2));

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

			//Get Challenge
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x84;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("MCU < 80 84 00 00 08");
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
					//Retrieve RNDc
					tempStr = "";

					for(i = 0; i<=7; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}", RecvBuff[i]).ToUpper() + " ";
					}
					lst_Log.Items.Add("MCU >> " + tempStr);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[8]).ToUpper() + string.Format("{0:x2}", RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			//Prepare ACOS authentication
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x78;

			if (rb_DES.Checked == true)
				SendBuff[2] = 0x1;
			else if(rb_3DES.Checked == true)
				SendBuff[2] = 0x0;
			
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 78 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 08");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
									+ tempStr.Substring(2, 2) + " "
									+ tempStr.Substring(4, 2) + " "
									+ tempStr.Substring(6, 2) + " "
									+ tempStr.Substring(8, 2) + " "
									+ tempStr.Substring(10, 2) + " "
									+ tempStr.Substring(12, 2) + " "
									+ tempStr.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6110")
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

			//Get Response to get result + RNDt
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x10;

			lst_Log.Items.Add("SAM < 00 C0 00 00 10");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 0x12;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[16]).ToUpper() + string.Format("{0:x2}",RecvBuff[17]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[16]).ToUpper() + string.Format("{0:x2}",RecvBuff[17]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result + RNDt
					tempStr = "";

					for(i = 0; i <=15; i++)
					{
						 tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + tempStr);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[16]).ToUpper() + string.Format("{0:x2}",RecvBuff[17]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			//Authenticate
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x82;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x10;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);
			SendBuff[13] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[14] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(20, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr.Substring(22, 2),16);
			SendBuff[17] = Convert.ToByte(tempStr.Substring(24, 2),16);
			SendBuff[18] = Convert.ToByte(tempStr.Substring(26, 2),16);
			SendBuff[19] = Convert.ToByte(tempStr.Substring(28, 2),16);
			SendBuff[20] = Convert.ToByte(tempStr.Substring(30, 2),16);


			lst_Log.Items.Add("MCU < 80 82 00 00 10");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
									+ tempStr.Substring(2, 2) + " "
									+ tempStr.Substring(4, 2) + " "
									+ tempStr.Substring(6, 2) + " "
									+ tempStr.Substring(8, 2) + " "
									+ tempStr.Substring(10, 2) + " "
									+ tempStr.Substring(12, 2) + " "
									+ tempStr.Substring(14, 2) + " "
									+ tempStr.Substring(16, 2) + " "
									+ tempStr.Substring(18, 2) + " "
									+ tempStr.Substring(20, 2) + " "
									+ tempStr.Substring(22, 2) + " "
									+ tempStr.Substring(24, 2) + " "
									+ tempStr.Substring(26, 2) + " "
									+ tempStr.Substring(28, 2) + " "
									+ tempStr.Substring(30, 2));


			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 21, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6108")
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

			//Get Response to get result
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;

			lst_Log.Items.Add("MCU < 80 C0 00 00 08");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 10;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=7; i++)
					{
						 tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("MCU >> " + tempStr);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}

			//Verify ACOS Authentication 
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7A;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);

			lst_Log.Items.Add("SAM < 80 7A 00 00 08");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
									+ tempStr.Substring(2, 2) + " "
									+ tempStr.Substring(4, 2) + " "
									+ tempStr.Substring(6, 2) + " "
									+ tempStr.Substring(8, 2) + " "
									+ tempStr.Substring(10, 2) + " "
									+ tempStr.Substring(12, 2) + " "
									+ tempStr.Substring(14, 2));

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

			cmd_SubmitPIN.Enabled = true;

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

		private void cmd_SubmitPIN_Click(object sender, System.EventArgs e)
		{
			string tempStr;
			int i;

			//Encrypt PIN
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x74;

			if (rb_DES.Checked == true)
				SendBuff[2] = 0x1;
			else if (rb_3DES.Checked == true)
				SendBuff[2] = 0x0;
			
			SendBuff[3] = 0x1;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(txt_PIN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(txt_PIN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(txt_PIN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(txt_PIN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(txt_PIN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(txt_PIN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(txt_PIN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(txt_PIN.Text.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 74 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 01 08");
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
		
			//Get Response to get encrypted PIN
			
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
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[16]).ToUpper() + string.Format("{0:x2}",RecvBuff[17]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve encrypted PIN
					tempStr = "";

					for(i = 0; i <=7; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + tempStr);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}

			//Submit Encrypted PIN

			SendBuff[0] = 0x80;
			SendBuff[1] = 0x20;
			SendBuff[2] = 0x6; //PIN
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);

			lst_Log.Items.Add("MCU < 80 20 06 00 08");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
										+ tempStr.Substring(2, 2) + " "
										+ tempStr.Substring(4, 2) + " "
										+ tempStr.Substring(6, 2) + " "
										+ tempStr.Substring(8, 2) + " "
										+ tempStr.Substring(10, 2) + " "
										+ tempStr.Substring(12, 2) + " "
										+ tempStr.Substring(14, 2));

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

			 cmd_Change_PIN.Enabled = true;

		}

		private void cmd_Change_PIN_Click(object sender, System.EventArgs e)
		{
			string tempStr;
			int i;

			//Decrypt PIN
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x76;

			if (rb_DES.Checked == true)
				SendBuff[2] = 0x1;
			else if (rb_3DES.Checked == true)
				SendBuff[2] = 0x0;
			
			SendBuff[3] = 0x1;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(txt_New_PIN.Text.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(txt_New_PIN.Text.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(txt_New_PIN.Text.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(txt_New_PIN.Text.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(txt_New_PIN.Text.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(txt_New_PIN.Text.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(txt_New_PIN.Text.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(txt_New_PIN.Text.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 76 " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 01 08");
			lst_Log.Items.Add("    <<" + txt_New_PIN.Text.Substring(0, 2) + " "
										+ txt_New_PIN.Text.Substring(2, 2) + " "
										+ txt_New_PIN.Text.Substring(4, 2) + " "
										+ txt_New_PIN.Text.Substring(6, 2) + " "
										+ txt_New_PIN.Text.Substring(8, 2) + " "
										+ txt_New_PIN.Text.Substring(10, 2) + " "
										+ txt_New_PIN.Text.Substring(12, 2) + " "
										+ txt_New_PIN.Text.Substring(14, 2));

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

			//Get Response to get decrypted PIN

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
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[16]).ToUpper() + string.Format("{0:x2}",RecvBuff[17]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve decrypted PIN
					tempStr = "";

					for(i = 0; i <=7; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + tempStr);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[8]).ToUpper() + string.Format("{0:x2}",RecvBuff[9]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}

			//Change PIN
		
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x24;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);


			lst_Log.Items.Add("MCU < 80 24 00 00 08 ");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
										+ tempStr.Substring(2, 2) + " "
										+ tempStr.Substring(4, 2) + " "
										+ tempStr.Substring(6, 2) + " "
										+ tempStr.Substring(8, 2) + " "
										+ tempStr.Substring(10, 2) + " "
										+ tempStr.Substring(12, 2) + " "
										+ tempStr.Substring(14, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SLT(ref SendBuff, 13, ref RecvLen, ref RecvBuff) == true)
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
			else
			{ 
				return;
			}
		}

		private void cmdInqAccount_Click(object sender, System.EventArgs e)
		{
			string SN, tempStr;
			int i;

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

					SN = SN.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			//Diversify Kcf

			SendBuff[0] = 0x80;
			SendBuff[1] = 0x72;
			SendBuff[2] = 0x2;
			SendBuff[3] = 0x86;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(SN.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(SN.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(SN.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(SN.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(SN.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(SN.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(SN.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(SN.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 72 02 86 08");
			lst_Log.Items.Add("    <<" + SN.Substring(0, 2) + " "
										+ SN.Substring(2, 2) + " "
										+ SN.Substring(4, 2) + " "
										+ SN.Substring(6, 2) + " "
										+ SN.Substring(8, 2) + " "
										+ SN.Substring(10, 2) + " "
										+ SN.Substring(12, 2) + " "
										+ SN.Substring(14, 2));

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

			
			//Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE4;
			SendBuff[2] = 0x2;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;

			//4 bytes reference
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;

			lst_Log.Items.Add("MCU < 80 E4 02 00 04");
			lst_Log.Items.Add("    <<AA BB CC DD");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6119")
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

			//Get Response to get result
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x19;

			lst_Log.Items.Add("MCU < 80 C0 00 00 19");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 27;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=24; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("MCU >> " + tempStr);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}

			//Verify Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7C;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;
			
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x1D;
			//Ref
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;
			//MAC
			SendBuff[9] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(6, 2),16);

			//Transaction Type
			SendBuff[13] = Convert.ToByte(tempStr.Substring(8, 2),16);

			//Balance
			SendBuff[14] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr.Substring(14, 2),16);

			//ATREF
			SendBuff[17] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[18] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[19] = Convert.ToByte(tempStr.Substring(20, 2),16);
			SendBuff[20] = Convert.ToByte(tempStr.Substring(22, 2),16);
			SendBuff[21] = Convert.ToByte(tempStr.Substring(24, 2),16);
			SendBuff[22] = Convert.ToByte(tempStr.Substring(26, 2),16);

			//Max Balance
			SendBuff[23] = Convert.ToByte(tempStr.Substring(28, 2),16);
			SendBuff[24] = Convert.ToByte(tempStr.Substring(30, 2),16);
			SendBuff[25] = Convert.ToByte(tempStr.Substring(32, 2),16);

			//TTREFc
			SendBuff[26] = Convert.ToByte(tempStr.Substring(34, 2),16);
			SendBuff[27] = Convert.ToByte(tempStr.Substring(36, 2),16);
			SendBuff[28] = Convert.ToByte(tempStr.Substring(38, 2),16);
			SendBuff[29] = Convert.ToByte(tempStr.Substring(40, 2),16);

			//TTREFd
			SendBuff[30] = Convert.ToByte(tempStr.Substring(42, 2),16);
			SendBuff[31] = Convert.ToByte(tempStr.Substring(44, 2),16);
			SendBuff[32] = Convert.ToByte(tempStr.Substring(46, 2),16);
			SendBuff[33] = Convert.ToByte(tempStr.Substring(48, 2),16);


			lst_Log.Items.Add("SAM < 80 7C " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 1D");
			lst_Log.Items.Add("    <<AA BB CC DD " + tempStr.Substring(0, 2) + " "
													+ tempStr.Substring(2, 2) + " "
													+ tempStr.Substring(4, 2) + " "
													+ tempStr.Substring(6, 2) + " "
													+ tempStr.Substring(8, 2) + " "
													+ tempStr.Substring(10, 2) + " "
													+ tempStr.Substring(12, 2) + " "
													+ tempStr.Substring(14, 2) + " "
													+ tempStr.Substring(16, 2) + " "
													+ tempStr.Substring(18, 2) + " "
													+ tempStr.Substring(20, 2) + " "
													+ tempStr.Substring(22, 2));

			lst_Log.Items.Add("    <<" + tempStr.Substring(24, 2) + " "
										+ tempStr.Substring(26, 2) + " "
										+ tempStr.Substring(28, 2) + " "
										+ tempStr.Substring(30, 2) + " "
										+ tempStr.Substring(32, 2) + " "
										+ tempStr.Substring(34, 2) + " "
										+ tempStr.Substring(36, 2) + " "
										+ tempStr.Substring(38, 2) + " "
										+ tempStr.Substring(40, 2) + " "
										+ tempStr.Substring(42, 2) + " "
										+ tempStr.Substring(44, 2) + " "
										+ tempStr.Substring(46, 2) + " "
										+ tempStr.Substring(48, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 34, ref RecvLen, ref RecvBuff) == true)
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

			txtMaxBalance.Text = Get_Balance(Convert.ToByte(tempStr.Substring(28, 2),16), Convert.ToByte(tempStr.Substring(30, 2),16), Convert.ToByte(tempStr.Substring(32, 2),16));

			txtInqAmount.Text = Get_Balance(Convert.ToByte(tempStr.Substring(10, 2),16), Convert.ToByte(tempStr.Substring(12, 2),16), Convert.ToByte(tempStr.Substring(14, 2),16));

		}

		private string Get_Balance(byte Data1, byte Data2, byte Data3)
		{

			int TotalBalance;

			//Get Total Balance
			TotalBalance = (Data1 * 65536);

			TotalBalance = (TotalBalance + Data2 * Convert.ToInt16(256));

			TotalBalance = (TotalBalance + Data3);
			
			return(Convert.ToString(TotalBalance.ToString()));
	
		}

		private void cmdDebit_Click(object sender, System.EventArgs e)
		{
			string SN, tempStr, tempStr2;
            int i;
			long Bal, NewBal;

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

					SN = SN.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			//Diversify Kd

			SendBuff[0] = 0x80;
			SendBuff[1] = 0x72;
			SendBuff[2] = 0x2;
			SendBuff[3] = 0x84;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(SN.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(SN.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(SN.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(SN.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(SN.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(SN.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(SN.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(SN.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 72 02 84 08");
			lst_Log.Items.Add("    <<" + SN.Substring(0, 2) + " "
										+ SN.Substring(2, 2) + " "
										+ SN.Substring(4, 2) + " "
										+ SN.Substring(6, 2) + " "
										+ SN.Substring(8, 2) + " "
										+ SN.Substring(10, 2) + " "
										+ SN.Substring(12, 2) + " "
										+ SN.Substring(14, 2));

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

			//Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE4;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;

			//4 bytes reference
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;

			lst_Log.Items.Add("MCU < 80 E4 00 00 04");
			lst_Log.Items.Add("    <<AA BB CC DD");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6119")
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

			//Get Response to get result
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x19;

			lst_Log.Items.Add("MCU < 80 C0 00 00 19");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 27;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr2  = "";

					for(i = 0; i <=24; i++)
					{
						tempStr2  = tempStr2 + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("MCU >> " + tempStr2 );
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr2 = tempStr2.Replace(" ", "");
				}
			}
			else
			{
				return;
			}


			//Verify Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7C;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;
			
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x1D;
			//Ref
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;
			//MAC
			SendBuff[9] = Convert.ToByte(tempStr2.Substring(0, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr2.Substring(2, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr2.Substring(4, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr2.Substring(6, 2),16);

			//Transaction Type
			SendBuff[13] = Convert.ToByte(tempStr2.Substring(8, 2),16);

			//Balance
			SendBuff[14] = Convert.ToByte(tempStr2.Substring(10, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr2.Substring(12, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr2.Substring(14, 2),16);

			//ATREF
			SendBuff[17] = Convert.ToByte(tempStr2.Substring(16, 2),16);
			SendBuff[18] = Convert.ToByte(tempStr2.Substring(18, 2),16);
			SendBuff[19] = Convert.ToByte(tempStr2.Substring(20, 2),16);
			SendBuff[20] = Convert.ToByte(tempStr2.Substring(22, 2),16);
			SendBuff[21] = Convert.ToByte(tempStr2.Substring(24, 2),16);
			SendBuff[22] = Convert.ToByte(tempStr2.Substring(26, 2),16);

			//Max Balance
			SendBuff[23] = Convert.ToByte(tempStr2.Substring(28, 2),16);
			SendBuff[24] = Convert.ToByte(tempStr2.Substring(30, 2),16);
			SendBuff[25] = Convert.ToByte(tempStr2.Substring(32, 2),16);

			//TTREFc
			SendBuff[26] = Convert.ToByte(tempStr2.Substring(34, 2),16);
			SendBuff[27] = Convert.ToByte(tempStr2.Substring(36, 2),16);
			SendBuff[28] = Convert.ToByte(tempStr2.Substring(38, 2),16);
			SendBuff[29] = Convert.ToByte(tempStr2.Substring(40, 2),16);

			//TTREFd
			SendBuff[30] = Convert.ToByte(tempStr2.Substring(42, 2),16);
			SendBuff[31] = Convert.ToByte(tempStr2.Substring(44, 2),16);
			SendBuff[32] = Convert.ToByte(tempStr2.Substring(46, 2),16);
			SendBuff[33] = Convert.ToByte(tempStr2.Substring(48, 2),16);


			lst_Log.Items.Add("SAM < 80 7C " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 1D");
			lst_Log.Items.Add("    <<AA BB CC DD " + tempStr2.Substring(0, 2) + " "
													+ tempStr2.Substring(2, 2) + " "
													+ tempStr2.Substring(4, 2) + " "
													+ tempStr2.Substring(6, 2) + " "
													+ tempStr2.Substring(8, 2) + " "
													+ tempStr2.Substring(10, 2) + " "
													+ tempStr2.Substring(12, 2) + " "
													+ tempStr2.Substring(14, 2) + " "
													+ tempStr2.Substring(16, 2) + " "
													+ tempStr2.Substring(18, 2) + " "
													+ tempStr2.Substring(20, 2) + " "
													+ tempStr2.Substring(22, 2));

			lst_Log.Items.Add("    <<" + tempStr2.Substring(24, 2) + " "
										+ tempStr2.Substring(26, 2) + " "
										+ tempStr2.Substring(28, 2) + " "
										+ tempStr2.Substring(30, 2) + " "
										+ tempStr2.Substring(32, 2) + " "
										+ tempStr2.Substring(34, 2) + " "
										+ tempStr2.Substring(36, 2) + " "
										+ tempStr2.Substring(38, 2) + " "
										+ tempStr2.Substring(40, 2) + " "
										+ tempStr2.Substring(42, 2) + " "
										+ tempStr2.Substring(44, 2) + " "
										+ tempStr2.Substring(46, 2) + " "
										+ tempStr2.Substring(48, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 34, ref RecvLen, ref RecvBuff) == true)
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

			Bal = Convert.ToInt64(Get_Balance(Convert.ToByte(tempStr2.Substring(10, 2),16), Convert.ToByte(tempStr2.Substring(12, 2),16), Convert.ToByte(tempStr2.Substring(14, 2),16)));


			//Prepare ACOS Transaction
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7E;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;

			SendBuff[3] = 0xE6;
			SendBuff[4] = 0xD;

			//Amount to Debit
			SendBuff[5] = Convert.ToByte(Convert.ToInt64(txtDebitAmount.Text) / 65536);
			SendBuff[6] = Convert.ToByte((Convert.ToInt64(txtDebitAmount.Text) / Convert.ToInt64(256)) % 65536 % Convert.ToInt64(256));
			SendBuff[7] = Convert.ToByte(Convert.ToInt64(txtDebitAmount.Text) % Convert.ToInt64(256));

			//TTREFd
			SendBuff[8] = Convert.ToByte(tempStr2.Substring(42, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr2.Substring(44, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr2.Substring(46, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr2.Substring(48, 2),16);

			//ATREF
			SendBuff[12] = Convert.ToByte(tempStr2.Substring(16, 2),16);
			SendBuff[13] = Convert.ToByte(tempStr2.Substring(18, 2),16);
			SendBuff[14] = Convert.ToByte(tempStr2.Substring(20, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr2.Substring(22, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr2.Substring(24, 2),16);
			SendBuff[17] = Convert.ToByte(tempStr2.Substring(26, 2),16);



			lst_Log.Items.Add("SAM < 80 7E " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " E6 0D");
			lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[12]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[13]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[14]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[15]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[16]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[17]).ToUpper());


			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SAM(ref SendBuff, 18, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "610B")
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

			//Get Response to get result
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0xB;

			lst_Log.Items.Add("SAM < 00 C0 00 00 0B");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 0xD;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[11]).ToUpper() + string.Format("{0:x2}",RecvBuff[12]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[11]).ToUpper() + string.Format("{0:x2}",RecvBuff[12]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=10; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + tempStr);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[11]).ToUpper() + string.Format("{0:x2}",RecvBuff[12]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}


			//Debit and return Debit Certificate
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE6;
			SendBuff[2] = 0x1;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0xB;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);
			SendBuff[13] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[14] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(20, 2),16);


			lst_Log.Items.Add("MCU < 80 E6 01 00 0B ");
						
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
										+ tempStr.Substring(2, 2) + " "
										+ tempStr.Substring(4, 2) + " "
										+ tempStr.Substring(6, 2) + " "
										+ tempStr.Substring(8, 2) + " "
										+ tempStr.Substring(10, 2) + " "
										+ tempStr.Substring(12, 2) + " "
										+ tempStr.Substring(14, 2) + " "
										+ tempStr.Substring(16, 2) + " "
										+ tempStr.Substring(18, 2) + " "
										+ tempStr.Substring(20, 2));
									
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 16, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6104")
				{
					if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() == "6A86")
					{
						lst_Log.Items.Add("Debit Certificate Not Supported By ACOS2 card or lower");
						lst_Log.Items.Add("Change P1 = 0 to perform Debit without returning debit certificate");
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
						Debit_ACOS2(tempStr);
					}
					else
					{
						lst_Log.Items.Add("MCU > " + string.Format("{0:x2}", RecvBuff[0]).ToUpper() + string.Format("{0:x2}", RecvBuff[1]).ToUpper());
						lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					}
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

			//Get Response to get result
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;

			lst_Log.Items.Add("MCU < 80 C0 00 00 04");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 6;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[4]).ToUpper() + string.Format("{0:x2}",RecvBuff[5]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[4]).ToUpper() + string.Format("{0:x2}",RecvBuff[5]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=3; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("MCU >> " + tempStr);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[4]).ToUpper() + string.Format("{0:x2}",RecvBuff[5]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}
			

			//Verify Debit Certificate
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x70;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;
			
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x14;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			
			//Amount last Debited from card
			SendBuff[9] = Convert.ToByte(Convert.ToInt64(txtDebitAmount.Text) / 65536);
			SendBuff[10] = Convert.ToByte((Convert.ToInt64(txtDebitAmount.Text) / Convert.ToInt64(256)) % 65536 % Convert.ToInt64(256));
			SendBuff[11] = Convert.ToByte(Convert.ToInt64(txtDebitAmount.Text) % Convert.ToInt64(256));

			//Expected New Balance after the Debit
			NewBal = Bal - Convert.ToInt64(txtDebitAmount.Text);

			SendBuff[12] = Convert.ToByte(Convert.ToInt64(NewBal) / 65536);
			SendBuff[13] = Convert.ToByte((Convert.ToInt64(NewBal) / Convert.ToInt64(256)) % 65536 % Convert.ToInt64(256));
			SendBuff[14] = Convert.ToByte(Convert.ToInt64(NewBal) % Convert.ToInt64(256));

			//ATREF
			SendBuff[15] = Convert.ToByte(tempStr2.Substring(16, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr2.Substring(18, 2),16);
			SendBuff[17] = Convert.ToByte(tempStr2.Substring(20, 2),16);
			SendBuff[18] = Convert.ToByte(tempStr2.Substring(22, 2),16);
			SendBuff[19] = Convert.ToByte(tempStr2.Substring(24, 2),16);
			SendBuff[20] = Convert.ToByte(tempStr2.Substring(26, 2),16);
			
			//TTREFd
			SendBuff[21] = Convert.ToByte(tempStr2.Substring(42, 2),16);
			SendBuff[22] = Convert.ToByte(tempStr2.Substring(44, 2),16);
			SendBuff[23] = Convert.ToByte(tempStr2.Substring(46, 2),16);
			SendBuff[24] = Convert.ToByte(tempStr2.Substring(48, 2),16);
	
			lst_Log.Items.Add("SAM < 80 70 02 00 14 ");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
										+ tempStr.Substring(2, 2) + " "
										+ tempStr.Substring(4, 2) + " "
										+ tempStr.Substring(6, 2) + " "
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
										+ string.Format("{0:x2}", SendBuff[9]).ToUpper());
										
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 25, ref RecvLen, ref RecvBuff) == true)
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
		}

		private void cmdCredit_Click(object sender, System.EventArgs e)
		{
			string SN, tempStr;
			int i;
			
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

					SN = SN.Replace(" ", "");

				}
			}
			else
			{
				return;
			}

			//Diversify Kcr

			SendBuff[0] = 0x80;
			SendBuff[1] = 0x72;
			SendBuff[2] = 0x2;
			SendBuff[3] = 0x85;
			SendBuff[4] = 0x8;
			SendBuff[5] = Convert.ToByte(SN.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(SN.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(SN.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(SN.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(SN.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(SN.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(SN.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(SN.Substring(14, 2),16);


			lst_Log.Items.Add("SAM < 80 72 02 85 08");
			lst_Log.Items.Add("    <<" + SN.Substring(0, 2) + " "
										+ SN.Substring(2, 2) + " "
										+ SN.Substring(4, 2) + " "
										+ SN.Substring(6, 2) + " "
										+ SN.Substring(8, 2) + " "
										+ SN.Substring(10, 2) + " "
										+ SN.Substring(12, 2) + " "
										+ SN.Substring(14, 2));

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

			//Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE4;
			SendBuff[2] = 0x1;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;

			//4 bytes reference
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;

			lst_Log.Items.Add("MCU < 80 E4 01 00 04");
			lst_Log.Items.Add("    <<AA BB CC DD");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6119")
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

			//Get Response to get result
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x19;

			lst_Log.Items.Add("MCU < 80 C0 00 00 19");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 27;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=24; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("MCU >> " + tempStr);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}

			
			//Verify Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7C;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;
			
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x1D;
			//Ref
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;
			//MAC
			SendBuff[9] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(6, 2),16);

			//Transaction Type
			SendBuff[13] = Convert.ToByte(tempStr.Substring(8, 2),16);

			//Balance
			SendBuff[14] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr.Substring(14, 2),16);

			//ATREF
			SendBuff[17] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[18] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[19] = Convert.ToByte(tempStr.Substring(20, 2),16);
			SendBuff[20] = Convert.ToByte(tempStr.Substring(22, 2),16);
			SendBuff[21] = Convert.ToByte(tempStr.Substring(24, 2),16);
			SendBuff[22] = Convert.ToByte(tempStr.Substring(26, 2),16);

			//Max Balance
			SendBuff[23] = Convert.ToByte(tempStr.Substring(28, 2),16);
			SendBuff[24] = Convert.ToByte(tempStr.Substring(30, 2),16);
			SendBuff[25] = Convert.ToByte(tempStr.Substring(32, 2),16);

			//TTREFc
			SendBuff[26] = Convert.ToByte(tempStr.Substring(34, 2),16);
			SendBuff[27] = Convert.ToByte(tempStr.Substring(36, 2),16);
			SendBuff[28] = Convert.ToByte(tempStr.Substring(38, 2),16);
			SendBuff[29] = Convert.ToByte(tempStr.Substring(40, 2),16);

			//TTREFd
			SendBuff[30] = Convert.ToByte(tempStr.Substring(42, 2),16);
			SendBuff[31] = Convert.ToByte(tempStr.Substring(44, 2),16);
			SendBuff[32] = Convert.ToByte(tempStr.Substring(46, 2),16);
			SendBuff[33] = Convert.ToByte(tempStr.Substring(48, 2),16);


			lst_Log.Items.Add("SAM < 80 7C " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 1D");
			lst_Log.Items.Add("    <<AA BB CC DD " + tempStr.Substring(0, 2) + " "
													+ tempStr.Substring(2, 2) + " "
													+ tempStr.Substring(4, 2) + " "
													+ tempStr.Substring(6, 2) + " "
													+ tempStr.Substring(8, 2) + " "
													+ tempStr.Substring(10, 2) + " "
													+ tempStr.Substring(12, 2) + " "
													+ tempStr.Substring(14, 2) + " "
													+ tempStr.Substring(16, 2) + " "
													+ tempStr.Substring(18, 2) + " "
													+ tempStr.Substring(20, 2) + " "
													+ tempStr.Substring(22, 2));

			lst_Log.Items.Add("    <<" + tempStr.Substring(24, 2) + " "
										+ tempStr.Substring(26, 2) + " "
										+ tempStr.Substring(28, 2) + " "
										+ tempStr.Substring(30, 2) + " "
										+ tempStr.Substring(32, 2) + " "
										+ tempStr.Substring(34, 2) + " "
										+ tempStr.Substring(36, 2) + " "
										+ tempStr.Substring(38, 2) + " "
										+ tempStr.Substring(40, 2) + " "
										+ tempStr.Substring(42, 2) + " "
										+ tempStr.Substring(44, 2) + " "
										+ tempStr.Substring(46, 2) + " "
										+ tempStr.Substring(48, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 34, ref RecvLen, ref RecvBuff) == true)
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

			txtMaxBalance.Text = Get_Balance(Convert.ToByte(tempStr.Substring(28, 2),16), Convert.ToByte(tempStr.Substring(30, 2),16), Convert.ToByte(tempStr.Substring(32, 2),16));

			txtInqAmount.Text = Get_Balance(Convert.ToByte(tempStr.Substring(10, 2),16), Convert.ToByte(tempStr.Substring(12, 2),16), Convert.ToByte(tempStr.Substring(14, 2),16));


			//Prepare ACOS Transaction
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7E;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;

			SendBuff[3] = 0xE2;
			SendBuff[4] = 0xD;

			//Amount to Credit
			SendBuff[5] = Convert.ToByte(Convert.ToInt64(txtCreditAmount.Text) / 65536);
			SendBuff[6] = Convert.ToByte((Convert.ToInt64(txtCreditAmount.Text) / 256) % 65536 % 256);
			SendBuff[7] = Convert.ToByte(Convert.ToInt64(txtCreditAmount.Text) % 256);

			//TTREFd
			SendBuff[8] = Convert.ToByte(tempStr.Substring(42, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(44, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(46, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(48, 2),16);

			//ATREF
			SendBuff[12] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[13] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[14] = Convert.ToByte(tempStr.Substring(20, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(22, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr.Substring(24, 2),16);
			SendBuff[17] = Convert.ToByte(tempStr.Substring(26, 2),16);



			lst_Log.Items.Add("SAM < 80 7E " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " E2 0D");
			lst_Log.Items.Add("    <<" + string.Format("{0:x2}", SendBuff[5]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[6]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[7]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[8]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[9]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[10]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[11]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[12]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[13]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[14]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[15]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[16]).ToUpper() + " " 
									+ string.Format("{0:x2}", SendBuff[17]).ToUpper());


			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if(Send_APDU_SAM(ref SendBuff, 18, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "610B")
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

			//Get Response to get result
			
			SendBuff[0] = 0x0;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0xB;

			lst_Log.Items.Add("SAM < 00 C0 00 00 0B");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 0xD;

			if(Send_APDU_SAM(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[11]).ToUpper() + string.Format("{0:x2}",RecvBuff[12]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[11]).ToUpper() + string.Format("{0:x2}",RecvBuff[12]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=10; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("SAM >> " + tempStr);
					lst_Log.Items.Add("SAM > " + string.Format("{0:x2}",RecvBuff[11]).ToUpper() + string.Format("{0:x2}",RecvBuff[12]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}


			//Credit
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE2;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0xB;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);
			SendBuff[13] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[14] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(20, 2),16);


			lst_Log.Items.Add("MCU < 80 E2 00 00 0B ");
						
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
										+ tempStr.Substring(2, 2) + " "
										+ tempStr.Substring(4, 2) + " "
										+ tempStr.Substring(6, 2) + " "
										+ tempStr.Substring(8, 2) + " "
										+ tempStr.Substring(10, 2) + " "
										+ tempStr.Substring(12, 2) + " "
										+ tempStr.Substring(14, 2) + " "
										+ tempStr.Substring(16, 2) + " "
										+ tempStr.Substring(18, 2) + " "
										+ tempStr.Substring(20, 2));
									
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 16, ref RecvLen, ref RecvBuff) == true)
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
			else
			{
				return;
			}


			//Perform Verify Inquire Account w/ Credit Key and new ammount

			//Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE4;
			SendBuff[2] = 0x1;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x4;

			//4 bytes reference
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;

			lst_Log.Items.Add("MCU < 80 E4 01 00 04");
			lst_Log.Items.Add("    <<AA BB CC DD");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 9, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[0]).ToUpper()+ string.Format("{0:x2}", RecvBuff[1]).ToUpper() != "6119")
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

			//Get Response to get result
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xC0;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x19;

			lst_Log.Items.Add("MCU < 80 C0 00 00 19");
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 27;

			if(Send_APDU_SLT(ref SendBuff, 5, ref RecvLen, ref RecvBuff) == true)
			{
				if (string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper() != "9000")
				{
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;
					return;
				}
				else
				{
					//Retrieve Result
					tempStr = "";

					for(i = 0; i <=24; i++)
					{
						tempStr = tempStr + string.Format("{0:x2}",RecvBuff[i]).ToUpper() + " ";
					}
					
					lst_Log.Items.Add("MCU >> " + tempStr);
					lst_Log.Items.Add("MCU > " + string.Format("{0:x2}",RecvBuff[25]).ToUpper() + string.Format("{0:x2}",RecvBuff[26]).ToUpper());
					lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

					tempStr = tempStr.Replace(" ", "");
				}
			}
			else
			{
				return;
			}

			//Verify Inquire Account
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0x7C;

			if (rb_3DES.Checked == true)
				SendBuff[2] = 0x2;
			else if(rb_DES.Checked == true)
				SendBuff[2] = 0x3;
			
			SendBuff[3] = 0x0;
			SendBuff[4] = 0x1D;
			//Ref
			SendBuff[5] = 0xAA;
			SendBuff[6] = 0xBB;
			SendBuff[7] = 0xCC;
			SendBuff[8] = 0xDD;
			//MAC
			SendBuff[9] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(6, 2),16);

			//Transaction Type
			SendBuff[13] = Convert.ToByte(tempStr.Substring(8, 2),16);

			//Balance
			SendBuff[14] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[16] = Convert.ToByte(tempStr.Substring(14, 2),16);

			//ATREF
			SendBuff[17] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[18] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[19] = Convert.ToByte(tempStr.Substring(20, 2),16);
			SendBuff[20] = Convert.ToByte(tempStr.Substring(22, 2),16);
			SendBuff[21] = Convert.ToByte(tempStr.Substring(24, 2),16);
			SendBuff[22] = Convert.ToByte(tempStr.Substring(26, 2),16);

			//Max Balance
			SendBuff[23] = Convert.ToByte(tempStr.Substring(28, 2),16);
			SendBuff[24] = Convert.ToByte(tempStr.Substring(30, 2),16);
			SendBuff[25] = Convert.ToByte(tempStr.Substring(32, 2),16);

			//TTREFc
			SendBuff[26] = Convert.ToByte(tempStr.Substring(34, 2),16);
			SendBuff[27] = Convert.ToByte(tempStr.Substring(36, 2),16);
			SendBuff[28] = Convert.ToByte(tempStr.Substring(38, 2),16);
			SendBuff[29] = Convert.ToByte(tempStr.Substring(40, 2),16);

			//TTREFd
			SendBuff[30] = Convert.ToByte(tempStr.Substring(42, 2),16);
			SendBuff[31] = Convert.ToByte(tempStr.Substring(44, 2),16);
			SendBuff[32] = Convert.ToByte(tempStr.Substring(46, 2),16);
			SendBuff[33] = Convert.ToByte(tempStr.Substring(48, 2),16);


			lst_Log.Items.Add("SAM < 80 7C " + string.Format("{0:x2}", SendBuff[2]).ToUpper() + " 00 1D");
			lst_Log.Items.Add("    <<AA BB CC DD " + tempStr.Substring(0, 2) + " "
													+ tempStr.Substring(2, 2) + " "
													+ tempStr.Substring(4, 2) + " "
													+ tempStr.Substring(6, 2) + " "
													+ tempStr.Substring(8, 2) + " "
													+ tempStr.Substring(10, 2) + " "
													+ tempStr.Substring(12, 2) + " "
													+ tempStr.Substring(14, 2) + " "
													+ tempStr.Substring(16, 2) + " "
													+ tempStr.Substring(18, 2) + " "
													+ tempStr.Substring(20, 2) + " "
													+ tempStr.Substring(22, 2));

			lst_Log.Items.Add("    <<" + tempStr.Substring(24, 2) + " "
										+ tempStr.Substring(26, 2) + " "
										+ tempStr.Substring(28, 2) + " "
										+ tempStr.Substring(30, 2) + " "
										+ tempStr.Substring(32, 2) + " "
										+ tempStr.Substring(34, 2) + " "
										+ tempStr.Substring(36, 2) + " "
										+ tempStr.Substring(38, 2) + " "
										+ tempStr.Substring(40, 2) + " "
										+ tempStr.Substring(42, 2) + " "
										+ tempStr.Substring(44, 2) + " "
										+ tempStr.Substring(46, 2) + " "
										+ tempStr.Substring(48, 2));

			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;

			if (Send_APDU_SAM(ref SendBuff, 34, ref RecvLen, ref RecvBuff) == true)
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

		}


			private void Debit_ACOS2(string tempStr)
			{

			//Debit without returning debit certificate for ACOS2 or lower
			int RecvLen;
			
			SendBuff[0] = 0x80;
			SendBuff[1] = 0xE6;
			SendBuff[2] = 0x0;
			SendBuff[3] = 0x0;
			SendBuff[4] = 0xB;
			SendBuff[5] = Convert.ToByte(tempStr.Substring(0, 2),16);
			SendBuff[6] = Convert.ToByte(tempStr.Substring(2, 2),16);
			SendBuff[7] = Convert.ToByte(tempStr.Substring(4, 2),16);
			SendBuff[8] = Convert.ToByte(tempStr.Substring(6, 2),16);
			SendBuff[9] = Convert.ToByte(tempStr.Substring(8, 2),16);
			SendBuff[10] = Convert.ToByte(tempStr.Substring(10, 2),16);
			SendBuff[11] = Convert.ToByte(tempStr.Substring(12, 2),16);
			SendBuff[12] = Convert.ToByte(tempStr.Substring(14, 2),16);
			SendBuff[13] = Convert.ToByte(tempStr.Substring(16, 2),16);
			SendBuff[14] = Convert.ToByte(tempStr.Substring(18, 2),16);
			SendBuff[15] = Convert.ToByte(tempStr.Substring(20, 2),16);


			lst_Log.Items.Add("MCU < 80 E6 00 00 0B ");
			lst_Log.Items.Add("    <<" + tempStr.Substring(0, 2) + " "
										+ tempStr.Substring(2, 2) + " "
										+ tempStr.Substring(4, 2) + " "
										+ tempStr.Substring(6, 2) + " "
										+ tempStr.Substring(8, 2) + " "
										+ tempStr.Substring(10, 2) + " "
										+ tempStr.Substring(12, 2) + " "
										+ tempStr.Substring(14, 2) + " "
										+ tempStr.Substring(16, 2) + " "
										+ tempStr.Substring(18, 2) + " "
										+ tempStr.Substring(20, 2));
			
			lst_Log.SelectedIndex = lst_Log.Items.Count - 1;

			RecvLen = 2;
			
			if(Send_APDU_SLT(ref SendBuff, 16, ref RecvLen, ref RecvBuff) == true)
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

		private void txt_PIN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F
        
			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		private void txt_New_PIN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F
        
			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
				
		}

		private void txtCreditAmount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Key Input
			if (e.KeyChar < 48 || e.KeyChar  > 57)
				if (e.KeyChar != (char)(Keys.Back))
					e.Handled = true;
				
		}

		private void txtDebitAmount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Key Input
			if (e.KeyChar < 48 || e.KeyChar  > 57)
				if (e.KeyChar != (char)(Keys.Back))
					e.Handled = true;
				
		}

				
	}
}
