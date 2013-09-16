using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class End
    {
        public static bool IsDead = false;
        
        public static void GameOver()
        {
            if (IsDead)
            {
                Formatting.type("Game Over. Press any key to exit, esc during non-combat to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

    }
}
