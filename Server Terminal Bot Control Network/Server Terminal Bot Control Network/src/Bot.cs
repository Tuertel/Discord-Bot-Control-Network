using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server_Terminal_Bot_Control_Network.data;

namespace Server_Terminal_Bot_Control_Network.src
{
    class Bot
    {
        public string name;
        public string address;
        public string key;
        private int id;
        private BotWebSocket ws;
        public BotStats bStats;
        public ShardStats[] shards;

        public Bot(string name, string address, string key, int id)
        {
            this.name = name;
            this.address = address;
            this.key = key;
            this.id = id;
            createWS();
        }

        public void generateID(Bot[] bots)
        {
            bool exsists = false;

            do
            {
                var ran = new Random();
                id = ran.Next(2000000000);

                for (int i = 0; i < bots.Length; i++)
                {
                    if (bots[i].getID() == id)
                    {
                        exsists = true;
                    }
                }
            } while (exsists);
        }

        public int getID()
        {
            return id;
        }

        private void createWS()
        {
            ws = new BotWebSocket(address);
            ws.connect();
        }

        public void disconnect()
        {
            ws.disconnect();
        }

        public void changedBotAddress()
        {
            disconnect();
            createWS();
        }

        //TODO: add logic for periodically requesting the information (5-10s?)
        //TODO: add logic for command execution
    }
}
