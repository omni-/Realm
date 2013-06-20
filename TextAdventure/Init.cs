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
            Console.WriteLine("\r\n You are the Hero of the Western Kingdom. The Western King has called for your   presence.");
            Globals.PlayerPosition.x = 2;
            Globals.PlayerPosition.y = 0;
            Main.MainLoop();
        }
    }
}
