namespace DeviceProgramming
{
    partial class DeviceProgramming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceProgramming));
            this.label1 = new System.Windows.Forms.Label();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.bInit = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.bGetFW = new System.Windows.Forms.Button();
            this.gbLED = new System.Windows.Forms.GroupBox();
            this.cbGreen = new System.Windows.Forms.CheckBox();
            this.cbRed = new System.Windows.Forms.CheckBox();
            this.bGetLed = new System.Windows.Forms.Button();
            this.bSetLed = new System.Windows.Forms.Button();
            this.gbBuzzState = new System.Windows.Forms.GroupBox();
            this.bSetBuzzState = new System.Windows.Forms.Button();
            this.bGetBuzzState = new System.Windows.Forms.Button();
            this.cbBuzzLed8 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed7 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed6 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed5 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed4 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed3 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed2 = new System.Windows.Forms.CheckBox();
            this.cbBuzzLed1 = new System.Windows.Forms.CheckBox();
            this.mMsg = new System.Windows.Forms.ListBox();
            this.bClear = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bQuit = new System.Windows.Forms.Button();
            this.gbBuzz = new System.Windows.Forms.GroupBox();
            this.bStopBuzz = new System.Windows.Forms.Button();
            this.bStartBuzz = new System.Windows.Forms.Button();
            this.bSetBuzzDur = new System.Windows.Forms.Button();
            this.tBuzzDur = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.gbLED.SuspendLayout();
            this.gbBuzzState.SuspendLayout();
            this.gbBuzz.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Reader";
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 16);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(189, 21);
            this.cbReader.TabIndex = 1;
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(154, 52);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(125, 23);
            this.bInit.TabIndex = 2;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(154, 81);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(125, 23);
            this.bConnect.TabIndex = 3;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bGetFW
            // 
            this.bGetFW.Location = new System.Drawing.Point(154, 110);
            this.bGetFW.Name = "bGetFW";
            this.bGetFW.Size = new System.Drawing.Size(125, 27);
            this.bGetFW.TabIndex = 4;
            this.bGetFW.Text = "Get Firmware Version";
            this.bGetFW.UseVisualStyleBackColor = true;
            this.bGetFW.Click += new System.EventHandler(this.bGetFW_Click);
            // 
            // gbLED
            // 
            this.gbLED.Controls.Add(this.cbGreen);
            this.gbLED.Controls.Add(this.cbRed);
            this.gbLED.Location = new System.Drawing.Point(12, 143);
            this.gbLED.Name = "gbLED";
            this.gbLED.Size = new System.Drawing.Size(136, 55);
            this.gbLED.TabIndex = 5;
            this.gbLED.TabStop = false;
            this.gbLED.Text = "LED Settings";
            // 
            // cbGreen
            // 
            this.cbGreen.AutoSize = true;
            this.cbGreen.Location = new System.Drawing.Point(64, 22);
            this.cbGreen.Name = "cbGreen";
            this.cbGreen.Size = new System.Drawing.Size(55, 17);
            this.cbGreen.TabIndex = 3;
            this.cbGreen.Text = "Green";
            this.cbGreen.UseVisualStyleBackColor = true;
            // 
            // cbRed
            // 
            this.cbRed.AutoSize = true;
            this.cbRed.Location = new System.Drawing.Point(16, 21);
            this.cbRed.Name = "cbRed";
            this.cbRed.Size = new System.Drawing.Size(46, 17);
            this.cbRed.TabIndex = 2;
            this.cbRed.Text = "Red";
            this.cbRed.UseVisualStyleBackColor = true;
            // 
            // bGetLed
            // 
            this.bGetLed.Location = new System.Drawing.Point(154, 146);
            this.bGetLed.Name = "bGetLed";
            this.bGetLed.Size = new System.Drawing.Size(124, 23);
            this.bGetLed.TabIndex = 6;
            this.bGetLed.Text = "Get Led State";
            this.bGetLed.UseVisualStyleBackColor = true;
            this.bGetLed.Click += new System.EventHandler(this.bGetLed_Click);
            // 
            // bSetLed
            // 
            this.bSetLed.Location = new System.Drawing.Point(154, 175);
            this.bSetLed.Name = "bSetLed";
            this.bSetLed.Size = new System.Drawing.Size(123, 23);
            this.bSetLed.TabIndex = 7;
            this.bSetLed.Text = "Set Led State";
            this.bSetLed.UseVisualStyleBackColor = true;
            this.bSetLed.Click += new System.EventHandler(this.bSetLed_Click);
            // 
            // gbBuzzState
            // 
            this.gbBuzzState.Controls.Add(this.bSetBuzzState);
            this.gbBuzzState.Controls.Add(this.bGetBuzzState);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed8);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed7);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed6);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed5);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed4);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed3);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed2);
            this.gbBuzzState.Controls.Add(this.cbBuzzLed1);
            this.gbBuzzState.Location = new System.Drawing.Point(12, 317);
            this.gbBuzzState.Name = "gbBuzzState";
            this.gbBuzzState.Size = new System.Drawing.Size(264, 271);
            this.gbBuzzState.TabIndex = 9;
            this.gbBuzzState.TabStop = false;
            this.gbBuzzState.Text = "LED and Buzzer Setting";
            // 
            // bSetBuzzState
            // 
            this.bSetBuzzState.Location = new System.Drawing.Point(140, 226);
            this.bSetBuzzState.Name = "bSetBuzzState";
            this.bSetBuzzState.Size = new System.Drawing.Size(108, 23);
            this.bSetBuzzState.TabIndex = 9;
            this.bSetBuzzState.Text = "Set Buzz/LED State";
            this.bSetBuzzState.UseVisualStyleBackColor = true;
            this.bSetBuzzState.Click += new System.EventHandler(this.bSetBuzzState_Click);
            // 
            // bGetBuzzState
            // 
            this.bGetBuzzState.Location = new System.Drawing.Point(16, 226);
            this.bGetBuzzState.Name = "bGetBuzzState";
            this.bGetBuzzState.Size = new System.Drawing.Size(118, 23);
            this.bGetBuzzState.TabIndex = 8;
            this.bGetBuzzState.Text = "Get Buzze/LED State";
            this.bGetBuzzState.UseVisualStyleBackColor = true;
            this.bGetBuzzState.Click += new System.EventHandler(this.bGetBuzzState_Click);
            // 
            // cbBuzzLed8
            // 
            this.cbBuzzLed8.AutoSize = true;
            this.cbBuzzLed8.Location = new System.Drawing.Point(16, 199);
            this.cbBuzzLed8.Name = "cbBuzzLed8";
            this.cbBuzzLed8.Size = new System.Drawing.Size(197, 17);
            this.cbBuzzLed8.TabIndex = 7;
            this.cbBuzzLed8.Text = "Enable Card Operation Blinking LED";
            this.cbBuzzLed8.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed7
            // 
            this.cbBuzzLed7.AutoSize = true;
            this.cbBuzzLed7.Location = new System.Drawing.Point(16, 176);
            this.cbBuzzLed7.Name = "cbBuzzLed7";
            this.cbBuzzLed7.Size = new System.Drawing.Size(205, 17);
            this.cbBuzzLed7.TabIndex = 6;
            this.cbBuzzLed7.Text = "Enable Exclusive Mode Status Buzzer";
            this.cbBuzzLed7.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed6
            // 
            this.cbBuzzLed6.AutoSize = true;
            this.cbBuzzLed6.Location = new System.Drawing.Point(16, 153);
            this.cbBuzzLed6.Name = "cbBuzzLed6";
            this.cbBuzzLed6.Size = new System.Drawing.Size(210, 17);
            this.cbBuzzLed6.TabIndex = 5;
            this.cbBuzzLed6.Text = "Enable RC531 Reset Indication Buzzer";
            this.cbBuzzLed6.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed5
            // 
            this.cbBuzzLed5.AutoSize = true;
            this.cbBuzzLed5.Location = new System.Drawing.Point(16, 130);
            this.cbBuzzLed5.Name = "cbBuzzLed5";
            this.cbBuzzLed5.Size = new System.Drawing.Size(245, 17);
            this.cbBuzzLed5.TabIndex = 4;
            this.cbBuzzLed5.Text = "Enable Card Insertion/Removal Events Buzzer";
            this.cbBuzzLed5.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed4
            // 
            this.cbBuzzLed4.AutoSize = true;
            this.cbBuzzLed4.Location = new System.Drawing.Point(16, 107);
            this.cbBuzzLed4.Name = "cbBuzzLed4";
            this.cbBuzzLed4.Size = new System.Drawing.Size(178, 17);
            this.cbBuzzLed4.TabIndex = 3;
            this.cbBuzzLed4.Text = "Enable PICC PPS Status Buzzer";
            this.cbBuzzLed4.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed3
            // 
            this.cbBuzzLed3.AutoSize = true;
            this.cbBuzzLed3.Location = new System.Drawing.Point(16, 84);
            this.cbBuzzLed3.Name = "cbBuzzLed3";
            this.cbBuzzLed3.Size = new System.Drawing.Size(204, 17);
            this.cbBuzzLed3.TabIndex = 2;
            this.cbBuzzLed3.Text = "Enable PICC Activation Status Buzzer";
            this.cbBuzzLed3.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed2
            // 
            this.cbBuzzLed2.AutoSize = true;
            this.cbBuzzLed2.Location = new System.Drawing.Point(16, 59);
            this.cbBuzzLed2.Name = "cbBuzzLed2";
            this.cbBuzzLed2.Size = new System.Drawing.Size(177, 17);
            this.cbBuzzLed2.TabIndex = 1;
            this.cbBuzzLed2.Text = "Enable PICC Polling Status LED";
            this.cbBuzzLed2.UseVisualStyleBackColor = true;
            // 
            // cbBuzzLed1
            // 
            this.cbBuzzLed1.AutoSize = true;
            this.cbBuzzLed1.Location = new System.Drawing.Point(16, 34);
            this.cbBuzzLed1.Name = "cbBuzzLed1";
            this.cbBuzzLed1.Size = new System.Drawing.Size(186, 17);
            this.cbBuzzLed1.TabIndex = 0;
            this.cbBuzzLed1.Text = "Enable ICC Activation Status LED";
            this.cbBuzzLed1.UseVisualStyleBackColor = true;
            // 
            // mMsg
            // 
            this.mMsg.FormattingEnabled = true;
            this.mMsg.Location = new System.Drawing.Point(296, 16);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(312, 537);
            this.mMsg.TabIndex = 10;
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(296, 565);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(104, 23);
            this.bClear.TabIndex = 11;
            this.bClear.Text = "Clear Screen";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(406, 565);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(97, 23);
            this.bReset.TabIndex = 12;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(509, 565);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(99, 23);
            this.bQuit.TabIndex = 13;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // gbBuzz
            // 
            this.gbBuzz.Controls.Add(this.bStopBuzz);
            this.gbBuzz.Controls.Add(this.bStartBuzz);
            this.gbBuzz.Controls.Add(this.bSetBuzzDur);
            this.gbBuzz.Controls.Add(this.tBuzzDur);
            this.gbBuzz.Controls.Add(this.Label2);
            this.gbBuzz.Location = new System.Drawing.Point(12, 205);
            this.gbBuzz.Name = "gbBuzz";
            this.gbBuzz.Size = new System.Drawing.Size(264, 106);
            this.gbBuzz.TabIndex = 14;
            this.gbBuzz.TabStop = false;
            this.gbBuzz.Text = "Set Buzzer Duration (x10 mSec)";
            // 
            // bStopBuzz
            // 
            this.bStopBuzz.Location = new System.Drawing.Point(134, 29);
            this.bStopBuzz.Name = "bStopBuzz";
            this.bStopBuzz.Size = new System.Drawing.Size(111, 23);
            this.bStopBuzz.TabIndex = 4;
            this.bStopBuzz.Text = "Stop Buzzer Tone";
            this.bStopBuzz.UseVisualStyleBackColor = true;
            this.bStopBuzz.Click += new System.EventHandler(this.bStopBuzz_Click);
            // 
            // bStartBuzz
            // 
            this.bStartBuzz.Location = new System.Drawing.Point(18, 29);
            this.bStartBuzz.Name = "bStartBuzz";
            this.bStartBuzz.Size = new System.Drawing.Size(108, 23);
            this.bStartBuzz.TabIndex = 3;
            this.bStartBuzz.Text = "Set Buzzer Tone";
            this.bStartBuzz.UseVisualStyleBackColor = true;
            this.bStartBuzz.Click += new System.EventHandler(this.bStartBuzz_Click);
            // 
            // bSetBuzzDur
            // 
            this.bSetBuzzDur.Location = new System.Drawing.Point(132, 66);
            this.bSetBuzzDur.Name = "bSetBuzzDur";
            this.bSetBuzzDur.Size = new System.Drawing.Size(110, 23);
            this.bSetBuzzDur.TabIndex = 2;
            this.bSetBuzzDur.Text = "Set Buzzer Duration";
            this.bSetBuzzDur.UseVisualStyleBackColor = true;
            this.bSetBuzzDur.Click += new System.EventHandler(this.bSetBuzzDur_Click_1);
            // 
            // tBuzzDur
            // 
            this.tBuzzDur.Location = new System.Drawing.Point(61, 68);
            this.tBuzzDur.Name = "tBuzzDur";
            this.tBuzzDur.Size = new System.Drawing.Size(59, 20);
            this.tBuzzDur.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(21, 71);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(34, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Value";
            // 
            // DeviceProgramming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 600);
            this.Controls.Add(this.gbBuzz);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.gbBuzzState);
            this.Controls.Add(this.bSetLed);
            this.Controls.Add(this.bGetLed);
            this.Controls.Add(this.gbLED);
            this.Controls.Add(this.bGetFW);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeviceProgramming";
            this.Text = "ACR 128 Device Programming";
            this.Load += new System.EventHandler(this.DeviceProgramming_Load);
            this.gbLED.ResumeLayout(false);
            this.gbLED.PerformLayout();
            this.gbBuzzState.ResumeLayout(false);
            this.gbBuzzState.PerformLayout();
            this.gbBuzz.ResumeLayout(false);
            this.gbBuzz.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReader;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Button bGetFW;
        private System.Windows.Forms.GroupBox gbLED;
        private System.Windows.Forms.Button bGetLed;
        private System.Windows.Forms.Button bSetLed;
        private System.Windows.Forms.GroupBox gbBuzzState;
        private System.Windows.Forms.Button bSetBuzzState;
        private System.Windows.Forms.Button bGetBuzzState;
        private System.Windows.Forms.CheckBox cbBuzzLed8;
        private System.Windows.Forms.CheckBox cbBuzzLed7;
        private System.Windows.Forms.CheckBox cbBuzzLed6;
        private System.Windows.Forms.CheckBox cbBuzzLed5;
        private System.Windows.Forms.CheckBox cbBuzzLed4;
        private System.Windows.Forms.CheckBox cbBuzzLed3;
        private System.Windows.Forms.CheckBox cbBuzzLed2;
        private System.Windows.Forms.CheckBox cbBuzzLed1;
        private System.Windows.Forms.ListBox mMsg;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.CheckBox cbGreen;
        internal System.Windows.Forms.CheckBox cbRed;
        internal System.Windows.Forms.GroupBox gbBuzz;
        internal System.Windows.Forms.Button bStopBuzz;
        internal System.Windows.Forms.Button bStartBuzz;
        internal System.Windows.Forms.Button bSetBuzzDur;
        internal System.Windows.Forms.TextBox tBuzzDur;
        internal System.Windows.Forms.Label Label2;
    }
}

