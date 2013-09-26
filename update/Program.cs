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
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://dl.dropboxusercontent.com/u/83385592/Realm.exe", path);
            Process.Start(path);
            Environment.Exit(0);
        }
    }
}
