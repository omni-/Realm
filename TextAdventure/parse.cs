using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class parse
    {
        public static void InputCleanse(string input, string key1, string key2)
        {
            while ((input.CompareTo(key1) != 0 || (input.CompareTo(key2) != 0)))
            {
                Console.WriteLine("Invalid.");
                input = Console.ReadLine();
            }
        }
    }
}
