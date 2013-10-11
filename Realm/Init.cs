using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Realm
{
    public class Init
    {
        public static void Initialize()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string programName = "update.exe";
            string resourceName = "Realm.update.exe";
            Console.Title = "Realm - (C) 2013 Omnichroma";
            Interface.type("Version Number - 1.7.2", ConsoleColor.White);
            Interface.type("Press p to download latest version. If this is your first time running the game, press p. ", 0);
            if (Interface.readkey().KeyChar == 'p')
            {
                var p = new Plop();
                p.DropAndRun(resourceName, programName);
                Environment.Exit(0);
            }
            Map.gencave();
            if (!Save.LoadGame())
            {
                Interface.type("\"Greetings. Before we begin, I must know your name.\"");
                Interface.type("Please enter your name. ");
                Main.Player.name = Interface.readinput(true);
                if (Main.Player.name == Main.devstring)
                {
                    //SecureString pass = new SecureString();
                    string input = Interface.SecureStringToString(Interface.getPassword());
                    if (input == "__dev__")
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
                        Map.PlayerPosition.x = 0;
                        Map.PlayerPosition.y = 6;
                        Main.Player.race = Interface.readinput();
                        Main.Player.pclass = Interface.readinput();
                        Main.MainLoop();
                    }
                    else
                        Interface.type("Invalid password.", ConsoleColor.White);
                }
                Map.PlayerPosition.x = 0;
                Map.PlayerPosition.y = 6;
                Main.Tutorial();
            }
            else
            {
                Map.PlayerPosition.x = 0;
                Map.PlayerPosition.y = 6;
                Main.MainLoop();
            }
        }
    }
    class Plop
    {
        public void DropAndRun(string rName, string fName)
        {
            var assembly = this.GetType().Assembly;
            using (var stream = assembly.GetManifestResourceStream(rName))
            {
                using (FileStream file = new FileStream(fName, FileMode.Create))
                {
                    stream.CopyTo(file);
                }
                Process.Start(fName);
            }
        }
    }
}
