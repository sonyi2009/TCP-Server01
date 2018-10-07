using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server_01
{
    class Program
    {
        private static Socket serverSocket = null;
        static void Main(string[] args)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9999);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                IPEndPoint ip = (IPEndPoint)clientSocket.RemoteEndPoint;
                Console.WriteLine(ip.Port.ToString());
                Thread thread = new Thread(listenClientConnect);
                thread.Start(clientSocket);
            }
        }
        private static void listenClientConnect(object obj)
        {
            Socket clientSocket = obj as Socket;
            //Socket clientSocket = serverSocket.Accept();
            clientSocket.Send(Encoding.Default.GetBytes("你可以的啊"));
            Thread recThread = new Thread(ReceiveClientMessage);
            recThread.Start(clientSocket);
        }

        private static void ReceiveClientMessage(object clientSocket)
        {
            Socket socket = clientSocket as Socket;
            byte[] buffer = new byte[1024];
            int length = socket.Receive(buffer);
            Console.WriteLine(Encoding.Default.GetString(buffer,0, length));
        }
    }
}
