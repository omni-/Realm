using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Home
    {
        public static void GameOver()
        {
            Console.WriteLine("Game Over. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void Init()
        {
            Console.WriteLine("You are the Hero of the Western Kingdom. The Western King has called for your presence.");
            Console.WriteLine("King: 'Come, my champion, for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite.'  The Western King approaches and unsheathes his blade emitting a strong aura of bloodlust. Fight or Flee? [F to fight, R to run]");
            string input = Console.ReadLine().ToString();
            if (input == "f")
            {
                Console.WriteLine("The King gives you swift respite. He decapitates your sorry ass, you piece of shit");
                GameOver();
            }

        }
    }
}
