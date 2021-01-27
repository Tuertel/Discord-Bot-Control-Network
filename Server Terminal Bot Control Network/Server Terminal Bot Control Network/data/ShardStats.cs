using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Terminal_Bot_Control_Network.data
{
    class ShardStats
    {
        public string identifier;
        public string status;
        public int ram;
        public float cpu_process;
        public float cpu_total;
        public int ping;
        public int guilds;
        public int errors;
    }
}
