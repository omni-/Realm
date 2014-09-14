// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/12/2014 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;

namespace Realm
{
    public class Achievement
    {
        private static readonly Dictionary<string, bool> masterach = new Dictionary<string, bool>
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
                else if (achievement == "100goblins" && Main.achieve["100goblins"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Are you Rosie?", ConsoleColor.Green);
                else if (achievement == "1drake" && Main.achieve["1drake"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Basically dragonborn.", ConsoleColor.Green);
                else if (achievement == "100drakes" && Main.achieve["100drakes"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: You monster.", ConsoleColor.Green);
                else if (achievement == "1bandit" && Main.achieve["1bandit"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Basically Batman.",
                        ConsoleColor.Green);
                else if (achievement == "100bandits" && Main.achieve["100bandits"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: The DK Crew", ConsoleColor.Green);
                else if (achievement == "itembuy" && Main.achieve["itembuy"] == false &&
                         !Main.achievements_disabled)
                    Interface.type(
                        "Achievement Unlocked!: One thing isn't a spree, mom.",
                        ConsoleColor.Green);
                else if (achievement == "cardboard" && Main.achieve["cardboard"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: grats on the upgrade",
                        ConsoleColor.Green);
                else if (achievement == "dragon" && Main.achieve["dragon"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Fus ro dah",
                        ConsoleColor.Green);
                else if (achievement == "set" && Main.achieve["set"] == false &&
                         !Main.achievements_disabled)
                    Interface.type("Achievement Unlocked!: Wombo Combo!",
                        ConsoleColor.Green);
                if (Main.achieve.ContainsKey(achievement))
                    Main.achieve[achievement] = true;
                if (!File.Exists(Main.achpath))
                    File.Create(Main.achpath).Close();
                using (var file = new StreamWriter(Main.tachpath))
                {
                    foreach (var pair in Main.achieve)
                    {
                        file.WriteLine(pair.Key + "=" + pair.Value);
                    }
                }
                Save.EncryptFile(Main.tachpath, Main.achpath, Save.key);
                File.Delete(Main.tachpath);
            }
            catch (Exception e)
            {
                Interface.type("Achievement load failed.");
                File.Delete(Path.GetDirectoryName(Main.tachpath));
                var listlines = new List<string> {"---Load Achievement Runtime Error---", "Message: \r\n" + e.Message};
                try
                {
                    listlines.Add("Inner Exception: \r\n" + e.InnerException);
                }
                catch
                {
                }
                listlines.Add("------------------------------------");
                try
                {
                    if (!File.Exists(Main.crashpath))
                    {
                        File.Create(Main.crashpath).Dispose();
                    }
                    listlines.Add(e.InnerException.ToString());
                }
                catch (NullReferenceException)
                {
                }
                File.WriteAllLines(Main.crashpath, listlines.ToArray());
            }
        }

        public static void LoadAchievements()
        {
            if (!File.Exists(Main.achpath))
            {
                Main.achieve = masterach;
            }
            else
            {
                Save.DecryptFile(
                    Main.achpath,
                    Main.tachpath,
                    Save.key);
                string aline;
                using (
                    var afile =
                        new StreamReader(Main.tachpath))
                {
                    if (afile.BaseStream.Length == 0)
                    {
                        Main.achieve = masterach;
                        return;
                    }
                    var tempdict = new Dictionary<string, string>();
                    while ((aline = afile.ReadLine()) != null)
                    {
                        var asplit = aline.Split(new[] {'='});
                        tempdict.Add(asplit[0], asplit[1]);
                    }
                    foreach (var entry in masterach)
                    {
                        if (!tempdict.ContainsKey(entry.Key))
                            tempdict.Add(entry.Key, "False");
                    }
                    foreach (var entry in tempdict)
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