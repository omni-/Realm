using System;
using System.Runtime.InteropServices;

namespace Realm
{
    public static class NativeMethods
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();
        public static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow); public const int HIDE = 0; public const int MAXIMIZE = 3; public const int MINIMIZE = 6; public const int RESTORE = 9;

        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
    }
}
