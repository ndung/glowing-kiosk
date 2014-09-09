/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   implement the binary file support in ACOS3-24K
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 23, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'==========================================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ACOSBinary
{
    public partial class MainACOSBin : Form
    {

        public int retCode, hContext, hCard, Protocol;
        public bool connActive, validATS;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        public ModWinsCard.SCARD_IO_REQUEST pioSendRequest;

        public MainACOSBin()
        {
            InitializeComponent();
        }

        private void MainACOSBin_Load(object sender, EventArgs e)
        {
            InitMenu();
        }

        private void InitMenu() 
        {

            connActive = false;
            cbReader.Items.Clear();
            mMsg.Items.Clear();
            displayOut(0, 0, "Program ready");
            bInit.Enabled = true;
            bConnect.Enabled = false;
            bReset.Enabled = false;
            tFileID1.Text = "";
            tFileID2.Text = "";
            tFileLen1.Text = "";
            tFileLen2.Text = "";
            gbFormat.Enabled = false;
            tFID1.Text = "";
            tFID2.Text = "";
            tOffset1.Text = "";
            tOffset2.Text = "";
            tLen.Text = "";
            tData.Clear();
            gbReadWrite.Enabled = false;

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
                cbReader.SelectedIndex = 0;

            }
         
        }

        private int SubmitIC()
        {

            int indx;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x80;                 // CLA
            SendBuff[1] = 0x20;                 // INS            
            SendBuff[2] = 0x07;                 // P1         
            SendBuff[3] = 0x00;                 // P2        
            SendBuff[4] = 0x08;                 // P3        
            SendBuff[5] = 0x41;                 // A        
            SendBuff[6] = 0x43;                 // C          
            SendBuff[7] = 0x4F;                 // O          
            SendBuff[8] = 0x53;                 // S       
            SendBuff[9] = 0x54;                 // T
            SendBuff[10] = 0x45;                // E         
            SendBuff[11] = 0x53;                // S         
            SendBuff[12] = 0x54;                // T
          
            SendLen = 0x0D;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(0);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return retCode;

            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "90 00")
            {

                displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return retCode;
            }
            return retCode;

        }

        private void getBinaryData()
        {

            int indx;
            int tmpLen;
            string tmpStr;


            // 1. Send IC Code
            retCode = SubmitIC();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                
                displayOut(0, 0, "Insert ACOS3-24K card on contact card reader.");

            }

            // 2. Select FF 04
            retCode = selectFile(0xFF, 0x04);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return; 
            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "90 00")
            {

                displayOut(4, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return; 

            }

            // 3. Read first record
            retCode = readRecord(0x00, 0x07);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(0, 0, "Card may not have been formatted yet.");
                return; 
            }

            // Provide parameter to Data Input Box
            tFID1.Text = string.Format("{0:X2}", RecvBuff[4]);
            tFID2.Text = string.Format("{0:X2}", RecvBuff[5]);
            tmpLen = RecvBuff[1];
            tmpLen = tmpLen + (RecvBuff[0] * 256);
            tData.MaxLength = tmpLen;

        }

        private int readRecord(byte RecNo, byte DataLen)
        {

            int indx;
            string tmpStr;

            ClearBuffers();
            SendBuff[0] = 0x80;                        // CLA
            SendBuff[1] = 0xB2;                        // INS
            SendBuff[2] = RecNo;                       // P1
            SendBuff[3] = 0x00;                        // P2
            SendBuff[4] = DataLen;                     // P3
         
            SendLen = 5;
            RecvLen = SendBuff[4] + 2;

            retCode = SendAPDUandDisplay(0);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return retCode;
                
            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + (string.Format("{0:X2}", (RecvBuff[indx + SendBuff[4]])));

            }

            if (tmpStr.Trim() != "90 00")
            {
                displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
            }
            return retCode;

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

                }

                displayOut(3, 0, tmpStr.Trim());

            }
            return retCode;

        }

        private int selectFile(byte HiAddr, byte LoAddr)
        {

            ClearBuffers();
            SendBuff[0] = 0x80;                 // CLA
            SendBuff[1] = 0xA4;                 // INS
            SendBuff[2] = 0x00;                 // P1
            SendBuff[3] = 0x00;                 // P2
            SendBuff[4] = 0x02;                 // P3
            SendBuff[5] = HiAddr;               // Value of High Byte
            SendBuff[6] = LoAddr;               // Value of Low Byte
 
            SendLen = 0x07;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(2);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return retCode;

            }
            return retCode;

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
            gbFormat.Enabled = true;
            gbReadWrite.Enabled = true;
            getBinaryData();        
            
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

        private int writeRecord(int caseType, byte RecNo, byte maxDataLen, byte DataLen, byte[] DataIn)
        {

            int indx;
            string tmpStr;

            if (caseType == 1)
            {
                // If card data is to be erased before writing new data
                // 1. Re-initialize card values to &H0
                ClearBuffers();
                SendBuff[0] = 0x80;             // CLA
                SendBuff[1] = 0xD2;             // INS
                SendBuff[2] = RecNo;            // P1   
                SendBuff[3] = 0x00;             // P2 
                SendBuff[4] = maxDataLen;       // P3    Length 


                for (indx = 0; indx <= maxDataLen - 1; indx++)
                {
                    SendBuff[indx + 5] = 0;
                }
                SendLen = maxDataLen + 5;
                RecvLen = 2;

                retCode = SendAPDUandDisplay(2);
                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {

                    return retCode;
                }

                tmpStr = "";
                for (indx = 0; indx <= 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

                if (tmpStr.Trim() != "90 00")
                {

                    displayOut(3, 0, "The return string is invalid. Value: " + tmpStr);
                    retCode = -450;
                    return retCode;
                }

            }

            // 2. Write data to card
            ClearBuffers();
            SendBuff[0] = 0x80;             // CLA
            SendBuff[1]= 0xD2;              // INS
            SendBuff[2] = RecNo;            // P1
            SendBuff[3] = 0x00;             // P2
            SendBuff[4] = DataLen;          // P3    Length 
         

            for (indx = 0; indx <= DataLen - 1; indx++)
            {

                SendBuff[indx + 5] = DataIn[indx];

            }

            SendLen = DataLen + 5;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(0);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return retCode;

            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "90 00")
            {

                displayOut(3, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return retCode;

            }
            return retCode;

        }
    
        private void bFormat_Click(object sender, EventArgs e)
        {

            int indx;
            string tmpStr;
            byte[] tmpArray = new byte[31];
            byte tmpLong;

            // Validate Inputs
            if ((tFileID1.Text == "" | !byte.TryParse(tFileID1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tFileID1.Focus();
                tFileID1.Text = "";
                return; 

            }

            if ((tFileID2.Text == "" | !byte.TryParse(tFileID2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tFileID2.Focus();
                tFileID2.Text = "";
                return;

            }

            if ((tFileLen2.Text == "" | !byte.TryParse(tFileLen2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tFileLen2.Focus();
                tFileLen2.Text = "";
                return; 

            }

            // Send ICC Code
            retCode = SubmitIC();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(0, 0, "Insert ACOS3-24K card on contact card reader.");
                return; 

            }


            // 2. Select FF 02
            retCode = selectFile(0xFF, 0x02);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "90 00")
            {

                displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return; 

            }

            //3. Write to FF 02
            // This will create 1 binary file, no Option registers and
            // Security Option registers defined, Personalization bit is not set

            tmpArray[0] = 0x00;                      // 00    Option registers
            tmpArray[1] = 0x00;                      // 00    Security option register
            tmpArray[2] = 0x01;                      // 01    No of user files
            tmpArray[3] = 0x00;                      // 00    Personalization bit

            retCode = writeRecord(0, 0x00, 0x04, 0x04, tmpArray);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            displayOut(0, 0, "File FF 02 is updated.");

            // 4. Perform a reset for changes in the ACOS3 to take effect
            connActive = false;
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(0, retCode, "");
                return; 

            }

            retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(0, retCode, "");
                return; 

            }

            displayOut(3, 0, "Card reset is successful.");
            connActive = true;

            // 5. Select FF 04
            retCode = selectFile(0xFF, 0x04);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "90 00")
            {

                displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return; 

            }

            // 6. Send IC Code
            retCode = SubmitIC();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            // 7. Write to FF 04
            // Write to first record of FF 04
            if (tFileLen1.Text == "")
            {

                tmpArray[0] = 0x00;                   // File Length: High Byte
              
            }

            else
            {

                tmpArray[0] = byte.Parse(tFileLen1.Text, System.Globalization.NumberStyles.HexNumber);    // File Length: High Byte              

            }

            tmpArray[1] = byte.Parse(tFileLen2.Text, System.Globalization.NumberStyles.HexNumber);      // File Length: Low Byte
            tmpArray[2] = 0;         // 00    Read security attribute          
            tmpArray[3] = 0;         // 00    Write security attribute           
            tmpArray[4] = byte.Parse(tFileID1.Text, System.Globalization.NumberStyles.HexNumber);       // File identifier
            tmpArray[5] = byte.Parse(tFileID2.Text, System.Globalization.NumberStyles.HexNumber);       // File identifier
            tmpArray[6] = 128;      // File Access Flag: Binary File Type
        
            retCode = writeRecord(0, 0x00, 0x07, 0x07, tmpArray);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            tmpStr = "";
            tmpStr = tFileID1.Text + " " + tFileID2.Text;
            displayOut(0, 0, "User File " + tmpStr + " is defined.");
       
        }

        private int readBinary(byte HiByte, byte LoByte, byte DataLen)
        {

            ClearBuffers();
            SendBuff[0] = 0x80;               // CLA
            SendBuff[1] = 0xB0;               // INS
            SendBuff[2] = HiByte;             // P1    High Byte Address
            SendBuff[3] = LoByte;             // P2    Low Byte Address          
            SendBuff[4] = DataLen;            // P3    Length of data to be read
         
            SendLen = 0x05;
            RecvLen = SendBuff[4] + 2;

            retCode = SendAPDUandDisplay(0);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return retCode;

            }

          return retCode;

        }

        private void bBinRead_Click(object sender, EventArgs e)
        {

            int indx;
            byte tmpLen;
            string tmpStr;
            byte HiByte, LoByte,FileID1, FileID2;
           
            // Validate Input
            if (tFID1.Text == "")
            {

                tFID1.Focus();
                return; 

            }

            if (tFID2.Text == "")
            {

                tFID2.Focus();
                return; 

            }

            if (tOffset2.Text == "")
            {

                tOffset2.Focus();
                return; 

            }

            if (tLen.Text == "")
            {

                tLen.Focus();
                return; 

            }

            ClearBuffers();
            FileID1 = byte.Parse(tFID1.Text, System.Globalization.NumberStyles.HexNumber);
            FileID2 = byte.Parse(tFID2.Text, System.Globalization.NumberStyles.HexNumber);   

            if (tOffset1.Text == "")
            {
                
                HiByte = 0x00;

            }
            else
            {
                
                HiByte = byte.Parse(tOffset1.Text, System.Globalization.NumberStyles.HexNumber);   
           
            }

            LoByte = byte.Parse(tOffset2.Text, System.Globalization.NumberStyles.HexNumber);
            tmpLen = byte.Parse(tLen.Text, System.Globalization.NumberStyles.HexNumber);   


            // 1. Select User File
            retCode = selectFile(FileID1, FileID2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return; 
            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "91 00")
            {

                displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return; 

            }

            // 2. Read binary

            retCode = readBinary(HiByte, LoByte, tmpLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                
                displayOut(0, 0, "Card may not have been formatted yet.");
                return; 

            }

            tmpStr = "";
            indx = 0;
            while ((RecvBuff[indx] != 0))
            {

                if (indx < tData.MaxLength)
                {

                    tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);

                }

                indx = indx + 1;
            }

            tData.Text = tmpStr;
     
        }

        private void bBinWrite_Click(object sender, EventArgs e)
        {
            
            int indx;
            byte tmpLen;
            string tmpStr;
            byte HiByte, LoByte,FileID1, FileID2,tmpLong;
            byte[] tmpArray = new byte[256];

            // Validate Input
            if ((tFID1.Text == "" | !byte.TryParse(tFID1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tFID1.Focus();
                tFID1.Text = "";
                return; 

            }

            if ((tFID2.Text == "" | !byte.TryParse(tFID2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tFID2.Focus();
                tFID2.Text = "";
                return;

            }

            if ((tOffset1.Text == "" | !byte.TryParse(tOffset1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tOffset1.Focus();
                tOffset1.Text = "";
                return;

            }

            if ((tOffset2.Text == "" | !byte.TryParse(tOffset2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tOffset2.Focus();
                tOffset2.Text = "";
                return; 

            }

            if ((tLen.Text == "" | !byte.TryParse(tLen.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tLen.Focus();
                tLen.Text = "";
                return; 

            }

            if (tData.Text == "")
            {

                tData.Focus();
                return; 

            }

            ClearBuffers();
            FileID1 = byte.Parse(tFID1.Text, System.Globalization.NumberStyles.HexNumber);
            FileID2 = byte.Parse(tFID2.Text, System.Globalization.NumberStyles.HexNumber);

            if (tOffset1.Text == "")
            {
                
                HiByte = 0x00;

            }
            else
            {
                
                HiByte = byte.Parse(tOffset1.Text, System.Globalization.NumberStyles.HexNumber);
          
            }

            LoByte = byte.Parse(tOffset2.Text, System.Globalization.NumberStyles.HexNumber);
            tmpLen = byte.Parse(tLen.Text, System.Globalization.NumberStyles.HexNumber);


            // 1. Select User File
            retCode = selectFile(FileID1, FileID2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return; 
            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            if (tmpStr.Trim() != "91 00")
            {

                displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return; 

            }

            // 2. Write input data to card

            tmpStr = tData.Text;
            tmpArray = ASCIIEncoding.ASCII.GetBytes(tmpStr);

            retCode = writeBinary(1, HiByte, LoByte, tData.MaxLength, tmpLen, tmpArray);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return; 
            }

        }
        public int writeBinary(int caseType, int HiByte, int LoByte, int maxDataLen, byte DataLen, byte[] DataIn)
        {

            int indx;
            string tmpStr;

            if (caseType == 1)
            {
                // If card data is to be erased before writing new data
                // 1. Re-initialize card values to &H0
                ClearBuffers();
                SendBuff[0] = 0x80;                 // CLA
                SendBuff[1] = 0xD0;                 // INS
                SendBuff[2] = (byte)HiByte;         // P1    High Byte Address            
                SendBuff[3] = (byte)LoByte;         // P2    Low Byte Address              
                SendBuff[4] = DataLen;              // P3    Length of data to be read         

                for (indx = 0; indx <= maxDataLen - 1; indx++)
                {
                    SendBuff[indx + 5] = 0;
                }
                SendLen = maxDataLen + 5;
                RecvLen = 0x02;

                retCode = SendAPDUandDisplay(0);
                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {

                    return retCode;

                }

                tmpStr = "";
                for (indx = 0; indx <= 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

                if (tmpStr.Trim() != "90 00")
                {

                    displayOut(3, 0, "The return string is invalid. Value: " + tmpStr);
                    retCode = -450;
                    return retCode; 

                }
            }

            // 2. Write data to card
            ClearBuffers();
            SendBuff[0] = 0x80;                 // CLA       
            SendBuff[1] = 0xD0;                 // INS            
            SendBuff[2] = (byte)HiByte;         // P1    High Byte Address            
            SendBuff[3] = (byte)LoByte;         // P2    Low Byte Address            
            SendBuff[4] = DataLen;              // P3    Length of data to be read            

            for (indx = 0; indx <= DataLen - 1; indx++)
            {

                SendBuff[indx + 5] = DataIn[indx];

            }

            SendLen = DataLen + 5;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(0);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return retCode;

            }

            tmpStr = "";
            for (indx = 0; indx <= 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);


            }

            if (tmpStr.Trim() != "90 00")
            {

                displayOut(3, 0, "The return string is invalid. Value: " + tmpStr);
                retCode = -450;
                return retCode;
         

            }
            return retCode;

        }

    
     
    }

}