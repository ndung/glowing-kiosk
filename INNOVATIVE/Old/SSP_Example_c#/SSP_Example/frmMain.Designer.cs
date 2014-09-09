namespace SSPDllExample
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSerialPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHostSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bttnPlay = new System.Windows.Forms.Button();
            this.txtGamePrice = new System.Windows.Forms.TextBox();
            this.txtLastCredit = new System.Windows.Forms.TextBox();
            this.txtMaxCredit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalCredit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.lv1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.bttnHalt = new System.Windows.Forms.Button();
            this.bttnRun = new System.Windows.Forms.Button();
            this.bttnExit = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuSetup,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(642, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.ShortcutKeyDisplayString = "";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.mnuExit.ShowShortcutKeys = false;
            this.mnuExit.Size = new System.Drawing.Size(85, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuSetup
            // 
            this.mnuSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSerialPort,
            this.toolStripSeparator1,
            this.mnuHostSystem});
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(47, 20);
            this.mnuSetup.Text = "&Setup";
            // 
            // mnuSerialPort
            // 
            this.mnuSerialPort.Name = "mnuSerialPort";
            this.mnuSerialPort.Size = new System.Drawing.Size(145, 22);
            this.mnuSerialPort.Text = "Serial &Port";
            this.mnuSerialPort.Click += new System.EventHandler(this.mnuSerialPort_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuHostSystem
            // 
            this.mnuHostSystem.Name = "mnuHostSystem";
            this.mnuHostSystem.Size = new System.Drawing.Size(145, 22);
            this.mnuHostSystem.Text = "&Host System";
            this.mnuHostSystem.Click += new System.EventHandler(this.mnuHostSystem_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(40, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(114, 22);
            this.mnuHelpAbout.Text = "&About";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bttnPlay);
            this.groupBox1.Controls.Add(this.txtGamePrice);
            this.groupBox1.Controls.Add(this.txtLastCredit);
            this.groupBox1.Controls.Add(this.txtMaxCredit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTotalCredit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Text1);
            this.groupBox1.Controls.Add(this.lv1);
            this.groupBox1.Controls.Add(this.bttnHalt);
            this.groupBox1.Controls.Add(this.bttnRun);
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 335);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // bttnPlay
            // 
            this.bttnPlay.Location = new System.Drawing.Point(332, 123);
            this.bttnPlay.Name = "bttnPlay";
            this.bttnPlay.Size = new System.Drawing.Size(264, 36);
            this.bttnPlay.TabIndex = 13;
            this.bttnPlay.Text = "&Play Game";
            this.bttnPlay.UseVisualStyleBackColor = true;
            this.bttnPlay.Click += new System.EventHandler(this.bttnPlay_Click);
            // 
            // txtGamePrice
            // 
            this.txtGamePrice.Location = new System.Drawing.Point(554, 66);
            this.txtGamePrice.Name = "txtGamePrice";
            this.txtGamePrice.ReadOnly = true;
            this.txtGamePrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtGamePrice.Size = new System.Drawing.Size(42, 20);
            this.txtGamePrice.TabIndex = 12;
            this.txtGamePrice.Text = "0";
            // 
            // txtLastCredit
            // 
            this.txtLastCredit.Location = new System.Drawing.Point(554, 39);
            this.txtLastCredit.Name = "txtLastCredit";
            this.txtLastCredit.ReadOnly = true;
            this.txtLastCredit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLastCredit.Size = new System.Drawing.Size(42, 20);
            this.txtLastCredit.TabIndex = 11;
            this.txtLastCredit.Text = "0";
            // 
            // txtMaxCredit
            // 
            this.txtMaxCredit.Location = new System.Drawing.Point(554, 12);
            this.txtMaxCredit.Name = "txtMaxCredit";
            this.txtMaxCredit.ReadOnly = true;
            this.txtMaxCredit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMaxCredit.Size = new System.Drawing.Size(42, 20);
            this.txtMaxCredit.TabIndex = 10;
            this.txtMaxCredit.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Game Price";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(460, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Last Credit Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Max Credit Value";
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.BackColor = System.Drawing.Color.Black;
            this.txtTotalCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCredit.ForeColor = System.Drawing.Color.Lime;
            this.txtTotalCredit.Location = new System.Drawing.Point(313, 35);
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.ReadOnly = true;
            this.txtTotalCredit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalCredit.Size = new System.Drawing.Size(115, 31);
            this.txtTotalCredit.TabIndex = 6;
            this.txtTotalCredit.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Current Credit Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Events";
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(22, 308);
            this.Text1.Name = "Text1";
            this.Text1.ReadOnly = true;
            this.Text1.Size = new System.Drawing.Size(313, 20);
            this.Text1.TabIndex = 3;
            // 
            // lv1
            // 
            this.lv1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv1.HideSelection = false;
            this.lv1.Location = new System.Drawing.Point(22, 19);
            this.lv1.Name = "lv1";
            this.lv1.ShowGroups = false;
            this.lv1.Size = new System.Drawing.Size(284, 230);
            this.lv1.TabIndex = 2;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.View = System.Windows.Forms.View.Details;
            this.lv1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "";
            this.columnHeader1.Text = "Parameter";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "dfg";
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            this.columnHeader3.Width = 93;
            // 
            // bttnHalt
            // 
            this.bttnHalt.Location = new System.Drawing.Point(231, 255);
            this.bttnHalt.Name = "bttnHalt";
            this.bttnHalt.Size = new System.Drawing.Size(75, 23);
            this.bttnHalt.TabIndex = 1;
            this.bttnHalt.Text = "Ha&lt";
            this.bttnHalt.UseVisualStyleBackColor = true;
            this.bttnHalt.Click += new System.EventHandler(this.bttnHalt_Click);
            // 
            // bttnRun
            // 
            this.bttnRun.Location = new System.Drawing.Point(22, 255);
            this.bttnRun.Name = "bttnRun";
            this.bttnRun.Size = new System.Drawing.Size(75, 23);
            this.bttnRun.TabIndex = 0;
            this.bttnRun.Text = "&Run";
            this.bttnRun.UseVisualStyleBackColor = true;
            this.bttnRun.Click += new System.EventHandler(this.bttnRun_Click);
            // 
            // bttnExit
            // 
            this.bttnExit.Location = new System.Drawing.Point(22, 368);
            this.bttnExit.Name = "bttnExit";
            this.bttnExit.Size = new System.Drawing.Size(75, 23);
            this.bttnExit.TabIndex = 2;
            this.bttnExit.Text = "E&xit";
            this.bttnExit.UseVisualStyleBackColor = true;
            this.bttnExit.Click += new System.EventHandler(this.bttnExit_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 403);
            this.Controls.Add(this.bttnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MaximumSize = new System.Drawing.Size(650, 430);
            this.MinimumSize = new System.Drawing.Size(650, 430);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Innovative Technology Ltd  SSP DLL Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuSerialPort;
        private System.Windows.Forms.ToolStripMenuItem mnuHostSystem;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bttnHalt;
        private System.Windows.Forms.Button bttnRun;
        private System.Windows.Forms.Button bttnExit;
        private System.Windows.Forms.ListView lv1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Text1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox txtTotalCredit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGamePrice;
        private System.Windows.Forms.TextBox txtLastCredit;
        private System.Windows.Forms.TextBox txtMaxCredit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bttnPlay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

