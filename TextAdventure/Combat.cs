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
        public static class Dice
        {
            public static int roll(int numdice, int numsides)
            {
                int total = 0;
                Random rand = new Random();
                for (int i = 0; i < numdice; i++)
                {
                    total += rand.Next(1, numsides + 1);
                }
                return total;
            }
        }
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

        public static CommandTable getBattleCommands()
        {
            CommandTable tempCommandTable = new CommandTable();
            for (int i = 0; i < Player.abilities.Count; i++)
            {
                tempCommandTable.AddCommand(Convert.ToChar(i + 49), );
            }
            return tempCommandTable;
        }
        public class Command
        {
            public virtual bool Execute(object Data)
            {
                return false;
            }
        }

        public class CommandTable
        {
            private Dictionary<char, Command> commands;

            public CommandTable()
            {
                commands = new Dictionary<char, Command>();
            }

            public int Count { get { return commands.Count; } }

            public void AddCommand(char ch, Command cmd)
            {
                commands.Add(ch, cmd);
            }

            public bool ExecuteCommand(char ch, object data)
            {
                try
                {
                    Command cmd = commands[ch];
                    cmd.Execute(data);
                }
                catch (KeyNotFoundException)
                {
                    return false;
                }
                return true;
            }
        }
        public class BasicAttack : Command
        {
            public static void basicattack(Enemy target)
            {
                double damage = Dice.roll(Player.atk, 4);
                damage *= Player.primary.multiplier;
                target.hp -= Convert.ToInt32(damage);
            }
        }
    }
}
