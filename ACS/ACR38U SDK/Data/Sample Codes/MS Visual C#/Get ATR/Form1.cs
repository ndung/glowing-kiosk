/***************************************************************************
' DESCRIPTION :  This sample program demonstrate displaying the ATR value 
'                using PC/SC.  
'****************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Get_ATR
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	
	
	public class Form1 : System.Windows.Forms.Form
	{
		// global declaration
		int hContext, hCard; //card reader context handle //card connection handle
		int ActiveProtocol, retcode; int Aprotocol;  
		byte[] rdrlist = new byte [100];
		string rreader;
		int ATRLen, ReaderLen,dwState, Protocol;  
		uint PRT,ReturnCode; 
		byte[] ATR = new byte[33];
		short index; 
		string Temp;

		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Button btnDisconnect;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnReader;
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
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(16, 40);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 24);
            this.btnConnect.TabIndex = 45;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstOutput);
            this.groupBox2.Location = new System.Drawing.Point(144, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 184);
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
            this.lstOutput.Size = new System.Drawing.Size(352, 160);
            this.lstOutput.TabIndex = 24;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(16, 104);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(120, 24);
            this.btnDisconnect.TabIndex = 46;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 136);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 24);
            this.btnClear.TabIndex = 47;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(16, 168);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 24);
            this.btnExit.TabIndex = 48;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(16, 72);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 24);
            this.btnReset.TabIndex = 50;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnReader
            // 
            this.btnReader.Location = new System.Drawing.Point(16, 8);
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(120, 23);
            this.btnReader.TabIndex = 49;
            this.btnReader.Text = "Reader";
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 197);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnReader);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get ATR";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
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
				
			lstOutput.SelectedIndex = lstOutput.Items.Count -1;
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			//terminate the application
			retcode = ModWinsCard.SCardReleaseContext(hContext);
			retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
			Application.Exit();
		}
	}
}
