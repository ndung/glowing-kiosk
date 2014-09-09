using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPayout
{
    public class ChannelData
    {
        public int Value;
        public byte Channel;
        public char[] Currency;
        public int Level;
        public bool Recycling;
        public ChannelData()
        {
            Value = 0;
            Channel = 0;
            Currency = new char[3];
            Level = 0;
            Recycling = false;
        }
    };
}
