using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Realm
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight);
                NativeMethods.ShowWindow(NativeMethods.ThisConsole, NativeMethods.MAXIMIZE);
                if (args.Contains("-wipe"))
                {
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\achievements.rlm"); }
                    catch { }
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.rlm"); }
                    catch { }
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\test.exe"); }
                    catch { }
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_save.rlm"); }
                    catch { }
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\temp_achievements.rlm"); }
                    catch { }
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\update.exe"); }
                    catch { }
                    try { File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\crashlog.txt"); }
                    catch { }
                }
                if (args.Contains("-noachievements"))
                    Realm.Main.achievements_disabled = true;
                if (args.Contains("-devmode"))
                    Realm.Main.devmode = true;
                Init.Initialize();
            }
            catch (Exception e)
            {
                Save.WriteError(e);
            }
        }
    }
}
