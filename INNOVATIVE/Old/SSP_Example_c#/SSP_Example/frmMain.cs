using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

//--------------------------------------------------------------------------------
//A C# implementation using SSP protocol and the unmanaged C++ SSP DLL.
//This code gives an example of a simple working PC based system
//of a host machine controlling a SSP validator.
//The user can set up credit limits and games prices.
//The system will monitor and store calculated credits
//from the validator and set the channel inhibit lines so as only
//allow a maximum spend. Credit may be spent by simply clicking a button.
//
//
//Note:
//This code is provided as a guide to the programmer.
//It is the users responsibility to test their implementation of the DLL
//to ensure the required operation.
//Innovative Technology Ltd does not accept liability for
//errors or omissions contained within this source code.
//
//--------------------------------------------------------------------------------


namespace SSPDllExample
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // get the last stored comport setting from the config file
            Global.iPort = Properties.Settings.Default.ComPort;

            // get the host system data from the config file
            Global.host.MaxCredit = Properties.Settings.Default.MaxCredit;
            Global.host.LastCredit = Properties.Settings.Default.LastCredit;
            Global.host.GamePrice = Properties.Settings.Default.GamePrice;
            Global.host.CurrentCredit = Properties.Settings.Default.CurrentCredit;
            Global.host.SerialNumber = Properties.Settings.Default.SerialNumber;
            // reset the connection attempts counter
            Global.host.ConnectionAttempts = 0;

            UpdateHostDisplay();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bttnHalt_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void bttnExit_Click(object sender, EventArgs e)
        {
            base.Dispose(true);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            base.Dispose(true);
        }

        private void mnuSerialPort_Click(object sender, EventArgs e)
        {
            Form formPort = new frmPort();
            formPort.ShowDialog();
        }

        private void mnuHostSystem_Click(object sender, EventArgs e)
        {
            Form formHost = new frmHost();
            formHost.ShowDialog();
            UpdateHostDisplay();
        }

        private void bttnRun_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Text1.Text = "";
            RunSlave();
        }

        private void bttnPlay_Click(object sender, EventArgs e)
        {
            //------------------------------------------------------------------------------
            // To deduct the game price from the current credit value

            Global.host.CurrentCredit = Global.host.CurrentCredit - Global.host.GamePrice;

            UpdateHostDisplay();
            UpdateInhibits();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //------------------------------
            //--- The delay timer

            timer1.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            short iCode;

            // disable timer until poll operation complete
            timer2.Enabled = false;
            // clear the event list display
            Text1.Text = "";
            Application.DoEvents();
            //  send the poll command
            iCode = SendPollCmd();
            //  test for good poll
            if (iCode > 0)
            {
                //  if connection attemps timed out
                if (Global.host.ConnectionAttempts >= Global.POLL_TRY_LIMIT)
                {
                    System.Windows.Forms.MessageBox.Show("Unable to Poll Slave. Error ");
                    return;
                }
                //  try to restart the validator
                RunSlave();
                Global.host.ConnectionAttempts = Global.host.ConnectionAttempts + 1;
            }
        }

        //open com port, send poll command, close com port
        //returns 0 if the process passed or error code if failed

        private short SendPollCmd()
        {
            Declare.UDT src = new Declare.UDT();
            Declare.UDT cpy = new Declare.UDT();
            Declare.RTRY reTry;
            bool retVal;

            //open com port
            if (Declare.OpenPort(Global.iPort) != 1)
                return Global.START_PORT_OPEN_FAIL;

            //  reset the retries to 10
            reTry.rDelay = 1;
            reTry.rRetries = 10;
            Declare.SetRetryParameters(ref reTry);

            //send poll command
            src.datalen = 1;
            src.array1[0] = DefMod.POLL_CMD;
            cpy = Declare.Command(src);

            //check if response is ok
            if (cpy.array1[0] != DefMod.OK)
            {
                Declare.CloseComm();
                return Global.POLL_CMD_FAIL;
            }

            //  decode the events. If result of decode is true then enable timer for next poll
            retVal = DecodeEvents(ref cpy.array1, ref  cpy.datalen);
            Declare.CloseComm();
            timer2.Enabled = true;

            return 0;
        }

        private bool DecodeEvents(ref byte[] evList, ref byte evLen)
        {
            short i, iCode;
            Declare.UDT cmd = new Declare.UDT();
            string evStr = "";

            // display all events in a packet
            for (i = 0; i < evLen; i++)
            {
                switch (evList[i])
                {
                    case DefMod.REJECTED_MS:
                        evStr = evStr + "Rejected ";
                        break;
                    case DefMod.FRAUD_ATTEMPT_MS:
                        evStr = evStr + "Fraud Attempt " + evList[i + 1] + " ";
                        // increment the event list count (double event)
                        i++;
                        break;
                    case DefMod.NOTE_STACKED:
                        evStr = evStr + "Stacked ";
                        break;
                    case DefMod.STACKING:
                        evStr = evStr + "Stacking ";
                        break;
                    case DefMod.CREDIT:
                        evStr = evStr + "Credit " + evList[i + 1] + " ";
                        //  update the host details
                        Global.host.LastCredit = Global.sspSlave.ChannelValue[evList[i + 1] - 1];
                        Global.host.CurrentCredit = Global.host.CurrentCredit + Global.host.LastCredit;
                        //  display the data and set inhibits
                        UpdateHostDisplay();
                        UpdateInhibits();
                        i++;
                        break;
                    case DefMod.SAFE_JAM:
                        evStr = evStr + "Safe Jam ";
                        break;
                    case DefMod.UNSAFE_JAM:
                        evStr = evStr + "Unsafe jam ";
                        break;
                    case DefMod.STACKER_FULL:
                        evStr = evStr + "Stacker full ";
                        break;
                    case DefMod.OK:
                        evStr = evStr + "OK ";
                        break;
                    case DefMod.SLAVE_RESET:
                        evStr = evStr + "Slave reset ";
                        //  if slave reset is seen during poll conditions, then slave has
                        //  reset. Run a full restart in this case. The serial number of the
                        //  validator will be checked by the start up routine
                        RunSlave();
                        return false;
                    case DefMod.COMMAND_NOT_KNOWN:
                        evStr = evStr + "Command not known ";
                        break;
                    case DefMod.WRONG_No_PARAMETERS:
                        evStr = evStr + "Wrong number of parameters ";
                        break;
                    case DefMod.PARAMETER_OUT_RANGE:
                        evStr = evStr + "Parameter out of range ";
                        break;
                    case DefMod.COMMAND_NOT_PROCESS:
                        evStr = evStr + "Command not processed ";
                        break;
                    case DefMod.SOFTWARE_ERROR:
                        evStr = evStr + "Software error ";
                        break;
                    case DefMod.DISABLED:
                        evStr = evStr + "Disabled ";
                        //  if we see a disabled, then re-enable here
                        cmd.datalen = 1;
                        cmd.array1[0] = DefMod.ENABLE_CMD;
                        iCode = Global.SendCommand(cmd);
                        break;
                    case DefMod.NOTE_READ:
                        evStr = evStr + "Note Read " + evList[i + 1] + " ";
                        //  if this is a valid note then send command to accept
                        if (evList[i + 1] > 0)
                            iCode = SendPollCmd();
                        // increment the event list count (double event)
                        i++;
                        break;
                    case DefMod.REJECTING_MS:
                        evStr = evStr + "Rejecting ";
                        break;
                    default:
                        evStr = evStr + "Unknown event " + evList[i] + " ";
                        break;
                }
            }
            Text1.Text = evStr;
            return true;
        }

        private void RunSlave()
        {
            //----------------------------------------------------------------
            //                          RunSlave()
            //a method to Perform start up on connected validator 
            //returns 0 if the process passed or error code if failed


            short iCode;
            lv1.Items.Clear();
            Application.DoEvents();

            //  check for port settings
            if (Global.iPort == 0)
            {
                System.Windows.Forms.MessageBox.Show("No serial port has been selected");
                return;
            }

            //  attempt to comunicate with validator and Get slave data
            iCode = GetSlaveData();

            //compare serial number with settings
            if (iCode == Global.SERIAL_NUMBER_MATCH_FAIL)
            {
                System.Windows.Forms.MessageBox.Show("Serial number doesn't match settings. Error ");
                return;
            }
            
            //check for error while getting slave data
            if (iCode > 0)
            {
                System.Windows.Forms.MessageBox.Show("Unable to get slave data. Error ");
                return;
            }

            //  now send the start-up commands to the slave
            iCode = SendSSPStart();

            //check for error while sending start-up commands
            if (iCode > 0)
            {
                System.Windows.Forms.MessageBox.Show("Unable to startup slave. Error ");
                return;
            }

            //  set up the slave inhibits dependent on the max credit and current credit
            UpdateInhibits();

            //  now start the poll timer
            timer2.Interval = 1000;
            timer2.Enabled = true;
            //  reset the conection attempts
            Global.host.ConnectionAttempts = 0;
        }

        private short SendSSPStart()
        {
            //----------------------------------------------------------------
            //                          SendSSPStart()
            //a method to start up and enable a connected validator.
            //returns 0 if the startup process passed or error code if failed

            short i;
            bool Break;
            Declare.UDT src = new Declare.UDT();
            Declare.UDT cpy = new Declare.UDT();
            Declare.RTRY reTry;
            short rCount;

            Global.InhibitLow = 0;
            Global.InhibitHigh = 0;

            if (Declare.OpenPort(Global.iPort) != 1)
            {
                return Global.START_PORT_OPEN_FAIL;
            }

            //  poll slave until slave reset response is gone from event queue
            reTry.rDelay = 1;
            reTry.rRetries = 1;
            Declare.SetRetryParameters(ref reTry);
            src.datalen = 1;
            src.array1[0] = DefMod.POLL_CMD;
            rCount = 1;
            Break = false;

            while ((!Break) && (rCount < Global.RESET_COUNT_TIMEOUT))
            {
                while (timer1.Enabled==true)
                {
                    Application.DoEvents();
                }
                cpy = Declare.Command(src);
                if (cpy.array1[0] != DefMod.OK)
                {
                }
                //  scan the event queue for slave reset event
                Break = true;
                for (i = 1; i < cpy.datalen; i++)
                {
                    //  if seen then set break flag
                    if (cpy.array1[i] == DefMod.SLAVE_RESET)
                        Break = false;
                }
                rCount++;
            }

            timer1.Enabled = false;

            //  if failed to clear the reset event, show error and exit
            if (!Break)
            {
                Declare.CloseComm();
                return Global.RESET_EVENT_FAIL;
            }

            //  send the set inhibit commands
            src.datalen = 3;
            src.array1[0] = DefMod.SET_INHIBITS_CMD;
            src.array1[1] = Global.InhibitLow;
            src.array1[2] = Global.InhibitHigh;
            cpy = Declare.Command(src);
            if (cpy.array1[0] != DefMod.OK)
            {
                Declare.CloseComm();
                return Global.SET_INIHBITS_FAIL;
            }

            //  send the enable command
            src.datalen = 1;
            src.array1[0] = DefMod.ENABLE_CMD;
            cpy = Declare.Command(src);
            if (cpy.array1[0] != DefMod.OK)
            {
                Declare.CloseComm();
                return Global.ENABLE_CMD_FAIL;
            }

            Declare.CloseComm();
            return 0;
        }

        private short GetSlaveData()
        {
            //----------------------------------------------------------------
            //                          GetSlaveData()
            //a method to extract setup data from a connected validator
            //returns 0 if the startup process passed or error code if passed

            bool Break;
            Declare.UDT src = new Declare.UDT();
            Declare.UDT cpy = new Declare.UDT();
            Declare.RTRY reTry = new Declare.RTRY();
            Global.sspSlave.ChannelValue = new int[16];
            Global.sspSlave.ChannelSecurity = new byte[16];
            short rCount;
            short i;

            //open com port
            if (Declare.OpenPort(Global.iPort) != 1)
            {
                return Global.START_PORT_OPEN_FAIL;
            }

            //check host data settings
            if (Global.host.MaxCredit == 0 || Global.host.GamePrice == 0)
            {
                return Global.HOST_DATA_NOT_SET;
            }

            //  send sync command until response recieved or time out reached
            //  we will just limit to 1 retry here
            reTry.rDelay = 1;
            reTry.rRetries = 1;
            Declare.SetRetryParameters(ref reTry);
            Break = false;
            rCount = 1;

            //  set up the command structure
            src.rxStatus = 0; //  the slave address
            src.datalen = 1;  //  data length
            src.array1[0] = DefMod.SYNC_CMD;  //  load the command array
            while (!(Break || rCount == Global.CON_RETRY_LIMIT))
            {
                Application.DoEvents();
                //send command
                cpy = Declare.Command(src);
                if (cpy.array1[0] == DefMod.OK)
                    Break = true;
                rCount++;
            }

            //  if no connection found within try limit then exit
            if (!Break)
            {
                Declare.CloseComm();
                return Global.CONNECTION_ATTEMPT_TIMEOUT;
            }

            // here we have established connection to a slave
            //  get the slave information

            //  serial number
            src.datalen = 1;
            src.array1[0] = DefMod.SERIAL_NUMBER;
            cpy = Declare.Command(src);
            if (cpy.array1[0] != DefMod.OK)
            {
                Declare.CloseComm();
                return Global.SERIAL_NUMBER_FAIL;
            }

            //convert serial number from 4 bytes to decimal value
            Global.sspSlave.SerialNumber = 0;
            for (i = 1; i <= 4; i++)
            {
                Global.sspSlave.SerialNumber += Convert.ToUInt32(((cpy.array1[i])) * Math.Pow(256, (4 - i)));
            }

            //  check for serial number match
            if (Global.sspSlave.SerialNumber != Global.host.SerialNumber)
            {
                Declare.CloseComm();
                return Global.SERIAL_NUMBER_MATCH_FAIL;
            }

            //  get slave setup data
            src.datalen = 1;
            src.array1[0] = DefMod.SETUP_REQUEST_CMD;
            cpy = Declare.Command(src);
            if (cpy.array1[0] != DefMod.OK)
            {
                Declare.CloseComm();
                return Global.SETUP_REQUEST_FAIL;
            }

            Global.sspSlave.unitType = cpy.array1[1];
            Global.sspSlave.FirmwareVersion = "";
            for (i = 2; i <= 5; i++)
            {
                Global.sspSlave.FirmwareVersion = Global.sspSlave.FirmwareVersion + Convert.ToChar(cpy.array1[i]);
            }
            Global.sspSlave.CountryCode = "";
            for (i = 6; i <= 8; i++)
            {
                Global.sspSlave.CountryCode = Global.sspSlave.CountryCode + Convert.ToChar(cpy.array1[i]);
            }
            Global.sspSlave.ValueMultiplier = 0;
            for (i = 9; i <= 11; i++)
            {
                Global.sspSlave.ValueMultiplier = Global.sspSlave.ValueMultiplier + (Convert.ToInt32(cpy.array1[i]) * (Convert.ToInt32(Math.Pow(256, (11 - i)))));
            }
            Global.sspSlave.NumberOfChannels = cpy.array1[12];
            for (i = 0; i < Global.sspSlave.NumberOfChannels; i++)
            {
                Global.sspSlave.ChannelValue[i] = Convert.ToInt32(cpy.array1[i + 13]) * Global.sspSlave.ValueMultiplier;
            }

            for (i = 0; i < Global.sspSlave.NumberOfChannels; i++)
            {
                Global.sspSlave.ChannelSecurity[i] = cpy.array1[i + 13 + Global.sspSlave.NumberOfChannels];
            }

            //  now display this set up data
            DisplaySetupData();

            Declare.CloseComm();
            return 0;
        }

        //display validator setup data
        private void DisplaySetupData()
        {
            short i;

            //  clear the display
            lv1.Clear();
            //  add the columns
            lv1.Columns.Add("Parameter", 93, HorizontalAlignment.Left);
            lv1.Columns.Add("Value", 93, HorizontalAlignment.Left);
            lv1.Columns.Add("Status", 93, HorizontalAlignment.Left);
            // add the parameter items and their values
            ListViewItem Serial = new ListViewItem(new string[] { "Serial Number", Convert.ToString(Global.sspSlave.SerialNumber) });
            ListViewItem Firmware = new ListViewItem(new string[] { "Firmware Version", Convert.ToString(Global.sspSlave.FirmwareVersion) });
            ListViewItem Country = new ListViewItem(new string[] { "Country Code", Convert.ToString(Global.sspSlave.CountryCode) });
            lv1.Items.AddRange(new ListViewItem[] { Serial, Firmware, Country });

            for (i = 0; i < Global.sspSlave.NumberOfChannels; i++)
            {
                ListViewItem item = new ListViewItem("Channel " + Convert.ToString(i + 1) + " value");
                item.SubItems.Add(Convert.ToString(Global.sspSlave.ChannelValue[i]));
                item.SubItems.Add("");
                lv1.Items.Add(item);
            }
        }

        //set inhibits
        private void UpdateInhibits()
        {
            byte ChannelLow;
            byte ChannelHigh;
            Declare.UDT cmd = new Declare.UDT();
            short i;
            int iCode;

            //  setup the slave inhibits to only accept notes upto max credit value
            ChannelLow = 0;
            ChannelHigh = 0;
            for (i = 0; i < (Global.sspSlave.NumberOfChannels); i++)
            {
                lv1.Items[i + 3].SubItems[2].Text = "inhibited";
                //  channel 1 to 8
                if ((Global.sspSlave.ChannelValue[i] <= (Global.host.MaxCredit - Global.host.CurrentCredit)) & (i < 8))
                {
                    ChannelLow = ChannelLow |= Convert.ToByte(Math.Pow(2, i));
                    lv1.Items[i + 3].SubItems[2].Text = "";
                }
                //  channel 9 to 16
                if ((Global.sspSlave.ChannelValue[i] <= (Global.host.MaxCredit - Global.host.CurrentCredit)) & (i >= 8))
                {
                    ChannelHigh = ChannelHigh |= Convert.ToByte(Math.Pow(2, (i - 8)));
                    lv1.Items[i + 3].SubItems[2].Text = "";
                }
            }

            // send inhibit commands if inhibit settings changed
            if ((ChannelLow != Global.InhibitLow) || (ChannelHigh != Global.InhibitHigh))
            {
                Global.InhibitLow = ChannelLow;
                Global.InhibitHigh = ChannelHigh;

                //  send the command to set the inhibits
                cmd.datalen = 3;
                cmd.array1[0] = DefMod.SET_INHIBITS;
                cmd.array1[1] = ChannelLow;
                cmd.array1[2] = ChannelHigh;
                iCode = Global.SendCommand(cmd);
            }
        }

        //update host display and save values to config file
        private void UpdateHostDisplay()
        {
            txtLastCredit.Text = Convert.ToString(Global.host.LastCredit);
            txtTotalCredit.Text = Convert.ToString(Global.host.CurrentCredit);
            txtMaxCredit.Text = Convert.ToString(Global.host.MaxCredit);
            txtGamePrice.Text = Convert.ToString(Global.host.GamePrice);

            // update the last transaction to regisry
            Properties.Settings.Default.LastCredit = Global.host.LastCredit;
            Properties.Settings.Default.CurrentCredit = Global.host.CurrentCredit;
            Properties.Settings.Default.Save();

            // set up the play button
            if ((Global.host.CurrentCredit >= Global.host.GamePrice) & (Global.host.CurrentCredit != 0))
                bttnPlay.Enabled = true;
            else
                bttnPlay.Enabled = false;
        }
    }
}
