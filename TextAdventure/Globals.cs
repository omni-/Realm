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
    public class Item
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
        public Item()
        {
            atkbuff = 0;
            defbuff = 0;
            spdbuff = 0;
            intlbuff = 0;
            multiplier = 1;
        }
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
        public Item wood_staff = new Item();
        public Item fmBP = new Item();
        public Item sonictee = new Item();
        public Item slwscreen = new Item();

        //Bad tier
        public Item wood_armor = new Item();
        public Item wood_plank = new Item();
        public Item plastic_ring = new Item();

        //mediocre tier
        public Item iron_lance = new Item();
        public Item iron_rapier = new Item();
        public Item iron_mail = new Item();
        public Item iron_buckler = new Item();
        public Item iron_band = new Item();

        //ok tier
        public Item bt_longsword = new Item();
        public Item bt_battleaxe = new Item();
        public Item bt_greatsword = new Item();
        public Item bt_plate = new Item();
        public Item blood_amulet = new Item();

        //good tier
        public Item p_shield = new Item();
        public Item p_shortsword = new Item();
        public Item p_mail = new Item();
        public Item goldcloth_cloak = new Item();

        //excellent tier
        public Item ds_amulet = new Item();
        public Item ds_kite = new Item();
        public Item ds_kris = new Item();
        public Item ds_scale = new Item();

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
            cardboard_armor.tier = 0;
            cardboard_armor.slot = 3;

            cardboard_sword.name = "Cardboard Sword";
            cardboard_sword.desc = "One of those wrapping paper tubes you hit your siblings with.";
            cardboard_sword.atkbuff = 1;
            cardboard_sword.tier = 0;
            cardboard_sword.slot = 1;
            cardboard_sword.multiplier = .5f;

            cardboard_shield.name = "Carboard Shield";
            cardboard_shield.desc = "It's just a box from costco.";
            cardboard_shield.defbuff = 1;
            cardboard_shield.tier = 0;
            cardboard_shield.slot = 2;

            plastic_ring.name = "Plastic Ring";
            plastic_ring.desc = "It's actually just a Ringpop.";
            plastic_ring.spdbuff = 1;
            plastic_ring.intlbuff = 1;
            plastic_ring.tier = 0;
            plastic_ring.slot = 4;

            wood_armor.name = "Wood Armor";
            wood_armor.desc = "Some plywood you stole from Home Depot. It give you the Home Depot feeling. ";
            wood_armor.defbuff = 5;
            wood_armor.tier = 1;
            wood_armor.slot = 3;

            wood_plank.name = "Wood Plank";
            wood_plank.desc = "A wood plank.";
            wood_plank.defbuff = 1;
            wood_plank.atkbuff = 2;
            wood_plank.tier = 1;
            wood_plank.slot = 1;
            wood_plank.multiplier = 1.1f;

            wood_staff.name = "Wood Staff";
            wood_staff.desc = "A label that reads 'Luigi(sammy)' is stuck on it. There's also blood on the hilt.";
            wood_staff.defbuff = 2;
            wood_staff.atkbuff = 2;
            wood_staff.spdbuff = 1;
            wood_staff.intlbuff = 1;
            wood_staff.tier = 1;
            wood_staff.slot = 1;
            wood_staff.multiplier = 1.075f;

            sonictee.name = "Sonic T-Shirt";
            sonictee.desc = "'Gotta go fast!'";
            sonictee.defbuff = 1;
            sonictee.spdbuff = 4;
            sonictee.tier = 1;
            sonictee.slot = 3;

            fmBP.name = "Fire Mario Backpack";
            fmBP.name = "Discontinued because people used its supernatural powers for bad.";
            fmBP.intlbuff = 5;
            fmBP.tier = 1;
            fmBP.slot = 4;

            slwscreen.name = "Sonic Lost World Screenshots";
            slwscreen.desc = "This proves you can't hate on Sonic";
            slwscreen.defbuff = 4;
            slwscreen.tier = 1;
            slwscreen.slot = 3;

            iron_lance.name = "Iron Lance";
            iron_lance.desc = "A lance of iron, gilded in gold";
            iron_lance.defbuff = 1;
            iron_lance.atkbuff = 3;
            iron_lance.spdbuff = 1;
            iron_lance.tier = 2;
            iron_lance.slot = 1;
            iron_lance.multiplier = 1.1f;

            iron_rapier.name = "Iron Rapier";
            iron_rapier.desc = "A well-smithed blade, edged to split hair.";
            iron_rapier.atkbuff = 5;
            iron_rapier.spdbuff = 2;
            iron_rapier.tier = 2;
            iron_rapier.slot = 1;
            iron_rapier.multiplier = 1.1f;

            iron_mail.name = "Iron Chainmail";
            iron_mail.desc = "Iron ringmail. Sturdy, but not invincible. You are slower while wearing it.";
            iron_mail.defbuff = 5;
            iron_mail.spdbuff = -1;
            iron_mail.tier = 2;
            iron_mail.slot = 3;

            iron_buckler.name = "Iron Buckler";
            iron_buckler.desc = "A lightweight buckler. Will stop fire and swords, but not Unstoppable Forces.";
            iron_buckler.defbuff = 3;
            iron_buckler.tier = 2;
            iron_buckler.slot = 2;

            iron_band.name = "Iron Band";
            iron_band.desc = "An iron ring. Ugly, but it appears to be magical.";
            iron_band.spdbuff = 2;
            iron_band.intlbuff = 5;
            iron_band.tier = 2;
            iron_band.slot = 4;
            iron_band.multiplier = 1.05f;

            bt_longsword.name = "Bloodthirsty Longsword";
            bt_longsword.desc = "It drinks the blood of enemies";
            bt_longsword.atkbuff = 12;
            bt_longsword.slot = 1;
            bt_longsword.multiplier = 1.3f;

            bt_battleaxe.name = "Bloodthirsty Battleaxe";
            bt_battleaxe.desc = "It hungers for flesh";
            bt_battleaxe.atkbuff = 10;
            bt_battleaxe.defbuff = 3;
            bt_battleaxe.spdbuff = -1;
            bt_battleaxe.slot = 1;
            bt_battleaxe.multiplier = 1.25f;

            bt_greatsword.name = "Bloodthirsty Greatsword";
            bt_greatsword.desc = "A massive blade with a crimson sheen";
            bt_greatsword.atkbuff = 13;
            bt_greatsword.slot = 1;
            bt_greatsword.multiplier = 1.31f;

            bt_plate.name = "Bloodmail";
            bt_plate.desc = "Thrives off of souls of the fallen.";
            bt_plate.atkbuff = 1;
            bt_plate.defbuff = 10;
            bt_plate.slot = 3;

            blood_amulet.name = "Blood Amulet";
            blood_amulet.desc = "It has a sinister sheen to it";
            blood_amulet.intlbuff = 10;
            blood_amulet.spdbuff = 1;
            blood_amulet.atkbuff = 1;
            blood_amulet.slot = 4;

            p_shield.name = "Palladium Shield";
            p_shield.desc = "";
            p_shield.defbuff = 11;
            p_shield.spdbuff = -1;
            p_shield.slot = 3;

            p_shortsword.name = "Palladium Shortsword";
            p_shortsword.desc = "";
            p_shortsword.defbuff = 1;
            p_shortsword.atkbuff = 10;
            p_shortsword.spdbuff = 2;
            p_shortsword.slot = 1;
            p_shortsword.multiplier = 1.25f;

            p_mail.name = "Palladium Mail";
            p_mail.desc = "";
            p_mail.defbuff = 13;
            p_mail.spdbuff = -1;
            p_mail.slot = 3;

            goldcloth_cloak.name = "Goldcloth Cloak";
            goldcloth_cloak.desc = "";
            goldcloth_cloak.intlbuff = 13;
            goldcloth_cloak.defbuff = 2;
            goldcloth_cloak.slot = 4;

            ds_amulet.name = "Darksteel Amulet";
            ds_amulet.desc = "";
            ds_amulet.defbuff = 7;
            ds_amulet.intlbuff = 15;
            ds_amulet.slot = 4;

            ds_kite.name = "Darksteel Kite Shield";
            ds_kite.desc = "";
            ds_kite.defbuff = 20;
            ds_kite.spdbuff = -2;
            ds_kite.slot = 3;

            ds_kris.name = "Darksteel Kris";
            ds_kris.desc = "";
            ds_kris.defbuff = 1;
            ds_kris.spdbuff = 2;
            ds_kris.atkbuff = 10;
            ds_kris.slot = 1;
            ds_kris.multiplier = 1.35f;

            ds_scale.name = "Darksteel Scalemail";
            ds_scale.desc = "";
            ds_scale.defbuff = 25;
            ds_scale.slot = 3;

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

            illusory_plate.name = "Illusory Plate";
            illusory_plate.desc = "Even though you know it to be an illusion, every attack against it is futile.";
            illusory_plate.defbuff = 100;
            illusory_plate.atkbuff = 0;
            illusory_plate.tier = 7;
            illusory_plate.slot = 3;

            void_cloak.name = "Void Cloack";
            void_cloak.desc = "It's existence is an oxymoron. It is made from nothing.";
            void_cloak.atkbuff = 0;
            void_cloak.spdbuff = 50;
            void_cloak.intlbuff = 50;
            void_cloak.tier = 7;
            void_cloak.slot = 4;

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
            xp = 0;
            g = 15;
            intl = 1;
            backpack = new List<Item>();
            abilities = new TextAdventure.Combat.CommandTable();
            abilities.AddCommand(new Combat.BasicAttack("Basic Attack", 'b'));
        }
    }
}
