using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ITLlib;

namespace SmartPayout
{
    public class CLogger
    {
        // Variables
        int m_PacketCounter;
        bool m_bLogging;
        string m_LogText;
        StreamWriter m_SW;
        string m_FileName;
        Boolean logEncEnabled = false;

        private static CLogger instance;


        public static CLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CLogger();
                }
                return instance;
            }
        }

        // Variable access
        public string Log
        {
            get { return m_LogText; }
            set { m_LogText = value; }
        }

        // Constructor
        private CLogger()
        {            
            m_PacketCounter = 1;
            m_bLogging = false;
            // create persistent log
            try
            {
                // if the log dir doesn't exist, create it
                string logDir = "Logs\\" + DateTime.Now.ToLongDateString();
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);
                // name this log as current time
                m_FileName = DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString() + "m" + DateTime.Now.Second.ToString() + "s.txt";
                // create/open the file
                m_SW = File.AppendText(logDir + "\\" + m_FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), "EXCEPTION");                
            }
            Start();
        }

        public void Start()
        {
            m_bLogging = true;
            m_SW.Write("\r\nStarted logging - " + DateTime.Now);
        }

        public void Stop()
        {
            m_bLogging = false;
            m_SW.Close();
        }

        public void UpdateLog(String info)
        {
            m_SW.Write("\r\n"+info);            
        }

        // This function should be called in a loop, it monitors the SSP_COMMAND_INFO parameter
        // and writes the info to a text box in a readable format. If the failedCommand bool
        // is set true then it will not write a response.
        public void UpdateLog(SSP_COMMAND_INFO info, bool failedCommand = false)
        {
            if (m_bLogging)
            {
                string byteStr;
                byte len;
                // NON-ENCRPYTED
                // transmission
                m_LogText = "\r\nNo Encryption\r\nSent Packet #" + m_PacketCounter;
                len = info.PreEncryptedTransmit.PacketData[2];
                m_LogText += "\tLength: " + len.ToString();
                m_LogText += "\tSync: " + (info.PreEncryptedTransmit.PacketData[1] >> 7);
                m_LogText += "\tData: ";
                byteStr = BitConverter.ToString(info.PreEncryptedTransmit.PacketData, 3, len);
                m_LogText += FormatByteString(byteStr);
                m_LogText += "\t";

                // received
                if (!failedCommand)
                {
                    m_LogText += "\r\nRecv Packet #" + m_PacketCounter;
                    len = info.PreEncryptedRecieve.PacketData[2];
                    m_LogText += "\tLength: " + len.ToString();
                    m_LogText += "\tSync: " + (info.PreEncryptedRecieve.PacketData[1] >> 7);
                    m_LogText += "\tData: ";
                    byteStr = BitConverter.ToString(info.PreEncryptedRecieve.PacketData, 3, len);
                    m_LogText += FormatByteString(byteStr);
                    m_LogText += "\t";
                }
                else
                {
                    m_LogText += "\r\nNo response...";
                }

                if (logEncEnabled)
                {
                    // ENCRYPTED
                    // transmission
                    m_LogText += "\r\nEncryption\r\nSent Packet #" + m_PacketCounter;
                    len = info.Transmit.PacketData[2];
                    m_LogText += "\tLength: " + len.ToString();
                    m_LogText += "\tSync: " + (info.Transmit.PacketData[1] >> 7);
                    m_LogText += "\tData: ";
                    byteStr = BitConverter.ToString(info.Transmit.PacketData, 3, len);
                    m_LogText += FormatByteString(byteStr);
                    m_LogText += "\t";

                    // received
                    if (!failedCommand)
                    {
                        m_LogText += "\r\nRecv Packet #" + m_PacketCounter;
                        len = info.Receive.PacketData[2];
                        m_LogText += "\tLength: " + len.ToString();
                        m_LogText += "\tSync: " + (info.Receive.PacketData[1] >> 7);
                        m_LogText += "\tData: ";
                        byteStr = BitConverter.ToString(info.Receive.PacketData, 3, len);
                        m_LogText += FormatByteString(byteStr);
                        m_LogText += "\t";
                    }
                    else
                    {
                        m_LogText += "\r\nNo response...";
                    }
                }
                
                AppendToLog(m_LogText);                
                m_PacketCounter++;
            }
        }

        private string FormatByteString(string s)
        {
            string formatted = s;
            string[] sArr;
            sArr = formatted.Split('-');
            formatted = "";
            for (int i = 0; i < sArr.Length; i++)
            {
                formatted += sArr[i];
                formatted += " ";
            }
            return formatted;
        }

        public void PauseLogging() { m_bLogging = false; }
        public void ResumeLogging() { m_bLogging = true; }

        private void AppendToLog(string stringToAppend)
        {
            m_SW.Write(stringToAppend);
        }
    }
}
