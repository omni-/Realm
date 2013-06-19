using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{

    public class Place
    {
        public string Description;

        public virtual char[] getAvailableCommands()
        {
            return new char[] { };
        }

        public virtual bool handleInput(char input)
        {
            return false;
        }
    }

    public class WKingdom : Place
    {
        public WKingdom()
        {
            Description = "King: 'Come, my champion, for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite.'\r\n\r\n" +
                "The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight or Run? [f to fight, r to run]";
        }

        public override char[] getAvailableCommands()
        {
            return new char[] { 'f', 'r' };
        }

        public override bool handleInput(char input)
        {
            switch (input)
            {
                case 'f':
                    Console.WriteLine("The King gives you swift respite. He decapitates your sorry ass");
                    End.GameOver();
                    break;
                case 'r':
                    Console.WriteLine("You have lived.");
                    Main.MainLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class IllusionForest : Place
    {

    }

    public class Seaport : Place
    {

    }

    public class Riverwell : Place
    {

    }

    public class Valleyburg : Place
    {

    }

    public class NMtns : Place
    {

    }

    public class NKingdom : Place
    {

    }

    public class CentralKingdom : Place
    {

    }

    public class FlamingDesert : Place
    {

    }

    public class SKingdom : Place
    {

    }

    public class Newport : Place
    {

    }

    public class BlackHorizon : Place
    {

    }

    public class Nomad : Place
    {

    }

    public class EKingdom : Place
    {

    }

    public class TwinPaths : Place
    {

    }

    public class Ravenkeep : Place
    {

    }

    public class QuestionMarkx3 : Place
    {

    }
}
