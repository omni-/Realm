using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Realm
{
    public class FileIO
    {
        private static bool FileCompare(string file1, string file2)
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
            } while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        private static bool isConnected()
        {
            int desc;
            var tf = NativeMethods.InternetGetConnectedState(out desc, 0);
            return tf;
        }

        private static string CalculateMD5Hash(string input)
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
        static string compute(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
                }
            }
        }
        public static void checkver()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string programName = "update.exe";
            const string resourceName = "Realm.update.exe";
            var exepath = path + "\\Realm.exe";
            var temppath = path + "\\test.txt";
            worker();
            while (!Init.init)
            {
            }

            if (isConnected())
            {
                if (Init.failed) return;
                if (compute(exepath) != File.ReadAllText(temppath))
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
            else
                Interface.type("Connection error.", ConsoleColor.Red);
        }

        private void startDownload()
        {
            Console.Write("Loading...0%");
            var temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.txt";
            var client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync((new Uri("https://dl.dropboxusercontent.com/u/83385592/md5.txt")), temppath);
        }

        private static void worker()
        {
            var bw = new BackgroundWorker { WorkerReportsProgress = true };

            bw.DoWork += delegate
            {
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
            };
            bw.RunWorkerAsync();
        }

        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;
            Console.Write("\rLoading...{0}%", (int)percentage);
        }

        private static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Interface.type("Done.");
            Init.init = true;
        }
    }
}
