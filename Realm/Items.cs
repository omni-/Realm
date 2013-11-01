using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
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
    public class cardboard_armor : Item
    {
        public cardboard_armor()
        {
            name = "Cardboard Armor";
            desc = "A refrigerator box barely held together by masking tape.";
            defbuff = 1;
            tier = 0;
            slot = 3;
        }
    }
    public class cardboard_sword : Item
    {
        public cardboard_sword()
        {
            name = "Cardboard Sword";
            desc = "One of those wrapping paper tubes you hit your siblings with.";
            atkbuff = 1;
            tier = 0;
            slot = 1;
            multiplier = 1;
        }
    }
    public class cardboard_shield : Item
    {
        public cardboard_shield()
        {
            name = "Carboard Shield";
            desc = "It's just a box from costco.";
            defbuff = 1;
            tier = 0;
            slot = 2;
        }
    }
    //sammy tier
    public class wood_staff : Item
    {
        public wood_staff()
        {

            name = "Wood Staff";
            desc = "A label that reads 'Luigi(sammy)' is stuck on it. There's also blood on the hilt.";
            defbuff = 2;
            atkbuff = 2;
            spdbuff = 1;
            intlbuff = 1;
            tier = 1;
            slot = 1;
            multiplier = 1.075f;
        }
    }
    public class fmBP : Item
    {
        public fmBP()
        {
            name = "Fire Mario Backpack";
            name = "Discontinued because people used its supernatural powers for bad.";
            intlbuff = 5;
            tier = 1;
            slot = 4;
        }
    }
    public class sonictee : Item
    {
        public sonictee()
        {
            name = "Sonic T-Shirt";
            desc = "'Gotta go fast!'(Alright, guys?)";
            defbuff = 1;
            spdbuff = 4;
            tier = 1;
            slot = 3;
        }
    }
    public class slwscreen : Item
    {
        public slwscreen()
        {
            name = "Sonic Lost World Screenshots";
            desc = "This proves you can't hate on Sonic. LIKE SONIC LIKE SONIC LIKE SONIC LIKE SONIC.";
            defbuff = 4;
            tier = 1;
            slot = 3;
        }
    }
    //Bad tier
    public class wood_armor : Item
    {
        public wood_armor()
        {
            name = "Wood Armor";
            desc = "Some plywood you stole from Home Depot. It give you the Home Depot feeling. ";
            defbuff = 5;
            tier = 1;
            slot = 3;
        }
    }
    public class wood_plank : Item
    {
        public wood_plank()
        {

            name = "Wood Plank";
            desc = "A wood plank.";
            defbuff = 1;
            atkbuff = 2;
            tier = 1;
            slot = 1;
            multiplier = 1.1f;
        }
    }
    public class plastic_ring : Item
    {
        public plastic_ring()
        {
            name = "Plastic Ring";
            desc = "It's actually just a Ringpop.";
            spdbuff = 1;
            intlbuff = 1;
            tier = 0;
            slot = 4;
        }
    }
    //mediocre tier
    public class m_robes : Item
    {
        public m_robes()
        {
            name = "Apprentice Robes";
            desc = "A soft, vaguely magical cloth worn by mage trainees.";
            intlbuff = 2;
            defbuff = 3;
            tier = 2;
            slot = 3;
        }
    }
    public class m_staff : Item
    {
        public m_staff()
        {
            name = "Junior Mage Staff";
            desc = "A staff given to those who are kinda good at magic.";
            intlbuff = 4;
            atkbuff = 2;
            tier = 2;
            slot = 1;
        }
    }
    public class m_tome : Item
    {
        public m_tome()
        {
            name = "Magic for Dummies";
            desc = "\"Learn Magic in just one week!\"";
            intlbuff = 3;
            defbuff = 1;
            tier = 2;
            slot = 2;
        }
    }
    public class m_amulet : Item
    {
        public m_amulet()
        {
            name = "Magic Ring";
            desc = "A copper ring with a glowing gem on it.";
            intlbuff = 3;
            spdbuff = 3;
            tier = 2;
            slot = 4;
        }
    }
    public class iron_lance : Item
    {
        public iron_lance()
        {
            name = "Iron Lance";
            desc = "A gilded lance of iron.";
            defbuff = 1;
            atkbuff = 3;
            spdbuff = 1;
            tier = 2;
            slot = 1;
            multiplier = 1.1f;
        }
    }
    public class iron_rapier : Item
    {
        public iron_rapier()
        {
            name = "Iron Rapier";
            desc = "A well-smithed blade said to even be able to pierce thick armor if used properly.";
            atkbuff = 5;
            spdbuff = 2;
            tier = 2;
            slot = 1;
            multiplier = 1.1f;
        }
    }
    public class iron_mail : Item
    {
        public iron_mail()
        {
            name = "Iron Chainmail";
            desc = "A reliable suit of armor that sacrifices speed for defense.";
            defbuff = 5;
            spdbuff = -1;
            tier = 2;
            slot = 3;
        }
    }
    public class iron_buckler : Item
    {
        public iron_buckler()
        {

            name = "Iron Buckler";
            desc = "A lightweight buckler meant for all professions.";
            defbuff = 4;
            tier = 2;
            slot = 2;
        }
    }
    public class iron_band : Item
    {
        public iron_band()
        {

            name = "Iron Band";
            desc = "A bland iron band with inscriptions on the inside.";
            spdbuff = 2;
            intlbuff = 5;
            tier = 2;
            slot = 4;
            multiplier = 1.05f;
        }
    }
    //ok tier
    public class bt_longsword : Item
    {
        public bt_longsword()
        {
            name = "Bloodthirsty Longsword";
            desc = "A longsword that has rusted from all of the blood that it has drawn from its prey. It is unknown as to why the rust is crimson.";
            atkbuff = 12;
            slot = 1;
            tier = 3;
            multiplier = 1.3f;
        }
    }
    public class bt_battleaxe : Item
    {
        public bt_battleaxe()
        {
            name = "Bloodthirsty Battleaxe";
            desc = "This weapon was once used on its creator. There is an eerie aura emanating from it. ";
            atkbuff = 10;
            defbuff = 3;
            spdbuff = -1;
            slot = 1;
            tier = 3;
            multiplier = 1.25f;
        }
    }
    public class bt_greatsword : Item
    {
        public bt_greatsword()
        {
            name = "Bloodthirsty Greatsword";
            desc = "An uncommonly large greatsword  traditionally used by the ancients. ";
            atkbuff = 13;
            slot = 1;
            tier = 3;
            multiplier = 1.31f;
        }
    }
    public class bt_plate : Item
    {
        public bt_plate()
        {
            name = "Bloodmail";
            desc = "Chainmail that gained magic protection from the blood of divine animals.";
            atkbuff = 1;
            defbuff = 10;
            tier = 3;
            slot = 3;
        }
    }
    public class blood_amulet : Item
    {
        public blood_amulet()
        {
            name = "Blood Amulet";
            desc = "A red amulet. It is said that the previous owner died from unknown causes.";
            intlbuff = 10;
            spdbuff = 1;
            atkbuff = 1;
            tier = 3;
            slot = 4;
        }
    }
    public class swifites : Item
    {
        public swifites()
        {
            name = "Icy Boots of Fast";
            desc = "Although offering little protection, they make you go fast.";
            spdbuff = 10;
            defbuff = 3;
            tier = 4;
            slot = 3;
        }
    }
    public class ice_amulet : Item
    {
        public ice_amulet()
        {
            name = "Amulet of Ice";
            desc = "Makes the slow fast, the fast Sonic.";
            spdbuff = 8;
            intlbuff = 2;
            defbuff = 2;
            slot = 4;
            tier = 4;
        }
    }
    public class ice_dagger : Item
    {
        public ice_dagger()
        {
            name = "Ice Dagger";
            desc = "The user almost seems to teleport, they go so fast.";
            spdbuff = 6;
            atkbuff = 6; 
            slot = 1;
            tier = 4;
        }
    }
    public class ice_shield : Item
    {
        public ice_shield()
        {
            name = "Ice Shield";
            desc = "A shield of ice. Strangely, it weighs nothing, and you feel like you're floating while holding it.";
            spdbuff = 6;
            defbuff = 6;
            slot = 2;
            tier = 4;
        }
    }
    public class p_shield : Item
    {
        public p_shield()
        {
            name = "Palladium Shield";
            desc = "A shield inscribed with the mark of the protectorate that guards this world.";
            defbuff = 11;
            spdbuff = -1;
            tier = 4;
            slot = 3;
        }
    }
    public class p_shortsword : Item
    {
        public p_shortsword()
        {
            name = "Palladium Shortsword";
            desc = "This blade is only meant to be used for the sake of protecting others.";
            defbuff = 1;
            atkbuff = 10;
            spdbuff = 2;
            slot = 1;
            tier = 4;
            multiplier = 1.25f;
        }
    }
    public class p_mail : Item
    {
        public p_mail()
        {
            name = "Palladium Mail";
            desc = "It is said that Palladium Mail is made by sacred iron that originates from the sky.";
            defbuff = 13;
            spdbuff = -1;
            tier = 4;
            slot = 3;
        }
    }
    public class goldcloth_cloak : Item
    {
        public goldcloth_cloak()
        {
            name = "Goldcloth Cloak";
            desc = "The cloth worn by the hero king who united the realm in the name of peace.";
            intlbuff = 13;
            defbuff = 2;
            tier = 4;
            slot = 4;
        }
    }
    public class a_mail : Item
    {
        public a_mail()
        {
            name = "Azurite Cloth";
            desc = "Each thread shines with the deep hue of the ocean. Aviran make. Worn by mostly mages";
            intlbuff = 12;
            defbuff = 3;
            tier = 5;
            slot = 3;
        }
    }
    public class a_staff : Item
    {
        public a_staff()
        {
            name = "Azurite Staff";
            desc = "An oakwood staff with a massive azurite gem crowning it. It gleams regardless of light level.";
            intlbuff = 10;
            atkbuff = 5;
            tier = 5;
            slot = 1;
        }
    }
    public class tome : Item
    {
        public void tomeability()
        {
            if (!Main.Player.abilities.commands.ContainsKey('p'))
                Main.Player.abilities.AddCommand(new Combat.ForcePulse("Force Pulse", 'f'));
        }
        public tome()
        {
            tomeability();
            name = "Dusty Tome";
            desc = "An old tome with an azurite gem adorning the cover. It glows with magical power. You can also block punches with it, you suppose.";
            intlbuff = 10;
            defbuff = 5;
            tier = 5;
            slot = 2;
        }
    }
    public class a_amulet : Item
    {
        public a_amulet()
        {
            name = "Azurite Amulet";
            desc = "An amulet of azurite, pulsating with blue light.";
            intlbuff = 15;
            tier = 5;
            slot = 4;
        }
    }
    //excellent tier
    public class ds_amulet : Item
    {
        public ds_amulet()
        {
            name = "Darksteel Amulet";
            desc = " An indestructible amulet that was worn by the protectorate when he salvaged the realm from chaos. It reads ' Do not cast the shadow away from light'.";
            defbuff = 7;
            intlbuff = 15;
            tier = 5;
            slot = 4;
        }
    }
    public class ds_kite : Item
    {
        public ds_kite()
        {
            name = "Darksteel Kite Shield";
            desc = "A shield created from the protectorate's shadow.";
            defbuff = 20;
            spdbuff = -2;
            tier = 5;
            slot = 3;
        }
    }
    public class ds_kris : Item
    {
        public ds_kris()
        {
            name = "Darksteel Kris";
            desc = "A dagger that was forged by the protectorate's soul.";
            defbuff = 1;
            spdbuff = 2;
            atkbuff = 10;
            slot = 1;
            tier = 5;
            multiplier = 1.35f;
        }
    }
    public class ds_scale : Item
    {
        public ds_scale()
        {
            name = "Darksteel Scalemail";
            desc = "It came to being when the protectorate casted off its darkness.";
            defbuff = 25;
            tier = 5;
            slot = 3;
        }
    }
    public class sb_saber : Item
    {
        public sb_saber()
        {
            name = "Sunburst Saber";
            desc = "Color where there is none. Light in the darkness.";
            atkbuff = 25;
            spdbuff = 10;
            slot = 1;
            tier = 5;
        }
    }
    public class sb_chain : Item
    {
        public sb_chain()
        {
            name = "Sunburst Ringmail";
            desc = "It gleams all the colors of the rainbow.";
            defbuff = 10;
            atkbuff = 18;
            tier = 5;
            slot = 3;
        }
    }
    public class sb_gauntlet : Item
    {
        public sb_gauntlet()
        {
            name = "Sunburst Gauntlet";
            desc = "a beautiful artifact made for blocking and punching with the left hand.";
            atkbuff = 12;
            defbuff = 8;
            intlbuff = 10;
            slot = 4;
            tier = 5;
        }
    }
    public class sb_shield : Item
    {
        public sb_shield()
        {
            name = "Sunburst Shield";
            desc = "A shield that almost looks like it's meant for attacking.";
            atkbuff = 15;
            defbuff = 20;
            slot = 2;
            tier = 5;
        }
    }
    //god tier
    public class phantasmal_claymore : Item
    {
        public phantasmal_claymore()
        {
            name = "Phantasmal Claymore";
            desc = "The claymore is made from the material used to create the realm.";
            defbuff = 10;
            atkbuff = 50;
            spdbuff = 25;
            intlbuff = 10;
            tier = 6;
            slot = 1;
            multiplier = 3.5f;
        }
    }
    public class spectral_bulwark : Item
    {
        public spectral_bulwark()
        {
            name = "Spectral Bulwark";
            desc = "A shield forged in the flames of the souls of the damned.";
            defbuff = 50;
            atkbuff = 10;
            spdbuff = 10;
            intlbuff = 10;
            tier = 6;
            slot = 2;
        }
    }
    public class illusory_plate : Item
    {
        public illusory_plate()
        {
            name = "Illusory Plate";
            desc = "Even though you know it to be an illusion, every attack against it is futile.";
            defbuff = 100;
            atkbuff = 0;
            tier = 6;
            slot = 3;
        }
    }
    public class void_cloak : Item
    {
        public void_cloak()
        {
            name = "Void Cloak";
            desc = "Its existence is an oxymoron. It is made from nothing.";
            atkbuff = 0;
            spdbuff = 50;
            intlbuff = 50;
            tier = 6;
            slot = 4;
        }
    }
}
