using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class WebSocketLogger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
    }
}
