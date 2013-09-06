using System;
using System.Collections;
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

        public static char[] getBattleCommands()
        {
            List<char> templist = new List<char>();
            for (int i = 0; i < Main.player.abilities.Count; i++)
            {
                templist.Add(Convert.ToChar(i + 49));
            }
            return templist.ToArray<char>();
        }
        private static bool IsValid(char cmd)
        {
            bool IsFound = false;
            char[] cmdlist = getBattleCommands();
            foreach (char c in cmdlist)
            {
                IsFound = c == cmd;
                if (IsFound)
                    break;
            }
            return IsFound;
        }

        public static bool handleInput(char input)
        {
            Hashtable dispatch = new Hashtable();
            char[] cmdlist = getBattleCommands();
            foreach (char c in cmdlist)
            {
                dispatch.Add(c, new BasicAttack());
            }
            return true;
        }
        public class BasicAttack
        {
            public static void basicattack()
            {
                Enemy enemy = new Enemy();
                enemy.hp -= Main.player.atk;
            }
        }
    }
}
