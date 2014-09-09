/*============================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  ModWinscard.cs
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 11, 2005
'
'   Revision: (Date /Author/Description)
'             (06/23/2008/ Daryl M. Rojas / Added File Access Flaf Bit to FF 04) 
'                                                 
'
'=============================================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ACOSCreateFiles
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
		byte[] ATR = new byte[33];
		short index; 
		string Temp;
		int Aprotocol, i;
	    byte[] array = new byte[256]; 
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		byte[] tmpArray =  new byte[32];
		ModWinsCard.APDURec apdu  = new ModWinsCard.APDURec();			
		ModWinsCard.SCARD_IO_REQUEST sIO = new ModWinsCard.SCARD_IO_REQUEST(); 
		int indx, SendBuffLen, RecvBuffLen;
		string sTemp;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnDisconnect;
		private System.Windows.Forms.Button btnReader;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnCreate;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnReader = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstOutput);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(136, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 192);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // lstOutput
            // 
            this.lstOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(8, 16);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(360, 173);
            this.lstOutput.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 144);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 23);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(16, 176);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(104, 23);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(16, 112);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(104, 23);
            this.btnDisconnect.TabIndex = 17;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnReader
            // 
            this.btnReader.Location = new System.Drawing.Point(16, 16);
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(104, 23);
            this.btnReader.TabIndex = 16;
            this.btnReader.Text = "Reader Port";
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(16, 48);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 23);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(16, 80);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(104, 23);
            this.btnCreate.TabIndex = 21;
            this.btnCreate.Text = "Create Files ";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(520, 213);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnReader);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACOSCreateFiles";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
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
			btnCreate.Enabled = true;
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
			btnCreate.Enabled = false;
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

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			
           SubmitIC();
		   SelectFile(0xFF, 0x2);

			/*  Write to FF 02
			'    This will create 3 User files, no Option registers and
			'    Security Option registers defined, Personalization bit
			'    is not set */

			tmpArray[0] = 0x0;      // 00    Option registers
			tmpArray[1] = 0x0;      // 00    Security option register
			tmpArray[2] = 0x3;      // 03    No of user files
			tmpArray[3] = 0x0;      // 00    Personalization bit

			writeRecord(0x00, 0x00, 0x04, 0x04, ref tmpArray);

			lstOutput.Items.Add("FF 02 is updated");

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}
			else
			{
				lstOutput.Items.Add("Card is successful.");
			}

			// Select FF 04
			lstOutput.Items.Add("Select FF 04");

            SelectFile(0xFF, 0x04);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}

			// Send IC Code
			SubmitIC();
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}

			// Write to FF 04
			//  Write to first record of FF 04
			tmpArray[0] = 0x05;       // 5     Record length
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

			lstOutput.Items.Add("User File AA 11 is defined");

			//  Write to second record of FF 04
			tmpArray[0] = 0x0A;       // 10    Record length
			tmpArray[1] = 0x02;       // 2     No of records
			tmpArray[2] = 0x00;       // 00    Read security attribute
			tmpArray[3] = 0x00;       // 00    Write security attribute
			tmpArray[4] = 0xBB;       // BB    File identifier
			tmpArray[5] = 0x22;       // 22    File identifier
            tmpArray[6] = 0x00;       // 00    File Access Flag Bit

			writeRecord(0x00, 0x01, 0x06, 0x06, ref tmpArray);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}

			lstOutput.Items.Add("User File BB 22 is defined");

			//  Write to third record of FF 04
			tmpArray[0] = 0x06;       // 6     Record length
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

			//  Select 3 User Files created previously for validation
			// Select User File AA 11
			SelectFile(0xAA, 0x11);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}
        
			 lstOutput.Items.Add("User File AA 11 is selected");

			//  Select User File BB 22
			 SelectFile(0xBB, 0x22);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}

			lstOutput.Items.Add("User File BB 22 is selected");

			//  Select User File CC 33
			 SelectFile(0xCC, 0x33);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{   
				return;
			}

			lstOutput.Items.Add("User File CC 33 is selected");
			lstOutput.SelectedIndex = lstOutput.Items.Count - 1;

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
			
			lstOutput.Items.Add("Select FF 02");

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
	}
}
/*=======================================================================
' Description : This sample program demonstrate on how to Create Files  
'               in ACOS card using PCSC platform.
'
'======================================================================== */