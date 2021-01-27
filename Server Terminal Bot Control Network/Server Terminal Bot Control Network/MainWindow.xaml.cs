using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Server_Terminal_Bot_Control_Network.src;

namespace Server_Terminal_Bot_Control_Network
{
    public partial class MainWindow : Window
    {
        private Main main;
        private int logEntrys = 0;
        private DispatcherTimer clearFeedback = new DispatcherTimer();
        //TODO: add Commands UI stuff

        public MainWindow()
        {
            clearFeedback.Tick += new EventHandler(clearFeedbackPrints);
            clearFeedback.Interval = new TimeSpan(0, 0, 5);

            DispatcherTimer logUpdate = new DispatcherTimer();
            logUpdate.Tick += new EventHandler(log_updateLog);
            logUpdate.Interval = new TimeSpan(0, 0, 1);
            logUpdate.Start();

            main = new Main();
            MyLogger.log("Loaded saved Bot Entry's.");

            InitializeComponent();
            MyLogger.log("Server Terminal Bot Control Network started.");

            updateBotsList();
        }

        private void addBot_saveNewBot(object sender, RoutedEventArgs e)
        {
            Bot[] bots = main.getBots();

            if (addBot_inputBotName.Text == "")
            {
                printErr("addBot", "The Bot needs to have a Name!");
                return;
            }
            else if(addBot_inputBotAddress.Text == "")
            {
                printErr("addBot", "The Bot needs to have a Address!");
                return;
            }
            else
            {
                for(int i = 0; i < bots.Length; i++)
                {
                    if(bots[i].name == addBot_inputBotName.Text)
                    {
                        printErr("addBot", "The Name already exists!");
                        return;
                    } 
                    else if(bots[i].address == addBot_inputBotAddress.Text)
                    {
                        printErr("addBot", "The Address already exists!");
                        return;
                    }
                }
            }

            main.addNewBotToList(addBot_inputBotName.Text, addBot_inputBotAddress.Text, addBot_inputBotKey.Password);
            updateBotsList();
            print("addBot", "Successfully created the Bot " + addBot_inputBotName.Text + ".");
            MyLogger.log($"Added new Bot with the name: {addBot_inputBotName.Text}");
            clearInputs("addBot");
        }

        private void manageBots_SelectedBot(object sender, SelectionChangedEventArgs e)
        {
            if(manageBots_List.SelectedItem != null)
            {
                string botName = manageBots_List.SelectedItem.ToString();
                Bot bot = main.getBotByName(botName);

                manageBots_fieldBotName.Text = bot.name;
                manageBots_fieldBotAddress.Text = bot.address;
                manageBots_fieldBotKey.Password = bot.key;
            }
            else
            {
                clearInputs("manageBots");
            }
        }

        private void manageBots_deleteBot(object sender, RoutedEventArgs e)
        {
            if (manageBots_List.SelectedItem != null)
            {
                string botName = manageBots_List.SelectedItem.ToString();
                bool found = main.deleteBotByName(botName);

                if(found)
                {
                    print("manageBots", "Successfully deleted the Bot Entry.");
                    MyLogger.log($"Deleted the Bot Entry with the name: {botName}");
                }
                else
                {
                    printErr("manageBots", "Could not find the Bot Entry!");
                }
            }
            updateBotsList();
        }

        private void manageBots_saveChanges(object sender, RoutedEventArgs e)
        {
            if (manageBots_List.SelectedItem != null)
            {
                string botName = manageBots_List.SelectedItem.ToString();
                Bot bot = main.getBotByName(botName);
                Bot[] bots = main.getBots();

                if (manageBots_fieldBotName.Text == "")
                {
                    printErr("manageBots", "The Bot needs to have a Name!");
                    return;
                }
                else if (manageBots_fieldBotAddress.Text == "")
                {
                    printErr("manageBots", "The Bot needs to have a Address!");
                    return;
                }
                else
                {
                    for (int i = 0; i < bots.Length; i++)
                    {
                        if (bots[i].name == manageBots_fieldBotName.Text && bot.name != manageBots_fieldBotName.Text)
                        {
                            printErr("manageBots", "The Name already exists!");
                            return;
                        }
                        else if (bots[i].address == manageBots_fieldBotAddress.Text && bot.address != manageBots_fieldBotAddress.Text)
                        {
                            printErr("manageBots", "The Address already exists!");
                            return;
                        }
                    }
                }

                bool sameName = manageBots_fieldBotName.Text == bot.name ? true : false;
                bool sameAddress = manageBots_fieldBotAddress.Text == bot.address ? true : false;
                bool sameKey = bot.key == manageBots_fieldBotKey.Password ? true : false;

                string oldName = bot.name;

                switch ($"{sameName}-{sameAddress}-{sameKey}")
                {
                    case "False-False-False":
                        bot.name = manageBots_fieldBotName.Text;
                        bot.address = manageBots_fieldBotAddress.Text;
                        bot.key = manageBots_fieldBotKey.Password;
                        print("manageBots", "Successfully changed the Name, the Address and the Password of the Bot Entry.");
                        MyLogger.log($"The Bot \"{oldName}\" has been renamed to \"{bot.name}\", the Address has been changed to \"{bot.address}\" and the Password has been changed.");
                        main.saveData();
                        bot.changedBotAddress();
                    break;

                    case "False-False-True":
                        bot.name = manageBots_fieldBotName.Text;
                        bot.address = manageBots_fieldBotAddress.Text;
                        print("manageBots", "Successfully changed the Name and the Address of the Bot Entry.");
                        MyLogger.log($"The Bot \"{oldName}\" has been renamed to \"{bot.name}\" and the Address has been changed to: {bot.address}");
                        main.saveData();
                        bot.changedBotAddress();
                    break;

                    case "False-True-False":
                        bot.name = manageBots_fieldBotName.Text;
                        bot.key = manageBots_fieldBotKey.Password;
                        print("manageBots", "Successfully changed the Name and the Password of the Bot Entry.");
                        MyLogger.log($"The Bot \"{oldName}\" has been renamed to \"{bot.name}\" and the Password has been changed.");
                        main.saveData();
                    break;

                    case "False-True-True":
                        bot.name = manageBots_fieldBotName.Text;
                        print("manageBots", "Successfully changed the Name of the Bot Entry.");
                        MyLogger.log($"The Bot \"{oldName}\" has been renamed to: {bot.name}");
                        main.saveData();
                    break;

                    case "True-False-False":
                        bot.address = manageBots_fieldBotAddress.Text;
                        bot.key = manageBots_fieldBotKey.Password;
                        print("manageBots", "Successfully changed the Address and the Password of the Bot Entry.");
                        MyLogger.log($"The Address has been changed to \"{bot.address}\" and the Password of the bot \"{bot.name}\" has been changed.");
                        main.saveData();
                        bot.changedBotAddress();
                    break;

                    case "True-False-True":
                        bot.address = manageBots_fieldBotAddress.Text;
                        print("manageBots", "Successfully changed the Address of the Bot Entry.");
                        MyLogger.log($"The Address of the bot \"{bot.name}\" has been changed to: {bot.address}");
                        main.saveData();
                        bot.changedBotAddress();
                    break;

                    case "True-True-False":
                        bot.key = manageBots_fieldBotKey.Password;
                        print("manageBots", "Successfully changed the Password of the Bot Entry.");
                        MyLogger.log($"The Password of the bot \"{bot.name}\" has been changed.");
                        main.saveData();
                    break;

                    case "True-True-True":
                        print("manageBots", "Nothing has been changed by the Bot Entry.");
                    break;

                    default:
                        printErr("manageBots", $"something went wrong!"); 
                        MyLogger.logErr($"The System could not Identify witch action to take by the Changing of a Entry's Information. ({sameName}-{sameAddress}-{sameKey})");
                    break;
                }
            }
            updateBotsList();
        }

        private void botInfo_selectedBot(object sender, SelectionChangedEventArgs e)
        {
            if (botInfo_list.SelectedItem != null)
            {
                string botName = botInfo_list.SelectedItem.ToString();
                Bot bot = main.getBotByName(botName);

                botInfo_fieldAdress.Content = bot.address;
                //TODO: add the other Information to the UI
            }
            else
            {
                clearBotInfoPrints();
            }
        }

        private void processClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Bot[] bots = main.getBots();
            foreach (Bot bot in bots)
            {
                bot.disconnect();
            }
        }

        private void printErr(string type, string content)
        {
            clearPrints();
            switch(type)
            {
                case "addBot":
                    addBot_outputErr.Content = content;
                break;

                case "manageBots":
                    manageBots_outputErr.Content = content;
                break;
            }
            clearFeedback.Start();
        }

        private void print(string type, string content)
        {
            clearPrints();
            switch (type)
            {
                case "addBot":
                    addBot_output.Content = content;
                break;

                case "manageBots":
                    manageBots_output.Content = content;
                break;
            }
            clearFeedback.Start();
        }

        private void clearPrints()
        {
            clearFeedback.Stop();

            addBot_outputErr.Content = "";
            addBot_output.Content = "";

            manageBots_outputErr.Content = "";
            manageBots_output.Content = "";
        }

        private void clearInputs(string type)
        {
            switch(type)
            {
                case "addBot":
                    addBot_inputBotName.Text = "";
                    addBot_inputBotAddress.Text = "";
                    addBot_inputBotKey.Password = "";
                break;

                case "manageBots":
                    manageBots_fieldBotName.Text = "";
                    manageBots_fieldBotAddress.Text = "";
                    manageBots_fieldBotKey.Password = "";
                break;
            }
        }

        private void updateBotsList()
        {
            manageBots_List.Items.Clear();
            botInfo_list.Items.Clear();
            foreach (string name in main.getBotNames())
            {
                manageBots_List.Items.Add(name);
                botInfo_list.Items.Add(name);
            }
        }

        private void log_updateLog(object sender, EventArgs e)
        {
            string[] log = MyLogger.getLog();
            if(log.Length != logEntrys)
            {
                for(int i = logEntrys; i < log.Length; i++)
                {
                    log_ListBox.Items.Add(log[i]);
                }
                logEntrys = log.Length;
            }
        }

        private void clearFeedbackPrints(object sender, EventArgs e)
        {
            clearFeedback.Stop();
            clearPrints();
        }

        private void clearBotInfoPrints()
        {
            botInfo_BotLog.Items.Clear();
            botInfo_fieldAdress.Content = "";
            botInfo_fieldCPUBack.Content = "";
            botInfo_fieldErrors.Content = "";
            botInfo_fieldPingBack.Content = "";
            botInfo_fieldRAMBack.Content = "";
            botInfo_fieldShards.Content = "";
            botInfo_fieldStatusGreen.Content = "";
            botInfo_fieldStatusRed.Content = "";
            botInfo_fieldStatusYellow.Content = "";
        }
    }
}
