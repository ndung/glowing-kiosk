using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSPDllExample
{
    public class DefMod
    {
        public const byte REJECT_NOTE_CMD = 0x08;
        public const byte BULB_ON_CMD = 0x03;
        public const byte BULB_OFF_CMD = 0x04;
        public const byte DISABLE_CMD = 0x09;
        public const byte DISPENSE_CMD = 0x12;
        public const byte ENABLE_CMD = 0x0A;
        public const byte POLL_CMD = 0x07;
        public const byte RESET_CMD = 0x01;
        public const byte PROGRAM_CURRENCY_CMD = 0x0B;
        public const byte SETUP_REQUEST_CMD = 0x05;
        public const byte SET_INHIBITS_CMD = 0x02;
        public const byte HOST_PROTOCOL = 0x06;
        public const byte SERIAL_NUMBER = 0x0C;
        public const byte SET_INHIBITS = 0x02;
        public const byte SYNC_CMD = 0x11;
        public const byte FILE_HEADER = 0x12;
        public const byte PROG_STATUS = 0x16;
        public const byte UNIT_DATA = 0x0D;
        public const byte VALUE_DATA = 0x0E;
        public const byte SECURITY_DATA = 0x0F;
        public const byte CHANNEL_RETEACH = 0x10;

        public const byte OK = 0xF0;
        public const byte SLAVE_RESET = 0xF1;
        public const byte COMMAND_NOT_KNOWN = 0xF2;
        public const byte WRONG_No_PARAMETERS = 0xF3;
        public const byte PARAMETER_OUT_RANGE = 0xF4;
        public const byte COMMAND_NOT_PROCESS = 0xF5;
        public const byte SOFTWARE_ERROR = 0xF6;

        public const byte DISABLED = 0xE8;
        public const byte NOTE_READ = 0xEF;

        public const byte REJECTING_MS = 0xED;

        public const byte HOLD = 0x18;

        public const byte REJECTED_MS = 0xEC;
        public const byte FRAUD_ATTEMPT_MS = 0xE6;
        public const byte NOTE_STACKED = 0xEB;
        public const byte STACKING = 0xCC;
        public const byte CREDIT = 0xEE;
        public const byte SAFE_JAM = 0xEA;
        public const byte UNSAFE_JAM = 0xE9;
        public const byte STACKER_FULL = 0xE7;

        public const byte NV4 = 0x02;
        public const byte NV7 = 0x05;
    }
}
