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

        {new Place(), new Place(), new Seaport(), new Place(), new IllusionForest(), new Place(), new WKingdom()},

        {new Place(), new Valleyburg(), new Place(), new Place(), new Place(), new Riverwell(), new Place()},
        
        {new SKingdom(), new Place(), new MagicCity(), new Place(), new Place(), new Place(), new NKingdom()},
 
        {new Place(), new Place(), new Place(), new CentralKingdom(), new Place(), new NMtns(), new Place()},
        
        {new FrozenFjords(), new Place(), new TwinPaths(), new Place(), new Newport(), new Place(), new Place()},
         
        {new Place(), new Ravenkeep(), new Place(), new EKingdom(), new Place(), new Nomad(), new Coaltown()}

        };

        public static Point PlayerPosition;
    }
}
