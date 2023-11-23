using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Server
{
    static class Parser
    {
        public static T DeserializeJson<T>(MessageEventArgs message)
        {
            // Deserialize the JSON string into the specified type
            T result = JsonConvert.DeserializeObject<T>(message.Data);

            return result;
        }
    }
}




