using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SmartPayout
{
    public class SocketServer
    {
        CLogger logger = CLogger.Instance;

        private Boolean stop;
        private SocketHandler handler;
        private Socket listener;

        private static SocketServer instance;

        private SocketServer() { }

        public static SocketServer Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new SocketServer();
                }
                return instance;
            }
        }

        public void StartListening()
        {            
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            stop = false;
            
            try
            {
                listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000));
                listener.Listen(100);

                Thread thread = new Thread(new ThreadStart(Run));
                thread.Start(); 
            }
            catch (Exception e)
            {
                logger.UpdateLog(e.ToString());
            }

        }

        public void Run()
        {
            while (!stop)
            {
                Socket socket = listener.Accept();
                handler = new SocketHandler(socket);

                Thread thread = new Thread(new ThreadStart(handler.Run));
                thread.Start(); 
            }
        }

        public void StopListening() 
        {
            stop = true;
            listener.Close();
        }

        public void Write(String message)
        {
            logger.UpdateLog("Text sent : "+ message);
            handler.Send(message);
        }
    }
}
