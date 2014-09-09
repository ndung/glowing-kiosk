using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SSPDllExample
{
    public class Global
    {

        //=========================================================

        public const int CON_RETRY_LIMIT = 10;
        public const short RESET_COUNT_TIMEOUT = 20;
        public const int POLL_TRY_LIMIT = 2;

        // error code definitions
        public const int START_PORT_OPEN_FAIL = 0x100;
        public const int CONNECTION_ATTEMPT_TIMEOUT = 0x101;
        public const int SERIAL_NUMBER_FAIL = 0x102;
        public const int RESET_EVENT_FAIL = 0x103;
        public const int SET_INIHBITS_FAIL = 0x104;
        public const int ENABLE_CMD_FAIL = 0x105;
        public const int CMD_FAIL = 0x106;
        public const int SETUP_REQUEST_FAIL = 0x107;
        public const int POLL_CMD_FAIL = 0x108;
        public const int SERIAL_NUMBER_MATCH_FAIL = 0x109;
        public const int HOST_DATA_NOT_SET = 0x10A;

        // structure defines

        public struct SSP_SLAVE_DATA
        {
            public uint SerialNumber;
            public byte unitType;
            public string FirmwareVersion;
            public string CountryCode;
            public int ValueMultiplier;
            public byte NumberOfChannels;
            public int[] ChannelValue;
            public byte[] ChannelSecurity;
        }

        public struct HOST_DATA
        {
            public int MaxCredit;
            public int CurrentCredit;
            public int LastCredit;
            public int GamePrice;
            public int ConnectionAttempts;
            public uint SerialNumber;
        }

        public static byte InhibitLow;
        public static byte InhibitHigh;

        // the serial port number
        public static int iPort; // the serial port number
        // the global slave and host definitions
        public static SSP_SLAVE_DATA sspSlave;
        public static HOST_DATA host;

        //open com port, send command, close com port
        //returns ok if response is ok, error code if failed
        public static short SendCommand(Declare.UDT cmd)
        {
            Declare.UDT cpy = new Declare.UDT();

            //open com port
            if (Declare.OpenPort(iPort) != 1)
            {
                return START_PORT_OPEN_FAIL;
            }

            //send command, response is in cpy
            cpy = Declare.Command(cmd);

            //check if response is ok
            if (cpy.array1[0] != DefMod.OK)
            {
                Declare.CloseComm();
                return CMD_FAIL;
            }

            //close com port
            Declare.CloseComm();
            return DefMod.OK;
        }

        public static string WhichError(int err)
        {

            // -------------------------------------------------------------------------
            // Convert the error code to a string
            // 
            // 
            switch (err)
            {
                case START_PORT_OPEN_FAIL:
                    return "START_PORT_OPEN_FAIL";
                case CONNECTION_ATTEMPT_TIMEOUT:
                    return "CONNECTION_ATTEMPT_TIMEOUT";
                case SERIAL_NUMBER_FAIL:
                    return "SERIAL_NUMBER_FAIL";
                case RESET_EVENT_FAIL:
                    return "RESET_EVENT_FAIL";
                case SET_INIHBITS_FAIL:
                    return "SET_INIHBITS_FAIL";
                case ENABLE_CMD_FAIL:
                    return "ENABLE_CMD_FAIL";
                case CMD_FAIL:
                    return "CMD_FAIL";
                case SETUP_REQUEST_FAIL:
                    return "SETUP_REQUEST_FAIL";
                case POLL_CMD_FAIL:
                    return "POLL_CMD_FAIL";
                case SERIAL_NUMBER_MATCH_FAIL:
                    return "SERIAL_NUMBER_MATCH_FAIL";
                case HOST_DATA_NOT_SET:
                    return "HOST_DATA_NOT_SET";
                default:
                    return "UNKNOWN_ERROR_CODE";
            }
        }
    }
}