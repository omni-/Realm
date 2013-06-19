using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class parse
    {
        public static string InputCleanse(string[] keys) //fucntion to make sure the input is valid
        {
            bool valid = false;
            string str = "";
            while (!valid)
            {
                str = Console.ReadLine();
                foreach (string s in keys)
                {
                    if (str.CompareTo(s) == 0)
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                {
                    //Write invalid while input is not one of the keys
                    Console.WriteLine("Invalid.");
                }
            }
            return str;
        }
    }
}
