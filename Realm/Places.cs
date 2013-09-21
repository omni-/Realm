using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class Place
    {
        public Random rand;
        public int q;
        public Globals globals = new Globals();
        public List<Enemy> enemylist = new List<Enemy>();

        protected virtual string GetDesc()
        {
            return "You are smack-dab in the middle of nowhere.";
        }
        public string Description
        {
            get { return GetDesc(); }
        }
        public virtual Enemy getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            if (Main.Player.level >= 3)
                templist.Add(new Goblin());
            if (Main.Player.level >= 5)
                templist.Add(new Bandit());
            if (Main.Player.level >= 10)
                templist.Add(new Drake());
            int randint = rand.Next(1, templist.Count + 1);

            return templist[randint - 1];
        }
        public virtual char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            if (Main.Player.backpack.Count > 0)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            templist.Add('#');
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

        public virtual void handleInput(char input)
        {
            bool inputHandled = false;
            while (!inputHandled)
            {
                inputHandled = _handleInput(input);
                if (!inputHandled)
                {
                    Formatting.type("Invalid.");
                    Formatting.type("");
                    input = Console.ReadKey().KeyChar;
                }
            }
        }
        public virtual bool _handleInput(char input)
        {
            switch (input)
            {
                case 'n':
                    if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                        Globals.PlayerPosition.y += 1;
                    break;
                case 'e':
                    if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                        Globals.PlayerPosition.x += 1;
                    break;
                case 's':
                    if (Globals.PlayerPosition.y > 0)
                        Globals.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    if (Globals.PlayerPosition.x > 0)
                        Globals.PlayerPosition.x -= 1;
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Main.BackpackLoop();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
        public Place()
        {
            rand = new Random();
        }
    }

    public class WKingdom : Place
    {
        protected override string GetDesc()
        {
            if (Main.wkingdead)
                return "There's nothing here. r to leave.";
            if (Main.wkingcounter <= 1)
            {
                Main.wkingcounter++;
                return "King: 'Come, " + Main.Player.name + ". I plan to give you the ultimate gift, eternal respite. You're not sure why he has called you but you don't like it. The Western King approaches and unsheathes his blade emitting a strong aura of bloodlust. He seems to hav powers far beyond anything you can imagine. Fight(f) or run(r)?";
            }
            else
                return "Back so soon?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            if (!Main.wkingdead)
                return new char[] { 'f', 'r', '#' };
            else
                return new char[] { 'r', '#' };
        }

        public override bool _handleInput(char input)
        {
            if (!Main.wkingdead)
            {
                switch (input)
                {
                    case 'f':
                        Main.BattleLoop(new WesternKing());
                        Formatting.type("Somehow, you managed to beat the Western King. *ahem* Cheater *ahem*");
                        Main.wkingdead = true;
                        break;
                    case 'r':
                        Formatting.type("You escaped the Western King, but you're pretty damn lost now.");
                        Globals.PlayerPosition.y -= 2;
                        Main.MainLoop();
                        break;
                    case '#':
                        Save.SaveGame();
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                switch (input)
                {
                    case 'r':
                        Formatting.type("You leave.");
                        Globals.PlayerPosition.y -= 1;
                        Main.MainLoop();
                        break;
                    case '#':
                        Save.SaveGame();
                        break;
                    default:
                        return false;
                }
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
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('z');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
                case 'n':
                    Globals.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Globals.PlayerPosition.x += 1;
                    break;
                case 's':
                    Globals.PlayerPosition.y -= 1;
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
                                Formatting.type("Amid the -trash- gear on the ground, you find a tile with the letter 'G' on it.");
                                Main.Player.backpack.Add(new cardboard_armor());
                                Formatting.type("Obtained 'Cardboard Armor'!");
                                Main.Player.backpack.Add(new cardboard_sword());
                                Formatting.type("Obtained 'Cardboard Shield'!");
                                Main.Player.backpack.Add(new cardboard_shield());
                                Formatting.type("Obtained 'Cardboard Shield'!");
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
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n'); 
            templist.Add('v');
            templist.Add('#');
            return templist.ToArray<char>();
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                            Formatting.type("You visit the arms dealer. He has naught to sell but a wooden staff and a plastic ring. Buy both for 11 gold? (y/n)");
                            char __tempinput = Console.ReadKey().KeyChar;
                            switch (__tempinput)
                            {
                                case 'y':
                                    if (Main.Purchase(11, new wood_staff()))
                                    {
                                        Formatting.type("Obtained 'Wood Staff'!");
                                        Main.Player.backpack.Add(new plastic_ring());
                                        Formatting.type("Obtained 'Plastic Ring'!");
                                        Formatting.type("On the inside of the ring, the letter 'o' is embossed.");
                                        Formatting.type("You buy the staff and the ring. He grins, and you know you've been ripped off.");
                                    }
                                    break;
                                case 'n':
                                    Formatting.type("You leave the shop.");
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
            return "You arrive at a seaside port bustling with couriers and merchants. Do you want to go to the arms dealer(a), the library(l), or inn(i)?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                                Formatting.type("As you're leaving the inn, you notice the letter 'd' in the coffee grounds from the espresso you had that morning.");
                                Main.Player.hp = Main.Player.maxhp;
                            }
                            break;
                        case 'n':
                            Formatting.type("You leave the inn.");
                            break;
                    }
                    break;
                case 'a':
                    Formatting.type("You visit the arms dealer. He's out of stock except for an iron lance(l, $15) and an iron buckler(b, $10). (n to leave)");
                    char __tempinput = Console.ReadKey().KeyChar;
                    switch (__tempinput)
                    {
                        case 'l':
                            if (Main.Purchase(15, new iron_lance()))
                            {
                                Formatting.type("It's almost as if he doesn't even see you.");
                                Formatting.type("Obtained 'Iron Lance'!");
                            }
                            break;
                        case 'b':
                            if (Main.Purchase(10, new iron_buckler()))
                            {
                                Formatting.type("It's almost as if he doesn't even see you.");
                                Formatting.type("Obtained 'Iron Buckler'!");
                            }
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
                                            Formatting.type("You squint your eyes to see the tiny text. This tome convinces you of the existence of Lord Luxferre, the Bringer of Light. The book teaches you the importance of protecting others.");
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
        protected override string GetDesc()
        {
            return "You arrive at a small town just east of the Western Kingdom. Do you want to visit the town(v) or head to the inn(i)?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('i');
            templist.Add('v');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                case 'i':
                    Formatting.type("Innkeep: \"It will cost you 5 gold. Are you sure?\"(y/n)");
                    char _tempinput = Console.ReadKey().KeyChar;
                    switch (_tempinput)
                    {
                        case 'y':
                            if (Main.Purchase(3))
                            {
                                Formatting.type("Your health has been fully restored, although the matress smelled of mildew, and so do your clothes.");
                                Main.Player.hp = Main.Player.maxhp;
                            }
                            break;
                        case 'n':
                            Formatting.type("You leave the inn.");
                            break;
                    }
                    break;
                case 'v':
                    Formatting.type("You visit the town. You may choose to visit with the townsfolk(t), or head to the artificer(a).");
                    char __tempinput = Console.ReadKey().KeyChar;
                    switch (__tempinput)
                    {
                        case 'a':
                            Formatting.type("The artificer has some magically charged rings for sale. Buy one for 20? (y/n)");
                            switch (Console.ReadKey().KeyChar)
                            {
                                case 'y':
                                    if (Main.Purchase(15, new iron_band()))
                                    {
                                        Formatting.type("He smiles weakly and thanks you.");
                                        Formatting.type("Obtained 'Iron Band'!");
                                    }
                                    break;
                                case 'n':
                                    Formatting.type("You leave.");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 't':
                            Formatting.type("You talk to a villager. He muses about the fact that sometimes, reality doesn't feel real at all. Puzzled by his comment, you walk away.");
                            Formatting.type("A paper flaps out of his cloack as he walks away. On it is nothing but the letter 'w'.");
                            break;
                        default:
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

    public class NMtns : Place
    {
        protected override string GetDesc()
        {
            return "You find yourself at the foot of a mountain. There is a village not far off do you wish to go there?(y to enter)";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('y');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                case 'y':
                    Formatting.type("You see a library(l) and a weaponsmith(w). Which do you wish to enter?");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'l':
                            if (Main.ramsaycounter == 0)
                            {
                                Formatting.type("There are three books. Do you wish to read Climbing Safety (c), Solomon's Answer (s), or Gordon Ramsay: A Biology (g)");
                                switch (Console.ReadKey().KeyChar)
                                {
                                    case 'c':
                                        Formatting.type("Climbing mountains requires absolute safety. Rule #1: Don't Fall(.....this book seems pretty thick. Do you wish to continue reading?(y/n))");
                                        switch (Console.ReadKey().KeyChar)
                                        {
                                            case 'y':
                                                Formatting.type("As you silently become informed about safety, you notice that a segment of the wall is opening itself up to your far right. Do you wish to enter?(y/n)");
                                                switch (Console.ReadKey().KeyChar)
                                                {
                                                    case 'y':
                                                        Formatting.type("You try to enter, but the door requires a password.");
                                                        if (Console.ReadLine() == "Don't Fall")
                                                        {
                                                            Formatting.type(" You open the door to find a dark room with a suspicious figure conducting suspicious rituals. The man looks flustered and says 'Nice day isn't it?'. As you wonder what anyone would be doing in such a dark room, the man edges his way to the entrance and dashes outside. He forgot to take his book with him. Do you wish to take it? (y/n) ");
                                                            switch (Console.ReadKey().KeyChar)
                                                            {
                                                                case 'y':
                                                                    Main.Player.abilities.AddCommand(new Combat.ConsumeSoul("Consume Soul", 'u'));
                                                                    Formatting.type(" Learned 'Consume Soul'!");
                                                                    break;
                                                                case 'n':
                                                                    Formatting.type("You remember that you are an exemplary member of society and that you will by no means touch another's belongings without their consent. You leave the room like the good man you are.");
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Formatting.type("Password incorrect.");
                                                            break;
                                                        }
                                                        break;
                                                    case 'n':
                                                        Formatting.type("You did not see anything out of the ordinary. You have never seen anything out of the ordinary. As you leave you makes sure to shut the door behind you because you did not see anything out of the ordinary.");
                                                        break;
                                                }
                                                break;
                                            case 'n':
                                                Formatting.type("You believe that you already know enough about safety. Put the book back in it's spot in the bookshelf?(y/n)");
                                                switch (Console.ReadKey().KeyChar)
                                                {
                                                    case 'y':
                                                        Formatting.type("You insert the book back in it's righteous position. You feel good about doing a good deed.");
                                                        break;
                                                    case 'n':
                                                        Main.Player.hp -= 2;
                                                        Formatting.type("You are trash. You are the pondscum of society. Repent and pay with your life. You take 2 damage.");
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                    case 's':
                                        Formatting.type("You don't know how to read this language, however you do find a crumpled up map of the realm in the back of the book.");
                                        Formatting.type("Obtained 'Map'!");
                                        Main.hasmap = true;
                                        break;
                                    case 'g':
                                        Main.Player.intl += 1;
                                        Formatting.type("You are touched by the art of cooking. Being forged in the flame of cooking, your ability to think up vicious insults has improved. Your intelligence has improved a little");
                                        Main.gbooks++;
                                        break;
                                }
                                Main.ramsaycounter++;
                                break;
                            }
                            else
                            {
                                Formatting.type("The library is closed, but you find a signed version of Gordon ramsay's book.");
                                break;
                            }
                        case 'w':
                            Formatting.type("You realize that you don't speak the same language as the shopkeeper. You take all of his peppermint candy and leave.");
                            Formatting.type("Before you toss it on the floor like the scumbad you are, you notice one of the candy wrappers has an apostrophe on the inside.");
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

    public class NKingdom : Place
    {
        protected override string GetDesc()
        {
            return "You have arrived at the gate of a castle. There are many people passing through the gate. You see the royal library(l), a smithy's guild(g). Where do you wish to go?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('l');
            templist.Add('g');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
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
                case 'l':
                    if (Main.nlibcounter == 0)
                    {
                        Formatting.type("In the royal library you find 'Crescent Path'(c), 'Tale of Sariel'(t), 'History of The Realm'(h), and Gordon ramsay: A Geology(g). Which do you wish to read?");
                        switch (Console.ReadKey().KeyChar)
                        {
                            case 'c':
                                Formatting.type(" The book reads 'In the land of the central sands.......(This book is incredibly long. If you wish to complete this book, it may cost you.");
                                switch (Console.ReadKey().KeyChar)
                                {
                                    case 'y':
                                        Main.Player.hp -= 1;
                                        Main.Player.spd += 3;
                                        Formatting.type("You are exhausted from finishing the book, but you feel like your speed has increased a little.");
                                        break;
                                    case 'n':
                                        Formatting.type("You decide that completing this book is not worth the time. Do you wish to put the book back in it's original spot on the bookshelf?(y/n)");
                                        switch (Console.ReadKey().KeyChar)
                                        {
                                            case 'y':
                                                Formatting.type("You place the book back in it's original position. You daydream about a perfect society where all of mankind would put books back in bookshelves.");
                                                break;
                                            case 'n':

                                                Formatting.type("Are you the trash of society?");
                                                switch (Console.ReadKey().KeyChar)
                                                {
                                                    case 'y':
                                                        Main.Player.hp -= 3;
                                                        Formatting.type("You lose 3 hp. Dirtbag.");
                                                        break;
                                                    case 'n':
                                                        Formatting.type("You place the book back in the bookshelf.");
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case 't':
                                Formatting.type("You read of the exploits of the ancient hero Sariel, and his philosophies of protecting others.");
                                Formatting.type("Learned 'Dawnstrike'!");
                                Main.Player.abilities.AddCommand(new Combat.Dawnstrike("Dawnstrike", 't'));
                                break;
                            case 'h':
                                Formatting.type("You read 5000 pages on the history of the realm. You're not sure why you did that, but you feel smarter.");
                                Main.Player.intl += 2;
                                break;
                            case 'g':
                                Formatting.type("You learn of the layers of granite and basalt on Planet ramsay.");
                                Main.Player.def += 1;
                                Main.gbooks++;
                                break;
                        }
                        Main.nlibcounter++;
                    }
                    else
                    {
                        Formatting.type("The library is closed.");
                        break;
                    }
                    break;
                case 'g':
                    Formatting.type("The Smith's Guild only has their cumulative project for sale. It's a masterpiece, but very expensive. Buy Bloodmail for 50 gold? (y/n)");
                    switch(Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            if (Main.Purchase(50, new bt_plate()))
                                Formatting.type("Obtained 'Bloodmail'!");
                            break;
                        case 'n':
                            Formatting.type("You leave.");
                            break;
                    }
                    break;
                case 'c':
                    break;
                case 'p':
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

    public class CentralKingdom : Place
    {
        protected override string GetDesc()
        {
            return "You step through the gates of the city, and witness the largest coalescence of humanity you've seen in you entire life. There are hundereds of great wonders in this behemoth of a city. You may visit the library(l), the weaponsmith(a), the inn(i), the magic shop(q), or the town's great monument(o)";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
            templist.Add('q');
            templist.Add('o');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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

                case 'l':
                    if (Main.centrallibcounter == 0)
                    {
                        Formatting.type("This massive building is a monument to human knowledge. You feel dwarfed by its towering presence.");
                        Formatting.type("You arrive at a shelf that draws your attention. There are six books. The first one is a musty tome entitled Alcwyn's Legacy(a). The second one is A Guide to Theivery(g), the third Sacrificial Rite(s). The fourth book is The Void(v). The fifth book is Samael's Game(j). The final book's title ramsay: A Mathematics(r). Which do you read?");
                        switch (Console.ReadKey().KeyChar)
                        {
                            case 'a':
                                Formatting.type("You become enlightened in the ways of the elder wizard Alcywn.");
                                Formatting.type("Learned 'Curse'!");
                                Main.Player.abilities.AddCommand(new Combat.Curse("Curse", 'c'));
                                
                                break;
                            case 'g':
                                Formatting.type("Now skilled in the art of stealing, you gain 10% more gold.");
                                Main.is_theif = true;
                                break;
                            case 's':
                                Formatting.type("You become skilled in the art of sacrifice.");
                                Formatting.type("Learned 'Sacrifice'!");
                                Main.Player.abilities.AddCommand(new Combat.Sacrifice("Sacrifice", 's'));
                                break;
                            case 'v':
                                Formatting.type("You learn of the Void. You can phase in and out of reality.");
                                Formatting.type("Learned 'Phase'!");
                                Main.Player.abilities.AddCommand(new Combat.Phase("Phase", 'p'));
                                break;
                            case 'r':
                                Formatting.type("You become educated on the mathematics of cooking.");
                                Main.Player.intl += 1;
                                Main.gbooks++;
                                break;
                            case 'j':
                                Formatting.type("You open the book and find dice inside. Do you wish to roll?(y/n)");
                                switch(Console.ReadKey().KeyChar)
                                {
                                    case 'y':
                                        int abilchance = Combat.Dice.roll(1,6);
                                        if (abilchance == 6)
                                        {
                                            Formatting.type("You feel as if something incredible has happened.");
                                            Formatting.type("Learned 'Gamble'!");
                                            Main.Player.abilities.AddCommand(new Combat.Gamble("Gamble", '$'));
                                        }
                                        else
                                        {
                                            Formatting.type("Wow you roll a" + abilchance);
                                            Main.Player.hp -= abilchance;
                                            Formatting.type("You lose" + abilchance + "hp");
                                        }
                                        break;
                                        
                                    case 'n':
                                        Formatting.type("You feel threatened by the words.");
                                        break;
                                }
                        Main.centrallibcounter++;
                        break;
                        }
                        Main.centrallibcounter++;
                    }
                    else
                    {
                        Formatting.type("The library is closed.");
                        break;
                    }
                    break;
                case 'a':
                    Formatting.type("You approach a building with a sigil bearing crossed swords. You suspect this is the weaponsmith. You enter, and he has loads of goodies for sale. Buy Iron Rapier(r, 30), Iron Chainmail(c, 30), Iron Buckler (b, 25), or Bloodthirsty Longsword(l, 50)?");
                    switch(Console.ReadKey().KeyChar)
                    {
                        case 'r':
                            Main.Purchase(30, new iron_rapier());
                            Formatting.type("Obtained 'Iron Rapier'!");
                            break;
                        case 'c':
                            Main.Purchase(30, new iron_mail());
                            Formatting.type("Obtained 'Iron Chainmail!'!");
                            break;
                        case 'b':
                            Main.Purchase(25, new iron_buckler());
                            Formatting.type("Obtained 'Iron Buckler'!");
                            break;
                        case 'l':
                            Main.Purchase(50, new bt_longsword());
                            Formatting.type("Obatined 'Bloodthirsty Longsword'!");
                            break;
                        default:
                            break;
                    }
                    break;
                case 'q':
                    if (Main.magiccounter == 0)
                    {
                        Formatting.type("You arrive at a shifty magic dealer in a back alley. He says, 'Hey! " + Main.Player.name + "! I can sell you this Blood Amulet(b, 50), a new ability(a, 50), or secret (s, 50). You game? You're not sure how he knows your name, but it freaks you out.");
                        switch (Console.ReadKey().KeyChar)
                        {
                            case 'b':
                                if (Main.Purchase(50, new blood_amulet()))
                                    Formatting.type("Obtained 'Blood Amulet'!");
                                break;
                            case 'a':
                                if (Main.Purchase(50))
                                {
                                    Formatting.type("He holds out his hand, and reality appears to bend around it. Kind of like the Degauss button on monitors from the 90's.");
                                    Main.Player.abilities.AddCommand(new Combat.VorpalBlades("Vorpal Blades", 'v'));
                                    Formatting.type("Learned 'Vorpal Blade'!");
                                }
                                break;
                            case 's':
                                if (Main.Purchase(50))
                                    Formatting.type("'I have a secret to tell you. Everything you know is wrong.' He holds out his hand, and reality appears to bend around it. Kind of like the Degauss button on monitors from the 90's. 'See this?' he says. 'This is what's known as the Flux. Everything is from the Flux, and controlled by the Flux. Learn to control it, and you control reality.'. he dissapears througha shimmering portal and leaves you there mystified.");
                                break;
                        }
                        Main.magiccounter++;
                        break;
                    }
                    else
                    {
                        Formatting.type("You look for the magic man, but he's gone.");
                        break;
                    }
                case 'b':
                    Main.BackpackLoop();
                    break;
                case 'i':
                    Formatting.type("The sign above the inn reads 'Donaldius Trumpe'. Stay the night for 15 gold? (y/n)");
                    switch(Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            Formatting.type("You feel refreshed, however the bedsheets smelled like cold blooded capitalism and weasely politicians.");
                            Main.Purchase(15);
                            Main.Player.hp = Main.Player.maxhp;
                            break;
                        case 'n':
                            Formatting.type("You leave the posh hotel.");
                            break;
                        default:
                            break;
                    }
                    break;
                case 'o':
                    Formatting.type("You visit the city's monument, which is a tourist attraction for the entire Realm. People are everywhere. You elbow your way through the crowd to get a better look. The monument is a massive sword, stabbed into the ground as if placed there by a giant god. Blue light is pulsing up it like a giant conduit. You spot a secret door in the blade of the sword. Enter (y/n)");
                    switch(Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            Formatting.type("The door requires a password.");
                            if (Console.ReadLine() == "God's Will")
                                Main.Endgame();
                            else
                                Formatting.type("The door remains shut.");
                            break;
                        case 'n':
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
    public class SKingdom : Place
    {
        protected override string GetDesc()
        {
            return "You step through the gates of the southernmost kingdom in the land. The gates are still scarred from the Invasion. The library is a smoking ruin. You may go to the arms dealer(a), the residential district(r) or the inn(i)";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
            templist.Add('q');
            templist.Add('o');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
                case 'n':
                    Globals.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Globals.PlayerPosition.x += 1;
                    break;
                case 'w':
                    Globals.PlayerPosition.x -= 1;
                    break;
                case 'a':
                    Formatting.type("The arms dealer has a small stand, but his wares are valuable. You may buy Darksteel Amulet(100, a), Darksteel Kris(80, k), Darksteel Kite Shield(s, 100), or Darksteel Scalemail(c, 110).");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'a':
                            if (Main.Purchase(100, new ds_amulet()))
                                Formatting.type("Obtained 'Darksteel Amulet'!");
                            break;
                        case 'c':
                            if (Main.Purchase(110, new ds_scale()))
                                Formatting.type("Obatined 'Darksteel Scalemail'!");
                            break;
                        case 'k':
                            if (Main.Purchase(80, new ds_kris()))
                                Formatting.type("Obtained 'Darksteel Kris!'!");
                            break;
                        case 's':
                            if (Main.Purchase(100, new ds_kite()))
                                Formatting.type("Obtained 'Darksteel Kite Shield'!");
                            break;
                        default:
                            break;
                    }
                    break;

                case 'b':
                    Main.BackpackLoop();
                    break;
                case 'i':
                    Formatting.type("The inn is burned, but still in use. Stay the night for 20 gold? (y/n)");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            Formatting.type("You feel refreshed, although smelling of ash.");
                            Main.Purchase(20);
                            Main.Player.hp = Main.Player.maxhp;
                            break;
                        case 'n':
                            Formatting.type("You leave the inn.");
                            break;
                        default:
                            break;
                    }
                    break;
                case 'r':
                    if (Main.townfolkcounter == 0)
                    {
                        Formatting.type("You visit the house of the jobless former librarian. He says there is something very strange about that sword monument in Central. He says you can never go anywhere unarmed. He teaches you a new ability.");
                        Formatting.type("Learned 'Incinerate'!");
                        Main.Player.abilities.AddCommand(new Combat.Incinerate("Incinerate", 'i'));
                        Formatting.type("As you're leaving, you notice the letter 'l' burned into the doorknob.");
                        Main.townfolkcounter++;
                    }
                    else
                        Formatting.type("You look for the librarian, but he's not there.");
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class Newport : Place
    {
        protected override string GetDesc()
        {
            return "You walk into a ghost town, obviously built for a port before the ocean receeded. You may visit the inn(i) or the arms dealer(a).";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('i');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                case 'a':
                    Formatting.type("You visit the arms dealer. It's a very old man selling some very expensive wares. You wonder where he came across such valuables. Buy Bloodthirsty Battleaxe(b, 55), or Bloodthirsty Greatsword(g, 55)?");
                    switch(Console.ReadKey().KeyChar)
                    {
                        case 'b':
                            if (Main.Purchase(55, new bt_battleaxe()))
                                Formatting.type("Obtained 'Bloodthirsty Battleaxe'!");
                            Formatting.type("The old man grins.");
                            break;
                        case 'g':
                            if (Main.Purchase(55, new bt_greatsword()))
                                Formatting.type("Obtained 'Bloodthirsty Greatsword'!");
                            Formatting.type("The old man grins.");
                            break;
                    }
                    break;
                case 'b':
                    Main.BackpackLoop();
                    break;
                case 'i':
                    Formatting.type("Stay at the average-ass hotel for 15 gold?(y/n)");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            if (Main.Purchase(15))
                                Main.Player.hp = Main.Player.maxhp;
                            Formatting.type("As you're leaving you pick up a scrabble tile off of the floor. It is a blank. (' ').");
                            break;
                        case 'n':
                            Formatting.type("You leave.");
                            break;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
    public class Nomad : Place
    {
        protected override string GetDesc()
        {
            return "You happen upon a travelling band of merchants wearing turbans. They have unimaginably valuable wares for sale. You may talk to the Sage Kaiser(k) or the man with the gear(g)";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('k');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                case 'g':
                    Formatting.type("You talk to the toothless man holding the wares. You may buy the Void Cloak(v, 150), the Illusory Plate(i, 150), or the Spectral Bulwark(s, 150). Or you may buy all 3(3, 300).");
                    switch(Console.ReadKey().KeyChar)
                    {
                        case 'v':
                            if (Main.Purchase(150, new void_cloak()))
                            {
                                Formatting.type("The toothless man reverently hands you artifact.");
                                Formatting.type("Obtained 'Void Cloak'!");
                            }
                            break;
                        case 'i':
                            if (Main.Purchase(150, new illusory_plate()))
                            {
                                Formatting.type("The toothless man reverently hands you artifact.");
                                Formatting.type("Obtained 'Illusory Plate'!");
                            }
                            break;
                        case 's':
                            if (Main.Purchase(150, new spectral_bulwark()))
                            {
                                Formatting.type("The toothless man reverently hands you artifact.");
                                Formatting.type("Obtained 'Spectral Bulwark'!");
                            }
                            break;
                        case '3':
                            if (Main.Purchase(300, new void_cloak()))
                            {
                                Formatting.type("Obtained 'Void Cloak'!");
                                Formatting.type("Obtained 'Spectral Bulwark'!");
                                Formatting.type("Obtained 'Illusory Plate'!");

                                if (Main.Player.backpack.Count <= 10)
                                    Main.Player.backpack.Add(new void_cloak());
                                else
                                    Formatting.type("Not enough space.");

                                if (Main.Player.backpack.Count <= 10)
                                    Main.Player.backpack.Add(new spectral_bulwark());
                                else
                                    Formatting.type("Not enough space.");

                                if (Main.Player.backpack.Count <= 10)
                                    Main.Player.backpack.Add(new illusory_plate());
                                else
                                    Formatting.type("Not enough space.");
                            }
                            break;
                    }
                    break;
                case 'k':
                    if (Main.nomadcounter == 0)
                    {
                        Formatting.type("You visit their Elder King, or Sage Kaiser, as they call him. He offers to teach you an ability for 50 gold. (y/n)");
                        switch (Console.ReadKey().KeyChar)
                        {
                            case 'y':
                                if (Main.Purchase(50))
                                {
                                    Formatting.type("You say yes, and he holds up hand, and some strange runes on his hand begin to glow.");
                                    Formatting.type("Learned 'Heavensplitter'!");
                                    Formatting.type("The old man also hands you a rune with the letter 's' inscribed.");
                                    Main.Player.abilities.AddCommand(new Combat.Heavensplitter("Heavensplitter", 'z'));
                                    Main.nomadcounter++;
                                }
                                break;
                        }
                    }
                    else
                        Formatting.type("He already taught you that ability. He has nothing more to offer.");
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
    public class EKingdom : Place
    {
        protected override string GetDesc()
        {
            return "You approach the gates of the East Kingdom. You see an arms dealer(a) and an inn(i). Where do you go?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('i');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
                case 'n':
                    Globals.PlayerPosition.y += 1;
                    break;
                case 'w':
                    Globals.PlayerPosition.x -= 1;
                    break;
                case 's':
                    Globals.PlayerPosition.y -= 1;
                    break;
                case 'i':
                    Formatting.type("Stay at the luxurious hotel for 40 gold?(y/n)");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            if (Main.Purchase(40))
                                Main.Player.hp = Main.Player.maxhp;
                            Formatting.type("As you're leaving you take all the free soap.");
                            break;
                        case 'n':
                            Formatting.type("You leave.");
                            break;
                    }
                    break;
                case 'a':
                    Formatting.type("You visit the arms dealer. It's being manned by a child. Buy Sunburst Saber(120, v) or Suburst Shield(100, s).");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'v':
                            if (Main.Purchase(120, new sb_saber()))
                            {
                                Formatting.type("The child smiles gleefully and hands you the Sunburst Saber.");
                                Formatting.type("Obtained 'Sunburst Saber'!");
                            }
                            break;
                        case 'i':
                            if (Main.Purchase(100, new sb_shield()))
                            {
                                Formatting.type("The child smiles and hands you the Sunburst Shield.");
                                Formatting.type("Obtained 'Sunburst Shield'!");
                            }
                            break;
                    }
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Main.BackpackLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }    
    }

    public class TwinPaths : Place
    {
        protected override string GetDesc()
        {
            return "You are in a small village at the base of a waterfall. You see an arms dealer(a) and an inn(i). Where do you want to go?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Globals.PlayerPosition.x > 0)
                templist.Add('w');
            if (Globals.PlayerPosition.x < Globals.map.GetUpperBound(0))
                templist.Add('e');
            if (Globals.PlayerPosition.y > 0)
                templist.Add('s');
            if (Globals.PlayerPosition.y < Globals.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('#');
            templist.Add('a');
            templist.Add('i');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
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
                case 'a':
                    Formatting.type("An average guy manages the shop. You can buy a Sunburst Ringmail(150) and a Sunburst Gauntlet(150).(r/g)");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'r':
                            Main.Purchase(150, new sb_chain());
                            Formatting.type("The man hands you the Sunburst Ringmail.");
                            Formatting.type("Obtained 'Sunburst Ringmail'!");
                            break;
                        case 'g':
                            Main.Purchase(150, new sb_gauntlet());
                            Formatting.type("The man hands you the Sunburst Gauntlet.");
                            Formatting.type("Obtained 'Sunburst Gauntlet'!");
                            break;
                    }
                    break;
                case 'i':
                    Formatting.type("Stay at the overpriced hotel for 60 gold?(y/n)");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case 'y':
                            if (Main.Purchase(60))
                                Main.Player.hp = Main.Player.maxhp;
                            Formatting.type("As you're leaving you set fire to the bathroom. You see a sign on the door that reads 'Romney 2012', and one of the tiles on the bathroom floor reads 'i'.");
                            break;
                        case 'n':
                            Formatting.type("You leave.");
                            break;
                    }
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Main.BackpackLoop();
                    break;
                default:
                    return false;
            }
            return true;
        }    
    }

    public class Ravenkeep : Place
    {
        protected override string GetDesc()
        {
            return "You are at the foot of a black citadel, do you wish to climb the stairs and commence the reconquest of Ravenkeep(c) or leave(l)?";
        }
        public override Enemy getEnemyList()
        {
            return null;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            templist.Add('#');
            templist.Add('c');
            templist.Add('l');
            return templist.ToArray<char>();
        }
        public override bool _handleInput(char input)
        {
            switch (input)
            {
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Formatting.drawmap();
                    break;
                case 'c':
                    if (!Main.raven_dead)
                    {
                        Formatting.type("You climb 18 flights of obsidian stairs. You're kind of huffing an puffing at this point, but you stand before the mad king who rules RavenKeep.");
                        Formatting.type("Challenge him, or run? (f/r).");
                        switch (Console.ReadKey().KeyChar)
                        {
                            case 'f':
                                Formatting.type("You challenge the mad king, and he stands from his obsidian throne, raven-feathered cloak swirling. He laughs a deep booming laugh and draws a wicked looking blade.");
                                Main.BattleLoop(new RavenKing(), true);
                                if (Main.Player.backpack.Count <= 10)
                                    Main.Player.backpack.Add(new phantasmal_claymore());
                                else
                                    Formatting.type("Not enough space.");
                                Formatting.type("The king falls to the ground, defeated. You pick up his night colored sword form the ground, and in your hand it changes to a shimmering blue claymore.");
                                Formatting.type("Obtained 'Phantasmal Claymore'!");
                                Formatting.type("A blue portal opens up with the glowing letter 'l' above it. You yell 'Jeronimo!' and jump through.");
                                Globals.PlayerPosition.x = 2;
                                Globals.PlayerPosition.y = 2;
                                break;
                            case 'r':
                                Formatting.type("You run back down the stairs like this sissy cRAVEN you are.");
                                break;
                        }
                    }
                    else
                        Formatting.type("There's no one here.");
                    break;
                case 'l':
                    Formatting.type("Coward.");
                    Globals.PlayerPosition.x -= 1;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
