/*  Copyright(C):     Advanced Card Systems Ltd
'
'  Project Name:      IICSample
'
'  Description:       This sample program outlines the steps on how to
'                     program IIC memory cards using ACS readers
'                     in PC/SC platform.
'
'  Author:            Malcolm Bernard U. Solaña
'
'  Date:              Feb 19, 2007
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace IICSample
{
	/// <summary>
	/// Summary description for IICSample.
	/// </summary>
	public class IICMain : System.Windows.Forms.Form
	{	
		// Variable Declaration
		int G_hContext;		//card reader context handle
		int hCard;			//card connection handle
		int ActiveProtocol, retcode, nBytesRet;  
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		byte[] tmpArray =  new byte[56];
		string tmpStr; 
		bool ConnActive;
		ModWinsCard.APDURec apdu  = new ModWinsCard.APDURec();		
		ModWinsCard.SCARD_IO_REQUEST sIO = new ModWinsCard.SCARD_IO_REQUEST();
		int SendBuffLen, RecvBuffLen;

		private System.Windows.Forms.ComboBox cbReader;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bInit;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button bConnect;
		private System.Windows.Forms.GroupBox fFunction;
		private System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button bReset;
		private System.Windows.Forms.Button bQuit;
		private System.Windows.Forms.Label Label3;
		private System.Windows.Forms.ComboBox cbPageSize;
		private System.Windows.Forms.Button bSet;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tBitAdd;
		private System.Windows.Forms.TextBox tHiAdd;
		private System.Windows.Forms.TextBox tLoAdd;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tLen;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tData;
		private System.Windows.Forms.Button bRead;
		private System.Windows.Forms.Button bWrite;
		private System.Windows.Forms.ComboBox cbCardType;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public IICMain()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IICMain));
			this.cbReader = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bInit = new System.Windows.Forms.Button();
			this.cbCardType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.bConnect = new System.Windows.Forms.Button();
			this.fFunction = new System.Windows.Forms.GroupBox();
			this.bWrite = new System.Windows.Forms.Button();
			this.bRead = new System.Windows.Forms.Button();
			this.tData = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tLen = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tLoAdd = new System.Windows.Forms.TextBox();
			this.tHiAdd = new System.Windows.Forms.TextBox();
			this.tBitAdd = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.bSet = new System.Windows.Forms.Button();
			this.cbPageSize = new System.Windows.Forms.ComboBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.lstOutput = new System.Windows.Forms.ListBox();
			this.bReset = new System.Windows.Forms.Button();
			this.bQuit = new System.Windows.Forms.Button();
			this.fFunction.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbReader
			// 
			this.cbReader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbReader.Location = new System.Drawing.Point(104, 8);
			this.cbReader.Name = "cbReader";
			this.cbReader.Size = new System.Drawing.Size(160, 22);
			this.cbReader.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 23);
			this.label1.TabIndex = 13;
			this.label1.Text = "Select Reader";
			// 
			// bInit
			// 
			this.bInit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bInit.Location = new System.Drawing.Point(160, 40);
			this.bInit.Name = "bInit";
			this.bInit.Size = new System.Drawing.Size(104, 24);
			this.bInit.TabIndex = 1;
			this.bInit.Text = "&Initialize";
			this.bInit.Click += new System.EventHandler(this.bInit_Click);
			// 
			// cbCardType
			// 
			this.cbCardType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbCardType.Items.AddRange(new object[] {
															"Auto Detect",
															"1 Kbit",
															"2 Kbit",
															"4 Kbit",
															"8 Kbit",
															"16 Kbit",
															"32 Kbit",
															"64 Kbit",
															"128 Kbit",
															"256 Kbit",
															"512 Kbit",
															"1024 Kbit"});
			this.cbCardType.Location = new System.Drawing.Point(160, 72);
			this.cbCardType.Name = "cbCardType";
			this.cbCardType.Size = new System.Drawing.Size(104, 22);
			this.cbCardType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 23);
			this.label2.TabIndex = 16;
			this.label2.Text = "Select Card Type";
			// 
			// bConnect
			// 
			this.bConnect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bConnect.Location = new System.Drawing.Point(160, 104);
			this.bConnect.Name = "bConnect";
			this.bConnect.Size = new System.Drawing.Size(104, 24);
			this.bConnect.TabIndex = 3;
			this.bConnect.Text = "&Connect";
			this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
			// 
			// fFunction
			// 
			this.fFunction.Controls.Add(this.bWrite);
			this.fFunction.Controls.Add(this.bRead);
			this.fFunction.Controls.Add(this.tData);
			this.fFunction.Controls.Add(this.label6);
			this.fFunction.Controls.Add(this.tLen);
			this.fFunction.Controls.Add(this.label5);
			this.fFunction.Controls.Add(this.tLoAdd);
			this.fFunction.Controls.Add(this.tHiAdd);
			this.fFunction.Controls.Add(this.tBitAdd);
			this.fFunction.Controls.Add(this.label4);
			this.fFunction.Controls.Add(this.bSet);
			this.fFunction.Controls.Add(this.cbPageSize);
			this.fFunction.Controls.Add(this.Label3);
			this.fFunction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fFunction.Location = new System.Drawing.Point(16, 136);
			this.fFunction.Name = "fFunction";
			this.fFunction.Size = new System.Drawing.Size(248, 192);
			this.fFunction.TabIndex = 4;
			this.fFunction.TabStop = false;
			this.fFunction.Text = "Functions";
			// 
			// bWrite
			// 
			this.bWrite.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bWrite.Location = new System.Drawing.Point(128, 152);
			this.bWrite.Name = "bWrite";
			this.bWrite.Size = new System.Drawing.Size(88, 24);
			this.bWrite.TabIndex = 9;
			this.bWrite.Text = "&Write";
			this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
			// 
			// bRead
			// 
			this.bRead.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bRead.Location = new System.Drawing.Point(24, 152);
			this.bRead.Name = "bRead";
			this.bRead.Size = new System.Drawing.Size(80, 24);
			this.bRead.TabIndex = 8;
			this.bRead.Text = "&Read";
			this.bRead.Click += new System.EventHandler(this.bRead_Click);
			// 
			// tData
			// 
			this.tData.Location = new System.Drawing.Point(56, 120);
			this.tData.Name = "tData";
			this.tData.Size = new System.Drawing.Size(176, 22);
			this.tData.TabIndex = 7;
			this.tData.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 25;
			this.label6.Text = "Data";
			// 
			// tLen
			// 
			this.tLen.Location = new System.Drawing.Point(88, 88);
			this.tLen.MaxLength = 2;
			this.tLen.Name = "tLen";
			this.tLen.Size = new System.Drawing.Size(32, 22);
			this.tLen.TabIndex = 6;
			this.tLen.Text = "";
			this.tLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tLen_KeyPress);
			this.tLen.TextChanged += new System.EventHandler(this.tLen_TextChanged);
			this.tLen.Leave += new System.EventHandler(this.tLen_Leave);
			this.tLen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tLen_KeyUp);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 23);
			this.label5.TabIndex = 23;
			this.label5.Text = "Length";
			// 
			// tLoAdd
			// 
			this.tLoAdd.Location = new System.Drawing.Point(168, 56);
			this.tLoAdd.MaxLength = 2;
			this.tLoAdd.Name = "tLoAdd";
			this.tLoAdd.Size = new System.Drawing.Size(32, 22);
			this.tLoAdd.TabIndex = 5;
			this.tLoAdd.Text = "";
			this.tLoAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tLoAdd_KeyPress);
			this.tLoAdd.TextChanged += new System.EventHandler(this.tLoAdd_TextChanged);
			// 
			// tHiAdd
			// 
			this.tHiAdd.Location = new System.Drawing.Point(128, 56);
			this.tHiAdd.MaxLength = 2;
			this.tHiAdd.Name = "tHiAdd";
			this.tHiAdd.Size = new System.Drawing.Size(32, 22);
			this.tHiAdd.TabIndex = 4;
			this.tHiAdd.Text = "";
			this.tHiAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tHiAdd_KeyPress);
			this.tHiAdd.TextChanged += new System.EventHandler(this.tHiAdd_TextChanged);
			// 
			// tBitAdd
			// 
			this.tBitAdd.Location = new System.Drawing.Point(88, 56);
			this.tBitAdd.MaxLength = 1;
			this.tBitAdd.Name = "tBitAdd";
			this.tBitAdd.Size = new System.Drawing.Size(32, 22);
			this.tBitAdd.TabIndex = 3;
			this.tBitAdd.Text = "";
			this.tBitAdd.TextChanged += new System.EventHandler(this.tBitAdd_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 23);
			this.label4.TabIndex = 19;
			this.label4.Text = "Address";
			// 
			// bSet
			// 
			this.bSet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bSet.Location = new System.Drawing.Point(128, 21);
			this.bSet.Name = "bSet";
			this.bSet.Size = new System.Drawing.Size(104, 24);
			this.bSet.TabIndex = 2;
			this.bSet.Text = "&Set Page Size";
			this.bSet.Click += new System.EventHandler(this.bSet_Click);
			// 
			// cbPageSize
			// 
			this.cbPageSize.Items.AddRange(new object[] {
															"8-byte",
															"16-byte",
															"32-byte",
															"64-byte",
															"128-byte"});
			this.cbPageSize.Location = new System.Drawing.Point(52, 21);
			this.cbPageSize.Name = "cbPageSize";
			this.cbPageSize.Size = new System.Drawing.Size(68, 22);
			this.cbPageSize.TabIndex = 1;
			// 
			// Label3
			// 
			this.Label3.Location = new System.Drawing.Point(16, 24);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(48, 16);
			this.Label3.TabIndex = 0;
			this.Label3.Text = "Size";
			// 
			// lstOutput
			// 
			this.lstOutput.HorizontalScrollbar = true;
			this.lstOutput.Location = new System.Drawing.Point(280, 8);
			this.lstOutput.Name = "lstOutput";
			this.lstOutput.ScrollAlwaysVisible = true;
			this.lstOutput.Size = new System.Drawing.Size(272, 277);
			this.lstOutput.TabIndex = 5;
			// 
			// bReset
			// 
			this.bReset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bReset.Location = new System.Drawing.Point(312, 296);
			this.bReset.Name = "bReset";
			this.bReset.Size = new System.Drawing.Size(88, 32);
			this.bReset.TabIndex = 6;
			this.bReset.Text = "Re&set";
			this.bReset.Click += new System.EventHandler(this.bReset_Click);
			// 
			// bQuit
			// 
			this.bQuit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bQuit.Location = new System.Drawing.Point(432, 296);
			this.bQuit.Name = "bQuit";
			this.bQuit.Size = new System.Drawing.Size(88, 32);
			this.bQuit.TabIndex = 7;
			this.bQuit.Text = "&Quit";
			this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
			// 
			// IICMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 342);
			this.Controls.Add(this.bQuit);
			this.Controls.Add(this.bReset);
			this.Controls.Add(this.lstOutput);
			this.Controls.Add(this.fFunction);
			this.Controls.Add(this.bConnect);
			this.Controls.Add(this.cbCardType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bInit);
			this.Controls.Add(this.cbReader);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "IICMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "IIC";
			this.Load += new System.EventHandler(this.IICMain_Load);
			this.fFunction.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new IICMain());
		}
		
		private void InitMenu()
		{
			cbReader.Items.Clear();
			bInit.Enabled = true;
			cbCardType.Enabled = false;
			bConnect.Enabled = false;
			bReset.Enabled = false;
			fFunction.Enabled = false;
			lstOutput.Items.Clear();
			DisplayOut(0, 0, "Program Ready", "");
		}

		private void ClearBuffers()
		{						
			long indx;
  
			for (indx=0; indx<262; indx++)
			{
				RecvBuff[indx] = 0x00;
				SendBuff[indx] = 0x00;
			}
												
		}
		
		Char Chr(int i)
		{
			//Return the character of the given character value
			return Convert.ToChar(i);
		}

		private void AddButtons()
		{
			cbReader.Enabled = true;
			bInit.Enabled = false;
			cbCardType.Enabled = true;
			bConnect.Enabled = true;
			bReset.Enabled = true;
			cbCardType.SelectedIndex = 0;
		}
		
		private void ClearFields()
		{	
			tBitAdd.Text = "";
			tHiAdd.Text = "";
			tLoAdd.Text = "";
			tLen.Text = "";
			tData.Text = "";

		}

		Boolean InputOK(int checkType, int opType)
		{
			if(checkType == 1) // For 17-bit address input
			{
				if(tBitAdd.Text == "")
				{
					tBitAdd.Focus();
					return false;
				}
			}
			if(tHiAdd.Text == "")
			{
				tHiAdd.Focus();
				return false;
			}
			if(tLoAdd.Text == "")
			{
				tLoAdd.Focus();
				return false;
			}
			if(opType == 1) // For Write Operation
			{
				if(tData.Text == "")
				{
					tData.Focus();
					return false;
				}
			}
			
			return true;
		}
		
		int SendAPDUandDisplay(int SendType, string ApduIn)
		{
			int indx;
			string tmpstr;
			
			sIO.dwProtocol = this.ActiveProtocol;
			sIO.cbPciLength = 8;

			DisplayOut(2, 0, ApduIn, "MCU");
			tmpstr = "";

			retcode = ModWinsCard.SCardTransmit(hCard, ref sIO,  ref SendBuff[0], SendBuffLen, ref sIO,  ref RecvBuff[0], ref RecvBuffLen);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{	
				DisplayOut(1, retcode, "" , "MCU");
				return retcode;
			}
			else
			{
				switch (SendType)
				{
					case 0 : // Display SW1/SW2 value
						for(indx = RecvBuffLen - 2; indx <= RecvBuffLen - 1; indx++)
						{
							tmpstr = tmpstr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
						}
						break;   
					case 1 : // Display ATR after checking SW1/SW2
						for(indx = RecvBuffLen - 2; indx <= RecvBuffLen - 1; indx++)
						{
							tmpstr = tmpstr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
						}
						DisplayOut(3, 0, tmpstr, "MCU");
						if (tmpstr != "90 00 ") 
						{
							DisplayOut(1, 0, "Return bytes are not acceptable.", "MCU");
						}
						else
						{
							tmpstr = "ATR: ";
							for(indx = 0; indx <= RecvBuffLen -3; indx++)
							{
								tmpstr = tmpstr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
							}
						}
						break;   
					case 2 : // Display all data after checking SW1/SW2
						for(indx = RecvBuffLen - 2; indx <= RecvBuffLen - 1; indx++)
						{	
							tmpstr = tmpstr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
						}
						if (tmpstr != "90 00 ") 
						{
							DisplayOut(1, 0, "Return bytes are not acceptable.", "MCU");
						}
						else
						{
							tmpstr = "";
							for(indx = 0; indx <= RecvBuffLen -3; indx++)
							{
								tmpstr = tmpstr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
							}
						}
						break;   
				}
				DisplayOut(3, 0, tmpstr, "MCU");
			}

			return retcode;
		}

		int DisplayOut(int errType, int retVal, string PrintText, string AppText) 
		{
			//Displays the APDU sent and recieved 
			//returns 1 if erronous and 0 if successful
			int i;
	
			if (errType == 0) 	//Information
			{
				i = lstOutput.Items.Add("> " + PrintText);
				lstOutput.SelectedIndex = i;
			}	
			else if (errType == 1)  //Error Messages
			{
				i = lstOutput.Items.Add(AppText + " Error> " + ModWinsCard.GetScardErrMsg(retVal));
				lstOutput.SelectedIndex = i;
				return 1;
				
			}
			else if (errType == 2) 	//Into Card command
			{
				i = lstOutput.Items.Add(AppText + "< " + PrintText);
				lstOutput.SelectedIndex = i;
			}
			else if (errType == 3) 	//Out from Card command
			{
				i = lstOutput.Items.Add(AppText + "> " + PrintText);
				lstOutput.SelectedIndex = i;
			}
			
			return 0;
		}


		private void bInit_Click(object sender, System.EventArgs e)
		{
			//Initialize List of Available Readers
			int pcchReaders = 0;
			int indx = 0;
			string rName = "";

			//Establish Context
			retcode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref G_hContext);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				DisplayOut(1, retcode, "SCardEstablishContext Error: " + ModWinsCard.GetScardErrMsg(retcode), "MCU");
				return;
			}
			else
			{
				DisplayOut(0, retcode, "SCardEstablishContext Success", "MCU");
			}

			//Get readers buffer len
			retcode = ModWinsCard.SCardListReaders(G_hContext, null, null, ref pcchReaders);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				DisplayOut(1, retcode, "SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(retcode), "MCU");
				return;
			}

			//Set reader buffer size
			byte[] ReadersList = new byte[pcchReaders];

			// Fill reader list
			retcode = ModWinsCard.SCardListReaders(G_hContext, null, ReadersList, ref pcchReaders);
				
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				DisplayOut(1, retcode, "SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(retcode), "MCU");
				return;
			}
			else
			{
				DisplayOut(0, retcode, "SCardListReaders Success", "MCU");
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
				cbReader.Text = rName;
				cbReader.Items.Add(rName);
				rName = "";
				indx = indx + 1;
				
			}

			this.AddButtons();
			cbCardType.Focus();
		}


		private void IICMain_Load(object sender, System.EventArgs e)
		{
			// Established using ScardEstablishedContext()
			retcode = ModWinsCard.SCardEstablishContext(G_hContext, 0, 0, ref G_hContext);
			
			if (retcode !=  ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("SCardEstablishContext Error"); 
			}

			InitMenu();

		}
 

		private void bReset_Click(object sender, System.EventArgs e)
		{
			//Disconnect and unpower card
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("Disconnection Error!"); 
			}
			else
			{
				lstOutput.Items.Add ("Disconnect OK");
				ConnActive = false;
			}
	
			lstOutput.SelectedIndex = -1;
			ClearFields();
			InitMenu();
		}


		private void bQuit_Click(object sender, System.EventArgs e)
		{
			//terminate the application
			retcode = ModWinsCard.SCardReleaseContext(G_hContext);
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			Application.Exit();
		}


		private void bConnect_Click(object sender, System.EventArgs e)
		{	
			int cardType;

			if (ConnActive == true)
			{
				DisplayOut(0, 0, "Connection is already active", "MCU");
				return;
			}

			// 1. Connect to reader using SCARD_SHARE_DIRECT
			retcode = ModWinsCard.SCardConnect(G_hContext,
												cbReader.Text,
												ModWinsCard.SCARD_SHARE_DIRECT,
												0, 
												ref hCard,
												ref ActiveProtocol);

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				DisplayOut(1, retcode, "", "MCU");
				ConnActive = false;
				return;
			}
			
			cardType = 0;
			// 2. Select Card Type
			switch(cbCardType.SelectedIndex)
			{
				case 0 : cardType = 0x1;
							break;
				case 1 : cardType = 0x2;
							break;
				case 2 : cardType = 0x3;
							break;
				case 3 : cardType = 0x4;
							break;
				case 4 : cardType = 0x5;
							break;
				case 5 : cardType = 0x6;
							break;
				case 6 : cardType = 0x7;
							break;
				case 7 : cardType = 0x8;
							break;
				case 8 : cardType = 0x9;
							break;
				case 9 : cardType = 0xA;
							break;
				case 10 : cardType = 0xB;
							break;
				case 11 : cardType = 0xC;
							break;
			}

			ClearBuffers();
			SendBuffLen = 4;
			SendBuff[0] = Convert.ToByte(cardType);
			RecvBuffLen = 262;
			nBytesRet = 0;

			retcode = ModWinsCard.SCardControl(hCard, 
												(uint) ModWinsCard.IOCTL_SMARTCARD_SET_CARD_TYPE,  
												ref SendBuff[0], 
												SendBuffLen, 
												ref RecvBuff[0], 
												RecvBuffLen, 
												ref nBytesRet);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				DisplayOut(1, retcode, "", "MCU");
				ConnActive = false;
				return;
			}
			
			
			// 3. Reconnect reader
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{	
				DisplayOut(1, retcode, "", "MCU");
				ConnActive = false;
				return;
			}

			retcode = ModWinsCard.SCardConnect(G_hContext, 
												cbReader.Text, 
												ModWinsCard.SCARD_SHARE_EXCLUSIVE, 
												0 | 1, 
												ref hCard, 
												ref ActiveProtocol);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("Connection Error"); 				
				DisplayOut(1, retcode, "", "MCU");
				ConnActive = false;
				return;
			}
			else
			{
				DisplayOut(0, 0, "Successful connection to " + cbReader.Text, "MCU");
				ConnActive = true;
			}	
			
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
  			
			fFunction.Enabled = true;
			cbPageSize.SelectedIndex = 0;

			if(cbCardType.SelectedIndex == 11)
			{
				tBitAdd.Enabled = true;
			}
			else
			{
				tBitAdd.Enabled = false;
			}
			cbPageSize.Focus();
		}


		private void tBitAdd_TextChanged(object sender, System.EventArgs e)
		{
			tBitAdd.Text = tBitAdd.Text.ToUpper();
		
			if (tBitAdd.Text.Length > 0)
			{
				tHiAdd.Focus();
			}
		}


		private void tHiAdd_TextChanged(object sender, System.EventArgs e)
		{
			tHiAdd.Text = tHiAdd.Text.ToUpper();
		
			if (tHiAdd.Text.Length > 1)
			{
				tLoAdd.Focus();
			}
		}


		private void tLen_TextChanged(object sender, System.EventArgs e)
		{
			tLen.Text = tLen.Text.ToUpper();
			if (tLen.Text.Length > 1)
			{
				tData.Focus();
			}
		}


		private void tLoAdd_TextChanged(object sender, System.EventArgs e)
		{
		
			tLoAdd.Text = tLoAdd.Text.ToUpper();
			if (tLoAdd.Text.Length > 1)
			{
				tLen.Focus();
			}
		}


		private void bSet_Click(object sender, System.EventArgs e)
		{
			int indx;

			ClearBuffers();
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0x01;
			SendBuff[2] = 0x00;
			SendBuff[3] = 0x00;
			SendBuff[4] = 0x01;

			switch(cbPageSize.SelectedIndex)
			{
				case 0 : SendBuff[5] = 0x03;
					break;
				case 1 : SendBuff[5] = 0x04;
					break;
				case 2 : SendBuff[5] = 0x05;
					break;
				case 3 : SendBuff[5] = 0x06;
					break;
				case 4 : SendBuff[5] = 0x07;
					break;
			}
			
			tmpStr = "";
			SendBuffLen = 6;
			RecvBuffLen = 2;
		
			for(indx = 0; indx <= SendBuffLen -1; indx++)
			{
				tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
			}
			
			retcode = this.SendAPDUandDisplay(0, tmpStr);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
		}


		private void bRead_Click(object sender, System.EventArgs e)
		{
			int indx;

			// 1. Check for all input fields
			if(cbCardType.SelectedIndex == 11)
			{
				indx = 1;
			}
			else
			{
				indx = 0;
			}

			if(this.InputOK(indx, 0) == false)
			{
				return;
			}

			// 2. Read input fields and pass data to card
			tData.Text = "";
			ClearBuffers();
			SendBuff[0] = 0xFF;
			
			if(cbCardType.SelectedIndex == 11 && tBitAdd.Text == "1") 
			{
				SendBuff[1] = 0xB1;
			}
			else
			{
				SendBuff[1] = 0xB0;
			}
			
			SendBuff[2] = Convert.ToByte(tHiAdd.Text.Substring(0, 2),16);
			SendBuff[3] = Convert.ToByte(tLoAdd.Text.Substring(0, 2),16);
			SendBuff[4] = Convert.ToByte(tLen.Text.Substring(0, 2),16);
			SendBuffLen = 5;
			RecvBuffLen = SendBuff[4] + 2;
			tmpStr = "";
			
			for(indx = 0; indx <= 5; indx++)
			{
				tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
			}

			retcode = this.SendAPDUandDisplay(2, tmpStr);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			 // 3. Display data read from card into Data textbox
			tmpStr = ""; 
			
			for(indx = 0; indx <= SendBuff[4] - 1; indx++)
			{	
				tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);
			}
			tData.Text = tmpStr;
		}


		private void bWrite_Click(object sender, System.EventArgs e)
		{
			int indx;

			// 1. Check for all input fields
			if(cbCardType.SelectedIndex == 11)
			{
				indx = 1;
			}
			else
			{
				indx = 0;
			}
			
			if(this.InputOK(indx, 1) == false) 
			{
				return;
			}

			// 2. Read input fields and pass data to card
			tmpStr = tData.Text;
			ClearBuffers();
			SendBuff[0] = 0xFF;
			
			if(cbCardType.SelectedIndex == 11 && tBitAdd.Text == "1")
			{
				SendBuff[1] = 0xD1;
			}
			else
			{
				SendBuff[1] = 0xD0;
			}
			
			SendBuff[2] = Convert.ToByte(tHiAdd.Text.Substring(0, 2),16);
			SendBuff[3] = Convert.ToByte(tLoAdd.Text.Substring(0, 2),16);
			SendBuff[4] = Convert.ToByte(tLen.Text.Substring(0, 2),16);
			
			for(indx = 0; indx <= tmpStr.Length -1; indx++)
			{
				SendBuff[indx + 5] =  Convert.ToByte(tmpStr[indx]);  
			}
			
			SendBuffLen = SendBuff[4] + 5;
			RecvBuffLen = 2;
			tmpStr = "";
			
			for(indx = 0; indx <= SendBuffLen -1; indx++)
			{
				tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
			}
			
			retcode = this.SendAPDUandDisplay(0, tmpStr);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}
			tData.Text = "";
		}


		private void tLen_Leave(object sender, System.EventArgs e)
		{
			if(tLen.Text != "")
			{
				tData.Text = "";
				tData.MaxLength = Convert.ToByte(tLen.Text.Substring(0, 2),16);
			}
			else
			{
				tData.MaxLength = 0;
			}
		}


		private void tLen_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
			tLen.Text = tLen.Text.ToUpper();
			if (tLen.Text.Length > 1)
			{
				tData.Focus();
			}
		}

		
		private void tLen_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
		}

		
		private void tHiAdd_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
		}


		private void tLoAdd_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
