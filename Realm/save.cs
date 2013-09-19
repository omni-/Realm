using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Realm
{
    public class Save
    {
        public static void SaveGame()
        {

            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.txt";
            Formatting.type("Saving...");
            List<string> lines = new List<string>();
            foreach (Combat.Command c in Main.Player.abilities.commands.Values)
                lines.Add(c.name + "=true");

            int bpcounter = 1;
            foreach (Item i in Main.Player.backpack)
            {
                lines.Add("bp" + bpcounter + "=" + i);
                bpcounter++;
            }
            if (!Main.Player.primary.Equals(new Item()))
                lines.Add("primary=" + Main.Player.primary);
            if (!Main.Player.secondary.Equals(new Item()))
                lines.Add("secondary=" + Main.Player.secondary);
            if (!Main.Player.armor.Equals(new Item()))
                lines.Add("armor=" + Main.Player.armor);
            if (!Main.Player.accessory.Equals(new Item()))
                lines.Add("accessory=" + Main.Player.accessory);
            lines.Add("ppx=" + Globals.PlayerPosition.x);
            lines.Add("ppy=" + Globals.PlayerPosition.y);
            lines.Add("level=" + Main.Player.level);
            lines.Add("hp=" + Main.Player.hp);
            if (Main.hasmap)
                lines.Add("hasmap=true");
            if (Main.wkingdead)
                lines.Add("wkingdead=true");
            if (Main.devmode)
                lines.Add("devmode=true");
            lines.Add("g=" + Main.Player.g);
            lines.Add("xp=" + Main.Player.hp);
            string[] linesarray = lines.ToArray<string>();
            File.WriteAllLines(path, linesarray);
            Formatting.type("Done.");
        }
        public static bool LoadGame()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                return false;
            }
            else if (new FileInfo("save.txt").Length == 0)
            {
                Formatting.type("Save file empty.");
                return false;
            }
            else
            {
                try
                {
                    Formatting.type("Loading Save...");
                    Dictionary<string, string> vals = new Dictionary<string, string>();
                    string line;
                    StreamReader file = new StreamReader(path);
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
                        if (entry.Key == "ppx")
                            Globals.PlayerPosition.x = Convert.ToInt32(entry.Value);
                        if (entry.Key == "ppx")
                            Globals.PlayerPosition.y = Convert.ToInt32(entry.Value);
                        if (entry.Key == "level")
                            Main.Player.level = Convert.ToInt32(entry.Value);
                        if (entry.Key == "hasmap")
                            Main.hasmap = true;
                        if (entry.Key == "wkingdead")
                            Main.wkingdead = true;
                        if (entry.Key == "hp")
                            Main.Player.hp = Convert.ToInt32(entry.Value);
                        if (entry.Key == "g")
                            Main.Player.g = Convert.ToInt32(entry.Value);
                        if (entry.Key == "xp")
                            Main.Player.xp = Convert.ToInt32(entry.Value);
                        if (entry.Key == "bp1")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp2")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp3")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp4")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp5")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp6")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp7")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp7")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp8")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp9")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
                            Item i = (Item)Activator.CreateInstance(atype);
                            Main.Player.backpack.Add(i);
                        }
                        if (entry.Key == "bp10")
                        {
                            Type atype = Type.GetType("Realm." + entry.Value);
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
                            Main.Player.abilities.AddCommand(new Combat.ConsumeSoul("Consume Soul", 'e'));
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
                        if (entry.Key == "gbooks")
                            Main.gbooks = Convert.ToInt32(entry.Value);
                        if (entry.Key == "isthief")
                            Main.is_theif = true;
                    }
                    Formatting.type("Done.");
                    file.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Formatting.type("Load failed.");
                    Formatting.type(e.ToString(), 0);
                    return false;
                }
            }
        }
    }
}
