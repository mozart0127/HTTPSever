using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace HTTPServer
{

    public class HTTPServerd
    {
        public const String MSG_DIR = "/root/msg/";
        public const String WEB_DIR = "/root/web/";
        public const String VERSION = "HTTP/1.1";
        public const String NAME = "Zheng HTTP SERVER v0.1";
        private bool isRuning = false;
        private TcpListener listener;

        public HTTPServerd(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);

        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            isRuning = true;
            listener.Start();
            while (isRuning)
            {
                Console.WriteLine("waiting for the connection....");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("client connection");
                HandleClient(client);

                client.Close();
            }
            isRuning = false;
            listener.Stop();
            
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            String msg = "";
            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine()+"\n";

            }

            Debug.WriteLine("Request: \n"+msg);
            Request request = Request.GetRequest(msg);
            Response response = Response.From(request);
            response.Post(client.GetStream());
        }


    }
}
