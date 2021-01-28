using System;
using Control_Network_Lib.data;
using Control_Network_Lib.src;
using WebSocketSharp;
using Newtonsoft.Json;

namespace Control_Network_Lib
{
    public class BackEnd
    //TODO: create the lib to work with the Control Network
    {
        internal static BotStats botStats;
        private static string accessKey;

        public static void BackEndStart(string url, string key)
        {
            accessKey = key;
            WebSocket ws = new WebSocket(url);
            ws.OnMessage += (sender, e) =>
            {
                if (e.IsText) recivedTMessage(e.Data);
            };
        }

        private static void recivedTMessage(string msg)
        {
            NetworkMessage message;
            try
            {
                message = JsonConvert.DeserializeObject<NetworkMessage>(msg);
            }catch(JsonSerializationException) { return; }

            if (message.accessKey != accessKey) return;
            switch(message.messageType)
            {
                case "getStats":
                    FormatedBotStats fBotStats = new FormatedBotStats();
                    fBotStats.errors = botStats.errors;
                    fBotStats.shards = BotStatsFormater.shardFormater(botStats.shards_active, botStats.shards_wished);
                    fBotStats.backEnd_RAM = BotStatsFormater.backEnd_RAMFormater();
                    fBotStats.backEnd_CPU = BotStatsFormater.backEnd_CPUFormater();
                    fBotStats.backEnd_averagePing = BotStatsFormater.backEnd_AveragePing(botStats.backEnd_averagePing);
                    //TODO: Send the data back
                break;
            }
        }
    }
}
