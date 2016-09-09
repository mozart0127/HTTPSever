using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace HTTPServer
{
    public class Response
    {
        private Byte[] data = null;
        private String Status;
        private String mime;

        private Response(String status,String mime,Byte[] data)
        {
            Status = status;
            this.data = data;
            this.mime = mime;
        }

        public static Response From(Request request)
        {
            if (request == null)
               return  MakeNullRequest();
            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + HTTPServerd.WEB_DIR + request.URL;
            }
            else
                return MakePageNotSupported();
            return MakeNullRequest();
        }

        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServerd.MSG_DIR + "400.html";
            FileInfo files = new FileInfo(file);
            FileStream fs = files.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return new Response("400 Bad request","html/text", d);
        }

        private static Response MakePageNotFound()
        {
            String file = Environment.CurrentDirectory + HTTPServerd.MSG_DIR + "404.html";
            FileInfo files = new FileInfo(file);
            FileStream fs = files.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return new Response("404 Page Not Found", "html/text", d);
        }
        private static Response MakePageNotSupported()
        {
            String file = Environment.CurrentDirectory + HTTPServerd.MSG_DIR + "405.html";
            FileInfo files = new FileInfo(file);
            FileStream fs = files.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return new Response("405 Page Not Supported", "html/text", d);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
           // writer.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent.Type: {3}\r\nAccept.Ranges: bytes\r\nContent.length: {4\r\n}",HTTPServerd.VERSION,Status,HTTPServerd.NAME,mime,data.Length));
            stream.Write(data,0,data.Length);
        }
    }
}
