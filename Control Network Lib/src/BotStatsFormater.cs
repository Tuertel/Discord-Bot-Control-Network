using System;
using System.Collections.Generic;
using System.Text;

namespace Control_Network_Lib.src
{
    extern class BotStatsFormater
    {
        
        public delegate string ShardFormater(int onlineShards, int expectedShards);
        public delegate string BackEnd_RAMFormater();
        public delegate string BackEnd_CPUFormater();
        public delegate string BackEnd_averagePing(int[] pings);
    }
}
