/*============================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  ModWinscard.cs
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 11, 2005
'
'   Revision: (Date /Author/Description)
'             (06/23/2008/ Daryl M. Rojas / Added File Access Flag Bit to FF 04)
'                                                 
'
'=============================================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
//using System.Text.Encoding;

namespace ACOSReadWrite
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

		// global declaration
		int hContext;		//card reader context handle
		int hCard;			//card connection handle
		int ActiveProtocol, retcode;  
		byte[] rdrlist = new byte [100];
		string rreader;
		int Aprotocol, i;
		byte[] array = new byte[256]; 
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		byte[] tmpArray =  new byte[56];
		string tmpStr; 
		ModWinsCard.APDURec apdu  = new ModWinsCard.APDURec();			
		ModWinsCard.SCARD_IO_REQUEST sIO = new ModWinsCard.SCARD_IO_REQUEST(); 
		int indx, SendBuffLen, RecvBuffLen;
		string sTemp;
		byte HiAddr, LoAddr, dataLen; 
		public string s1;

		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox txtData;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.Button btnWrite;
		internal System.Windows.Forms.Button btnRead;
		internal System.Windows.Forms.GroupBox GBUserFile;
		internal System.Windows.Forms.RadioButton RBBB22;
		internal System.Windows.Forms.RadioButton RBAA11;
		internal System.Windows.Forms.RadioButton RBCC33;
		internal System.Windows.Forms.Button btnExit;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Button btnClear;
		internal System.Windows.Forms.Button btnConnect;
		internal System.Windows.Forms.Button btnDisconnect;
		internal System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button btnReaderPort;
		private System.Windows.Forms.Button btnFormat;
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
            this.Label3 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.GBUserFile = new System.Windows.Forms.GroupBox();
            this.RBBB22 = new System.Windows.Forms.RadioButton();
            this.RBAA11 = new System.Windows.Forms.RadioButton();
            this.RBCC33 = new System.Windows.Forms.RadioButton();
            this.btnFormat = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnReaderPort = new System.Windows.Forms.Button();
            this.GroupBox2.SuspendLayout();
            this.GBUserFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(152, 120);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(144, 23);
            this.Label3.TabIndex = 32;
            this.Label3.Text = "String Value in Data";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(152, 144);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(232, 20);
            this.txtData.TabIndex = 31;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnWrite);
            this.GroupBox2.Controls.Add(this.btnRead);
            this.GroupBox2.Enabled = false;
            this.GroupBox2.Location = new System.Drawing.Point(248, 8);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(136, 96);
            this.GroupBox2.TabIndex = 30;
            this.GroupBox2.TabStop = false;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(16, 56);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(104, 24);
            this.btnWrite.TabIndex = 1;
            this.btnWrite.Text = "Write";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(16, 24);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(104, 24);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "Read";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // GBUserFile
            // 
            this.GBUserFile.Controls.Add(this.RBBB22);
            this.GBUserFile.Controls.Add(this.RBAA11);
            this.GBUserFile.Controls.Add(this.RBCC33);
            this.GBUserFile.Enabled = false;
            this.GBUserFile.Location = new System.Drawing.Point(152, 8);
            this.GBUserFile.Name = "GBUserFile";
            this.GBUserFile.Size = new System.Drawing.Size(88, 96);
            this.GBUserFile.TabIndex = 29;
            this.GBUserFile.TabStop = false;
            this.GBUserFile.Text = "User File";
            // 
            // RBBB22
            // 
            this.RBBB22.Location = new System.Drawing.Point(16, 40);
            this.RBBB22.Name = "RBBB22";
            this.RBBB22.Size = new System.Drawing.Size(64, 24);
            this.RBBB22.TabIndex = 1;
            this.RBBB22.Text = "BB 22";
            this.RBBB22.Click += new System.EventHandler(this.RBBB22_Click);
            // 
            // RBAA11
            // 
            this.RBAA11.Location = new System.Drawing.Point(16, 16);
            this.RBAA11.Name = "RBAA11";
            this.RBAA11.Size = new System.Drawing.Size(64, 24);
            this.RBAA11.TabIndex = 0;
            this.RBAA11.Text = "AA 11";
            this.RBAA11.Click += new System.EventHandler(this.RBAA11_Click);
            // 
            // RBCC33
            // 
            this.RBCC33.Location = new System.Drawing.Point(16, 64);
            this.RBCC33.Name = "RBCC33";
            this.RBCC33.Size = new System.Drawing.Size(64, 24);
            this.RBCC33.TabIndex = 19;
            this.RBCC33.Text = "CC 33";
            this.RBCC33.Click += new System.EventHandler(this.RBCC33_Click);
            // 
            // btnFormat
            // 
            this.btnFormat.Enabled = false;
            this.btnFormat.Location = new System.Drawing.Point(16, 80);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(112, 24);
            this.btnFormat.TabIndex = 28;
            this.btnFormat.Text = "Format Card";
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(280, 352);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 24);
            this.btnExit.TabIndex = 27;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(24, 160);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(92, 16);
            this.Label2.TabIndex = 26;
            this.Label2.Text = "Result";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(160, 352);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 24);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(16, 48);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 24);
            this.btnConnect.TabIndex = 23;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(40, 352);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(112, 24);
            this.btnDisconnect.TabIndex = 24;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(24, 184);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(384, 160);
            this.lstOutput.TabIndex = 22;
            // 
            // btnReaderPort
            // 
            this.btnReaderPort.Location = new System.Drawing.Point(16, 16);
            this.btnReaderPort.Name = "btnReaderPort";
            this.btnReaderPort.Size = new System.Drawing.Size(112, 24);
            this.btnReaderPort.TabIndex = 33;
            this.btnReaderPort.Text = "Reader Name";
            this.btnReaderPort.Click += new System.EventHandler(this.btnReaderPort_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(416, 381);
            this.Controls.Add(this.btnReaderPort);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GBUserFile);
            this.Controls.Add(this.btnFormat);
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
            this.Text = "ACOSReadWrite";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GBUserFile.ResumeLayout(false);
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

			btnFormat.Enabled = false;
			GBUserFile.Enabled = false;
			GroupBox2.Enabled = false;
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

		private void btnFormat_Click(object sender, System.EventArgs e)
		{
			
			//Send IC Code
			lstOutput.Items.Add("Submit Code");
			SubmitIC();

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}   

			//Select FF 02
			lstOutput.Items.Add("Select FF 02");
			SelectFile(0xFF, 0x02);
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}   

			/* Write to FF 02
			This will create 3 User files, no Option registers and
			Security Option registers defined, Personalization bit is not set */ 
			tmpArray[0] = 0x00;      // 00    Option registers
			tmpArray[1] = 0x00;      // 00    Security option register
			tmpArray[2] = 0x03;      // 03    No of user files
			tmpArray[3] = 0x00;      // 00    Personalization bit

			writeRecord(0x00, 0x00, 0x04, 0x04, ref tmpArray);

			//Select FF 04
			lstOutput.Items.Add("Select FF 04");
			SelectFile(0xFF, 0x04);

			// Send IC Code
			lstOutput.Items.Add("Submit Code");
			SubmitIC();
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}  

			/* Write to FF 04
			 Write to first record of FF 04 */
			tmpArray[0] = 0x0A;       // 10    Record length
			tmpArray[1] = 0x03;       // 3     No of records
			tmpArray[2] = 0x00;       // 00    Read security attribute
			tmpArray[3] = 0x00;       // 00    Write security attribute
			tmpArray[4] = 0xAA;       // AA    File identifier
			tmpArray[5] = 0x11;       // 11    File identifier
            tmpArray[6] = 0x00;       // 00    File Access Flag Bit


			writeRecord(0x00, 0x00, 0x06, 0x06, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			//Write to second record of FF 04
			tmpArray[0] = 0x10;			// 16    Record length
			tmpArray[1] = 0x02;			// 2     No of records
			tmpArray[2] = 0x00;			// 00    Read security attribute
			tmpArray[3] = 0x00;			// 00    Write security attribute
			tmpArray[4] = 0xBB;			// BB    File identifier
			tmpArray[5] = 0x22;			// 22    File identifier
            tmpArray[6] = 0x00;         // 00    File Access Flag Bit

			writeRecord(0x00, 0x01, 0x06, 0x06, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			lstOutput.Items.Add("User File BB 22 is defined");

			//Write to third record of FF 04
			tmpArray[0] = 0x20;       // 32    Record length
			tmpArray[1] = 0x04;       // 4     No of records
			tmpArray[2] = 0x00;       // 00    Read security attribute
			tmpArray[3] = 0x00;       // 00    Write security attribute
			tmpArray[4] = 0xCC;       // CC    File identifier
			tmpArray[5] = 0x33;       // 33    File identifier
            tmpArray[6] = 0x00;       // 00    File Access Flag Bit
			
			writeRecord(0x00, 0x02, 0x06, 0x06, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			lstOutput.Items.Add("User File CC 33 is defined");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

			GBUserFile.Enabled = true;
			GroupBox2.Enabled = true;
		
		}

		private void SubmitIC() 
		{
			
			//Send IC Code
			apdu.Data = array;

			apdu.bCLA = 0x80;          // CLA
			apdu.bINS = 0x20;          // INS
			apdu.bP1 = 0x07;           // P1
			apdu.bP2 = 0x00;           // P2
			apdu.bP3 = 0x08;           // P3

			apdu.Data[0] = 0x41;       // A
			apdu.Data[1] = 0x43;       // C
			apdu.Data[2] = 0x4F;       // O
			apdu.Data[3] = 0x53;       // S
			apdu.Data[4] = 0x54;       // T
			apdu.Data[5] = 0x45;       // E
			apdu.Data[6] = 0x53;       // S
			apdu.Data[7] = 0x54;       // T

			apdu.IsSend = true;
			lstOutput.Items.Add("Submit IC");

			PerformTransmitAPDU(ref apdu);
		}


		private void SelectFile(byte HiAddr, byte LoAddr) 
		{

			// Select FF 02
			apdu.Data = array;

			apdu.bCLA = 0x080;       // CLA
			apdu.bINS = 0x0A4;       // INS
			apdu.bP1 = 0x00;         // P1
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = 0x02;         // P3

			apdu.Data[0] = HiAddr;      // Value of High Byte
			apdu.Data[1] = LoAddr;      // Value of Low Byte

			apdu.IsSend = true;
			
			PerformTransmitAPDU(ref apdu);
		}

		private void  writeRecord(int caseType, byte RecNo, byte maxLen, byte DataLen, ref byte[] ApduIn)
		{																										
			if (caseType == 1)    // If card data is to be erased before writing new data. Re-initialize card values to $00
			{
				apdu.bCLA = 0x80;        // CLA
				apdu.bINS = 0xD2;        // INS
				apdu.bP1 = RecNo;		 // Record No
				apdu.bP2 = 0x00;         // P2
				apdu.bP3 = maxLen;        // Length of Data

				apdu.IsSend = true;

				for(i=0; i< maxLen; i++) 
					apdu.Data[i] = ApduIn[i];
           
				PerformTransmitAPDU(ref apdu);
			}

			//Write data to card
			apdu.bCLA = 0x80;       // CLA
			apdu.bINS = 0xD2;       // INS
			apdu.bP1 = RecNo;       // Record No
			apdu.bP2 = 0x00;        // P2
			apdu.bP3 = DataLen;     // Length of Data

			apdu.IsSend = true;

			for(i=0; i< maxLen; i++) 
				apdu.Data[i] = ApduIn[i];
        
			lstOutput.Items.Add("Write to FF 02");
			PerformTransmitAPDU(ref apdu);

		}

		private void RBAA11_Click(object sender, System.EventArgs e)
		{
			txtData.Text = "";
			txtData.MaxLength = 10;
		}

		private void RBBB22_Click(object sender, System.EventArgs e)
		{
			txtData.Text = "";
			txtData.MaxLength = 16;

		}

		private void RBCC33_Click(object sender, System.EventArgs e)
		{
			txtData.Text = "";
			txtData.MaxLength = 32;
		}

		private void btnRead_Click(object sender, System.EventArgs e)
		{
			int indx; 
			string ChkStr;

			// Check User File selected by user
			if (RBAA11.Checked == true) 
			{
				HiAddr = 0xAA;
				LoAddr = 0x11;
				dataLen = 0x0A;
				//ChkStr = "91 00";
			}

			if (RBBB22.Checked == true) 
			{
				HiAddr = 0xBB;
				LoAddr = 0x22;
				dataLen = 0x10;
				//ChkStr = "91 01";
			}

			if (RBCC33.Checked == true)
			{
				HiAddr = 0xCC;
				LoAddr = 0x33;
				dataLen = 0x20;
				//ChkStr = "91 02";
			}

			// Select User File
			SelectFile(HiAddr, LoAddr);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			
			// Read First Record of User File selected
			readRecord(0x00, dataLen);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			
			// Display data read from card to textbox
			tmpStr = "";
			indx = 0;

			while (RecvBuff[indx] != 0x00)
			{
				if (indx < txtData.MaxLength) 
				{
					tmpStr = tmpStr + Chr(RecvBuff[indx]);
				}
				indx = indx + 1;
			}
			txtData.Text = tmpStr;
			
			lstOutput.Items.Add("Data read from card is displayed");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;
		}

		private void readRecord(byte RecNo, byte dataLen)
		{
			apdu.Data = array;
		
			// Read data from card
			apdu.bCLA = 0x80;        // CLA
			apdu.bINS =0xB2;         // INS
			apdu.bP1 = RecNo;        // Record No
			apdu.bP2 = 0x00;         // P2
			apdu.bP3 = dataLen;      // Length of Data

			apdu.IsSend = false;

			PerformTransmitAPDU(ref apdu);

		}
		Char Chr(int i)
		{
			//Return the character of the given character value
			return Convert.ToChar(i);
		}

		private void btnWrite_Click(object sender, System.EventArgs e)
		{
			
			// Validate input template
			if (txtData.Text == "") 
			{
				txtData.Focus();
			}

			//Check User File selected by user
			if (RBAA11.Checked == true) 
			{
				HiAddr = 0xAA;
				LoAddr = 0x11;
				dataLen = 0x0A;
				//ChkStr = "91 00";
			}

			if (RBBB22.Checked == true)
			{
				HiAddr = 0xBB;
				LoAddr = 0x22;
				dataLen = 0x10;
				//ChkStr = "91 01";
			}

			if (RBCC33.Checked == true) 
			{	
				HiAddr = 0xCC;
				LoAddr = 0x33;
				dataLen = 0x20;
				//ChkStr = "91 02";
			}

			// Select User File
			SelectFile(HiAddr, LoAddr);
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}
			
			tmpStr = ""; 
			tmpStr = txtData.Text;
			
			for(indx=0; indx < tmpStr.Length; indx++)
				tmpArray[indx] = (byte)Asc(tmpStr.Substring(indx,1));
			 		
			writeRecord(0x01, 0x00, dataLen, dataLen, ref tmpArray);			

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}

			lstOutput.Items.Add("Data read from Text Box is written to card.");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;  
		}
	
		public static string Mid(string tmpStr, int start)
		{
			return tmpStr.Substring(start, tmpStr.Length - start);
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
        			
	}
}

/*=================================================================================
 *  Description :  This sample program demonstrates on how to format  ACOS card 
 *                 and how to read/write data into it using the PC/SC platform    
 * ================================================================================*/