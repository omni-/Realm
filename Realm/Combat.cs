// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/27/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

namespace Realm
{
    public class Combat
    {
        public static void BattleLoop(Enemy enemy)
        {
            Main.Player.applybonus();

            var enemydmg = 0;
            var mana = 1 + Main.Player.intl/10;
            Interface.type("You have entered combat! Ready your weapons!", ConsoleColor.Red);
            Interface.type("Level " + enemy.level + " " + enemy.name + ":", ConsoleColor.Yellow);
            Interface.type("-------------------------", ConsoleColor.Yellow);
            Interface.type("HP: " + enemy.hp, ConsoleColor.Yellow);
            Interface.type("Attack: " + enemy.atk, ConsoleColor.Yellow);
            Interface.type("Defense: " + enemy.def, ConsoleColor.Yellow);
            Interface.type("-------------------------", ConsoleColor.Yellow);
            var is_turn = enemy.spd < Main.Player.spd;
            while (enemy.hp >= 0)
            {
                Interface.type("----------------------", ConsoleColor.Cyan);
                Interface.type("Your HP: " + Main.Player.hp, ConsoleColor.Cyan);
                Interface.type("Your Mana: " + mana, ConsoleColor.Cyan);
                Interface.type("----------------------", ConsoleColor.Cyan);
                Interface.type("Enemy HP: " + enemy.hp, ConsoleColor.Red);
                Interface.type("----------------------", ConsoleColor.Cyan);
                if (is_turn && !Main.Player.stunned)
                {
                    if (Main.Player.phased)
                        Main.Player.phased = false;
                    Interface.type("\r\nAVAILABLE MOVES:", ConsoleColor.Cyan);
                    Interface.type("-------------", ConsoleColor.Cyan);
                    var i = 0;
                    foreach (var c in Main.Player.abilities.commands.Values)
                    {
                        var src = "||   " + c.cmdchar + ". " + c.name;
                        Interface.type(src, ConsoleColor.Cyan);
                        i++;
                    }
                    Interface.type("-------------", ConsoleColor.Cyan);
                    Interface.type("");

                    if (Main.Player.fire >= 3)
                        Main.Player.on_fire = false;
                    if (Main.Player.on_fire)
                    {
                        Main.Player.fire++;
                        var dmg = Dice.roll(1, 3);
                        Main.Player.hp -= dmg;
                        Interface.type("You take " + dmg + " fire damage.", ConsoleColor.Red);
                    }
                    if (Main.Player.blinded)
                        Main.Player.blinded = false;
                    if (Main.Player.cursed)
                    {
                        Main.Player.hp -= Dice.roll(1, 6);
                        Interface.type("You are cursed!", ConsoleColor.Red);
                    }

                    var oldhp = enemy.hp;
                    var ch = Interface.readkey().KeyChar;
                    while (!Main.Player.abilities.commandChars.Contains(ch))
                    {
                        Interface.type("Invalid.");
                        Interface.type("");
                        ch = Interface.readkey().KeyChar;
                    }
                    while (ch != 'b' && mana <= 0)
                    {
                        Interface.type("Out of mana!", ConsoleColor.Red);
                        Interface.type("");
                        ch = Interface.readkey().KeyChar;
                    }
                    if (ch != 'b')
                        mana--;
                    if (ch == 'm')
                    {
                        Interface.type("You mimc the enemy's damage!", ConsoleColor.Cyan);
                        enemy.hp -= enemydmg;
                    }
                    if (!Main.Player.blinded)
                        Main.Player.abilities.ExecuteCommand(ch, enemy);
                    else
                    {
                        Interface.type("You are blind!", ConsoleColor.Red);
                        if (Dice.roll(1, 10) == 1)
                        {
                            Interface.type("By some miracle, you manage to hit", ConsoleColor.Cyan);
                            Main.Player.abilities.ExecuteCommand(ch, enemy);
                            var blindenemyhp = oldhp - enemy.hp;
                            if (ch != 'l')
                                Interface.type("The enemy takes " + blindenemyhp + " damage!", ConsoleColor.Cyan);
                        }
                    }
                    var enemyhp = oldhp - enemy.hp;
                    if (ch != 'l')
                        Interface.type("The enemy takes " + enemyhp + " damage!", ConsoleColor.Cyan);
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!", ConsoleColor.Yellow);
                        enemy.droploot();
                        Main.Player.levelup();
                        if (enemy.name == "slime")
                        {
                            Main.slimecounter++;
                            Main.ach.Get("1slime");
                        }
                        else if (enemy.name == "goblin")
                        {
                            Main.ach.Get("1goblin");
                            Main.goblincounter++;
                        }
                        else if (enemy.name == "bandit")
                        {
                            Main.banditcounter++;
                            Main.ach.Get("1bandit");
                        }
                        else if (enemy.name == "drake")
                        {
                            Main.ach.Get("1drake");
                            Main.drakecounter++;
                        }
                        return;
                    }
                    if (!Main.Player.phased)
                        is_turn = false;
                }
                else if (Main.Player.stunned)
                {
                    Interface.type("You are stunned!", ConsoleColor.Red);
                    Main.Player.stunned = false;
                    if (Main.Player.fire >= 3)
                        Main.Player.on_fire = false;
                    if (Main.Player.on_fire)
                    {
                        Main.Player.fire++;
                        var dmg = Dice.roll(1, 3);
                        Main.Player.hp -= dmg;
                        Interface.type("You take " + dmg + " fire damage.", ConsoleColor.Red);
                    }
                    if (Main.Player.cursed)
                    {
                        Main.Player.hp -= Dice.roll(1, 6);
                        Interface.type("You are cursed!", ConsoleColor.Red);
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
                        var dmg = Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " fire damage.", ConsoleColor.Cyan);
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!", ConsoleColor.Yellow);
                            enemy.droploot();
                            Main.Player.levelup();
                            if (enemy.name == "slime")
                            {
                                Main.slimecounter++;
                                Main.ach.Get("1slime");
                            }
                            else if (enemy.name == "goblin")
                            {
                                Main.ach.Get("1goblin");
                                Main.goblincounter++;
                            }
                            else if (enemy.name == "bandit")
                            {
                                Main.banditcounter++;
                                Main.ach.Get("1bandit");
                            }
                            else if (enemy.name == "drake")
                            {
                                Main.ach.Get("1drake");
                                Main.drakecounter++;
                            }
                            return;
                        }
                    }
                    if (enemy.cursed)
                    {
                        Interface.type(enemy.name + " is cursed!", ConsoleColor.Cyan);
                        enemy.hp -= Dice.roll(1, 6);
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!", ConsoleColor.Yellow);
                            enemy.droploot();
                            Main.Player.levelup();
                            return;
                        }
                    }
                    if (enemy.trapped)
                    {
                        Interface.type("Your trap has sprung!");
                        var dmg = (Main.Player.level/5) + (Main.Player.intl/3) + Dice.roll(1, 5);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " damage!", ConsoleColor.Cyan);
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!", ConsoleColor.Yellow);
                            enemy.droploot();
                            Main.Player.levelup();
                            return;
                        }
                    }
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!", ConsoleColor.Yellow);
                        enemy.droploot();
                        Main.Player.levelup();
                        return;
                    }
                    var oldhp = Main.Player.hp;
                    string ability;
                    if (!Main.Player.guarded && !enemy.blinded)
                    {
                        enemy.attack(out ability);
                        enemydmg = oldhp - Main.Player.hp;
                        Interface.type(enemy.name + " used " + ability, ConsoleColor.Red);
                        Interface.type("You take " + enemydmg + " damage!", ConsoleColor.Red);
                    }
                    else
                    {
                        if (Main.Player.blinded)
                        {
                            Interface.type(enemy.name + "is blind!", ConsoleColor.Cyan);
                            if (Dice.roll(1, 10) == 1)
                            {
                                Interface.type("By some miracle, " + enemy.name + " manages to hit you!",
                                    ConsoleColor.Red);
                                enemy.attack(out ability);
                                enemydmg = oldhp - Main.Player.hp;
                                Interface.type(enemy.name + " used " + ability, ConsoleColor.Red);
                                Interface.type("You take " + enemydmg + " damage!", ConsoleColor.Red);
                            }
                        }
                        else if (Main.Player.guarded)
                            Interface.type("Safeguard prevented damage!", ConsoleColor.Cyan);
                    }
                    if (Main.Player.hp <= 0)
                        End.GameOver();
                    is_turn = true;
                }
                else if (enemy.stunned)
                {
                    Interface.type(enemy.name + " is stunned!", ConsoleColor.Cyan);
                    enemy.stunned = false;
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        var dmg = Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " fire damage.", ConsoleColor.Cyan);
                    }
                    if (enemy.cursed)
                    {
                        Interface.type(enemy.name + " is cursed!", ConsoleColor.Cyan);
                        enemy.hp -= Dice.roll(1, 6);
                    }
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!", ConsoleColor.Yellow);
                        enemy.droploot();
                        Main.Player.levelup();
                        if (enemy.name == "slime")
                        {
                            Main.slimecounter++;
                            Main.ach.Get("1slime");
                        }
                        else if (enemy.name == "goblin")
                        {
                            Main.ach.Get("1goblin");
                            Main.goblincounter++;
                        }
                        else if (enemy.name == "bandit")
                        {
                            Main.banditcounter++;
                            Main.ach.Get("1bandit");
                        }
                        else if (enemy.name == "drake")
                        {
                            Main.ach.Get("1drake");
                            Main.drakecounter++;
                        }
                        return;
                    }
                    is_turn = true;
                }
            }
        }

        public static void SammysAdventure()
        {
        }

        public static bool CheckBattle()
        {
            var randint = new Random();
            var rollresult = Dice.roll(1, 3);
            return rollresult == 1;
        }

        public static string DecideAttack(List<string> abilities)
        {
            var i = abilities.Count;
            var rand = new Random();
            var randint = rand.Next(1, ((i*3) + 1));
            if (randint > i)
                return abilities[0];
            return abilities[rand.Next(1, abilities.Count)];
            //return abilities[1];
        }

        public static bool stunchance(int chance)
        {
            if (Dice.roll(1, chance) == 1)
                return true;
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

            public int Count
            {
                get { return _commands.Count; }
            }

            public void AddCommand(Command cmd)
            {
                _commands.Add(cmd.cmdchar, cmd);
                if (cmd.name != "Basic Attack" && cmd.GetType() == typeof (Command))
                    Interface.type("Learned " + cmd.name + "!", ConsoleColor.Cyan);
            }

            public char[] commandChars
            {
                get { return _commands.Keys.ToArray(); }
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
                    var cmd = _commands[ch];
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
                var target = (Enemy) data;
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
                var target = (Enemy) data;
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
                var target = (Enemy) data;
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
                var target = (Enemy) data;
                double damage = Dice.roll(2, Main.Player.atk*2/3);
                if (damage <= 0)
                    damage = 1;
                if (Dice.misschance(target.spd))
                    damage = 0;
                target.hp -= Convert.ToInt32(damage);
                var heal = Convert.ToInt32(damage/3);
                Main.Player.hp += heal;
                Interface.type("You gain " + heal + " life.", ConsoleColor.Cyan);
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
                var target = (Enemy) data;
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
                var target = (Enemy) data;
                double damage = Dice.roll((Main.Player.atk + Main.Player.def + Main.Player.spd + Main.Player.intl),
                    Main.Player.level);
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
                var target = (Enemy) Data;
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
                var target = (Enemy) Data;
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
                var target = (Enemy) Data;
                var dmg = Dice.roll(1, Main.Player.atk/2);
                if (dmg <= 0)
                    dmg = 1;
                if (Dice.misschance(target.spd))
                    dmg = 0;
                target.hp -= dmg;
                Main.Player.hp -= dmg/2;
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
                var target = (Enemy) Data;
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
                var target = (Enemy) Data;
                var damage = Dice.roll(1, ((Main.Player.atk/3) + Main.Player.intl/3));
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
                var target = (Enemy) Data;
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
                var target = (Enemy) Data;
                if (stunchance(3))
                    target.stunned = true;
                target.on_fire = true;
                var damage = Dice.roll(1, Main.Player.atk/2);
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
                var target = (Enemy) Data;
                if (stunchance(2))
                    target.stunned = true;
                var damage = (Main.Player.atk) + (Main.Player.def/3);
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
                : base(aname, cmd)
            {
            }

            public override bool Execute(object Data)
            {
                var target = (Enemy) Data;
                target.stunned = true;
                target.on_fire = true;
                target.cursed = true;
                target.hp -= 5;
                return true;
            }
        }

        public class Gamble : Command
        {
            public Gamble(string aname, char cmd)
                : base(aname, cmd)
            {
            }

            public override bool Execute(object Data)
            {
                var target = (Enemy) Data;
                var chance = Dice.roll(1, 10);
                if (chance <= 5)
                {
                    Main.Player.hp -= 2*chance;
                }
                else
                {
                    target.hp -= Main.Player.atk*5;
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

        public class Heal : Command
        {
            public Heal(string aname, char cmd)
                : base(aname, cmd)
            {
            }

            public override bool Execute(object Data)
            {
                var heal = Math.Max(((Main.Player.intl*2)/3), 1);
                Main.Player.hp += heal;
                if (Main.Player.hp > Main.Player.maxhp)
                    Main.Player.hp = Main.Player.maxhp;
                Interface.type("You gain " + heal + " life.", ConsoleColor.Cyan);
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
                var target = (Enemy) Data;
                var damage = (Main.Player.maxhp - Main.Player.hp);
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
                var target = (Enemy) Data;
                target.hp -= Dice.roll(1, Main.Player.spd) + Dice.roll(1, (Main.Player.spd/5));
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
                var target = (Enemy) Data;
                target.blinded = true;
                var dmg = Dice.roll(2, Main.Player.atk);
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
                var target = (Enemy) Data;
                var dmg = Dice.roll(9, (Main.Player.intl + Main.Player.def)/10);
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
                var target = (Enemy) Data;
                var dmg = Dice.roll(6, (Main.Player.intl + Main.Player.atk)/6);
                var stun = Dice.roll(1, 5);
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
                var target = (Enemy) Data;
                var dmg = Dice.roll(1, Main.Player.intl);
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
                var target = (Enemy) Data;
                var dmg = Dice.roll(1, (Main.Player.intl + Main.Player.atk)/2);
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
                var target = (Enemy) Data;
                var dmg = Dice.roll(2, Main.Player.intl) - 2;
                target.hp -= dmg;
                return true;
            }
        }
    }
}