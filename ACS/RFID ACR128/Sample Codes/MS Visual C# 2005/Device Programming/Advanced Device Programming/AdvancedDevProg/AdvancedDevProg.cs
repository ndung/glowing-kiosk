/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute advanced device-specific functions of ACR128
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 17, 2008
'
'  Revision Trail:  (Date/Author/Description) 
'
'=========================================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdvDevProg
{
    public partial class AdvDevProg : Form
    {
        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen;
        public int RecvLen;
        public int nBytesRet;
        public int reqType;
        public ModWinsCard.SCARD_READERSTATE RdrState;

        public AdvDevProg()
        {
            InitializeComponent();
        }

        private void AdvDevProg_Load(object sender, EventArgs e)
        {

            InitMenu();

        }
    
        private void InitMenu()
        {

            connActive = false;
            autoDet = false;
            Polltimer.Enabled = false;
            cbReader.Items.Clear();
            mMsg.Items.Clear();
            displayOut(0, 0, "Program ready");
            bConnect.Enabled = false;
            bInit.Enabled = true;
            bReset.Enabled = false;
            tFWI.Text = "";
            tPollTO.Text = "";
            tTFS.Text = "";
            gbFWI.Enabled = false;
            rbAntOn.Checked = false;
            rbAntOff.Checked = false;
            gbAntenna.Enabled = false;
            tFStop.Text = "";
            tSetup.Text = "";
            cbFilter.Checked = false;
            tRecGain.Text = "";
            gbTransSet.Enabled = false;
            tPICC1.Text = "";
            tPICC2.Text = "";
            tPICC3.Text = "";
            tPICC4.Text = "";
            tPICC5.Text = "";
            tPICC6.Text = "";
            tPICC7.Text = "";
            tPICC8.Text = "";
            tPICC9.Text = "";
            tPICC10.Text = "";
            tPICC11.Text = "";
            tPICC12.Text = "";
            gbPICC.Enabled = false;
            tMsg.Text = "";
            rbType1.Checked = false;
            rbType2.Checked = false;
            rbType3.Checked = false;
            gbPolling.Enabled = false;
            gbErrHand.Enabled = false;
            gbPPS.Enabled = false;
            tRegNo.Text = "";
            tRegVal.Text = "";
            gbReg.Enabled = false;
            rbRSI1.Checked = false;
            rbRSI2.Checked = false;
            rbRSI3.Checked = false;
            gbRefIS.Enabled = false;

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;
            bReset.Enabled = true;
            bClear.Enabled = true;

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

                if (cbReader.SelectedIndex == 2)
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
            gbFWI.Enabled = true;
            gbAntenna.Enabled = true;
            gbTransSet.Enabled = true;
            gbPICC.Enabled = true;
            gbPolling.Enabled = true;
            rbType3.Checked = true;
            gbErrHand.Enabled = true;
            gbPPS.Enabled = true;
            gbReg.Enabled = true;
            gbRefIS.Enabled = true;
            rbRSI3.Checked = true;
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
                displayOut(3, 0, tmpStr.Trim());

            }
            return retCode;

        }


        private void bGetFWI_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

            ClearBuffers();

            SendBuff[0] = 0x1F;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 8;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }


            tmpStr = "";

            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}",RecvBuff[indx]);

            }

            if (tmpStr == "E100000003")
            {

                // Interpret response data
                tFWI.Text = string.Format("{0:X2}",RecvBuff[5]);
                tPollTO.Text = string.Format("{0:X2}", RecvBuff[6]);
                tTFS.Text = string.Format("{0:X2}", RecvBuff[7]);
                displayOut(3, 0, tmpStr);
            }

            else
            {

                tFWI.Text = "";
                tPollTO.Text = "";
                tTFS.Text = "";
                displayOut(3, 0, "Invalid response");

            }

        }

        private void bSetFWI_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;
            byte tmpLong;


            if (tFWI.Text == "" | !byte.TryParse(tFWI.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tFWI.SelectAll();
                tFWI.Focus();
                tFWI.Text = "";
                return;

            }

            if ( tPollTO.Text == "" | !byte.TryParse(tPollTO.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPollTO.SelectAll();
                tPollTO.Focus();
                tPollTO.Text = "";
                return; 

            }

            if (tTFS.Text == "" | !byte.TryParse(tTFS.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tTFS.SelectAll();
                tTFS.Focus();
                tTFS.Text = "";
                return; 

            }

            ClearBuffers();
            SendBuff[0] = 0x1F;
            SendBuff[1] = 0x03;
            SendBuff[2] = byte.Parse(tFWI.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[3] = byte.Parse(tPollTO.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[4] = byte.Parse(tTFS.Text, System.Globalization.NumberStyles.HexNumber);
            SendLen = 5;
            RecvLen = 8;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";

            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }
            
            if ((tmpStr != "E100000003"))
            {

                displayOut(3, 0, "Invalid Response");

            }
       
        }

        private void bGetAS_Click(object sender, EventArgs e)
        {
            int indx;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x25;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 6;

            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";

            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }
         
            if ((tmpStr == "E100000001"))
            {

                // Interpret response data
                if (RecvBuff[5] == 0)
                {

                    rbAntOff.Checked = true;
                }

                else
                {

                    rbAntOn.Checked = true;

                }
            }

            else
            {

                rbAntOff.Checked = false;
                rbAntOn.Checked = false;
                displayOut(3, 0, "Invalid Response");

            }
        }

        private void ReadPollingOption()
        {

            ClearBuffers();
            SendBuff[0] = 0x23;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 6;

            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;

            }

        }

        private void bSetAS_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

            ReadPollingOption();

            if ((RecvBuff[5] & 0x01) != 0)
            {

                displayOut(0, 0, "Turn off automatic PICC Polling in the device before using this function.");
                return; 

            }

            ClearBuffers();
            SendBuff[0] = 0x25;
            SendBuff[1] = 0x01;

            if (rbAntOn.Checked)
            {

                SendBuff[2] = 0x01;
            }

            else
            {

                if (rbAntOff.Checked)
                {

                    SendBuff[2] = 0x00;
                }

                else
                {

                    rbAntOn.Focus();
                    return; 

                }


            }

            SendLen = 3;
            RecvLen = 6;

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";

            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr != "E100000001")
            {

                displayOut(3, 0, "Invalid response");

            }

        }

        private void bGetTranSet_Click(object sender, EventArgs e)
        {

            int indx;
            int tmpVal;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x20;
            SendBuff[1] = 0x01;
            SendLen = 2;
            RecvLen = 9;

            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";

            for (indx = 0; indx <= 5; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr != "E100000001")
            {

                // Interpret resonse data
                tmpVal = RecvBuff[6] >> 4;
                tFStop.Text = tmpVal.ToString();
                tmpVal = RecvBuff[6] & 0x0F;
                tSetup.Text = tmpVal.ToString();

                if ((RecvBuff[7] & 0x04) != 0)
                {

                    cbFilter.Checked = true;
                }

                else
                {

                    cbFilter.Checked = false;

                }
                tmpVal = RecvBuff[7] & 0x03;
                tRecGain.Text = tmpVal.ToString();
                tTxMode.Text = string.Format("{0:X2}", RecvBuff[indx]);
            }

            else
            {

                tFStop.Text = "";
                tSetup.Text = "";
                cbFilter.Checked = false;
                tRecGain.Text = "";
                tTxMode.Text = "";
                displayOut(3, 0, "Invalid response");

            }
    
        }

        private void bSetTranSet_Click(object sender, EventArgs e)
        {

            int tempInt;
            byte tmpLong;

            if (tFStop.Text == "" | !int.TryParse(tFStop.Text, out tempInt))
            {

                tFStop.Focus();
                tFStop.Text = "";
                return;

            }

            if (tSetup.Text == "" | !int.TryParse(tSetup.Text, out tempInt))
            {

                tSetup.Focus();
                tSetup.Text = "";
                return;

            }

            if (tRecGain.Text == "" | !int.TryParse(tRecGain.Text, out tempInt))
            {

                tRecGain.Focus();
                tRecGain.Text = "";
                return;

            }


            if (tTxMode.Text == "" | !byte.TryParse(tTxMode.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tTxMode.Focus();
                tTxMode.Text = "";
                return; 

            }

            if (int.Parse(tFStop.Text) > 15)
            {

                tFStop.Text = "15";
                tFStop.Focus();

            }

            if (int.Parse(tSetup.Text) > 15)
            {

                tSetup.Text = "15";
                tSetup.Focus();

            }

            if (int.Parse(tRecGain.Text) > 3)
            {

                tRecGain.Text = "3";
                tRecGain.Focus();

            }

            ClearBuffers();
            SendBuff[0] = 0x20;
            SendBuff[1] = 0x04;
            SendBuff[2] = 0x06;
            SendBuff[3] = (byte)(int.Parse(tFStop.Text));
            SendBuff[3] = (byte)(SendBuff[3] + int.Parse(tSetup.Text));

            if (cbFilter.Checked)
            {

                SendBuff[4] = 4;

            }

            SendBuff[4] = (byte)(SendBuff[4] + int.Parse(tRecGain.Text));
            SendBuff[5] = byte.Parse(tTxMode.Text, System.Globalization.NumberStyles.HexNumber);
            SendLen = 6;
            RecvLen = 5;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }


            if (RecvBuff[0] != 0xE1)
            {

                displayOut(3, 0, "Invalid response");

            }
        
        }

        private void bGetEH_Click(object sender, EventArgs e)
        {

            int indx;
            int tmpVal;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x2C;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 7;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";

            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr == "E100000002") && (RecvBuff[6] != 0x7F))
            {

                // Interpret response data
                tmpVal = RecvBuff[5] >> 4;
                tPc2Pi.Text = (tmpVal).ToString();
                tmpVal = RecvBuff[5] & 0x03;
                tPi2PC.Text = (tmpVal).ToString();
            }

            else
            {

                tPc2Pi.Text = "";
                tPi2PC.Text = "";
                displayOut(3, 0, "Invalid response");

            }
        
        }

        private void bSetEH_Click(object sender, EventArgs e)
        {
            
            int indx;
            int tempInt;
            string tmpStr;

            if (tPc2Pi.Text == "" | !int.TryParse(tPc2Pi.Text, out tempInt))
            {

                tPc2Pi.Focus();
                tPc2Pi.Text = "";
                return;

            }

            if (tPi2PC.Text == "" | !int.TryParse(tPi2PC.Text, out tempInt))
            {
                tPi2PC.Focus();
                tPi2PC.Text = "";
                return;

            }

            if (int.Parse(tPc2Pi.Text) > 3)
            {

                tPc2Pi.Text = "3";
                tPc2Pi.Focus();

            }

            if (int.Parse(tPi2PC.Text) > 3)
            {

                tPi2PC.Text = "3";
                tPi2PC.Focus();

            }

            ClearBuffers();
            SendBuff[0] = 0x2C;
            SendBuff[1] = 0x02;
            SendBuff[2] = (byte)((int.Parse(tPc2Pi.Text)) << 4);
            SendBuff[2] = (byte)(SendBuff[2] + int.Parse(tPi2PC.Text));
            SendLen = 4;
            RecvLen = 7;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr != "E100000002"))
            {

                displayOut(3, 0, "Invalid response");

            }
       
        }

        private void bGetPICC_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x2A;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 17;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr == "E10000000C"))
            {

                // Interpret response data
                tPICC1.Text = string.Format("{0:X2}", RecvBuff[5]);
                tPICC2.Text = string.Format("{0:X2}", RecvBuff[6]);
                tPICC3.Text = string.Format("{0:X2}", RecvBuff[7]);
                tPICC4.Text = string.Format("{0:X2}", RecvBuff[8]);
                tPICC5.Text = string.Format("{0:X2}", RecvBuff[9]);
                tPICC6.Text = string.Format("{0:X2}", RecvBuff[10]);
                tPICC7.Text = string.Format("{0:X2}", RecvBuff[11]);
                tPICC8.Text = string.Format("{0:X2}", RecvBuff[12]);
                tPICC9.Text = string.Format("{0:X2}", RecvBuff[13]);
                tPICC10.Text = string.Format("{0:X2}",RecvBuff[14]);
                tPICC11.Text = string.Format("{0:X2}",RecvBuff[15]);
                tPICC12.Text = string.Format("{0:X2}",RecvBuff[16]);
            }

            else
            {

                tPICC1.Text = "";
                tPICC2.Text = "";
                tPICC3.Text = "";
                tPICC4.Text = "";
                tPICC5.Text = "";
                tPICC6.Text = "";
                tPICC7.Text = "";
                tPICC8.Text = "";
                tPICC9.Text = "";
                tPICC10.Text = "";
                tPICC11.Text = "";
                tPICC12.Text = "";
                displayOut(3, 0, "Invalid response");

            }
     
        }

        private void bSetPICC_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;
            byte tmpLong;

            if (tPICC1.Text == "" | !byte.TryParse(tPICC1.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC1.Focus();
                tPICC1.Text = "";
                return;

            }

            if (tPICC2.Text == "" | !byte.TryParse(tPICC2.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC2.Focus();
                tPICC2.Text = "";
                return;


            }

            if (tPICC3.Text == "" | !byte.TryParse(tPICC3.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC3.Focus();
                tPICC3.Text ="";
                return;
                    
            }

            if (tPICC4.Text == "" | !byte.TryParse(tPICC4.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC4.Focus();
                tPICC4.Text = "";
                return;

            }

            if (tPICC5.Text == "" | !byte.TryParse(tPICC5.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC5.Focus();
                tPICC5.Text = "";
                return;
            }

            if (tPICC6.Text == "" | !byte.TryParse(tPICC6.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC6.Focus();
                tPICC6.Text = "";
                return;
            }

            if (tPICC7.Text == "" | !byte.TryParse(tPICC7.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {
                
                tPICC7.Focus();
                tPICC7.Text = "";
                return;

            }

            if (tPICC8.Text == "" | !byte.TryParse(tPICC8.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC8.Focus();
                tPICC8.Text = "";
                return;

            }

            if (tPICC9.Text == "" | !byte.TryParse(tPICC9.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC9.Focus();
                tPICC9.Text = "";
                return;

            }

            if (tPICC10.Text == "" | !byte.TryParse(tPICC10.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC10.Focus();
                tPICC10.Text = "";
                return;
            }

            if (tPICC11.Text == "" | !byte.TryParse(tPICC11.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC11.Focus();
                tPICC11.Text = "";
                return;
            }

            if (tPICC12.Text == "" | !byte.TryParse(tPICC12.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tPICC12.Focus();
                tPICC12.Text = "";
                return;

            }

            ClearBuffers();
            SendBuff[0] = 0x2A;
            SendBuff[1] = 0x0C;
            SendBuff[2] = byte.Parse(tPICC1.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[3] = byte.Parse(tPICC2.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[4] = byte.Parse(tPICC3.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[5] = byte.Parse(tPICC4.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[6] = byte.Parse(tPICC5.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[7] = byte.Parse(tPICC6.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[8] = byte.Parse(tPICC7.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[9] = byte.Parse(tPICC8.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[10] = byte.Parse(tPICC9.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[11] = byte.Parse(tPICC10.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[12] = byte.Parse(tPICC11.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[13] = byte.Parse(tPICC12.Text, System.Globalization.NumberStyles.HexNumber);
            SendLen = 14;
            RecvLen = 17;

            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr != "E10000000C"))
            {

                displayOut(3, 0, "Invalid response");

            }
      
        }

        private void bGetPSet_Click(object sender, EventArgs e)
        {
            int indx;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x20;
            SendBuff[1] = 0x00;
            SendBuff[3] = 0xFF;
            SendLen = 4;
            RecvLen = 6;

            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr != "E100000001"))
            {

                mMsg.Text = "Invalid Card Detected";
                return;

            }

            switch (RecvBuff[5])
            {

                case 1:
                    rbType1.Checked = true;
                    break;
                case 2:
                    rbType2.Checked = true;
                    break;
                default:
                    rbType3.Checked = true;
                    break;

            }
        
        }

        private void bSetPSet_Click(object sender, EventArgs e)
        {
            if (rbType1.Checked)
            {

                reqType = 1;
            }

            else
            {

                if (rbType2.Checked)
                {

                    reqType = 2;
                }

                else
                {

                    if (rbType3.Checked)
                    {

                        reqType = 3;
                    }

                    else
                    {

                        rbType1.Focus();
                        return; 

                    }

                }

            }

            ClearBuffers();
            SendBuff[0] = 0x20;
            SendBuff[1] = 0x02;

            switch (reqType)
            {

                case 1:
                    SendBuff[2] = 0x01;
                    break;
                case 2:
                    SendBuff[2] = 0x02;
                    break;
                default:
                    SendBuff[2] = 0x03;
                    break;

            }

            SendBuff[3] = 0xFF;
            SendLen = 4;
            RecvLen = 5;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        
        }

        private void bPoll_Click(object sender, EventArgs e)
        {
            if (autoDet)
            {

                autoDet = false;
                bPoll.Text = "Start Auto &Detection";
                Polltimer.Enabled = false;
                tMsg.Text = "Polling stopped...";
                return; 

            }

            tMsg.Text = "Polling started...";
            autoDet = true;
            Polltimer.Enabled = true;
            bPoll.Text = "End Auto &Detection";
        
        }

        private void bGetPPS_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x24;
            SendBuff[1] = 0x00;
            SendLen = 2;
            RecvLen = 7;

            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr == "E100000002"))
            {

                // Interpret response data
                switch (RecvBuff[5])
                {

                    case 0:
                        rbMaxSpeed1.Checked = true;
                        break;
                    case 1:
                        rbMaxSpeed2.Checked = true;
                        break;
                    case 2:
                        rbMaxSpeed3.Checked = true;
                        break;
                    case 3:
                        rbMaxSpeed4.Checked = true;
                        break;
                    default:
                        rbMaxSpeed5.Checked = true;
                        break;

                }

                switch (RecvBuff[6])
                {

                    case 0:
                        rbCurrSpeed1.Checked = true;
                        break;
                    case 1:
                        rbCurrSpeed2.Checked = true;
                        break;
                    case 2:
                        rbCurrSpeed3.Checked = true;
                        break;
                    case 3:
                        rbCurrSpeed4.Checked = true;
                        break;
                    default:
                        rbCurrSpeed5.Checked = true;
                        break;


                }
            }


            else
            {

                displayOut(3, 0, "Invalid response");

            }
        
        }

        private void bSetPPS_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

            if (((rbMaxSpeed1.Checked == false) & (rbMaxSpeed2.Checked == false) & (rbMaxSpeed3.Checked == false) & (rbMaxSpeed4.Checked == false)))
            {

                rbMaxSpeed5.Checked = true;

            }

            if (((rbCurrSpeed1.Checked == false) & (rbCurrSpeed2.Checked == false) & (rbCurrSpeed3.Checked == false) & (rbCurrSpeed4.Checked == false)))
            {

                rbCurrSpeed5.Checked = true;

            }

            ClearBuffers();
            SendBuff[0] = 0x24;
            SendBuff[1] = 0x01;

            if (rbMaxSpeed1.Checked == true)
            {

                SendBuff[2] = 0x00;

            }

            if (rbMaxSpeed2.Checked == true)
            {

                SendBuff[2] = 0x01;

            }

            if (rbMaxSpeed3.Checked == true)
            {

                SendBuff[2] = 0x02;

            }

            if (rbMaxSpeed4.Checked == true)
            {

                SendBuff[2] = 0x03;

            }

            if (rbMaxSpeed5.Checked == true)
            {

                SendBuff[2] = 0xFF;

            }

            SendLen = 3;
            RecvLen = 7;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr != "E100000002"))
            {

                displayOut(3, 0, "Invalid Response");

            }
        
        }

        private void bGetReg_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

             if (tRegNo.Text == "") 
             {

                 tRegNo.Focus();
                 return; 
             }

            ClearBuffers();
            SendBuff[0] = 0x19;
            SendBuff[1] = 0x01;
            SendBuff[2] = byte.Parse(tRegNo.Text, System.Globalization.NumberStyles.HexNumber);
            SendLen = 3;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }


            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr == "E100000001"))
            {

                // Interpret response data
                tRegVal.Text = string.Format("{0:X2}", RecvBuff[5]);
            }

            else
            {
                tRegVal.Text = "";
                displayOut(3, 0, "Invalid Response");

            }

        }

        private void bSetReg_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;
            byte tmpLong;

            if (tRegNo.Text == "")
            {

                tRegNo.Focus();
                return;

            }

            if (tRegVal.Text == "")
            {

                tRegVal.Focus();
                return;

            }

            if (!byte.TryParse(tRegNo.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tRegNo.Focus();
                tRegNo.Text = "";
                return; 

            }

            if (!byte.TryParse(tRegVal.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tRegVal.Focus();
                tRegVal.Text = "";
                return; 

            }

            ClearBuffers();
            SendBuff[0] = 0x1A;
            SendBuff[1] = 0x02;
            SendBuff[2] = byte.Parse(tRegNo.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[3] = byte.Parse(tRegVal.Text, System.Globalization.NumberStyles.HexNumber);
            SendLen = 4;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr == "E100000001"))
            {

                tRegVal.Text = string.Format("{0:X2}", RecvBuff[5]);
            }

            else
            {

                tRegNo.Text = "";
                tRegVal.Text = "";
                displayOut(3, 0, "Invalid response");

            }
        
        }

        private void bRefIS_Click(object sender, EventArgs e)
        {

            int indx=0;
            string tmpStr;

            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            cbReader.SelectedIndex = indx;


            while (cbReader.SelectedIndex == 0)
            {

                if (indx == cbReader.Items.Count)
                {

                    displayOut(0, 0, "Cannot find ACR128 SAM reader.");
                    connActive = false;

                }

                indx = indx + 2;
                cbReader.SelectedIndex = indx;

            }

            // 1. For SAM Refresh, connect to SAM Interface in direct mode

            if (rbRSI3.Checked)
            {

                retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT, 0, ref hCard, ref Protocol);

                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {

                    displayOut(1, retCode, "");
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

                // 2. For other interfaces, connect to SAM Interface in direct or shared mode
                retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT | ModWinsCard.SCARD_SHARE_SHARED, 0, ref hCard, ref Protocol);

                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {

                    displayOut(1, retCode, "");
                    connActive = false;
                    return; 
                }

                else
                {

                    displayOut(0, 0, "Successful connection to " + cbReader.Text);

                }

            }

            ClearBuffers();
            SendBuff[0] = 0x2D;
            SendBuff[1] = 0x01;

            if (rbRSI1.Checked)
            {

                SendBuff[2] = 0x01;                // bit 0
                
            }

            else
            {

                if (rbRSI2.Checked)
                {

                    SendBuff[2] = 0x02;             // bit 1
                   
                }

                else
                {

                    SendBuff[2] = 0x04;            // bit 2
                    

                }

            }

            SendLen = 3;
            RecvLen = 6;
            retCode = CallCardControl();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;

            }

            tmpStr = "";
            for (indx = 0; indx <= 4; indx++)
            {

                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if ((tmpStr != "E100000001"))
            {

                displayOut(3, 0, "Invalid response");

            }

            // 3. For SAM interface, disconnect and connect to SAM Interface in direct or shared mode
            if (rbRSI3.Checked)
            {

                if (connActive)
                {

                    retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

                }

                indx = 0;
                cbReader.SelectedIndex = indx;

                while (cbReader.SelectedIndex == 0)
                {

                    if (indx == cbReader.Items.Count)
                    {

                        displayOut(0, 0, "Cannot find ACR128 SAM reader.");
                        connActive = false;

                    }

                    indx = indx + 2;
                    cbReader.SelectedIndex = indx;

                }

                retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT | ModWinsCard.SCARD_SHARE_SHARED, 0, ref hCard, ref Protocol);

                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {

                    displayOut(1, retCode, "");
                    connActive = false;
                    return; 
                }

                else
                {
                    displayOut(0, 0, "Successful connection to " + cbReader.Text);
                }

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

            InitMenu();
        
        }

        private void bQuit_Click(object sender, EventArgs e)
        {

            // terminate the application
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);
        
        
        }

        private void Polltimer_Tick(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;

            indx = 0;
            cbReader.SelectedIndex = indx;

            while (cbReader.SelectedIndex == 0)
            {
                if (indx == cbReader.Items.Count)
                {

                    displayOut(0, 0, "Cannot find ACR128 ICC reader.");
                    Polltimer.Enabled = false;

                }

                indx = indx + 1;

                cbReader.SelectedIndex = indx;

            }

            RdrState.RdrName = cbReader.Text;
            retCode = ModWinsCard.SCardGetStatusChange(hContext, 0, ref RdrState, 1);

            if (retCode == ModWinsCard.SCARD_S_SUCCESS)
            {

                if ((RdrState.RdrEventState & ModWinsCard.SCARD_STATE_PRESENT) != 0)
                {

                    switch (reqType)
                    {

                        case 1:
                            tmpStr = "ISO14443 Type A card";
                            break;
                        case 2:
                            tmpStr = "ISO14443 Type B card";
                            break;
                        default:
                            tmpStr = "ISO14443 card";
                            break;

                    }

                    tMsg.Text = tmpStr + " is detected";
                }


                else
                {

                    tMsg.Text = "No Card within range.";

                }
            }
        }

     
    }
}
