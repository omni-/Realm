using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static bool done = false;
        public static bool failed = false;
        public static void Initialize()
        {
            FileIO.worker();
            while(!done)
            {
            }
            FileIO.checkver();
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe");
            Console.Title = "Realm: " + Interface.GetTitle();
            Interface.type(Main.version, ConsoleColor.White);
            Map.gencave();
            if (!Save.LoadGame())
            {
                Interface.type("\"Greetings. Before we begin, I must know your name.\"");
                Interface.type("Please enter your name. ");
                Main.Player.name = Interface.readinput(true);
                if (Main.Player.name == Main.devstring)
                {
                    string input = Interface.SecureStringToString(Interface.getPassword());
                    if (input == Main.password)
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
    public static class FileIO
    {
        public static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            File.SetAttributes(file1, FileAttributes.Normal);
            File.SetAttributes(file2, FileAttributes.Normal);
            fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }
        public static void checkver()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string programName = "update.exe";
            string resourceName = "Realm.update.exe";
            string exepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Realm.exe";
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile("https://dl.dropboxusercontent.com/u/83385592/Realm.exe", temppath);
            }
            catch(WebException)
            {
                Interface.type("Failed to connect to server.");
                Init.failed = true;
            }
            if (!Init.failed)
            {
                if (!FileCompare(exepath, temppath))
                {
                    Interface.type("New version available. Press 'p' to download. ", 0);
                    if (Interface.readkey().KeyChar == 'p')
                    {
                        var p = new Plop();
                        p.DropAndRun(resourceName, programName);
                        Environment.Exit(0);
                    }
                }
                File.Delete(temppath);
            }
        }
        public static void worker()
        {
            BackgroundWorker bw = new BackgroundWorker();

            // this allows our worker to report progress during work
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                Console.Write("Loading...");
                // do some simple processing for 10 seconds
                for (int i = 0; i <= 100; i++)
                {
                    Console.SetCursorPosition(11, 0);
                    Console.Write("{0}%", i);
                    Thread.Sleep(10);
                }
                Init.done = true;
            });
            bw.RunWorkerAsync();
        }
    } 
}
