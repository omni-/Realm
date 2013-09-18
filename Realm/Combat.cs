using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
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
                    total += rand.Next(1, Math.Abs(numsides + 1));
                }
                return total;
            }
            public static bool misschance(int spd)
            {
                int misschance = Dice.roll(1, 101 - (spd * 3));
                if (misschance == 1)
                {
                    Formatting.type("Missed!");
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool CheckBattle()
        {
            Random randint = new Random();
            int rollresult = Dice.roll(1, 2);
            return rollresult == 1;
        }
        public static string DecideAttack(List<string> abilities)
        {
            int i = abilities.Count;
            Random rand = new Random();
            int randint = rand.Next(1, ((i * 3) + 1));
            if (randint > i)
                return abilities[0];
            else
            {
                return abilities[rand.Next(1, abilities.Count)];
            }
            //return abilities[1];
        }
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
        public class BasicAttack : Command
        {
            public BasicAttack(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object data)
            {
                // data should be the enemy
                Enemy target = (Enemy)data;
                double damage = Dice.roll(1, Main.Player.atk);
                if (Main.Player.primary.multiplier != 0)
                    damage *= Main.Player.primary.multiplier;
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(Main.Player.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                return true;
            }
        }
        public class EnergyOverload : Command
        {
            public EnergyOverload(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object data)
            {
                // data should be the enemy
                Enemy target = (Enemy)data;
                double damage = Dice.roll(2, Main.Player.intl);
                if (damage <= 0)
                    damage = 1;
                if (Combat.Dice.misschance(-Main.Player.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                return true;
            }
        }
        public class BladeDash : Command
        {
            public BladeDash(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object data)
            {
                // data should be the enemy
                Enemy target = (Enemy)data;
                double damage = Dice.roll(2, Main.Player.atk);
                if (Combat.Dice.misschance(-Main.Player.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                return true;
            }
        }
        public class ConsumeSoul : Command
        {
            public ConsumeSoul(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object data)
            {
                // data should be the enemy
                Enemy target = (Enemy)data;
                double damage = Dice.roll(2, Main.Player.atk * 2 / 3);
                if (damage <= 0)
                    damage = 1;
                if (Combat.Dice.misschance(-Main.Player.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                Main.Player.hp += Convert.ToInt32(damage / 3);
                Formatting.type("You gain " + damage + " life.");
                return true;
            }
        }
        public class HolySmite : Command
        {
            public HolySmite(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object data)
            {
                // data should be the enemy
                Enemy target = (Enemy)data;
                double damage = Dice.roll(2, Main.Player.def/2);
                if (damage <= 0)
                    damage = 1;
                if (Combat.Dice.misschance(-Main.Player.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                return true;
            }
        }
        public class EndtheIllusion : Command
        {
            public EndtheIllusion(string aname, char cmd)
                : base(aname, cmd)
            {
            }

            public override bool Execute(object data)
            {
                Enemy target = (Enemy)data;
                double damage = Dice.roll((Main.Player.atk + Main.Player.def + Main.Player.spd + Main.Player.intl), Main.Player.level);
                target.hp -= Convert.ToInt32(damage);
                return true;
            }
        }
        public class ArrowsofLies : Command
        {
            public ArrowsofLies(string aname, char cmd)
                : base(aname, cmd)
            {
            }

            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.hp = 0;
                return true;
            }
        }

        public class Curse : Command
        {
            public Curse(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.hp -= Dice.roll(1, Main.Player.intl);
                target.is_cursed = true;
                return true;
            }
        }

        public class Sacrifice : Command
        {
            public Sacrifice(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int dmg = Dice.roll(1, Main.Player.atk / 2);
                target.hp -= dmg;
                Main.Player.hp -= dmg / 2;
                return true;
            }
        }
        public class Phase : Command
        {
            public Phase(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                Main.Player.is_phased = true;
                return true;
            }
        }
        public class VorpalBlades : Command
        {
            public VorpalBlades(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.hp -= Dice.roll(1, ((Main.Player.atk/3) + Main.Player.intl/3));
                return true;
            }
        }
        public class Incinerate : Command
        {
            public Incinerate(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.hp -= Dice.roll(1, Main.Player.intl);
                target.on_fire = true;
                return true;
            }
        }
        public class Dawnstrike : Command
        {
            public Dawnstrike(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.stunned = true;
                target.on_fire = true;
                target.hp -= Dice.roll(1, Main.Player.atk / 2);
                return true;
            }
        }
        public class Heavensplitter : Command
        {
            public Heavensplitter(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.stunned = true;
                target.hp -= (Main.Player.atk) + (Main.Player.def / 3);
                return true;
            }
        }
        public class HellsKitchen : Command
        {
            public HellsKitchen(string aname, char cmd)
                : base (aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.stunned = true;
                target.on_fire = true;
                target.is_cursed = true;
                target.hp -= 5;
                return true;
            }
        }

        public class Gamble : Command
        {
            public Gamble (string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int chance = Dice.roll(1, 10);
                if (chance <= 5)
                {
                    Main.Player.hp -= 2 * chance;
                }
                else
                {
                    target.hp -= Main.Player.atk * 5;
                    target.stunned = true;
                    target.on_fire = true;
                    target.is_cursed = true;
                }
                    return true;
            }
        }
    }
}
   