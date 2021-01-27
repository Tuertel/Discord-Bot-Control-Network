using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Terminal_Bot_Control_Network.data
{
    class BotData
    {
        public string name;
        public string address;
        public string key;
        public int id;

        public BotData(string name, string address, string key, int id)
        {
            this.name = name;
            this.address = address;
            this.key = key;
            this.id = id;
        }
    }
}
