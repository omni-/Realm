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
            if (Globals.PlayerPosition.x > 0)
                templist.Add('e');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(1))
                templist.Add('w');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(0))
                templist.Add('n');

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
            switch (input)
            {
                case 'n':
                    Globals.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Globals.PlayerPosition.x += 1;
                    break;
                case 's':
                    Globals.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Globals.PlayerPosition.x -= 1;
                    break;
                default:
                    return false;
            }
            return true;
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
                    Globals.PlayerPosition.y += 1;
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
            Description = "\r\n You find yourself in a forest, the bizarrely appears to wrap itself around you, like a fun house mirror. You are more lost than that time you were on a road trip and your phone died so you had no GPS." + "\r\n"
                + "Do you want to travel east(e), north(n) or search the area(z)?";
        }

        public override char[] getAvailableCommands()
        {
            return new char[] { 'n', 'e', 'z' };
        }

        public override bool handleInput(char input)
        {
            switch (input)
            {
                case 'n':
                    Globals.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Globals.PlayerPosition.x += 1;
                    break;
                case 'z':
                    Console.WriteLine("\r\n You decide to look around. You find a trail leading to a clearing. Once in the clearing, you see a suit of cardboard armor held together with duct tape, a refigerator box, and a cardboad tube. Pick them up? \r\n Your current commands are y, n");
                    char tempinput = Console.ReadKey().KeyChar;
                    switch (tempinput)
                    {
                        case 'y':
                            Main.player.backpack.Add(0, Globals.cardboard_armor);
                            Main.player.backpack.Add(1, Globals.cardboard_sword);
                            break;
                        case 'n':
                            Console.WriteLine("\r\n Loser.");
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
        public Seaport()
        {
            Description = "\r\n You arrive at a seaside port bustling with couriers and merchants. Where do you want to go?";
        }
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
