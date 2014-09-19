// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/17/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;

namespace Realm
{
    public static class Init
    {
        public static bool init;
        public static bool failed;

        public static void Initialize()
        {
            //if (!Main.devmode)
                FileIO.checkver();
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe");
            Console.Title = "Realm: " + Interface.GetTitle() + " (" + Main.version + ")";
            Interface.type("You are running Realm " + Main.version, ConsoleColor.White);
            Main.mainplayer = new SoundPlayer(Properties.Resources.main);
            Map.gencave();

            Achievement.LoadAchievements();
            if (!Save.LoadGame())
            {
                Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight);
                NativeMethods.ShowWindow(NativeMethods.ThisConsole, NativeMethods.MAXIMIZE);
                Interface.type("\"Greetings. Before we begin, I must know your name.\"");
                Interface.type("Please enter your name. ");
                Main.Player.name = Interface.readinput(true);
                if (Main.devmode)
                {
                    Console.Clear();
                    int reason = 0;
                    if (!Save.LoadSettings(out reason))
                    {
                        switch (reason)
                        {
                            case 1:
                                Interface.type("Settings file not found.");
                                break;
                            case 2:
                                Interface.type("Invalid settings file.");
                                break;
                        }
                    }
                    Main.Player.race = (pRace)int.Parse(Interface.readinput());
                    Main.Player.pclass = (pClass)int.Parse(Interface.readinput());
                }
                Main.ach.Get("name");
                Main.Tutorial();
            }
            else
            {
                if (Main.fullscreen)
                {
                    Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight);
                    NativeMethods.ShowWindow(NativeMethods.ThisConsole, NativeMethods.MAXIMIZE);
                }
                Main.MainLoop();
            }
        }
    }

    internal class Plop
    {
        public void DropAndRun(string rName, string fName)
        {
            var assembly = GetType().Assembly;
            using (var stream = assembly.GetManifestResourceStream(rName))
            {
                using (var file = new FileStream(fName, FileMode.Create))
                {
                    stream.CopyTo(file);
                }
                Process.Start(fName);
            }
        }
    }
}