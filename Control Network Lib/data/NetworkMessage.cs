using System;
using System.Collections.Generic;
using System.Text;

namespace Control_Network_Lib.data
{
    internal class NetworkMessage
    {
        public string messageType;
        public string accessKey;
        public string data;

        public NetworkMessage(string messageType, string accessKey, string data)
        {
            this.messageType = messageType;
            this.accessKey = accessKey;
            this.data = data;
        }
    }
}
