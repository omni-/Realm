using System;
using System.Collections.Generic;
using System.IO;

namespace Realm
{
    public class Achievement
    {
        public static Dictionary<string, bool> masterach = new Dictionary<string, bool>()
             {
               {"name", false},
               {"wking", false},
               {"1slime", false},
               {"100slimes", false},
               {"raven", false},
               {"finalboss", false},
               {"1goblin", false},
               {"100goblins", false},
               {"1drake", false},
               {"100drakes", false},
               {"1bandit", false},
               {"100bandits", false},
               {"itembuy", false},
               {"cardboard", false},
               {"dragon", false},
               {"set", false}
             };
        public void Get(string achievement)
        {
            try
            {
                string tachpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_achievements.rlm";
                string achpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\achievements.rlm";
                if (achievement == "name" && Main.achieve["name"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Howdy, stranger.", ConsoleColor.Green);
                else if (achievement == "wking" && Main.achieve["wking"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Brave, brave Sir Robin...", ConsoleColor.Green);
                else if (achievement == "1slime" && Main.achieve["1slime"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Tru hero");
                else if (achievement == "100slime" && Main.achieve["100slime"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Farm Simulator 2013", ConsoleColor.Green);
                else if (achievement == "raven" && Main.achieve["raven"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Caw Caw", ConsoleColor.Green);
                else if (achievement == "finalboss" && Main.achieve["finalboss"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: The end?");
                else if (achievement == "1goblin" && Main.achieve["1goblin"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: On your way...", ConsoleColor.Green);
                else if (achievement == "100goblins" && Main.achieve["100goblins"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Are you Rosie?", ConsoleColor.Green);
                else if (achievement == "1drake" && Main.achieve["1drake"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Basically dragonborn.", ConsoleColor.Green);
                else if (achievement == "100drakes" && Main.achieve["100drakes"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: You monster.", ConsoleColor.Green);
                else if (achievement == "1bandit" && Main.achieve["1bandit"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Basically Batman.", ConsoleColor.Green);
                else if (achievement == "100bandits" && Main.achieve["100bandits"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: The DK Crew", ConsoleColor.Green);
                else if (achievement == "itembuy" && Main.achieve["itembuy"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: One thing isn't a spree, mom.", ConsoleColor.Green);
                else if (achievement == "cardboard" && Main.achieve["cardboard"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: grats on the upgrade", ConsoleColor.Green);
                else if (achievement == "dragon" && Main.achieve["dragon"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Fus ro dah", ConsoleColor.Green);
                else if (achievement == "set" && Main.achieve["set"] == false && !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Wombo Combo!", ConsoleColor.Green);
                if (Main.achieve.ContainsKey(achievement))
                    Main.achieve[achievement] = true;
                if (!File.Exists(achpath))
                    File.Create(achpath).Close();
                using (StreamWriter file = new StreamWriter(tachpath))
                {
                    foreach (KeyValuePair<string, bool> pair in Main.achieve)
                    {
                        file.WriteLine(pair.Key + "=" + pair.Value);
                    }
                }
                Save.EncryptFile(tachpath, achpath, Save.key);
                File.Delete(tachpath);
            }
            catch (Exception e)
            {
                Interface.type("Achievement load failed.");
                File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_achievements.rlm");
                string crashpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\crashlog.txt";
                List<string> listlines = new List<string>();
                listlines.Add("---Load Achievement Runtime Error---");
                listlines.Add("Message: \r\n" + e.Message);
                try
                {
                    listlines.Add("Inner Exception: \r\n" + e.InnerException.ToString());
                }
                catch
                {
                }
                listlines.Add("------------------------------------");
                try
                {
                    if (!File.Exists(crashpath))
                    {
                        File.Create(crashpath).Dispose();
                    }
                    listlines.Add(e.InnerException.ToString());
                }
                catch (NullReferenceException)
                {
                }
                File.WriteAllLines(crashpath, listlines.ToArray());
            }
        }
        public static void LoadAchievements()
        {
            if (!File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\achievements.rlm"))
            {
                Main.achieve = masterach;
            }
            else
            {
                Save.DecryptFile(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\achievements.rlm", Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_achievements.rlm", Save.key);
                string aline;
                using (StreamReader afile = new StreamReader(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_achievements.rlm"))
                {
                    if (afile.BaseStream.Length == 0)
                    {
                        Main.achieve = masterach;
                        return;
                    }
                    Dictionary<string, string> tempdict = new Dictionary<string, string>();
                    while ((aline = afile.ReadLine()) != null)
                    {
                        string[] asplit = aline.Split(new char[] { '=' });
                        tempdict.Add(asplit[0], asplit[1]);
                    }
                    foreach (KeyValuePair<string, bool> entry in masterach)
                    {
                        if (!tempdict.ContainsKey(entry.Key))
                            tempdict.Add(entry.Key, "False");
                    }
                    foreach (KeyValuePair<string, string> entry in tempdict)
                    {
                        if (entry.Value == "True")
                            Main.achieve.Add(entry.Key, true);
                        else if (entry.Value == "False")
                            Main.achieve.Add(entry.Key, false);
                    }
                }
            }
        }
    }
}
    