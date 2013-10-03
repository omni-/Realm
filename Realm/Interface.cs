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
            Interface.type("_________________________________________________________________________", 0);
            Interface.type("I--WESTERN--I-----------I--NORTHERN-I-----------I-----------I-----------I", 0);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-COALTOWN--I", 0);
            Interface.type("I--KINGDOM--I-----------I--KINGDOM--I-----------I-----------I-----------I", 0);
            Interface.type("I_______________________________________________________________________I", 0);
            Interface.type("I-----------I-----------I-----------I--NORTHERN-I-----------I-----------I", 0);
            Interface.type("I-----------I-RIVERWELL-I-----------I-----------I-----------I--NOMADS---I", 0);
            Interface.type("I-----------I-----------I-----------I-MOUNTAINS-I-----------I-----------I", 0);
            Interface.type("I_______________________________________________________________________I", 0);
            Interface.type("I-ILLUSION--I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("I-----------I-----------I-----------I-----------I--NEWPORT--I-----------I", 0);
            Interface.type("I--FOREST---I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("I_______________________________________________________________________I", 0);
            Interface.type("I-----------I-----------I-----------I--CENTRAL--I-----------I--EASTERN--I", 0);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("I-----------I-----------I-----------I--KINGDOM--I-----------I--KINGDOM--I", 0);
            Interface.type("I_______________________________________________________________________I", 0);
            Interface.type("I-----------I-----------I---MAGIC---I-----------I---TWIN----I-----------I", 0);
            Interface.type("I--SEAPORT--I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("I-----------I-----------I---CITY----I-----------I---PATHS---I-----------I", 0);
            Interface.type("I_______________________________________________________________________I", 0);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("I-----------I-VALLEYBURGI-----------I-----------I-----------I-RAVENKEEP-I", 0);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("_________________________________________________________________________", 0);
            Interface.type("I-----------I-----------I-SOUTHERN--I-----------I--FROZEN---I-----------I", 0);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 0);
            Interface.type("I-----------I-----------I--KINGDOM--I-----------I--FJORDS---I-----------I", 0);
            Interface.type("_________________________________________________________________________", 0);
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
