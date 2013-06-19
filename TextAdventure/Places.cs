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
            return new char[] {};
        }

        public virtual bool handleInput(char input)
        {
s    }

    public class WKingdom: Place
    {
        WKingdom()
        {
            Description = "King: 'Come, my champion, for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite.'\r\n\r\n" +
                "The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight or Run? [f to fight, r to run]";
        }

        public virtual char[] getAvailableCommands()
        {
            return new char[] { 'f', 'r'};
        }

        public virtual bool handleInput(char input)
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

    public class IllusionForest
    {

    }

    public class Seaport
    {

    }

    public class Riverwell
    {

    }

    public class Valleyburg
    {

    }

    public class NMtns
    {

    }

    public class NKingdom
    {

    }

    public class CentralKingdom
    {

    }

    public class FlamingDesert
    {

    }

    public class SKingdom
    {

    }

    public class Newport
    {

    }

    public class BlackHorizon
    {

    }

    public class Nomad
    {

    }

    public class WKingdom
    {

    }

    public class TwinPaths
    {

    }

    public class Ravenkeep
    {

    }

    public class QuestionMarkx3
    {

    }
}
