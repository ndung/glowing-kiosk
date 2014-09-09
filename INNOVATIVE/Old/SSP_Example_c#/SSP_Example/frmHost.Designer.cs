namespace SSPDllExample
{
    partial class frmHost
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
            this.bttnOK = new System.Windows.Forms.Button();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bttnUpdateSerial = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtGamePrice = new System.Windows.Forms.TextBox();
            this.txtLastCredit = new System.Windows.Forms.TextBox();
            this.txtCurrentCredit = new System.Windows.Forms.TextBox();
            this.txtMaxCredit = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttnOK
            // 
            this.bttnOK.Location = new System.Drawing.Point(12, 235);
            this.bttnOK.Name = "bttnOK";
            this.bttnOK.Size = new System.Drawing.Size(75, 23);
            this.bttnOK.TabIndex = 0;
            this.bttnOK.Text = "&OK";
            this.bttnOK.UseVisualStyleBackColor = true;
            this.bttnOK.Click += new System.EventHandler(this.bttnOK_Click);
            // 
            // bttnCancel
            // 
            this.bttnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnCancel.Location = new System.Drawing.Point(160, 235);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(75, 23);
            this.bttnCancel.TabIndex = 1;
            this.bttnCancel.Text = "&Cancel";
            this.bttnCancel.UseVisualStyleBackColor = true;
            this.bttnCancel.Click += new System.EventHandler(this.bttnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bttnUpdateSerial);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSerial);
            this.groupBox1.Controls.Add(this.txtGamePrice);
            this.groupBox1.Controls.Add(this.txtLastCredit);
            this.groupBox1.Controls.Add(this.txtCurrentCredit);
            this.groupBox1.Controls.Add(this.txtMaxCredit);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 229);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // bttnUpdateSerial
            // 
            this.bttnUpdateSerial.Location = new System.Drawing.Point(15, 179);
            this.bttnUpdateSerial.Name = "bttnUpdateSerial";
            this.bttnUpdateSerial.Size = new System.Drawing.Size(68, 34);
            this.bttnUpdateSerial.TabIndex = 10;
            this.bttnUpdateSerial.Text = "Update From Slave";
            this.bttnUpdateSerial.UseVisualStyleBackColor = true;
            this.bttnUpdateSerial.Click += new System.EventHandler(this.bttnUpdateSerial_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Slave Serial Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Game Price";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Last Credit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current Credit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Max Credit";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(152, 145);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(65, 20);
            this.txtSerial.TabIndex = 4;
            this.txtSerial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGamePrice
            // 
            this.txtGamePrice.Location = new System.Drawing.Point(152, 112);
            this.txtGamePrice.Name = "txtGamePrice";
            this.txtGamePrice.Size = new System.Drawing.Size(65, 20);
            this.txtGamePrice.TabIndex = 3;
            this.txtGamePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLastCredit
            // 
            this.txtLastCredit.Location = new System.Drawing.Point(152, 81);
            this.txtLastCredit.Name = "txtLastCredit";
            this.txtLastCredit.Size = new System.Drawing.Size(65, 20);
            this.txtLastCredit.TabIndex = 2;
            this.txtLastCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCurrentCredit
            // 
            this.txtCurrentCredit.Location = new System.Drawing.Point(152, 50);
            this.txtCurrentCredit.Name = "txtCurrentCredit";
            this.txtCurrentCredit.Size = new System.Drawing.Size(65, 20);
            this.txtCurrentCredit.TabIndex = 1;
            this.txtCurrentCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxCredit
            // 
            this.txtMaxCredit.Location = new System.Drawing.Point(152, 19);
            this.txtMaxCredit.Name = "txtMaxCredit";
            this.txtMaxCredit.Size = new System.Drawing.Size(65, 20);
            this.txtMaxCredit.TabIndex = 0;
            this.txtMaxCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmHost
            // 
            this.AcceptButton = this.bttnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnCancel;
            this.ClientSize = new System.Drawing.Size(247, 270);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.bttnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmHost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Up Host System";
            this.Load += new System.EventHandler(this.frmHost_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bttnOK;
        private System.Windows.Forms.Button bttnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.TextBox txtGamePrice;
        private System.Windows.Forms.TextBox txtLastCredit;
        private System.Windows.Forms.TextBox txtCurrentCredit;
        private System.Windows.Forms.TextBox txtMaxCredit;
        private System.Windows.Forms.Button bttnUpdateSerial;
    }
}