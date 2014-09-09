using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPayout
{
    class Printer
    {
        SerialPort serialPort;

        public Printer()
        {
            serialPort = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One);
            serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = true;
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                //Console.
            }

            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
            String str = serialPort.ReadExisting();
            
            
        }
    }
}
