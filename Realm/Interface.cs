using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Realm
{
    public static class Interface
    {
        public static void type(string src, int speed)
        {
            Main.is_typing = true;
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(speed * 1000000);
            }
            Main.is_typing = false;
        }

        public static void type(string src)
        {
            Main.is_typing = true;
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000);
            }
            Main.is_typing = false;
        }
        //public static void typeAbility(string abilityname)
        //{
        //    Main.is_typing = true;
        //    Console.WriteLine("\r\n");
        //    string leanred = "Learned '";
        //    string exclaim = "'!";
        //    foreach (char c in leanred)
        //    {
        //        Console.Write(c);
        //        Thread.SpinWait(1000000);
        //    }
        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    Console.Write(abilityname);
        //    Console.ResetColor();
        //    foreach (char c in exclaim)
        //    {
        //        Console.Write(c);
        //        Thread.SpinWait(1000000);
        //    }
        //    Main.is_typing = false;
        //}
        public static void typeStats()
        {
            Interface.type("--------------STATS-----------------", ConsoleColor.Yellow);
            Interface.type(Main.Player.name + "(" + Interface.ToUpperFirstLetter(Main.Player.race) + ")," + " Level " + Main.Player.level + " " + Interface.ToUpperFirstLetter(Main.Player.pclass) + ":", ConsoleColor.Yellow);
            Interface.type("HP: " + Main.Player.hp + "/" + Main.Player.maxhp, ConsoleColor.Yellow);
            Interface.type("Attack: " + Main.Player.atk + " / Defense: " + Main.Player.def + " / Speed: " + Main.Player.spd + " / Intelligence: " + Main.Player.intl, ConsoleColor.Yellow);
            Interface.type("Mana: " + (1 + (Main.Player.intl / 10)), ConsoleColor.Yellow);
            Interface.type("Gold: " + Main.Player.g + " / Exp to Level: " + (Main.Player.xp_next - Main.Player.xp), ConsoleColor.Yellow);
            Interface.type("-------------------------------------", ConsoleColor.Yellow);
        }
        public static void typeOnSameLine(string src)
        {
            Main.is_typing = true;
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000);
            }
            Main.is_typing = false;
        }
        public static void typeOnSameLine(string src, int speed)
        {
            Main.is_typing = true;
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000 * speed);
            }
            Main.is_typing = false;
        }
        public static void typeOnSameLine(string src, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }
        public static void typeOnSameLine(string src, int speed, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000 * speed);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }
        public static void type(string src, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }
        public static void type(string src, int speed, ConsoleColor color)
        {
            Main.is_typing = true;
            Console.ForegroundColor = color;
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000 * speed);
            }
            Main.is_typing = false;
            Console.ResetColor();
        }
        public static void type(string src, bool rainbow)
        {
            Main.is_typing = true;
            List<ConsoleColor> colors = new List<ConsoleColor>{ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGray, ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow};
            Console.WriteLine("\r\n");
            int i = 0;
            foreach (char c in src)
            {
                Console.ForegroundColor = colors[i];
                Console.Write(c);
                Thread.SpinWait(1000000);
                Console.ResetColor();
                i++;
                if (i > colors.Count - 1)
                    i = 0;
            }
            Main.is_typing = false;
            Console.ResetColor();
        }
        public static void type(string src, int speed, bool rainbow)
        {
            Main.is_typing = true;
            List<ConsoleColor> colors = new List<ConsoleColor> { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGray, ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow };
            Console.WriteLine("\r\n");
            int i = 0;
            foreach (char c in src)
            {
                Console.ForegroundColor = colors[i];
                Console.Write(c);
                Thread.SpinWait(1000000 * speed);
                Console.ResetColor();
                i++;
                if (i > colors.Count - 1)
                    i = 0;
            }
            Main.is_typing = false;
            Console.ResetColor();
        }
        public static ConsoleKeyInfo readkey()
        {
            while (Main.is_typing)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                ConsoleKeyInfo key = Console.ReadKey(true);
            }
            return Console.ReadKey();
        }
        public static string readinput()
        {
            while (Main.is_typing)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                ConsoleKeyInfo key = Console.ReadKey(true);
            }
            return Console.ReadLine().ToLower();
        }
        public static string readinput(bool ExactSpelling)
        {
            while (Main.is_typing)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                ConsoleKeyInfo key = Console.ReadKey(true);
            }
            return Console.ReadLine();
        }
        public static void drawmap()
        {
            Interface.type("_________________________________________________________________________", 0, ConsoleColor.Yellow);
            Interface.type("I--WESTERN--I-----------I--NORTHERN-I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-COALTOWN--I", 0, ConsoleColor.Yellow);
            Interface.type("I--KINGDOM--I-----------I--KINGDOM--I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I--NORTHERN-I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-RIVERWELL-I-----------I-----------I-----------I--NOMADS---I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-MOUNTAINS-I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            Interface.type("I-ILLUSION--I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-----------I--NEWPORT--I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I--FOREST---I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I--CENTRAL--I-----------I--EASTERN--I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I--KINGDOM--I-----------I--KINGDOM--I", 0, ConsoleColor.Yellow);
            Interface.type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I---MAGIC---I-----------I---TWIN----I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I--SEAPORT--I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I---CITY----I-----------I---PATHS---I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I_______________________________________________________________________I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-VALLEYBURGI-----------I-----------I-----------I-RAVENKEEP-I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("_________________________________________________________________________", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-SOUTHERN--I-----------I--FROZEN---I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("I-----------I-----------I--KINGDOM--I-----------I--FJORDS---I-----------I", 0, ConsoleColor.Yellow);
            Interface.type("_________________________________________________________________________", 0, ConsoleColor.Yellow);
        }
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}
