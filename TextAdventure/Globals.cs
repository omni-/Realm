using System;
using System.Collections;
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
        public string desc;
        public int atkbuff;
        public int defbuff;
        public int spdbuff;
        public int intlbuff;
        public int tier;
        public int slot;
        //1 for primary 2 for secondary 3 for armor 4 for Accessory
    }
    public static class Globals
    {
        public static Place[,] map = new Place[,] {

        {new Place(), new Seaport(), new WKingdom(), new IllusionForest(), new Place(), new Place()},
        {new Place(), new Place(), new Valleyburg(), new Place(), new Riverwell(), new NKingdom()},        
        {new SKingdom(), new FlamingDesert(), new CentralKingdom(), new Place(), new Place(), new Place()}, 
        {new Place(), new TwinPaths(), new Nomad(), new Newport(), new NMtns(), new Place()},                 
        {new QuestionMarkx3(), new Ravenkeep(), new EKingdom(), new Place(), new Place(), new BlackHorizon()}

        };

        public static Point PlayerPosition;

        public static Item cardboard_armor;
        public static Item cardboard_sword;
        public static Item cardboard_shield;
        public static Item wood_armor;
        public static Item wood_staff;
        public static Item wood_plank;


    }
    public class Player
    {
        public int hp;
        public int spd;
        public int atk;
        public int intl;
        public int def;
        public string name = "";
        public Hashtable backpack = new Hashtable();
    }

}
