// ////////////////////////////////////////////////////////////////////////////////////////////////////
// Project - Realm - Created on 09/25/2013 by Cooper Teixeira                                        //
//                                                                                                   //
// Copyright (c) 2014 - All rights reserved                                                          //
//                                                                                                   //
// This software is provided 'as-is', without any express or implied warranty.                       //
// In no event will the authors be held liable for any damages arising from the use of this software.//
// ////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace update
{
    static class Program
    {
        private static bool init;

        public static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                if (File.Exists(path + "\realm.exe"))
                    File.Delete(path + "\\realm.exe");
                if (!File.Exists(path + "\\test.exe"))
                {
                    worker();
                    while (!init) { }
                }
                File.Copy(path + "\\test.exe", path + "\\realm.exe", true);
                Process.Start(path + "\\realm.exe");
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
        private static void startDownload()
        {
            Console.Write("Downloading...0%");
            var temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            var client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync((new Uri("https://dl.dropboxusercontent.com/u/83385592/realm.exe")), temppath);
        }

        private static void worker()
        {
            var bw = new BackgroundWorker { WorkerReportsProgress = true };

            bw.DoWork += delegate
            {
                try
                {
                    startDownload();
                }
                catch (WebException)
                {
                    Console.WriteLine("Failed to connect to server.");
                }
            };
            bw.RunWorkerAsync();
        }

        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;
            Console.Write("\rDownloading...{0}%", (int)percentage);
        }

        private static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Done.");
            init = true;
        }
    }
}