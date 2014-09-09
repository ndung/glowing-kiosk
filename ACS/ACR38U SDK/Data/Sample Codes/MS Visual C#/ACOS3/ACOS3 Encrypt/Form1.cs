using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ACOSEncrypt
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		int retcode, hContext, hCard,Aprotocol, ActiveProtocol; 
		string  tmpStr, sTemp, rreader;  
        ModWinsCard.SCARD_IO_REQUEST ioRequest = new ModWinsCard.SCARD_IO_REQUEST();
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
	    byte[] SessionKey = new byte[16];
	    byte[] array = new byte[256]; 
		ModWinsCard.APDURec apdu = new ModWinsCard.APDURec();
	    byte[] CRnd = new byte[8];    // Card random number
		byte[] TRnd = new byte[8];    // Terminal random number
		byte[] ReverseKey = new byte[16];      // Reverse of Terminal Key
		byte[] cKey = new byte[16]; 
		byte[] tKey = new byte[16]; 
		byte[] tmpArray = new byte[33]; 
		int  tmpindx,RecvBuffLen, SendBuffLen; 
		byte indx, tmpByte; 


		internal System.Windows.Forms.GroupBox GBCodeSub;
		internal System.Windows.Forms.ComboBox cmbCode;
		internal System.Windows.Forms.TextBox txtValue;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.GroupBox GBKeyTemp;
		internal System.Windows.Forms.TextBox txtTerminalKey;
		internal System.Windows.Forms.TextBox txtCardKey;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.GroupBox GBSucureOpt;
		internal System.Windows.Forms.RadioButton RB3DES;
		internal System.Windows.Forms.RadioButton RBDES;
		internal System.Windows.Forms.GroupBox GBEncrypt;
		internal System.Windows.Forms.RadioButton RBNotEnc;
		internal System.Windows.Forms.RadioButton RBEncrypt;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button btnReaderPort;
		internal System.Windows.Forms.Button btnSubmit;
		internal System.Windows.Forms.Button btnSetValue;
		internal System.Windows.Forms.Button btnFormat;
		internal System.Windows.Forms.Button btnExit;
		internal System.Windows.Forms.Button btnClear;
		internal System.Windows.Forms.Button btnConnect;
		internal System.Windows.Forms.Button btnDisconnect;
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
            this.GBCodeSub = new System.Windows.Forms.GroupBox();
            this.cmbCode = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnSetValue = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.GBKeyTemp = new System.Windows.Forms.GroupBox();
            this.btnFormat = new System.Windows.Forms.Button();
            this.txtTerminalKey = new System.Windows.Forms.TextBox();
            this.txtCardKey = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.GBSucureOpt = new System.Windows.Forms.GroupBox();
            this.RB3DES = new System.Windows.Forms.RadioButton();
            this.RBDES = new System.Windows.Forms.RadioButton();
            this.GBEncrypt = new System.Windows.Forms.GroupBox();
            this.RBNotEnc = new System.Windows.Forms.RadioButton();
            this.RBEncrypt = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnReaderPort = new System.Windows.Forms.Button();
            this.GBCodeSub.SuspendLayout();
            this.GBKeyTemp.SuspendLayout();
            this.GBSucureOpt.SuspendLayout();
            this.GBEncrypt.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBCodeSub
            // 
            this.GBCodeSub.Controls.Add(this.cmbCode);
            this.GBCodeSub.Controls.Add(this.btnSubmit);
            this.GBCodeSub.Controls.Add(this.btnSetValue);
            this.GBCodeSub.Controls.Add(this.txtValue);
            this.GBCodeSub.Controls.Add(this.Label6);
            this.GBCodeSub.Controls.Add(this.Label5);
            this.GBCodeSub.Enabled = false;
            this.GBCodeSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBCodeSub.Location = new System.Drawing.Point(8, 280);
            this.GBCodeSub.Name = "GBCodeSub";
            this.GBCodeSub.Size = new System.Drawing.Size(264, 120);
            this.GBCodeSub.TabIndex = 30;
            this.GBCodeSub.TabStop = false;
            this.GBCodeSub.Text = "Code Submission";
            // 
            // cmbCode
            // 
            this.cmbCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCode.Items.AddRange(new object[] {
            "PIN",
            "App Code 1",
            "App Code 2",
            "App Code 3",
            "App Code 4",
            "App Code 5"});
            this.cmbCode.Location = new System.Drawing.Point(112, 16);
            this.cmbCode.Name = "cmbCode";
            this.cmbCode.Size = new System.Drawing.Size(136, 21);
            this.cmbCode.TabIndex = 16;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(152, 88);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(104, 23);
            this.btnSubmit.TabIndex = 20;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnSetValue
            // 
            this.btnSetValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetValue.Location = new System.Drawing.Point(24, 88);
            this.btnSetValue.Name = "btnSetValue";
            this.btnSetValue.Size = new System.Drawing.Size(104, 23);
            this.btnSetValue.TabIndex = 19;
            this.btnSetValue.Text = "Set Value";
            this.btnSetValue.Click += new System.EventHandler(this.btnSetValue_Click);
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(112, 48);
            this.txtValue.MaxLength = 8;
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(136, 20);
            this.txtValue.TabIndex = 17;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(16, 48);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(80, 16);
            this.Label6.TabIndex = 17;
            this.Label6.Text = "Set Value";
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(16, 24);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(80, 23);
            this.Label5.TabIndex = 15;
            this.Label5.Text = "Code Value";
            // 
            // GBKeyTemp
            // 
            this.GBKeyTemp.Controls.Add(this.btnFormat);
            this.GBKeyTemp.Controls.Add(this.txtTerminalKey);
            this.GBKeyTemp.Controls.Add(this.txtCardKey);
            this.GBKeyTemp.Controls.Add(this.Label4);
            this.GBKeyTemp.Controls.Add(this.Label3);
            this.GBKeyTemp.Enabled = false;
            this.GBKeyTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBKeyTemp.Location = new System.Drawing.Point(8, 160);
            this.GBKeyTemp.Name = "GBKeyTemp";
            this.GBKeyTemp.Size = new System.Drawing.Size(264, 112);
            this.GBKeyTemp.TabIndex = 29;
            this.GBKeyTemp.TabStop = false;
            this.GBKeyTemp.Text = "Key Template";
            // 
            // btnFormat
            // 
            this.btnFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormat.Location = new System.Drawing.Point(152, 80);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(104, 23);
            this.btnFormat.TabIndex = 13;
            this.btnFormat.Text = "Format";
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // txtTerminalKey
            // 
            this.txtTerminalKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminalKey.Location = new System.Drawing.Point(112, 48);
            this.txtTerminalKey.MaxLength = 8;
            this.txtTerminalKey.Name = "txtTerminalKey";
            this.txtTerminalKey.Size = new System.Drawing.Size(136, 20);
            this.txtTerminalKey.TabIndex = 12;
            // 
            // txtCardKey
            // 
            this.txtCardKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardKey.Location = new System.Drawing.Point(112, 16);
            this.txtCardKey.MaxLength = 8;
            this.txtCardKey.Name = "txtCardKey";
            this.txtCardKey.Size = new System.Drawing.Size(136, 20);
            this.txtCardKey.TabIndex = 10;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(8, 48);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(88, 16);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Terminal Key";
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(8, 24);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(88, 23);
            this.Label3.TabIndex = 9;
            this.Label3.Text = "Card Key";
            // 
            // GBSucureOpt
            // 
            this.GBSucureOpt.Controls.Add(this.RB3DES);
            this.GBSucureOpt.Controls.Add(this.RBDES);
            this.GBSucureOpt.Enabled = false;
            this.GBSucureOpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBSucureOpt.Location = new System.Drawing.Point(149, 64);
            this.GBSucureOpt.Name = "GBSucureOpt";
            this.GBSucureOpt.Size = new System.Drawing.Size(112, 88);
            this.GBSucureOpt.TabIndex = 28;
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
            this.RB3DES.Click += new System.EventHandler(this.RB3DES_Click);
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
            this.RBDES.Click += new System.EventHandler(this.RBDES_Click);
            this.RBDES.CheckedChanged += new System.EventHandler(this.RBDES_CheckedChanged);
            // 
            // GBEncrypt
            // 
            this.GBEncrypt.Controls.Add(this.RBNotEnc);
            this.GBEncrypt.Controls.Add(this.RBEncrypt);
            this.GBEncrypt.Enabled = false;
            this.GBEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBEncrypt.Location = new System.Drawing.Point(16, 64);
            this.GBEncrypt.Name = "GBEncrypt";
            this.GBEncrypt.Size = new System.Drawing.Size(120, 88);
            this.GBEncrypt.TabIndex = 27;
            this.GBEncrypt.TabStop = false;
            this.GBEncrypt.Text = "Encrypt Option";
            // 
            // RBNotEnc
            // 
            this.RBNotEnc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBNotEnc.Location = new System.Drawing.Point(8, 24);
            this.RBNotEnc.Name = "RBNotEnc";
            this.RBNotEnc.Size = new System.Drawing.Size(104, 24);
            this.RBNotEnc.TabIndex = 3;
            this.RBNotEnc.Text = "Not Encrypted";
            this.RBNotEnc.CheckedChanged += new System.EventHandler(this.RBNotEnc_Click);
            // 
            // RBEncrypt
            // 
            this.RBEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBEncrypt.Location = new System.Drawing.Point(8, 48);
            this.RBEncrypt.Name = "RBEncrypt";
            this.RBEncrypt.Size = new System.Drawing.Size(104, 24);
            this.RBEncrypt.TabIndex = 4;
            this.RBEncrypt.Text = "Encrypted";
            this.RBEncrypt.Click += new System.EventHandler(this.RBEncrypt_Click);
            this.RBEncrypt.CheckedChanged += new System.EventHandler(this.RBEncrypt_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(504, 376);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(104, 24);
            this.btnExit.TabIndex = 34;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(280, 8);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(92, 16);
            this.Label2.TabIndex = 35;
            this.Label2.Text = "Result";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(392, 376);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 24);
            this.btnClear.TabIndex = 33;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(144, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 24);
            this.btnConnect.TabIndex = 26;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(280, 376);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(104, 24);
            this.btnDisconnect.TabIndex = 32;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(280, 24);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(328, 342);
            this.lstOutput.TabIndex = 31;
            // 
            // btnReaderPort
            // 
            this.btnReaderPort.Location = new System.Drawing.Point(16, 24);
            this.btnReaderPort.Name = "btnReaderPort";
            this.btnReaderPort.Size = new System.Drawing.Size(120, 24);
            this.btnReaderPort.TabIndex = 36;
            this.btnReaderPort.Text = "Reader Name";
            this.btnReaderPort.Click += new System.EventHandler(this.btnReaderPort_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(616, 413);
            this.Controls.Add(this.btnReaderPort);
            this.Controls.Add(this.GBCodeSub);
            this.Controls.Add(this.GBKeyTemp);
            this.Controls.Add(this.GBSucureOpt);
            this.Controls.Add(this.GBEncrypt);
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
            this.Text = "ACOSEncrypt";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GBCodeSub.ResumeLayout(false);
            this.GBCodeSub.PerformLayout();
            this.GBKeyTemp.ResumeLayout(false);
            this.GBKeyTemp.PerformLayout();
            this.GBSucureOpt.ResumeLayout(false);
            this.GBEncrypt.ResumeLayout(false);
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Established using ScardEstablishedContext()
			retcode = ModWinsCard.SCardEstablishContext(hContext, 0, 0, ref hContext);
			
			if (retcode !=  ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("SCardEstablishContext Error"); 
			}		
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
				lstOutput.Items.Add ("Connection Error"); 				
			}
			else
			{
				lstOutput.Items.Add ("Connection OK");
				rreader = pcReaders[0];
			}	
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;

			GBEncrypt.Enabled = true;
			GBKeyTemp.Enabled = true;
			GBCodeSub.Enabled = true;
			GBSucureOpt.Enabled = true;
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
			
			GBEncrypt.Enabled = false;
			GBKeyTemp.Enabled = false;
			GBCodeSub.Enabled = false;
			GBSucureOpt.Enabled = false;

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


		 private void RBNotEnc_Click(object sender, System.EventArgs e)
		{
			ClearTextFields();
			txtCardKey.MaxLength = 8;
			txtTerminalKey.MaxLength = 8;
			
			RBDES.Checked = true;
			RB3DES.Checked = false;
			GBSucureOpt.Enabled = false;
		}

	

		 private void RBEncrypt_Click(object sender, System.EventArgs e)
		{
			ClearTextFields();
			RBDES.Checked = true;
			GBSucureOpt.Enabled = true;
		}


		 private void RBDES_Click(object sender, System.EventArgs e)
		{
			 txtCardKey.Text = "";
			 txtTerminalKey.Text = "";
			 txtCardKey.MaxLength = 8;
			 txtTerminalKey.MaxLength = 8;
		}


		
		 private void RB3DES_Click(object sender, System.EventArgs e)
		{
			txtCardKey.Text = "";
			txtTerminalKey.Text = "";
			txtCardKey.MaxLength = 16;
			txtTerminalKey.MaxLength = 16;
		}

		private void txtCardKey_KeyUp(object sender, System.EventArgs e)
		{
			if (txtCardKey.Text.Length >= txtCardKey.MaxLength)
			 { 
				txtTerminalKey.Focus();
			 }		
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

		private void btnFormat_Click(object sender, System.EventArgs e)
		{
		  
			// Validate data template
			if ( ! ValidTemplate(0))
			{
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
			{	tmpArray[0] = 0x00;		  // 00h  3-DES is not set	
			}
			else
			{	tmpArray[0] = 0x02;         // 02h  3-DES is enabled
			}																				 

			if (RBNotEnc.Checked == true) // Security Option register
			{ 
				tmpArray[1] = 0x00;         // 00h  Encryption is not set
			}
			else
			{  
				tmpArray[1] = 0x7E;        // Encryption on all codes, except IC, enabled		
			}	
			tmpArray[2] = 0x03;			  // 00    No of user files
			tmpArray[3] = 0x00;           // 00    Personalization bit

			writeRecord(0x00, 0x00, 0x04, 0x04,ref tmpArray);

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			lstOutput.Items.Add("FF 02 is updated");

			/*Perform a reset for changes in the ACOS to take effect
            Reconnect reader to accommodate change of cards */
			//retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_RESET_CARD);
            //btnConnect.Select();

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			 lstOutput.Items.Add("Account files are enabled.");

			// Submit Issuer Code to write into FF 03
            SubmitIC();
        
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			
			//  Select FF 03
			SelectFile(0xFF, 0x03);
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			
			// Write to FF 03
			if (RBDES.Checked == true)     // DES option uses 8-byte key
			{
				//  Record 02 for Card key
				tmpStr = txtCardKey.Text;

				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x02, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				//  Record 03 for Terminal key
				tmpStr = txtTerminalKey.Text;
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x03, 0x08, 0x08, ref tmpArray);
            
				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}
			}
			else                          // 3-DES option uses 16-byte key
			{							  // Write Record 02 for Left half of Card key
				tmpStr = txtCardKey.Text;
				for (indx=0; indx<8; indx++)         // Left half of Card key
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x02, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Record 12 for Right half of Card key
				for (indx=8; indx<16; indx++)        // Right half of Card key
					tmpArray[indx - 8] = (byte)Asc(tmpStr.Substring(indx,1));
            
				writeRecord(0, 0x0C, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				// Write Record 03 for Left half of Terminal key
				tmpStr = txtTerminalKey.Text;
            
				for (indx=0; indx<8; indx++)         // Left half of Terminal key
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x03, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				//  Record 13 for Right half of Terminal key
				for (indx=8; indx<16; indx++)          // Right half of Terminal key
					tmpArray[indx - 8] = (byte)Asc(tmpStr.Substring(indx,1));

				writeRecord(0x00, 0x0D, 0x08, 0x08, ref tmpArray);

				if(retcode != ModWinsCard.SCARD_S_SUCCESS)
				{
					return;
				}

				lstOutput.Items.Add("FF 03 is updated");
				lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

			}
		}

		private bool ValidTemplate(int valType) 
		{
			if ((txtCardKey.Text.Length) < (txtCardKey.MaxLength))
			{   txtCardKey.Focus();
				return false;
			}

			if ((txtTerminalKey.Text.Length) < (txtTerminalKey.MaxLength)) 
			{   txtTerminalKey.Focus();
				return false;
			}				
											 
			if( valType == 1)      // validation requires code input
			{  
				if (txtValue.Text.Length < 8) 
				{   txtValue.Focus();
					return false;;
				}										  
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

			return true;; 
			
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

            //SelectFile = retcode;
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
			apdu.bP2 = 0x00;         // P2
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

		private void RBEncrypt_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void RBDES_CheckedChanged(object sender, System.EventArgs e)
		{
			txtCardKey.MaxLength=8;
			txtTerminalKey.MaxLength=8;
			txtCardKey.Text="";
			txtTerminalKey.Text="";
			txtValue.Text = "";
		}

		private void RB3DES_CheckedChanged(object sender, System.EventArgs e)
		{
			txtCardKey.MaxLength=16;
			txtTerminalKey.MaxLength=16;
			txtCardKey.Text="";
			txtTerminalKey.Text="";
			txtValue.Text = "";
		}
	
		private void RBNotEnc_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}


		private void ClearTextFields()
		{
			txtCardKey.Text = "";
			txtTerminalKey.Text = "";
			txtValue.Text = "";
			cmbCode.SelectedIndex = -1;
		}

		private void btnSetValue_Click(object sender, System.EventArgs e)
		{
		
			//Validate data template
			if (! ValidTemplate(1))
			{
				return;
			}

			//Check if card inserted is an ACOS card
			if (! CheckACOS()) 
			{ lstOutput.Items.Add("Please insert an ACOS card");
			}
			lstOutput.Items.Add("ACOS card is detected.");

			//  Submit Issuer Code
			SubmitIC();
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			//  Select FF 03
            SelectFile(0xFF, 0x03);
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			//  Get Code input
			tmpStr = txtValue.Text;
				
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));

			switch (cmbCode.SelectedIndex)
			{ 
				case 0:
					tmpByte = 0x01;
					tmpStr = "PIN Code";
					break;
				case 1:
					tmpByte = 0x05;
					tmpStr = "Application Code 1";
					break;
				case 2:
					tmpByte = 0x06;
					tmpStr = "Application Code 2";
					break;
				case 3 :
					tmpByte = 0x07;
					tmpStr = "Application Code 3";
					break;
				case 4 :
					tmpByte = 0x08;
					tmpStr = "Application Code 4";
					break;
				case 5:
					tmpByte = 0x09;
					tmpStr = "Application Code 5";
					return;
			}
			
			//  Write to corresponding record in FF 03
            writeRecord(0x00, tmpByte, 0x08, 0x08, ref tmpArray);
        
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}
			
			lstOutput.Items.Add(tmpStr + " submitted successfully.");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;		
		}
		
	
		
		private void SubmitCode(byte CodeType, ref byte[] DataIn)
		{
			 int indx; 
			

			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0x20;       // INS
			apdu.bP1 = CodeType;    // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x08;         // P3

			apdu.IsSend = true;

			for(indx=0; indx<8; indx++)
				apdu.Data[indx] = DataIn[indx];

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}
		}
		
		
		public bool ACOSError(byte Sw1, byte Sw2)
		{
			// Check for error returned by ACOS card
			//ACOSError1(Sw1, Sw2);

			if ((Sw1==0x62) && (Sw2==0x81)) 
			{
				lstOutput.Items.Add("Account data may be corrupted.");
			}			
			if (Sw1==0x63) 
			{
				lstOutput.Items.Add("MAC cryptographic checksum is wrong.");
			} 	
			if ((Sw1==0x69) && (Sw2==0x66))
			{
				lstOutput.Items.Add("Command not available or option bit not set.");
			}	
			if ((Sw1==0x69) && (Sw2==0x82)) 
			{
				lstOutput.Items.Add("Security status not satisfied. Secret code, IC or PIN not submitted.");
			}		
			if ((Sw1==0x69) && (Sw2==0x83))
			{ 
				lstOutput.Items.Add("The specified code is locked.");
			}
			if ((Sw1==0x69) && (Sw2==0x85))
			{
				lstOutput.Items.Add("Preceding transaction was not DEBIT or mutual authentication has not been completed.");
			}
			if ((Sw1==0x69) && (Sw2==0xF0))
			{ 
				lstOutput.Items.Add("Data in account is inconsistent. No access unless in Issuer mode.");
			}
			if ((Sw1== 0x6A) && (Sw2==0x82)) 
			{
				lstOutput.Items.Add("Account does not exist.");
			}								   
			if ((Sw1==0x6A) && (Sw2==0x83)) 
			{
				lstOutput.Items.Add("Record not found or file too short.");
			}
			if ((Sw1==0x6A) && (Sw2==0x86)) 
			{
				lstOutput.Items.Add("P1 or P2 is incorrect.");
			}
			if ((Sw1==0x6B) && (Sw2==0x20)) 
			{
				lstOutput.Items.Add("Invalid amount in DEBIT/CREDIT command.");
			}	
			if (Sw1==0x6C) 
			{
				lstOutput.Items.Add("Issue GET RESPONSE with P3 =  to get response data.");
			}
			if (Sw1==0x6D) 
			{
				lstOutput.Items.Add("Unknown INS.");
			}
			if (Sw1==0x6E) 
			{
				lstOutput.Items.Add("Unknown CLA.");
			}
			if ((Sw1==0x6F) && (Sw2==0x10)) 
			{
				lstOutput.Items.Add("Account Transaction Counter at maximum. No more transaction possible.");
			}
			  
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

			return false;
		}																																		  

		private void GetSessionKey()
		{
			// Get Card Key and Terminal Key from Key Template
			tmpStr = txtCardKey.Text;

			for (indx=0; indx< txtCardKey.MaxLength; indx++)
				cKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));

			tmpStr = txtTerminalKey.Text;
			for (indx=0; indx< txtTerminalKey.MaxLength; indx++)
				tKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));
       
			StartSession();

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{	//GetSessionKey = retCode
				return;
			}

			//  Encrypt Random No (CRnd) with Terminal Key (tKey)
			//    tmpArray will hold the 8-byte Enrypted number
			for (indx=0; indx<8; indx++)
				tmpArray[indx] = CRnd[indx];

			if (RBDES.Checked == true) 
			{
				DES(ref tmpArray[0], ref tKey[0]);	
			}
			else
			{
			    TripleDES(ref tmpArray[0], ref tKey[0]);
			}

			//  Issue Authenticate command using 8-byte Encrypted No (tmpArray)
			//    and Random Terminal number (TRnd)
			for (indx=0; indx<8; indx++)
				tmpArray[indx + 8] = TRnd[indx];
        
			Authenticate(ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{	//GetSessionKey = retCode
				return;
			}

			//  Compute for Session Key
			if (RBDES.Checked == true) 
			{
				/*  for single DES
				' prepare SessionKey
				' SessionKey = DES (DES(RNDc, KC) XOR RNDt, KT) */

				// calculate DES(cRnd,cKey)
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = CRnd[indx];

				DES(ref tmpArray[0], ref cKey[0]);

				// XOR the result with tRnd
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = tmpArray[indx] ^= TRnd[indx];
            
				// DES the result with tKey
				DES(ref tmpArray[0],ref tKey[0]);

				// temp now holds the SessionKey
				for (indx=0; indx<8; indx++)
					SessionKey[indx] = tmpArray[indx];
			}
			else
			{
				/*  for triple DES
				' prepare SessionKey
				' Left half SessionKey =  3DES (3DES (CRnd, cKey), tKey)
				' Right half SessionKey = 3DES (TRnd, REV (tKey))
				' tmpArray = 3DES (CRnd, cKey) */

				for (indx=0; indx<8; indx++)
					tmpArray[indx] = CRnd[indx];
            
				TripleDES(ref tmpArray[0], ref cKey[0]);

				// tmpArray = 3DES (tmpArray, tKey)
				TripleDES(ref tmpArray[0],ref tKey[0]);

				// tmpArray holds the left half of SessionKey
				for (indx=0; indx<8;indx++)
					SessionKey[indx] = tmpArray[indx];

				/* compute ReverseKey of tKey
				' just swap its left side with right side
				' ReverseKey = right half of tKey + left half of tKey */
				for (indx=0;indx<8; indx++)
					ReverseKey[indx] = tKey[8 + indx];
           
				for (indx=8;indx<16;indx++)
					ReverseKey[indx] = tKey[indx - 8];

				// compute tmpArray = 3DES (TRnd, ReverseKey)
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = TRnd[indx];

				TripleDES(ref tmpArray[0], ref ReverseKey[0]);

				// tmpArray holds the right half of SessionKey
				for (indx=0; indx<8; indx++)
					SessionKey[indx + 8] = tmpArray[indx];	

			}
    	}
		private void StartSession() 
		{
			
			apdu.Data = array;

			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0x84;       // INS
			apdu.bP1 = 0x00;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x08;         // P3

			apdu.IsSend = false;

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{	
				return;
			}

			// Store the random number generated by the card to Crnd
			for (tmpindx=0; tmpindx<8; tmpindx++)
				CRnd[tmpindx] = apdu.Data[tmpindx];

		}

		private void Authenticate(ref byte[] DataIn) 
		{
			apdu.Data = array;

			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0x82;       // INS
			apdu.bP1 = 0x00;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x10;        // P3

			apdu.IsSend = true;

			for (indx=0; indx<16; indx++)
				apdu.Data[indx] = DataIn[indx];

			PerformTransmitAPDU(ref apdu);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{   
				return;
			}

		}

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			//Validate data template
			if (! ValidTemplate(1))
			{
				return;
			}
			//Check if card inserted is an ACOS card
			if (! CheckACOS()) 
			{
				lstOutput.Items.Add("Please insert an ACOS card");
			}

			lstOutput.Items.Add("ACOS card is detected.");

			// Get Code input
			
			if (RBEncrypt.Checked == true) 
			{
				GetSessionKey();

				if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
				{
					return;
				}

				tmpStr = " ";
				tmpStr = txtValue.Text;
			
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));


				if (RBDES.Checked == true) 
				{
					DES(ref tmpArray[0], ref SessionKey[0]);
				}
				else
				{
					TripleDES(ref tmpArray[0], ref SessionKey[0]);
				}
			}
			switch (cmbCode.SelectedIndex)
			{ 
				case 0:
					tmpByte = 0x06;
					tmpStr = "PIN Code";
					break;
				case 1:
					tmpByte = 0x01;
					tmpStr = "Application Code 1";
					break;
				case 2:
					tmpByte = 0x02;
					tmpStr = "Application Code 2";
					break;
				case 3 :
					tmpByte = 0x03;
					tmpStr = "Application Code 3";
					break;
				case 4 :
					tmpByte = 0x04;
					tmpStr = "Application Code 4";
					break;
				case 5:
					tmpByte = 0x05;
					tmpStr = "Application Code 5";
					return;
			}
			 //  Submit Issuer Code
			 SubmitCode(tmpByte, ref tmpArray);
			 
			 if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			 {
				 return;
			 }
			 lstOutput.Items.Add(tmpStr + " submitted successfully.");
			 lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

    	}
	}
}

/* ==========================================================
' Description : This sample program demonstrate on how to  
'               Encrypt ACOS card using PCSC platform.
'
'========================================================== */