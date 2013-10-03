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
        public static void BattleLoop(Enemy enemy)
        {
            Main.game_state = 1;

            Main.Player.applybonus();

            int enemydmg = 0;
            int mana = 1 + Main.Player.intl / 10;
            Interface.type("You have entered combat! Ready your weapons!");
            Interface.type("Level " + enemy.level + " " + enemy.name + ":");
            Interface.type("-------------------------");
            Interface.type("HP: " + enemy.hp);
            Interface.type("Attack: " + enemy.atk);
            Interface.type("Defense: " + enemy.def);
            Interface.type("-------------------------");
            bool is_turn = enemy.spd < Main.Player.spd;
            while (enemy.hp >= 0)
            {
                Interface.type("//////////////////////");
                Interface.type("Your HP: " + Main.Player.hp);
                Interface.type("Your Mana: " + mana);
                Interface.type("----------------------");
                Interface.type("Enemy HP: " + enemy.hp);
                Interface.type("//////////////////////");
                if (is_turn && !Main.Player.stunned)
                {
                    if (Main.Player.phased)
                        Main.Player.phased = false;
                    Interface.type("\r\nAVAILABLE MOVES:");
                    Interface.type("=========================");
                    int i = 0;
                    foreach (Realm.Combat.Command c in Main.Player.abilities.commands.Values)
                    {
                        string src = "||   " + c.cmdchar + ". " + c.name;
                        Interface.type(src);
                        i++;
                    }
                    Interface.type("=========================");
                    Interface.type("");

                    if (Main.Player.fire >= 3)
                        Main.Player.on_fire = false;
                    if (Main.Player.on_fire)
                    {
                        Main.Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Main.Player.hp -= dmg;
                        Interface.type("You take " + dmg + " fire damage.");
                    }
                    if (Main.Player.blinded)
                        Main.Player.blinded = false;
                    if (Main.Player.cursed)
                    {
                        Main.Player.hp -= Combat.Dice.roll(1, 6);
                        Interface.type("You are cursed!");
                    }

                    int oldhp = enemy.hp;
                    char ch = Interface.readkey().KeyChar;
                    while (!Main.Player.abilities.commandChars.Contains(ch))
                    {
                        Interface.type("Invalid.");
                        Interface.type("");
                        ch = Interface.readkey().KeyChar;
                    }
                    while (ch != 'b' && mana <= 0)
                    {
                        Interface.type("Out of mana!");
                        Interface.type("");
                        ch = Interface.readkey().KeyChar;
                    }
                    if (ch != 'b')
                        mana--;
                    if (ch == 'm')
                    {
                        Interface.type("You mimc the enemy's damage!");
                        enemy.hp -= enemydmg;
                    }
                    if (!Main.Player.blinded)
                        Main.Player.abilities.ExecuteCommand(ch, enemy);
                    else
                    {
                        Interface.type("You are blind!");
                        if (Combat.Dice.roll(1, 10) == 1)
                        {
                            Interface.type("By some miracle, you manage to hit them!");
                            Main.Player.abilities.ExecuteCommand(ch, enemy);
                            int blindenemyhp = oldhp - enemy.hp;
                            Interface.type("The enemy takes " + blindenemyhp + " damage!");
                        }
                    }
                    int enemyhp = oldhp - enemy.hp;
                    Interface.type("The enemy takes " + enemyhp + " damage!");
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        Main.Player.levelup();
                        return;
                    }
                    if (!Main.Player.phased)
                        is_turn = false;
                }
                else if (Main.Player.stunned)
                {
                    Interface.type("You are stunned!");
                    Main.Player.stunned = false;
                    if (Main.Player.fire >= 3)
                        Main.Player.on_fire = false;
                    if (Main.Player.on_fire)
                    {
                        Main.Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Main.Player.hp -= dmg;
                        Interface.type("You take " + dmg + " fire damage.");
                    }
                    if (Main.Player.cursed)
                    {
                        Main.Player.hp -= Combat.Dice.roll(1, 6);
                        Interface.type("You are cursed!");
                    }
                    is_turn = false;
                }

                else if (!is_turn && !enemy.stunned)
                {
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (Main.Player.guard >= 2)
                        Main.Player.guarded = false;
                    if (Main.Player.guarded)
                        Main.Player.guard++;
                    if (enemy.blinded)
                        enemy.blinded = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " fire damage.");
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!");
                            enemy.droploot();
                            Main.Player.levelup();
                            return;
                        }
                    }
                    if (enemy.cursed)
                    {
                        Interface.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!");
                            enemy.droploot();
                            Main.Player.levelup();
                            return;
                        }
                    }
                    if (enemy.trapped)
                    {
                        Interface.type("Your trap has sprung!");
                        int dmg = (Main.Player.level / 5) + (Main.Player.intl / 3) + Combat.Dice.roll(1, 5);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " damage!");
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!");
                            enemy.droploot();
                            Main.Player.levelup();
                            return;
                        }
                    }
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        Main.Player.levelup();
                        return;
                    }
                    int oldhp = Main.Player.hp;
                    string ability;
                    if (!Main.Player.guarded && !enemy.blinded)
                    {
                        enemy.attack(out ability);
                        enemydmg = oldhp - Main.Player.hp;
                        Interface.type(enemy.name + " used " + ability);
                        Interface.type("You take " + enemydmg + " damage!");
                    }
                    else
                    {
                        if (Main.Player.blinded)
                        {
                            Interface.type(enemy.name + "is blind!");
                            if (Combat.Dice.roll(1, 10) == 1)
                            {
                                Interface.type("By some miracle, " + enemy.name + " manages to hit you!");
                                enemy.attack(out ability);
                                enemydmg = oldhp - Main.Player.hp;
                                Interface.type(enemy.name + " used " + ability);
                                Interface.type("You take " + enemydmg + " damage!");
                            }
                        }
                        else if (Main.Player.guarded)
                            Interface.type("Safeguard prevented damage!");
                    }
                    if (Main.Player.hp <= 0)
                        End.GameOver();
                    is_turn = true;
                }
                else if (enemy.stunned)
                {
                    Interface.type(enemy.name + " is stunned!");
                    enemy.stunned = false;
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " fire damage.");
                    }
                    if (enemy.cursed)
                    {
                        Interface.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                    }
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        Main.Player.levelup();
                        return;
                    }
                    is_turn = true;

                }
            }
        }
        public static void SammysAdventure()
        {

        }
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
                int misschance = Dice.roll(1, 101 + (spd * 3));
                if (misschance == 1)
                {
                    Interface.type("Missed!");
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool CheckBattle()
        {
            Random randint = new Random();
            int rollresult = Dice.roll(1, 3);
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
        public static bool stunchance(int chance)
        {
            if (Dice.roll(1, chance) == 1)
                return true;
            else
                return false;
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
                if (Dice.misschance(target.spd))
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
                if (Dice.misschance(target.spd))
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
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(target.spd))
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
                if (Dice.misschance(target.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                int heal = Convert.ToInt32(damage / 3);
                Main.Player.hp += heal;
                Interface.type("You gain " + heal + " life.");
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
                if (Dice.misschance(target.spd))
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
                target.cursed = true;
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
                if (dmg <= 0)
                    dmg = 1;
                if (Dice.misschance(target.spd))
                    dmg = 0;
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
                Main.Player.phased = true;
                target.hp -= 2;
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
                int damage = Dice.roll(1, ((Main.Player.atk/3) + Main.Player.intl/3));
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(target.spd))
                    damage = 0;
                target.hp -= damage;
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
                if (stunchance(3))
                    target.stunned = true;
                target.on_fire = true;
                int damage = Dice.roll(1, Main.Player.atk / 2);
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(target.spd))
                    damage = 0;
                target.hp -= damage;
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
                if (stunchance(2))
                    target.stunned = true;
                int damage = (Main.Player.atk) + (Main.Player.def / 3);
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(target.spd))
                    damage = 0;
                target.hp -= damage;
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
                target.cursed = true;
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
                    target.cursed = true;
                }
                    return true;
            }
        }
        public class Mimic : Command
        {
            public Mimic(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                return true;
            }
        }
        public class LayTrap : Command
        {
            public LayTrap(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.trapped = true;
                return true;
            }
        }
        public class Safeguard : Command
        {
            public Safeguard(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Main.Player.guarded = true;
                return true;
            }
        }
        public class Rage : Command
        {
            public Rage(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int damage = (Main.Player.maxhp - Main.Player.hp);
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(target.spd))
                    damage = 0;
                target.hp -= damage;
                return true;
            }
        }
        public class Lightspeed : Command
        {
            public Lightspeed(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.hp -= Main.Player.spd + Dice.roll(1, Main.Player.spd);
                return true;
            }
        }
        public class Nightshade : Command
        {
            public Nightshade(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                target.blinded = true;
                int dmg = Dice.roll(2, Main.Player.atk);
                if (dmg <= 0)
                    dmg = 1;
                if (Dice.misschance(target.spd))
                    dmg = 0;
                target.hp -= dmg;
                return true;
            }
        }
        public class ForcePulse : Command
        {
            public ForcePulse(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int dmg = Dice.roll(9, (Main.Player.intl + Main.Player.def) / 10);
                target.hp -= dmg;
                return true;
            }
        }
        public class IceChains : Command
        {
            public IceChains(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int dmg = Dice.roll(6, (Main.Player.intl + Main.Player.atk) / 6);
                int stun = Dice.roll(1, 5);
                if (stun == 1 || stun == 2 || stun == 3 || stun == 4)
                    target.stunned = true;
                target.hp -= dmg;
                return true;
            }
        }
        public class NowYouSeeMe : Command
        {
            public NowYouSeeMe(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int dmg = Dice.roll(1, Main.Player.intl);
                target.blinded = true;
                target.hp -= dmg;
                return true;
            }
        }
        public class Illusion : Command
        {
            public Illusion(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int dmg = Dice.roll(1, (Main.Player.intl + Main.Player.atk) / 2);
                target.hp -= dmg;
                return true;
            }
        }
        public class PewPewPew : Command
        {
            public PewPewPew(string aname, char cmd)
                : base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Enemy target = (Enemy)Data;
                int dmg = Dice.roll(2, Main.Player.intl) - 2;
                target.hp -= dmg;
                return true;
            }
        }
    }
}
   