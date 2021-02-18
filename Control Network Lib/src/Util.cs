using System;
using System.Collections.Generic;
using System.Text;

namespace Control_Network_Lib.src
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
