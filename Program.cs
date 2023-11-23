using System;
using System.Reflection.Metadata;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace ProgramowanieZaawansowane
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new WsServer(System.Net.IPAddress.Loopback, 8888);
            server.Start();

            //server.Stop();


            Console.ReadKey();
        }
    }
}
