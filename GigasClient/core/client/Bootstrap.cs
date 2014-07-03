using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using GigasClient.core.client.handler;
namespace GigasClient.core
{
    public class Bootstrap
    {
        private Socket client;
        private string ip;
        private int port;
        private bool untilSuccess;
        private int inverval;
        private int timeout;
        private Handler handler;
        public Bootstrap(string ip, int port, bool untilSuccess, int interval, int timeout)
        {
            this.ip = ip;
            this.port = port;
            this.untilSuccess = untilSuccess;
            this.inverval = interval;
            this.timeout = timeout;
            this.handler = new Handler();
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

        }
        public void startConnect()
        {
            Thread connectThread = new Thread(connect);
            connectThread.Start();
        }
        private void connect()
        {
            int timeOutCount = 0;
            IPAddress ip = ip = IPAddress.Parse(this.ip); ;
            IPEndPoint ipe = new IPEndPoint(ip, Convert.ToInt32(this.port));
            Console.WriteLine("start to connect......->" + this.ip + ":" + this.port);
            while (!client.Connected && untilSuccess && timeOutCount<=this.timeout)
            {
                try
                {
                    //client.ConnectAsync();
                    client.Connect(this.ip, this.port);
                }
                catch (Exception e)
                {
                    Console.WriteLine("connect failed,try again after " + this.inverval + " ms");
                    try
                    {
                        Thread.Sleep(inverval*1000);
                        timeOutCount += inverval;
                    }
                    catch (Exception threadSleepException)
                    {
                        Console.WriteLine(threadSleepException);
                        break;
                    }
                }
            }
            if (client.Connected)
            {
                Console.WriteLine("connect->" + client.RemoteEndPoint.ToString() + "success!");
                byte[] bytes =new byte[1024];

                while (client.Connected)
                {
                    client.Receive(bytes);
                }
            }
            else {
                Console.WriteLine("connect fail!");
            }
        }

        private static void connectCallBack(IAsyncResult result)
        {
            if (result == null)
            {
                Console.WriteLine("connect failed!");
                return;
            }
            Socket socket = (Socket)result.AsyncState;
            if (!socket.Connected)
            {
                return;
            }
            socket.EndConnect(result);
            Console.WriteLine("connect->" + socket.RemoteEndPoint.ToString() + "success!");
        }
    }
}
