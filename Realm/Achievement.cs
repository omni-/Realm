using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                if (achievement == "name" && Main.achieve["name"] == false)
                    MessageBox.Show("Howdy, stranger.", "Achievement Unlocked!");
                else if (achievement == "wking" && Main.achieve["wking"] == false)
                    MessageBox.Show("Brave, brave Sir Robin...", "Achievement Unlocked!");
                else if (achievement == "1slime" && Main.achieve["1slime"] == false)
                    MessageBox.Show("Tru hero", "Achievement Unlocked!");
                else if (achievement == "100slime" && Main.achieve["100slime"] == false)
                    MessageBox.Show("Professional Farmer", "Achievement Unlocked!");
                else if (achievement == "raven" && Main.achieve["raven"] == false)
                    MessageBox.Show("Caw Caw", "Achievement Unlocked!");
                else if (achievement == "finalboss" && Main.achieve["finalboss"] == false)
                    MessageBox.Show("The end?", "Achievement Unlocked!");
                else if (achievement == "1goblin" && Main.achieve["1goblin"] == false)
                    MessageBox.Show("On your way...", "Achievement Unlocked!");
                else if (achievement == "100goblins" && Main.achieve["100goblins"] == false)
                    MessageBox.Show("Are you Rosie?", "Achievement Unlocked!");
                else if (achievement == "1drake" && Main.achieve["1drake"] == false)
                    MessageBox.Show("Basically dragonborn.", "Achievement Unlocked!");
                else if (achievement == "100drakes" && Main.achieve["100drakes"] == false)
                    MessageBox.Show("You monster.", "Achievement Unlocked!");
                else if (achievement == "1bandit" && Main.achieve["1bandit"] == false)
                    MessageBox.Show("Basically Batman.", "Achievement Unlocked!");
                else if (achievement == "100bandits" && Main.achieve["100bandits"] == false)
                    MessageBox.Show("The DK Crew", "Achievement Unlocked!");
                else if (achievement == "itembuy" && Main.achieve["itembuy"] == false)
                    MessageBox.Show("One thing isn't a spree, mom.", "Achievement Unlocked!");
                else if (achievement == "cardboard" && Main.achieve["cardboard"] == false)
                    MessageBox.Show("grats on the upgrade", "Achievement Unlocked!");
                else if (achievement == "dragon" && Main.achieve["dragon"] == false)
                    MessageBox.Show("Fus ro dah", "Achievement Unlocked!");
                else if (achievement == "set" && Main.achieve["set"] == false)
                    MessageBox.Show("Wombo Combo!", "Achievement Unlocked!");
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
    