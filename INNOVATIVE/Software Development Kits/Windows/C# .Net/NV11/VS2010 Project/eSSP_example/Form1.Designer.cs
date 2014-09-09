﻿namespace eSSP_example
{
    partial class Form1
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyNoteFloatToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnHalt = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.StorageListBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackNextNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyStoredNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetTotalsText = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.totalAcceptedNumText = new System.Windows.Forms.TextBox();
            this.payoutBtn = new System.Windows.Forms.Button();
            this.noteToRecycleComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cashboxBtn = new System.Windows.Forms.Button();
            this.resetValidatorBtn = new System.Windows.Forms.Button();
            this.logTickBox = new System.Windows.Forms.CheckBox();
            this.totalNumNotesDispensedText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.notesInStorageText = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.StorageListBoxMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(524, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyNoteFloatToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // emptyNoteFloatToolStripMenuItem1
            // 
            this.emptyNoteFloatToolStripMenuItem1.Name = "emptyNoteFloatToolStripMenuItem1";
            this.emptyNoteFloatToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.emptyNoteFloatToolStripMenuItem1.Text = "Empty Note Float";
            this.emptyNoteFloatToolStripMenuItem1.Click += new System.EventHandler(this.emptyNoteFloatToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 400);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "&Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnHalt
            // 
            this.btnHalt.Location = new System.Drawing.Point(105, 400);
            this.btnHalt.Name = "btnHalt";
            this.btnHalt.Size = new System.Drawing.Size(75, 23);
            this.btnHalt.TabIndex = 2;
            this.btnHalt.Text = "&Halt";
            this.btnHalt.UseVisualStyleBackColor = true;
            this.btnHalt.Click += new System.EventHandler(this.btnHalt_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.ContextMenuStrip = this.StorageListBoxMenu;
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(280, 367);
            this.textBox1.TabIndex = 4;
            // 
            // StorageListBoxMenu
            // 
            this.StorageListBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.stackNextNoteToolStripMenuItem,
            this.emptyStoredNotesToolStripMenuItem});
            this.StorageListBoxMenu.Name = "contextMenuStrip1";
            this.StorageListBoxMenu.Size = new System.Drawing.Size(182, 70);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.testToolStripMenuItem.Text = "Payout Next Note";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // stackNextNoteToolStripMenuItem
            // 
            this.stackNextNoteToolStripMenuItem.Name = "stackNextNoteToolStripMenuItem";
            this.stackNextNoteToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.stackNextNoteToolStripMenuItem.Text = "Stack Next Note";
            this.stackNextNoteToolStripMenuItem.Click += new System.EventHandler(this.stackNextNoteToolStripMenuItem_Click);
            // 
            // emptyStoredNotesToolStripMenuItem
            // 
            this.emptyStoredNotesToolStripMenuItem.Name = "emptyStoredNotesToolStripMenuItem";
            this.emptyStoredNotesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.emptyStoredNotesToolStripMenuItem.Text = "Empty Stored Notes";
            this.emptyStoredNotesToolStripMenuItem.Click += new System.EventHandler(this.emptyStoredNotesToolStripMenuItem_Click);
            // 
            // ResetTotalsText
            // 
            this.ResetTotalsText.Location = new System.Drawing.Point(6, 100);
            this.ResetTotalsText.Name = "ResetTotalsText";
            this.ResetTotalsText.Size = new System.Drawing.Size(187, 23);
            this.ResetTotalsText.TabIndex = 5;
            this.ResetTotalsText.Text = "Reset Stats";
            this.ResetTotalsText.UseVisualStyleBackColor = true;
            this.ResetTotalsText.Click += new System.EventHandler(this.ResetTotalsText_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of Notes Accepted:";
            // 
            // totalAcceptedNumText
            // 
            this.totalAcceptedNumText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.totalAcceptedNumText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAcceptedNumText.Location = new System.Drawing.Point(6, 32);
            this.totalAcceptedNumText.Name = "totalAcceptedNumText";
            this.totalAcceptedNumText.ReadOnly = true;
            this.totalAcceptedNumText.Size = new System.Drawing.Size(188, 22);
            this.totalAcceptedNumText.TabIndex = 7;
            this.totalAcceptedNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // payoutBtn
            // 
            this.payoutBtn.Location = new System.Drawing.Point(308, 366);
            this.payoutBtn.Name = "payoutBtn";
            this.payoutBtn.Size = new System.Drawing.Size(199, 23);
            this.payoutBtn.TabIndex = 8;
            this.payoutBtn.Text = "&Payout Next Note";
            this.payoutBtn.UseVisualStyleBackColor = true;
            this.payoutBtn.Click += new System.EventHandler(this.payoutBtn_Click);
            // 
            // noteToRecycleComboBox
            // 
            this.noteToRecycleComboBox.FormattingEnabled = true;
            this.noteToRecycleComboBox.Location = new System.Drawing.Point(308, 310);
            this.noteToRecycleComboBox.Name = "noteToRecycleComboBox";
            this.noteToRecycleComboBox.Size = new System.Drawing.Size(200, 21);
            this.noteToRecycleComboBox.TabIndex = 9;
            this.noteToRecycleComboBox.SelectedIndexChanged += new System.EventHandler(this.noteToRecycleComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Note to Recycle:";
            // 
            // cashboxBtn
            // 
            this.cashboxBtn.Location = new System.Drawing.Point(308, 336);
            this.cashboxBtn.Name = "cashboxBtn";
            this.cashboxBtn.Size = new System.Drawing.Size(199, 23);
            this.cashboxBtn.TabIndex = 14;
            this.cashboxBtn.Text = "&Next Note to Cashbox";
            this.cashboxBtn.UseVisualStyleBackColor = true;
            this.cashboxBtn.Click += new System.EventHandler(this.cashboxBtn_Click);
            // 
            // resetValidatorBtn
            // 
            this.resetValidatorBtn.Location = new System.Drawing.Point(308, 395);
            this.resetValidatorBtn.Name = "resetValidatorBtn";
            this.resetValidatorBtn.Size = new System.Drawing.Size(199, 23);
            this.resetValidatorBtn.TabIndex = 15;
            this.resetValidatorBtn.Text = "R&eset Validator";
            this.resetValidatorBtn.UseVisualStyleBackColor = true;
            this.resetValidatorBtn.Click += new System.EventHandler(this.resetValidatorBtn_Click);
            // 
            // logTickBox
            // 
            this.logTickBox.AutoSize = true;
            this.logTickBox.Checked = true;
            this.logTickBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logTickBox.Location = new System.Drawing.Point(212, 404);
            this.logTickBox.Name = "logTickBox";
            this.logTickBox.Size = new System.Drawing.Size(81, 17);
            this.logTickBox.TabIndex = 16;
            this.logTickBox.Text = "Comms Log";
            this.logTickBox.UseVisualStyleBackColor = true;
            this.logTickBox.CheckedChanged += new System.EventHandler(this.logTickBox_CheckedChanged);
            // 
            // totalNumNotesDispensedText
            // 
            this.totalNumNotesDispensedText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.totalNumNotesDispensedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalNumNotesDispensedText.Location = new System.Drawing.Point(6, 74);
            this.totalNumNotesDispensedText.Name = "totalNumNotesDispensedText";
            this.totalNumNotesDispensedText.ReadOnly = true;
            this.totalNumNotesDispensedText.Size = new System.Drawing.Size(188, 22);
            this.totalNumNotesDispensedText.TabIndex = 20;
            this.totalNumNotesDispensedText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Number of Notes Dispensed:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.totalNumNotesDispensedText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.totalAcceptedNumText);
            this.groupBox1.Controls.Add(this.ResetTotalsText);
            this.groupBox1.Location = new System.Drawing.Point(308, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 129);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TOTALS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(306, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Notes in Storage:";
            // 
            // notesInStorageText
            // 
            this.notesInStorageText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.notesInStorageText.ContextMenuStrip = this.StorageListBoxMenu;
            this.notesInStorageText.Location = new System.Drawing.Point(308, 46);
            this.notesInStorageText.Multiline = true;
            this.notesInStorageText.Name = "notesInStorageText";
            this.notesInStorageText.ReadOnly = true;
            this.notesInStorageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesInStorageText.Size = new System.Drawing.Size(200, 93);
            this.notesInStorageText.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 435);
            this.ContextMenuStrip = this.StorageListBoxMenu;
            this.Controls.Add(this.logTickBox);
            this.Controls.Add(this.resetValidatorBtn);
            this.Controls.Add(this.notesInStorageText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cashboxBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.noteToRecycleComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHalt);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.payoutBtn);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "NV11 eSSP C# example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StorageListBoxMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnHalt;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ResetTotalsText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox totalAcceptedNumText;
        private System.Windows.Forms.Button payoutBtn;
        private System.Windows.Forms.ComboBox noteToRecycleComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem emptyNoteFloatToolStripMenuItem1;
        private System.Windows.Forms.Button cashboxBtn;
        private System.Windows.Forms.Button resetValidatorBtn;
        private System.Windows.Forms.CheckBox logTickBox;
        private System.Windows.Forms.TextBox totalNumNotesDispensedText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox notesInStorageText;
        private System.Windows.Forms.ContextMenuStrip StorageListBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stackNextNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyStoredNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;

    }
}

