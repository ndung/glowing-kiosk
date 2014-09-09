/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                     get ATR from cards using ACR128
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

namespace GetATR
{
    public partial class GetATR : Form
    {

        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen, ATRLen, dwState, dwActProtocol;
        public byte[] ATRVal = new byte[257];
        
        public GetATR()
        {
            
            InitializeComponent();

        }

        private void GetATR_Load(object sender, EventArgs e)
        {

            InitMenu();

        }

        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:                                                 // Notifications
                    break;
                case 1:                                                 // Error Messages
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);     
                    break;
                case 2:                                                 // Input data
                    PrintText = "<" + PrintText;                    
                    break;
                case 3:
                    PrintText = ">" + PrintText;                       // Output data
                    break;
                case 4:
                    break;

            }

            mMsg.Items.Add(PrintText);
            mMsg.ForeColor = Color.Black;
            mMsg.Focus();

        }

        private void InitMenu()
        {
            
            connActive = false;
            cbReader.Text = "";
            mMsg.Text = "";
            mMsg.Items.Clear();
            bInit.Enabled = true;
            bConnect.Enabled = false;
            bGetAtr.Enabled = false;
            bClear.Enabled = false;
            displayOut(0, 0, "Program ready");

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;
            bGetAtr.Enabled = true;
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
                displayOut(0,0," ");
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

                if (String.Compare("ACR128 SAM", cbReader.Text) > 0)
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

        private void bGetAtr_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx;

            displayOut(0, 0, "Invoke Card Status");
            ATRLen = 33;

            retCode = ModWinsCard.SCardStatus(hCard, cbReader.Text, ref ReaderLen, ref dwState, ref dwActProtocol, ref ATRVal[0], ref ATRLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");
                System.Environment.Exit(0);

            }

            else
            {

                tmpStr = "ATR Length : " + ATRLen.ToString();
                displayOut(3, 0, tmpStr);

                tmpStr = "ATR Value : ";

                for (indx = 0; indx <= ATRLen - 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", ATRVal[indx]);
                  
                }

                displayOut(3, 0, tmpStr);

            }

            tmpStr = "Active Protocol: ";

            switch (dwActProtocol)
            {

                case 0:
                    tmpStr = tmpStr + "T=0";
                    break;
                case 1:
                    tmpStr = tmpStr + "T=1";    
                    break;

                default:
                    tmpStr = "No protocol is defined.";
                    break;
            }

            displayOut(3, 1, tmpStr);

        }

    }
    
}