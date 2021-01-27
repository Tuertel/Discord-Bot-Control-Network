using System;
using System.Collections.Generic;
using System.Text;

namespace Control_Network_Lib.src
{
    public class BotStatsFormater
    {
        public static ShardFormater shardFormater = shardFormating;
        public static BackEnd_RAMFormater backEnd_RAMFormater;
        public static BackEnd_CPUFormater backEnd_CPUFormater;
        public static BackEnd_averagePing backEnd_AveragePing = backEnd_ping;

        public delegate string ShardFormater(int onlineShards, int expectedShards);
        public delegate string BackEnd_RAMFormater();
        public delegate string BackEnd_CPUFormater();
        public delegate string BackEnd_averagePing(int[] pings);

        private static string shardFormating(int onlineShards, int expectedShards)
        {
            return $"{onlineShards}/{expectedShards} ({expectedShards / 100 * onlineShards})";
        }

        private static string backEnd_ping(int[] pings)
        {
            int allpings = 0;

            foreach (int p in pings)
            {
                allpings += p;
            }

            return $"{allpings / pings.Length}";
        }
    }
}
