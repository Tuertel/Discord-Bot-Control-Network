using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Server_Terminal_Bot_Control_Network.data;

namespace Server_Terminal_Bot_Control_Network.src
{
    class SaveData
    {
        private static string tempFolder = Path.GetTempPath();
        private static byte[] key = { 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10, 0x11 };
        private SaveBotData data;
        private callbackThreaded callback;

        public SaveData(SaveBotData data, callbackThreaded callback)
        {
            this.data = data;
            this.callback = callback;
        }

        public void saveData()
        {
            //creating the streams for JSON writing
            StreamWriter fileWrite = new StreamWriter($"{tempFolder}/tempControlNet");

            JsonSerializer jsonWrite = new JsonSerializer();

            //writing the data into JSON
            jsonWrite.Serialize(fileWrite, data);

            fileWrite.Close();

            //reading the data and encrypt it
            FileStream writeCrypt = new FileStream("save", FileMode.Create);

            Aes aes = Aes.Create();
            aes.Key = key;

            byte[] iv = aes.IV;
            writeCrypt.Write(iv, 0, iv.Length);

            CryptoStream cryptStream = new CryptoStream(writeCrypt, aes.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream readJson = new FileStream($"{tempFolder}/tempControlNet", FileMode.Open);

            readJson.CopyTo(cryptStream);
            readJson.Close();
            cryptStream.Close();
            writeCrypt.Close();

            callback();
        }

        public delegate void callbackThreaded();

        public static SaveBotData loadData()
        {
            if (!File.Exists("save")) return null;
            //decrypting the data
            FileStream readCrypt = new FileStream("save", FileMode.Open);

            Aes aes = Aes.Create();
            byte[] iv = new byte[aes.IV.Length];
            readCrypt.Read(iv, 0, iv.Length);

            CryptoStream cryptStream = new CryptoStream(readCrypt, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);

            FileStream writeJson = new FileStream($"{tempFolder}/tempControlNet", FileMode.Create);

            cryptStream.CopyTo(writeJson);

            readCrypt.Close();
            cryptStream.Close();
            writeJson.Close();

            StreamReader fileReader = new StreamReader($"{tempFolder}/tempControlNet");

            JsonSerializer serializer = new JsonSerializer();
            SaveBotData data = (SaveBotData)serializer.Deserialize(fileReader, typeof(SaveBotData));
            fileReader.Close();

            return data;
        }
    }
}
