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

        public static Hashtable getBattleCommands()
        {
            Hashtable temptable = new Hashtable();
            for (int i = 0; i < Main.player.abilities.Count; i++)
            {
                temptable.Add(Convert.ToChar(i + 49), null);
            }
            return temptable;
        }
        public class Attack
        {
            public virtual void HandleInput(char cmd, Hashtable cmds)
            {
                foreach (DictionaryEntry de in cmds)
                {
                    if(cmd == (char)de.Key)
                    {
                        //de.Value.
                    }
                }
            }
            public virtual void attack()
            {

            }
        }
        public class BasicAttack
        {
            //public override void attack(Enemy target)
            //{
            //    Hashtable cmds = getBattleCommands();
            //    char cmd = Console.ReadKey().KeyChar;
            //    while (!cmds.ContainsKey(cmd))
            //        cmd = Console.ReadKey().KeyChar;
            //    target.hp -= Main.player.atk - target.def;
            //}
        }
    }
}
