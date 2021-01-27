using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server_Terminal_Bot_Control_Network.src
{
    class BotWebSocket
    {
        private WebSocketServer ws;

        public BotWebSocket(string url)
        {
            ws = new WebSocketServer("ws://127.0.0.1");
            ws.AddWebSocketService<BotWebSocketBehavior>($"/{url}");
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
            ws.WebSocketServices.Broadcast(msg);
        }
    }

    class BotWebSocketBehavior : WebSocketBehavior
    {
        public BotWebSocketBehavior()
        {
            IgnoreExtensions = true;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Send("Hello!");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
            if(e.IsText) Console.WriteLine(e.Data);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }
}
