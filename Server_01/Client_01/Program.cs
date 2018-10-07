using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Client_01
{
    class Program
    {
        private static Socket clientSocket = null;
        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.UserToken = clientSocket;
            e.RemoteEndPoint = remoteEP;
            clientSocket.ConnectAsync(e);
            while (true) { }
        }
    }
}
