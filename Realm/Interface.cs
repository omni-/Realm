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
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(speed * 1000000);
            }
        }

        public static void type(string src)
        {
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.SpinWait(1000000);
            }
        }
        public static ConsoleKeyInfo readkey()
        {
            return Console.ReadKey();
        }
        public static string readinput()
        {
            return Console.ReadLine();
        }
        public static void drawmap()
        {
            Interface.type("_________________________________________________________________________", 1);
            Interface.type("I--WESTERN--I-----------I--NORTHERN-I-----------I-----------I-----------I", 1);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-COALTOWN--I", 1);
            Interface.type("I--KINGDOM--I-----------I--KINGDOM--I-----------I-----------I-----------I", 1);
            Interface.type("I_______________________________________________________________________I", 1);
            Interface.type("I-----------I-----------I-----------I--NORTHERN-I-----------I-----------I", 1);
            Interface.type("I-----------I-RIVERWELL-I-----------I-----------I-----------I--NOMADS---I", 1);
            Interface.type("I-----------I-----------I-----------I-MOUNTAINS-I-----------I-----------I", 1);
            Interface.type("I_______________________________________________________________________I", 1);
            Interface.type("I-ILLUSION--I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("I-----------I-----------I-----------I-----------I--NEWPORT--I-----------I", 1);
            Interface.type("I--FOREST---I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("I_______________________________________________________________________I", 1);
            Interface.type("I-----------I-----------I-----------I--CENTRAL--I-----------I--EASTERN--I", 1);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("I-----------I-----------I-----------I--KINGDOM--I-----------I--KINGDOM--I", 1);
            Interface.type("I_______________________________________________________________________I", 1);
            Interface.type("I-----------I-----------I---MAGIC---I-----------I---TWIN----I-----------I", 1);
            Interface.type("I--SEAPORT--I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("I-----------I-----------I---CITY----I-----------I---PATHS---I-----------I", 1);
            Interface.type("I_______________________________________________________________________I", 1);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("I-----------I-VALLEYBURGI-----------I-----------I-----------I-RAVENKEEP-I", 1);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("_________________________________________________________________________", 1);
            Interface.type("I-----------I-----------I-SOUTHERN--I-----------I--FROZEN---I-----------I", 1);
            Interface.type("I-----------I-----------I-----------I-----------I-----------I-----------I", 1);
            Interface.type("I-----------I-----------I--KINGDOM--I-----------I--FJORDS---I-----------I", 1);
            Interface.type("_________________________________________________________________________", 1);
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
