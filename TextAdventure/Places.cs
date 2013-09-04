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
            List<char> templist = new List<char>();
            if (Globals.PlayerPosition.y > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('e');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('n');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('s');

            return templist.ToArray<char>();
        }

        private bool IsValid(char cmd)
        {
            bool IsFound = false;
            char[] cmdlist = getAvailableCommands();
            foreach (char c in cmdlist)
            {
                IsFound = c == cmd;
                if (IsFound)
                    break;
            }
            return IsFound;
        }

        public virtual bool handleInput(char input)
        {
            return IsValid(input);
        }
    }

    public class WKingdom : Place
    {
        public WKingdom()
        {
            Console.Clear();
            Description = "\n King: 'Come, my champion, for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite. \n The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight or Run?";
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
                    Console.WriteLine("\n The King gives you swift respite. He decapitates your sorry ass");
                    End.IsDead = true;
                    End.GameOver();
                    break;
                case 'r':
                    Console.WriteLine("\n You have lived.");
                    Globals.PlayerPosition.y = 0;
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

        public IllusionForest()
        {
            Description = "\n You find yourself in a forest, the bizarrely appears to wrap itself around you, like a fun house mirror. You are more lost than that time you were on a road trip and your phone died so you had no GPS." + "\n"
                + "Do you want to travel north, west, or search the area?";           
        }

        public override char[] getAvailableCommands()
        {
            List<char> tempcmdlist = new List<char>();
            tempcmdlist.Concat(base.getAvailableCommands());
            tempcmdlist.Add('s');
            return tempcmdlist.ToArray<char>();
        }

        public override bool handleInput(char input)
        {
            switch (input)
            {
                case 'n':
                    Globals.PlayerPosition.x += 1;
                    break;
                case 'e':
                    Globals.PlayerPosition.y += 1;
                    break;
                case 's':
                    Console.WriteLine("\n You decide to look around. You find a trail leading to a clearing. Once in the clearing, you see a suit of cardboard armor held together with duct tape, a refigerator box for a shield, and a cardboad tube for a sword. Pick them up? \n Your current commands are y, n");
                    char tempinput = Console.ReadKey().KeyChar;
                    switch (tempinput)
                    {
                        case 'y':
                            //add it to the backpack
                            break;
                        case 'n':
                            Console.WriteLine("\n Loser.");
                            break;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
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
