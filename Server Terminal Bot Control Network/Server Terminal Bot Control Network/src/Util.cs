using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Terminal_Bot_Control_Network.src
{
    class Util
    {
        private static ASCIIEncoding encASCII = new ASCIIEncoding();

        internal static byte[] StringToByteArray(string str)
        {
            return encASCII.GetBytes(str);
        }

        internal static string ByteArrayToString(byte[] arr)
        {
            return encASCII.GetString(arr);
        }
    }
}
