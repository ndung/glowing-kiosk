using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Polling
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 

	public class Form1 : System.Windows.Forms.Form
	{
		// global declaration
		int hContext;		//card reader context handle					
		int retcode;  
		byte[] rdrlist = new byte [100];
		byte[] ATRValue = new byte[37];
		bool CardStatus; 
		string rrname;
		
			
		SCARD_READERSTATE ReaderState = new SCARD_READERSTATE();
		
		private System.Windows.Forms.Panel Panel1;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.GroupBox GroupBox1;
		private System.Windows.Forms.GroupBox GroupBox2;
		private System.Windows.Forms.TextBox TextBox1;
		private System.Windows.Forms.Button btnReader;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReader = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.btnStop);
            this.Panel1.Controls.Add(this.btnStart);
            this.Panel1.Location = new System.Drawing.Point(8, 56);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(224, 40);
            this.Panel1.TabIndex = 8;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(120, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop Polling";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(8, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Polling";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.btnReader);
            this.GroupBox1.Location = new System.Drawing.Point(8, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(224, 48);
            this.GroupBox1.TabIndex = 7;
            this.GroupBox1.TabStop = false;
            // 
            // btnReader
            // 
            this.btnReader.Location = new System.Drawing.Point(72, 16);
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(96, 23);
            this.btnReader.TabIndex = 0;
            this.btnReader.Text = "Reader Name";
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.TextBox1);
            this.GroupBox2.Location = new System.Drawing.Point(8, 104);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(224, 48);
            this.GroupBox2.TabIndex = 9;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Card Status";
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.TextBox1.Location = new System.Drawing.Point(8, 16);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(208, 20);
            this.TextBox1.TabIndex = 5;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(240, 157);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polling";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel1.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
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

		private void timer1_Tick(object sender, System.EventArgs e)
		{

			ReaderState.RdrName =  rrname;
			
			//perform if the card is inserted/not inserted on the reader 
			retcode = ModWinsCard.SCardGetStatusChange(this.hContext, 0, ref ReaderState, 1);

			if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
				TextBox1.Text = "Error!";
			}    
			else
			{   
					
				if((ReaderState.RdrEventState &  32) != 0)  
				{
				   CardStatus = true;				
				}
				else
				{
					CardStatus = false;
				} 
   
			}
        	if (retcode != ModWinsCard.SCARD_S_SUCCESS) 
			{
			}
			else
			{
				if (CardStatus == true) 
				{
					TextBox1.Text = "Card Inserted";
				}
				else
				{
					TextBox1.Text = "No Card Inserted";
				}
			}
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			//set buttons
			timer1.Enabled = true;
			btnStop.Enabled = true;
			btnStart.Enabled = false;
		}

		private void btnReader_Click(object sender, System.EventArgs e)
		{
			//used to split null delimited strings into string arrays
			char[] delimiter = new char[1];
			delimiter[0] = Convert.ToChar(0);

			// using SCardListReaderGroups() to determine reader to use
			string mzGroupList = ""+Convert.ToChar(0);

			int pcchGroups = -1;	//SCARD_AUTOALLOCATE
			
			retcode = ModWinsCard.SCardListReaderGroups(hContext, ref mzGroupList, ref pcchGroups);
			
			if (retcode != ModWinsCard.SCARD_S_SUCCESS)
			{
				TextBox1.Text = "SCardListReaderGroups Error!";
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
				TextBox1.Text = "SCardListReader Error!";
			}
			
			string[] pcReaders = ReaderList.Split(delimiter);
			foreach (string RName in pcReaders)
			{
				TextBox1.Text =  RName;
                rrname = RName;
			}
		}
		private void btnStop_Click(object sender, System.EventArgs e)
		{
			//set buttons
			timer1.Enabled = false;
			btnStart.Enabled = true;
			btnStop.Enabled = false;

			TextBox1.Text = "";
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Established using ScardEstablishedContext()
			retcode = ModWinsCard.SCardEstablishContext(hContext, 0, 0, ref hContext);
			
			if (retcode !=  ModWinsCard.SCARD_S_SUCCESS) 
			{       
				TextBox1.Text = "SCardEstablishContext Error"; 
			}	
		}
	}		
}
/************************************************************************
'DESCRIPTION:            This program demonstrate polling process of the 
'                        card presence in the reader using PC/SC.
'************************************************************************/