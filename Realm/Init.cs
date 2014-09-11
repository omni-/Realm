using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Realm
{
    public class Init
    {
        public static bool init = false;
        public static bool failed = false;
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
                sb.Append(t.ToString("X2"));
            return sb.ToString();
        }

        public static void Initialize()
        {
            FileIO.checkver();
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe");
            Console.Title = "Realm: " + Interface.GetTitle() + " (" + Main.version + ")";
            Interface.type("You are running Realm " + Main.version, ConsoleColor.White);
            Map.gencave();
            Achievement.LoadAchievements();
            if (!Save.LoadGame())
            {
                Interface.type("\"Greetings. Before we begin, I must know your name.\"");
                Interface.type("Please enter your name. ");
                Main.Player.name = Interface.readinput(true);
                if (Main.devmode)
                {
                    Console.Clear();
                    Main.Player.race = Interface.readinput();
                    Main.Player.pclass = Interface.readinput();
                }
                Main.ach.Get("name");
                Main.Tutorial();
            }
            else
                Main.MainLoop();
        }
    }
    class Plop
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
        public static bool isConnected()
        {
            int desc;
            var tf = NativeMethods.InternetGetConnectedState(out desc, 0);
            return tf;
        }
        public static void checkver()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string programName = "update.exe";
            const string resourceName = "Realm.update.exe";
            var exepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Realm.exe";
            var temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
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
                Interface.type("Connection error.", ConsoleColor.Red);
        }
        private void startDownload()
        {
            Console.Write("Loading...0%");
            var temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            var client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync((new Uri("https://dl.dropboxusercontent.com/u/83385592/Realm.exe")), temppath);
        }
        public static void worker()
        {
            var bw = new BackgroundWorker();

            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                var b = o as BackgroundWorker;
                var webClient = new WebClient();
                try
                {
                    var fio = new FileIO();
                    fio.startDownload();
                }
                catch (WebException)
                {
                    Interface.type("Failed to connect to server.");
                    Init.failed = true;
                }
            });
            bw.RunWorkerAsync();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;
            Console.Write("\rLoading...{0}%", (int)percentage);
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Interface.type("Done.");
            Init.init = true;
        }
    } 
}