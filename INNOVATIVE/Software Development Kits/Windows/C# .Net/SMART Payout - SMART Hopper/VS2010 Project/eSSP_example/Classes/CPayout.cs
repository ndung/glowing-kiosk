﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Threading;
using ITLlib;

namespace eSSP_example
{
    public class CPayout
    {
        // ssp library variables

        SSP_COMMAND cmd;
        SSP_KEYS keys;
        SSP_FULL_KEY sspKey;
        SSP_COMMAND_INFO info;

        // variable declarations

        // The logging class
        CCommsWindow m_Comms;

        // The number of channels used in this validator
        int m_NumberOfChannels;

        // The type of unit this class represents, set in the setup request
        char m_UnitType;

        // The multiplier by which the channel values are multiplied to get their
        // true penny value.
        int m_ValueMultiplier;

        // A list of dataset data, sorted by value. Holds the info on channel number, value, currency,
        // level and whether it is being recycled.
        List<ChannelData> m_UnitDataList;

        // constructor
        public CPayout()
        {
            cmd = new SSP_COMMAND();
            keys = new SSP_KEYS();
            sspKey = new SSP_FULL_KEY();
            info = new SSP_COMMAND_INFO();

            m_Comms = new CCommsWindow("SMARTPayout");
            m_Comms.Text = "SMART Payout Comms";
            m_NumberOfChannels = 0;
            m_ValueMultiplier = 1;
            m_UnitDataList = new List<ChannelData>();
        }

        /* Variable Access */

        // access to ssp vars
        public SSP_COMMAND CommandStructure
        {
            get { return cmd; }
            set { cmd = value; }
        }

        public SSP_COMMAND_INFO InfoStructure
        {
            get { return info; }
            set { info = value; }
        }

        // access to the comms log
        public CCommsWindow CommsLog
        {
            get { return m_Comms; }
            set { m_Comms = value; }
        }

        // access to number of channels
        public int NumberOfChannels
        {
            get { return m_NumberOfChannels; }
            set { m_NumberOfChannels = value; }
        }

        // access to value multiplier
        public int Multiplier
        {
            get { return m_ValueMultiplier; }
            set { m_ValueMultiplier = value; }
        }

        // access to sorted list of hash entries
        public List<ChannelData> UnitDataList
        {
            get { return m_UnitDataList; }
        }

        // access to the type of unit
        public char UnitType
        {
            get { return m_UnitType; }
        }

        /* Command functions */

        // The enable command enables the validator, allowing it to receive and act on commands.
        public void EnableValidator(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_ENABLE;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;
            // check response
            if (log != null)
                log.AppendText("Unit enabled\r\n");
        }

        // Disable command stops the validator from acting on commands sent to it.
        public void DisableValidator(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_DISABLE;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;
            // check response
            if (log != null)
                log.AppendText("Unit disabled\r\n");
        }

        // Enable payout allows the validator to payout and store notes.
        public void EnablePayout(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_ENABLE_PAYOUT;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
                log.AppendText("Payout enabled\r\n");
        }

        // Disable payout stops the validator being able to store/payout notes.
        public void DisablePayout(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_DISABLE_PAYOUT;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;
            if (log != null)
                log.AppendText("Payout disabled\r\n");
        }

        // Empty payout device takes all the notes stored and moves them to the cashbox.
        public void EmptyPayoutDevice(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_EMPTY;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
                log.AppendText("Emptying payout device\r\n");
        }

        // This function uses the command GET NOTE AMOUNT to find out the number of
        // a specified type of note stored in the payout. Returns the number of notes stored
        // of that denomination.
        public int CheckNoteLevel(int note, char[] currency, TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_GET_NOTE_AMOUNT;
            byte[] b = CHelpers.ConvertInt32ToBytes(note);
            cmd.CommandData[1] = b[0];
            cmd.CommandData[2] = b[1];
            cmd.CommandData[3] = b[2];
            cmd.CommandData[4] = b[3];

            cmd.CommandData[5] = (byte)currency[0];
            cmd.CommandData[6] = (byte)currency[1];
            cmd.CommandData[7] = (byte)currency[2];
            cmd.CommandDataLength = 8;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return 0;

            int i = (int)cmd.ResponseData[1];
            return i;
        }

        // The set routing command changes the way the validator deals with a note, either it can send the note straight to the cashbox
        // or it can store the note for payout. This is specified in the second byte (0x00 to store for payout, 0x01 for cashbox). The 
        // bytes after this represent the 4 bit value of the note.
        // This function allows the note to be specified as an int in the param note, the stack bool is true for cashbox, false for storage.
        public void ChangeNoteRoute(int note, char[] currency, bool stack, TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_SET_ROUTING;

            // if this note is being changed to stack (cashbox)
            if (stack)
                cmd.CommandData[1] = 0x01;
            // note being stored (payout)
            else
                cmd.CommandData[1] = 0x00;

            // get the note as a byte array
            byte[] b = BitConverter.GetBytes(note);
            cmd.CommandData[2] = b[0];
            cmd.CommandData[3] = b[1];
            cmd.CommandData[4] = b[2];
            cmd.CommandData[5] = b[3];

            // send country code (protocol 6+)
            cmd.CommandData[6] = (byte)currency[0];
            cmd.CommandData[7] = (byte)currency[1];
            cmd.CommandData[8] = (byte)currency[2];

            cmd.CommandDataLength = 9;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
            {
                string s = new string(currency);
                if (stack)
                    s += " to cashbox)\r\n";
                else
                    s += " to storage)\r\n";

                log.AppendText("Note routing successful (" + CHelpers.FormatToCurrency(note) + " " + s);
            }
        }

        // The reset command instructs the validator to restart (same effect as switching on and off)
        public void Reset(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_RESET;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;
        }

        // This just sends a sync command to the validator.
        public bool SendSync(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_SYNC;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return false;
            if (log != null) log.AppendText("Sent sync\r\n");
            return true;
        }

        // This function sets the protocol version in the validator to the version passed across. Whoever calls
        // this needs to check the response to make sure the version is supported.
        public void SetProtocolVersion(byte pVersion, TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_HOST_PROTOCOL_VERSION;
            cmd.CommandData[1] = pVersion;
            cmd.CommandDataLength = 2;
            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;
        }

        // This function calls the PAYOUT AMOUNT command to payout a specified value. This can be sent
        // with the option byte 0x19 to test whether the payout is possible or 0x58 to actually do the payout.
        public void PayoutAmount(int amount, char[] currency, bool test = false, TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_PAYOUT_AMOUNT;
            byte[] b = CHelpers.ConvertInt32ToBytes(amount);
            cmd.CommandData[1] = b[0];
            cmd.CommandData[2] = b[1];
            cmd.CommandData[3] = b[2];
            cmd.CommandData[4] = b[3];

            cmd.CommandData[5] = (byte)currency[0];
            cmd.CommandData[6] = (byte)currency[1];
            cmd.CommandData[7] = (byte)currency[2];

            if (!test)
                cmd.CommandData[8] = 0x58; // real payout
            else
                cmd.CommandData[8] = 0x19; // test payout

            cmd.CommandDataLength = 9;
            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
            {
                log.AppendText("Paying out " + CHelpers.FormatToCurrency(amount) + " " +
                    new string(currency) + "\r\n");
            }
        }

        // Payout by denomination. This function allows a developer to payout specified amounts of certain
        // notes. Due to the variable length of the data that could be passed to the function, the user 
        // passes an array containing the data to payout and the length of that array along with the number
        // of denominations they are paying out.
        public void PayoutByDenomination(byte numDenoms, byte[] data, byte dataLength, TextBox log = null)
        {
            // First is the command byte
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_PAYOUT_BY_DENOMINATION;

            // Next is the number of denominations to be paid out
            cmd.CommandData[1] = numDenoms;

            // Copy over data byte array parameter into command structure
            int currentIndex = 2;
            for (int i = 0; i < dataLength; i++)
                cmd.CommandData[currentIndex++] = data[i];

            // Perform a real payout (0x19 for test)
            cmd.CommandData[currentIndex++] = 0x58;

            // Length of command data (add 3 to cover the command byte, num of denoms and real/test byte)
            dataLength += 3;
            cmd.CommandDataLength = dataLength;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
                log.AppendText("Paying out by denomination...\r\n");
        }

        // This function performs a number of commands in order to setup the encryption between the host and the validator.
        public bool NegotiateKeys(TextBox log = null)
        {
            byte i;

            // send sync
            if (log != null) log.AppendText("Syncing... ");
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_SYNC;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return false;

            if (log != null) log.AppendText("Success\r\n");

            LibraryHandler.InitiateKeys(ref keys, ref cmd);

            // send generator
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_SET_GENERATOR;
            cmd.CommandDataLength = 9;
            if (log != null) log.AppendText("Setting generator... ");
            for (i = 0; i < 8; i++)
            {
                cmd.CommandData[i + 1] = (byte)(keys.Generator >> (8 * i));
            }

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return false;
            
            if (log != null) log.AppendText("Success\r\n");

            // send modulus
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_SET_MODULUS;
            cmd.CommandDataLength = 9;
            if (log != null) log.AppendText("Sending modulus... ");
            for (i = 0; i < 8; i++)
            {
                cmd.CommandData[i + 1] = (byte)(keys.Modulus >> (8 * i));
            }

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return false;

            if (log != null) log.AppendText("Success\r\n");

            // send key exchange
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_REQUEST_KEY_EXCHANGE;
            cmd.CommandDataLength = 9;
            if (log != null) log.AppendText("Exchanging keys... ");
            for (i = 0; i < 8; i++)
            {
                cmd.CommandData[i + 1] = (byte)(keys.HostInter >> (8 * i));
            }

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return false;

            if (log != null) log.AppendText("Success\r\n");

            keys.SlaveInterKey = 0;
            for (i = 0; i < 8; i++)
            {
                keys.SlaveInterKey += (UInt64)cmd.ResponseData[1 + i] << (8 * i);
            }

            LibraryHandler.CreateFullKey(ref keys);

            // get full encryption key
            cmd.Key.FixedKey = 0x0123456701234567;
            cmd.Key.VariableKey = keys.KeyHost;

            if (log != null) log.AppendText("Keys successfully negotiated\r\n");
            return true;
        }

        // This function uses the setup request command to get all the information about the validator.
        public void SetupRequest(TextBox log = null)
        {
            // send setup request
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_SETUP_REQUEST;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            // display setup request
            string displayString = "Unit Type: ";
            int index = 1;

            // unit type (table 0-1)
            m_UnitType = (char)cmd.ResponseData[index++];
            switch (m_UnitType)
            {
                case (char)0x00: displayString += "Validator"; break;
                case (char)0x03: displayString += "SMART Hopper"; break;
                case (char)0x06: displayString += "SMART Payout"; break;
                case (char)0x07: displayString += "NV11"; break;
                default: displayString += "Unknown Type"; break;
            }

            displayString += "\r\nFirmware: ";

            // firmware (table 2-5)
            while (index <= 5)
            {
                displayString += (char)cmd.ResponseData[index++];
                if (index == 4)
                    displayString += ".";
            }

            // country code (table 6-8)
            // this is legacy code, in protocol version 6+ each channel has a seperate currency
            index = 9; // to skip country code

            // value multiplier (table 9-11) 
            // also legacy code, a real value multiplier appears later in the response
            index = 12; // to skip value multiplier

            displayString += "\r\nNumber of Channels: ";
            int numChannels = cmd.ResponseData[index++];
            m_NumberOfChannels = numChannels;

            displayString += numChannels + "\r\n";
            // channel values (table 13 to 13+n)
            // the channel values located here in the table are legacy, protocol 6+ provides a set of expanded
            // channel values.
            index = 13 + m_NumberOfChannels; // Skip channel values

            // channel security (table 13+n to 13+(n*2))
            // channel security values are also legacy code
            index = 13 + (m_NumberOfChannels * 2); // Skip channel security

            displayString += "Real Value Multiplier: ";

            // real value multiplier (table 13+(n*2) to 15+(n*2))
            // (big endian)
            m_ValueMultiplier = cmd.ResponseData[index + 2];
            m_ValueMultiplier += cmd.ResponseData[index + 1] << 8;
            m_ValueMultiplier += cmd.ResponseData[index] << 16;
            displayString += m_ValueMultiplier + "\r\nProtocol Version: ";
            index += 3;

            // protocol version (table 16+(n*2))
            index = 16 + (m_NumberOfChannels * 2);
            int protocol = cmd.ResponseData[index++];
            displayString += protocol + "\r\n";

            // protocol 6+ only

            // channel currency country code (table 17+(n*2) to 17+(n*5))
            index = 17 + (m_NumberOfChannels * 2);
            int sectionEnd = 17 + (m_NumberOfChannels * 5);
            int count = 0;
            byte[] channelCurrencyTemp = new byte[3 * m_NumberOfChannels];
            while (index < sectionEnd)
            {
                displayString += "Channel " + ((count / 3) + 1) + ", currency: ";
                channelCurrencyTemp[count] = cmd.ResponseData[index++];
                displayString += (char)channelCurrencyTemp[count++];
                channelCurrencyTemp[count] = cmd.ResponseData[index++];
                displayString += (char)channelCurrencyTemp[count++];
                channelCurrencyTemp[count] = cmd.ResponseData[index++];
                displayString += (char)channelCurrencyTemp[count++];
                displayString += "\r\n";
            }

            // expanded channel values (table 17+(n*5) to 17+(n*9))
            index = sectionEnd;
            displayString += "Expanded channel values:\r\n";
            sectionEnd = 17 + (m_NumberOfChannels * 9);
            int n = 0;
            count = 0;
            int[] channelValuesTemp = new int[m_NumberOfChannels];
            while (index < sectionEnd)
            {
                n = CHelpers.ConvertBytesToInt32(cmd.ResponseData, index);
                channelValuesTemp[count] = n;
                index += 4;
                displayString += "Channel " + ++count + ", value = " + n + "\r\n";
            }

            // Create list entry for each channel
            m_UnitDataList.Clear(); // clear old table

            for (byte i = 0; i < m_NumberOfChannels; i++)
            {
                ChannelData d = new ChannelData();
                d.Channel = i;
                d.Channel++;
                d.Value = channelValuesTemp[i] * Multiplier;
                d.Currency[0] = (char)channelCurrencyTemp[0 + (i * 3)];
                d.Currency[1] = (char)channelCurrencyTemp[1 + (i * 3)];
                d.Currency[2] = (char)channelCurrencyTemp[2 + (i * 3)];
                
                d.Level = CheckNoteLevel(d.Value, d.Currency, log);
                bool b = false;
               
                IsNoteRecycling(d.Value, d.Currency, ref b, log);
                
                d.Recycling = b;
                m_UnitDataList.Add(d);
            }

            // Sort the list of data by the channel.
            m_UnitDataList.Sort(delegate(ChannelData d1, ChannelData d2) { return d1.Channel.CompareTo(d2.Channel); });

            if (log != null)
                log.AppendText(displayString);
        }

        // This function sends the set inhibits command to set the inhibits on the validator. An additional two
        // bytes are sent along with the command byte to indicate the status of the inhibits on the channels.
        // For example 0xFF and 0xFF in binary is 11111111 11111111. This indicates all 16 channels supported by
        // the validator are uninhibited. If a user wants to inhibit channels 8-16, they would send 0x00 and 0xFF.
        public void SetInhibits(TextBox log = null)
        {
            // set inhibits
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_SET_INHIBITS;
            cmd.CommandData[1] = 0xFF;
            cmd.CommandData[2] = 0xFF;
            cmd.CommandDataLength = 3;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
                log.AppendText("Inhibits set\r\n");
        }

        // This function uses the GET ROUTING command to see if a specified note is recycling. The
        // caller passes a bool across which is set by the function.
        public void IsNoteRecycling(int noteValue, char[] currency, ref bool response, TextBox log = null)
        {
            // Determine if the note is currently being recycled
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_GET_ROUTING;
            byte[] b = CHelpers.ConvertInt32ToBytes(noteValue);
            cmd.CommandData[1] = b[0];
            cmd.CommandData[2] = b[1];
            cmd.CommandData[3] = b[2];
            cmd.CommandData[4] = b[3];

            // Add currency
            cmd.CommandData[5] = (byte)currency[0];
            cmd.CommandData[6] = (byte)currency[1];
            cmd.CommandData[7] = (byte)currency[2];
            cmd.CommandDataLength = 8;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            // True if it is currently being recycled
            if (cmd.ResponseData[1] == 0x00)
                response = true;
            // False if not
            else if (cmd.ResponseData[1] == 0x01)
                response = false;
        }

        // This function uses the FLOAT AMOUNT command to set the float amount. The validator will empty
        // notes into the cashbox leaving the requested floating amount in the payout. The minimum payout
        // is also setup so the validator will leave itself the ability to payout the minimum value requested.
        public void SetFloat(int minPayout, int floatAmount, char[] currency, TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_FLOAT_AMOUNT;
            byte[] b = CHelpers.ConvertInt32ToBytes(minPayout);
            cmd.CommandData[1] = b[0];
            cmd.CommandData[2] = b[1];
            cmd.CommandData[3] = b[2];
            cmd.CommandData[4] = b[3];
            b = CHelpers.ConvertInt32ToBytes(floatAmount);
            cmd.CommandData[5] = b[0];
            cmd.CommandData[6] = b[1];
            cmd.CommandData[7] = b[2];
            cmd.CommandData[8] = b[3];

            // Add currency
            cmd.CommandData[9] = (byte)currency[0];
            cmd.CommandData[10] = (byte)currency[1];
            cmd.CommandData[11] = (byte)currency[2];

            cmd.CommandData[12] = 0x58; // real float (could use 0x19 for test)

            cmd.CommandDataLength = 13;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
                log.AppendText("Floated amount successfully\r\n");
        }

        // This function uses the SMART EMPTY command which empties all the notes in the note
        // storage to the cashbox but unlike the EMPTY command it keeps a track of all the notes
        // it has moved. This data can be retrieved using the CASHBOX PAYOUT OPERATION DATA command.
        public void SmartEmpty(TextBox log = null)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_SMART_EMPTY;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return;

            if (log != null)
                log.AppendText("SMART Emptying...\r\n");
        }

        // This function can be called after SMART events such as SMART empty. It will return a report
        // of what was moved to the cashbox. It returns a formatted string. The command can be used for
        // more useful things such as detecting what has been paid into the cashbox in the case of a payout
        // error.
        public string GetCashboxPayoutOpData(TextBox log = null)
        {
            // first send the command
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_CASHBOX_PAYOUT_OP_DATA;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log))
                return "";

            // now deal with the response data
            // number of different notes
            int n = cmd.ResponseData[1];
            string displayString = "Number of Total Notes: " + n.ToString() + "\r\n\r\n";
            int i = 0;
            for (i = 2; i < (9 * n); i += 9)
            {
                displayString += "Moved " + CHelpers.ConvertBytesToInt16(cmd.ResponseData, i) +
                    " x " + CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(cmd.ResponseData, i + 2)) +
                    " " + (char)cmd.ResponseData[i + 6] + (char)cmd.ResponseData[i + 7] + (char)cmd.ResponseData[i + 8] + " to cashbox\r\n";
            }
            displayString += CHelpers.ConvertBytesToInt32(cmd.ResponseData, i) + " notes not recognised\r\n";

            if (log != null) log.AppendText(displayString);
            return displayString;
            
        }

        // This function sends the command LAST REJECT CODE which gives info about why a note has been rejected. It then
        // outputs the info to a passed across textbox.
        public void QueryRejection(TextBox log)
        {
            cmd.CommandData[0] = CCommands.SMARTPayout.SSP_CMD_LAST_REJECT_CODE;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log) || !CheckGenericResponses(log) || log == null)
                return;
            switch (cmd.ResponseData[1])
            {
                case 0x00: log.AppendText("Note accepted\r\n"); break;
                case 0x01: log.AppendText("Note length incorrect\r\n"); break;
                case 0x02: log.AppendText("Invalid note\r\n"); break;
                case 0x03: log.AppendText("Invalid note\r\n"); break;
                case 0x04: log.AppendText("Invalid note\r\n"); break;
                case 0x05: log.AppendText("Invalid note\r\n"); break;
                case 0x06: log.AppendText("Channel inhibited\r\n"); break;
                case 0x07: log.AppendText("Second note inserted during read\r\n"); break;
                case 0x08: log.AppendText("Host rejected note\r\n"); break;
                case 0x09: log.AppendText("Invalid note\r\n"); break;
                case 0x0A: log.AppendText("Invalid note read\r\n"); break;
                case 0x0B: log.AppendText("Note too long\r\n"); break;
                case 0x0C: log.AppendText("Validator disabled\r\n"); break;
                case 0x0D: log.AppendText("Mechanism slow/stalled\r\n"); break;
                case 0x0E: log.AppendText("Strim attempt\r\n"); break;
                case 0x0F: log.AppendText("Fraud channel reject\r\n"); break;
                case 0x10: log.AppendText("No notes inserted\r\n"); break;
                case 0x11: log.AppendText("Invalid note read\r\n"); break;
                case 0x12: log.AppendText("Twisted note detected\r\n"); break;
                case 0x13: log.AppendText("Escrow time-out\r\n"); break;
                case 0x14: log.AppendText("Bar code scan fail\r\n"); break;
                case 0x15: log.AppendText("Invalid note read\r\n"); break;
                case 0x16: log.AppendText("Invalid note read\r\n"); break;
                case 0x17: log.AppendText("Invalid note read\r\n"); break;
                case 0x18: log.AppendText("Invalid note read\r\n"); break;
                case 0x19: log.AppendText("Incorrect note width\r\n"); break;
                case 0x1A: log.AppendText("Note too short\r\n"); break;
            }
        }

        // The poll function is called repeatedly to poll to validator for information, it returns as
        // a response in the command structure what events are currently happening.
        public bool DoPoll(TextBox log)
        {
            byte i;

            //send poll
            cmd.CommandData[0] = CCommands.Generic.SSP_CMD_POLL;
            cmd.CommandDataLength = 1;

            if (!SendCommand(log))
                return false;
            if (cmd.ResponseData[0] == 0xFA)
                return false;

            // store response locally so data can't get corrupted by other use of the cmd variable
            byte[] response = new byte[255];
            cmd.ResponseData.CopyTo(response, 0);
            byte responseLength = cmd.ResponseDataLength;

            //parse poll response
            ChannelData data = new ChannelData();
            for (i = 1; i < responseLength; ++i)
            {
                switch (response[i])
                {
                    // This response indicates that the unit was reset and this is the first time a poll
                    // has been called since the reset.
                    case CCommands.SMARTPayout.SSP_POLL_RESET:
                        log.AppendText("Unit reset\r\n");
                        UpdateData();
                        break;
                    // This response indicates the unit is disabled.
                    case CCommands.SMARTPayout.SSP_POLL_DISABLED:
                        log.AppendText("Unit disabled...\r\n");
                        break;
                    // A note is currently being read by the validator sensors. The second byte of this response
                    // is zero until the note's type has been determined, it then changes to the channel of the 
                    // scanned note.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_READ:
                        if (cmd.ResponseData[i + 1] > 0)
                        {
                            GetDataByChannel(response[i + 1], ref data);
                            log.AppendText("Note in escrow, amount: " + CHelpers.FormatToCurrency(data.Value) + "\r\n");
                        }
                        else
                            log.AppendText("Reading note\r\n");
                        i++;
                        break;
                    // A credit event has been detected, this is when the validator has accepted a note as legal currency.
                    case CCommands.SMARTPayout.SSP_POLL_CREDIT:
                        GetDataByChannel(response[i + 1], ref data);
                        log.AppendText("Credit " + CHelpers.FormatToCurrency(data.Value) + "\r\n");
                        UpdateData();
                        i++;
                        break;
                    // A note is being rejected from the validator. This will carry on polling while the note is in transit.
                    case CCommands.SMARTPayout.SSP_POLL_REJECTING:
                        log.AppendText("Rejecting note\r\n");
                        break;
                    // A note has been rejected from the validator, the note will be resting in the bezel. This response only
                    // appears once.
                    case CCommands.SMARTPayout.SSP_POLL_REJECTED:
                        log.AppendText("Note rejected\r\n");
                        QueryRejection(log);
                        break;
                    // A note is in transit to the cashbox.
                    case CCommands.SMARTPayout.SSP_POLL_STACKING:
                        log.AppendText("Stacking note\r\n");
                        break;
                    // The payout device is 'floating' a specified amount of notes. It will transfer some to the cashbox and
                    // leave the specified amount in the payout device.
                    case CCommands.SMARTPayout.SSP_POLL_FLOATING:
                        log.AppendText("Floating notes\r\n");
                        // Now the index needs to be moved on to skip over the data provided by this response so it
                        // is not parsed as a normal poll response.
                        // In this response, the data includes the number of countries being floated (1 byte), then a 4 byte value
                        // and 3 byte currency code for each country. 
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // A note has reached the cashbox.
                    case CCommands.SMARTPayout.SSP_POLL_STACKED:
                        log.AppendText("Note stacked\r\n");
                        break;
                    // The float operation has been completed.
                    case CCommands.SMARTPayout.SSP_POLL_FLOATED:
                        log.AppendText("Completed floating\r\n");
                        UpdateData();
                        EnableValidator();
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // A note has been stored in the payout device to be paid out instead of going into the cashbox.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_STORED:
                        log.AppendText("Note stored\r\n");
                        UpdateData();
                        break;
                    // A safe jam has been detected. This is where the user has inserted a note and the note
                    // is jammed somewhere that the user cannot reach.
                    case CCommands.SMARTPayout.SSP_POLL_SAFE_JAM:
                        log.AppendText("Safe jam\r\n");
                        break;
                    // An unsafe jam has been detected. This is where a user has inserted a note and the note
                    // is jammed somewhere that the user can potentially recover the note from.
                    case CCommands.SMARTPayout.SSP_POLL_UNSAFE_JAM:
                        log.AppendText("Unsafe jam\r\n");
                        break;
                    // An error has been detected by the payout unit.
                    case CCommands.SMARTPayout.SSP_POLL_PAYOUT_ERROR: // Note: Will be reported only when Protocol version >= 7
                        log.AppendText("Detected error with payout device\r\n");
                        i += (byte)((response[i + 1] * 7) + 2);
                        break;
                    // A fraud attempt has been detected. 
                    case CCommands.SMARTPayout.SSP_POLL_FRAUD_ATTEMPT:
                        log.AppendText("Fraud attempt\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // The stacker (cashbox) is full.
                    case CCommands.SMARTPayout.SSP_POLL_STACKER_FULL:
                        log.AppendText("Stacker full\r\n");
                        break;
                    // A note was detected somewhere inside the validator on startup and was rejected from the front of the
                    // unit.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_CLEARED_FROM_FRONT:
                        log.AppendText("Note cleared from front of validator\r\n");
                        i++;
                        break;
                    // A note was detected somewhere inside the validator on startup and was cleared into the cashbox.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_CLEARED_TO_CASHBOX:
                        log.AppendText("Note cleared to cashbox\r\n");
                        i++;
                        break;
                    // A note has been detected in the validator on startup and moved to the payout device 
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_PAID_INTO_STORE_ON_POWERUP:
                        log.AppendText("Note paid into payout device on startup\r\n");
                        i += 7;
                        break;
                    // A note has been detected in the validator on startup and moved to the cashbox
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_PAID_INTO_STACKER_ON_POWERUP:
                        log.AppendText("Note paid into cashbox on startup\r\n");
                        i += 7;
                        break;
                    // The cashbox has been removed from the unit. This will continue to poll until the cashbox is replaced.
                    case CCommands.SMARTPayout.SSP_POLL_CASHBOX_REMOVED:
                        log.AppendText("Cashbox removed\r\n");
                        break;
                    // The cashbox has been replaced, this will only display on a poll once.
                    case CCommands.SMARTPayout.SSP_POLL_CASHBOX_REPLACED:
                        log.AppendText("Cashbox replaced\r\n");
                        break;
                    // The validator is in the process of paying out a note, this will continue to poll until the note has 
                    // been fully dispensed and removed from the front of the validator by the user.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_DISPENSING:
                        log.AppendText("Dispensing note(s)\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // The note has been dispensed and removed from the bezel by the user.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_DISPENSED:
                        log.AppendText("Dispensed note(s)\r\n");
                        UpdateData();
                        EnableValidator();
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // The payout device is in the process of emptying all its stored notes to the cashbox. This
                    // will continue to poll until the device is empty.
                    case CCommands.SMARTPayout.SSP_POLL_EMPTYING:
                        log.AppendText("Emptying\r\n");
                        break;
                    // This single poll response indicates that the payout device has finished emptying.
                    case CCommands.SMARTPayout.SSP_POLL_EMPTIED:
                        log.AppendText("Emptied\r\n");
                        UpdateData();
                        EnableValidator();
                        break;
                    // The payout device is in the process of SMART emptying all its stored notes to the cashbox, keeping
                    // a count of the notes emptied. This will continue to poll until the device is empty.
                    case CCommands.SMARTPayout.SSP_POLL_SMART_EMPTYING:
                        log.AppendText("SMART Emptying...\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // The payout device has finished SMART emptying, the information of what was emptied can now be displayed
                    // using the CASHBOX PAYOUT OPERATION DATA command.
                    case CCommands.SMARTPayout.SSP_POLL_SMART_EMPTIED:
                        log.AppendText("SMART Emptied, getting info...\r\n");
                        UpdateData();
                        GetCashboxPayoutOpData(log);
                        EnableValidator();
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // The payout device has encountered a jam. This will not clear until the jam has been removed and the unit
                    // reset.
                    case CCommands.SMARTPayout.SSP_POLL_JAMMED:
                        log.AppendText("Unit jammed...\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // This is reported when the payout has been halted by a host command. This will report the value of
                    // currency dispensed upto the point it was halted. 
                    case CCommands.SMARTPayout.SSP_POLL_HALTED:
                        log.AppendText("Halted...\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // This is reported when the payout was powered down during a payout operation. It reports the original amount
                    // requested and the amount paid out up to this point for each currency.
                    case CCommands.SMARTPayout.SSP_POLL_INCOMPLETE_PAYOUT:
                        log.AppendText("Incomplete payout\r\n");
                        i += (byte)((response[i + 1] * 11) + 1);
                        break;
                    // This is reported when the payout was powered down during a float operation. It reports the original amount
                    // requested and the amount paid out up to this point for each currency.
                    case CCommands.SMARTPayout.SSP_POLL_INCOMPLETE_FLOAT:
                        log.AppendText("Incomplete float\r\n");
                        i += (byte)((response[i + 1] * 11) + 1);
                        break;
                    // A note has been transferred from the payout unit to the stacker.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_TRANSFERRED_TO_STACKER:
                        log.AppendText("Note transferred to stacker\r\n");
                        i += 7;
                        break;
                    // A note is resting in the bezel waiting to be removed by the user.
                    case CCommands.SMARTPayout.SSP_POLL_NOTE_HELD_IN_BEZEL:
                        log.AppendText("Note in bezel...\r\n");
                        i += 7;
                        break;
                    // The payout has gone out of service, the host can attempt to re-enable the payout by sending the enable payout
                    // command.
                    case CCommands.SMARTPayout.SSP_POLL_PAYOUT_OUT_OF_SERVICE:
                        log.AppendText("Payout out of service...\r\n");
                        break;
                    // The unit has timed out while searching for a note to payout. It reports the value dispensed before the timeout
                    // event.
                    case CCommands.SMARTPayout.SSP_POLL_TIMEOUT:
                        log.AppendText("Timed out searching for a note\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    default:
                        log.AppendText("Unsupported poll response received: " + (int)cmd.ResponseData[i] + "\r\n");
                        break;
                }
            }
            return true;
        }

        /* Non-Command functions */

        // Use the library handler to open the port
        public bool OpenPort()
        {
            return LibraryHandler.OpenPort(ref cmd);
        }

        // Returns a nicely formatted string of the values and currencies of notes stored in each channel
        public string GetChannelLevelInfo()
        {
            string s = "";
            foreach (ChannelData d in m_UnitDataList)
            {
                s += (d.Value / 100f).ToString() + " " + d.Currency[0] + d.Currency[1] + d.Currency[2];
                s += " [" + d.Level + "] = " + ((d.Level * d.Value) / 100f).ToString();
                s += " " + d.Currency[0] + d.Currency[1] + d.Currency[2] + "\r\n";
            }
            return s;
        }

        // Updates all the data in the list.
        public void UpdateData(TextBox log = null)
        {
            foreach (ChannelData d in m_UnitDataList)
            {
                d.Level = CheckNoteLevel(d.Value, d.Currency, log);
                IsNoteRecycling(d.Value, d.Currency, ref d.Recycling, log);
            }
        }

        // This allows the caller to access all the data stored with a channel. An empty ChannelData struct is passed
        // over which gets filled with info.
        public void GetDataByChannel(int channel, ref ChannelData d)
        {
            // Iterate through each 
            foreach (ChannelData dList in m_UnitDataList)
            {
                if (dList.Channel == channel) // Compare channel
                {
                    d = dList; // Copy data from list to param
                    break;
                }
            }
        }

        /* Exception and Error Handling */

        // This is used for generic response error catching, it outputs the info in a
        // meaningful way.
        private bool CheckGenericResponses(TextBox log)
        {
            if (cmd.ResponseData[0] == CCommands.Generic.SSP_RESPONSE_OK)
                return true;
            else
            {
                if (log != null)
                {
                    switch (cmd.ResponseData[0])
                    {
                        case CCommands.Generic.SSP_RESPONSE_CMD_CANNOT_BE_PROCESSED:
                            if (cmd.ResponseData[1] == 0x03)
                            {
                                log.AppendText("Unit responded with a \"Busy\" response, command cannot be " +
                                    "processed at this time\r\n"); 
                            }
                            else
                            {
                                log.AppendText("Command response is CANNOT PROCESS COMMAND, error code - 0x"
                                + BitConverter.ToString(cmd.ResponseData, 1, 1) + "\r\n");
                            }
                            return false;
                        case CCommands.Generic.SSP_RESPONSE_FAIL:
                            log.AppendText("Command response is FAIL\r\n");
                            return false;
                        case CCommands.Generic.SSP_RESPONSE_KEY_NOT_SET:
                            log.AppendText("Command response is KEY NOT SET, renegotiate keys\r\n");
                            return false;
                        case CCommands.Generic.SSP_RESPONSE_PARAMS_OUT_OF_RANGE:
                            log.AppendText("Command response is PARAM OUT OF RANGE\r\n");
                            return false;
                        case CCommands.Generic.SSP_RESPONSE_SOFTWARE_ERROR:
         
                            log.AppendText("Command response is SOFTWARE ERROR\r\n");
                            return false;
                        case CCommands.Generic.SSP_RESPONSE_COMMAND_NOT_KNOWN:
                            log.AppendText("Command response is UNKNOWN\r\n");
                            return false;
                        case CCommands.Generic.SSP_RESPONSE_WRONG_NUMBER_OF_PARAMS:
                            log.AppendText("Command response is WRONG PARAMETERS\r\n");
                            return false;
                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool SendCommand(TextBox log = null)
        {
            // attempt to send the command
            if (!LibraryHandler.SendCommand(ref cmd, ref info))
            {
                m_Comms.UpdateLog(info, true);
                if (log != null) log.AppendText("Sending command failed\r\nPort status: " + cmd.ResponseStatus.ToString() + "\r\n");
                return false;
            }

            // update the log after every command
            m_Comms.UpdateLog(info);
            return true;
        }
    };
}
