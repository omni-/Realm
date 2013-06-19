using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class End
    {
        public static void GameOver()
        {
            Console.WriteLine("Game Over. Press any key to exit.");
            Console.ReadKey();
            Init.Initialize();
        }

    }
}
