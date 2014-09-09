using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SmartPayout
{
    public class SocketHandler
    {
        private Socket socket;

        CLogger logger = CLogger.Instance;

        public SocketHandler(Socket socket)
        {
            this.socket = socket;
        }

        public void Run()
        {
            try
            {
                String msgLength = null;
                String data = null;
                while (true)
                {
                    byte[] bytes = new byte[4];
                    int bytesRec = socket.Receive(bytes);
                    msgLength = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    int length = Convert.ToInt32(msgLength);
                    msgLength = null;

                    bytes = new byte[length];
                    bytesRec = socket.Receive(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    logger.UpdateLog("Text received : "+ data);
                    Thread.Sleep(1000);
                    String msgType = data.Substring(0, 2);
                    String deviceCode = data.Substring(2, 2);
                    String deviceCommand = data.Substring(4, 2);
                    String trxId = "";
                    if (!deviceCommand.Equals("09")) { 
                        trxId = data.Substring(8, 6);
                    }
                    logger.UpdateLog("msgType : " + msgType);
                    logger.UpdateLog("device command : " + deviceCommand);
                    logger.UpdateLog("trxId : " + trxId);                    
                    if (deviceCommand.Equals("01"))
                    {
                        SmartPayout.Instance.Start(trxId);
                    }
                    else if (deviceCommand.Equals("03"))
                    {
                        Thread.Sleep(2000);
                        if (!SmartPayout.Instance.IsDispensing())
                        {
                            if (data.Length > 26)
                            {
                                int dataLength = (data.Length - 14) / 14;
                                Dictionary<Int32, String> map = new Dictionary<Int32, String>();
                                for (int i = 0; i < dataLength; i++)
                                {
                                    Int32 denom = Int32.Parse(data.Substring(14 + i * 14, 12));
                                    String amount = data.Substring(26 + i * 14, 2);
                                    map.Add(denom, amount);
                                }
                                SmartPayout.Instance.Dispense(map);
                            }
                            else
                            {
                                String amount = data.Substring(14, 12);
                                SmartPayout.Instance.Dispense(amount);
                            }
                        }
                    }
                    else if (deviceCommand.Equals("04"))
                    {
                        SmartPayout.Instance.Halt(trxId);
                    }
                    else if (deviceCommand.Equals("05"))
                    {
                        SmartPayout.Instance.Payout.ResetFromServer = true;
                        SmartPayout.Instance.Reset();
                    }
                    else if (deviceCommand.Equals("06"))
                    {
                        SmartPayout.Instance.Empty();
                    }
                    else if (deviceCommand.Equals("07"))
                    {
                        String amount = data.Substring(14, 12);
                        String minAmount = data.Substring(26, 12);
                        SmartPayout.Instance.Float(minAmount, amount);
                    }                   
                    else if (deviceCommand.Equals("09"))
                    {
                        String idx = data.Substring(8, 1);
                        String idxRoute = data.Substring(9, 1);
                        if (idxRoute.Equals("0"))
                        {
                            SmartPayout.Instance.SetNoteRoute(idx, false);
                        }
                        else if (idxRoute.Equals("1"))
                        {
                            SmartPayout.Instance.SetNoteRoute(idx, true);
                        }
                    }
                    data = null;
                }
            }
            catch (Exception e)
            {
                logger.UpdateLog(e.ToString());
            }
        }

        public void Send(String data)
        {
            int length = data.Length;
            data = length.ToString().PadLeft(4, '0') + data;
            byte[] msg = Encoding.ASCII.GetBytes(data);
            socket.Send(msg);
        }

    }
}
