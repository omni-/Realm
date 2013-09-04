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
        public static Place[,] map = new Place[,] {{new Place(), new Place(), new SKingdom(), new Place(), new QuestionMarkx3()},
                                                   {new Seaport(), new Place(), new FlamingDesert(), new TwinPaths(), new Ravenkeep()},
                                                   {new WKingdom(), new Valleyburg(), new CentralKingdom(), new Nomad(), new EKingdom()}, 
                                                   {new IllusionForest(), new Place(), new Place(), new Newport(), new Place()},
                                                   {new Place(), new Riverwell(), new Place(), new NMtns(), new Place()},
                                                   {new Place(), new Place(), new NKingdom(), new Place(), new BlackHorizon()}};

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
