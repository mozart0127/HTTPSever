using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on port 8080");
            HTTPServerd server = new HTTPServerd(8080);
            server.Start();


        }
    }
}
