using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Realm
{
    public static class Interface
    {
        public static string GetTitle()
        {
            List<string> titlelist = new List<string>();
            titlelist.Add("Slimes OP");
            titlelist.Add("Now with Color!");
            titlelist.Add("Stochastic!");
            titlelist.Add("Sammy Classic Sonic Fan!");
            titlelist.Add("Realm: Realm:");
            titlelist.Add("Realm 2 Coming -Soon- Someday!");
            titlelist.Add("A Game of Slimes");
            titlelist.Add("Rustling Jimmies since 2013!");
            titlelist.Add("Revengeance");
            titlelist.Add("Redemption");
            titlelist.Add("A Slimes' Tale");
            int result = Main.rand.Next(0, titlelist.Count + 1);
            return titlelist[result];
        }
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
            if (rainbow)
            {
                Main.is_typing = true;
                List<ConsoleColor> colors = new List<ConsoleColor> { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkMagenta, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow };
                Console.WriteLine("\r\n");
                string[] words = src.Split();
                int i = 0;
                foreach (string word in words)
                {
                    if (word != words[0])
                        Console.Write(" ");
                    Console.ForegroundColor = colors[i];
                    foreach (char c in word)
                    {
                        Console.Write(c);
                        Thread.SpinWait(1000000);
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
                foreach (char c in src)
                {
                    Console.Write(c);
                    Thread.SpinWait(1000000);
                }
                Main.is_typing = false;
            }
        }
        public static void type(string src, int speed, bool rainbow)
        {
            if (rainbow)
            {
                Main.is_typing = true;
                List<ConsoleColor> colors = new List<ConsoleColor> { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkMagenta, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow };
                Console.WriteLine("\r\n");
                string[] words = src.Split();
                int i = 0;
                foreach (string word in words)
                {
                    if (word != words[0])
                        Console.Write(" ");
                    Console.ForegroundColor = colors[i];
                    foreach (char c in word)
                    {
                        Console.Write(c);
                        Thread.SpinWait(1000000 * speed);
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
                foreach (char c in src)
                {
                    Console.Write(c);
                    Thread.SpinWait(1000000 * speed);
                }
                Main.is_typing = false;
            }
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
            if (ExactSpelling)
            {
                while (Main.is_typing)
                {
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    ConsoleKeyInfo key = Console.ReadKey(true);
                }
                return Console.ReadLine();
            }
            else
            {
                while (Main.is_typing)
                {
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    ConsoleKeyInfo key = Console.ReadKey(true);
                }
                return Console.ReadLine().ToLower();
            }
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
        public static SecureString getPassword()
        {
            SecureString pwd = new SecureString();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    try
                    {
                        pwd.RemoveAt(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                    catch(ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                {
                    pwd.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            return pwd;
        }
        public static String SecureStringToString(SecureString value)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}
