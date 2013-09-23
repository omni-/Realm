using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Realm
{
    public class Init
    {
        public static void Initialize()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string updaterpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location + "\\updater.exe");
            Formatting.type("Version Number - 1.3.2");
            Formatting.type("Press p to download latest version (anything else to cancel). ");
            if (Console.ReadKey().KeyChar == 'p')
            {
                Process.Start(updaterpath);
                Environment.Exit(0);
            }
            if (!Save.LoadGame())
            {
                Formatting.type("\"Greetings. Before we begin, I must know your name.\"");
                Formatting.type("Please enter your name. ");
                Main.Player.name = Console.ReadLine();
                if (Main.Player.name == "__dev__")
                {
                    Console.Clear();
                    Main.Player.level = 100;
                    Main.Player.hp = 1000;
                    Main.Player.maxhp = 1000;
                    Main.Player.g = 1000;
                    Main.Player.atk = 1000;
                    Main.Player.def = 1000;
                    Main.Player.spd = 1000;
                    Main.Player.intl = 1000;
                    Main.devmode = true;
                    Globals.PlayerPosition.x = 0;
                    Globals.PlayerPosition.y = 5;
                    Main.Player.race = Console.ReadLine();
                    Main.Player.pclass = Console.ReadLine();
                    Main.MainLoop();
                }
                Globals.PlayerPosition.x = 0;
                Globals.PlayerPosition.y = 5;
                Main.Tutorial();
            }
            else
            {
                Globals.PlayerPosition.x = 0;
                Globals.PlayerPosition.y = 5;
                Main.MainLoop();
            }
        }
    }
}
