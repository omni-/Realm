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
    }

    public class Slime : Enemy
    {
        public Slime()
        {
            name = "Slime";
            hp = 5;
            atk = 1;
            def = 0;
            spd = 0;
            abilities.Add("BasicAttack");
            abilities.Add("SuperSlimySlam");
        }
    }
}
