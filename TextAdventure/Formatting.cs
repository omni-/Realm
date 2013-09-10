using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Formatting
    {
        public static void type(string src, int speed)
        {
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
        }

        public static void type(string src)
        {
            Console.WriteLine("\r\n");
            foreach (char c in src)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
        }
    }
}
