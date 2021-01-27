using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Server_Terminal_Bot_Control_Network.src
{
    class MyLogger
    {
        private static Serilog.ILogger logger = new Serilog.LoggerConfiguration()
            .WriteTo.File("Log.txt")
            .CreateLogger();
        private static List<string> logList = new List<string>();

        public static void log(string logContent)
        {
            logger.Information(logContent);
            logList.Add($"{generateDate()} [INF] {logContent}");
        }

        public static void logWarn(string logContent)
        {
            logger.Warning(logContent);
            logList.Add($"{generateDate()} [WRN] {logContent}");
        }

        public static void logErr(string logContent)
        {
            logger.Error(logContent);
            logList.Add($"{generateDate()} [ERR] {logContent}");
        }

        public static void logFatal(string logContent)
        {
            logger.Fatal(logContent);
        }

        public static string[] getLog()
        {
            return logList.ToArray();
        }

        private static string generateDate()
        {
            var date = DateTime.Now;
            return $"{date:yyyy}-{date:MM}-{date:dd} {date:HH:mm:ss}.{date:fff} {date:zzz}";
        }
    }
}
