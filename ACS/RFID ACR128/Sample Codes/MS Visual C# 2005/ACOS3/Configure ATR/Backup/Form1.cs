using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.VisualBasic;
namespace Get_ATR
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	
	
	public class Form1 : System.Windows.Forms.Form
	{
		// global declaration
		int hContext, hCard; //card reader context handle //card connection handle
		int AProtocol, retcode;  
		byte[] rdrlist = new byte [100];
		string rreader,Temp;
		int ATRLen, ReaderLen,dwState, Protocol, RecvBuffLen, SendBuffLen;  
		uint PRT;
		short index; 
		ModWinsCard.APDURec apdu = new ModWinsCard.APDURec();
		byte[] SendBuff = new byte [262];
		byte[] RecvBuff = new byte [262];
		byte[] ATR = new byte[33];
		byte[] array = new byte[256]; 
		byte[] tmpAPDU = new byte[35];	
		string[] ctrbyte = new string [14]; 
		string sTemp;
		byte indx; 
		long SendLen, RecvLen;
		int num_historical_byte; 

		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button btnDisconnect;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnReader;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button btnUpdateATR;
		private System.Windows.Forms.ComboBox cbo_rate;
		private System.Windows.Forms.ComboBox cbo_nobytes;
		private System.Windows.Forms.TextBox t1;
		private System.Windows.Forms.TextBox t2;
		private System.Windows.Forms.TextBox t3;
		private System.Windows.Forms.TextBox t4;
		private System.Windows.Forms.TextBox t5;
		private System.Windows.Forms.TextBox t6;
		private System.Windows.Forms.TextBox t7;
		private System.Windows.Forms.TextBox t8;
		private System.Windows.Forms.TextBox t9;
		private System.Windows.Forms.TextBox t10;
		private System.Windows.Forms.TextBox t11;
		private System.Windows.Forms.TextBox t12;
		private System.Windows.Forms.TextBox t13;
		private System.Windows.Forms.TextBox t14;
		private System.Windows.Forms.TextBox t15;
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnReader = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_rate = new System.Windows.Forms.ComboBox();
            this.cbo_nobytes = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.t1 = new System.Windows.Forms.TextBox();
            this.t2 = new System.Windows.Forms.TextBox();
            this.t3 = new System.Windows.Forms.TextBox();
            this.t4 = new System.Windows.Forms.TextBox();
            this.t5 = new System.Windows.Forms.TextBox();
            this.t6 = new System.Windows.Forms.TextBox();
            this.t7 = new System.Windows.Forms.TextBox();
            this.t8 = new System.Windows.Forms.TextBox();
            this.t9 = new System.Windows.Forms.TextBox();
            this.t10 = new System.Windows.Forms.TextBox();
            this.t11 = new System.Windows.Forms.TextBox();
            this.t12 = new System.Windows.Forms.TextBox();
            this.t13 = new System.Windows.Forms.TextBox();
            this.t14 = new System.Windows.Forms.TextBox();
            this.t15 = new System.Windows.Forms.TextBox();
            this.btnUpdateATR = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(48, 56);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 24);
            this.btnConnect.TabIndex = 45;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstOutput);
            this.groupBox2.Location = new System.Drawing.Point(208, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 400);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // lstOutput
            // 
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(8, 16);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(344, 381);
            this.lstOutput.TabIndex = 24;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(216, 416);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(112, 24);
            this.btnDisconnect.TabIndex = 46;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(336, 416);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 24);
            this.btnClear.TabIndex = 47;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(456, 416);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 24);
            this.btnExit.TabIndex = 48;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(48, 88);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 24);
            this.btnReset.TabIndex = 50;
            this.btnReset.Text = "Get ATR";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnReader
            // 
            this.btnReader.Location = new System.Drawing.Point(48, 24);
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(120, 23);
            this.btnReader.TabIndex = 49;
            this.btnReader.Text = "Reader";
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 52;
            this.label1.Text = "Card BaudRate";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(152, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "TA";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "No.of Historical Bytes";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(152, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "TO";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 25;
            this.label5.Text = "Historical Bytes";
            // 
            // cbo_rate
            // 
            this.cbo_rate.Enabled = false;
            this.cbo_rate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "28800",
            "57600",
            "115200"});
            this.cbo_rate.Location = new System.Drawing.Point(16, 168);
            this.cbo_rate.Name = "cbo_rate";
            this.cbo_rate.Size = new System.Drawing.Size(112, 21);
            this.cbo_rate.TabIndex = 25;
            this.cbo_rate.SelectedIndexChanged += new System.EventHandler(this.cbo_rate_SelectedIndexChanged);
            // 
            // cbo_nobytes
            // 
            this.cbo_nobytes.Enabled = false;
            this.cbo_nobytes.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "0A",
            "0B",
            "0C",
            "0D",
            "0E",
            "0F",
            "FF"});
            this.cbo_nobytes.Location = new System.Drawing.Point(16, 224);
            this.cbo_nobytes.Name = "cbo_nobytes";
            this.cbo_nobytes.Size = new System.Drawing.Size(112, 21);
            this.cbo_nobytes.TabIndex = 53;
            this.cbo_nobytes.SelectedIndexChanged += new System.EventHandler(this.cbo_nobytes_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(144, 168);
            this.textBox1.MaxLength = 0;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(32, 20);
            this.textBox1.TabIndex = 54;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(144, 224);
            this.textBox2.MaxLength = 0;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(32, 20);
            this.textBox2.TabIndex = 25;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // t1
            // 
            this.t1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t1.Location = new System.Drawing.Point(9, 280);
            this.t1.MaxLength = 2;
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(32, 20);
            this.t1.TabIndex = 55;
            this.t1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t1.TextChanged += new System.EventHandler(this.t1_TextChanged);
            // 
            // t2
            // 
            this.t2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t2.Location = new System.Drawing.Point(48, 280);
            this.t2.MaxLength = 2;
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(32, 20);
            this.t2.TabIndex = 56;
            this.t2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t2.TextChanged += new System.EventHandler(this.t2_TextChanged);
            // 
            // t3
            // 
            this.t3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t3.Location = new System.Drawing.Point(88, 280);
            this.t3.MaxLength = 2;
            this.t3.Name = "t3";
            this.t3.Size = new System.Drawing.Size(32, 20);
            this.t3.TabIndex = 57;
            this.t3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t3.TextChanged += new System.EventHandler(this.t3_TextChanged);
            // 
            // t4
            // 
            this.t4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t4.Location = new System.Drawing.Point(128, 280);
            this.t4.MaxLength = 2;
            this.t4.Name = "t4";
            this.t4.Size = new System.Drawing.Size(32, 20);
            this.t4.TabIndex = 58;
            this.t4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t4.TextChanged += new System.EventHandler(this.t4_TextChanged);
            // 
            // t5
            // 
            this.t5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t5.Location = new System.Drawing.Point(168, 280);
            this.t5.MaxLength = 2;
            this.t5.Name = "t5";
            this.t5.Size = new System.Drawing.Size(32, 20);
            this.t5.TabIndex = 59;
            this.t5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t5.TextChanged += new System.EventHandler(this.t5_TextChanged);
            // 
            // t6
            // 
            this.t6.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t6.Location = new System.Drawing.Point(8, 312);
            this.t6.MaxLength = 2;
            this.t6.Name = "t6";
            this.t6.Size = new System.Drawing.Size(32, 20);
            this.t6.TabIndex = 60;
            this.t6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t6.TextChanged += new System.EventHandler(this.t6_TextChanged);
            // 
            // t7
            // 
            this.t7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t7.Location = new System.Drawing.Point(48, 312);
            this.t7.MaxLength = 2;
            this.t7.Name = "t7";
            this.t7.Size = new System.Drawing.Size(32, 20);
            this.t7.TabIndex = 61;
            this.t7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t7.TextChanged += new System.EventHandler(this.t7_TextChanged);
            // 
            // t8
            // 
            this.t8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t8.Location = new System.Drawing.Point(88, 312);
            this.t8.MaxLength = 2;
            this.t8.Name = "t8";
            this.t8.Size = new System.Drawing.Size(32, 20);
            this.t8.TabIndex = 62;
            this.t8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t8.TextChanged += new System.EventHandler(this.t8_TextChanged);
            // 
            // t9
            // 
            this.t9.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t9.Location = new System.Drawing.Point(128, 312);
            this.t9.MaxLength = 2;
            this.t9.Name = "t9";
            this.t9.Size = new System.Drawing.Size(32, 20);
            this.t9.TabIndex = 63;
            this.t9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t9.TextChanged += new System.EventHandler(this.t9_TextChanged);
            // 
            // t10
            // 
            this.t10.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t10.Location = new System.Drawing.Point(168, 312);
            this.t10.MaxLength = 2;
            this.t10.Name = "t10";
            this.t10.Size = new System.Drawing.Size(32, 20);
            this.t10.TabIndex = 64;
            this.t10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t10.TextChanged += new System.EventHandler(this.t10_TextChanged);
            // 
            // t11
            // 
            this.t11.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t11.Location = new System.Drawing.Point(8, 344);
            this.t11.MaxLength = 2;
            this.t11.Name = "t11";
            this.t11.Size = new System.Drawing.Size(32, 20);
            this.t11.TabIndex = 65;
            this.t11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t11.TextChanged += new System.EventHandler(this.t11_TextChanged);
            // 
            // t12
            // 
            this.t12.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t12.Location = new System.Drawing.Point(48, 344);
            this.t12.MaxLength = 2;
            this.t12.Name = "t12";
            this.t12.Size = new System.Drawing.Size(32, 20);
            this.t12.TabIndex = 66;
            this.t12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t12.TextChanged += new System.EventHandler(this.t12_TextChanged);
            // 
            // t13
            // 
            this.t13.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t13.Location = new System.Drawing.Point(88, 344);
            this.t13.MaxLength = 2;
            this.t13.Name = "t13";
            this.t13.Size = new System.Drawing.Size(32, 20);
            this.t13.TabIndex = 67;
            this.t13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t13.TextChanged += new System.EventHandler(this.t13_TextChanged);
            // 
            // t14
            // 
            this.t14.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t14.Location = new System.Drawing.Point(128, 344);
            this.t14.MaxLength = 2;
            this.t14.Name = "t14";
            this.t14.Size = new System.Drawing.Size(32, 20);
            this.t14.TabIndex = 68;
            this.t14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t14.TextChanged += new System.EventHandler(this.t14_TextChanged);
            // 
            // t15
            // 
            this.t15.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.t15.Location = new System.Drawing.Point(168, 344);
            this.t15.MaxLength = 2;
            this.t15.Name = "t15";
            this.t15.Size = new System.Drawing.Size(32, 20);
            this.t15.TabIndex = 69;
            this.t15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t15.TextChanged += new System.EventHandler(this.t15_TextChanged);
            // 
            // btnUpdateATR
            // 
            this.btnUpdateATR.Enabled = false;
            this.btnUpdateATR.Location = new System.Drawing.Point(48, 392);
            this.btnUpdateATR.Name = "btnUpdateATR";
            this.btnUpdateATR.Size = new System.Drawing.Size(112, 24);
            this.btnUpdateATR.TabIndex = 70;
            this.btnUpdateATR.Text = "Update ATR";
            this.btnUpdateATR.Click += new System.EventHandler(this.btnUpdateATR_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(574, 443);
            this.Controls.Add(this.btnUpdateATR);
            this.Controls.Add(this.t15);
            this.Controls.Add(this.t14);
            this.Controls.Add(this.t13);
            this.Controls.Add(this.t12);
            this.Controls.Add(this.t11);
            this.Controls.Add(this.t10);
            this.Controls.Add(this.t9);
            this.Controls.Add(this.t8);
            this.Controls.Add(this.t7);
            this.Controls.Add(this.t6);
            this.Controls.Add(this.t5);
            this.Controls.Add(this.t4);
            this.Controls.Add(this.t3);
            this.Controls.Add(this.t2);
            this.Controls.Add(this.t1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cbo_nobytes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnReader);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbo_rate);
            this.Controls.Add(this.textBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure ATR";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
			retcode = ModWinsCard.SCardEstablishContext(hContext, 0, 0, ref hContext);
			
			if (retcode !=  ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("SCardEstablishContext Error"); 
			}	
		}

		private void btnReader_Click(object sender, System.EventArgs e)
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

			btnConnect.Enabled = true;
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
			retcode = ModWinsCard.SCardConnect(hContext, pcReaders[0], ModWinsCard.SCARD_SHARE_EXCLUSIVE, 0 | 1, ref hCard, ref AProtocol);
			
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

			btnReset.Enabled = true;
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			ATRLen = 33;		

			//perform the Card Status
			retcode = ModWinsCard.SCardStatus(this.hCard, rreader, ref ReaderLen, ref dwState, ref Protocol, ref ATR[0], ref ATRLen);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				lstOutput.Items.Add("SCardState Failed!");
			} 
			else
			{
				lstOutput.Items.Add("SCardStatus OK...");

				// select for the protocol
				switch (Protocol)
				{
					case 0 : PRT = ModWinsCard.SCARD_PROTOCOL_UNDEFINED;
						lstOutput.Items.Add("Active Protocol Undefined");
						break;
					case 1 : PRT = ModWinsCard.SCARD_PROTOCOL_T0;
						lstOutput.Items.Add("Active Protocol T0");
						break;
					case 2 : PRT = ModWinsCard.SCARD_PROTOCOL_T1;
						lstOutput.Items.Add("Active Protocol T1");
						break;
				}

				Temp = "";

				for (index=0; index< ATRLen; index++)
					Temp = Temp + " " + string.Format("{0:X2}",ATR[index]);
            
				// Display ATR value 
				lstOutput.Items.Add("ATR:" + Temp);
				lstOutput.SelectedIndex = lstOutput.Items.Count - 1; 
       
				btnDisconnect.Enabled = true;

				textBox2.Text = " " + string.Format("{0:X2}", ATR[1]);
				textBox1.Text = " " + string.Format("{0:X2}", ATR[2]);

            btnUpdateATR.Enabled = true;
            cbo_rate.Enabled = true;
            cbo_nobytes.Enabled = true;

			}		
			
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			lstOutput.Items.Clear();
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
				
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			//terminate the application
			retcode = ModWinsCard.SCardReleaseContext(hContext);
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			Application.Exit();
		}

		private void cbo_rate_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch  (cbo_rate.SelectedIndex)
			{
				case 0 : textBox1.Text = "11";
					break;
				case 1 : textBox1.Text = "92";
					break;
				case 2 : textBox1.Text = "93";
					break;
				case 3 : textBox1.Text = "94";
					break;		
				case 4 : textBox1.Text = "95";
					break;
			}
		}

		private void btnUpdateATR_Click(object sender, System.EventArgs e)
		{
			string tmpStr; 
			int indx;	
			
			int ctr; 

			//Select FF07 file
			SelectFile(0xFF, 0x07);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 			
				return;
			}

			tmpStr = "";
			for (indx = 0; indx<1; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

			// Submit Issuer Code
			SubmitIC();
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}	

			//Updating the ATR Value.
			num_historical_byte = cbo_nobytes.SelectedIndex;
       
			tmpAPDU[0] =  Byte.Parse(textBox1.Text, System.Globalization.NumberStyles.HexNumber);

			if (num_historical_byte == 16) 
			{
				tmpAPDU[1] = 0xFF; //restoring to it original historical bytes
			}
			else
			{
				tmpAPDU[1] =  Convert.ToByte(num_historical_byte);
			}

			 ctr = 2;
			
		  	if (num_historical_byte < 16) 
			{
				for (indx = 0; indx< num_historical_byte; indx++)
             
					if ((ctrbyte[indx]) == null)  
					{
						ctrbyte[indx] = "00";
					}

				for (indx = 0; indx<(num_historical_byte); indx++)
				{
					tmpAPDU[ctr] = Byte.Parse(ctrbyte[indx], System.Globalization.NumberStyles.HexNumber);

					ctr = ctr + 1;
				}
			}


			for (indx = (ctr + 1); indx < 35; indx++)
				tmpAPDU[indx] = 0x00;

			writeRecord(0, 36, ref tmpAPDU);
		 
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

			SendRequest.dwProtocol = AProtocol;
			SendRequest.cbPciLength = 8;

			RecvRequest.dwProtocol = AProtocol;
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



		public void writeRecord(byte RecNo, byte DataLen, ref byte[] ApduIn) 
		{
			int indx; 
			string tmpStr; 

			// Write data to card
			apdu.Data = array;

			apdu.bCLA = 0x80;    //CLA
			apdu.bINS = 0xD2;    // INS
			apdu.bP1 = RecNo;    // Record No
			apdu.bP2 = 0x00;     // P2
			apdu.bP3 = DataLen;  //Length of Data

			apdu.IsSend = true;


			for (indx = 0; indx<(DataLen -1); indx++)
				apdu.Data[indx] = ApduIn[indx];

			tmpStr  = "";
			// do loop for sendbuffLen
			for (indx=0; indx< SendLen; indx++)
				tmpStr =tmpStr+ " " + string.Format("{0:X2}", SendBuff[indx]);

			PerformTransmitAPDU(ref apdu);
				
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{ 
				return;
			}

			tmpStr  = "";
			// do loop for recbuffLen
			for (indx=0; indx< SendLen; indx++)
				tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
		}


		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			   switch (textBox1.Text.Trim())
			   {	
					case "11" : cbo_rate.SelectedIndex = 0;
					   break;
					case "92" : cbo_rate.SelectedIndex = 1;
					   break;
					case "93" : cbo_rate.SelectedIndex = 2;
					   break;
					case "94" : cbo_rate.SelectedIndex = 3;
					   break;
					case "95" : cbo_rate.SelectedIndex = 4;
					   break;
			}
		}

		private void cbo_nobytes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch (cbo_nobytes.SelectedIndex)
			{ 
				case 0 : textBox2.Text = "B0";
					break;
				case 1 : textBox2.Text = "B1";
					break;
				case 2 : textBox2.Text = "B2";
					break;
				case 3 : textBox2.Text = "B3";
					break;
				case 4 : textBox2.Text = "B4";
					break;
				case 5 : textBox2.Text = "B5";
					break;
				case 6 : textBox2.Text = "B6";
					break;
				case 7 : textBox2.Text = "B7";
					break;
				case 8 : textBox2.Text = "B8";
					break;
				case 9 : textBox2.Text = "B9";
					break;
				case 10 : textBox2.Text = "BA";
					break;
				case 11 : textBox2.Text = "BB";
					break;
				case 12 : textBox2.Text = "BC";
					break;
				case 13 : textBox2.Text = "BD";
					break;
				case 14 : textBox2.Text = "BE";
					break;
				case 15 : textBox2.Text = "BF";
					break;
				case 16 : textBox2.Text = "BE";
					break;
			}
		}

		private void textBox2_TextChanged(object sender, System.EventArgs e)
		{
			switch (textBox2.Text.Trim())
			{
				case "B0" : cbo_nobytes.SelectedIndex = 0;
					break;
				case "B1" : cbo_nobytes.SelectedIndex = 1;
					break;
				case "B2" : cbo_nobytes.SelectedIndex = 2;
					break;
				case "B3" : cbo_nobytes.SelectedIndex = 3;
					break;
				case "B4" : cbo_nobytes.SelectedIndex = 4;
					break;
				case "B5" : cbo_nobytes.SelectedIndex = 5;
					break;
				case "B6" : cbo_nobytes.SelectedIndex = 6;
					break;
				case "B7" : cbo_nobytes.SelectedIndex = 7;
					break;
				case "B8" : cbo_nobytes.SelectedIndex = 8;
					break;
				case "B9" : cbo_nobytes.SelectedIndex = 9;
					break;
				case "BA" : cbo_nobytes.SelectedIndex = 10;
					break;
				case "BB" : cbo_nobytes.SelectedIndex = 11;
					break;
				case "BC" : cbo_nobytes.SelectedIndex = 12;
					break;
				case "BD" : cbo_nobytes.SelectedIndex = 13;
					break;
				case "BF" : cbo_nobytes.SelectedIndex = 15;
					break;
				default:

					 if (cbo_nobytes.SelectedIndex == 16) 
					{
						cbo_nobytes.SelectedIndex = 16;
					}
					else
					{
						cbo_nobytes.SelectedIndex = 14;
						
					}
               
					t1.Text = "";
					t2.Text = "";
					t3.Text = "";
					t4.Text = "";
					t5.Text = "";
					t6.Text = "";
					t7.Text = "";
					t8.Text = "";
					t9.Text = "";
					t10.Text = "";
					t11.Text = "";
					t12.Text = "";
					t13.Text = "";
					t14.Text = "";
					t15.Text = "";
				break;
			}



			if (cbo_nobytes.SelectedIndex == 0){
				t1.Enabled = false;
				t2.Enabled = false;
				t3.Enabled = false;
				t4.Enabled = false;
				t5.Enabled = false;
				t6.Enabled = false;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}
			
			else if (cbo_nobytes.SelectedIndex == 1) 
			{
				t1.Enabled = true;
				t2.Enabled = false;
				t3.Enabled = false;
				t4.Enabled = false;
				t5.Enabled = false;
				t6.Enabled = false;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 2) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = false;
				t4.Enabled = false;
				t5.Enabled = false;
				t6.Enabled = false;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 3) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = false;
				t5.Enabled = false;
				t6.Enabled = false;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 4)
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = false;
				t6.Enabled = false;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 5) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = false;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 6) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = false;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 7) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = false;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 8) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = false;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 9) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = true;
				t10.Enabled = false;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}


			else if (cbo_nobytes.SelectedIndex == 10) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = true;
				t10.Enabled = true;
				t11.Enabled = false;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}
			else if (cbo_nobytes.SelectedIndex == 11) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = true;
				t10.Enabled = true;
				t11.Enabled = true;
				t12.Enabled = false;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}
			else if (cbo_nobytes.SelectedIndex == 12) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = true;
				t10.Enabled = true;
				t11.Enabled = true;
				t12.Enabled = true;
				t13.Enabled = false;
				t14.Enabled = false;
				t15.Enabled = false;
			}

			else if (cbo_nobytes.SelectedIndex == 13) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = true;
				t10.Enabled = true;
				t11.Enabled = true;
				t12.Enabled = true;
				t13.Enabled = true;
				t14.Enabled = false;
				t15.Enabled = false;
			}
			else if (cbo_nobytes.SelectedIndex == 14) 
			{
				t1.Enabled = true;
				t2.Enabled = true;
				t3.Enabled = true;
				t4.Enabled = true;
				t5.Enabled = true;
				t6.Enabled = true;
				t7.Enabled = true;
				t8.Enabled = true;
				t9.Enabled = true;
				t10.Enabled = true;
				t11.Enabled = true;
				t12.Enabled = true;
				t13.Enabled = true;
				t14.Enabled = true;
				t15.Enabled = false;
			}

			else 
				 {
					 t1.Enabled = true;
					 t2.Enabled = true;
					 t3.Enabled = true;
					 t4.Enabled = true;
					 t5.Enabled = true;
					 t6.Enabled = true;
					 t7.Enabled = true;
					 t8.Enabled = true;
					 t9.Enabled = true;
					 t10.Enabled = true;
					 t11.Enabled = true;
					 t12.Enabled = true;
					 t13.Enabled = true;
					 t14.Enabled = true;
					 t15.Enabled = true;
				 }

		}

		private void t1_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[0] = t1.Text;
		}

		private void t2_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[1] = t2.Text;
		}

		private void t3_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[2] = t3.Text;
		}

		private void t4_TextChanged(object sender, System.EventArgs e)
		{
			 ctrbyte[3] = t4.Text;
		}

		private void t5_TextChanged(object sender, System.EventArgs e)
		{
			 ctrbyte[4] = t5.Text;
		}

		private void t6_TextChanged(object sender, System.EventArgs e)
		{
			 ctrbyte[5] = t6.Text;
		}

		private void t7_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[6] = t7.Text;
		}

		private void t8_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[7] = t8.Text;
		}

		private void t9_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[8] = t9.Text;
		}

		private void t10_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[9] = t10.Text;
		}

		private void t11_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[10] = t11.Text;
		}

		private void t12_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[11] = t12.Text;
		}

		private void t13_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[12] = t13.Text;
		}

		private void t14_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[13] = t14.Text;
		}

		private void t15_TextChanged(object sender, System.EventArgs e)
		{
			ctrbyte[14] = t15.Text;
		}	
	}
}
/*************************************************************************************
' DESCRIPTION :  This sample program demonstrate displaying and configue the ATR value 
'                using PC/SC.  
'*************************************************************************************/