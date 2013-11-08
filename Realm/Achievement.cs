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
        public void Get(string achievement)
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
            if (Main.achieve.ContainsKey(achievement))
                Main.achieve[achievement] = true;
            if (!File.Exists(achpath))
                File.Create(achpath);
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
    }
}
    