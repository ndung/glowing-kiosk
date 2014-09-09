using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ACOSAccount
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		 
		int retcode, hContext, hCard,Aprotocol, ReaderCount, ActiveProtocol; 
		int  i, tmpindx, RecvBuffLen, SendBuffLen; 	
		string  tmpStr, sTemp, rreader;  
		byte indx; 
		byte LastTran; 
		long SendLen, RecvLen, Amount, tmpVal;

		ModWinsCard.APDURec apdu = new ModWinsCard.APDURec();
		ModWinsCard.SCARD_IO_REQUEST ioRequest = new ModWinsCard.SCARD_IO_REQUEST();
		byte[] array = new byte[256]; 
		
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		
		byte[] CRnd = new byte[8];    // Card random number
		byte[] TRnd = new byte[8];    // Terminal random number
		byte[] cKey = new byte[16]; 
		byte[] tKey = new byte[16]; 
		byte[] tmpArray = new byte[32];
		byte[] tmpResult = new byte[8];
		byte[] ReverseKey = new byte[16];      // Reverse of Terminal Key
		byte[] SessionKey = new byte[16];
				
		long[] tmpBalance = new long[4]; 
		byte[] tmpKey = new byte [16];         // certify key to verify MAC
		byte[] TTREFc = new byte [4]; 
		byte[] TTREFd = new byte [4]; 
		byte[] ATREF = new byte [6]; 
	
		private System.Windows.Forms.Button btnReaderPort;
		internal System.Windows.Forms.GroupBox GBSucureOpt;
		internal System.Windows.Forms.RadioButton RB3DES;
		internal System.Windows.Forms.RadioButton RBDES;
		internal System.Windows.Forms.Button btnExit;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Button btnClear;
		internal System.Windows.Forms.Button btnConnect;
		internal System.Windows.Forms.Button btnDisconnect;
		internal System.Windows.Forms.ListBox lstOutput;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox txtAmount;
		internal System.Windows.Forms.TextBox txtRevoke;
		internal System.Windows.Forms.TextBox txtCertify;
		internal System.Windows.Forms.TextBox txtDebit;
		internal System.Windows.Forms.TextBox txtCredit;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.GroupBox GBAccount;
		internal System.Windows.Forms.Button btnRevoke;
		internal System.Windows.Forms.Button btnInquire;
		internal System.Windows.Forms.Button btnDebit;
		internal System.Windows.Forms.Button btnCredit;
		internal System.Windows.Forms.GroupBox GBSecure;
		internal System.Windows.Forms.Button btnFormat;
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
			string ReaderList = ""+Convert.ToChar(0);
			char[] delimiter = new char[1];
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
            this.btnReaderPort = new System.Windows.Forms.Button();
            this.GBSucureOpt = new System.Windows.Forms.GroupBox();
            this.RB3DES = new System.Windows.Forms.RadioButton();
            this.RBDES = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.GBAccount = new System.Windows.Forms.GroupBox();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.btnInquire = new System.Windows.Forms.Button();
            this.btnDebit = new System.Windows.Forms.Button();
            this.btnCredit = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.GBSecure = new System.Windows.Forms.GroupBox();
            this.txtRevoke = new System.Windows.Forms.TextBox();
            this.txtCertify = new System.Windows.Forms.TextBox();
            this.txtDebit = new System.Windows.Forms.TextBox();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFormat = new System.Windows.Forms.Button();
            this.GBSucureOpt.SuspendLayout();
            this.GBAccount.SuspendLayout();
            this.GBSecure.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReaderPort
            // 
            this.btnReaderPort.Location = new System.Drawing.Point(8, 24);
            this.btnReaderPort.Name = "btnReaderPort";
            this.btnReaderPort.Size = new System.Drawing.Size(104, 24);
            this.btnReaderPort.TabIndex = 53;
            this.btnReaderPort.Text = "Reader Name";
            this.btnReaderPort.Click += new System.EventHandler(this.btnReaderPort_Click);
            // 
            // GBSucureOpt
            // 
            this.GBSucureOpt.Controls.Add(this.RB3DES);
            this.GBSucureOpt.Controls.Add(this.RBDES);
            this.GBSucureOpt.Enabled = false;
            this.GBSucureOpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBSucureOpt.Location = new System.Drawing.Point(144, 24);
            this.GBSucureOpt.Name = "GBSucureOpt";
            this.GBSucureOpt.Size = new System.Drawing.Size(104, 80);
            this.GBSucureOpt.TabIndex = 47;
            this.GBSucureOpt.TabStop = false;
            this.GBSucureOpt.Text = "Security Option";
            // 
            // RB3DES
            // 
            this.RB3DES.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RB3DES.Location = new System.Drawing.Point(16, 48);
            this.RB3DES.Name = "RB3DES";
            this.RB3DES.Size = new System.Drawing.Size(80, 24);
            this.RB3DES.TabIndex = 7;
            this.RB3DES.Text = "3DES";
            this.RB3DES.CheckedChanged += new System.EventHandler(this.RB3DES_CheckedChanged);
            // 
            // RBDES
            // 
            this.RBDES.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBDES.Location = new System.Drawing.Point(16, 24);
            this.RBDES.Name = "RBDES";
            this.RBDES.Size = new System.Drawing.Size(80, 24);
            this.RBDES.TabIndex = 6;
            this.RBDES.Text = "DES";
            this.RBDES.CheckedChanged += new System.EventHandler(this.RBDES_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(488, 392);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(104, 24);
            this.btnExit.TabIndex = 51;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(264, 8);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(88, 16);
            this.Label2.TabIndex = 52;
            this.Label2.Text = "Result";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(376, 392);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 24);
            this.btnClear.TabIndex = 50;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(8, 56);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 24);
            this.btnConnect.TabIndex = 46;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(264, 392);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(104, 24);
            this.btnDisconnect.TabIndex = 49;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(264, 24);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(328, 355);
            this.lstOutput.TabIndex = 48;
            // 
            // GBAccount
            // 
            this.GBAccount.Controls.Add(this.btnRevoke);
            this.GBAccount.Controls.Add(this.btnInquire);
            this.GBAccount.Controls.Add(this.btnDebit);
            this.GBAccount.Controls.Add(this.btnCredit);
            this.GBAccount.Controls.Add(this.Label6);
            this.GBAccount.Controls.Add(this.txtAmount);
            this.GBAccount.Enabled = false;
            this.GBAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBAccount.Location = new System.Drawing.Point(8, 264);
            this.GBAccount.Name = "GBAccount";
            this.GBAccount.Size = new System.Drawing.Size(248, 128);
            this.GBAccount.TabIndex = 55;
            this.GBAccount.TabStop = false;
            this.GBAccount.Text = "Account";
            // 
            // btnRevoke
            // 
            this.btnRevoke.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevoke.Location = new System.Drawing.Point(128, 88);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(104, 23);
            this.btnRevoke.TabIndex = 5;
            this.btnRevoke.Text = "Revoke Debit";
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // btnInquire
            // 
            this.btnInquire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInquire.Location = new System.Drawing.Point(128, 56);
            this.btnInquire.Name = "btnInquire";
            this.btnInquire.Size = new System.Drawing.Size(104, 23);
            this.btnInquire.TabIndex = 4;
            this.btnInquire.Text = "Inquire";
            this.btnInquire.Click += new System.EventHandler(this.btnInquire_Click);
            // 
            // btnDebit
            // 
            this.btnDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebit.Location = new System.Drawing.Point(8, 88);
            this.btnDebit.Name = "btnDebit";
            this.btnDebit.Size = new System.Drawing.Size(104, 23);
            this.btnDebit.TabIndex = 3;
            this.btnDebit.Text = "Debit";
            this.btnDebit.Click += new System.EventHandler(this.btnDebit_Click);
            // 
            // btnCredit
            // 
            this.btnCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredit.Location = new System.Drawing.Point(8, 56);
            this.btnCredit.Name = "btnCredit";
            this.btnCredit.Size = new System.Drawing.Size(104, 23);
            this.btnCredit.TabIndex = 2;
            this.btnCredit.Text = "Credit";
            this.btnCredit.Click += new System.EventHandler(this.btnCredit_Click);
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(8, 24);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(72, 16);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(88, 24);
            this.txtAmount.MaxLength = 10;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(144, 20);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GBSecure
            // 
            this.GBSecure.Controls.Add(this.txtRevoke);
            this.GBSecure.Controls.Add(this.txtCertify);
            this.GBSecure.Controls.Add(this.txtDebit);
            this.GBSecure.Controls.Add(this.txtCredit);
            this.GBSecure.Controls.Add(this.Label5);
            this.GBSecure.Controls.Add(this.Label4);
            this.GBSecure.Controls.Add(this.Label3);
            this.GBSecure.Controls.Add(this.label1);
            this.GBSecure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBSecure.Location = new System.Drawing.Point(8, 128);
            this.GBSecure.Name = "GBSecure";
            this.GBSecure.Size = new System.Drawing.Size(248, 128);
            this.GBSecure.TabIndex = 54;
            this.GBSecure.TabStop = false;
            this.GBSecure.Text = "Security Keys";
            // 
            // txtRevoke
            // 
            this.txtRevoke.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRevoke.Location = new System.Drawing.Point(88, 96);
            this.txtRevoke.MaxLength = 8;
            this.txtRevoke.Name = "txtRevoke";
            this.txtRevoke.Size = new System.Drawing.Size(152, 20);
            this.txtRevoke.TabIndex = 7;
            // 
            // txtCertify
            // 
            this.txtCertify.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertify.Location = new System.Drawing.Point(88, 72);
            this.txtCertify.MaxLength = 8;
            this.txtCertify.Name = "txtCertify";
            this.txtCertify.Size = new System.Drawing.Size(152, 20);
            this.txtCertify.TabIndex = 6;
            // 
            // txtDebit
            // 
            this.txtDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebit.Location = new System.Drawing.Point(88, 48);
            this.txtDebit.MaxLength = 8;
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new System.Drawing.Size(152, 20);
            this.txtDebit.TabIndex = 5;
            // 
            // txtCredit
            // 
            this.txtCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredit.Location = new System.Drawing.Point(88, 24);
            this.txtCredit.MaxLength = 8;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(152, 20);
            this.txtCredit.TabIndex = 4;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(8, 96);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(88, 23);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Revoke Debit";
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(8, 72);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(56, 23);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Certify";
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(8, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(64, 23);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Debit";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Credit";
            // 
            // btnFormat
            // 
            this.btnFormat.Enabled = false;
            this.btnFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormat.Location = new System.Drawing.Point(8, 88);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(104, 23);
            this.btnFormat.TabIndex = 56;
            this.btnFormat.Text = "Format ACOS";
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 421);
            this.Controls.Add(this.btnFormat);
            this.Controls.Add(this.GBAccount);
            this.Controls.Add(this.GBSecure);
            this.Controls.Add(this.btnReaderPort);
            this.Controls.Add(this.GBSucureOpt);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lstOutput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACOS Account";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GBSucureOpt.ResumeLayout(false);
            this.GBAccount.ResumeLayout(false);
            this.GBAccount.PerformLayout();
            this.GBSecure.ResumeLayout(false);
            this.GBSecure.PerformLayout();
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
			// Established using ScardEstablishedContext()
			RBDES.Checked=true;
			RBDES_CheckedChanged(sender,e);

			retcode = ModWinsCard.SCardEstablishContext(hContext, 0, 0, ref hContext);
			
			if (retcode !=  ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("SCardEstablishContext Error"); 
			}		
		
		}
	
		private void btnReaderPort_Click(object sender, System.EventArgs e)
		{
			//used to split null delimited strings into string arrays
			char[] delimiter = new char[1];
			delimiter[0] = Convert.ToChar(0);

			// using SCardListReaderGroups() to determine reader to use
			string mzGroupList = ""+Convert.ToChar(0);

			int pcchGroups = -1;	//SCARD_AUTOALLOCATE
			
			retcode = ModWinsCard.SCardListReaderGroups(hContext,ref mzGroupList, ref pcchGroups);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lstOutput.Items.Add("SCardListReaderGroups Error!");
				return;
			}
			string[] mzGroups = mzGroupList.Split(delimiter);
	
			string ReaderList = ""+Convert.ToChar(0);

			string temp = ""+Convert.ToChar(0);
			
			int pcchReaders= -1;
			
			// List PCSC card readers installed 
			retcode = ModWinsCard.SCardListReaders(this.hContext, temp, ref ReaderList, ref pcchReaders);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lstOutput.Items.Add ("SCardListReader Error!");
			}
			
			string[] pcReaders = ReaderList.Split(delimiter);
			
			foreach (string RName in pcReaders)
			{
				lstOutput.Items.Add("ReaderName: "+RName);
			}
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
		}

		private void btnConnect_Click(object sender, System.EventArgs e)
		{
			//used to split null delimited strings into string arrays
			char[] delimiter = new char[1];
			delimiter[0] = Convert.ToChar(0);

			//to determine the reader using SCardListReaderGroups 
			string mzGroupList = ""+Convert.ToChar(0);

			int pcchGroups = -1;	//SCARD_AUTOALLOCATE
			
			// List PCSC card readers installed 
			retcode = ModWinsCard.SCardListReaderGroups(this.hContext, ref mzGroupList, ref pcchGroups);
			
			string[] mzGroups = mzGroupList.Split(delimiter);	
			string ReaderList = ""+Convert.ToChar(0);
			
			string temp = ""+Convert.ToChar(0);

			int pcchReaders= -1;
			
			retcode = ModWinsCard.SCardListReaders(this.hContext, temp, ref ReaderList, ref pcchReaders);
		
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lstOutput.Items.Add ("SCardListReader Error!");
			}
			string[] pcReaders = ReaderList.Split(delimiter);
			
			// Connect to the reader using hContext handle and obtain hCard handle  
			retcode = ModWinsCard.SCardConnect(hContext, pcReaders[0], ModWinsCard.SCARD_SHARE_EXCLUSIVE, 0 | 1, ref hCard, ref ActiveProtocol);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
                lstOutput.Items.Add(retcode);
                lstOutput.Items.Add(ModWinsCard.GetScardErrMsg(retcode));
				lstOutput.Items.Add ("Connection Error"); 				
			}
			else
			{
				lstOutput.Items.Add ("Connection OK");
				rreader = pcReaders[0];
			}	
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;

			GBSecure.Enabled = true;
			GBSucureOpt.Enabled = true;
			btnFormat.Enabled = true;

		}

		private void btnDisconnect_Click(object sender, System.EventArgs e)
		{
			//Disconnect  and unpower card
			retcode = ModWinsCard.SCardDisconnect(hCard,ModWinsCard.SCARD_UNPOWER_CARD);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("Disconnection Error!"); 
			}
			else
			{
				lstOutput.Items.Add ("Disconnect OK");
			}
	
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;

			GBSecure.Enabled = false;
			GBAccount.Enabled = false;
			GBSucureOpt.Enabled = false;
			btnFormat.Enabled = false;
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			lstOutput.Items.Clear();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			//terminate the application
			retcode = ModWinsCard.SCardReleaseContext(hContext);
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			Application.Exit();
		}

		// this routine will encrypt 8-byte data with 8-byte key
		// the result is stored in data
		public void DES(ref byte Data, ref byte key) 
		{
			Chain3DES.Chain_DES(ref Data, ref key, 0, 1, Chain3DES.DATA_ENCRYPT);
		}
		
		// this routine will use 3DES algo to encrypt 8-byte data with 16-byte key
		// the result is stored in data
		public void TripleDES(ref byte Data,ref byte key)
		{
			Chain3DES.Chain_DES(ref Data, ref key, 1, 1, Chain3DES.DATA_ENCRYPT);
		}

		// MAC as defined in ACOS manual
		// receives 8-byte Key and 16-byte Data
		// result is stored in Data
		public void mac(byte[] Data, byte[]  key)
		{
			int i;   

			DES(ref Data[0],ref key[0]);
			for (i=0; i<8; i++)
				Data[i] = Data[i] ^= Data [i + 8];
			
			DES(ref Data[0], ref key[0]);
		}	
	
		// Triple MAC as defined in ACOS manual
		// receives 16-byte Key and 16-byte Data
		// result is stored in Data
		public void TripleMAC(byte[] Data, byte[] key)		
		{
			int i;
			
			TripleDES(ref Data[0], ref key[0]);

			for (i=0; i<8; i++)
				Data[i] = Data[i] ^= Data[i + 8];

			TripleDES(ref Data[0], ref key[0]);
		}

		private void PerformTransmitAPDU(ref ModWinsCard.APDURec apdu) 
		{
            ModWinsCard.SCARD_IO_REQUEST SendRequest;
			ModWinsCard.SCARD_IO_REQUEST RecvRequest;

			SendBuff[0] = apdu.bCLA;
			SendBuff[1] = apdu.bINS;
			SendBuff[2] = apdu.bP1;
			SendBuff[3] = apdu.bP2;
			SendBuff[4] = apdu.bP3;

			if (apdu.IsSend) 
			{
				for (indx=0; indx<apdu.bP3; indx++)
					SendBuff[5 + indx] = apdu.Data[indx];

				SendBuffLen = 5 + apdu.bP3;
				RecvBuffLen = 2;
			}
			else
			{
				SendBuffLen = 5;
				RecvBuffLen = 2 + apdu.bP3;
			}

			SendRequest.dwProtocol = Aprotocol;
			SendRequest.cbPciLength = 8;

			RecvRequest.dwProtocol = Aprotocol;
			RecvRequest.cbPciLength = 8;

			retcode = ModWinsCard.SCardTransmit(hCard, ref SendRequest,  ref SendBuff[0], SendBuffLen, ref SendRequest,  ref RecvBuff[0], ref RecvBuffLen);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				lstOutput.Items.Add("SCardTransmit Error!");
			}				
			else
			{
				lstOutput.Items.Add("SCardTransmit OK...");
			}

			sTemp = "";
			// do loop for sendbuffLen
			for (indx=0; indx< SendBuffLen; indx++)
				sTemp = sTemp + " " + string.Format("{0:X2}", SendBuff[indx]);
           
			// Display Send Buffer Value
			lstOutput.Items.Add("Send Buffer : " + sTemp);
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

			sTemp = "";
			// do loop for RecvbuffLen
			for (indx=0; indx< RecvBuffLen; indx++)
				sTemp = sTemp + " " + string.Format("{0:X2}", RecvBuff[indx]);
        
			// Display Receive Buffer Value
			lstOutput.Items.Add("Receive Buffer:" + sTemp);
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

			if (apdu.IsSend == false)
			{
				for (indx=0; indx< apdu.bP3 + 2; indx++)
					apdu.Data[indx] = RecvBuff[indx];
			} 
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
		}

		private void RBDES_Checked(object sender, System.EventArgs e)
		{
						txtCredit.MaxLength = 8;
			txtDebit.MaxLength = 8;
			txtCertify.MaxLength = 8;
			txtRevoke.MaxLength = 8;
		}

		private void RB3DES_Click(object sender, System.EventArgs e)
		{
			
			txtCredit.MaxLength = 16;
			txtDebit.MaxLength = 16;
			txtCertify.MaxLength = 16;
			txtRevoke.MaxLength = 16;
		}

		private void ClearFields()
		{
			txtCredit.Text = "";
			txtDebit.Text = "";
			txtCertify.Text = "";
			txtRevoke.Text = "";
			txtAmount.Text = "";
		}

		private void btnFormat_Click(object sender, System.EventArgs e)
		{
			// Validate data template
			if ( ! ValidTemplate())
			{
				if(txtCredit.MaxLength == 8)
				{
					lstOutput.Items.Add("Please enter 8 digit security keys");	
				}
				else
				{
					lstOutput.Items.Add("Please enter 16 digit security keys");	
				}
				return;
			}

			// Check if card inserted is an ACOS card
			if (! CheckACOS())  
			{  
				lstOutput.Items.Add("Please insert an ACOS card");
			}
			lstOutput.Items.Add("ACOS card is detected.");

			// Submit Issuer Code
			SubmitIC();
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}	
			// Select FF 02
			SelectFile(0xFF, 0x02);
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			/* Write to FF 02
				This step will define the Option registers and Security Option registers.
				Personalization bit is not set. Although Secret Codes may be set
				individually, this program adopts uniform encryption option for
				all codes to simplify coding. Issuer Code (IC) is not encrypted
				to remove risk of locking the ACOS card for wrong IC submission. */

			if (RBDES.Checked == true)    // DES option only
			{
				tmpArray[0] = 0x29;		  // 00h  3-DES is not set	
			}
			else
			{
				tmpArray[0] = 0x2B;         // 02h  3-DES is enabled
			}	
			tmpArray[1] = 0x00;             // 00    Security option register
			tmpArray[2] = 0x03;             // 00    No of user files
			tmpArray[3] = 0x00;             // 00    Personalization bit

			writeRecord(0x00, 0x00, 0x04, 0x04, ref tmpArray);

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			lstOutput.Items.Add("FF 02 is updated");

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			lstOutput.Items.Add("Account files are enabled.");

			//Submit Issuer Code to write into FF 05 and FF 06
			SubmitIC();
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//  Select FF 05
			SelectFile(0xFF, 0x05);
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			// Write to FF 05 Record(0)
			tmpArray[0] = 0x00;        // TRANSTYP 0
			tmpArray[1] = 0x00;        // (3 bytes
			tmpArray[2] = 0x00;        // reserved for
			tmpArray[3] = 0x00;        //  BALANCE 0)

			writeRecord(0x00, 0x00, 0x04, 0x04, ref tmpArray);

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Record(1)
			tmpArray[0] = 0x00;          // (2 bytes reserved
			tmpArray[1] = 0x00;          //  for ATC 0)
			tmpArray[2] = 0x01;          // Set CHECKSUM 0
			tmpArray[3] = 0x00;          // 00h

			writeRecord(0x00, 0x01, 0x04, 0x04, ref tmpArray);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			// Record 02
			tmpArray[0] = 0x00;          // TRANSTYP 1
			tmpArray[1] = 0x00;          // (3 bytes
			tmpArray[2] = 0x00;         //  reserved for
			tmpArray[3] = 0x00;          //  BALANCE 1)

			writeRecord(0x00, 0x02, 0x04, 0x04, ref tmpArray);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Record 03
			tmpArray[0] = 0x00;          // (2 bytes reserved
			tmpArray[1] = 0x00;          //  for ATC 1)
			tmpArray[2] = 0x01;          // Set CHECKSUM 1
			tmpArray[3] = 0x00;          // 00h

			writeRecord(0x00, 0x03, 0x04, 0x04, ref tmpArray);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Record(4)
			tmpArray[0] = 0xFF;          // (3 bytes
			tmpArray[1] = 0xFF;          //  initialized for
			tmpArray[2] = 0xFF;          //  MAX BALANCE)
			tmpArray[3] = 0x00;           // 00h

			writeRecord(0x00, 0x04, 0x04, 0x04, ref tmpArray);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			// Record 05
			tmpArray[0] = 0x00;           // (4 bytes
			tmpArray[1] = 0x00;           //  reserved
			tmpArray[2] = 0x00;           //  for
			tmpArray[3] = 0x00;           //  AID)

			writeRecord(0x00, 0x05, 0x04, 0x04, ref tmpArray);
        
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Record 06
			tmpArray[0] = 0x00;           // (4 bytes
			tmpArray[1] = 0x00;           //  reserved
			tmpArray[2] = 0x00;           //  for
			tmpArray[3] = 0x00;           //  TTREF_C)

			writeRecord(0x00, 0x06, 0x04, 0x04, ref tmpArray);
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Record 07
			tmpArray[0] = 0x00;           // (4 bytes
			tmpArray[1] = 0x00;           //  reserved
			tmpArray[2] = 0x00;           //  for
			tmpArray[3] = 0x00;           //  TTREF_D)

			writeRecord(0x00, 0x07, 0x04, 0x04, ref tmpArray);
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			lstOutput.Items.Add("FF 05 is updated");

			// Select FF 06
			SelectFile(0xFF, 0x06);
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			if (RBDES.Checked == true)    // DES option uses 8-byte key
			{
				// Record 00 for Debit key
				tmpStr = "";
				tmpStr = txtDebit.Text;
            
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x00, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 01 for Credit key
				tmpStr = "";
				tmpStr = txtCredit.Text;
            
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x01, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 02 for Certify key
				tmpStr = "";
				tmpStr = txtCertify.Text;

				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x02, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 03 for Revoke Debit key
				tmpStr = "";
				tmpStr = txtRevoke.Text;
            
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x03, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}
			}
			else
			{  // 3-DES option uses 16-byte key

				// Record 04 for Left half of Debit key
				tmpStr = txtDebit.Text;
				
				for (indx=0; indx<8; indx++)           // Left half of Debit key
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));
           
				writeRecord(0x00, 0x04, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 00 for Right half of Debit key
				for (indx=8; indx<16; indx++)          // Right half of Debit key
					tmpArray[indx - 8] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x00, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 05 for Left half of Credit key
				tmpStr = txtCredit.Text;
				for (indx=0; indx<8; indx++)           // Left half of Credit key
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));
          
				writeRecord(0x00, 0x05, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 01 for Right half of Credit key
				for (indx=8; indx<16; indx++)          // Right half of Credit key
					tmpArray[indx - 8] = (byte)Asc(tmpStr.Substring(indx,1));
			
				writeRecord(0x00, 0x01, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 06 for Left half of Certify key
				tmpStr = txtCertify.Text;
				for (indx=0; indx<8; indx++)           // Left half of Certify key
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));
         
				writeRecord(0x00, 0x06, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 02 for Right half of Certify key
				for (indx=8; indx<16; indx++)          // Right half of Certify key
					tmpArray[indx - 8] = (byte)Asc(tmpStr.Substring(indx,1));
            
				writeRecord(0x00, 0x02, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 07 for Left half of Revoke Debit key
				tmpStr = txtRevoke.Text;
				for (indx=0; indx<8; indx++)          // Left half of Revoke Debit key
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x07, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 03 for Right half of Revoke Debit key
				for (indx=8; indx<16; indx++)          // Right half of Revoke Debit key
					tmpArray[indx - 8] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x03, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

			}

			lstOutput.Items.Add("FF 06 is updated");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
			GBAccount.Enabled = true;

		}

		private bool ValidTemplate() 
		{
			if ((txtCredit.Text.Length) < txtCredit.MaxLength) 
			{
				txtCredit.SelectionStart = txtCredit.Text.Length;
				txtCredit.Focus();
				return false; 
			}
			if ((txtDebit.Text.Length) < txtDebit.MaxLength) 
			{	
				txtDebit.SelectionStart = (txtDebit.Text.Length);
				txtDebit.Focus();
				return false; 
			}

			if ((txtCertify.Text.Length) < txtCertify.MaxLength) 
			{
				txtCertify.SelectionStart = (txtCertify.Text.Length);
				txtCertify.Focus();
				return false;
			}
			if ((txtRevoke.Text.Length) < txtRevoke.MaxLength) 
			{	
				txtRevoke.SelectionStart = (txtRevoke.Text.Length);
				txtRevoke.Focus();
				return false;
			}
			return true;
		}

		private bool CheckACOS() 
		{
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return false;
			}
		
			SelectFile(0xFF, 0x00);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{  
				return false;
			}
			// Check for File FF 01
			SelectFile(0xFF, 0x01);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{  
				return false;
			}
			// Check for File FF 02
			SelectFile(0xFF, 0x02);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return false;
			}
			return true; 			
		}


		private void SubmitIC() 
		{
			apdu.Data = array;
			
			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0x20;       // INS
			apdu.bP1 = 0x07;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x08;         // P3

			apdu.Data[0] = 0x41;   // A
			apdu.Data[1] = 0x43;   // C
			apdu.Data[2] = 0x4F;   // O
			apdu.Data[3] = 0x53;   // S
			apdu.Data[4] = 0x54;   // T
			apdu.Data[5] = 0x45;   // E
			apdu.Data[6] = 0x53;   // S
			apdu.Data[7] = 0x54;   // T

			apdu.IsSend = true;
			lstOutput.Items.Add("Submit IC");

			PerformTransmitAPDU(ref apdu);
		}


		private void SelectFile(byte HiAddr, byte LoAddr) 
		{
			apdu.Data = array;

			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0xA4;       // INS
			apdu.bP1 = 0x00;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x02;         // P3

			apdu.Data[0] = HiAddr;     // Value of High Byte
			apdu.Data[1] = LoAddr;     // Value of Low Byte

			apdu.IsSend = true;

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
			  return; 
			}
		}

		private void writeRecord(int caseType, byte RecNo, byte maxLen, byte dataLen, ref byte[] ApduIn) 
		{
			int i;  

			if (caseType == 1)    // If card data is to be erased before writing new data
			{					  //  Re-initialize card values to $00
				apdu.Data = array;
				apdu.bCLA = 0x80;        // CLA
				apdu.bINS = 0xD2;        // INS
				apdu.bP1 = RecNo;        // Record No
				apdu.bP2 = 0x00;          // P2
				apdu.bP3 = maxLen;       // Length of Data

				apdu.IsSend = true;

				for(i=0; i< maxLen; i++)
					apdu.Data[i] = ApduIn[i];

				PerformTransmitAPDU(ref apdu);
			}
			//Write data to card
			apdu.Data = array;
			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0xD2;       // INS
			apdu.bP1 = RecNo;       // Record No
			apdu.bP2 = 0x00;        // P2
			apdu.bP3 = dataLen;     // Length of Data

			apdu.IsSend = true;

			for(i=0; i< maxLen; i++)
				apdu.Data[i] = ApduIn[i];

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return;
			}

		}

		private void btnCredit_Click(object sender, System.EventArgs e)
		{
			int indx; 
			string tmpStr; 
		
			// Check if Credit key and valid Transaction value are provided
			if ((txtCredit.Text.Length) < txtCredit.MaxLength)
			{
				txtCredit.Focus();
				return;
			}
			if (txtAmount.Text.Trim() == " " || txtAmount.Text == "")
			{							 
				txtAmount.Focus();
				return;
			}

			if (! IsNumeric(txtAmount.Text))
			{
				txtAmount.Focus();
				txtAmount.SelectionStart = 0;
				txtAmount.SelectionLength = txtAmount.Text.Length;
				return;
			}
			if (System.Convert.ToInt64(txtAmount.Text) > 16777215)
			{
				txtAmount.Text = "16777215";
				txtAmount.Focus();
				return;
			}

			retcode = 0;

			// Check if card inserted is an ACOS card
			if (! CheckACOS())  
			{  
				lstOutput.Items.Add("Please insert an ACOS card");
				return;
			}
			lstOutput.Items.Add("ACOS card is detected.");

			// Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
			//    Arbitrary data is 1111h
			for (indx=0; indx<4; indx++)
				tmpArray[indx] = 0x01;

			InquireAccount(0x02, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return;
			}

			// Issue GET RESPONSE command with Le = 19h or 25 bytes
			GetResponse(RecvBuff);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return;
			}

			// Store ACOS card values for TTREFc and ATREF
			for (indx=0; indx<4; indx++)
				TTREFc[indx] = RecvBuff[indx + 17];
			
			for (indx=0; indx<6; indx++)
				ATREF[indx] = RecvBuff[indx + 8];

			//  Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
			//    use tmpArray as the data block
			Amount = System.Convert.ToInt64(txtAmount.Text);
			tmpArray[0] = 0xE2;
			tmpVal = (Amount / 256);
			tmpVal = (tmpVal / 256);
			tmpVal = tmpVal % 256;
			tmpArray[1] = (byte)(tmpVal);                       
			tmpVal =  (Amount / 256);
			tmpArray[2] = (byte) (tmpVal % 256);                
			tmpArray[3] = (byte) (Amount % 256);

			for (indx=0; indx<4; indx++)
				tmpArray[indx + 4] = TTREFc[indx];

			for (indx=0; indx<6; indx++)
				tmpArray[indx + 8] = ATREF[indx];

			tmpArray[13] += 1;               // increment last byte of ATREF
			tmpArray[14] = 0x00;
			tmpArray[15] = 0x00;

			// Generate applicable MAC values, MAC result will be stored in tmpArray
			tmpStr = "";
			tmpStr = txtCredit.Text;

			for (indx=0; indx<tmpStr.Length; indx++)
				tmpKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));
			
			if (RBDES.Checked == true) 
			{
				mac(tmpArray, tmpKey);
			}
			else
			{
				TripleMAC(tmpArray, tmpKey);
			}

			// Format Credit command data and execute credit command
			// Using tmpArray, the first four bytes are carried over
			tmpVal = (Amount / 256);
			tmpVal = (tmpVal / 256);
			tmpArray[4] =(byte)(tmpVal % 256);                  // Amount MSByte
			tmpVal = (Amount / 256);
			tmpArray[5] =(byte)(tmpVal % 256);                  // Amount Middle Byte
			tmpArray[6] =(byte)(Amount % 256);                  // Amount LSByte

			for (indx=0; indx<4; indx++)
				tmpArray[indx + 7] = TTREFc[indx];

			retcode = CreditAmount(ref tmpArray);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return;
			}

			lstOutput.Items.Add("Credit transaction completed");
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
			
		}

		int Asc(string character)
		{
			if (character.Length == 1)
			{
				System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
				int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
				return (intAsciiCode);
			}
			else
			{
				throw new Exception("Character is not valid.");
			}
		}

		public static bool IsNumeric(object value)
		{
			try    
			{
				int i = Convert.ToInt32(value.ToString());
				return true; 
			}
			catch (FormatException)    
			{
				return false;
			}
		}

		public long InquireAccount(byte keyNo, ref byte[] DataIn)
		{
			int indx; 
			string tmpStr; 

			apdu.Data = array; 

			apdu.bCLA = 0x80;    // CLA
			apdu.bINS = 0xE4;    // INS
			apdu.bP1 = keyNo;    // Key No
			apdu.bP2 = 0x00;     // P2
			apdu.bP3 = 0x04;     // Length of Data

			apdu.IsSend = true;

			for (indx=0; indx<4; indx++)
				apdu.Data[indx] = DataIn[indx];
			
			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return retcode;
			}
			tmpStr = "";
			
			for (indx=0; indx<1; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

			if (RecvBuff[0] != 0x61)
			{
				lstOutput.Items.Add("Error!!");
				return 0;
			}

			if (tmpStr.Trim() != "61 19") // SW1/SW2 must be equal to 6119h
			{
				return retcode;
			}
			return 0;
		}

		public int GetResponse(byte[] buff)
		{			 
			int ctr;

			apdu.Data = array;

			// Get Response Command 
			apdu.bCLA = 0x80;   // CLA
			apdu.bINS = 0xC0;   // INS
			apdu.bP1 = 0x00;    // P1
			apdu.bP2 = 0x00;    // P2
			apdu.bP3 = 0x19;    // P3

			apdu.IsSend = false;

			lstOutput.Items.Add(">>>> Get Response...");

			PerformTransmitAPDU(ref apdu);

			for(ctr=0;ctr<0x19;ctr++)
				buff[ctr] = apdu.Data[ctr];

			return 0;								
		}

		private int CreditAmount(ref byte[] CreditData) 
		{
			apdu.Data = array;

			apdu.bCLA = 0x80;        // CLA
			apdu.bINS = 0xE2;        // INS
			apdu.bP1 = 0x00;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x0B;         // P3

			apdu.IsSend = true;

			for (indx=0;indx<11;indx++)
				//SendBuff(indx + 5) = CreditData(indx)
				apdu.Data[indx] = CreditData[indx];

			tmpStr = "";

			for (indx=0; indx<SendLen; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

			lstOutput.Items.Add("Invoke Credit Command.");

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return retcode;
			}

			tmpStr = "";

			for (indx=0; indx<=1; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);	

			if (tmpStr.Trim() != "90 00") 
			{
				return -1000;
			}
		
			return 0;
		}

		private void btnInquire_Click(object sender, System.EventArgs e)
		{
			// Check if Certify key is provided
			if ((txtCertify.Text.Length) < txtCertify.MaxLength) 
			{
				txtCertify.Focus();
			}

			//  Check if card inserted is an ACOS card
			if (! CheckACOS()) 
			{
				lstOutput.Items.Add("Please insert an ACOS card.");
			}
			
			lstOutput.Items.Add("ACOS card is detected.");

			// Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
			//    Arbitrary data is 01 01 01 01
			for (indx=0; indx<3; indx++)
				tmpArray[indx] = 0x01;

			InquireAccount(0x02, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Issue GET RESPONSE command with Le = 19h or 25 bytes.
			GetResponse(RecvBuff);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			//Check integrity of data returned by card.
			//Build MAC input data. Extract the info from ACOS card in Dataout.
			LastTran = RecvBuff[4];
			tmpBalance[1] = RecvBuff[7];
			tmpBalance[2] = RecvBuff[6];
			tmpBalance[2] = tmpBalance[2] * 256;
			tmpBalance[3] = RecvBuff[5];
			tmpBalance[3] = tmpBalance[3] * 256;
			tmpBalance[3] = tmpBalance[3] * 256;
			tmpBalance[0] = tmpBalance[1] + tmpBalance[2] + tmpBalance[3];

			for (indx=0; indx<4; indx++)
				TTREFc[indx] = RecvBuff[indx + 17];
       
			for (indx=0; indx<4; indx++)
				TTREFd[indx] = RecvBuff[indx + 21];
       
			for (indx=0; indx<6; indx++)
				ATREF[indx] = RecvBuff[indx + 8];
			

			//Move data from ACOS card as input to MAC calculations
			tmpArray[4] = RecvBuff[4];          // 4 BYTE MAC + LAST TRANS TYPE
			for (indx=0; indx<3; indx++)        // Copy BALANCE
				tmpArray[indx + 5] = RecvBuff[indx + 5];
			
			for (indx=0; indx<6; indx++)        // Copy ATREF
				tmpArray[indx + 8] = ATREF[indx];
			
			//Pad 2 bytes of zero value according to formula.
			tmpArray[14] = 0x00;
			tmpArray[15] = 0x00;

			for (indx=0; indx<4; indx++)        // Copy TTREFc
				tmpArray[indx + 16] = TTREFc[indx];
		
			for (indx=0; indx<4; indx++)       // Copy TTREFd
				tmpArray[indx + 20] = TTREFd[indx];
			
			
			//  Generate applicable MAC values
			tmpStr = txtCertify.Text;
			for (indx=0; indx<tmpStr.Length; indx++)
				tmpKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));		


			if (RBDES.Checked == true) 
			{ 
				mac(tmpArray, tmpKey);
			}
			else
			{
				TripleMAC(tmpArray, tmpKey);
			}

			//Compare MAC values
			for (indx=0; indx<4; indx++)

				if (tmpArray[indx] != RecvBuff[indx])
				{
					lstOutput.Items.Add("MAC is incorrect, data integrity is jeopardized.");
					lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
				}

			// Display relevant data from ACOS card
			switch (LastTran)  
			{ 
				case 1: tmpStr = "DEBIT";
					break;
				case 2: tmpStr = "REVOKE DEBIT";
					break;
				case 3: tmpStr = "CREDIT";
					break;
				default : tmpStr = "NOT DEFINED";
					break;
			}

			lstOutput.Items.Add("Last transaction is:" + tmpStr);
			lstOutput.Items.Add("Success Inquiring Balance.");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
						
			txtAmount.Clear();
			txtAmount.Text = txtAmount.Text + "" + string.Format("{0:d}", tmpBalance[0]); 
			
		}

		private void btnDebit_Click(object sender, System.EventArgs e)
		{
			int indx; 

			// Check if Debit key and valid Transaction value are provided
			if ((txtDebit.Text.Length) < txtDebit.MaxLength) 
			{	txtDebit.Focus();
				return;
			}
			if (txtAmount.Text == "") 
			{	txtAmount.Focus();
				return;
			}
			if (! IsNumeric(txtAmount.Text))
			{	txtAmount.Focus();
				txtAmount.SelectionStart = 0;
				txtAmount.SelectionLength = txtAmount.Text.Length;
				return;
			}
			if (System.Convert.ToInt64(txtAmount.Text) > 16777215)
			{	txtAmount.Text = "16777215";
				txtAmount.Focus();
				return;
			}

			retcode = 0;

			//  Check if card inserted is an ACOS card
			if (! CheckACOS()) 
			{
				lstOutput.Items.Add("Please insert an ACOS card.");
				return;
			}
			lstOutput.Items.Add("ACOS card is detected.");

			//Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
			//    Arbitrary data is 01 01 01 01
			for (indx=0; indx<4; indx++)
				tmpArray[indx] = 0x01;

			InquireAccount(0x02, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}
			//Issue GET RESPONSE command with Le = 19h or 25 bytes
			GetResponse(RecvBuff);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			//Store ACOS card values for TTREFd and ATREF
			for (indx=0;indx<4; indx++)
				TTREFd[indx] = RecvBuff[indx + 21];

			for (indx=0;indx<6; indx++) 
				ATREF[indx] = RecvBuff[indx + 8];

			//  Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
			//    use tmpArray as the data block

			Amount = System.Convert.ToInt64(txtAmount.Text);
			tmpArray[0] = 0xE6;
			tmpVal =(Amount / 256);
			tmpVal =(tmpVal / 256);
			tmpArray[1] = (byte)(tmpVal % 256);            // Amount MSByte
			tmpVal = (Amount / 256);
			tmpArray[2] =(byte)(tmpVal % 256);             // Amount Middle Byte
			tmpArray[3] =(byte)(Amount % 256);           // Amount LSByte

			
			for (indx=0; indx<4; indx++)
				tmpArray[indx + 4] = TTREFd[indx];

			for (indx=0; indx<6; indx++)
				tmpArray[indx + 8] = ATREF[indx];

			tmpArray[13] += 1 ;               // increment last byte of ATREF
			tmpArray[14] = 0x00;
			tmpArray[15] = 0x00;

			//Generate applicable MAC values, MAC result will be stored in tmpArray			
			tmpStr = txtDebit.Text;
			for (indx=0; indx<tmpStr.Length; indx++)
				tmpKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));		

			if (RBDES.Checked == true) 
			{
				mac(tmpArray, tmpKey);
			}
			else
			{
				TripleMAC(tmpArray, tmpKey);
			}

			//  Format Debit command data and execute debit command
			//    Using tmpArray, the first four bytes are carried over
			tmpVal = (Amount / 256);
			tmpVal = (tmpVal / 256);
			tmpArray[4] =(byte)(tmpVal % 256);                  // Amount MSByte
			tmpVal = (Amount / 256);
			tmpArray[5] =(byte) (tmpVal % 256);                  // Amount Middle Byte
			tmpArray[6] =(byte) (Amount % 256);                  // Amount LSByte

			/*for (indx=0; indx<4;indx ++)
				tmpArray[indx + 7] = TTREFd[indx];*/

			 for (indx=0; indx<6; indx ++)
				tmpArray[indx + 7] = ATREF[indx];

			retcode = DebitAmount(ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			lstOutput.Items.Add("Debit transaction completed");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
			
		}

		private int DebitAmount(ref byte[] DebitData)
		{
			int indx; 
			string tmpStr; 

			tmpStr = " ";

			apdu.Data = array;

			apdu.bCLA = 0x80;  // CLA
			apdu.bINS = 0xE6;  // INS
			apdu.bP1 = 0x00;    // P1
			apdu.bP2 = 0x00;    // P2
			apdu.bP3 = 0x0B;    // P3

			apdu.IsSend = true;

			for (indx=0; indx<11; indx++)
				apdu.Data[indx] = DebitData[indx];

			for (indx=0; indx<SendLen; indx++) 
				tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

			lstOutput.Items.Add("Invoke Debit Command.");

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{  
				return retcode;
			}									

			tmpStr = "";
			
			for (indx=0; indx<=1; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

			if (tmpStr.Trim() != "90 00") 
			{
				return -1000;
			}
			return 0;
		}

		private void btnRevoke_Click(object sender, System.EventArgs e)
		{

			// Check if Debit key and valid Transaction value are provided
			if ((txtRevoke.Text.Length) < txtRevoke.MaxLength) 
			{
				txtRevoke.Focus();
				return;
			}

			if (txtAmount.Text == "") 
			{
				txtAmount.Focus();
				return;
			}
			if (! IsNumeric(txtAmount.Text))
			{
				txtAmount.Focus();
				txtAmount.SelectionStart = 0;
				txtAmount.SelectionLength = txtAmount.Text.Length;
				return;
			}

			if (System.Convert.ToInt64(txtAmount.Text) > 16777215)
			{
				txtAmount.Text = "16777215";
				txtAmount.Focus();
				return;
			}

			retcode=0;

			//  Check if card inserted is an ACOS card
			if (! CheckACOS()) 
			{
				lstOutput.Items.Add("Please insert an ACOS card.");
				return;
			}
			
			lstOutput.Items.Add("ACOS card is detected.");

			// Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
			//    Arbitrary data is 01 01 01 01
			for (indx=0; indx<4; indx++)
				tmpArray[indx] = 0x01;

			InquireAccount(0x02, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Issue GET RESPONSE command with Le = 19h or 25 bytes.
			GetResponse(RecvBuff);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			// Store ACOS card values for TTREFd and ATREF
			for (indx=0; indx<4; indx++)
				TTREFd[indx] = RecvBuff[indx + 21];

			for (indx=0; indx<6; indx++)
				ATREF[indx] = RecvBuff[indx + 8];

			// Store ACOS card values for TTREFd and ATREF
			for (indx=0; indx<4; indx++)
				TTREFd[indx] = RecvBuff[indx + 21];

			for (indx=0; indx<6; indx++)
				ATREF[indx] = RecvBuff[indx + 8];

			//Prepare MAC data block: E8 + AMT + TTREFd + ATREF + 00 + 00
			//use tmpArray as the data block
			Amount = System.Convert.ToInt64(txtAmount.Text);
			tmpArray[0] = 0xE8;
			tmpVal = (Amount / 256);
			tmpVal = (tmpVal / 256);
			tmpArray[1] = (byte)(tmpVal % 256);                  // Amount MSByte
			tmpVal = (Amount / 256);
			tmpArray[2] = (byte)(tmpVal % 256);                   // Amount Middle Byte
			tmpArray[3] = (byte)(Amount % 256) ;                  // Amount LSByte

			for (indx=0; indx<4; indx++)
				tmpArray[indx + 4] = TTREFd[indx];

			for (indx=0; indx<6; indx++)
				tmpArray[indx + 8] = ATREF[indx];

			tmpArray[13] += 1;               // increment last byte of ATREF
			tmpArray[14] = 0x00;
			tmpArray[15] = 0x00;

			//  Generate applicable MAC values, MAC result will be stored in tmpArray
			tmpStr = txtRevoke.Text;

			for (indx=0; indx<tmpStr.Length; indx++)
				tmpKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));		

			if (RBDES.Checked == true) 
			{
				mac(tmpArray, tmpKey);
			}
			else
			{
				TripleMAC(tmpArray, tmpKey);
			}

			// Execute Revoke Debit command data and execute credit command
			// Using tmpArray, the first four bytes storing the MAC value are carried over
			retcode = RevokeDebit(ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			lstOutput.Items.Add("Revoke Debit transaction completed");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
			
		}

		private int RevokeDebit(ref byte[] RevDebData) 
		{
			apdu.Data = array;

			apdu.bCLA = 0x80;        // CLA
			apdu.bINS = 0xE8;        // INS
			apdu.bP1 = 0x00;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x04;         // P3
		
			tmpStr = "";
		
			apdu.IsSend = true;

			for (indx=0; indx<4; indx++)
				apdu.Data[indx] = RevDebData[indx];

			for (indx=0; indx<SendLen; indx++) 
				tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);


			lstOutput.Items.Add("Invoke RevokeDebit Command.");

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return retcode;
			}									

			for (indx=0; indx<=1; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
       
			if (tmpStr.Trim() != "90 00") 
			{
				return -1000;
			}
			return 0;	
		}

		private void RBDES_CheckedChanged(object sender, System.EventArgs e)
		{
			ClearFields();
			txtCredit.MaxLength = 8;
			txtDebit.MaxLength = 8;
			txtCertify.MaxLength = 8;
			txtRevoke.MaxLength = 8;
		}

		private void RB3DES_CheckedChanged(object sender, System.EventArgs e)
		{
			ClearFields();
			txtCredit.MaxLength = 16;
			txtDebit.MaxLength = 16;
			txtCertify.MaxLength = 16;
			txtRevoke.MaxLength = 16;
		}

	
	
	}
}
/*============================================================================
' Description : This sample program demonstrates the Account functions using 
'               ACOS card.
'
'============================================================================*/

