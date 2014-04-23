using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Realm
{
    public class Init
    {
        public static bool init = false;
        public static bool failed = false;
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in CalculateMD5Hash(inputString))
                sb.Append(b.ToString("X"));

            return sb.ToString();
        }
        public static void Initialize()
        {
            FileIO.checkver();
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe");
            Console.Title = "Realm: " + Interface.GetTitle();
            Interface.type(Main.version, ConsoleColor.White);
            Map.gencave();
            Achievement.LoadAchievements();
            if (!Save.LoadGame())
            {
                Interface.type("\"Greetings. Before we begin, I must know your name.\"");
                Interface.type("Please enter your name. ");
                Main.Player.name = Interface.readinput(true);
                Main.ach.Get("name");
                if (Main.Player.name == Main.devstring)
                {
                    string hash = GetHashString(Interface.SecureStringToString(Interface.getPassword()));
                    if (hash == Main.password)
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
    public class FileIO
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
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool isConnected()
        {
            int desc;
            bool tf = InternetGetConnectedState(out desc, 0);
            return tf;
        }
        public static void checkver()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string programName = "update.exe";
            string resourceName = "Realm.update.exe";
            string exepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Realm.exe";
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            worker();
            while (!Init.init) { }

            if (isConnected())
            {
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
            else
                Interface.type("Connection error.");
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
                WebClient webClient = new WebClient();
                try
                {
                    FileIO fio = new FileIO();
                    fio.startDownload();
                }
                catch (WebException)
                {
                    Interface.type("Failed to connect to server.");
                    Init.failed = true;
                }
                // do some simple processing for 10 seconds
            });
            bw.RunWorkerAsync();
        }
        private void startDownload()
        {
            Console.Write("Loading...0%");
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync((new Uri("https://dl.dropboxusercontent.com/u/83385592/Realm.exe")), temppath);
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Console.Write("\rLoading...{0}%", (int)percentage);
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Interface.type("Done.");
            Init.init = true;
        }
    } 
}