/*  Copyright(C):     Advanced Card Systems Ltd
'
'  Project Name:      SLE4432_4442
'
'  Description:       This sample program outlines the steps on how to
'                     program SLE4432/4442 memory cards using ACS readers
'                     in PC/SC platform.
'
'  Author:            Malcolm Bernard U. Solaña
'
'  Date:              Feb 15, 2007
'
'  Revision Trail:   (Date/Author/Description)
'						
'					  June 4, 2007 / Aileen Grace L. Sarte / Added the SLE5542 Memory card support 	
'
'==================================================================================================*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace SLE4432_4442
{
	/// <summary>
	/// Summary description for SLE4432_4442.
	/// </summary>
	public class SLE4432_4442Main : System.Windows.Forms.Form
	{	
		// variable declaration
		int G_hContext;		//card reader context handle
		int hCard;			//card connection handle
		int ActiveProtocol, retcode, nBytesRet;  
		string rreader, tmpStr;  
		byte[] SendBuff = new byte[262];
		byte[] RecvBuff = new byte[262];
		bool ConnActive;
		ModWinsCard.APDURec apdu  = new ModWinsCard.APDURec();		
		ModWinsCard.SCARD_IO_REQUEST sIO = new ModWinsCard.SCARD_IO_REQUEST(); 
		int SendBuffLen, RecvBuffLen;
		
		private System.Windows.Forms.ComboBox cbReader;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox fCardType;
		private System.Windows.Forms.RadioButton SLE4432;
		private System.Windows.Forms.RadioButton SLE4442;
		private System.Windows.Forms.Button bInit;
		private System.Windows.Forms.Button bConnect;
		private System.Windows.Forms.GroupBox fFunction;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tAdd;
		private System.Windows.Forms.TextBox tLen;
		private System.Windows.Forms.TextBox tData;
		private System.Windows.Forms.Button bErrCtr;
		private System.Windows.Forms.Button bWriteProt;
		private System.Windows.Forms.Button bSubmit;
		private System.Windows.Forms.Button bRead;
		private System.Windows.Forms.Button bWrite;
		private System.Windows.Forms.Button bChange;
		private System.Windows.Forms.Button bReset;
		private System.Windows.Forms.Button bQuit;
		private System.Windows.Forms.ListBox lstOutput;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SLE4432_4442Main()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SLE4432_4442Main));
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fCardType = new System.Windows.Forms.GroupBox();
            this.SLE4442 = new System.Windows.Forms.RadioButton();
            this.SLE4432 = new System.Windows.Forms.RadioButton();
            this.bInit = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.fFunction = new System.Windows.Forms.GroupBox();
            this.bChange = new System.Windows.Forms.Button();
            this.bWrite = new System.Windows.Forms.Button();
            this.bRead = new System.Windows.Forms.Button();
            this.bSubmit = new System.Windows.Forms.Button();
            this.bWriteProt = new System.Windows.Forms.Button();
            this.bErrCtr = new System.Windows.Forms.Button();
            this.tData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tLen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tAdd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bReset = new System.Windows.Forms.Button();
            this.bQuit = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.fCardType.SuspendLayout();
            this.fFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbReader
            // 
            this.cbReader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReader.Location = new System.Drawing.Point(112, 16);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(160, 22);
            this.cbReader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Reader";
            // 
            // fCardType
            // 
            this.fCardType.Controls.Add(this.SLE4442);
            this.fCardType.Controls.Add(this.SLE4432);
            this.fCardType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fCardType.Location = new System.Drawing.Point(24, 48);
            this.fCardType.Name = "fCardType";
            this.fCardType.Size = new System.Drawing.Size(136, 88);
            this.fCardType.TabIndex = 2;
            this.fCardType.TabStop = false;
            this.fCardType.Text = "Card Type";
            // 
            // SLE4442
            // 
            this.SLE4442.Location = new System.Drawing.Point(16, 56);
            this.SLE4442.Name = "SLE4442";
            this.SLE4442.Size = new System.Drawing.Size(112, 16);
            this.SLE4442.TabIndex = 1;
            this.SLE4442.Text = "SLE 4442/5542";
            this.SLE4442.Click += new System.EventHandler(this.SLE4442_Click);
            // 
            // SLE4432
            // 
            this.SLE4432.Location = new System.Drawing.Point(16, 24);
            this.SLE4432.Name = "SLE4432";
            this.SLE4432.Size = new System.Drawing.Size(104, 24);
            this.SLE4432.TabIndex = 0;
            this.SLE4432.Text = "SLE 4432";
            this.SLE4432.Click += new System.EventHandler(this.SLE4432_Click);
            // 
            // bInit
            // 
            this.bInit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bInit.Location = new System.Drawing.Point(184, 56);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(80, 24);
            this.bInit.TabIndex = 3;
            this.bInit.Text = "&Initialize";
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // bConnect
            // 
            this.bConnect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bConnect.Location = new System.Drawing.Point(184, 96);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(80, 24);
            this.bConnect.TabIndex = 4;
            this.bConnect.Text = "&Connect";
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // fFunction
            // 
            this.fFunction.Controls.Add(this.bChange);
            this.fFunction.Controls.Add(this.bWrite);
            this.fFunction.Controls.Add(this.bRead);
            this.fFunction.Controls.Add(this.bSubmit);
            this.fFunction.Controls.Add(this.bWriteProt);
            this.fFunction.Controls.Add(this.bErrCtr);
            this.fFunction.Controls.Add(this.tData);
            this.fFunction.Controls.Add(this.label4);
            this.fFunction.Controls.Add(this.tLen);
            this.fFunction.Controls.Add(this.label3);
            this.fFunction.Controls.Add(this.tAdd);
            this.fFunction.Controls.Add(this.label2);
            this.fFunction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fFunction.Location = new System.Drawing.Point(16, 152);
            this.fFunction.Name = "fFunction";
            this.fFunction.Size = new System.Drawing.Size(248, 200);
            this.fFunction.TabIndex = 5;
            this.fFunction.TabStop = false;
            this.fFunction.Text = "Functions";
            // 
            // bChange
            // 
            this.bChange.Enabled = false;
            this.bChange.Location = new System.Drawing.Point(136, 168);
            this.bChange.Name = "bChange";
            this.bChange.Size = new System.Drawing.Size(88, 23);
            this.bChange.TabIndex = 11;
            this.bChange.Text = "C&hange Code";
            this.bChange.Click += new System.EventHandler(this.bChange_Click);
            // 
            // bWrite
            // 
            this.bWrite.Location = new System.Drawing.Point(136, 136);
            this.bWrite.Name = "bWrite";
            this.bWrite.Size = new System.Drawing.Size(88, 24);
            this.bWrite.TabIndex = 10;
            this.bWrite.Text = "&Write";
            this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
            // 
            // bRead
            // 
            this.bRead.Location = new System.Drawing.Point(136, 104);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(88, 24);
            this.bRead.TabIndex = 9;
            this.bRead.Text = "Re&ad";
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // bSubmit
            // 
            this.bSubmit.Enabled = false;
            this.bSubmit.Location = new System.Drawing.Point(32, 168);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(88, 23);
            this.bSubmit.TabIndex = 8;
            this.bSubmit.Text = "&Submit Code";
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // bWriteProt
            // 
            this.bWriteProt.Location = new System.Drawing.Point(32, 136);
            this.bWriteProt.Name = "bWriteProt";
            this.bWriteProt.Size = new System.Drawing.Size(88, 23);
            this.bWriteProt.TabIndex = 7;
            this.bWriteProt.Text = "Write &Protect";
            this.bWriteProt.Click += new System.EventHandler(this.bWriteProt_Click);
            // 
            // bErrCtr
            // 
            this.bErrCtr.Enabled = false;
            this.bErrCtr.Location = new System.Drawing.Point(32, 104);
            this.bErrCtr.Name = "bErrCtr";
            this.bErrCtr.Size = new System.Drawing.Size(88, 23);
            this.bErrCtr.TabIndex = 6;
            this.bErrCtr.Text = "Read ErrC&tr";
            this.bErrCtr.Click += new System.EventHandler(this.bErrCtr_Click);
            // 
            // tData
            // 
            this.tData.Location = new System.Drawing.Point(16, 72);
            this.tData.Name = "tData";
            this.tData.Size = new System.Drawing.Size(224, 22);
            this.tData.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Data (String)";
            // 
            // tLen
            // 
            this.tLen.Location = new System.Drawing.Point(200, 24);
            this.tLen.MaxLength = 2;
            this.tLen.Name = "tLen";
            this.tLen.Size = new System.Drawing.Size(32, 22);
            this.tLen.TabIndex = 3;
            this.tLen.Leave += new System.EventHandler(this.tLen_Leave);
            this.tLen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tLen_KeyUp);
            this.tLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tLen_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(152, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Length";
            // 
            // tAdd
            // 
            this.tAdd.Location = new System.Drawing.Point(64, 24);
            this.tAdd.MaxLength = 2;
            this.tAdd.Name = "tAdd";
            this.tAdd.Size = new System.Drawing.Size(32, 22);
            this.tAdd.TabIndex = 1;
            this.tAdd.TextChanged += new System.EventHandler(this.tAdd_TextChanged);
            this.tAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tAdd_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Address";
            // 
            // bReset
            // 
            this.bReset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bReset.Location = new System.Drawing.Point(320, 320);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(75, 24);
            this.bReset.TabIndex = 7;
            this.bReset.Text = "R&eset";
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(464, 320);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(75, 23);
            this.bQuit.TabIndex = 0;
            this.bQuit.Text = "&Quit";
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(288, 8);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(288, 303);
            this.lstOutput.TabIndex = 8;
            // 
            // SLE4432_4442Main
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(584, 366);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.fFunction);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.fCardType);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SLE4432_4442Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Using SLE4432/4442/5542 in PC/SC";
            this.Load += new System.EventHandler(this.SLE4432_4442Main_Load);
            this.fCardType.ResumeLayout(false);
            this.fFunction.ResumeLayout(false);
            this.fFunction.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SLE4432_4442Main());
		}

		private void SLE4432_4442Main_Load(object sender, System.EventArgs e)
		{
			// Established using ScardEstablishedContext()
			retcode = ModWinsCard.SCardEstablishContext(G_hContext, 0, 0, ref G_hContext);
			
			if (retcode !=  ModWinsCard.SCARD_S_SUCCESS) 
			{       
				lstOutput.Items.Add ("SCardEstablishContext Error"); 
			}

			InitMenu();
		}
		
		Char Chr(int i)
		{
			//Return the character of the given character value
			return Convert.ToChar(i);
		}
	
		Boolean InputOK(int checkType)
		{
			int indx;
  			
			switch (checkType)
			{
				case 0 :// for Read function
					if (tAdd.Text == "")
					{					  
						tAdd.Focus();
						return false;
					}
					if (tLen.Text == "")
					{
						tLen.Focus();
						return false;
					}
					break;
				case 1 :// for Write function
					if (tAdd.Text == "") 
					{
						tAdd.Focus();
						return false;
					}
					if(tLen.Text == "")
					{
						tLen.Focus();
						return false;
					}
					if(tData.Text == "") 
					{
						tData.Focus();
						return false;
					}
					break;
				case 2 :// for Verify function
					tAdd.Text = "";
					tLen.Text = "";
					if (tData.Text == "") 
					{
						tData.Focus();
						return false;
					}
					
					tData.Text = tData.Text.ToUpper();
					tmpStr = "";
						
					for(indx = 0; indx <= (tData.Text.Length) -1; indx++)
					{	
						if (tData.Text.Substring(indx, 1) != " ")
						{
							tmpStr = tmpStr + string.Format(tData.Text.Substring(indx, 1),"%02X");
						}
					}
					if (tmpStr.Length != 6) 
					{	
						tData.SelectAll();
						tData.Focus();
						return false;
					}
						
					for(indx = 0; indx <= (tmpStr.Length) -1; indx++)
					{	if (Convert.ToInt32(tmpStr[indx]) < 48 || Convert.ToInt32(tmpStr[indx]) > 57)
						{
							if (Convert.ToInt32(tmpStr[indx]) < 65 || Convert.ToInt32(tmpStr[indx]) > 70)
							{
								tData.SelectAll();
								tData.Focus();
								return false;
							}
						}
					}
					break;
			}
			return true;
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

		int SendAPDUandDisplay(int SendType, string ApduIn)
		{
			int indx;
						
			sIO.dwProtocol = this.ActiveProtocol;
			sIO.cbPciLength = 8;

			DisplayOut(2, 0, ApduIn, "MCU");
			tmpStr = "";

			retcode = ModWinsCard.SCardTransmit(hCard, 
											ref sIO,  
											ref SendBuff[0], 
											SendBuffLen, 
											ref sIO,  
											ref RecvBuff[0], 
											ref RecvBuffLen);
			
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
							tmpStr = tmpStr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
						}
						break;   
					case 1 : // Display ATR after checking SW1/SW2
						for(indx = RecvBuffLen - 2; indx <= RecvBuffLen - 1; indx++)
						{
							tmpStr = tmpStr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
						}
						if (tmpStr != "90 00 ") 
						{
							DisplayOut(1, 0, "Return bytes are not acceptable.", "MCU");
						}
						else
						{
							tmpStr = "ATR: ";
							for(indx = 0; indx <= RecvBuffLen -3; indx++)
							{
								tmpStr = tmpStr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
							}
						}
						break;   
					case 2 : //' Display all data after checking SW1/SW2
						for(indx = RecvBuffLen - 2; indx <= RecvBuffLen - 1; indx++)
						{	
							tmpStr = tmpStr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
						}
						if (tmpStr != "90 00 ") 
						{
							DisplayOut(1, 0, "Return bytes are not acceptable.", "MCU");
						}
						else
						{
							tmpStr = "";
							for(indx = 0; indx <= RecvBuffLen -1; indx++)
							{
								tmpStr = tmpStr + string.Format("{0:x2}", RecvBuff[indx]).ToUpper() + " ";
							}
						}
						break;   
				}
				DisplayOut(3, 0, tmpStr, "MCU");
			}

			return retcode;
		}

		int DisplayOut(int errType, int retVal, string PrintText, string AppText) 
		{
			//Displays the APDU sent and recieved 
			//returns 1 if erronous and 0 if successful
			int i;
	
			if (errType == 0)
			{
				i = lstOutput.Items.Add("> " + PrintText);
				lstOutput.SelectedIndex = i;
			}		//Information
			else if (errType == 1)
			{
				i = lstOutput.Items.Add(AppText + "Error> " + ModWinsCard.GetScardErrMsg(retVal));
				//Convert.ToUInt64 (retVal)).ToString()
					lstOutput.SelectedIndex = i;
				return 1;
				//Error
			}
			else if (errType == 2)
			{
				i = lstOutput.Items.Add(AppText + "< " + PrintText);
				lstOutput.SelectedIndex = i;
				//Into Card command
			}
			else if (errType ==3)
			{
				i = lstOutput.Items.Add(AppText + "> " + PrintText);
				lstOutput.SelectedIndex = i;
				//Out from Card command
			}
			
			return 0;
		}
		
		private void InitMenu()
		{
			cbReader.Items.Clear();
			bInit.Enabled = true;
			fCardType.Enabled = false;
			bConnect.Enabled = false;
			bReset.Enabled = false;
			fFunction.Enabled = false;
			lstOutput.Items.Clear();
			DisplayOut(0, 0, "Program Ready", "");
		}

		private void AddButtons()
		{
			cbReader.Enabled = true;
			bInit.Enabled = false;
			fCardType.Enabled = true;
			SLE4432.Checked = true;
			bConnect.Enabled = true;
			bReset.Enabled = true;
		}

		
		private void ClearFields()
		{
			tAdd.Text = "";
			tLen.Text = "";
			tData.Text = "";

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
			
		}

		private void bConnect_Click(object sender, System.EventArgs e)
		{
            int indx;
            string tmpStr;

			if (this.ConnActive == true)
			{
				DisplayOut(0, 0, "Connection is already active", "MCU");
				return;
			}

			// 1. Connect to reader using SCARD_SHARE_DIRECT
			retcode = ModWinsCard.SCardConnect(G_hContext,
												cbReader.Text,
												ModWinsCard.SCARD_SHARE_SHARED,
												ModWinsCard.SCARD_PROTOCOL_T0, 
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
			
			}	
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;

            ClearBuffers();
            //Select CardType
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xA4;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x01;
            SendBuff[5] = 0x06;

            SendBuffLen = 6;
            RecvBuffLen = 255;

            tmpStr = "";
            for (indx = 0; indx <= SendBuffLen - 1; indx++)
            {
                tmpStr = tmpStr + string.Format("{0:x2}", (SendBuff[indx])).ToUpper() + " ";
            }
            retcode = this.SendAPDUandDisplay(2, tmpStr);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

			ConnActive = true;

			fFunction.Enabled = true;
			ClearFields(); 
			tAdd.Focus();
			
		}

		private void bReset_Click(object sender, System.EventArgs e)
		{
			//Disconnect  and unpower card
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
	
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
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

		private void bErrCtr_Click(object sender, System.EventArgs e)
		{	
			int indx;
			
			// 1. Clear all input fields
			ClearFields();

			// 2. Read input fields and pass data to card
			ClearBuffers();
			
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0xB1;
			SendBuff[2] = 0x00;
			SendBuff[3] = 0x00;
			SendBuff[4] = 0x04;
			
			SendBuffLen = 5;
			RecvBuffLen = 6;
			tmpStr = "";
			
			for (indx = 0; indx <= SendBuffLen - 1; indx++)
			{	
				tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
			}

			retcode = SendAPDUandDisplay(2, tmpStr);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				return;
			}
		}

		private void bRead_Click(object sender, System.EventArgs e)
		{
			int indx;
			
			// 1. Check for all input fields
			if(this.InputOK(0) == false)
			{
				return;
			}

			// 2. Read input fields and pass data to card
			tmpStr = tData.Text;
			
			ClearBuffers();
			
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0xB0;
			SendBuff[2] = 0x00;
			SendBuff[3] = Convert.ToByte(tAdd.Text.Substring(0, 2),16);
			SendBuff[4] = Convert.ToByte(tLen.Text.Substring(0, 2),16);

			SendBuffLen = 5;
			RecvBuffLen = SendBuff[4] + 6;

			tmpStr = "";
			
			for(indx = 0; indx <= SendBuffLen -1; indx++)
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
			for(indx = 0; indx <= SendBuff[4] -1; indx++)
			{
				tmpStr = tmpStr +  Convert.ToChar(RecvBuff[indx]);
			}

			tData.Text = tmpStr;
		}

		private void bWriteProt_Click(object sender, System.EventArgs e)
		{
			int indx;
			
			// 1. Check for all input fields
			if(this.InputOK(3) == false)
			{
				return;
			}

			// 2. Read input fields and pass data to card
			tmpStr = tData.Text;
			ClearBuffers();
			
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0xD1;
			SendBuff[2] = 0x00;
			SendBuff[3] = Convert.ToByte(tAdd.Text.Substring(0, 2),16);
			SendBuff[4] = Convert.ToByte(tLen.Text.Substring(0, 2),16);
			
			for(indx = 0; indx <= tmpStr.Length -1; indx++)
			{
				if (Convert.ToByte(Convert.ToByte(tmpStr[indx])) != 0x00 )
				{	
					SendBuff[indx + 5] =  Convert.ToByte(tmpStr[indx]);  
				}
			}

			SendBuffLen = SendBuff[4] + 5;
			RecvBuffLen = 2;
			tmpStr = "";
			
			for(indx = 0; indx <= SendBuffLen -1; indx++)
			{
				tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
			}

			retcode = this.SendAPDUandDisplay(2, tmpStr);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			tData.Text = "";
		}

		private void bSubmit_Click(object sender, System.EventArgs e)
		{
			int indx;
			
			// 1. Check for all input fields
			if(this.InputOK(2) == false)
			{
				return;
			}

			// 2. Read input fields and pass data to card
			tmpStr = "";
			
			for(indx = 0; indx <= tData.Text.Length-1; indx++)
			{
				if (tData.Text.Substring(indx, 1) != " ")
				{
					tmpStr = tmpStr + tData.Text.Substring(indx, 1);
				}
			}
			
			ClearBuffers();
			
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0x20;
			SendBuff[2] = 0x00;
			SendBuff[3] = 0x00;
			SendBuff[4] = 0x03;
			
			for(indx = 0; indx <= 2; indx++)
			{
				SendBuff[indx + 5] = Convert.ToByte(tmpStr.Substring(indx * 2, 2), 16);
				
			}
			SendBuffLen = SendBuff[4] + 5;
			RecvBuffLen = 6;
			tmpStr = "";
			
			for(indx = 0; indx <= SendBuffLen -1; indx++)
			{
				tmpStr = tmpStr + string.Format("{0:x2}", SendBuff[indx]).ToUpper() + " ";
			}

			retcode = this.SendAPDUandDisplay(2, tmpStr);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			tData.Text = "";
		}

		private void bWrite_Click(object sender, System.EventArgs e)
		{
			int indx;
			
			// 1. Check for all input fields
			if(this.InputOK(1) == false)
			{
				return;
			}

			// 2. Read input fields and pass data to card
			tmpStr = tData.Text;
			ClearBuffers();
			
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0xD0;
			SendBuff[2] = 0x00;
			SendBuff[3] = Convert.ToByte(tAdd.Text.Substring(0, 2),16);
			SendBuff[4] = Convert.ToByte(tLen.Text.Substring(0, 2),16);
			
			for(indx = 0; indx <= tmpStr.Length -1; indx++)
			{
				if (Convert.ToByte(Convert.ToByte(tmpStr[indx])) != 0x00 )
				{	
					SendBuff[indx + 5] =  Convert.ToByte(tmpStr[indx]);  
				}
			}

			SendBuffLen = SendBuff[4] + 5;
			RecvBuffLen = 2;
			tmpStr = "";
			
			for(indx = 0; indx <= SendBuffLen -1; indx++)
			{
				tmpStr = tmpStr + string.Format("{0:x2}", (SendBuff[indx])).ToUpper() + " ";
			}

			retcode = this.SendAPDUandDisplay(0, tmpStr);
			
			if(retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				return;
			}

			tData.Text = "";
		}

		private void bChange_Click(object sender, System.EventArgs e)
		{	
			int indx;
			
			// 1. Check for all input fields
			if(this.InputOK(2) == false)
			{
				return;
			}
			
			// 2. Read input fields and pass data to card
			tmpStr = "";
			
			for(indx = 0; indx <= tData.Text.Length-1; indx++)
			{
				if (tData.Text.Substring(indx, 1) != " ")
				{
					tmpStr = tmpStr + tData.Text.Substring(indx, 1);
				}
			}
			ClearBuffers();
			
			SendBuff[0] = 0xFF;
			SendBuff[1] = 0xD2;
			SendBuff[2] = 0x00;
			SendBuff[3] = 0x01;
			SendBuff[4] = 0x03;
			
			for(indx = 0; indx <= 2; indx++)
			{
				SendBuff[indx + 5] = Convert.ToByte(tmpStr.Substring(indx * 2, 2), 16);
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

		private void tAdd_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//Verify Input
			//Character 0-9 and A-F

			if (e.KeyChar < 97 || e.KeyChar  > 102)
				if (e.KeyChar < 48 || e.KeyChar  > 57)
					if (e.KeyChar < 65 || e.KeyChar > 70)
						if (e.KeyChar != (char)(Keys.Back))
							e.Handled = true;
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

		private void tAdd_TextChanged(object sender, System.EventArgs e)
		{
			tAdd.Text = tAdd.Text.ToUpper();
			if (tAdd.Text.Length > 1)
			{
				tLen.Focus();
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

		private void SLE4432_Click(object sender, System.EventArgs e)
		{
			fFunction.Enabled = false;
			ClearFields();
			
			if (ConnActive == true) 
			{
				retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
				ConnActive = false;
			}
            
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			ConnActive = false;
        
			bSubmit.Enabled = false;
			bChange.Enabled = false;
			bErrCtr.Enabled = false;
		}

		private void SLE4442_Click(object sender, System.EventArgs e)
		{
			fFunction.Enabled = false;
			ClearFields();
			
			if (ConnActive == true) 
			{
				retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
				ConnActive = false;
			}
            
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			ConnActive = false;
        
			bSubmit.Enabled = true;
			bErrCtr.Enabled = true;
			bChange.Enabled = true;
		}
	}
}
