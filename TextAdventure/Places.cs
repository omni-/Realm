using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Place
    {
        public int q;
        public Globals globals = new Globals();
        public List<Enemy> enemylist = new List<Enemy>();

        protected virtual string GetDesc()
        {
            return "You are smack-dab in the middle nowhere.";
        }
        public string Description
        {
            get { return GetDesc(); }
        }

        public virtual List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            //templist.Add(new Goblin());
            return templist;
        }
        public virtual char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(1))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(0))
                templist.Add('n');
            if (Main.Player.backpack.Count > 0)
                templist.Add('b');
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
                case 'b':
                    Main.BackpackLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class WKingdom : Place
    {
        protected override string GetDesc()
        {
            return "King: 'Come, " + Main.Player.name + ", for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite.The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight(f) or run(r)?";
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
                    Formatting.type("The King gives you swift respite. He decapitates your sorry ass");
                    End.IsDead = true;
                    End.GameOver();
                    break;
                case 'r':
                    Formatting.type("You have lived.");
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
        protected override string GetDesc()
        {
            return "You find yourself in a forest, that bizarrely appears to wrap itself around you, like a fun house mirror. You are more lost than that time you were on a road trip and your phone died so you had no GPS. Do you want to travel east(e), north(n), backpack(b), or search the area(z)?";
        }

        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            templist.Add('e');
            templist.Add('n');
            templist.Add('z');
            return templist.ToArray<char>();
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
                    if (Main.forrestcounter == 0)
                    {
                        Formatting.type("You decide to look around. You find a trail leading to a clearing. Once in the  clearing, you see a suit of cardboard armor held together with duct tape, a refrigerator box, and a cardboad tube. Pick them up? Your current commands are y, n");
                        Formatting.type("");
                        char tempinput = Console.ReadKey().KeyChar;
                        switch (tempinput)
                        {
                            case 'y':
                                Main.Player.backpack.Add(globals.cardboard_armor);
                                Main.Player.backpack.Add(globals.cardboard_sword);
                                Main.Player.backpack.Add(globals.cardboard_shield);
                                Main.forrestcounter++;
                                break;
                            case 'n':
                                Formatting.type("\r\nLoser.");
                                break;
                        }
                    }
                    else
                    {
                        Formatting.type("You've already been here!");
                    }
                    break;
                case 'b':
                    Main.BackpackLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class Riverwell : Place
    {
        protected override string GetDesc()
        {
            return "You come across a river town centered around a massive well full of magically enriched electrolyte water. And the good stuff. Do you want to go north(n), south(s), east(e), west(w), backpack(b), or visit the town(v)";
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            templist.Add('n');
            templist.Add('e');
            templist.Add('w');
            templist.Add('s');
            templist.Add('v');
            return templist.ToArray<char>();
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
                case 'w':
                    Globals.PlayerPosition.x -= 1;
                    break;
                case 's':
                    Globals.PlayerPosition.y -= 1;
                    break;
                case 'v':
                    Formatting.type("There is an inn(i) and an arms dealer(a). Or you can investigate the well(w).");
                    Formatting.type("");
                    char tempinput = Console.ReadKey().KeyChar;
                    switch (tempinput)
                    {
                        case 'i':
                            Formatting.type("Innkeep: \"It will cost you 3 gold. Are you sure?\"(y/n)");
                            char _tempinput = Console.ReadKey().KeyChar;
                            switch (_tempinput)
                            {
                                case 'y':
                                    if (Main.Purchase(3))
                                    {
                                        Formatting.type("Your health has been fully restored, although you suspect you have lice.");
                                        Main.Player.hp = Main.Player.maxhp;
                                    }
                                    break;
                                case 'n':
                                    Formatting.type("You leave the inn.");
                                    break;
                            }
                            break;
                        case 'a':
                            Formatting.type("You visit the arms dealer. He has naught to sell but a wooden staff. Buy for 10 gold? (y/n)");
                            char __tempinput = Console.ReadKey().KeyChar;
                            switch (__tempinput)
                            {
                                case 'y':
                                    if (Main.Purchase(10, globals.wood_staff))
                                        Formatting.type("You buy the staff. He grins, and you know you've been ripped off.");
                                    break;
                                case 'n':
                                    Formatting.type("You leave the inn.");
                                    break;
                            }
                            break;
                        case 'w':
                            Formatting.type("Do you want to look inside the well?(y/n)");
                            char ___tempinput = Console.ReadKey().KeyChar;
                            switch (___tempinput)
                            {
                                case 'y':
                                    Formatting.type("You fall in and drown.");
                                    End.IsDead = true;
                                    End.GameOver();
                                    break;
                                case 'n':
                                    Formatting.type("You leave.");
                                    break;
                            }
                            break;
                    }
                    break;
                case 'b':
                    Main.BackpackLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class Seaport : Place
    {
        protected override string GetDesc()
        {
            return "You arrive at a seaside port bustling with couriers and merchants. Do you want to go to the arms dealer(a), the library(r), or inn(i)?";
        }
        public override List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            templist.Add(new Goblin());
            return templist;
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
                case 's':
                    Globals.PlayerPosition.y -= 1;
                    break;
                case 'i':
                    Formatting.type("Innkeep: \"It will cost you 5 gold. Are you sure?\"(y/n)");
                    char _tempinput = Console.ReadKey().KeyChar;
                    switch (_tempinput)
                    {
                        case 'y':
                            if (Main.Purchase(3))
                            {
                                Formatting.type("Your health has been fully restored, although the matress was as hard as stale crackers and your back kinda hurts.");
                                Main.Player.hp = Main.Player.maxhp;
                            }
                            break;
                        case 'n':
                            Formatting.type("You leave the inn.");
                            break;
                    }
                    break;
                case 'a':
                    Formatting.type("You visit the arms dealer. He's out of stock except for an iron lance Buy for 15 gold? (y/n)");
                    char __tempinput = Console.ReadKey().KeyChar;
                    switch (__tempinput)
                    {
                        case 'y':
                            if (Main.Purchase(15, globals.wood_staff))
                                Formatting.type("It's almost as if he doesn't even see you.");
                            break;
                        case 'n':
                            Formatting.type("You leave.");
                            break;
                        default:
                            break;
                    }
                    break;
                case 'l':
                    if (Main.libcounter == 0)
                    {
                        Formatting.type("You see a massive building with columns the size of a house. This is obivously the town's main attraction. Nerds are streaming in and out like a river. You try to go inside, but you're stopped at the door. The enterance fee is 3 g. Pay? (y/n)");
                        char ___tempinput = Console.ReadKey().KeyChar;
                        switch (___tempinput)
                        {
                            case 'y':
                                if (Main.Purchase(3))
                                {
                                    Formatting.type("You enter the library. Before you lays a vast emporium of knowledge. You scratch your butt, then look for some comic books or something. 3 books catch your eye. 'The Wizard's Lexicon'(a), 'The Warrior's Code'(b), and 'Codex Pallatinus'(c). Which do you read?");
                                    switch (Console.ReadKey().KeyChar)
                                    {
                                        case 'a':
                                            Formatting.type("You read of the maegi of old. As you flip through, something catches your eye. You see what looks to be ancient writing, but you somehow understand it.");
                                            Main.Player.abilities.AddCommand(new Combat.EnergyOverload("Energy Overload", 'e'));
                                            Formatting.type("Learned 'Energy Overload!'");
                                            break;
                                        case 'b':
                                            Formatting.type("You pore over the pages, and see a diagram of an ancient technique, lost to the ages.");
                                            Main.Player.abilities.AddCommand(new Combat.BladeDash("Blade Dash", 'd'));
                                            Formatting.type("Learned 'Blade Dash'!");
                                            break;
                                        case 'c':
                                            Formatting.type("You squint your eyes to see the tiny text. This tome convinces you of the existence of Lord Luxfert, the Bringer of Light. The book teaches you the importance of protecting others.");
                                            Main.Player.abilities.AddCommand(new Combat.HolySmite("Holy Smite", 'h'));
                                            break;
                                    }
                                }
                                break;
                            case 'n':
                                Formatting.type("You leave.");
                                break;
                        }
                        Main.libcounter++;
                    }
                    else
                        Formatting.type("The library is closed.");
                    break;
                case 'b':
                    Main.BackpackLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }
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
