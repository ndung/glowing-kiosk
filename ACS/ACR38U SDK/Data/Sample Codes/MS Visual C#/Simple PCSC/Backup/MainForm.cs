/*'=======================================================================================
'
'   Class Form:  SimplePCSC.vb
'
'   Company   : Advanced Card System LTD.
'
'   Author    : Aileen Grace Sarte
'
'   Date      : October 14, 2006
'
'   Revision  :
'                Name                   Date                    Brief Description
'=======================================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SimplePCSC
{
	/// <summary>
	/// Summary description for SimplePCSC.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		// variable declaration
		int Protocol, hContext, hCard, ReaderCount; 
		int retCode;
		//byte[] sReaderList = new byte [255];
		string sReaderList;
		string sReaderGroup;
		string tmpStr;
		public ModWinsCard.SCARD_IO_REQUEST ioRequest= new ModWinsCard.SCARD_IO_REQUEST();
		int SendLen, RecvLen;
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		int indx;
        
		const int INVALID_SW1SW2 = -450;

		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.RichTextBox mMsg;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.ComboBox cbReader;
		internal System.Windows.Forms.Button bClear;
		internal System.Windows.Forms.Button bQuit;
		internal System.Windows.Forms.Button bRelease;
		internal System.Windows.Forms.Button bDisconnect;
		internal System.Windows.Forms.Button bEnd;
		internal System.Windows.Forms.Button bTransmit;
		internal System.Windows.Forms.Button bStatus;
		internal System.Windows.Forms.Button bBeginTran;
		internal System.Windows.Forms.Button bConnect;
		internal System.Windows.Forms.Button bList;
		internal System.Windows.Forms.Button bInit;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.GroupBox fApdu;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox tDataIn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.mMsg = new System.Windows.Forms.RichTextBox();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.cbReader = new System.Windows.Forms.ComboBox();
			this.bClear = new System.Windows.Forms.Button();
			this.bQuit = new System.Windows.Forms.Button();
			this.bRelease = new System.Windows.Forms.Button();
			this.bDisconnect = new System.Windows.Forms.Button();
			this.bEnd = new System.Windows.Forms.Button();
			this.bTransmit = new System.Windows.Forms.Button();
			this.bStatus = new System.Windows.Forms.Button();
			this.bBeginTran = new System.Windows.Forms.Button();
			this.bConnect = new System.Windows.Forms.Button();
			this.bList = new System.Windows.Forms.Button();
			this.bInit = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.fApdu = new System.Windows.Forms.GroupBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.tDataIn = new System.Windows.Forms.TextBox();
			this.GroupBox1.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.fApdu.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox1
			// 
			this.GroupBox1.Controls.Add(this.mMsg);
			this.GroupBox1.Controls.Add(this.GroupBox2);
			this.GroupBox1.Controls.Add(this.bClear);
			this.GroupBox1.Controls.Add(this.bQuit);
			this.GroupBox1.Controls.Add(this.bRelease);
			this.GroupBox1.Controls.Add(this.bDisconnect);
			this.GroupBox1.Controls.Add(this.bEnd);
			this.GroupBox1.Controls.Add(this.bTransmit);
			this.GroupBox1.Controls.Add(this.bStatus);
			this.GroupBox1.Controls.Add(this.bBeginTran);
			this.GroupBox1.Controls.Add(this.bConnect);
			this.GroupBox1.Controls.Add(this.bList);
			this.GroupBox1.Controls.Add(this.bInit);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.fApdu);
			this.GroupBox1.Location = new System.Drawing.Point(6, 11);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(424, 631);
			this.GroupBox1.TabIndex = 1;
			this.GroupBox1.TabStop = false;
			// 
			// mMsg
			// 
			this.mMsg.Location = new System.Drawing.Point(17, 416);
			this.mMsg.Name = "mMsg";
			this.mMsg.Size = new System.Drawing.Size(383, 200);
			this.mMsg.TabIndex = 14;
			this.mMsg.Text = "";
			// 
			// GroupBox2
			// 
			this.GroupBox2.Controls.Add(this.cbReader);
			this.GroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupBox2.Location = new System.Drawing.Point(202, 64);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(200, 56);
			this.GroupBox2.TabIndex = 12;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Port";
			// 
			// cbReader
			// 
			this.cbReader.Location = new System.Drawing.Point(16, 19);
			this.cbReader.Name = "cbReader";
			this.cbReader.Size = new System.Drawing.Size(168, 23);
			this.cbReader.TabIndex = 3;
			// 
			// bClear
			// 
			this.bClear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bClear.Location = new System.Drawing.Point(208, 374);
			this.bClear.Name = "bClear";
			this.bClear.Size = new System.Drawing.Size(168, 32);
			this.bClear.TabIndex = 13;
			this.bClear.Text = "Clear Output Window";
			this.bClear.Click += new System.EventHandler(this.bClear_Click);
			// 
			// bQuit
			// 
			this.bQuit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bQuit.Location = new System.Drawing.Point(208, 334);
			this.bQuit.Name = "bQuit";
			this.bQuit.Size = new System.Drawing.Size(168, 32);
			this.bQuit.TabIndex = 12;
			this.bQuit.Text = "Quit";
			this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
			// 
			// bRelease
			// 
			this.bRelease.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bRelease.Location = new System.Drawing.Point(17, 373);
			this.bRelease.Name = "bRelease";
			this.bRelease.Size = new System.Drawing.Size(168, 32);
			this.bRelease.TabIndex = 11;
			this.bRelease.Text = "SCardReleaseContext";
			this.bRelease.Click += new System.EventHandler(this.bRelease_Click);
			// 
			// bDisconnect
			// 
			this.bDisconnect.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bDisconnect.Location = new System.Drawing.Point(17, 334);
			this.bDisconnect.Name = "bDisconnect";
			this.bDisconnect.Size = new System.Drawing.Size(168, 32);
			this.bDisconnect.TabIndex = 10;
			this.bDisconnect.Text = "SCardDisconnect";
			this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
			// 
			// bEnd
			// 
			this.bEnd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bEnd.Location = new System.Drawing.Point(17, 295);
			this.bEnd.Name = "bEnd";
			this.bEnd.Size = new System.Drawing.Size(168, 32);
			this.bEnd.TabIndex = 9;
			this.bEnd.Text = "SCardEndTransaction";
			this.bEnd.Click += new System.EventHandler(this.bEnd_Click);
			// 
			// bTransmit
			// 
			this.bTransmit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bTransmit.Location = new System.Drawing.Point(16, 256);
			this.bTransmit.Name = "bTransmit";
			this.bTransmit.Size = new System.Drawing.Size(168, 32);
			this.bTransmit.TabIndex = 7;
			this.bTransmit.Text = "SCardTransmit";
			this.bTransmit.Click += new System.EventHandler(this.bTransmit_Click);
			// 
			// bStatus
			// 
			this.bStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bStatus.Location = new System.Drawing.Point(16, 217);
			this.bStatus.Name = "bStatus";
			this.bStatus.Size = new System.Drawing.Size(168, 32);
			this.bStatus.TabIndex = 6;
			this.bStatus.Text = "SCardStatus";
			this.bStatus.Click += new System.EventHandler(this.bStatus_Click);
			// 
			// bBeginTran
			// 
			this.bBeginTran.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bBeginTran.Location = new System.Drawing.Point(16, 178);
			this.bBeginTran.Name = "bBeginTran";
			this.bBeginTran.Size = new System.Drawing.Size(168, 32);
			this.bBeginTran.TabIndex = 5;
			this.bBeginTran.Text = "SCardBeginTransaction";
			this.bBeginTran.Click += new System.EventHandler(this.bBeginTran_Click);
			// 
			// bConnect
			// 
			this.bConnect.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bConnect.Location = new System.Drawing.Point(16, 139);
			this.bConnect.Name = "bConnect";
			this.bConnect.Size = new System.Drawing.Size(168, 32);
			this.bConnect.TabIndex = 4;
			this.bConnect.Text = "SCardConnect";
			this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
			// 
			// bList
			// 
			this.bList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bList.Location = new System.Drawing.Point(16, 99);
			this.bList.Name = "bList";
			this.bList.Size = new System.Drawing.Size(168, 32);
			this.bList.TabIndex = 2;
			this.bList.Text = "SCardListReaders";
			this.bList.Click += new System.EventHandler(this.bList_Click);
			// 
			// bInit
			// 
			this.bInit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bInit.Location = new System.Drawing.Point(16, 60);
			this.bInit.Name = "bInit";
			this.bInit.Size = new System.Drawing.Size(168, 32);
			this.bInit.TabIndex = 1;
			this.bInit.Text = "SCardEstablishContext";
			this.bInit.Click += new System.EventHandler(this.bInit_Click);
			// 
			// Label1
			// 
			this.Label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.Location = new System.Drawing.Point(15, 23);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(264, 24);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "This is simple PCSC Application";
			// 
			// fApdu
			// 
			this.fApdu.Controls.Add(this.Label2);
			this.fApdu.Controls.Add(this.tDataIn);
			this.fApdu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fApdu.Location = new System.Drawing.Point(202, 237);
			this.fApdu.Name = "fApdu";
			this.fApdu.Size = new System.Drawing.Size(200, 67);
			this.fApdu.TabIndex = 13;
			this.fApdu.TabStop = false;
			this.fApdu.Text = "APDU Input";
			// 
			// Label2
			// 
			this.Label2.Location = new System.Drawing.Point(17, 45);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(136, 16);
			this.Label2.TabIndex = 1;
			this.Label2.Text = "(Use HEX values only)";
			// 
			// tDataIn
			// 
			this.tDataIn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tDataIn.Location = new System.Drawing.Point(17, 24);
			this.tDataIn.MaxLength = 0;
			this.tDataIn.Name = "tDataIn";
			this.tDataIn.Size = new System.Drawing.Size(160, 21);
			this.tDataIn.TabIndex = 8;
			this.tDataIn.Text = "";
			this.tDataIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tDataIn_KeyPress);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(437, 652);
			this.Controls.Add(this.GroupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Simple PCSC";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			this.fApdu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			InitMenu();
		}

		void ClearBuffers()
		{
			int indx;

			for(indx = 0; indx < 262; indx++)
			{
				RecvBuff[indx] = 0x00;
				SendBuff[indx] = 0x00;
			}													
		}
					
		void DisableAPDUInput()
		{
			tDataIn.Text = "";
			fApdu.Enabled = false;
		}

		void InitMenu()
		{
			cbReader.Items.Clear();
			bInit.Enabled = true;
			bList.Enabled = false;
			bConnect.Enabled = false;
			bBeginTran.Enabled = false;
			bStatus.Enabled = false;
			bTransmit.Enabled = false;
			bEnd.Enabled = false;
			bDisconnect.Enabled = false;
			bRelease.Enabled = false;
			mMsg.Text = "";
			DisplayOut(0, 0, "Program ready");
			DisableAPDUInput();
		}

		void DisplayOut(int mType, long msgCode, string PrintText)
		{
			switch(mType)
			{
				case 0:                                  // Notifications only
					mMsg.SelectionColor = Color.Green;
					break;
				case 1:                                  // Error Messages
					mMsg.SelectionColor = Color.Red;
					PrintText = ModWinsCard.GetScardErrMsg(retCode);
					break;
				case 2:                                  // Input data
					mMsg.SelectionColor  = Color.Black;
					PrintText = "< " + PrintText;
					break;
				case 3:                                  // Output data
					mMsg.SelectionColor = Color.Black;
					PrintText = "> " + PrintText;
					break;
				case 4:                                  // Critical Errors
					mMsg.SelectionColor = Color.Red;
					break;
			}

		
			mMsg.SelectedText = PrintText + "\n"; 
			mMsg.SelectionStart = mMsg.Text.Length; 
			mMsg.SelectionColor = Color.Black;
	
		}



		private int SendAPDU()
		{
			ioRequest.dwProtocol = Protocol;
			ioRequest.cbPciLength = 8; 
			tmpStr = "";
			RecvLen = 262;

			retCode = ModWinsCard.SCardTransmit(hCard, ref ioRequest, ref SendBuff[0], SendLen, ref ioRequest, ref RecvBuff[0], ref RecvLen);

			if(retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				DisplayOut(1, retCode, "");
				return(retCode);
			}

			return(retCode);

		}


		private void bInit_Click(object sender, System.EventArgs e)
		{
			ReaderCount = 255;

			//Establish context and obtain hContext handle
			retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);

			if(retCode != ModWinsCard.SCARD_S_SUCCESS)
				DisplayOut(1, retCode, "");
			else
				DisplayOut(0, 0, "SCardEstablishContext... OK");

			bInit.Enabled = false;
			bList.Enabled = true;
			bRelease.Enabled = true;
		}

		private void bList_Click(object sender, System.EventArgs e)
		{
			//used to split null delimited strings into string arrays
			char[] delimiter = new char[1];
			delimiter[0] = Convert.ToChar(0);

			string ReaderList = ""+Convert.ToChar(0);
			
			string temp = ""+Convert.ToChar(0);

			int pcchReaders= -1;
			
			// List PC/SC card readers installed in the system
			retCode = ModWinsCard.SCardListReaders(this.hContext, temp, ref ReaderList, ref pcchReaders);
		
			if (retCode != ModWinsCard.SCARD_S_SUCCESS)
				DisplayOut(1, retCode, "");
			else
				DisplayOut(0, 0, "SCardListReaders... OK");

			string[] pcReaders = ReaderList.Split(delimiter);

			cbReader.Items.Add(ReaderList);
			cbReader.SelectedIndex = 0;

			bConnect.Enabled = true;
		}

		private void bConnect_Click(object sender, System.EventArgs e)
		{
			//'Connect to selected reader using hContext handle
			//'and obtain valid hCard handle
			retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_EXCLUSIVE,
				ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, 
				ref hCard, ref Protocol);
			if(retCode != ModWinsCard.SCARD_S_SUCCESS)
				DisplayOut(1, retCode, "");
			else
				DisplayOut(0, 0, "SCardConnect...OK");

			bList.Enabled = false;
			bConnect.Enabled = false;
			bBeginTran.Enabled = true;
			bDisconnect.Enabled = true;
			bRelease.Enabled = false;
		}

		private void bBeginTran_Click(object sender, System.EventArgs e)
		{
			retCode = ModWinsCard.SCardBeginTransaction(hCard);

			if(retCode != ModWinsCard.SCARD_S_SUCCESS)
				DisplayOut(1, retCode, "");
			else
				DisplayOut(0, 0, "SCardBeginTransaction...OK");

			fApdu.Enabled = true;
			tDataIn.Focus();
			bBeginTran.Enabled = false;
			bStatus.Enabled = true;
			bTransmit.Enabled = true;
			bEnd.Enabled = true;
			bDisconnect.Enabled = false;

		}

		private void bStatus_Click(object sender, System.EventArgs e)
		{
			int ReaderLen, dwState, ATRLen, indx;
			byte[] ATRVal=new byte[31];
			ATRLen = 32;

			ReaderLen = 0;
			dwState = 0;

			retCode = ModWinsCard.SCardStatus(hCard, cbReader.Text, ref ReaderLen, ref dwState, 
				ref Protocol, ref ATRVal[0], ref ATRLen);
			if(retCode != ModWinsCard.SCARD_S_SUCCESS)
				DisplayOut(1, retCode, "");
			else
				DisplayOut(0, 0, "SCardStatus...OK");

			//'Format ATRVal returned and display string as ATR value
			tmpStr = "";
			for(indx = 0; indx < ATRLen; indx++)
			{
				tmpStr = tmpStr +  string.Format("{0:x2}",ATRVal[indx]).ToUpper() + " "; 
			}
			DisplayOut(3, 0, tmpStr);

			//'Interpret dwstate returned and display as state
			switch(dwState)
			{
				case 0:
					tmpStr = "SCARD_UNKNOWN";
					break;
				case 1:
					tmpStr = "SCARD_ABSENT";
					break;
				case 2:
					tmpStr = "SCARD_PRESENT";
					break;
				case 3:
					tmpStr = "SCARD_SWALLOWED";
					break;
				case 4:
					tmpStr = "SCARD_POWERED";
					break;
				case 5:
					tmpStr = "SCARD_NEGOTIABLE";
					break;
				case 6:
					tmpStr = "SCARD_SPECIFIC";
					break;
			}
			DisplayOut(3, 0, "Reader State: " + tmpStr);

			//'Interpret dwActProtocol returned and display as active protocol
			switch(Protocol)
			{
				case 1:
					tmpStr = "T=0";
					break;
								
				case 2:
					tmpStr = "T=1";
					break;
			}
			DisplayOut(3, 0, "Active protocol: " + tmpStr);

		}

			
		private void bTransmit_Click(object sender, System.EventArgs e)
		{
			if (tDataIn.Text == "")
			{
				DisplayOut(4, 0, "No data input");
				return;
			}    
        
			tmpStr = (tDataIn.Text.Replace(" ",""));
			if (tmpStr.Length < 10)
			{
				DisplayOut(4, 0, "Insufficient data input");
				return;
			}
            
			if((tmpStr.Length % 2) != 0)
			{
				DisplayOut(4, 0, "Invalid data input, uneven number of characters");
				tDataIn.Focus();
				return;
			}
        
		ClearBuffers();
								
			// if APDU length < 6 then P3 is Le
			if (tmpStr.Length < 12)
			{
				for(indx = 0; indx <= 4; indx++)
				{
					SendBuff[indx] = Convert.ToByte(tmpStr.Substring(indx * 2 , 2),16);
				}

				SendLen = 0x05;
				RecvLen = SendBuff[4] + 2;
				tmpStr = "";

				for (indx = 0; indx <= 4; indx++)
				{
					tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
				}

				DisplayOut(2, 0, tmpStr);
				retCode = SendAPDU();

				if (retCode == ModWinsCard.SCARD_S_SUCCESS)
					tmpStr = "";

				for (indx = 0; indx < RecvLen ; indx++)
				{
					tmpStr = tmpStr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " "; 
				}

				DisplayOut(3, 0, tmpStr);
			}   
			else
			{
				for (indx = 0; indx <= 4; indx++)
				{
					SendBuff[indx] = Convert.ToByte(tmpStr.Substring(indx * 2, 2),16);
				}
            
				SendLen = 0x05 + SendBuff[4];
				if (tmpStr.Length < SendLen * 2)
				{
					DisplayOut(4, 0, "Invalid data input, insufficient data length");
					tDataIn.Focus();
					return;
				}
            
				for (indx = 0; indx < SendBuff[4] - 1; indx++)
				{					
					SendBuff[indx + 5] = Convert.ToByte(tmpStr.Substring((indx + 5) * 2, 2),16);
				}

				RecvLen = 0x02;
				tmpStr = "";

				for (indx = 0; indx < SendLen ; indx++)
				{
					tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper()+ " ";
				}

				DisplayOut(2, 0, tmpStr);
				retCode = SendAPDU();

				if (retCode == ModWinsCard.SCARD_S_SUCCESS)
				{
					tmpStr = "";
					for (indx = 0; indx < RecvLen ; indx++)
					{
						tmpStr = tmpStr +  string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " "; 
					}
					DisplayOut(3, 0, tmpStr);
				}
			}

		}

		private void bEnd_Click(object sender, System.EventArgs e)
		{
			retCode = ModWinsCard.SCardEndTransaction(hCard, ModWinsCard.SCARD_LEAVE_CARD);
			if (retCode != ModWinsCard.SCARD_S_SUCCESS)
			{
				DisplayOut(1, retCode, "");
			}    
			else
				DisplayOut(0, 0, "SCardEndTransaction...OK");

        DisableAPDUInput();

        bBeginTran.Enabled = true;
        bStatus.Enabled = false;
        bTransmit.Enabled = false;
        bEnd.Enabled = false;
        bDisconnect.Enabled = true;
		}

		private void bDisconnect_Click(object sender, System.EventArgs e)
		{
		retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

        if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            DisplayOut(1, retCode, "");
        else
            DisplayOut(0, 0, "SCardDisconnect...OK");

        bList.Enabled = true;
        bConnect.Enabled = true;
        bBeginTran.Enabled = false;
        bDisconnect.Enabled = false;
        bRelease.Enabled = true;
		}

		private void bRelease_Click(object sender, System.EventArgs e)
		{
		retCode = ModWinsCard.SCardReleaseContext(hContext);

        if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            DisplayOut(1, retCode, "");
        else
            DisplayOut(0, 0, "SCardReleaseContext...OK");

        bInit.Enabled = true;
        bList.Enabled = false;
        bConnect.Enabled = false;
        bRelease.Enabled = false;
        cbReader.Items.Clear();
		}

		private void bQuit_Click(object sender, System.EventArgs e)
		{
			retCode = ModWinsCard.SCardReleaseContext(hContext);
			retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			Application.Exit();
		}

		private void bClear_Click(object sender, System.EventArgs e)
		{
			mMsg.Clear(); 
		}

		private void tDataIn_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
		}

	}

}