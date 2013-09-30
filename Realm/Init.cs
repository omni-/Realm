﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
            Interface.type("Version Number - 1.6.2", 0);
            Interface.type("Press p to download latest version. If this is your first time running the game, press p. ", 0);
            if (Interface.readkey().KeyChar == 'p')
            {
                var p = new Plop();
                p.DropAndRun(resourceName, programName);
                Environment.Exit(0);
            }
            Globals.gencave();
            if (!Save.LoadGame())
            {
                Interface.type("\"Greetings. Before we begin, I must know your name.\"");
                Interface.type("Please enter your name. ");
                Main.Player.name = Interface.readinput(true);
                if (Main.Player.name == Main.devstring)
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
                    Globals.PlayerPosition.y = 6;
                    Main.Player.race = Interface.readinput();
                    Main.Player.pclass = Interface.readinput();
                    Main.MainLoop();
                }
                Globals.PlayerPosition.x = 0;
                Globals.PlayerPosition.y = 6;
                Main.Tutorial();
            }
            else
            {
                Globals.PlayerPosition.x = 0;
                Globals.PlayerPosition.y = 6;
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
