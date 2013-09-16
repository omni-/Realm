using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Realm
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
            Formatting.type("+----(Western Kingdom)--(Northern Kingdon)--()----------------()---()-------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+-------------()--(Riverwell)--()--(Northern Mountains)--()--()-------------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+---------(Illusion Forest)--()--()------------------(Newport)--()----------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+------------()--(Valleyburg)--(Central Kindom)--()--(East Kingdom)---------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+-------------(Seaport)--()--(Nomads)--(Twin Paths)--(RavenKeep)------------+", 1);
            Formatting.type("+---------------------------------------------------------------------------+", 1);
            Formatting.type("+----------------()--()--(Southern Kingdom)--()--()-------------------------+", 1);
            Formatting.type("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++", 1);
        }
    }
}
