using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Server_Terminal_Bot_Control_Network.data;

namespace Server_Terminal_Bot_Control_Network.src
{
    class Main
    {
        public Bot[] bots = new Bot[0];
        private bool savingQueued = false;
        private bool saving = false;

        public Main()
        {
            loadData();
        }

        public void addNewBotToList(string name, string address, string key)
        {
            Bot b = new Bot(name, address, key, 0);
            b.generateID(bots);
            addBotToList(b);
        }

        private void addBotToList(Bot b)
        {
            Bot[] tempList = new Bot[bots.Length + 1];
            for(int i = 0; i < bots.Length; i++)
            {
                tempList[i] = bots[i];
            }
            tempList[bots.Length] = b;
            bots = tempList;
            saveData();
        }

        public Bot[] getBots() { return bots; }

        public string[] getBotNames()
        {
            string[] names = new string[bots.Length];

            for(int i = 0; i < bots.Length; i++)
            {
                names[i] = bots[i].name;
            }

            return names;
        }

        public Bot getBotByName(string botName)
        {
            foreach (Bot b in bots)
            {
                if (b.name == botName)
                {
                    return b;
                }
            }

            return null;
        }

        public bool deleteBotByName(string botName)
        {
            Bot[] botList = new Bot[bots.Length - 1];
            bool found = false;
            for(int i = 0; i < bots.Length; i++)
            {
                if(bots[i].name == botName)
                {
                    found = true;
                }
                else if(i == bots.Length - 1 && !found)
                {
                    return false;
                }
                else if(found)
                {
                    botList[i - 1] = bots[i];
                }
                else
                {
                    botList[i] = bots[i];
                }
            }

            bots = botList;
            saveData();
            return true;
        }

        public void saveData()
        {
            if(savingQueued && saving)
            {
                return;
            }
            else if(saving)
            {
                savingQueued = true;
                savingQueue();
            }
            else if(!savingQueued && !saving)
            {
                saving = true;
                //generating the save Object
                SaveBotData data = new SaveBotData(bots.Length);
                data.addBotsToData(bots);

                SaveData threaded = new SaveData(data, savingCallback);

                Thread saveing = new Thread(new ThreadStart(threaded.saveData));
                saveing.Start();
            }
        }

        public void savingQueue()
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 1);
            time.Tick += new EventHandler(queuedSaving);
        }

        private void queuedSaving(object sender, EventArgs e)
        {
            while(saving) { }
            savingQueued = false;
            saveData();
        }

        private void savingCallback()
        {
            saving = false;
        }

        public void loadData()
        {
            SaveBotData data = SaveData.loadData();
            if(data != null)
            {
                foreach (BotData bd in data.bots)
                {
                    addBotToList(new Bot(bd.name, bd.address, bd.key, bd.id));
                }

            }
        }
    }
}
