using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace SSPDllExample
{
    public partial class frmPort : Form
    {
        public frmPort()
        {
            InitializeComponent();
        }

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void bttnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ComPort = Convert.ToInt16(this.cBoxSerPort.Text.Remove(0,3));
            //Properties.Settings.Default.ComPort = 0;
            Properties.Settings.Default.Save();
            Global.iPort = Properties.Settings.Default.ComPort;
            base.Dispose();

        }

        private void frmPort_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if ((ports == null) || (ports.Length == 0))
            {
                MessageBox.Show("no Serial Port found", "ERROR");
                base.Dispose();
            }
            else
            {
                Array.Sort<string>(ports);
                for (byte i = 0; i < ports.Length; i = (byte)(i + 1))
                {
                    this.cBoxSerPort.Items.Add(ports[i]);
                }
                this.cBoxSerPort.SelectedItem = this.cBoxSerPort.Items[0];
            }

        }
    }
}
