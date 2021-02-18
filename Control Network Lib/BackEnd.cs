using System;
using Control_Network_Lib.data;
using Control_Network_Lib.src;
using WatsonWebsocket;
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
            WatsonWsClient wsClient = new WatsonWsClient(new Uri(url));
            wsClient.MessageReceived += receivedMessage;

        }

        private static void receivedMessage(object sender, MessageReceivedEventArgs args)
        {
            //converting the received Message to a NetworkMessage Object
            string msg = Util.ByteArrayToString(args.Data);
            NetworkMessage message;
            try
            {
                message = JsonConvert.DeserializeObject<NetworkMessage>(msg);
            }
            catch (JsonSerializationException) { return; }

            //NetworkMessage is getting processed
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
