using System;
using System.Collections.Generic;

namespace Realm
{
    public class Enemy
    {
        public string name;
        public int level,hp, atk, def, spd, xpdice, gpdice, fire, extrarep;
        public bool trapped = false, cursed = false, on_fire = false, stunned = false, blinded = false;
        protected List<string> abilities;
        
        public virtual void attack(out string ability_used)
        {
            ability_used = "";
            return;
        }
        public virtual void droploot()
        {
            var gold = Combat.Dice.roll(1, gpdice);
            Interface.type("You gain " + gold + " gold.", ConsoleColor.Yellow);
            if (!Main.is_theif)
                Main.Player.g += gold;
            else
                Main.Player.g += (gold + (gold / 10));

            var xp = Combat.Dice.roll(1, xpdice);
            Interface.type("You gained " + xp + " xp.", ConsoleColor.Yellow);
            Main.Player.xp += xp;
            var dropcands = Player.getCorrectlyTieredItems();

            if (Main.rand.NextDouble() <= .1d)
            {
                Main.Player.backpack.Add(dropcands[Main.rand.Next(0, dropcands.Count - 1)]);
                Interface.type("Obtained " + dropcands[Main.rand.Next(0, dropcands.Count - 1)].name + "!", ConsoleColor.Green);
            }

            Main.Player.reputation += (1 + extrarep);
        }
        public Enemy()
        {
            level = Main.Player.level;
        }
    }

    public class Slime : Enemy
    {
        public Slime()
        {
            name = "Slime";
            hp = 5 + level;
            atk = level < 3 ? 1 : 2 + (level / 2);
            def = level < 3 ? 0 : 2 + (level / 2);
            spd = level < 3 ? 0 : 2 + (level / 2);
            xpdice = 12 - (level / 5);
            gpdice = 8 - (level / 5);
            abilities = new List<string> { "BasicAttack", "SuperSlimySlam" };
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "SuperSlimySlam")
            {
                double damage = Combat.Dice.roll(1, (atk * 2) + 1);
                dmg = (Convert.ToInt32(damage) - Main.Player.def) + 1;
                ability_used = "Super Slimy Slam";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class Goblin : Enemy
    {
        public Goblin()
        {
            name = "Goblin";
            hp = 8 + (level / 2);
            atk = 3 + (level / 2);
            def = 1 + (level / 2);
            spd = 1 + (level / 2);
            xpdice = 20;
            gpdice = 15;
            abilities = new List<string> { "BasicAttack", "Impale", "CrazedSlashes" };
        }
        public override void attack(out string ability_used)
        {
            var dmg = 0;
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Math.Max((Convert.ToInt32(damage) - (Main.Player.def / 3)), 0);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Impale")
            {
                double damage = Combat.Dice.roll(1, atk * 2);
                dmg = Convert.ToInt32(damage);
                ability_used = "Impale";
            }
            else if (Combat.DecideAttack(abilities) == "CrazedSlashes")
            {
                double damage = Combat.Dice.roll(1, atk);
                damage *= Combat.Dice.roll(1, 5);
                dmg = Convert.ToInt32(damage);
                ability_used = "Crazed Slashes";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class Bandit : Enemy
    {
        public Bandit()
        {
            name = "Bandit";
            hp = 12 + (level / 2);
            atk = 1 + (level / 2);
            def = 0 + (level / 2);
            spd = 3 + (level / 2);
            xpdice = 25;
            gpdice = 18;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("DustStorm");
            abilities.Add("RavenousPound");
        }
        public override void attack(out string ability_used)
        {
            var dmg = 0;
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Math.Max((Convert.ToInt32(damage) - (Main.Player.def / 3)), 0);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "DustStorm")
            {
                double damage = Combat.Dice.roll(atk, 4);
                dmg = Convert.ToInt32(damage);
                ability_used = "Dust Storm";
            }
            else if (Combat.DecideAttack(abilities) == "RavenousPound")
            {
                double damage = Combat.Dice.roll(3, 4);
                dmg = Convert.ToInt32(damage);
                ability_used = "Ravenous Pound";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class WesternKing: Enemy
    {
        public WesternKing()
        {
            name = "Western King";
            hp = 1500 + (level / 2);
            atk = 50 + (level / 2);
            def = 35 + (level / 2);
            spd = 25 + (level / 2);
            xpdice = 100 + (level / 2);
            gpdice = 100 + (level / 2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Terminate");
            extrarep = 99;
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - -(Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Terminate")
            {
                double damage = Combat.Dice.roll(atk, atk * 2);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Terminate";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class Drake : Enemy
    {
        public Drake()
        {
            name = "Drake";
            hp = 30 + (level / 2);
            atk = 12 + (level / 2);
            def = 10 + (level / 2);
            spd = 10 + (level / 2);
            xpdice = 35;
            gpdice = 35;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Singe");
            abilities.Add("Chomp");

        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Singe")
            {
                double damage = Combat.Dice.roll(1, atk * 2 / 3);
                Main.Player.on_fire = true;
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Singe";
            }
            else if (Combat.DecideAttack(abilities) == "Chomp")
            {
                double damage = Combat.Dice.roll(2, atk);
                Main.Player.stunned = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Chomp";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class RavenKing : Enemy
    {
        public RavenKing()
        {
            name = "Raven King";
            hp = 250 + (level / 2);
            atk = 45 + (level / 2);
            def = 20 + (level / 2);
            spd = 25 + (level / 2);
            xpdice = 80;
            gpdice = 80;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Crow Call");
            abilities.Add("Murder");
            extrarep = 49;

        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Crow Call")
            {
                double damage = Combat.Dice.roll(2, atk / 3);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Crow Call";
            }
            else if (Combat.DecideAttack(abilities) == "Murder")
            {
                double damage = Combat.Dice.roll(3, atk);
                Main.Player.stunned = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Murder";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class finalboss : Enemy
    {
        public finalboss()
        {
            name = "Janus";
            hp = 750 + (level / 2);
            atk = 150 + (level / 2);
            def = 85 + (level / 2);
            spd = 60 + (level / 2);
            xpdice = 1 + (level / 2);
            gpdice = 1 + (level / 2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Illusory Slash");
            abilities.Add("Time Bend");

        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Illusory Slash")
            {
                double damage = Combat.Dice.roll(3, atk);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage);
                ability_used = "Illusory Slash";
            }
            else if (Combat.DecideAttack(abilities) == "Time Bend")
            {
                double damage = Combat.Dice.roll(2, atk / 2);
                Main.Player.stunned = true;
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class cavespider : Enemy
    {
        public cavespider()
        {
            name = "Cave Spider";
            hp = 20 + (level / 2);
            atk = 10 + (level / 2);
            def = 5 + (level / 2);
            spd = 6 + (level / 2);
            xpdice = 30 + (level / 2);
            gpdice = 30 + (level / 2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Poison Bite");
            abilities.Add("Cocoon");

        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Poison Bite")
            {
                double damage = Combat.Dice.roll(1, atk);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage);
                ability_used = "Posion Bite";
            }
            else if (Combat.DecideAttack(abilities) == "Cocoon")
            {
                Main.Player.stunned = true;
                dmg = 2;
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class cavebat : Enemy
    {
        public cavebat()
        {
            name = "Cave Bat";
            hp = 10 + (level / 2);
            atk = 6 + (level / 2);
            def = 3 + (level / 2);
            spd = 4 + (level / 2);
            xpdice = 15 + (level / 2);
            gpdice = 15 + (level / 2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Screech");
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Screech")
            {
                if (Combat.stunchance(3))
                    Main.Player.stunned = true;
                dmg = 3;
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class Dragon : Enemy
    {
        public Dragon()
        {
            name = "Tyrone the Dragon";
            hp = 100 + (level / 2);
            atk = 50 + (level / 2);
            def = 25 + (level / 2);
            spd = 30 + (level / 2);
            xpdice = 300 + (level / 2);
            gpdice = 75 + (level / 2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Flame Breath");
            abilities.Add("Cursed Claw");
            extrarep = 149;
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def / 3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Flame Breath")
            {
                Main.Player.on_fire = true;
                dmg = Combat.Dice.roll(1, atk / 2);
            }
            else if (Combat.DecideAttack(abilities) == "Cursed Claw")
            {
                Main.Player.cursed = true;
                dmg = Combat.Dice.roll(2, 20);
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
}
