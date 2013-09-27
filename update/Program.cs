using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace update
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Realm.exe";
            try
            {
                WebClient webClient = new WebClient();
                //webClient.OpenWrite("https://dl.dropboxusercontent.com/u/83385592/Realm.exe");
                webClient.DownloadFile("https://dl.dropboxusercontent.com/u/83385592/Realm.exe", path);
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
