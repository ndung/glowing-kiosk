using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PICCCardProg
{
    public partial class Form1 : Form
    {

        public int retCode, hContext, hCard, Protocol;
        public bool connActive, validATS;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        public ModWinsCard.SCARD_READERSTATE RdrState;
        public ModWinsCard.SCARD_IO_REQUEST pioSendRequest;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitMenu();
        }

        private void InitMenu()
        {

            connActive = false;
            validATS = false;
            cbReader.Items.Clear();
            cbReader.Text = "";
            mMsg.Items.Clear();
            displayOut(0, 0, "Program ready");
            bConnect.Enabled = false;
            bInit.Enabled = true;
            bReset.Enabled = false;
            cbIso14443A.Checked = false;
            gbGetData.Enabled = false;
            tCLA.Text = "";
            tINS.Text = "";
            tP1.Text = "";
            tP2.Text = "";
            tLc.Text = "";
            tLe.Text = "";
            tData.Text = "";
            gbSendApdu.Enabled = false;

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
                cbReader.SelectedIndex = 1;

            }
            indx = 1;

            // Look for ACR128 PICC and make it the default reader in the combobox

            for (indx = 1; indx <= cbReader.Items.Count - 1; indx++)
            {

                cbReader.SelectedIndex = indx;

                if (cbReader.Text == "ACS ACR128U PICC Interface 0")
                {
                    cbReader.SelectedIndex = 1;
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
            retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode == ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(0, 0, "Successful connection to " + cbReader.Text);
            }

            else
            {

                displayOut(0, 0, "The smart card has been removed, so that further communication is not possible.");

            }


            connActive = true;
            gbGetData.Enabled = true;
            gbSendApdu.Enabled = true;
            tCLA.Focus();
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

        private int SendAPDUandDisplay(int reqType)
        {

            int indx;
            string tmpStr;

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            // Display Apdu In
            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }

            displayOut(2, 0, tmpStr);
            retCode = ModWinsCard.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0], SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");
                return retCode;


            }

            else
            {

                tmpStr = "";
                switch (reqType)
                {

                    case 0:
                        for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                        }


                        if ((tmpStr).Trim() != "90 00")
                        {

                            displayOut(4, 0, "Return bytes are not acceptable.");

                        }

                        break;

                    case 1:

                        for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

                        }


                        if (tmpStr.Trim() != "90 00")
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                        }

                        else
                        {

                            tmpStr = "ATR : ";
                            for (indx = 0; indx <= (RecvLen - 3); indx++)
                            {

                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                            }

                        }

                        break;

                    case 2:

                        for (indx = 0; indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                        }

                        break;

                    case 3:

                        for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);


                        }


                        if (tmpStr.Trim() == "6A 81")
                        {

                            displayOut(0, 0, "The function is not supported.");
                            return retCode;
                        
                        }


                        if (tmpStr.Trim() == "63 00")
                        {

                            displayOut(0, 0, "The operation failed.");
                            return retCode;
  
                        }


                        validATS = true;
                        break;

                }

                displayOut(3, 0, tmpStr.Trim());

            }
            return retCode;


        }

        private void bGetData_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx;


            validATS = false;
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xCA;

            if (cbIso14443A.Checked)
            {

                SendBuff[2] = 0x01;
            }

            else
            {

                SendBuff[2] = 0x00;

            }

            SendBuff[3] = 0x00;
            SendBuff[4] = 0x00;

            SendLen = SendBuff[4] + 5;
            RecvLen = 0xFF;

            retCode = SendAPDUandDisplay(3);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }


            // Interpret and display return values
            if (validATS)
            {

                if (cbIso14443A.Checked)
                {

                    tmpStr = "UID: ";
                }

                else
                {

                    tmpStr = "ATS: ";

                }


                for (indx = 0; indx <= (RecvLen - 3); indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

                displayOut(3, 0, tmpStr.Trim());

            }

        }

        private string TrimInput(int TrimType, string StrIn)
        {

            int indx;
            string tmpstr;

            tmpstr = "";

            switch (TrimType)
            {

                case 0:

                    for (indx = 0; indx <= StrIn.Length-1; indx++)
                    {

                        if (((StrIn[indx] != Convert.ToChar(13)) & (StrIn[indx] != Convert.ToChar(10))))
                        {

                            tmpstr = tmpstr + StrIn[indx];

                        }

                    }

                    break;

                case 1:

                    for (indx = 0; indx <= StrIn.Length-1; indx++)
                    {

                        if (StrIn[indx] != ' ')
                        {

                            tmpstr = tmpstr + StrIn[indx];

                        }

                    }

                    break;

            }
            return tmpstr;

        }
   
        private void bSend_Click(object sender, EventArgs e)
         {

            string tmpData;
            bool directCmd;
            int indx;
            byte tmpLong;

            directCmd = true;

            // Validate inputs
            if (tCLA.Text == "")
            {

                tCLA.Text = "00";
                tCLA.Focus();
                return; 

            }

            if (!byte.TryParse(tCLA.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tCLA.Focus();
                tCLA.Text = "";
                return;

            }

            if (!byte.TryParse(tINS.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tINS.Focus();
                tINS.Text = "";
                return;

            }

            if (!byte.TryParse(tP1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tP1.Focus();
                tP1.Text = "";
                return;

            }

            if (!byte.TryParse(tP2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tP2.Focus();
                tP2.Text = "";
                return;

            }

            if (!byte.TryParse(tLc.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tLc.Focus();
                tLc.Text = "";
                return;

            }

            if (!byte.TryParse(tLe.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tLe.Focus();
                tLe.Text = "";
                return;

            }

            tmpData = "";

            ClearBuffers();

            SendBuff[0] = byte.Parse(tCLA.Text, System.Globalization.NumberStyles.HexNumber);           // CLA      

            if (tINS.Text != "")
            {

                SendBuff[1] = byte.Parse(tINS.Text, System.Globalization.NumberStyles.HexNumber);       //INS

            }

            if (tP1.Text != "")
            {

                directCmd = false;

            }

            if (directCmd == false) 
            {

                SendBuff[2] = byte.Parse(tP1.Text, System.Globalization.NumberStyles.HexNumber);       // P1

                if (tP2.Text == "")
                {

                    tP2.Text = "00";                                                                   // P2: Ask user to confirm
                    tP2.Focus();
                    return; 
                }

                else
                {

                    SendBuff[3] = byte.Parse(tP2.Text, System.Globalization.NumberStyles.HexNumber);     // P2

                }

                if (tLc.Text != "")
                {
                
                    SendBuff[4] = byte.Parse(tLc.Text, System.Globalization.NumberStyles.HexNumber);    // Lc    
                 
                    // Process Data In if Lc > 0
                    if (SendBuff[4] > 0)
                    {

                        tmpData = TrimInput(0, tData.Text);
                        tmpData = TrimInput(1, tmpData);
                        int tmpo = Math.Abs(tmpData.Length / 2);
                        string tmpstr = string.Empty;

                        // Check if Data In is consistent with Lc value
                        if (SendBuff[4] > tmpo)
                        {

                            tData.Focus();
                            return;

                        }

                        for (indx = 0; indx <= SendBuff[4] - 1; indx++)
                        {                          
 
                            tmpstr = tmpData.Substring(indx, 2);
                            SendBuff[indx + 5] = byte.Parse(tmpstr, System.Globalization.NumberStyles.HexNumber, null); // Format Data In

                        }

                        if (tLe.Text != "")
                        {

                            SendBuff[SendBuff[4] + 5] = byte.Parse(tLe.Text, System.Globalization.NumberStyles.HexNumber);  //Le      
                            

                        }
                    }

                    else
                    {

                        if (tLe.Text != "")
                        {

                            SendBuff[5] = byte.Parse(tLe.Text, System.Globalization.NumberStyles.HexNumber);  // Le                              

                        }

                    }
                }

                else
                {

                    if (tLe.Text != "")
                    {

                        SendBuff[4] = byte.Parse(tLe.Text, System.Globalization.NumberStyles.HexNumber);     // Le   
            
                    }

                }

            }

            if (directCmd == true)
            {

                if (tINS.Text == "")
                {

                    SendLen = 0x01;
                }

                else
                {

                    SendLen = 0x02;

                }
            }

            else
            {

                if (tLc.Text == "")
                {

                    if (tLe.Text != "")
                    {

                        SendLen = 5;
                    }

                    else
                    {

                        SendLen = 4;

                    }
                }

                else
                {

                    if (tLe.Text == "")
                    {

                        SendLen = SendBuff[4] + 5;
                    }

                    else
                    {

                        SendLen = SendBuff[4] + 6;

                    }

                }

            }

            RecvLen = 0xFF;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;
            }

        }

    }
}