using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class Save
    {
        public static string key = "???t\0sl";
        public static void SaveGame()
        {

            string tpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_save.rlm";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.rlm";
            Formatting.type("Saving...");
            List<string> lines = new List<string>();
            lines.Add("name=" + Main.Player.name);
            foreach (Combat.Command c in Main.Player.abilities.commands.Values)
                lines.Add(c.name + "=true");

            int bpcounter = 1;
            foreach (Item i in Main.Player.backpack)
            {
                lines.Add("bp" + bpcounter + "=" + i);
                bpcounter++;
            }
            lines.Add("race=" + Main.Player.race);
            lines.Add("class=" + Main.Player.pclass);
            if (!Main.Player.primary.Equals(new Item()))
                lines.Add("primary=" + Main.Player.primary);
            if (!Main.Player.secondary.Equals(new Item()))
                lines.Add("secondary=" + Main.Player.secondary);
            if (!Main.Player.armor.Equals(new Item()))
                lines.Add("armor=" + Main.Player.armor);
            if (!Main.Player.accessory.Equals(new Item()))
                lines.Add("accessory=" + Main.Player.accessory);
            lines.Add("level=" + Main.Player.level);
            lines.Add("hp=" + Main.Player.hp);
            lines.Add("ppx=" + Globals.PlayerPosition.x);
            lines.Add("ppy=" + Globals.PlayerPosition.y);
            if (Main.hasmap)
                lines.Add("hasmap=true");
            if (Main.wkingdead)
                lines.Add("wkingdead=true");
            if (Main.raven_dead)
                lines.Add("ravendead=true");
            if (Main.is_theif)
                lines.Add("is_thief=true");
            if (Main.devmode)
                lines.Add("devmode=true");
            lines.Add("wkingcounter=" + Main.wkingcounter);
            lines.Add("slibcounter=" + Main.slibcounter);
            lines.Add("forrestcounter=" + Main.forrestcounter);
            lines.Add("libcounter=" + Main.libcounter);
            lines.Add("centrallibcounter=" + Main.centrallibcounter);
            lines.Add("ramsaycounter=" + Main.ramsaycounter);
            lines.Add("magiccounter=" + Main.magiccounter);
            lines.Add("nlibcounter=" + Main.nlibcounter);
            lines.Add("townfolkcounter=" + Main.townfolkcounter);
            lines.Add("nomadcounter=" + Main.nomadcounter);
            lines.Add("minecounter=" + Main.minecounter);
            lines.Add("g=" + Main.Player.g);
            lines.Add("xp=" + Main.Player.xp);
            string[] linesarray = lines.ToArray<string>();
            File.WriteAllLines(tpath, linesarray);
            Formatting.type("Done.");
            EncryptFile(tpath, path, key);
        }
        public static bool LoadGame()
        {
            string tpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_save.rlm";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.rlm";
            try
            {
                if (!File.Exists(path))
                    return false;
                Formatting.type("Loading Save...");
                DecryptFile(path, tpath, key);
                Dictionary<string, string> vals = new Dictionary<string, string>();
                string line;
                using (StreamReader file = new StreamReader(tpath))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] split = line.Split(new char[] { '=' });
                        vals.Add(split[0], split[1]);
                    }
                    foreach (KeyValuePair<string, string> entry in vals)
                    {
                        if (entry.Key == "devmode")
                        {
                            throw new Exception("Devmode detected. Save file not loaded.");
                        }
                        if (entry.Key == "name")
                            Main.Player.name = entry.Value;
                        if (entry.Key == "level")
                            Main.Player.level = Convert.ToInt32(entry.Value);
                        //if (entry.Key == "ppx")
                        //    Globals.PlayerPosition.x = Convert.ToInt32(entry.Value);
                        //if (entry.Key == "ppy")
                        //    Globals.PlayerPosition.y = Convert.ToInt32(entry.Value);
                        if (entry.Key == "hasmap")
                            Main.hasmap = true;
                        if (entry.Key == "wkingdead")
                            Main.wkingdead = true;
                        if (entry.Key == "ravendead")
                            Main.raven_dead = true;
                        if (entry.Key == "hp")
                            Main.Player.hp = Convert.ToInt32(entry.Value);
                        if (entry.Key == "g")
                            Main.Player.g = Convert.ToInt32(entry.Value);
                        if (entry.Key == "xp")
                            Main.Player.xp = Convert.ToInt32(entry.Value);
                        if (entry.Key == "race")
                        {
                            if (entry.Value != "")
                                Main.Player.race = entry.Value;
                            else
                                Main.Player.race = "Human";
                        }
                        if (entry.Key == "class")
                        {
                            if (entry.Value != "")
                                Main.Player.pclass = entry.Value;
                            else
                                Main.Player.pclass = "Warrior";
                        }
                        if (entry.Key == "bp1")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp2")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp3")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp4")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp5")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp6")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp7")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp7")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp8")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp9")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp10")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "primary")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.primary = i;
                        }
                        if (entry.Key == "secondary")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.secondary = i;
                        }
                        if (entry.Key == "armor")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.armor = i;
                        }
                        if (entry.Key == "accessory")
                        {
                            Type atype = Type.GetType(entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.accessory = i;
                        }
                        if (entry.Key == "Energy Overload")
                            Main.Player.abilities.AddCommand(new Combat.EnergyOverload("Energy Overload", 'e'));
                        if (entry.Key == "Blade Dash")
                            Main.Player.abilities.AddCommand(new Combat.BladeDash("Blade Dash", 'd'));
                        if (entry.Key == "Holy Smite")
                            Main.Player.abilities.AddCommand(new Combat.HolySmite("Holy Smite", 'h'));
                        if (entry.Key == "Consume Soul")
                            Main.Player.abilities.AddCommand(new Combat.ConsumeSoul("Consume Soul", 'u'));
                        if (entry.Key == "Curse")
                            Main.Player.abilities.AddCommand(new Combat.Curse("Curse", 'c'));
                        if (entry.Key == "Sacrifice")
                            Main.Player.abilities.AddCommand(new Combat.Sacrifice("Sacrifice", 's'));
                        if (entry.Key == "Phase")
                            Main.Player.abilities.AddCommand(new Combat.Phase("Phase", 'p'));
                        if (entry.Key == "Dawnstrike")
                            Main.Player.abilities.AddCommand(new Combat.Dawnstrike("Dawnstrike", 't'));
                        if (entry.Key == "Vorpal Blades")
                            Main.Player.abilities.AddCommand(new Combat.VorpalBlades("Vorpal Blades", 'v'));
                        if (entry.Key == "Incinerate")
                            Main.Player.abilities.AddCommand(new Combat.Incinerate("Incinerate", 'i'));
                        if (entry.Key == "Heavensplitter")
                            Main.Player.abilities.AddCommand(new Combat.Heavensplitter("Heavensplitter", 'z'));
                        if (entry.Key == "Gamble")
                            Main.Player.abilities.AddCommand(new Combat.Gamble("Gamble", '$'));
                        if (entry.Key == "Force Pulse")
                            Main.Player.abilities.AddCommand(new Combat.ForcePulse("Force Pulse", 'p'));
                        if (entry.Key == "Nightshade")
                            Main.Player.abilities.AddCommand(new Combat.Nightshade("Nightshade", 'n'));
                        if (entry.Key == "Safeguard")
                            Main.Player.abilities.AddCommand(new Combat.Safeguard("Safeguard", 'g'));
                        if (entry.Key == "Mimic")
                            Main.Player.abilities.AddCommand(new Combat.Mimic("Mimic", 'm'));
                        if (entry.Key == "Lay Trap")
                            Main.Player.abilities.AddCommand(new Combat.LayTrap("Lay Trap", 'l'));
                        if (entry.Key == "Lightspeed")
                            Main.Player.abilities.AddCommand(new Combat.Lightspeed("Lightspeed", '!'));
                        if (entry.Key == "Rage")
                            Main.Player.abilities.AddCommand(new Combat.Rage("Rage", 'r'));
                        if (entry.Key == "Hell's Kitchen")
                            Main.Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
                        if (entry.Key == "loopnumber")
                            Main.loop_number = Convert.ToInt32(entry.Value);
                        if (entry.Key == "wkingcounter")
                            Main.wkingcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "slibcounter")
                            Main.slibcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "forrestcounter")
                            Main.forrestcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "libcounter")
                            Main.libcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "centrallibcounter")
                            Main.centrallibcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "ramsaycounter")
                            Main.ramsaycounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "magiccounter")
                            Main.magiccounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "nlibcounter")
                            Main.nlibcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "townfolkcounter")
                            Main.townfolkcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "nomadcounter")
                            Main.nomadcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "minecounter")
                            Main.minecounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "is_thief")
                            Main.is_theif = true;
                    }
                    Formatting.type("Done.");
                    file.Close();
                    File.Delete(tpath);
                    return true;
                }
            }
            catch (IOException e)
            {
                Formatting.type("Load failed.");
                Formatting.type(e.ToString(), 0);
                return false;
            }
        }

        static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.OpenOrCreate, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt, CryptoStreamMode.Write);
            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
            File.Delete(sInputFilename);
        }

        static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt, CryptoStreamMode.Read);
            if (!File.Exists(sOutputFilename))
                File.Create(sOutputFilename).Close();
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
            fsread.Close();
            //File.Delete(sInputFilename);
        }
    }
}
