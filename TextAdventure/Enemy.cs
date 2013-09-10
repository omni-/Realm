using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Enemy
    {
        public string name;
        public int hp;
        public int atk;
        public int def;
        public int spd;
        public int xp;
        public int gpdice;
        public List<string> abilities;
        
        public virtual void attack(out string ability_used)
        {
            ability_used = "";
            return;
        }
        public virtual void droploot()
        {
            int gold = Combat.Dice.roll(gpdice, 4);
            Formatting.type("You gain " + gold + " gold.");
            Main.Player.g += gold;
        }
    }

    public class Slime : Enemy
    {
        public Slime()
        {
            name = "Slime";
            hp = 5;
            atk = 2;
            def = 0;
            spd = 0;
            xp = 5;
            gpdice = 1;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("SuperSlimySlam");
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                if (atk < Main.Player.def)
                    damage = 1;
                Main.Player.hp -= (Convert.ToInt32(damage) - Main.Player.def);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "SuperSlimySlam")
            {
                double damage = Combat.Dice.roll(atk, atk * 2);
                Main.Player.hp -= (Convert.ToInt32(damage) - Main.Player.def);
                ability_used = "Super Slimy Slam";
            }
        }
    }
    public class Goblin : Enemy
    {
        public Goblin()
        {
            name = "Goblin";
            hp = 8;
            atk = 3;
            def = 1;
            spd = 1;
            xp = 10;
            gpdice = 3;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Impale");
            abilities.Add("CrazedSlashes");
        }
        public override void attack(out string ability_used)
        {
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                Main.Player.hp -= (Convert.ToInt32(damage) - Main.Player.def);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Impale")
            {
                double damage = Combat.Dice.roll(atk, atk * 2);
                Main.Player.hp -= Convert.ToInt32(damage);
                ability_used = "Impale";
            }
            else if (Combat.DecideAttack(abilities) == "CrazedSlashes")
            {
                double damage = Combat.Dice.roll(1, atk);
                damage *= Combat.Dice.roll(1, 5);
                Main.Player.hp -= Convert.ToInt32(damage);
                ability_used = "Crazed Slashes";
            }
        }
    }
}
