using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Realm
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight);
                NativeMethods.ShowWindow(NativeMethods.ThisConsole, NativeMethods.MAXIMIZE);
                if (args.Contains("-wipe"))
                {
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                    "\\achievements.rlm");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\save.rlm");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp_save.rlm");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                    "\\temp_achievements.rlm");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\update.exe");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\crashlog.txt");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
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