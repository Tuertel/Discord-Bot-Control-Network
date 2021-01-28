using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Network_Lib.data
{
    class BotStats
    {
        //TODO: adjust the variable types to the given information
        public int errors;
        public int shards_active;
        public int shards_wished;
        public int backEnd_RAM_used;
        public int backEnd_RAM_available;
        public int backEnd_RAM_usedTotal;
        public int backEnd_RAM_availableTotal;
        public float backEnd_CPU_Process;
        public float backEnd_CPU_Total;
        public int[] backEnd_averagePing;
    }
}
