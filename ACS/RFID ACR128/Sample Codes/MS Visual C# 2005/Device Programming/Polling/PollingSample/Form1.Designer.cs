namespace PollingSample
{
    partial class PollingSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PollingSample));
            this.label1 = new System.Windows.Forms.Label();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.bInit = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.gbExMode = new System.Windows.Forms.GroupBox();
            this.optEither = new System.Windows.Forms.RadioButton();
            this.optBoth = new System.Windows.Forms.RadioButton();
            this.gbCurrMode = new System.Windows.Forms.GroupBox();
            this.optExActive = new System.Windows.Forms.RadioButton();
            this.optExNotActive = new System.Windows.Forms.RadioButton();
            this.bGetExMode = new System.Windows.Forms.Button();
            this.bSetExMode = new System.Windows.Forms.Button();
            this.gbPollOpt = new System.Windows.Forms.GroupBox();
            this.cbPollOpt6 = new System.Windows.Forms.CheckBox();
            this.cbPollOpt5 = new System.Windows.Forms.CheckBox();
            this.cbPollOpt4 = new System.Windows.Forms.CheckBox();
            this.cbPollOpt3 = new System.Windows.Forms.CheckBox();
            this.cbPollOpt2 = new System.Windows.Forms.CheckBox();
            this.cbPollOpt1 = new System.Windows.Forms.CheckBox();
            this.gbPICCInt = new System.Windows.Forms.GroupBox();
            this.opt25 = new System.Windows.Forms.RadioButton();
            this.opt1 = new System.Windows.Forms.RadioButton();
            this.opt500 = new System.Windows.Forms.RadioButton();
            this.opt250 = new System.Windows.Forms.RadioButton();
            this.bReadPollOpt = new System.Windows.Forms.Button();
            this.bSetPollOpt = new System.Windows.Forms.Button();
            this.bManPoll = new System.Windows.Forms.Button();
            this.bAutoPoll = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsMsg1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMsg2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMsg3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMsg4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mMsg = new System.Windows.Forms.ListBox();
            this.bClear = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bQuit = new System.Windows.Forms.Button();
            this.PollTimer = new System.Windows.Forms.Timer(this.components);
            this.gbExMode.SuspendLayout();
            this.gbCurrMode.SuspendLayout();
            this.gbPollOpt.SuspendLayout();
            this.gbPICCInt.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Reader";
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 19);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(175, 21);
            this.cbReader.TabIndex = 1;
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(15, 46);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(125, 23);
            this.bInit.TabIndex = 2;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(146, 46);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(122, 23);
            this.bConnect.TabIndex = 3;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // gbExMode
            // 
            this.gbExMode.Controls.Add(this.optEither);
            this.gbExMode.Controls.Add(this.optBoth);
            this.gbExMode.Location = new System.Drawing.Point(15, 75);
            this.gbExMode.Name = "gbExMode";
            this.gbExMode.Size = new System.Drawing.Size(253, 71);
            this.gbExMode.TabIndex = 4;
            this.gbExMode.TabStop = false;
            this.gbExMode.Text = "Configuration Setting";
            // 
            // optEither
            // 
            this.optEither.AutoSize = true;
            this.optEither.Location = new System.Drawing.Point(14, 46);
            this.optEither.Name = "optEither";
            this.optEither.Size = new System.Drawing.Size(194, 17);
            this.optEither.TabIndex = 1;
            this.optEither.TabStop = true;
            this.optEither.Text = "Either ICC or PICC can be activated";
            this.optEither.UseVisualStyleBackColor = true;
            // 
            // optBoth
            // 
            this.optBoth.AutoSize = true;
            this.optBoth.Location = new System.Drawing.Point(14, 23);
            this.optBoth.Name = "optBoth";
            this.optBoth.Size = new System.Drawing.Size(229, 17);
            this.optBoth.TabIndex = 0;
            this.optBoth.TabStop = true;
            this.optBoth.Text = "Both ICC_PICC interfaces can be activated";
            this.optBoth.UseVisualStyleBackColor = true;
            // 
            // gbCurrMode
            // 
            this.gbCurrMode.Controls.Add(this.optExActive);
            this.gbCurrMode.Controls.Add(this.optExNotActive);
            this.gbCurrMode.Location = new System.Drawing.Point(15, 161);
            this.gbCurrMode.Name = "gbCurrMode";
            this.gbCurrMode.Size = new System.Drawing.Size(253, 78);
            this.gbCurrMode.TabIndex = 5;
            this.gbCurrMode.TabStop = false;
            this.gbCurrMode.Text = "Current Mode";
            // 
            // optExActive
            // 
            this.optExActive.AutoSize = true;
            this.optExActive.Location = new System.Drawing.Point(11, 47);
            this.optExActive.Name = "optExActive";
            this.optExActive.Size = new System.Drawing.Size(142, 17);
            this.optExActive.TabIndex = 1;
            this.optExActive.TabStop = true;
            this.optExActive.Text = "Exclusive Mode is active";
            this.optExActive.UseVisualStyleBackColor = true;
            // 
            // optExNotActive
            // 
            this.optExNotActive.AutoSize = true;
            this.optExNotActive.Location = new System.Drawing.Point(11, 24);
            this.optExNotActive.Name = "optExNotActive";
            this.optExNotActive.Size = new System.Drawing.Size(160, 17);
            this.optExNotActive.TabIndex = 0;
            this.optExNotActive.TabStop = true;
            this.optExNotActive.Text = "Exclusive Mode is not active";
            this.optExNotActive.UseVisualStyleBackColor = true;
            // 
            // bGetExMode
            // 
            this.bGetExMode.Location = new System.Drawing.Point(15, 247);
            this.bGetExMode.Name = "bGetExMode";
            this.bGetExMode.Size = new System.Drawing.Size(123, 23);
            this.bGetExMode.TabIndex = 6;
            this.bGetExMode.Text = "Read Current Mode";
            this.bGetExMode.UseVisualStyleBackColor = true;
            this.bGetExMode.Click += new System.EventHandler(this.bGetExMode_Click);
            // 
            // bSetExMode
            // 
            this.bSetExMode.Location = new System.Drawing.Point(141, 247);
            this.bSetExMode.Name = "bSetExMode";
            this.bSetExMode.Size = new System.Drawing.Size(127, 23);
            this.bSetExMode.TabIndex = 7;
            this.bSetExMode.Text = "Set Mode Configuration";
            this.bSetExMode.UseVisualStyleBackColor = true;
            this.bSetExMode.Click += new System.EventHandler(this.bSetExMode_Click);
            // 
            // gbPollOpt
            // 
            this.gbPollOpt.Controls.Add(this.cbPollOpt6);
            this.gbPollOpt.Controls.Add(this.cbPollOpt5);
            this.gbPollOpt.Controls.Add(this.cbPollOpt4);
            this.gbPollOpt.Controls.Add(this.cbPollOpt3);
            this.gbPollOpt.Controls.Add(this.cbPollOpt2);
            this.gbPollOpt.Controls.Add(this.cbPollOpt1);
            this.gbPollOpt.Location = new System.Drawing.Point(17, 276);
            this.gbPollOpt.Name = "gbPollOpt";
            this.gbPollOpt.Size = new System.Drawing.Size(251, 168);
            this.gbPollOpt.TabIndex = 8;
            this.gbPollOpt.TabStop = false;
            this.gbPollOpt.Text = "Contactless Polling Options";
            // 
            // cbPollOpt6
            // 
            this.cbPollOpt6.AutoSize = true;
            this.cbPollOpt6.Location = new System.Drawing.Point(11, 134);
            this.cbPollOpt6.Name = "cbPollOpt6";
            this.cbPollOpt6.Size = new System.Drawing.Size(152, 17);
            this.cbPollOpt6.TabIndex = 5;
            this.cbPollOpt6.Text = "Enforce ISO 14443A Part4";
            this.cbPollOpt6.UseVisualStyleBackColor = true;
            // 
            // cbPollOpt5
            // 
            this.cbPollOpt5.AutoSize = true;
            this.cbPollOpt5.Location = new System.Drawing.Point(12, 111);
            this.cbPollOpt5.Name = "cbPollOpt5";
            this.cbPollOpt5.Size = new System.Drawing.Size(77, 17);
            this.cbPollOpt5.TabIndex = 4;
            this.cbPollOpt5.Text = "Test Mode";
            this.cbPollOpt5.UseVisualStyleBackColor = true;
            // 
            // cbPollOpt4
            // 
            this.cbPollOpt4.AutoSize = true;
            this.cbPollOpt4.Location = new System.Drawing.Point(12, 88);
            this.cbPollOpt4.Name = "cbPollOpt4";
            this.cbPollOpt4.Size = new System.Drawing.Size(184, 17);
            this.cbPollOpt4.TabIndex = 3;
            this.cbPollOpt4.Text = "Activate the PICC when detected";
            this.cbPollOpt4.UseVisualStyleBackColor = true;
            // 
            // cbPollOpt3
            // 
            this.cbPollOpt3.AutoSize = true;
            this.cbPollOpt3.Location = new System.Drawing.Point(11, 65);
            this.cbPollOpt3.Name = "cbPollOpt3";
            this.cbPollOpt3.Size = new System.Drawing.Size(212, 17);
            this.cbPollOpt3.TabIndex = 2;
            this.cbPollOpt3.Text = "Turn off antenna field if PICC is inactive";
            this.cbPollOpt3.UseVisualStyleBackColor = true;
            // 
            // cbPollOpt2
            // 
            this.cbPollOpt2.AutoSize = true;
            this.cbPollOpt2.Location = new System.Drawing.Point(11, 42);
            this.cbPollOpt2.Name = "cbPollOpt2";
            this.cbPollOpt2.Size = new System.Drawing.Size(237, 17);
            this.cbPollOpt2.TabIndex = 1;
            this.cbPollOpt2.Text = "Turn off antenna field if no PICC within range";
            this.cbPollOpt2.UseVisualStyleBackColor = true;
            // 
            // cbPollOpt1
            // 
            this.cbPollOpt1.AutoSize = true;
            this.cbPollOpt1.Location = new System.Drawing.Point(11, 19);
            this.cbPollOpt1.Name = "cbPollOpt1";
            this.cbPollOpt1.Size = new System.Drawing.Size(134, 17);
            this.cbPollOpt1.TabIndex = 0;
            this.cbPollOpt1.Text = "Automatic PICC Polling";
            this.cbPollOpt1.UseVisualStyleBackColor = true;
            // 
            // gbPICCInt
            // 
            this.gbPICCInt.Controls.Add(this.opt25);
            this.gbPICCInt.Controls.Add(this.opt1);
            this.gbPICCInt.Controls.Add(this.opt500);
            this.gbPICCInt.Controls.Add(this.opt250);
            this.gbPICCInt.Location = new System.Drawing.Point(19, 450);
            this.gbPICCInt.Name = "gbPICCInt";
            this.gbPICCInt.Size = new System.Drawing.Size(119, 114);
            this.gbPICCInt.TabIndex = 9;
            this.gbPICCInt.TabStop = false;
            this.gbPICCInt.Text = "Poll Interval";
            // 
            // opt25
            // 
            this.opt25.AutoSize = true;
            this.opt25.Location = new System.Drawing.Point(9, 88);
            this.opt25.Name = "opt25";
            this.opt25.Size = new System.Drawing.Size(60, 17);
            this.opt25.TabIndex = 3;
            this.opt25.TabStop = true;
            this.opt25.Text = "2.5 sec";
            this.opt25.UseVisualStyleBackColor = true;
            // 
            // opt1
            // 
            this.opt1.AutoSize = true;
            this.opt1.Location = new System.Drawing.Point(10, 65);
            this.opt1.Name = "opt1";
            this.opt1.Size = new System.Drawing.Size(51, 17);
            this.opt1.TabIndex = 2;
            this.opt1.TabStop = true;
            this.opt1.Text = "1 sec";
            this.opt1.UseVisualStyleBackColor = true;
            // 
            // opt500
            // 
            this.opt500.AutoSize = true;
            this.opt500.Location = new System.Drawing.Point(10, 45);
            this.opt500.Name = "opt500";
            this.opt500.Size = new System.Drawing.Size(71, 17);
            this.opt500.TabIndex = 1;
            this.opt500.TabStop = true;
            this.opt500.Text = "500 msec";
            this.opt500.UseVisualStyleBackColor = true;
            // 
            // opt250
            // 
            this.opt250.AutoSize = true;
            this.opt250.Location = new System.Drawing.Point(10, 22);
            this.opt250.Name = "opt250";
            this.opt250.Size = new System.Drawing.Size(71, 17);
            this.opt250.TabIndex = 0;
            this.opt250.TabStop = true;
            this.opt250.Text = "250 msec";
            this.opt250.UseVisualStyleBackColor = true;
            // 
            // bReadPollOpt
            // 
            this.bReadPollOpt.Location = new System.Drawing.Point(154, 465);
            this.bReadPollOpt.Name = "bReadPollOpt";
            this.bReadPollOpt.Size = new System.Drawing.Size(114, 23);
            this.bReadPollOpt.TabIndex = 10;
            this.bReadPollOpt.Text = "Read Polling Option";
            this.bReadPollOpt.UseVisualStyleBackColor = true;
            this.bReadPollOpt.Click += new System.EventHandler(this.bReadPollOpt_Click);
            // 
            // bSetPollOpt
            // 
            this.bSetPollOpt.Location = new System.Drawing.Point(155, 498);
            this.bSetPollOpt.Name = "bSetPollOpt";
            this.bSetPollOpt.Size = new System.Drawing.Size(113, 23);
            this.bSetPollOpt.TabIndex = 11;
            this.bSetPollOpt.Text = "Set Polling Option";
            this.bSetPollOpt.UseVisualStyleBackColor = true;
            this.bSetPollOpt.Click += new System.EventHandler(this.bSetPollOpt_Click);
            // 
            // bManPoll
            // 
            this.bManPoll.Location = new System.Drawing.Point(19, 583);
            this.bManPoll.Name = "bManPoll";
            this.bManPoll.Size = new System.Drawing.Size(133, 23);
            this.bManPoll.TabIndex = 12;
            this.bManPoll.Text = "Manual Card Detection";
            this.bManPoll.UseVisualStyleBackColor = true;
            this.bManPoll.Click += new System.EventHandler(this.bManPoll_Click);
            // 
            // bAutoPoll
            // 
            this.bAutoPoll.Location = new System.Drawing.Point(154, 583);
            this.bAutoPoll.Name = "bAutoPoll";
            this.bAutoPoll.Size = new System.Drawing.Size(114, 23);
            this.bAutoPoll.TabIndex = 13;
            this.bAutoPoll.Text = "Start Auto Detection";
            this.bAutoPoll.UseVisualStyleBackColor = true;
            this.bAutoPoll.Click += new System.EventHandler(this.bAutoPoll_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMsg1,
            this.tsMsg2,
            this.tsMsg3,
            this.tsMsg4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(661, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsMsg1
            // 
            this.tsMsg1.AutoSize = false;
            this.tsMsg1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg1.Name = "tsMsg1";
            this.tsMsg1.Size = new System.Drawing.Size(150, 17);
            this.tsMsg1.Text = "ICC Reader Status";
            // 
            // tsMsg2
            // 
            this.tsMsg2.AutoSize = false;
            this.tsMsg2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg2.Name = "tsMsg2";
            this.tsMsg2.Size = new System.Drawing.Size(150, 17);
            this.tsMsg2.Text = " ";
            // 
            // tsMsg3
            // 
            this.tsMsg3.AutoSize = false;
            this.tsMsg3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg3.Name = "tsMsg3";
            this.tsMsg3.Size = new System.Drawing.Size(150, 17);
            this.tsMsg3.Text = "PICC Reader Status";
            // 
            // tsMsg4
            // 
            this.tsMsg4.AutoSize = false;
            this.tsMsg4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg4.Name = "tsMsg4";
            this.tsMsg4.Size = new System.Drawing.Size(150, 17);
            this.tsMsg4.Text = " ";
            // 
            // mMsg
            // 
            this.mMsg.FormattingEnabled = true;
            this.mMsg.Location = new System.Drawing.Point(286, 19);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(352, 537);
            this.mMsg.TabIndex = 15;
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(286, 583);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(114, 23);
            this.bClear.TabIndex = 16;
            this.bClear.Text = "Clear Screen";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(406, 583);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(116, 23);
            this.bReset.TabIndex = 17;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(528, 583);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(110, 23);
            this.bQuit.TabIndex = 18;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // PollTimer
            // 
            this.PollTimer.Enabled = true;
            this.PollTimer.Tick += new System.EventHandler(this.PollTimer_Tick);
            // 
            // PollingSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 658);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.bAutoPoll);
            this.Controls.Add(this.bManPoll);
            this.Controls.Add(this.bSetPollOpt);
            this.Controls.Add(this.bReadPollOpt);
            this.Controls.Add(this.gbPICCInt);
            this.Controls.Add(this.gbPollOpt);
            this.Controls.Add(this.bSetExMode);
            this.Controls.Add(this.bGetExMode);
            this.Controls.Add(this.gbCurrMode);
            this.Controls.Add(this.gbExMode);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PollingSample";
            this.Text = "Polling Sample";
            this.Load += new System.EventHandler(this.PollingSample_Load);
            this.gbExMode.ResumeLayout(false);
            this.gbExMode.PerformLayout();
            this.gbCurrMode.ResumeLayout(false);
            this.gbCurrMode.PerformLayout();
            this.gbPollOpt.ResumeLayout(false);
            this.gbPollOpt.PerformLayout();
            this.gbPICCInt.ResumeLayout(false);
            this.gbPICCInt.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReader;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.GroupBox gbExMode;
        private System.Windows.Forms.RadioButton optEither;
        private System.Windows.Forms.RadioButton optBoth;
        private System.Windows.Forms.GroupBox gbCurrMode;
        private System.Windows.Forms.RadioButton optExActive;
        private System.Windows.Forms.RadioButton optExNotActive;
        private System.Windows.Forms.Button bGetExMode;
        private System.Windows.Forms.Button bSetExMode;
        private System.Windows.Forms.GroupBox gbPollOpt;
        private System.Windows.Forms.CheckBox cbPollOpt6;
        private System.Windows.Forms.CheckBox cbPollOpt5;
        private System.Windows.Forms.CheckBox cbPollOpt4;
        private System.Windows.Forms.CheckBox cbPollOpt3;
        private System.Windows.Forms.CheckBox cbPollOpt2;
        private System.Windows.Forms.CheckBox cbPollOpt1;
        private System.Windows.Forms.GroupBox gbPICCInt;
        private System.Windows.Forms.RadioButton opt25;
        private System.Windows.Forms.RadioButton opt1;
        private System.Windows.Forms.RadioButton opt500;
        private System.Windows.Forms.RadioButton opt250;
        private System.Windows.Forms.Button bReadPollOpt;
        private System.Windows.Forms.Button bSetPollOpt;
        private System.Windows.Forms.Button bManPoll;
        private System.Windows.Forms.Button bAutoPoll;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsMsg1;
        private System.Windows.Forms.ToolStripStatusLabel tsMsg2;
        private System.Windows.Forms.ToolStripStatusLabel tsMsg3;
        private System.Windows.Forms.ToolStripStatusLabel tsMsg4;
        private System.Windows.Forms.ListBox mMsg;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Button bQuit;
        private System.Windows.Forms.Timer PollTimer;
    }
}

