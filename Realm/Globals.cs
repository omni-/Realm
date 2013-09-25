using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public struct Point
    {
        public int x;
        public int y;
    }
    public class Globals
    {
        public static Place[,] map = new Place[,] {

        {new Place(), new Seaport(), new Place(), new IllusionForest(), new Place(), new WKingdom()},

        {new Place(), new Place(), new Valleyburg(), new Place(), new Riverwell(), new NKingdom()},
        
        {new SKingdom(), new Nomad(), new CentralKingdom(), new Place(), new Place(), new Place()},
 
        {new Place(), new TwinPaths(), new Place(), new Newport(), new NMtns(), new Place()},
        
        //{},
         
        {new Place(), new Ravenkeep(), new EKingdom(), new Place(), new Place(), new Place()}

        };

        public static Point PlayerPosition;
    }
}
