using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPayout
{
    class Program
    {
        static void Main(string[] args)
        {            
            SocketServer.Instance.StartListening();            
        }
    }
}
