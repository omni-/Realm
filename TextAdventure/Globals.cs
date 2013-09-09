using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public struct Point
    {
        public int x;
        public int y;
    }
    public struct Item
    {
        public string name;
        public string desc;
        public int atkbuff;
        public int defbuff;
        public int spdbuff;
        public int intlbuff;
        public int tier;
        public int slot;
        public float multiplier;
        public bool in_use;
        //1 for primary 2 for secondary 3 for armor 4 for Accessory
    }
    public class Globals
    {
        public static Place[,] map = new Place[,] {

        {new Place(), new Seaport(), new WKingdom(), new IllusionForest(), new Place(), new Place()},
        {new Place(), new Place(), new Valleyburg(), new Place(), new Riverwell(), new NKingdom()},        
        {new SKingdom(), new FlamingDesert(), new CentralKingdom(), new Place(), new Place(), new Place()}, 
        {new Place(), new TwinPaths(), new Nomad(), new Newport(), new NMtns(), new Place()},                 
        {new QuestionMarkx3(), new Ravenkeep(), new EKingdom(), new Place(), new Place(), new BlackHorizon()}

        };

        public static Point PlayerPosition;

        public Item cardboard_armor = new Item();
        public Item cardboard_sword = new Item();
        public Item cardboard_shield = new Item();
        public Item wood_armor = new Item();
        public Item wood_staff = new Item();
        public Item wood_plank = new Item();

        public Globals()
        {
            cardboard_armor.name = "Cardboard Armor";
            cardboard_armor.desc = "A shitty refrigerator box barely held together by masking tape.";
            cardboard_armor.defbuff = 1;
            cardboard_armor.atkbuff = 0;
            cardboard_armor.spdbuff = 0;
            cardboard_armor.intlbuff = 0;
            cardboard_armor.tier = 0;
            cardboard_armor.slot = 3;
            cardboard_armor.multiplier = 1;

            cardboard_sword.name = "Cardboard Sword";
            cardboard_sword.desc = "One of those wrapping paper tubes you hit your siblings with.";
            cardboard_sword.defbuff = 0;
            cardboard_sword.atkbuff = 1;
            cardboard_sword.spdbuff = 0;
            cardboard_sword.intlbuff = 0;
            cardboard_sword.tier = 0;
            cardboard_sword.slot = 1;
            cardboard_sword.multiplier = .5f;

            cardboard_shield.name = "Carboard Shield";
            cardboard_shield.desc = "It's just a box from costco.";
            cardboard_shield.defbuff = 1;
            cardboard_shield.atkbuff = 0;
            cardboard_shield.spdbuff = 0;
            cardboard_shield.intlbuff = 0;
            cardboard_shield.tier = 0;
            cardboard_shield.slot = 2;
            cardboard_shield.multiplier = 1;

            wood_armor.name = "Wood Armor";
            wood_armor.desc = "Some plywood you stole from Home Depot. It give you the Home Depot feeling. ";
            wood_armor.defbuff = 5;
            wood_armor.atkbuff = 0;
            wood_armor.spdbuff = 0;
            wood_armor.intlbuff = 0;
            wood_armor.tier = 1;
            wood_armor.slot = 3;
            wood_armor.multiplier = 0;

            wood_staff.name = "Wood Staff";
            wood_staff.desc = "A label that reads 'S̶a̶m̶m̶y̶ Luigi' is stuck on it. There's also blood on the hilt.";
            wood_staff.defbuff = 4;
            wood_staff.atkbuff = 2;
            wood_staff.spdbuff = 1;
            wood_staff.intlbuff = 0;
            wood_staff.tier = 1;
            wood_staff.slot = 1;
            wood_staff.multiplier = 1.3f;

            wood_plank.name = "Wood Plank";
            wood_plank.desc = "A wood plank.";
            wood_plank.defbuff = 3;
            wood_plank.atkbuff = 0;
            wood_plank.spdbuff = 0;
            wood_plank.intlbuff = 0;
            wood_plank.tier = 1;
            wood_plank.slot = 1;
            wood_plank.multiplier = 1;
        }
    }
    public class GamePlayer
    {
        public int hp;
        public int maxhp;
        public int spd;
        public int atk;
        public int intl;
        public int def;
        public string name;
        public Item primary;
        public Item secondary;
        public Item armor;
        public Item accessory;
        public List<Item> backpack;
        public int g;
        public int level;
        public int xp;
        public TextAdventure.Combat.CommandTable abilities;
        public void levelup()
        {
            int xp_to_lvl = 10;
            if (level <= 10)
            {
                if (xp == xp_to_lvl)
                {
                    level += 1;
                    hp = maxhp;
                    xp_to_lvl *= level;
                }
            }
            else
            {
                int oldlevel = level;
                level = (int)Math.Log(xp, 1.2);
                if (level > oldlevel)
                    hp = maxhp;
            }
        }
        public GamePlayer()
        {
            hp = 10;
            level = 1;
            xp = 1;
            g = 10;
            backpack = new List<Item>();
            abilities = new TextAdventure.Combat.CommandTable();
            abilities.AddCommand(new Combat.BasicAttack("Basic Attack", '0'));
        }
    }
}
