// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/17/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Realm
{
    public class Save
    {
        public const string key = "???t\0sl";

        public static void SaveGame()
        {
            Interface.type("Saving...", ConsoleColor.White);
            var lines = new List<string> {"name=" + Main.Player.name};
            lines.AddRange(Main.Player.abilities.commands.Values.Select(c => c.name + "=true"));

            var bpcounter = 1;
            foreach (var i in Main.Player.backpack)
            {
                lines.Add("bp" + bpcounter + "=" + i);
                bpcounter++;
            }
            lines.Add("race=" + Convert.ChangeType(Main.Player.race, Main.Player.race.GetTypeCode()));
            lines.Add("class=" + Convert.ChangeType(Main.Player.pclass, Main.Player.pclass.GetTypeCode()));
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
            lines.Add("xpos=" + Map.PlayerPosition.x);
            lines.Add("ypos=" + Map.PlayerPosition.y);
            var linesarray = lines.ToArray<string>();
            File.WriteAllLines(Main.tpath, linesarray);
            Interface.type("Done.", ConsoleColor.White);
            EncryptFile(Main.tpath, Main.path, key);
        }

        public static bool LoadGame()
        {
            try
            {
                if (!File.Exists(Main.path))
                    return false;
                Interface.type("Loading Save...", ConsoleColor.White);
                DecryptFile(Main.path, Main.tpath, key);
                var vals = new Dictionary<string, string>();
                string line;
                using (var file = new StreamReader(Main.tpath))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        var split = line.Split(new[] {'='});
                        vals.Add(split[0], split[1]);
                    }
                    foreach (var entry in vals)
                    {
                        if (entry.Key == "devmode")
                        {
                            var devmode = new Exception("Devmode detected. Load save aborted.");
                            throw devmode;
                        }
                        if (entry.Key == "name")
                            Main.Player.name = entry.Value;
                        if (entry.Key == "level")
                            Main.Player.level = Convert.ToInt32(entry.Value);
                        if (entry.Key == "xpos")
                            Map.PlayerPosition.x = Convert.ToInt32(entry.Value);
                        if (entry.Key == "ypos")
                            Map.PlayerPosition.y = Convert.ToInt32(entry.Value);
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
                                Main.Player.race = (pRace) int.Parse(entry.Value);
                            else
                                Main.Player.race = pRace.human;
                        }
                        if (entry.Key == "class")
                        {
                            if (!String.IsNullOrEmpty(entry.Value))
                                Main.Player.pclass = (pClass) int.Parse(entry.Value);
                            else
                                Main.Player.pclass = pClass.warrior;
                        }
                        if (entry.Key == "bp1")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp2")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp3")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp4")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp5")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp6")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp7")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp7")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp8")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp9")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp10")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "primary")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.primary = i;
                        }
                        if (entry.Key == "secondary")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.secondary = i;
                        }
                        if (entry.Key == "armor")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
                            Main.Player.armor = i;
                        }
                        if (entry.Key == "accessory")
                        {
                            var atype = Type.GetType(entry.Value);
                            var i = (Item) Activator.CreateInstance(atype);
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
                        if (entry.Key == "cl")
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
                    File.Delete(Main.tpath);
                    File.Delete(Main.tachpath);
                    return true;
                }
            }
            catch (Exception e)
            {
                Interface.type("Load failed. Would you like to delete your save? (press 'y' to delete)",
                    ConsoleColor.White);
                var ckey = Interface.readkey().KeyChar;
                if (ckey == 'y')
                    File.Delete(Main.path);
                else if (ckey == 'e')
                {
                    Interface.type(e.ToString());
                }
                File.Delete(Main.tachpath);
                File.Delete(Main.tpath);

                WriteError(e);

                return false;
            }
        }

        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            using (var fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
            {
                using (var fsEncrypted = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var DES = new DESCryptoServiceProvider
                    {
                        Key = Encoding.ASCII.GetBytes(sKey),
                        IV = Encoding.ASCII.GetBytes(sKey)
                    };
                    var desencrypt = DES.CreateEncryptor();
                    using (var cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write))
                    {
                        var bytearrayinput = new byte[fsInput.Length];
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
                var DES = new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(sKey),
                    IV = Encoding.ASCII.GetBytes(sKey)
                };

                using (var fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
                {
                    var desdecrypt = DES.CreateDecryptor();
                    using (var cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read))
                    {
                        if (!File.Exists(sOutputFilename))
                            File.Create(sOutputFilename).Close();
                        using (var fsDecrypted = new StreamWriter(sOutputFilename))
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

        public static void WriteError(Exception e)
        {
            var listlines = new List<string>
            {
                "---Runtime Error: Realm " + Main.version + "---",
                "Error: " + e.GetBaseException(),
                "Message:\r\n     " + e.Message + "\r\n"
            };
            try
            {
                listlines.Add("Inner Exception: \r\n" + e.InnerException + "\r\n");
            }
            catch (NullReferenceException)
            {
            }
            listlines.Add("Target Site:\r\n     ");
            listlines.Add(e.TargetSite + "\r\n");
            listlines.Add("Stack Trace:\r\n     ");
            listlines.Add(e.StackTrace);
            listlines.Add("-------------------------------------------------------");
            try
            {
                if (!File.Exists(Main.crashpath))
                    File.Create(Main.crashpath).Dispose();
                listlines.Add(e.InnerException.ToString());
            }
            catch (NullReferenceException)
            {
            }
            File.AppendAllLines(Main.crashpath, listlines.ToArray());
            MessageBox.Show(e.Message + e.InnerException, "Realm has encountered an error!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}