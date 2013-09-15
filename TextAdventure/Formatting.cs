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

        public static void drawmap()
        {
            Formatting.type("++++++++++++++++++++++++++++++++++Realm Map++++++++++++++++++++++++++++++++++", 1);
            Formatting.type("+--------()--(Northern Kingdon)--()----------------()---(Black Horizon)-----+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+-------------()--(Riverwell)--()--(Northern Mountains)--()--()-------------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+---------(Illusion Forest)--()--()------------------(Newport)--()----------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+(Western Kingdom)--(Valleyburg)--(Central Kindom)--(Nomads)--(East Kingdom)+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+----------(Seaport)--()--(Flaming Desert)--(Twin Paths)--(RavenKeep)-------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+----------------()--()--(Southern Kingdom)--()--(???)----------------------+", 1);
            Formatting.type("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++", 1);
        }
    }
}
