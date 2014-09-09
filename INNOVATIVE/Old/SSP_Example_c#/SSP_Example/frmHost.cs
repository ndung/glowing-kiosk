using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SSPDllExample
{
    public partial class frmHost : Form
    {
        public frmHost()
        {
            InitializeComponent();
        }

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void frmHost_Load(object sender, EventArgs e)
        {
            txtMaxCredit.Text = Convert.ToString(Properties.Settings.Default.MaxCredit);
            txtCurrentCredit.Text = Convert.ToString(Properties.Settings.Default.CurrentCredit);
            txtLastCredit.Text = Convert.ToString(Properties.Settings.Default.LastCredit);
            txtGamePrice.Text = Convert.ToString(Properties.Settings.Default.GamePrice);
            txtSerial.Text = Convert.ToString(Properties.Settings.Default.SerialNumber);
        }

        private void bttnOK_Click(object sender, EventArgs e)
        {
            // update the host values
            Global.host.MaxCredit = Convert.ToUInt16(txtMaxCredit.Text);
            Global.host.LastCredit = Convert.ToUInt16(txtLastCredit.Text);
            Global.host.GamePrice = Convert.ToUInt16(txtGamePrice.Text);
            Global.host.CurrentCredit = Convert.ToUInt16(txtCurrentCredit.Text);
            Global.host.SerialNumber = Convert.ToUInt32(txtSerial.Text);

            Properties.Settings.Default.MaxCredit = Global.host.MaxCredit;
            Properties.Settings.Default.LastCredit = Global.host.LastCredit;
            Properties.Settings.Default.GamePrice = Global.host.GamePrice;
            Properties.Settings.Default.CurrentCredit = Global.host.CurrentCredit;
            Properties.Settings.Default.SerialNumber = Global.host.SerialNumber;
            Properties.Settings.Default.Save();

            base.Dispose();
        }

        private void bttnUpdateSerial_Click(object sender, EventArgs e)
        {
            // ------------------------------------------------------------------------
            // Get the serial number from the connected device and update the host system
            // stored number.
            // 

            //check com port settings
            if (Global.iPort == 0)
            {
                MessageBox.Show("no Serial Port selected", "ERROR");
                base.Dispose();
            }

            int iCode, i;
            Declare.UDT cmd = new Declare.UDT();
            Declare.UDT cpy = new Declare.UDT();

            // send sync command
            cmd.datalen = 1;
            cmd.rxStatus = 0;
            cmd.array1[0] = 0x11;

            iCode = Global.SendCommand(cmd);

            //show error and exit when response is not ok
            if (iCode != DefMod.OK)
            {
                MessageBox.Show("No connection to slave");
                return;
            }

            //open com port
            if (Declare.OpenPort(Global.iPort) != 1)
            {
                return;
            }

            // send the serial number command
            cmd.rxStatus = 0;
            cmd.datalen = 1;
            cmd.array1[0] = DefMod.SERIAL_NUMBER;

            cpy = Declare.Command(cmd); //send command cmd, response is in cpy

            //convert serial number from 4 bytes to decimal value
            Global.host.SerialNumber = 0;
            for (i = 1; i <= 4; i++)
            {
                Global.host.SerialNumber += Convert.ToUInt32(((cpy.array1[i])) * Math.Pow(256, (4 - i)));
            }

            //save serial number
            txtSerial.Text = Convert.ToString(Global.host.SerialNumber);

            //close com port
            Declare.CloseComm();

        }
    }
}
