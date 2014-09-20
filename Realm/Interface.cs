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
using System.Threading;

namespace Realm
{
    public static class Interface
    {
        public static string GetTitle()
        {
            var titlelist = new List<string>
            {
                "Slimes OP",
                "Now with Color!",
                "Stochastic!",
                "Sammy Classic Sonic Fan!",
                "Expand",
                "Drugs are bad, kids",
                "Avoid Realm 2!",
                "A Game of Slimes",
                "I, for one, welcome our new Slime overlord.",
                "Revengeance",
                "Redemption",
                "A Slimes' Tale",
                "Masaaki Endoh Version",
                "Big in Japan",
                "Is this thing on?",
                "Better than the sequel!",
                "Forever in our Hearts",
                "Platinum Edition",
                "Try the Bear DLC for only $49.99!",
                "Inspired by 'Go Go Nippon'",
                "u weeb",
                "wedsmok stonr",
                "slimecounter++;",
                "no more luigi",
                "mayro"
            };
            var result = Main.rand.Next(0, titlelist.Count);
            return titlelist[result];
        }

        public static void typeNoDelay(string src, ConsoleColor color)
        {
            Console.ForegroundColor = Main.DefaultColor;
            Console.WriteLine(src, color);
            Console.ResetColor();
        }

        public static void type(string src, int speed)
        {
            Main.is_typing = true;
            Console.ForegroundColor = Main.DefaultColor;
            Console.WriteLine("\r\n");
            foreach (var c in src)
            {
                Console.Write(c);
                Thread.SpinWait(speed*1000000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }

        public static void type(string src)
        {
            Main.is_typing = true;
            Console.ForegroundColor = Main.DefaultColor;
            Console.WriteLine("\r\n");
            foreach (var c in src)
            {
                Console.Write(c);
                Thread.SpinWait((int)Main.speed * 100000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }

        public static void typeStats()
        {
            type("--------------STATS-----------------", ConsoleColor.Yellow);
            type(
                Main.Player.name + "(" + ToUpperFirstLetter(Main.Player.race.ToString()) + ")," + " Level " +
                Main.Player.level +
                " " + ToUpperFirstLetter(Main.Player.pclass.ToString()) + ":", ConsoleColor.Yellow);
            type("HP: " + Main.Player.hp + "/" + Main.Player.maxhp, ConsoleColor.Yellow);
            type(
                "Attack: " + Main.Player.atk + " / Defense: " + Main.Player.def + " / Speed: " + Main.Player.spd +
                " / Intelligence: " + Main.Player.intl, ConsoleColor.Yellow);
            type("Mana: " + Main.Player.mana + "/" + Main.Player.maxmana, ConsoleColor.Yellow);
            type("Gold: " + Main.Player.g + " / Exp to Level: " + (Main.Player.xp_next - Main.Player.xp),
                ConsoleColor.Yellow);
            type("=============Achievements============", ConsoleColor.Cyan);
            foreach (var entry in Main.achieve)
            {
                if (entry.Key == "name" && Main.achieve[entry.Key])
                    type("Howdy, stranger. - Name Yourself", ConsoleColor.Cyan);
                else if (entry.Key == "wking" && Main.achieve[entry.Key])
                    type("Brave, brave Sir Robin... - Run away from the Western King", ConsoleColor.Cyan);
                else if (entry.Key == "1slime" && Main.achieve[entry.Key])
                    type("Tru hero - Kill a slime", ConsoleColor.Cyan);
                else if (entry.Key == "100slime" && Main.achieve[entry.Key])
                    type("Professional Farmer - Kill 100 slimes", ConsoleColor.Cyan);
                else if (entry.Key == "raven" && Main.achieve[entry.Key])
                    type("Caw Caw - Defeat the Raven King", ConsoleColor.Cyan);
                else if (entry.Key == "finalboss" && Main.achieve[entry.Key])
                    type("The end? - Defeat Janus", ConsoleColor.Cyan);
                else if (entry.Key == "1goblin" && Main.achieve[entry.Key])
                    type("On your way... - Kill a goblin", ConsoleColor.Cyan);
                else if (entry.Key == "100goblins" && Main.achieve[entry.Key])
                    type("Are you Rosie? - Kill 100 goblins", ConsoleColor.Cyan);
                else if (entry.Key == "1drake" && Main.achieve[entry.Key])
                    type("Basically dragonborn. - Kill a drake", ConsoleColor.Cyan);
                else if (entry.Key == "100drakes" && Main.achieve[entry.Key])
                    type("You monster. - Kill 100 drakes", ConsoleColor.Cyan);
                else if (entry.Key == "1bandit" && Main.achieve[entry.Key])
                    type("Basically Batman. - Kill a bandit", ConsoleColor.Cyan);
                else if (entry.Key == "100bandits" && Main.achieve[entry.Key])
                    type("The DK Crew - Kill 100 bandits", ConsoleColor.Cyan);
                else if (entry.Key == "itembuy" && Main.achieve[entry.Key])
                    type("One thing isn't a spree, mom. - Buy an item", ConsoleColor.Cyan);
                else if (entry.Key == "cardboard" && Main.achieve[entry.Key])
                    type("grats on the upgrade - Find the cardboard armor", ConsoleColor.Cyan);
                else if (entry.Key == "dragon" && Main.achieve[entry.Key])
                    type("Fus ro dah - Slay Tyrone", ConsoleColor.Cyan);
                else if (entry.Key == "set" && Main.achieve[entry.Key])
                    type("Wombo Combo! - Obtain all the pieces", ConsoleColor.Cyan);
            }
            type("-------------------------------------", ConsoleColor.Yellow);
        }

        public static void typeOnSameLine(string src)
        {
            Main.is_typing = true;
            Console.ForegroundColor = Main.DefaultColor;
            foreach (var c in src)
            {
                Console.Write(c);
                Thread.SpinWait((int)Main.speed * 100000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }

        public static void typeOnSameLine(string src, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            foreach (var c in src)
            {
                Console.Write(c);
                Thread.SpinWait((int)Main.speed * 100000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }

        public static void type(string src, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            Console.WriteLine("\r\n");
            foreach (var c in src)
            {
                Console.Write(c);
                Thread.SpinWait((int)Main.speed * 100000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }

        public static void type(string src, int speed, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            Console.WriteLine("\r\n");
            foreach (var c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000*speed);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }

        public static void type(string src, bool rainbow)
        {
            if (rainbow)
            {
                Main.is_typing = true;
                var colors = new List<ConsoleColor>
                {
                    ConsoleColor.Blue,
                    ConsoleColor.Cyan,
                    ConsoleColor.DarkMagenta,
                    ConsoleColor.Green,
                    ConsoleColor.Magenta,
                    ConsoleColor.Red,
                    ConsoleColor.White,
                    ConsoleColor.Yellow
                };
                Console.WriteLine("\r\n");
                var words = src.Split();
                var i = 0;
                foreach (var word in words)
                {
                    if (word != words[0])
                        Console.Write(" ");
                    Console.ForegroundColor = colors[i];
                    foreach (var c in word)
                    {
                        Console.Write(c);
                        Thread.SpinWait((int)Main.speed * 100000);
                    }
                    i++;
                    if (i > colors.Count - 1)
                        i = 0;
                }
                Main.is_typing = false;
                Console.ResetColor();
            }
            else
            {
                Main.is_typing = true;
                Console.WriteLine("\r\n");
                foreach (var c in src)
                {
                    Console.Write(c);
                    Thread.SpinWait(1000000);
                }
                Main.is_typing = false;
            }
        }

        public static ConsoleKeyInfo readkey()
        {
            while (Main.is_typing)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                var key = Console.ReadKey(true);
            }
            return Console.ReadKey();
        }

        public static string readinput()
        {
            while (Main.is_typing)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                var key = Console.ReadKey(true);
            }
            return Console.ReadLine().ToLower();
        }

        public static string readinput(bool ExactSpelling)
        {
            if (ExactSpelling)
            {
                while (Main.is_typing)
                {
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    var key = Console.ReadKey(true);
                }
                return Console.ReadLine();
            }
            while (Main.is_typing)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                var key = Console.ReadKey(true);
            }
            return Console.ReadLine().ToLower();
        }

        public static void drawmap()
        {
            type("_________________________________________________________________________", 0, ConsoleColor.Yellow);
            type("I--WESTERN--I-----------I--NORTHERN-I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I-----------I-----------I-COALTOWN--I", 0, ConsoleColor.Yellow);
            type("I--KINGDOM--I-----------I--KINGDOM--I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I--NORTHERN-I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-RIVERWELL-I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I-MOUNTAINS-I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            type("I-ILLUSION--I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I-----------I--NEWPORT--I-----------I", 0, ConsoleColor.Yellow);
            type("I--FOREST---I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I--CENTRAL--I-----------I--EASTERN--I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I--KINGDOM--I-----------I--KINGDOM--I", 0, ConsoleColor.Yellow);
            type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I---MAGIC---I-----------I---TWIN----I-----------I", 0, ConsoleColor.Yellow);
            type("I--SEAPORT--I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I---CITY----I-----------I---PATHS---I-----------I", 0, ConsoleColor.Yellow);
            type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I--VALLEY---I-----------I-----------I-----------I-RAVENKEEP-I", 0, ConsoleColor.Yellow);
            type("I-----------I---BURG----I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("_________________________________________________________________________", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-SOUTHERN--I-----------I--FROZEN---I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            type("I-----------I-----------I--KINGDOM--I-----------I--FJORDS---I-----------I", 0, ConsoleColor.Yellow);
            type("_________________________________________________________________________", 0, ConsoleColor.Yellow);
        }

        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            var letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
    public enum TextSpeed
    {
        Slow = 10,
        Medium = 5,
        Fast = 1,
        Zero = 0
    }
}