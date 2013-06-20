using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class End
    {
        public static bool IsDead = false;
        
        public static void GameOver()
        {
            if (IsDead)
            {
                Console.WriteLine("\r\n Game Over. Press any key to restart, x at any time to exit.");
                Console.ReadKey();
                Init.Initialize();
                Console.Clear();
                IsDead = false;
            }
        }

    }
}
