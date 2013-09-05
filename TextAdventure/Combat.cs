using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Combat
    {
        public static bool CheckBattle()
        {
            Random randint = new Random();
            if (randint.Next(1, 5) == 2)
                return true;
            else
                return false;
        }

        public static string DecideAttack(List<string> abilities)
        {
            int i = 0;
            foreach (string s in abilities)
                i++;
            Random rand = new Random();
            int randint = rand.Next(1, ((i * 3) + 1));
            if (randint > i)
                return abilities[0];
            else
            {
                return abilities[rand.Next(2, abilities.Count)];
            }
        }
    }
}
