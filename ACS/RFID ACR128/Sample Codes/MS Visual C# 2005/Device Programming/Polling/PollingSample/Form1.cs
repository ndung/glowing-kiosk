/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute card detection polling functions using ACR128
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 6, 2008
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

namespace PollingSample
{
    public partial class PollingSample : Form
    {


        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen; 
        public long pollCase;
        public bool autoDet;
        public bool dualPoll;
        public int nBytesRet;
        public ModWinsCard.SCARD_READERSTATE RdrState;

        public PollingSample()
        {
            InitializeComponent();
        }

        private void PollingSample_Load(object sender, EventArgs e)
        {

            InitMenu();

        }

        private void InitMenu()
        {

            connActive = false;
            autoDet = false;
            dualPoll = false;
            bConnect.Enabled = false;
            bReset.Enabled = false;
            bInit.Enabled = true;
            gbExMode.Enabled = false;
            gbCurrMode.Enabled = false;
            bSetExMode.Enabled = false;
            bGetExMode.Enabled = false;
            gbPollOpt.Enabled = false;
            cbPollOpt1.Enabled = true;
            cbPollOpt2.Enabled = true;
            cbPollOpt3.Enabled = true;
            cbPollOpt4.Enabled = true;
            cbPollOpt5.Enabled = true;
            cbPollOpt6.Enabled = true;
            gbPICCInt.Enabled = false;
            bReadPollOpt.Enabled = false;
            bSetPollOpt.Enabled = false;
            bManPoll.Enabled = false;
            bAutoPoll.Enabled = false;
            cbReader.Text = "";
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

                case 0:                                                  // Notifications only
                    mMsg.Items.Add(PrintText);
                    break;
                case 1:
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);     // Error Messages
                    mMsg.Items.Add(PrintText);
                    break;
                case 2:                                                // Input data
                    PrintText = "< " + PrintText;
                    mMsg.Items.Add(PrintText);
                    break;
                case 3:                                                // Output data
                    PrintText = "> " + PrintText;
                    mMsg.Items.Add(PrintText);
                    break;
                case 4:                                               // For ACOS1 error
                    mMsg.Items.Add(PrintText);
                    break;
                case 5:
                    tsMsg2.Text = PrintText;                          // ICC Polling Status
                    break;
                case 6:
                    tsMsg4.Text = PrintText;                         // PICC Polling Status
                    break;

            }

            mMsg.ForeColor = Color.Black;
            mMsg.Focus();

        }

        private void EnableButtons(int reqType)
        {

            switch (reqType)
            {

                case 0:
                    bInit.Enabled = false;
                    bConnect.Enabled = true;
                    bReset.Enabled = true;
                    break;

                case 1:
                    gbExMode.Enabled = true;
                    gbCurrMode.Enabled = true;
                    bSetExMode.Enabled = true;
                    bGetExMode.Enabled = true;
                    gbPollOpt.Enabled = true;
                    gbPICCInt.Enabled = true;
                    bReadPollOpt.Enabled = true;
                    bSetPollOpt.Enabled = true;
                    bAutoPoll.Enabled = true;
                    bManPoll.Enabled = true;
                    break;

            }

        }

        private void bInit_Click(object sender, EventArgs e)
        {

            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            // 1. Establish context and obtain hContext handle
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

            EnableButtons(0);

            byte[] ReadersList = new byte[pcchReaders];

            // Fill reader list
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, ReadersList, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(1,retCode,"");
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

                if (cbReader.SelectedIndex == 3)
                {

                    return;

                }

            }

            return; 


        }

        private void bConnect_Click(object sender, EventArgs e)
        {

            CallCardConnect(1);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            connActive = true;
            EnableButtons(1);

        }

        private int CallCardConnect(int reqType)
        {

            int intIndx;

            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            // Shared Connection
            retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                // Connect to SAM Reader
                if (reqType == 1)
                {

                    // check if ACR128 SAM is used and use Direct Mode if SAM is not detected
                    intIndx = 0;

                    cbReader.SelectedIndex = intIndx;

                    while (cbReader.SelectedIndex == 0)
                    {

                        if (intIndx == cbReader.Items.Count)
                        {

                            displayOut(0, 0, "Cannot find ACR128 SAM reader.");

                        }

                        intIndx = intIndx + 2;
                        cbReader.SelectedIndex = intIndx;
               

                    }

                    // Direct Connection
                    retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT, 0, ref hCard, ref Protocol);

                    if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                    {

                        displayOut(1, retCode, "");
                        connActive = false;
                        return retCode;             
                      
                    }

                    else
                    {

                        displayOut(0, 0, "Successful connection to " + cbReader.Text);

                    }
                }

                else
                {

                    displayOut(1, retCode, "");
                    connActive = false;
                    return retCode;

                }
            }

            else
            {

                displayOut(0, 0, "Successful connection to " + cbReader.Text);
                return retCode;

            }

            return retCode;
        
        }

        private int CallCardControl()
        {

            string tmpStr;
            int indx;

            // Display Apdu In
            tmpStr = "SCardControl: ";
            for (indx = 0; indx <= SendLen-1 ; indx++)
            {

                tmpStr = tmpStr + " " +string.Format("{0:X2}", SendBuff[indx]);

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

                displayOut(3, 0, tmpStr);

            }
            return retCode;

        }

        private void GetExMode(int reqType)
        {

            int indx;
            string tmpStr;

            ClearBuffers();

            SendBuff[0] = 0x2B;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 7;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            // Interpret Configuration Setting and Current Mode
            tmpStr = "";

            for (indx = 0; indx <= RecvLen - 1; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Equals("E1000000020000") | tmpStr.Equals("E1000000020100"))
            {

                if (reqType == 1)
                {

                    if (RecvBuff[5] == 0)
                    {

                        optBoth.Checked = true;
                    }

                    else
                    {

                        optEither.Checked = true;

                    }

                    if (RecvBuff[6] == 0)
                    {

                        optExNotActive.Checked = true;
                    }

                    else
                    {

                        optExActive.Checked = true;

                    }

                }
            }

            else
            {

                displayOut(4, 0, "Wrong return values from device.");

            }

        }

        private void bGetExMode_Click(object sender, EventArgs e)
        {

            GetExMode(1);

        }

        private void bSetExMode_Click(object sender, EventArgs e)
        {

            ClearBuffers();
            SendBuff[0] = 0x2B;
            SendBuff[1] = 0x01;

            if (optBoth.Checked == true)
            {

                SendBuff[2] = 0x00;

            }

            if (optEither.Checked == true)
            {

                SendBuff[2] = 0x01;

            }

            SendLen = 3;
            RecvLen = 7;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

        }

        private void ReadPollingOption(int reqType)
        {

            ClearBuffers();
            SendBuff[0] = 0x23  ;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;

            }

            if (reqType == 1)
            {

                // Interpret PICC Polling Setting
                if ((RecvBuff[5] & 0x01) != 0)
                {

                    displayOut(3, 0, "Automatic PICC polling is enabled.");
                    cbPollOpt1.Checked = true;
                }

                else
                {

                    displayOut(3, 0, "Automatic PICC polling is disabled.");
                    cbPollOpt1.Checked = false;

                }

                if ((RecvBuff[5] & 0x2) != 0)
                {

                    displayOut(3, 0, "Antenna off when no PICC found is enabled.");
                    cbPollOpt2.Checked = true;
                }

                else
                {

                    displayOut(3, 0, "Antenna off when no PICC found is disabled.");
                    cbPollOpt2.Checked = false;

                }

                if ((RecvBuff[5] & 0x4) != 0)
                {

                    displayOut(3, 0, "Antenna off when PICC is inactive is enabled.");
                    cbPollOpt3.Checked = true;
                }

                else
                {

                    displayOut(3, 0, "Antenna off when PICC is inactive is disabled.");
                    cbPollOpt3.Checked = false;

                }

                if ((RecvBuff[5] & 0x8) != 0)
                {

                    displayOut(3, 0, "Activate PICC when detected is enabled.");
                    cbPollOpt4.Checked = true;
                }

                else
                {

                    displayOut(3, 0, "Activate PICC when detected is disabled.");
                    cbPollOpt4.Checked = false;

                }

                if ((((RecvBuff[5] & 0x10) == 0) & ((RecvBuff[5] & 0x20) == 0)))
                {

                    displayOut(3, 0, "Poll interval is 250 msec.");
                    opt250.Checked = true;

                }

                if ((((RecvBuff[5] & 0x10) != 0) & ((RecvBuff[5]  & 0x20) == 0)))
                {

                    displayOut(3, 0, "Poll interval is 500 msec.");
                    opt500.Checked = true;

                }

                if ((((RecvBuff[5] & 0x10) == 0) & ((RecvBuff[5] & 0x20) != 0)))
                {

                    displayOut(3, 0, "Poll interval is 1 sec.");
                    opt1.Checked = true;

                }

                if ((((RecvBuff[5] & 0x10) != 0) & ((RecvBuff[5] & 0x20) != 0)))
                {

                    displayOut(3, 0, "Poll interval is 2.5 sec.");
                    opt25.Checked = true;

                }

                if ((RecvBuff[5] & 0x40) != 0)
                {

                    displayOut(3, 0, "Test Mode is enabled.");
                    cbPollOpt5.Checked = true;
                }

                else
                {

                    displayOut(3, 0, "Test Mode is disabled.");
                    cbPollOpt5.Checked = false;

                }

                if ((RecvBuff[5] & 0x80) != 0)
                {

                    displayOut(3, 0, "ISO14443A Part4 is enforced.");
                    cbPollOpt6.Checked = true;
                }

                else
                {

                    displayOut(3, 0, "ISO14443A Part4 is not enforced.");
                    cbPollOpt6.Checked = false;

                }

            }

        }

        private void bReadPollOpt_Click(object sender, EventArgs e)
        {

            ReadPollingOption(1);

        }

        private void bSetPollOpt_Click(object sender, EventArgs e)
        {

            ClearBuffers();
            SendBuff[0] = 0x23;
            SendBuff[1] = 0x01;

            if (cbPollOpt1.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x01);

            }

            if (cbPollOpt2.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x02);

            }

            if (cbPollOpt3.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x04);

            }

            if (cbPollOpt4.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x08);

            }

            if (opt500.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x10);

            }

            if (opt1.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x20);

            }

            if (opt25.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x10);
                SendBuff[2] = (byte)(SendBuff[2] | 0x20);

            }

            if (cbPollOpt5.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x40);

            }

            if (cbPollOpt6.Checked == true)
            {

                SendBuff[2] = (byte)(SendBuff[2] | 0x80);

            }

            SendLen = 3;
            RecvLen = 7;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        
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
            mMsg.Items.Clear();
            cbReader.Items.Clear();
            InitMenu();
                

        }

        private void bQuit_Click(object sender, EventArgs e)
        {

            // terminate the application
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);

        }

        private void bManPoll_Click(object sender, EventArgs e)
        {

            // Always use a valid connection for Card Control commands
            retCode = CallCardConnect(1);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;

            }

            ReadPollingOption(0);

            if ((RecvBuff[5] & 0x01 ) != 0)
            {

                displayOut(0, 0, "Turn off automatic PICC polling in the device before using this function.");
            }

            else
            {

                ClearBuffers();
                SendBuff[0] = 0x22;
                SendBuff[1] = 0x01;
                SendBuff[2] = 0xA;
                SendLen = 3;
                RecvLen = 6;
                retCode = CallCardControl();

                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {

                    return; 

                }

                if ((RecvBuff[5] & 0x01) != 0)
                {

                    displayOut(6, 0, "No card within range.");
                }

                else
                {

                    displayOut(6, 0, "Card is detected.");

                }

            }
       

        }

        private void bAutoPoll_Click(object sender, EventArgs e)
        {

            // pollCase legend
            // 1 = Both ICC and PICC can poll, but only one at a time
            // 2 = Only ICC can poll
            // 3 = Both reader can be polled

            if (autoDet)
            {

                autoDet = false;
                bAutoPoll.Text = "Start &Auto Detection";
                PollTimer.Enabled = false;
                displayOut(5, 0, "");
                displayOut(6, 0, "");
                return; 

            }

            // Always use a valid connection for Card Control commands
            retCode = CallCardConnect(1);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                bAutoPoll.Text = "Start &Auto Detection";
                autoDet = false;
                return; 
            }

            GetExMode(0);

            // Either ICC or PICC can be polled at any one time
            if (RecvBuff[5] != 0)   
            {

                ReadPollingOption(0);

                // Auto PICC polling is enabled
                if ((RecvBuff[5] & 0x01) != 0)
                {

                    // Either ICC and PICC can be polled
                    pollCase = 1;
                }

                else
                {

                    // Only ICC can be polled
                    pollCase = 2;

                }
            }

            // Both ICC and PICC can be enabled at the same time
            else
            {

                ReadPollingOption(0);

                // Auto PICC polling is enabled
                if ((RecvBuff[5] & 0x01) != 0)
                {

                    // Both ICC and PICC can be polled
                    pollCase = 3;
                }

                else
                {

                    // Only ICC can be polled
                    pollCase = 2;
                    
                }

            }

            switch (pollCase)
            {

                case 1:
                    displayOut(0, 0, "Either reader can detect cards, but not both.");
                    break;

                case 2:
                    displayOut(0, 0, "Automatic PICC polling is disabled, only ICC can detect card.");
                    break;

                case 3:
                    displayOut(0, 0, "Both ICC and PICC readers can automatically detect card.");
                    break;

            }

            autoDet = true;
            PollTimer.Enabled = true;
            bAutoPoll.Text = "End &Auto Detection";
       

        }

        private void PollTimer_Tick(object sender, EventArgs e)
        {

            int intIndx;

            switch (pollCase)
            {

                // Automatic PICC polling is disabled, only ICC can detect card
                case 2:
                    
                    // Connect to ICC reader
                    displayOut(6, 0, "Auto Polling is disabled.");
                    intIndx = 0;
                    cbReader.SelectedIndex = intIndx;

                    while (cbReader.SelectedIndex == 0)
                    {                    

                        if (intIndx == cbReader.Items.Count)
                        {

                            displayOut(0, 0, "Cannot find ACR128 ICC reader.");
                            PollTimer.Enabled = false;

                        }

                        intIndx = intIndx + 1;
                        cbReader.SelectedIndex = intIndx;

                    }


                    RdrState.RdrName = cbReader.Text;
                    retCode = ModWinsCard.SCardGetStatusChange(this.hContext, 0, ref RdrState, 1);

                    if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                    {

                        displayOut(1, retCode, "");
                        PollTimer.Enabled = false;
                        return;

                    }


                    if ((RdrState.RdrEventState & ModWinsCard.SCARD_STATE_PRESENT) != 0)
                    {

                        displayOut(5, 0, "Card is inserted.");
                    }

                    else
                    {

                        displayOut(5, 0, "Card is removed.");

                    }

                    break;

                // Both ICC and PICC readers can automatically detect card
                case 1:
                case 3:
                    
                    // Attempt to poll ICC reader
                    intIndx = 0;
                    cbReader.SelectedIndex = intIndx;

                    while (cbReader.SelectedIndex == 1)
                    {

                        if (intIndx == cbReader.Items.Count)
                        {

                            displayOut(0, 0, "Cannot find ACR128 ICC reader.");
                            PollTimer.Enabled = false;

                        }

                        intIndx = intIndx + 1;
                        cbReader.SelectedIndex = intIndx;

                    }


                    RdrState.RdrName = cbReader.Text;

                    retCode = ModWinsCard.SCardGetStatusChange(this.hContext, 0, ref RdrState, 1);

                    if (retCode == ModWinsCard.SCARD_S_SUCCESS)
                    {

                        if ((RdrState.RdrEventState & ModWinsCard.SCARD_STATE_PRESENT) != 0)
                        {

                            displayOut(5, 0, "Card is inserted.");

                        }

                        else
                        {

                            displayOut(5, 0, "Card is removed.");

                        }

                    }

                    // Attempt to poll PICC reader
                    intIndx = 0;
                    cbReader.SelectedIndex = intIndx;

                    while (cbReader.SelectedIndex == 0)
                    {

                        if (intIndx == cbReader.SelectedIndex)
                        {

                            displayOut(0, 0, "Cannot find ACR128 PICC reader.");
                            PollTimer.Enabled = false;

                        }

                        intIndx = intIndx + 1;
                        cbReader.SelectedIndex = intIndx;

                    }


                    RdrState.RdrName = cbReader.Text;

                    retCode = ModWinsCard.SCardGetStatusChange(hContext, 0, ref RdrState, 1);

                    if (retCode == ModWinsCard.SCARD_S_SUCCESS)
                    {

                        if ((RdrState.RdrEventState & ModWinsCard.SCARD_STATE_PRESENT) != 0)
                        {

                            displayOut(6, 0, "Card is detected.");
                        }

                        else
                        {

                            displayOut(6, 0, "No card within range.");

                        }

                    }

                    break;

            }

        }
        
    }

}