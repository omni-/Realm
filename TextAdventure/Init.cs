using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Init
    {
        public static void Initialize()
        {
            Console.Clear();
            Formatting.type("\"Greetings. Before we begin, I must know your name.\"");
            Formatting.type("Please enter your name.\r\n");
            Main.Player.name = Console.ReadLine();

            //Formatting.type("You are the Hero of the Western Kingdom. The Western King has called for your\r\npresence.");
            Globals.PlayerPosition.x = 0;
            Globals.PlayerPosition.y = 2;
            Main.Tutorial();
        }
    }
}
