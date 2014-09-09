using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ACOSMutualAuthentication
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

		int retcode, hContext, hCard, Aprotocol, ActiveProtocol; 
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
		byte[] tmpResult = new byte[8];
		int  tmpindx,RecvBuffLen, SendBuffLen; 
		byte indx; 

		private System.Windows.Forms.Button btnReaderPort;
		internal System.Windows.Forms.GroupBox GBKeyTemp;
		internal System.Windows.Forms.Button btnFormat;
		internal System.Windows.Forms.TextBox txtTerminalKey;
		internal System.Windows.Forms.TextBox txtCardKey;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.GroupBox GBSucureOpt;
		internal System.Windows.Forms.RadioButton RB3DES;
		internal System.Windows.Forms.RadioButton RBDES;
		internal System.Windows.Forms.Button btnExit;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Button btnClear;
		internal System.Windows.Forms.Button btnConnect;
		internal System.Windows.Forms.Button btnDisconnect;
		internal System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button btnExecute;
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
            this.GBKeyTemp = new System.Windows.Forms.GroupBox();
            this.btnFormat = new System.Windows.Forms.Button();
            this.txtTerminalKey = new System.Windows.Forms.TextBox();
            this.txtCardKey = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.GBSucureOpt = new System.Windows.Forms.GroupBox();
            this.RB3DES = new System.Windows.Forms.RadioButton();
            this.RBDES = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.GBKeyTemp.SuspendLayout();
            this.GBSucureOpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReaderPort
            // 
            this.btnReaderPort.Location = new System.Drawing.Point(16, 32);
            this.btnReaderPort.Name = "btnReaderPort";
            this.btnReaderPort.Size = new System.Drawing.Size(104, 24);
            this.btnReaderPort.TabIndex = 45;
            this.btnReaderPort.Text = "Reader Name";
            this.btnReaderPort.Click += new System.EventHandler(this.btnReaderPort_Click);
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
            this.GBKeyTemp.Location = new System.Drawing.Point(8, 120);
            this.GBKeyTemp.Name = "GBKeyTemp";
            this.GBKeyTemp.Size = new System.Drawing.Size(232, 112);
            this.GBKeyTemp.TabIndex = 39;
            this.GBKeyTemp.TabStop = false;
            this.GBKeyTemp.Text = "Key Template";
            // 
            // btnFormat
            // 
            this.btnFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormat.Location = new System.Drawing.Point(120, 80);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(104, 23);
            this.btnFormat.TabIndex = 13;
            this.btnFormat.Text = "Format";
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // txtTerminalKey
            // 
            this.txtTerminalKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminalKey.Location = new System.Drawing.Point(88, 48);
            this.txtTerminalKey.MaxLength = 8;
            this.txtTerminalKey.Name = "txtTerminalKey";
            this.txtTerminalKey.Size = new System.Drawing.Size(136, 20);
            this.txtTerminalKey.TabIndex = 12;
            // 
            // txtCardKey
            // 
            this.txtCardKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardKey.Location = new System.Drawing.Point(88, 24);
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
            this.Label4.Size = new System.Drawing.Size(72, 16);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Terminal Key";
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(8, 24);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(56, 23);
            this.Label3.TabIndex = 9;
            this.Label3.Text = "Card Key";
            // 
            // GBSucureOpt
            // 
            this.GBSucureOpt.Controls.Add(this.RB3DES);
            this.GBSucureOpt.Controls.Add(this.RBDES);
            this.GBSucureOpt.Enabled = false;
            this.GBSucureOpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBSucureOpt.Location = new System.Drawing.Point(136, 24);
            this.GBSucureOpt.Name = "GBSucureOpt";
            this.GBSucureOpt.Size = new System.Drawing.Size(104, 80);
            this.GBSucureOpt.TabIndex = 38;
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
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(480, 272);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(104, 24);
            this.btnExit.TabIndex = 43;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(256, 8);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(88, 16);
            this.Label2.TabIndex = 44;
            this.Label2.Text = "Result";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(368, 272);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 24);
            this.btnClear.TabIndex = 42;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(16, 64);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 24);
            this.btnConnect.TabIndex = 37;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(256, 272);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(104, 24);
            this.btnDisconnect.TabIndex = 41;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(256, 32);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(328, 225);
            this.lstOutput.TabIndex = 40;
            // 
            // btnExecute
            // 
            this.btnExecute.Enabled = false;
            this.btnExecute.Location = new System.Drawing.Point(128, 248);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(104, 23);
            this.btnExecute.TabIndex = 46;
            this.btnExecute.Text = "Execute MA";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 301);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnReaderPort);
            this.Controls.Add(this.GBKeyTemp);
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
            this.Text = "ACOSMutualAuthentication";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GBKeyTemp.ResumeLayout(false);
            this.GBKeyTemp.PerformLayout();
            this.GBSucureOpt.ResumeLayout(false);
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
				lstOutput.Items.Add ("Connection Error"); 				
			}
			else
			{
				lstOutput.Items.Add ("Connection OK");
				rreader = pcReaders[0];
			}	
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
			GBKeyTemp.Enabled = true;
			GBSucureOpt.Enabled = true;
			btnExecute.Enabled = true;
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

			GBKeyTemp.Enabled = false;
			GBSucureOpt.Enabled = false;
			btnExecute.Enabled = false;
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


		private void btnFormat_Click(object sender, System.EventArgs e)
		{
			// Validate data template
			if ( ! ValidTemplate())
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
			{
				tmpArray[0] = 0x00;		  // 00h  3-DES is not set	
			}
			else
			{
				tmpArray[0] = 0x02;         // 02h  3-DES is enabled
			}	
			tmpArray[1] = 0x00;             // 00    Security option register
			tmpArray[2] = 0x03;             // 00    No of user files
			tmpArray[3] = 0x00;             // 00    Personalization bit

			writeRecord(0x00, 0x00, 0x04, 0x04,ref tmpArray);

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
			else
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

		private void ClearTextFields()
		{
			txtCardKey.Text = "";
			txtTerminalKey.Text = "";
		}
		
		private bool ValidTemplate() 
		{
			if ((txtCardKey.Text.Length) < (txtCardKey.MaxLength))
			{
				txtCardKey.Focus();
				return false;
				
			}			
			if ((txtTerminalKey.Text.Length) < (txtTerminalKey.MaxLength)) 
			{
				txtTerminalKey.Focus();
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

		private void btnExecute_Click(object sender, System.EventArgs e)
		{
			//Validate data template
			if (! ValidTemplate())
			{
				return;
			}
			//Check if card inserted is an ACOS card
			if (! CheckACOS()) 
			{
			  lstOutput.Items.Add("Please insert an ACOS card");
			}
			lstOutput.Items.Add("ACOS card is detected.");

			// Card-side authentication process
			// Generate random number from card
            StartSession();
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			// Retrieve Terminal Key from Input Template
			tmpStr = " ";
			tmpStr = txtTerminalKey.Text;
			for (indx=0; indx< txtTerminalKey.MaxLength; indx++)
				tKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));

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
			{	
				return;
			}

			// Get 8-byte result of card-side authentication
			// and save to tmpResult
            GetResponse();

			for (indx=0; indx<8; indx++)
               tmpResult[indx] = RecvBuff[indx];
       
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{	
				return;
			}
			/*  Terminal-side authentication process
            '  Retrieve Card Key from Input Template */
			tmpStr = "";
			tmpStr = txtCardKey.Text;
			for (indx=0; indx< txtCardKey.MaxLength; indx++)
				cKey[indx] = (byte)Asc(tmpStr.Substring(indx,1));
			
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
           
				for (indx=0;indx<8;indx++)
					ReverseKey[8 + indx] = tKey[indx];

				// compute tmpArray = 3DES (TRnd, ReverseKey)
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = TRnd[indx];

				TripleDES(ref tmpArray[0], ref ReverseKey[0]);

				// tmpArray holds the right half of SessionKey
				for (indx=0; indx<8; indx++)
					SessionKey[indx + 8] = tmpArray[indx];	
			}

			// compute DES (TRnd, SessionKey)
			for (indx=0; indx<8;indx++)
				tmpArray[indx] = TRnd[indx];

			if (RBDES.Checked == true) 
			{ 
				DES(ref tmpArray[0],ref SessionKey[0]);
			}
			else //: RBSDES.Checked = True
			{   
				TripleDES(ref tmpArray[0], ref SessionKey[0]);
			}

			// Compare Card-side and Terminal-side authentication results
			for (indx=0; indx<8;indx++)
				if (tmpResult[indx] != tmpArray[indx]) 
				{
					lstOutput.Items.Add("Mutual Authentication failed.");
					lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
				}
			lstOutput.Items.Add("Mutual Authentication is successful.");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
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

		private void  GetResponse() 
		{
			apdu.Data = array;
									
			apdu.bCLA = 0x80;        // CLA
			apdu.bINS = 0xC0;        // INS
			apdu.bP1 = 0x00;          // P1
			apdu.bP2 = 0x00;          // P2
			apdu.bP3 = 0x08;          // Length of Data

			apdu.IsSend = false;

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

		private void RB3DES_CheckedChanged(object sender, System.EventArgs e)
		{
			ClearTextFields();
			txtCardKey.MaxLength = 16;
			txtTerminalKey.MaxLength = 16;
		}

		private void RBDES_CheckedChanged(object sender, System.EventArgs e)
		{
			ClearTextFields();
			txtCardKey.MaxLength = 8;
			txtTerminalKey.MaxLength = 8;
		}
	}
}
 /*=======================================================================
 ' Description : This sample program demonstrate on how to use  
 '               Mutual Authentication in ACOS card using PCSC platform.
 '
 '========================================================================*/
