namespace ACOSBinary
{
    partial class MainACOSBin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainACOSBin));
            this.gbReadWrite = new System.Windows.Forms.GroupBox();
            this.tLen = new System.Windows.Forms.TextBox();
            this.tOffset2 = new System.Windows.Forms.TextBox();
            this.tOffset1 = new System.Windows.Forms.TextBox();
            this.tFID2 = new System.Windows.Forms.TextBox();
            this.tFID1 = new System.Windows.Forms.TextBox();
            this.bBinWrite = new System.Windows.Forms.Button();
            this.bBinRead = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.tData = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.mMsg = new System.Windows.Forms.ListBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.tFileID1 = new System.Windows.Forms.TextBox();
            this.tFileID2 = new System.Windows.Forms.TextBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.gbFormat = new System.Windows.Forms.GroupBox();
            this.bFormat = new System.Windows.Forms.Button();
            this.tFileLen2 = new System.Windows.Forms.TextBox();
            this.tFileLen1 = new System.Windows.Forms.TextBox();
            this.bQuit = new System.Windows.Forms.Button();
            this.gbReadWrite.SuspendLayout();
            this.gbFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbReadWrite
            // 
            this.gbReadWrite.Controls.Add(this.tLen);
            this.gbReadWrite.Controls.Add(this.tOffset2);
            this.gbReadWrite.Controls.Add(this.tOffset1);
            this.gbReadWrite.Controls.Add(this.tFID2);
            this.gbReadWrite.Controls.Add(this.tFID1);
            this.gbReadWrite.Controls.Add(this.bBinWrite);
            this.gbReadWrite.Controls.Add(this.bBinRead);
            this.gbReadWrite.Controls.Add(this.Label7);
            this.gbReadWrite.Controls.Add(this.tData);
            this.gbReadWrite.Controls.Add(this.Label6);
            this.gbReadWrite.Controls.Add(this.Label5);
            this.gbReadWrite.Controls.Add(this.Label4);
            this.gbReadWrite.Location = new System.Drawing.Point(16, 207);
            this.gbReadWrite.Name = "gbReadWrite";
            this.gbReadWrite.Size = new System.Drawing.Size(266, 267);
            this.gbReadWrite.TabIndex = 15;
            this.gbReadWrite.TabStop = false;
            this.gbReadWrite.Text = "Read and Write to Binary File";
            // 
            // tLen
            // 
            this.tLen.Location = new System.Drawing.Point(227, 59);
            this.tLen.MaxLength = 2;
            this.tLen.Name = "tLen";
            this.tLen.Size = new System.Drawing.Size(33, 20);
            this.tLen.TabIndex = 23;
            // 
            // tOffset2
            // 
            this.tOffset2.Location = new System.Drawing.Point(118, 59);
            this.tOffset2.MaxLength = 2;
            this.tOffset2.Name = "tOffset2";
            this.tOffset2.Size = new System.Drawing.Size(33, 20);
            this.tOffset2.TabIndex = 22;
            // 
            // tOffset1
            // 
            this.tOffset1.Location = new System.Drawing.Point(78, 59);
            this.tOffset1.MaxLength = 2;
            this.tOffset1.Name = "tOffset1";
            this.tOffset1.Size = new System.Drawing.Size(33, 20);
            this.tOffset1.TabIndex = 21;
            // 
            // tFID2
            // 
            this.tFID2.Location = new System.Drawing.Point(118, 33);
            this.tFID2.MaxLength = 2;
            this.tFID2.Name = "tFID2";
            this.tFID2.Size = new System.Drawing.Size(33, 20);
            this.tFID2.TabIndex = 20;
            // 
            // tFID1
            // 
            this.tFID1.Location = new System.Drawing.Point(78, 33);
            this.tFID1.MaxLength = 2;
            this.tFID1.Name = "tFID1";
            this.tFID1.Size = new System.Drawing.Size(33, 20);
            this.tFID1.TabIndex = 19;
            // 
            // bBinWrite
            // 
            this.bBinWrite.Location = new System.Drawing.Point(150, 233);
            this.bBinWrite.Name = "bBinWrite";
            this.bBinWrite.Size = new System.Drawing.Size(104, 23);
            this.bBinWrite.TabIndex = 18;
            this.bBinWrite.Text = "Write Binary";
            this.bBinWrite.UseVisualStyleBackColor = true;
            this.bBinWrite.Click += new System.EventHandler(this.bBinWrite_Click);
            // 
            // bBinRead
            // 
            this.bBinRead.Location = new System.Drawing.Point(150, 204);
            this.bBinRead.Name = "bBinRead";
            this.bBinRead.Size = new System.Drawing.Size(104, 23);
            this.bBinRead.TabIndex = 17;
            this.bBinRead.Text = "Read Binary";
            this.bBinRead.UseVisualStyleBackColor = true;
            this.bBinRead.Click += new System.EventHandler(this.bBinRead_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(18, 91);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(30, 13);
            this.Label7.TabIndex = 16;
            this.Label7.Text = "Data";
            // 
            // tData
            // 
            this.tData.Location = new System.Drawing.Point(21, 107);
            this.tData.Multiline = true;
            this.tData.Name = "tData";
            this.tData.Size = new System.Drawing.Size(233, 91);
            this.tData.TabIndex = 15;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(18, 62);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(54, 13);
            this.Label6.TabIndex = 8;
            this.Label6.Text = "File Offset";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(181, 62);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(40, 13);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "Length";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(20, 33);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(37, 13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "File ID";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "Select Reader";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(20, 29);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(37, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "File ID";
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(404, 451);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(101, 23);
            this.bReset.TabIndex = 18;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(297, 451);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(101, 23);
            this.bClear.TabIndex = 17;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // mMsg
            // 
            this.mMsg.FormattingEnabled = true;
            this.mMsg.Location = new System.Drawing.Point(297, 15);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(325, 420);
            this.mMsg.TabIndex = 16;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(20, 53);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(40, 13);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Length";
            // 
            // tFileID1
            // 
            this.tFileID1.Location = new System.Drawing.Point(78, 26);
            this.tFileID1.MaxLength = 2;
            this.tFileID1.Name = "tFileID1";
            this.tFileID1.Size = new System.Drawing.Size(33, 20);
            this.tFileID1.TabIndex = 2;
            // 
            // tFileID2
            // 
            this.tFileID2.Location = new System.Drawing.Point(118, 26);
            this.tFileID2.MaxLength = 2;
            this.tFileID2.Name = "tFileID2";
            this.tFileID2.Size = new System.Drawing.Size(33, 20);
            this.tFileID2.TabIndex = 3;
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(162, 71);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(120, 23);
            this.bConnect.TabIndex = 13;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(162, 42);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(120, 23);
            this.bInit.TabIndex = 12;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(94, 15);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(188, 21);
            this.cbReader.TabIndex = 11;
            // 
            // gbFormat
            // 
            this.gbFormat.Controls.Add(this.bFormat);
            this.gbFormat.Controls.Add(this.tFileLen2);
            this.gbFormat.Controls.Add(this.tFileLen1);
            this.gbFormat.Controls.Add(this.tFileID2);
            this.gbFormat.Controls.Add(this.tFileID1);
            this.gbFormat.Controls.Add(this.Label3);
            this.gbFormat.Controls.Add(this.Label2);
            this.gbFormat.Location = new System.Drawing.Point(16, 100);
            this.gbFormat.Name = "gbFormat";
            this.gbFormat.Size = new System.Drawing.Size(266, 97);
            this.gbFormat.TabIndex = 14;
            this.gbFormat.TabStop = false;
            this.gbFormat.Text = "Card Fornat Routine";
            // 
            // bFormat
            // 
            this.bFormat.Location = new System.Drawing.Point(156, 62);
            this.bFormat.Name = "bFormat";
            this.bFormat.Size = new System.Drawing.Size(104, 23);
            this.bFormat.TabIndex = 6;
            this.bFormat.Text = "Format Card";
            this.bFormat.UseVisualStyleBackColor = true;
            this.bFormat.Click += new System.EventHandler(this.bFormat_Click);
            // 
            // tFileLen2
            // 
            this.tFileLen2.Location = new System.Drawing.Point(118, 50);
            this.tFileLen2.MaxLength = 2;
            this.tFileLen2.Name = "tFileLen2";
            this.tFileLen2.Size = new System.Drawing.Size(33, 20);
            this.tFileLen2.TabIndex = 5;
            // 
            // tFileLen1
            // 
            this.tFileLen1.Location = new System.Drawing.Point(78, 50);
            this.tFileLen1.MaxLength = 2;
            this.tFileLen1.Name = "tFileLen1";
            this.tFileLen1.Size = new System.Drawing.Size(33, 20);
            this.tFileLen1.TabIndex = 4;
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(511, 451);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(111, 23);
            this.bQuit.TabIndex = 19;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // MainACOSBin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 482);
            this.Controls.Add(this.gbReadWrite);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.gbFormat);
            this.Controls.Add(this.bQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainACOSBin";
            this.Text = "Using Binary Files in ACOS3";
            this.Load += new System.EventHandler(this.MainACOSBin_Load);
            this.gbReadWrite.ResumeLayout(false);
            this.gbReadWrite.PerformLayout();
            this.gbFormat.ResumeLayout(false);
            this.gbFormat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbReadWrite;
        internal System.Windows.Forms.Button bBinWrite;
        internal System.Windows.Forms.Button bBinRead;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox tData;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.ListBox mMsg;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox tFileID1;
        internal System.Windows.Forms.TextBox tFileID2;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.GroupBox gbFormat;
        internal System.Windows.Forms.Button bFormat;
        internal System.Windows.Forms.TextBox tFileLen2;
        internal System.Windows.Forms.TextBox tFileLen1;
        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.TextBox tLen;
        internal System.Windows.Forms.TextBox tOffset2;
        internal System.Windows.Forms.TextBox tOffset1;
        internal System.Windows.Forms.TextBox tFID2;
        internal System.Windows.Forms.TextBox tFID1;
    }
}

