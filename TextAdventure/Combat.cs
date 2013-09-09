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

        //public static CommandTable getBattleCommands()
        //{
        //    CommandTable tempCommandTable = new CommandTable();
        //    for (int i = 0; i < Player.abilities.Count; i++)
        //    {
        //        int cmd = i + 49;
        //        char cmdchar = Convert.ToChar(cmd);
        //        tempCommandTable.AddCommand(new Command(BasicAttack, cmdchar));
        //    }
        //    return tempCommandTable;
        //}
        public class Command
        {
            public string name;
            public char cmdchar;

            public Command()
            {
            }

            public Command(string aname, char achar)
            {
                name = aname;
                cmdchar = achar;
            }

            public virtual bool Execute(object Data)
            {
                return false;
            }
        }

        public class CommandTable
        {
            private Dictionary<char, Command> _commands;

            public CommandTable()
            {
                _commands = new Dictionary<char, Command>();
            }

            public int Count { get { return _commands.Count; } }

            public void AddCommand(Command cmd)
            {
                _commands.Add(cmd.cmdchar, cmd);
            }

            public char[] commandChars
            {
                get { return _commands.Keys.ToArray<char>(); }
            }

            public Dictionary<char, Command> commands
            {
                get { return _commands; }
                set { _commands = value; }
            }

            public bool ExecuteCommand(char ch, object data)
            {
                try
                {
                    Command cmd = _commands[ch];
                    cmd.Execute(data);
                }
                catch (KeyNotFoundException)
                {
                    return false;
                }
                return true;
            }
        }
        public class BasicAttack: Command
        {
            public BasicAttack(string aname, char cmd): base(aname, cmd)
            {
            }
            public override bool Execute(object data)
            {
                // data should be the enemy
                Enemy target = (Enemy)data;
                double damage = Dice.roll(Main.Player.atk, 4);
                if (Main.Player.primary.multiplier != 0)
                    damage *= Main.Player.primary.multiplier;
                target.hp -= Convert.ToInt32(damage);
                return true;
            }
        }
    }
}
