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
        public List<string> abilities;
        
        public virtual void attack()
        {
            return;
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
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("SuperSlimySlam");
        }
        public override void attack()
        {
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Combat.Dice.roll(1, atk);
                Main.Player.hp -= (Convert.ToInt32(damage) - Main.Player.def);
            }
            else if (Combat.DecideAttack(abilities) == "SuperSlimySlam")
            {
                double damage = Combat.Dice.roll(atk, atk * 2);
                Main.Player.hp -= (Convert.ToInt32(damage) - Main.Player.def);
            }
        }
    }
}
