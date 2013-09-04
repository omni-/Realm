using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Init
    {
        public static void Initialize()
        {
            Console.Clear();
            Console.WriteLine("\n You are the Hero of the Western Kingdom. The Western King has called for your \n presence.");
            Globals.PlayerPosition.x = 0;
            Globals.PlayerPosition.y = 2;
            Main.MainLoop();
        }
    }
}
