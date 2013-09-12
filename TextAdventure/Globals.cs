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

        //shit tier
        public Item cardboard_armor = new Item();
        public Item cardboard_sword = new Item();
        public Item cardboard_shield = new Item();

        //sammy tier
        public Item wood_armor = new Item();
        public Item wood_staff = new Item();
        public Item wood_plank = new Item();
        public Item plastic_ring = new Item();

        //mediocre tier
        public Item iron_lance = new Item();
        public Item iron_rapier = new Item();
        public Item iron_mail = new Item();
        public Item iron_buckler = new Item();
        public Item iron_band = new Item();

        //god tier
        public Item phantasmal_claymore = new Item();
        public Item spectral_bulwark = new Item();
        public Item illusory_plate = new Item();
        public Item void_cloak = new Item();
        public Globals()
        {
            cardboard_armor.name = "Cardboard Armor";
            cardboard_armor.desc = "A refrigerator box barely held together by masking tape.";
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

            plastic_ring.name = "Plastic Ring";
            plastic_ring.desc = "It's actually just a Ringpop.";
            plastic_ring.defbuff = 0;
            plastic_ring.atkbuff = 0;
            plastic_ring.spdbuff = 1;
            plastic_ring.intlbuff = 1;
            plastic_ring.tier = 0;
            plastic_ring.slot = 4;
            plastic_ring.multiplier = 1;

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
            wood_staff.desc = "A label that reads 'Luigi(sammy)' is stuck on it. There's also blood on the hilt.";
            wood_staff.defbuff = 2;
            wood_staff.atkbuff = 2;
            wood_staff.spdbuff = 1;
            wood_staff.intlbuff = 1;
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

            iron_lance.name = "Iron Lance";
            iron_lance.desc = "A lance of iron, gilded in gold";
            iron_lance.defbuff = 1;
            iron_lance.atkbuff = 3;
            iron_lance.spdbuff = 1;
            iron_lance.tier = 2;
            iron_lance.slot = 1;
            iron_lance.multiplier = 1.2f;

            iron_rapier.name = "Iron Rapier";
            iron_rapier.desc = "A well-smithed blade, edged to split hair.";
            iron_rapier.defbuff = 0;
            iron_rapier.atkbuff = 5;
            iron_rapier.spdbuff = 2;
            iron_rapier.tier = 2;
            iron_rapier.slot = 1;
            iron_rapier.multiplier = 1.5f;

            iron_mail.name = "Iron Chainmail";
            iron_mail.desc = "Iron ringmail. Sturdy, but not invincible. You are slower while wearing it.";
            iron_mail.defbuff = 5;
            iron_mail.atkbuff = 0;
            iron_mail.spdbuff = -1;
            iron_mail.tier = 2;
            iron_mail.slot = 3;
            iron_mail.multiplier = 1;

            iron_buckler.name = "Iron Buckler";
            iron_buckler.desc = "A lightweight buckler. Will stop fire and swords, but not Unstoppable Forces.";
            iron_buckler.defbuff = 3;
            iron_buckler.atkbuff = 0;
            iron_buckler.spdbuff = 0;
            iron_buckler.tier = 2;
            iron_buckler.slot = 2;
            iron_buckler.multiplier = 1;

            iron_band.name = "Iron Band";
            iron_band.desc = "An iron ring. Ugly, but it appears to be magical.";
            iron_band.defbuff = 0;
            iron_band.atkbuff = 0;
            iron_band.spdbuff = 2;
            iron_band.intlbuff = 5;
            iron_band.tier = 2;
            iron_band.slot = 4;
            iron_band.multiplier = 1.2f;

            phantasmal_claymore.name = "Phantasmal Claymore";
            phantasmal_claymore.desc = "A legendary blade made from the essence of reality.";
            phantasmal_claymore.defbuff = 10;
            phantasmal_claymore.atkbuff = 50;
            phantasmal_claymore.spdbuff = 25;
            phantasmal_claymore.intlbuff = 10;
            phantasmal_claymore.tier = 7;
            phantasmal_claymore.slot = 1;
            phantasmal_claymore.multiplier = 3.5f;

            spectral_bulwark.name = "Spectral Bulwark";
            spectral_bulwark.desc = "A shield forged in the flames of the souls of the damned.";
            spectral_bulwark.defbuff = 50;
            spectral_bulwark.atkbuff = 10;
            spectral_bulwark.spdbuff = 10;
            spectral_bulwark.intlbuff = 10;
            spectral_bulwark.tier = 7;
            spectral_bulwark.slot = 2;
            spectral_bulwark.multiplier = 1;

            illusory_plate.name = "Illusory Plate";
            illusory_plate.desc = "Even though you know it to be an illusion, every attack against it is futile.";
            illusory_plate.defbuff = 100;
            illusory_plate.atkbuff = 0;
            illusory_plate.spdbuff = 0;
            illusory_plate.intlbuff = 0;
            illusory_plate.tier = 7;
            illusory_plate.slot = 3;
            illusory_plate.multiplier = 1;

            void_cloak.name = "Void Cloack";
            void_cloak.desc = "It's existence is an oxymoron. It is made from nothing.";
            void_cloak.defbuff = 0;
            void_cloak.atkbuff = 0;
            void_cloak.spdbuff = 50;
            void_cloak.intlbuff = 50;
            void_cloak.tier = 7;
            void_cloak.slot = 4;
            void_cloak.multiplier = 1;
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
        public int xp_next;
        public TextAdventure.Combat.CommandTable abilities;
        public void levelup()
        {
            xp_next = level >= 30 ? 62 + (level - 30) * 7 : (level >= 15 ? 17 + (level - 15) * 3 : 17);
            if (xp >= xp_next)
            {
                hp = maxhp;
                level++;
                Formatting.type("Congratulations! You have leveled up! You are now level " + level + ".");
                xp = 0;
                xp_next = level >= 30 ? 62 + (level - 30) * 7 : (level >= 15 ? 17 + (level - 15) * 3 : 17);
                if (xp >= xp_next)
                    levelup();
            }
        }
        public void applybonus()
        {
            maxhp = 10 + (level * 2);
            def = -1 + level;
            atk = 0 + level;
            intl = 0 + level;
            spd = 0 + level;

            if (!primary.Equals(default(Item)))
            {
                def += primary.defbuff;
                atk += primary.atkbuff;
                intl += primary.intlbuff;
                spd += primary.spdbuff;
            }

            if (!secondary.Equals(default(Item)))
            {
                def += secondary.defbuff;
                atk += secondary.atkbuff;
                intl += secondary.intlbuff;
                spd += secondary.spdbuff;
            }

            if (!armor.Equals(default(Item)))
            {
                def += armor.defbuff;
                atk += armor.atkbuff;
                intl += armor.intlbuff;
                spd += armor.spdbuff;
            }
            if (!accessory.Equals(default(Item)))
            {
                def += accessory.defbuff;
                atk += accessory.atkbuff;
                intl += accessory.intlbuff;
                spd += accessory.spdbuff;
            }

        }
        public void applydevbonus()
        {
            if (!primary.Equals(default(Item)))
            {
                def += primary.defbuff;
                atk += primary.atkbuff;
                intl += primary.intlbuff;
                spd += primary.spdbuff;
            }

            if (!secondary.Equals(default(Item)))
            {
                def += secondary.defbuff;
                atk += secondary.atkbuff;
                intl += secondary.intlbuff;
                spd += secondary.spdbuff;
            }

            if (!armor.Equals(default(Item)))
            {
                def += armor.defbuff;
                atk += armor.atkbuff;
                intl += armor.intlbuff;
                spd += armor.spdbuff;
            }
            if (!accessory.Equals(default(Item)))
            {
                def += accessory.defbuff;
                atk += accessory.atkbuff;
                intl += accessory.intlbuff;
                spd += accessory.spdbuff;
            }
        }
        public GamePlayer()
        {
            maxhp = 12;
            hp = 12;
            level = 1;
            xp = 17;
            g = 15;
            backpack = new List<Item>();
            abilities = new TextAdventure.Combat.CommandTable();
            abilities.AddCommand(new Combat.BasicAttack("Basic Attack", 'b'));
        }
    }
}
