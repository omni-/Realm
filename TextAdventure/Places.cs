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
            templist.Add(new Goblin());
            templist.Add(new Bandit());
            return templist;
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
            return "King: 'Come, " + Main.Player.name + ", for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite. The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight(f) or run(r)?";
        }

        public override List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            //templist.Add(new Slime());
            //templist.Add(new Goblin());
            return templist;
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
                    Main.BattleLoop(new WesternKing());
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
        public override List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            //templist.Add(new Goblin());
            return templist;
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
                                    if (Main.Purchase(11, globals.wood_staff))
                                    {
                                        Main.Player.backpack.Add(globals.plastic_ring);
                                        Formatting.type("You buy the staff. He grins, and you know you've been ripped off.");
                                    }
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
            return "You arrive at a seaside port bustling with couriers and merchants. Do you want to go to the arms dealer(a), the library(l), or inn(i)?";
        }
        public override List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            templist.Add(new Goblin());
            return templist;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            templist.Add('n');
            templist.Add('e');
            templist.Add('s');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
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
                    Formatting.type("You visit the arms dealer. He's out of stock except for an iron lance(l, $15) and an iron buckler(b, $10). (n to leave)");
                    char __tempinput = Console.ReadKey().KeyChar;
                    switch (__tempinput)
                    {
                        case 'l':
                            if (Main.Purchase(15, globals.iron_lance))
                                Formatting.type("It's almost as if he doesn't even see you.");
                            break;
                        case 'b':
                            if (Main.Purchase(10, globals.iron_buckler))
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
        protected override string GetDesc()
        {
            return "You arrive at a small town just east of the Western Kingdom. Do you want to visit the town(v) or head to the inn(i)?";
        }
        public override List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            templist.Add(new Goblin());
            return templist;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            templist.Add('n');
            templist.Add('e');
            templist.Add('s');
            templist.Add('w');
            templist.Add('i');
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
                                    if (Main.Purchase(15, globals.iron_band))
                                        Formatting.type("He smiles weakly and thanks you.");
                                    break;
                                case 'n':
                                    Formatting.type("You leave.");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 't':
                            Formatting.type("You talk to a villager. He muses about the fact that sometimes, reality doens't feel real at all. Puzzled by his comment, you walk away.");
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
        public override List<Enemy> getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Goblin());
            templist.Add(new Bandit());
            return templist;
        }
        public override char[] getAvailableCommands()
        {
            List<char> templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            templist.Add('n');
            templist.Add('e');
            templist.Add('s');
            templist.Add('w');
            templist.Add('y');
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
                            Formatting.type("There are three books. Do you wish to read Climbing Safety (c), Solomon's Answer (s), or Gordon Ramsay: A Biology (g)");
                            switch(Console.ReadKey().KeyChar)
                            {
                                case 'c':
                                    Formatting.type("Climbing mountains requires absolute safety. (.....this book seems pretty thick. Do you wish to continue reading?(y/n))");
                                        switch(Console.ReadKey().KeyChar)
                                        {
                                            case 'y':
                                                Formatting.type("As you silently become informed about safety, you notice that a segment of the wall is opening itself up to your far right. Do you wish to enter?(y/n)");
                                                switch(Console.ReadKey().KeyChar)
                                                {
                                                    case 'y':
                                                        Formatting.type("You try to enter, but the door requires a password.");
                                                        if (Console.ReadLine() == "Don't Fall")
                                                        {
                                                            Formatting.type(" You open the door to find a dark room with a suspicious figure conducting suspicious rituals. The man looks flustered and says 'Nice day isn't it?'. As you wonder what anyone would be doing in such a dark room, the man edges his way to the entrance and dashes outside. He forgot to take his book with him. Do you wish to take it? (y/n) ");
                                                            switch (Console.ReadKey().KeyChar)
                                                            {
                                                                case 'y':
                                                                    Main.Player.abilities.AddCommand(new Combat.LifeEat("Life Eat", 'e'));
                                                                    Formatting.type(" Learned 'Life Eat'!(e)");
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
                                                switch(Console.ReadKey().KeyChar)
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
                                    Formatting.type("You don't know how to read this language");
                                    break;
                                case 'g':
                                    Main.Player.intl += 1;
                                    Formatting.type("You are touched by the art of cooking. Being forged in the flame of cooking, your ability to think up vicious insults has improved. Your intelligence has improved a little");
                                    break;
                            }
                            break;
                        case 'w':
                            Formatting.type("You realize that you don't speak the same language as the shopkeeper. You take all of his peppermint candy and leave.");
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
