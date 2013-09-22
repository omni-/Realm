using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class Enemy
    {
        public string name;
        public int level;
        public int hp;
        public int atk;
        public int def;
        public int spd;
        public int xpdice;
        public int gpdice;
        public bool trapped = false;
        public bool cursed = false;
        public bool on_fire = false;
        public bool stunned = false;
        public bool blinded = false;
        public int fire;
        public List<string> abilities;
        
        public virtual void attack(out string ability_used)
        {
            ability_used = "";
            return;
        }
        public virtual void droploot()
        {
            int gold = Combat.Dice.roll(1, gpdice);
            Formatting.type("You gain " + gold + " gold.");
            if (!Main.is_theif)
                Main.Player.g += gold;
            else
                Main.Player.g += (gold + (gold / 10));

            int xp = Combat.Dice.roll(1, xpdice);
            Formatting.type("You gained " + xp + " xp.");
            Main.Player.xp += xp;
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
            hp = 5 + (level / 2);
            atk = 1  + (level / 2);
            def = 0  + (level / 2);
            spd = 0 + (level / 2);
            xpdice = 10;
            gpdice = 1;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("SuperSlimySlam");
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            int dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                if (atk < Main.Player.def)
                    damage = 1;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "SuperSlimySlam")
            {
                double damage = Combat.Dice.roll(1, (atk * 2) + 1);
                dmg = (Convert.ToInt32(damage) - Main.Player.def);
                dmg += 1;
                ability_used = "Super Slimy Slam";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(-Main.Player.spd))
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
            xpdice = 15;
            gpdice = 3;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Impale");
            abilities.Add("CrazedSlashes");
        }
        public override void attack(out string ability_used)
        {
            int dmg = 0;
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Math.Max((Convert.ToInt32(damage) - Main.Player.def), 0);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Impale")
            {
                double damage = Combat.Dice.roll(atk, atk * 2);
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
            if (Combat.Dice.misschance(-Main.Player.spd))
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
            xpdice = 18;
            gpdice = 3;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("DustStorm");
            abilities.Add("RavenousPound");
        }
        public override void attack(out string ability_used)
        {
            int dmg = 0;
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                dmg = Math.Max((Convert.ToInt32(damage) - Main.Player.def), 0);
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
                double damage = Combat.Dice.roll(3, 2);
                dmg = Convert.ToInt32(damage);
                ability_used = "Ravenous Pound";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(-Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
    public class WesternKing: Enemy
    {
        public WesternKing()
        {
            name = "Western King";
            hp = 150 + (level / 2);
            atk = 50 + (level / 2);
            def = 35 + (level / 2);
            spd = 25 + (level / 2);
            xpdice = 100 + (level / 2);
            gpdice = 100 + (level / 2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Terminate");
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            int dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                if (atk < Main.Player.def)
                    damage = 1;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Terminate")
            {
                double damage = Combat.Dice.roll(atk, atk * 2);
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Terminate";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(-Main.Player.spd))
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
            atk = 15 + (level / 2);
            def = 10 + (level / 2);
            spd = 15 + (level / 2);
            xpdice = 75;
            gpdice = 75;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Singe");
            abilities.Add("Chomp");

        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            int dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                if (atk < Main.Player.def)
                    damage = 1;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Singe")
            {
                double damage = Combat.Dice.roll(1, atk * 2 / 3);
                Main.Player.on_fire = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Singe";
            }
            else if (Combat.DecideAttack(abilities) == "Chomp")
            {
                double damage = Combat.Dice.roll(2, atk);
                Main.Player.stunned = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(-Main.Player.spd))
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

        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            int dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                if (atk < Main.Player.def)
                    damage = 1;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Crow Call")
            {
                double damage = Combat.Dice.roll(2, atk / 3);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
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
            if (Combat.Dice.misschance(-Main.Player.spd))
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
            int dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                if (atk < Main.Player.def)
                    damage = 1;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
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
                dmg = Convert.ToInt32(damage) - Main.Player.def;
            }
            if (dmg <= 0)
                dmg = 1;
            if (Combat.Dice.misschance(-Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
}
