using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Realm
{
    public class Save
    {
        public static string key = "???t\0sl";
        public static void SaveGame()
        {
            string tpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_save.rlm";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.rlm";
            Interface.type("Saving...", ConsoleColor.White);
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
            lines.Add("ppx=" + Map.PlayerPosition.x);
            lines.Add("ppy=" + Map.PlayerPosition.y);
            lines.Add("intlbuff=" + Main.intlbuff);
            lines.Add("atkbuff=" + Main.atkbuff);
            lines.Add("defbuff=" + Main.defbuff);
            lines.Add("spdbuff=" + Main.spdbuff);
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
            lines.Add("frozencounter=" + Main.frozencounter);
            lines.Add("noobcounter=" + Main.noobcounter);
            lines.Add("slimecounter=" + Main.slimecounter);
            lines.Add("banditcounter=" + Main.banditcounter);
            lines.Add("goblincounter=" + Main.goblincounter);
            lines.Add("drakecounter=" + Main.drakecounter);
            lines.Add("g=" + Main.Player.g);
            lines.Add("xp=" + Main.Player.xp);
            string[] linesarray = lines.ToArray<string>();
            File.WriteAllLines(tpath, linesarray);
            Interface.type("Done.", ConsoleColor.White);
            EncryptFile(tpath, path, key);
        }
        public static bool LoadGame()
        {
            string tpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_save.rlm";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.rlm";
            string achpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\achievements.rlm";
            string tachpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_achievements.rlm";
            try
            {
                if (!File.Exists(path))
                    return false;
                Interface.type("Loading Save...", ConsoleColor.White);
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
                            Exception devmode = new Exception("Devmode detected. Load save aborted.");
                            //try
                            //{
                                throw devmode;
                            //}
                            //catch(Exception)
                            //{
                            //    Interface.type(devmode.ToString(), ConsoleColor.White);
                            //    if (File.Exists(path))
                            //        File.Delete(path);
                            //    Interface.type("Press any key to continue.", ConsoleColor.White);
                            //    Interface.readkey();
                            //    Environment.Exit(0);
                            //}
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
                            if (!String.IsNullOrEmpty(entry.Value))
                                Main.Player.race = entry.Value;
                            else
                                Main.Player.race = "human";
                        }
                        if (entry.Key == "class")
                        {
                            if (!String.IsNullOrEmpty(entry.Value))
                                Main.Player.pclass = entry.Value;
                            else
                                Main.Player.pclass = "warrior";
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
                        if (entry.Key == "slimecounter")
                            Main.slimecounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "goblincounter")
                            Main.goblincounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "banditcounter")
                            Main.banditcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "drakecounter")
                            Main.drakecounter = Convert.ToInt32(entry.Value);
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
                            Main.Player.abilities.AddCommand(new Combat.Incinerate("Incinerate", 'a'));
                        if (entry.Key == "Heavensplitter")
                            Main.Player.abilities.AddCommand(new Combat.Heavensplitter("Heavensplitter", 'z'));
                        if (entry.Key == "Gamble")
                            Main.Player.abilities.AddCommand(new Combat.Gamble("Gamble", '$'));
                        if (entry.Key == "Nightshade")
                            Main.Player.abilities.AddCommand(new Combat.Nightshade("Nightshade", 'n'));
                        if (entry.Key == "Safeguard")
                            Main.Player.abilities.AddCommand(new Combat.Safeguard("Safeguard", 'g'));
                        if (entry.Key == "Mimic")
                            Main.Player.abilities.AddCommand(new Combat.Mimic("Mimic", 'm'));
                        if (entry.Key == "Heal")
                            Main.Player.abilities.AddCommand(new Combat.Heal("Heal", 'l'));
                        if (entry.Key == "Lightspeed")
                            Main.Player.abilities.AddCommand(new Combat.Lightspeed("Lightspeed", '!'));
                        if (entry.Key == "Rage")
                            Main.Player.abilities.AddCommand(new Combat.Rage("Rage", 'r'));
                        if (entry.Key == "Hell's Kitchen")
                            Main.Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
                        if (entry.Key == "Ice Chains")
                            Main.Player.abilities.AddCommand(new Combat.IceChains("Ice Chains", 'i'));
                        if (entry.Key == "Now You See Me")
                            Main.Player.abilities.AddCommand(new Combat.NowYouSeeMe("Now You See Me", 'y'));
                        if (entry.Key == "Illusion")
                            Main.Player.abilities.AddCommand(new Combat.Illusion("Illusion", '?'));
                        if (entry.Key == "Pew Pew Pew")
                            Main.Player.abilities.AddCommand(new Combat.PewPewPew("Pew Pew Pew", 'w'));
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
                        if (entry.Key == "frozencounter")
                            Main.frozencounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "noobcounter")
                            Main.noobcounter = Convert.ToInt32(entry.Value);
                        if (entry.Key == "is_thief")
                            Main.is_theif = true;
                        if (entry.Key == "intlbuff")
                            Main.intlbuff = Convert.ToInt32(entry.Value);
                        if (entry.Key == "atkbuff")
                            Main.atkbuff = Convert.ToInt32(entry.Value);
                        if (entry.Key == "defbuff")
                            Main.defbuff = Convert.ToInt32(entry.Value);
                        if (entry.Key == "spdbuff")
                            Main.spdbuff = Convert.ToInt32(entry.Value);
                    }
                    Interface.type("Done.", ConsoleColor.White);
                    file.Close();
                    File.Delete(tpath);
                    File.Delete(tachpath);
                    return true;
                }
            }
            catch (Exception e)
            {
                Interface.type("Load failed. Would you like to delete your save (press 'y' to delete)? ", ConsoleColor.White);
                char key = Interface.readkey().KeyChar;
                if (key == 'y')
                    File.Delete(path);
                else if (key == 'e')
                {
                    Interface.type(e.ToString());
                }
                File.Delete(tachpath);
                File.Delete(tpath);
                string crashpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\crashlog.txt";
                List<string> listlines = new List<string>();
                listlines.Add("---Load Save Runtime Error---");
                listlines.Add("Message: \r\n" + e.Message);
                try
                {
                    listlines.Add("Inner Exception: \r\n" + e.InnerException.ToString());
                }
                catch
                {
                }
                listlines.Add("-----------------------------");
                try
                {
                    if (!File.Exists(crashpath))
                    {
                        File.Create(crashpath).Dispose();
                    }
                    listlines.Add(e.InnerException.ToString());
                }
                catch(NullReferenceException)
                {
                }
                File.WriteAllLines(crashpath, listlines.ToArray());
                return false;
            }
        }
        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            using (FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    ICryptoTransform desencrypt = DES.CreateEncryptor();
                    using (CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write))
                    {
                        byte[] bytearrayinput = new byte[fsInput.Length];
                        fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                        cryptostream.Close();
                        fsInput.Close();
                        fsEncrypted.Close();
                        File.Delete(sInputFilename);
                    }
                }
            }
        }

        public static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                using (FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
                {
                    ICryptoTransform desdecrypt = DES.CreateDecryptor();
                    using (CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read))
                    {
                        if (!File.Exists(sOutputFilename))
                            File.Create(sOutputFilename).Close();
                        using (StreamWriter fsDecrypted = new StreamWriter(sOutputFilename))
                        {
                            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                            fsDecrypted.Flush();
                            fsDecrypted.Close();
                            fsread.Close();
                        }
                        //File.Delete(sInputFilename);
                    }
                }
            }
            catch (CryptographicException)
            {
            }
        }
    }
}
