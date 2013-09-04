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

    public static class Globals
    {
        public static Place[,] map = new Place[,] {{new Place(), new Place(), new IllusionForest(), new WKingdom(),new Seaport(), new Place()},
                                                   {new NKingdom(), new Riverwell(), new Place(), new Valleyburg(),new Place(),new Place()},
                                                   {new Place(), new Place(), new Place(), new CentralKingdom(),new FlamingDesert(),new SKingdom()},
                                                   {new Place(), new NMtns(), new Newport(), new Nomad(),new TwinPaths(), new Place()}, 
                                                   {new BlackHorizon(),new Place(),new Place(),new EKingdom(),new Ravenkeep(),new QuestionMarkx3()}};


        public static Point PlayerPosition;
    }

    public struct Item
    {
        public string Description;
        public int HPBonus;
        public int SpeedBonus;
        public int AtkBonus;
        public int IntBonus;
        public int DefBonus;
    }

    public class Player
    {
        public int hp;
        public int spd;
        public int atk;
        public int intl;
        public int def;
        public string name = "";
        public Backpack pack;
        public Player()
        {
            pack = new Backpack();
        }
    }

    public class Backpack
    {
        Hashtable Bp = new Hashtable();
    }
}
