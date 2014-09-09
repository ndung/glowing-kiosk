/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute device-specific functions of ACR128
'
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 5, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'=========================================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeviceProgramming
{
    public partial class DeviceProgramming : Form
    {

        public int retCode, hContext, hCard, Protocol, nBytesRet;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen, dwState, dwActProtocol;

        public DeviceProgramming()
        {

            InitializeComponent();

        }
        private void DeviceProgramming_Load(object sender, EventArgs e)
        {

            InitMenu();

        }
        private void InitMenu()
        {

            connActive = false;
            cbReader.Text = "";
            mMsg.Text = "";
            mMsg.Items.Clear();
            bConnect.Enabled = false;
            bInit.Enabled = true;
            bReset.Enabled = false;
            bGetFW.Enabled = false;
            gbLED.Enabled = false;
            cbBuzzLed1.Checked = false;
            cbBuzzLed2.Checked = false;
            cbBuzzLed3.Checked = false;
            cbBuzzLed4.Checked = false;
            cbBuzzLed5.Checked = false;
            cbBuzzLed6.Checked = false;
            cbBuzzLed7.Checked = false;
            tBuzzDur.Text = "";
            gbBuzz.Enabled = false;
            bSetBuzzDur.Enabled = false;
            bGetLed.Enabled = false;
            bSetLed.Enabled = false;
            bGetBuzzState.Enabled = false;
            bSetBuzzState.Enabled = false;
            gbBuzzState.Enabled = false;
            displayOut(0, 0, "Program ready");

        }

        private void ClearBuffers()
        {

            long indx;

            for (indx = 0; indx <= 262; indx++)
            {
                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;
            }

        }

        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:
                    break;
                case 1:
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);
                    break;
                case 2:
                    PrintText = "<" + PrintText;
                    break;
                case 3:
                    PrintText = ">" + PrintText;
                    break;

            }
            mMsg.Items.Add(PrintText);
            mMsg.ForeColor = Color.Black;
            mMsg.Focus();

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;
            bReset.Enabled = true;
            bClear.Enabled = true;
            gbLED.Enabled = true;

        }

        private void bInit_Click(object sender, EventArgs e)
        {
            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            // 1. Establish Context
            retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

                return;

            }

            // 2. List PC/SC card readers installed in the system
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

                return;
            }

            EnableButtons();

            byte[] ReadersList = new byte[pcchReaders];

            // Fill reader list
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, ReadersList, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                mMsg.Items.Add("SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(retCode));
                mMsg.SelectedIndex = mMsg.Items.Count - 1;
                return;
            }
            else
            {
                displayOut(0, 0, " ");
            }

            rName = "";
            indx = 0;

            // Convert reader buffer to string
            while (ReadersList[indx] != 0)
            {

                while (ReadersList[indx] != 0)
                {
                    rName = rName + (char)ReadersList[indx];
                    indx = indx + 1;
                }

                // Add reader name to list
                cbReader.Items.Add(rName);
                rName = "";
                indx = indx + 1;

            }

            if (cbReader.Items.Count > 0)
            {
                cbReader.SelectedIndex = 0;

            }

            // Look for ACR128 SAM and make it the default reader in the combobox
            for (indx = 0; indx <= cbReader.Items.Count - 1; indx++)
            {

                cbReader.SelectedIndex = indx;

                if (cbReader.SelectedIndex == 2 )
                {

                    return;

                }

            }

            return;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {

            // Connect to selected reader using hContext handle and obtain hCard handle
            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            // Shared Connection
            retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_EXCLUSIVE, 0 | 1, ref hCard, ref Protocol);

            if (retCode == ModWinsCard.SCARD_S_SUCCESS)
            {

                // check if ACR128 SAM is used and use Direct Mode if SAM is not detected
                if (String.Compare(cbReader.Text, "ACR128U SAM") > 0)
                {
                    retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

                    // 1. Direct Connection
                    retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT, 0, ref hCard, ref Protocol);

                    if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                    {

                        displayOut(0, 0, "The smart card has been removed, so that further communication is not possible.");
                        connActive = false;

                        return;
                    }

                    else
                    {

                        displayOut(0, 0, "Successful connection to " + cbReader.Text);

                    }
                }

                else
                {

                    displayOut(0, 0, "The smart card has been removed, so that further communication is not possible.");
                    connActive = false;
                    return;

                }
            }

            else
            {

                displayOut(0, 0, "The smart card has been removed, so that further communication is not possible.");

            }

            connActive = true;
            bGetFW.Enabled = true;
            bGetLed.Enabled = true;
            bSetLed.Enabled = true;
            gbBuzz.Enabled = true;
            gbBuzzState.Enabled = true;
            gbLED.Enabled = true;
            bGetBuzzState.Enabled = true;
            bSetBuzzState.Enabled = true;
            bSetBuzzDur.Enabled = true;
            GetLEDState();
            GetBuzzLEDState();


        }

        private void bClear_Click(object sender, EventArgs e)
        {

            mMsg.Items.Clear();

        }

        private void bReset_Click(object sender, EventArgs e)
        {

            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            retCode = ModWinsCard.SCardReleaseContext(hCard);
            InitMenu();

        }

        private void bQuit_Click(object sender, EventArgs e)
        {

            // terminate the application
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);

        }

        private int CallCardControl()
        {

            string tmpStr;
            int indx;

            // Display Apdu In
            tmpStr = "SCardControl: ";

            for (indx = 0; indx <= SendLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }

            displayOut(2, 0, tmpStr);

            retCode = ModWinsCard.SCardControl(hCard, (uint)ModWinsCard.IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND, ref SendBuff[0], SendLen, ref RecvBuff[0], RecvLen, ref nBytesRet);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

            }

            else
            {

                tmpStr = "";

                for (indx = 0; indx <= RecvLen - 1; indx++)
                {

                    tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

                }

                return retCode;

            }
            return retCode;

        }

        private void bGetFW_Click(object sender, EventArgs e)
        {

            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x18;
            SendBuff[1] = 0x0;
            SendLen = 2;
            RecvLen = 35;
            retCode = CallCardControl();

            // Interpret Firmware Data
            tmpStr = "Firmware Version: ";

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            tmpStr = tmpStr + System.Text.ASCIIEncoding.ASCII.GetString(RecvBuff, 5, 19);

            displayOut(3, 0, tmpStr);
        
        }

        private int GetLEDState()
        {

            ClearBuffers();
            SendBuff[0] = 0x29;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return retCode;

            }

            // interpret LED data
            switch (RecvBuff[5])
            {

                case 0:
                    displayOut(3, 0, "Currently connected to SAM reader interface.");
                    cbRed.Enabled = true;
                    break;

                case 1:
                    displayOut(3, 0, "No PICC found.");
                    cbRed.Enabled = true;
                    break;

                case 2:
                    displayOut(3, 0, "PICC is present but not activated.");
                    cbRed.Enabled = true;
                    break;

                case 3:
                    displayOut(3, 0, "PICC is present and activated.");
                    cbRed.Enabled = true;
                    break;

                case 4:
                    displayOut(3, 0, "PICC is present and activated.");
                    cbRed.Enabled = true;
                    break;

                case 5:
                    displayOut(3, 0, "PICC is present and activated.");
                    cbGreen.Enabled = true;
                    break;

                case 6:
                    displayOut(3, 0, "ICC is absent or not activated.");
                    cbGreen.Enabled = true;
                    break;

                case 7:
                    displayOut(3, 0, "ICC is operating.");
                    cbGreen.Enabled = true;
                    break;

            }
            
            if ((RecvBuff[5] & 0x02) != 0) 
            {
            
                cbGreen.Checked = true;

            }
        
            else
            {
        
                cbGreen.Checked = false;

            }

            if ((RecvBuff[5] & 0x01) != 0)
            {
           
                cbRed.Checked = true;
            }

            else
            {

                cbRed.Checked = false;
            }

            return retCode;

        }

        private void bGetLed_Click(object sender, EventArgs e)
        {
           
            GetLEDState();

        }

        private void bSetLed_Click(object sender, EventArgs e)
        {
            
            ClearBuffers();
            SendBuff[0] = 0x29;
            SendBuff[1] = 0x01;

            if (cbRed.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x01);
            }

            if (cbGreen.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x02);

            }

            SendLen = 3;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
     
        }

        private void bSetBuzzDur_Click(object sender, EventArgs e)
        {
            int tempInt;

            if (tBuzzDur.Text == "")
            {

                tBuzzDur.Text = "1";
                tBuzzDur.SelectAll();
                tBuzzDur.Focus();

            }

            if (!int.TryParse(tBuzzDur.Text, out tempInt))
            {
                
                MessageBox.Show("Invalid Input");
                tBuzzDur.Text = "";
                return;

            }

            if (int.Parse(tBuzzDur.Text) > 255)
            {

                tBuzzDur.Text = "255";
                tBuzzDur.SelectAll();
                tBuzzDur.Focus();
                return; 

            }

            if (int.Parse(tBuzzDur.Text) < 1) 
            {
        
               tBuzzDur.Text = "1";
               tBuzzDur.SelectAll();
               tBuzzDur.Focus();
        
            }

            ClearBuffers();
            SendBuff[0] = 0x28;
            SendBuff[1] = 0x01;
            SendBuff[2] = byte.Parse(tBuzzDur.Text);
            SendLen = 3;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;

            }
        }

        private void bGetBuzzState_Click(object sender, EventArgs e)
        {

            GetBuzzLEDState();

        }

        private int GetBuzzLEDState()
        {

            ClearBuffers();
            SendBuff[0] = 0x21;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return retCode;
            }

            // Interpret buzzer State Data
            if ((RecvBuff[5] & 0x01) != 0)
            {

                displayOut(3, 0, "ICC Activation Status LED is enabled.");
                cbBuzzLed1.Checked = true;
            }

            else
            {

                displayOut(3, 0, "ICC Activation Status LED is disabled.");
                cbBuzzLed1.Checked = false;

            }

            if ((RecvBuff[5] & 0x02) != 0)
            {

                displayOut(3, 0, "PICC Polling Status LED is enabled.");
                cbBuzzLed2.Checked = true;
            }

            else
            {

                displayOut(3, 0, "PICC Polling Status LED is disabled.");
                cbBuzzLed2.Checked = false;

            }

            if ((RecvBuff[5] & 0x04) != 0)
            {

                displayOut(3, 0, "PICC Activation Status Buzzer is enabled.");
                cbBuzzLed3.Checked = true;
            }

            else
            {

                displayOut(3, 0, "PICC Activation Status Buzzer is disabled.");
                cbBuzzLed3.Checked = false;

            }

            if ((RecvBuff[5] & 0x08) != 0)
            {

                displayOut(3, 0, "PICC PPS Status Buzzer is enabled.");
                cbBuzzLed4.Checked = true;
            }

            else
            {

                displayOut(3, 0, "PICC PPS Status Buzzer is disabled.");
                cbBuzzLed4.Checked = false;

            }

            if ((RecvBuff[5] & 0x10) != 0)
            {

                displayOut(3, 0, "Card Insertion and Removal Events Buzzer is enabled.");
                cbBuzzLed5.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Card Insertion and Removal Events Buzzer is disabled.");
                cbBuzzLed5.Checked = false;

            }

            if ((RecvBuff[5] & 0x20) != 0)
            {

                displayOut(3, 0, "RC531 Reset Indication Buzzer is enabled.");
                cbBuzzLed6.Checked = true;
            }

            else
            {

                displayOut(3, 0, "RC531 Reset Indication Buzzer is disabled.");
                cbBuzzLed6.Checked = false;

            }

            if ((RecvBuff[5] & 0x40) != 0)
            {

                displayOut(3, 0, "Exclusive Mode Status Buzzer is enabled.");
                cbBuzzLed7.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Exclusive Mode Status Buzzer is disabled.");
                cbBuzzLed7.Checked = false;

            }

            if ((RecvBuff[5] & 0x80) != 0)
            {

                displayOut(3, 0, "Card Operation Blinking LED is enabled.");
                cbBuzzLed8.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Card Operation Blinking LED is disabled.");
                cbBuzzLed8.Checked = false;

            }
            return retCode;


        }

        private void bSetBuzzState_Click(object sender, EventArgs e)
        {

            ClearBuffers();
            SendBuff[0] = 0x21;
            SendBuff[1] = 0x01;
            SendBuff[2] = 0x00;
        
            if (cbBuzzLed1.Checked == true) {
            
                SendBuff[2] = (byte)(SendBuff[2] | 0x01);
           
            }
        
            if (cbBuzzLed2.Checked == true) {

                SendBuff[2] = (byte)(SendBuff[2] | 0x02);
            
            }
        
            if (cbBuzzLed3.Checked == true) {
            
                SendBuff[2] = (byte)(SendBuff[2] | 0x04);
            
            }
        
            if (cbBuzzLed4.Checked == true) {
            
                SendBuff[2] = (byte)(SendBuff[2] | 0x08);
            
            }
        
            if (cbBuzzLed5.Checked == true) {
            
                SendBuff[2] = (byte)(SendBuff[2] | 0x10);
            
            }
        
            if (cbBuzzLed6.Checked == true) {
            
                SendBuff[2] = (byte)(SendBuff[2] | 0x20);
            
            }
        
            if (cbBuzzLed7.Checked == true) {
            
                SendBuff[2]= (byte)(SendBuff[2] | 0x40);
            
            }
        
            if (cbBuzzLed8.Checked == true) {
            
                SendBuff[2] = (byte)(SendBuff[2] | 0x80);
            
            }
        
            SendLen = 3;
            RecvLen = 6;
            retCode = CallCardControl();
        
            if (retCode != ModWinsCard.SCARD_S_SUCCESS) {
            
                return;
            
            }

        }

        private void bStartBuzz_Click(object sender, EventArgs e)
        {
        
            ClearBuffers();

            SendBuff[0] = 0x28;
            SendBuff[1] = 0x01;
            SendBuff[2] = 0xFF;
            SendLen = 3;
            RecvLen = 6;
            
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS) 
            {
                return;
            }
        }

        private void bStopBuzz_Click(object sender, EventArgs e)
        {
            
            ClearBuffers();

            SendBuff[0] = 0x28;
            SendBuff[1] = 0x01;
            SendBuff[2] = 0x00;
            SendLen = 3;
            RecvLen = 6;
        
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

        }

        private void bSetBuzzDur_Click_1(object sender, EventArgs e)
        {

            int tempInt;

            if (tBuzzDur.Text == "")
            {

                tBuzzDur.Text = "1";
                tBuzzDur.SelectAll();
                tBuzzDur.Focus();

            }

            if (!int.TryParse(tBuzzDur.Text, out tempInt))
            {

                MessageBox.Show("Invalid Input");
                tBuzzDur.Text = "";
                return;

            }

            if (int.Parse(tBuzzDur.Text) > 255)
            {

                tBuzzDur.Text = "255";
                tBuzzDur.SelectAll();
                tBuzzDur.Focus();
                return;

            }

            if (int.Parse(tBuzzDur.Text) < 1)
            {

                tBuzzDur.Text = "1";
                tBuzzDur.SelectAll();
                tBuzzDur.Focus();

            }

            ClearBuffers();
            SendBuff[0] = 0x28;
            SendBuff[1] = 0x01;
            SendBuff[2] = byte.Parse(tBuzzDur.Text);
            SendLen = 3;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;

            }
        }   

    }

}