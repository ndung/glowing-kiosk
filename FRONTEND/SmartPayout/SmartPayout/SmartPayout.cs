using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ITLlib;
using System.Windows.Forms;

namespace SmartPayout
{
    public class SmartPayout
    {
        private static SmartPayout instance;

        private SmartPayout()
        {
            reconnectionTimer.Tick += new EventHandler(reconnectionTimer_Tick);
            Payout = new CPayout();
        }

        public static SmartPayout Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SmartPayout();
                }
                return instance;
            }
        }

        CLogger logger = CLogger.Instance;

        // Variables used by this program.
        bool Running = false;
        int reconnectionAttempts = 5;
        System.Windows.Forms.Timer reconnectionTimer = new System.Windows.Forms.Timer();
        public CPayout Payout
        {
            get;
            private set;
        }

        Thread thread;
        String trxId;

        public Boolean IsDispensing()
        {
            return Payout.IsDispensing();
        }

        public void Start(String trxId)
        {
            Payout.TrxId = trxId;
            if (!Running)
            {
                //Payout.CommandStructure.ComPort = Global.ComPort;
                Payout.CommandStructure.ComPort = "COM"+DBConnect.Instance.GetSmartPayoutPort();
                Payout.CommandStructure.SSPAddress = Global.SSPAddress;
                Payout.CommandStructure.Timeout = 3000;

                // connect to validator
                if (ConnectToValidator(reconnectionAttempts, 2))
                {
                    Running = true;
                    SocketServer.Instance.Write("RS010100" + trxId);
                    logger.UpdateLog("\r\nPoll Loop\r\n*********************************\r\n");
                }
                else
                {
                    SocketServer.Instance.Write("RS010101" + trxId);
                }

                thread = new Thread(new ThreadStart(Run));
                thread.Start();
            }
            else
            {
                SocketServer.Instance.Write("RS010100" + trxId);
            }
        }

        public void Run()
        {
            while (Running)
            {
                // if the poll fails, try to reconnect
                if (Payout.DoPoll() == false)
                {
                    logger.UpdateLog("Poll failed, attempting to reconnect...\r\n");
                    while (true)
                    {
                        Payout.SSPComms.CloseComPort(); // close com port

                        // attempt reconnect, pass over number of reconnection attempts
                        if (ConnectToValidator(reconnectionAttempts, 2) == true)
                        {
                            break; // if connection successful, break out and carry on
                        }                        
                        // if not successful, stop the execution of the poll loop                        
                        Payout.SSPComms.CloseComPort(); // close com port before return
                        thread.Join();
                        return;
                    }
                    logger.UpdateLog("Reconnected\r\n");
                }
                Thread.Sleep(250); // Yield to free up CPU
            }

            //close com port
            Payout.SSPComms.CloseComPort();
            SocketServer.Instance.Write("RS010400" + trxId);
            thread.Join();
        }

        // This function opens the com port and attempts to connect with the validator. It then negotiates
        // the keys for encryption and performs some other setup commands.
        private bool ConnectToValidator(int attempts, int interval)
        {
            // setup timer
            reconnectionTimer.Interval = interval * 1000; // ms
            // run for number of attempts specified
            for (int i = 0; i < attempts; i++)
            {
                // close com port in case it was open
                Payout.SSPComms.CloseComPort();

                // turn encryption off for first stage
                Payout.CommandStructure.EncryptionStatus = false;

                // if the key negotiation is successful then set the rest up
                if (Payout.OpenComPort() && Payout.NegotiateKeys())
                {
                    Payout.CommandStructure.EncryptionStatus = true; // now encrypting
                    // find the max protocol version this validator supports
                    byte maxPVersion = FindMaxProtocolVersion();
                    if (maxPVersion >= 6)
                    {
                        Payout.SetProtocolVersion(maxPVersion);
                    }
                    else
                    {
                        logger.UpdateLog("This program does not support slaves under protocol 6! ERROR");
                        return false;
                    }
                    // get info from the validator and store useful vars
                    Payout.SetupRequest();
                    // check this unit is supported
                    if (!IsUnitValid(Payout.UnitType))
                    {
                        logger.UpdateLog("Unsupported type shown by SMART Payout, this SDK supports the SMART Payout only");
                        //Application.Exit();
                        return false;
                    }
                    // inhibits, this sets which channels can receive notes
                    Payout.SetInhibits();
                    // enable, this allows the validator to operate
                    Payout.EnableValidator();
                    // enable the payout system on the validator
                    Payout.EnablePayout();
                    return true;
                }
                // Set timer
                reconnectionTimer.Enabled = true;
                while (reconnectionTimer.Enabled) Application.DoEvents();
            }
            return false;
        }

        // This function finds the maximum protocol version that a validator supports. To do this
        // it attempts to set a protocol version starting at 6 in this case, and then increments the
        // version until error 0xF8 is returned from the validator which indicates that it has failed
        // to set it. The function then returns the version number one less than the failed version.
        private byte FindMaxProtocolVersion()
        {
            // not dealing with protocol under level 6
            // attempt to set in validator
            byte b = 0x06;
            while (true)
            {
                Payout.SetProtocolVersion(b);
                if (Payout.CommandStructure.ResponseData[0] == CCommands.SSP_RESPONSE_CMD_FAIL)
                    return --b;
                b++;

                // catch runaway
                if (b > 12)
                    return 0x06; // return default
            }
        }

        private bool IsUnitValid(char unitType)
        {
            if (unitType == (char)0x06) // 0x06 is Payout, no other types supported by this program
                return true;
            return false;
        }

        public void Halt(String trxId)
        {            
            this.trxId = trxId;
            if (!Running)
            {
                SocketServer.Instance.Write("RS010400" + trxId);
            }
            else
            {
                Running = false;
            }
        }

        public void SmartEmpty()
        {
            Payout.SmartEmpty();
        }

        public void Empty()
        {
            Payout.EmptyPayoutDevice();
        }

        public void Dispense(String amount)
        {
            if (amount != "")
            {
                Payout.PayoutByDenomData = null;
                int n = Int32.Parse(amount) * 100;
                Payout.PayoutAmount(n);
            }
        }

        public void Dispense(Dictionary<Int32,String> amounts)
        {
            if (amounts != null)
            {
                bool payoutRequired = false;
                byte[] data = new byte[9 * Payout.NumberOfChannels]; // create to size of maximum possible
                byte length = 0;
                int dataIndex = 0;
                byte denomsToPayout = 0;
                // For each denomination
                for (int i = 0; i < Payout.NumberOfChannels; i++)
                {
                    try
                    {                        
                        if (amounts.ContainsKey(Payout.UnitDataList[i].Value/100)) { 
                            String amount = amounts[Payout.UnitDataList[i].Value/100];
                                                    
                            denomsToPayout++;
                            length += 9; // 9 bytes per denom to payout (2 amount, 4 value, 3 currency)
                            payoutRequired = true; // need to do a payout as there is now > 0 denoms

                            // Number of this denomination to payout
                            UInt16 numToPayout = UInt16.Parse(amount);
                            byte[] b = CHelpers.ConvertIntToBytes(numToPayout);
                            data[dataIndex++] = b[0];
                            data[dataIndex++] = b[1];

                            // Value of this denomination
                            ChannelData d = Payout.UnitDataList[i];
                            b = CHelpers.ConvertIntToBytes(d.Value);
                            data[dataIndex++] = b[0];
                            data[dataIndex++] = b[1];
                            data[dataIndex++] = b[2];
                            data[dataIndex++] = b[3];

                            // Currency of this denomination
                            data[dataIndex++] = (Byte)d.Currency[0];
                            data[dataIndex++] = (Byte)d.Currency[1];
                            data[dataIndex++] = (Byte)d.Currency[2];
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.UpdateLog(ex.ToString());
                        payoutRequired = false; // don't payout on exception
                    }
                }

                if (payoutRequired)
                {                    
                    Payout.PayoutByDenomData = amounts;
                    Payout.PayoutByDenomination(denomsToPayout, data, length);                    
                }
            }
        }

        public void Float(String minPayout, String floatAmount)
        {
            try
            {
                double mp = double.Parse(minPayout) * 100;
                double fa = double.Parse(floatAmount) * 100;
                Payout.SetFloat((Int32)mp, (Int32)fa);
            }
            catch (Exception ex)
            {
                logger.UpdateLog(ex.ToString()+ " EXCEPTION");
                return;
            }
        }

        public void Reset()
        {
            Payout.Reset();
            // Shut port to force reconnect
            Payout.SSPComms.CloseComPort();
        }

        public void SetNoteRoute(String idx, bool stack)
        {
            ChannelData d = new ChannelData();
            Payout.GetDataByChannel(Int32.Parse(idx), ref d);
            Payout.ChangeNoteRoute(d.Value, d.Currency, stack);
        }

        private void reconnectionTimer_Tick(object sender, EventArgs e)
        {
            reconnectionTimer.Enabled = false;
        }
    }
}
