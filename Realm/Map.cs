using System;
using System.Collections;
using System.Collections.Generic;

namespace Realm
{
    public struct Point
    {
        public int x;
        public int y;
    }
    public class Map
    {
        public static Place[,] map = new Place[,] {

        {new Place(), new Place(), new Seaport(), new Place(), new IllusionForest(), new Place(), new WKingdom()},

        {new Place(), new Valleyburg(), new Place(), new Place(), new Place(), new Riverwell(), new Place()},
        
        {new SKingdom(), new Place(), new MagicCity(), new Place(), new Place(), new Place(), new NKingdom()},
 
        {new Place(), new Place(), new Place(), new CentralKingdom(), new Place(), new NMtns(), new Place()},
        
        {new FrozenFjords(), new Place(), new TwinPaths(), new Place(), new Newport(), new Place(), new Place()},
         
        {new Place(), new Ravenkeep(), new Place(), new EKingdom(), new Place(), new Nomad(), new Coaltown()}

        };

        public static caveplace[] cavemap = new caveplace[5];
        public static List<caveplace> cavelist = new List<caveplace> { new caveplace(), new caveplace(), new crystal(), new mushrooms() };
        public static int CavePosition;
        public static Point PlayerPosition;
        public static void gencave()
        {
            Random rand = new Random();
            caveplace curr;
            for (int i = 0; i < 5; i++)
            {
                if (cavelist.Count > 1)
                    curr = cavelist[Combat.Dice.roll(1, cavelist.Count - 1)];
                else if (cavelist.Count == 1)
                    curr = cavelist[0];
                else
                    break;
                cavemap[i] = curr;
                cavelist.Remove(curr);
            }
            cavemap[4] = new dragon();
        }

        public static string GetNPCName()
        {
            List<string> names = new List<string>()
            {
                "Jean-Philippe", "Jacques-Cartier", "Bill", "Hank", "Ernie", "Selena", "Gomie", "Chambers", "Takeshi", "Clark", "Rosie", "Nick", "Carter", "Charlie", "Claude", "Ben", "Steve", "Margie", "Isabel", "Connor"
            };
            return names[Main.rand.Next(0, names.Count - 1)];
        }
    }
}
