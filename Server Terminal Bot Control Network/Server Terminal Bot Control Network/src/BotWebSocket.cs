using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatsonWebsocket;

namespace Server_Terminal_Bot_Control_Network.src
{
    class BotWebSocket
    {
        private WatsonWsServer ws;

        public BotWebSocket(string url)
        {
            ws = new WatsonWsServer(new Uri(url));

        }

        public void connect()
        {
            ws.Start();
        }

        public void disconnect()
        {
            ws.Stop();
        }

        public void send(string msg)
        {
            //ws.SendAsync(IPPORT, Util.StringToByteArray(msg));
        }
    }
}
