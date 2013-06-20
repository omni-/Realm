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
            Description = "\r\n King: 'Come, my champion, for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite.'\r\n\r\n" +
                "The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight or Run?";
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
                    Console.WriteLine("\r\n The King gives you swift respite. He decapitates your sorry ass");
                    End.IsDead = true;
                    End.GameOver();
                    break;
                case 'r':
                    Console.WriteLine("\r\n You have lived.");
                    Globals.PlayerPosition.y = 1;
                    Globals.PlayerPosition.x = 3;
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
