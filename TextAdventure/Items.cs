using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Items
    {
       public Items()
       {
           Globals.cardboard_armor.desc = "A shitty refrigerator box barely held together by masking tape.";
           Globals.cardboard_armor.defbuff = 1;
           Globals.cardboard_armor.atkbuff = 0;
           Globals.cardboard_armor.spdbuff = 0;
           Globals.cardboard_armor.intlbuff = 0;
           Globals.cardboard_armor.tier = 0;
           Globals.cardboard_armor.slot = 3;

           Globals.cardboard_sword.desc = "One of those wrapping paper tubes you hit your siblings with.";
           Globals.cardboard_sword.defbuff = 0;
           Globals.cardboard_sword.atkbuff = 1;
           Globals.cardboard_sword.spdbuff = 0;
           Globals.cardboard_sword.intlbuff = 0;
           Globals.cardboard_sword.tier = 0;
           Globals.cardboard_sword.slot = 1;

           Globals.cardboard_shield.desc = "It's just a box from costco.";
           Globals.cardboard_shield.defbuff = 1;
           Globals.cardboard_shield.atkbuff = 0;
           Globals.cardboard_shield.spdbuff = 0;
           Globals.cardboard_shield.intlbuff = 0;
           Globals.cardboard_shield.tier = 0;
           Globals.cardboard_shield.slot = 2;

           Globals.wood_armor.desc = "Some plywood you stole from Home Depot. It give you the Home Depot feeling. ";
           Globals.wood_armor.defbuff = 5;
           Globals.wood_armor.atkbuff = 0;
           Globals.wood_armor.spdbuff = 0;
           Globals.wood_armor.intlbuff = 0;
           Globals.wood_armor.tier = 1;
           Globals.wood_armor.slot = 3;

           Globals.wood_staff.desc = "A label that reads 'S̶a̶m̶m̶y̶ Luigi' is stuck on it. There's also blood on the hilt.";
           Globals.wood_staff.defbuff = 4;
           Globals.wood_staff.atkbuff = 2;
           Globals.wood_staff.spdbuff = 1;
           Globals.wood_staff.intlbuff = 0;
           Globals.wood_staff.tier = 1;
           Globals.wood_staff.slot = 1;

           Globals.wood_plank.desc = "A wood plank.";
           Globals.wood_plank.defbuff = 3;
           Globals.wood_plank.atkbuff = 0;
           Globals.wood_plank.spdbuff = 0;
           Globals.wood_plank.intlbuff = 0;
           Globals.wood_plank.tier = 1;
           Globals.wood_plank.slot = 1;
       }
    }
}
