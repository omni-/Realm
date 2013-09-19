using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow(); private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow); private const int HIDE = 0; private const int MAXIMIZE = 3; private const int MINIMIZE = 6; private const int RESTORE = 9;
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.txt";
            if (!File.Exists(path))
                File.Create(path);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight); ShowWindow(ThisConsole, MAXIMIZE);
            Init.Initialize();
        }
    }
}
