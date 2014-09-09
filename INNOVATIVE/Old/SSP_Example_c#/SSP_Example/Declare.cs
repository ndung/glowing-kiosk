using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace SSPDllExample
{
    public class Declare
    {

        //=========================================================

        const int DOWNLOAD_COMPLETE = 0x100000;

        // error code definitions
        const int OPEN_FILE_ERROR = 0x100001;
        const int READ_FILE_ERROR = 0x100002;
        const int NOT_ITL_FILE = 0x100003;
        const int PORT_OPEN_FAIL = 0x100004;
        const int SYNC_CONNECTION_FAIL = 0x100005;
        const int SECURITY_PROTECTED_FILE = 0x100006;

        const int DATA_TRANSFER_FAIL = 0x100010;
        const int PROG_COMMAND_FAIL = 0x100011;
        const int HEADER_FAIL = 0x100012;
        const int PROG_STATUS_FAIL = 0x100013;
        const int PROG_RESET_FAIL = 0x100014;
        const int DOWNLOAD_NOT_ALLOWED = 0x100015;


        // dll function to set port baud rate
        [DllImport("InnTechSSP.dll")]
        public static extern int SetBaud(int iBaud);
        [DllImport("InnTechSSP.dll")]
        public static extern int SetFastBaud(int iBaud);
        // sets/resets the DTR pin (0 and 1)
        [DllImport("InnTechSSP.dll")]
        public static extern int SetDTR(int iDTR);
        // set the sequence byte (0 or &h80)
        [DllImport("InnTechSSP.dll")]
        public static extern int FlipSeq();
        // dll function to open a comms port (iPort)
        [DllImport("InnTechSSP.dll")]
        public static extern int OpenPort(int iport);
        // dll function to close a comms port
        [DllImport("InnTechSSP.dll")]
        public static extern int CloseComm();
        [DllImport("InnTechSSP.dll")]
        public static extern void SetRetryParameters(ref RTRY rRtry);
        [DllImport("InnTechSSP.dll")]
        public static extern int GetBaud();
        [DllImport("InnTechSSP.dll")]
        public static extern int DownloadFile(string szName, int iport);
        [DllImport("InnTechSSP.dll")]
        public static extern int GetDownloadData();

        // dll function to send an SSP command and recieve a reply
        // the command is sent and returned in the form of a structure (UDT)
        // rxStatus is not used when sending, datalen is the number of command bytes
        // array1 contains the array bytes
        public static UDT Command(UDT src)
        {
            UDT ret = Declare_udt.Command_(src);
            return ret;
        }

        public static UDT GetLastRxPacket()
        {
            UDT ret = Declare_udt.GetLastRxPacket_();
            return ret;
        }

        public static UDT GetLastTxPacket()
        {
            UDT ret = Declare_udt.GetLastTxPacket_();
            return ret;
        }

        // the user defined structure with unmanaged byte array array1 

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class UDT
        {
            public short rxStatus;
            public byte datalen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
            public byte[] array1 = new byte[255];
        }

        public struct RTRY
        {
            public byte rDelay;
            public byte rRetries;
        }

        public static RTRY reTry;

    }
}