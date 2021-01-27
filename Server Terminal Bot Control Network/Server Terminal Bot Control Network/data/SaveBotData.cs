using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server_Terminal_Bot_Control_Network.src;

namespace Server_Terminal_Bot_Control_Network.data
{
    class SaveBotData
    {
        public BotData[] bots;

        public SaveBotData(int botLength)
        {
            bots = new BotData[botLength];
        }

        public void addBotsToData(Bot[] bots)
        {
            for(int i = 0; i < bots.Length; i++)
            {
                this.bots[i] = new BotData(bots[i].name, bots[i].address, bots[i].key, bots[i].getID());
            }
        }
    }
}
