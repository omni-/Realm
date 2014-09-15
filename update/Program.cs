#region Header

// Project - Realm - Created on 09/25/2013 by Cooper Teixeira
// 
// Copyright (c) 2014 - All rights reserved
// 
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace update
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Realm.exe";
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.OpenRead("https://dl.dropboxusercontent.com/u/83385592/Realm.exe");
                    webClient.DownloadFile("https://dl.dropboxusercontent.com/u/83385592/Realm.exe", path);
                }
                Process.Start(path);
                Environment.Exit(0);
            }
            catch (WebException w)
            {
                Console.WriteLine("Failed to download file.");
                Console.WriteLine("Press e to view error.");
                if (Console.ReadKey().KeyChar == 'e')
                {
                    Console.WriteLine(w.ToString());
                    Console.ReadKey();
                }
                else
                    Environment.Exit(0);
            }
        }
    }
}