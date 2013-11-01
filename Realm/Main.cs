using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public static class Main
    {
        public static int loop_number = 0;
        public static int game_state = 0;

        public static int wkingcounter = 0;
        public static int slibcounter = 0;
        public static int forrestcounter = 0;
        public static int libcounter = 0;
        public static int centrallibcounter = 0;
        public static int ramsaycounter = 0;
        public static int magiccounter = 0;
        public static int nlibcounter = 0;
        public static int townfolkcounter = 0;
        public static int nomadcounter = 0;
        public static int minecounter = 0;
        public static int frozencounter = 0;
        public static int noobcounter = 0;

        public static int gbooks = 0;

        public static int intlbuff = 0;
        public static int defbuff = 0;
        public static int atkbuff = 0;
        public static int spdbuff = 0;

        public static bool raven_dead = false;
        public static bool is_theif = false;
        public static bool wkingdead = false;
        public static bool is_typing = false;

        public static Realm.Player.GamePlayer Player = new Realm.Player.GamePlayer();
        public static Map globals = new Map();

        public static Random rand = new Random();

        public static bool devmode = false;
        public static bool hasmap = false;

        public static string version = "Version Number - 1.7.6";
        public static string devstring = "Cooper";
        public static string password = "__dev__";

        public static void Tutorial()
        {
            List<string> racelist = new List<string> { "human", "elf", "rockman", "giant", "zephyr", "shade" };
            List<string> classlist = new List<string> { "warrior", "paladin", "mage", "thief" };
            is_typing = true;
            Interface.type("Welcome, ");
            Interface.typeOnSameLine(Player.name, ConsoleColor.White);
            Interface.typeOnSameLine(", to Realm.");
            Interface.type("To do anything in Realm, simply press one of the listed commands.");
            Interface.type("If at any time, you wish to you view you stats, press 'v'");
            Interface.type("Make sure to visit every library! They offer many valuable abilities as well as experience.");
            Interface.type("When in combat, select an availible move. All damage is randomized. Mana is refilled after each fight.");
            Interface.type("While in the backpack, simply select a number corresponding to an item. You may swap this item in or out. Make sure to equip an item once you pick it up!");
            Interface.type("At any specified time, you may press x, then y. This will cause you to commit suicide.");
            Interface.type("At any specified time, you may press #. Doing so will save the game.");
            Interface.type("In Realm, every player selects a race. Each race gives its own bonuses. You may choose from Human, Elf, Rockman, Giant, Zephyr, or Shade.");
            Interface.type("Please enter a race. ");
            is_typing = false;
            string race = Interface.readinput();
            while (!racelist.Contains(race))
            {
                Interface.type("Invalid. Please try again. ");
                race = Interface.readinput();
            }
            Player.race = race;
            Interface.type("You have selected " + Interface.ToUpperFirstLetter(Player.race) + ".", ConsoleColor.Magenta);
            Interface.type("Each player also has a class. You may choose from Warrior, Paladin, Mage, or Thief.");
            Interface.type("Please enter a class. ");
            string pclass = Interface.readinput();
            while (!classlist.Contains(pclass))
            {
                Interface.type("Invalid. Please try again. ");
                pclass = Interface.readinput();
            }
            Player.pclass = pclass;
            Interface.type("You have selected " + Interface.ToUpperFirstLetter(Player.pclass) + ".", ConsoleColor.Magenta);
            Interface.type("You are now ready to play Realm. Good luck!");
            Interface.type("Press any key to continue.", ConsoleColor.White);
            Interface.readkey();
            Map.PlayerPosition.x = 0;
            Map.PlayerPosition.y = 6;
            if (Player.race == "giant")
                Player.hp = 15;
            MainLoop();
        }
        public static void MainLoop()
        {
            try
            {
                game_state = 0;
                Place currPlace;
                if (devmode)
                {
                    Interface.type("Dev Powers!", ConsoleColor.DarkMagenta);
                    Player.primary = new phantasmal_claymore();
                    Player.secondary = new spectral_bulwark();
                    Player.armor = new illusory_plate();
                    Player.accessory = new void_cloak();
                    hasmap = true;
                }
                while (Player.hp > 0)
                {
                    Player.applybonus();
                    Enemy enemy = new Enemy();
                    Player.levelup();
                    currPlace = Map.map[Map.PlayerPosition.x, Map.PlayerPosition.y];
                    if (Player.hp > Player.maxhp)
                        Player.hp = Player.maxhp;
                    if (loop_number >= 1)
                    {
                        if (Combat.CheckBattle() && currPlace.getEnemyList() != null)
                        {
                            enemy = currPlace.getEnemyList();
                            Combat.BattleLoop(enemy);
                        }
                    }
                    Item pc = new phantasmal_claymore();
                    Item sb = new spectral_bulwark();
                    Item ip = new illusory_plate();
                    Item vc = new void_cloak();
                    if (!String.IsNullOrEmpty(Player.primary.name) && !String.IsNullOrEmpty(Player.secondary.name) && !String.IsNullOrEmpty(Player.armor.name) && !String.IsNullOrEmpty(Player.accessory.name))
                        if (Player.primary.name.Equals(pc.name) && Player.secondary.name.Equals(sb.name) && Player.armor.name.Equals(ip.name) && Player.accessory.name.Equals(vc.name))
                            if (!Player.abilities.commandChars.Contains('*'))
                                Player.abilities.AddCommand(new Combat.EndtheIllusion("End the Illusion", '*'));
                    if (devmode)
                        Interface.type(Map.PlayerPosition.x + " " + Map.PlayerPosition.y);
                    if (!devmode)
                        Player.applybonus();
                    else
                        Player.applydevbonus();
                    if (gbooks >= 3 && !Player.abilities.commandChars.Contains('@'))
                    {
                        Interface.type("Having read all of the Ramsay books, you are enlightened in the ways of Gordon Ramsay.");
                        Interface.type("Learned 'Hell's Kitchen'!", ConsoleColor.Cyan);
                        Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
                    }
                    //currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                    if (!devmode)
                        Interface.type(currPlace.Description);
                    else
                        Interface.type(currPlace.ToString());
                    char[] currcommands = currPlace.getAvailableCommands();
                    Interface.typeOnSameLine("\r\nYour current commands are x", ConsoleColor.DarkBlue);
                    foreach (char c in currcommands)
                    {
                        Interface.typeOnSameLine(", " + c, ConsoleColor.DarkBlue);
                    }
                    Interface.type("");

                    ConsoleKeyInfo command = Interface.readkey();
                    if (command.KeyChar == 'x')
                    {
                        Interface.type("\r\nAre you sure?", ConsoleColor.Red);
                        char surecommand = Interface.readkey().KeyChar;
                        if (surecommand == 'y')
                        {
                            End.GameOver();
                        }
                    }
                    else if (command.Key == ConsoleKey.Escape)
                        Environment.Exit(0);
                    else if (command.KeyChar == '-' && devmode)
                    {
                        string input = Interface.readinput();
                        if (input == "e")
                            End.Endgame();
                        else if (input == "c")
                        {
                            string combat_input = Interface.readinput();
                            Type etype = Type.GetType("Realm." + combat_input);
                            try
                            {
                                Enemy e = (Enemy)Activator.CreateInstance(etype);
                                Combat.BattleLoop(e);
                            }
                            catch (ArgumentNullException)
                            {
                                Interface.type("Invalid Enemy.");
                            }
                        }
                        else if (input == "n")
                            Player.name = Interface.readinput();
                        else if (input == "a")
                        {
                            string add_input = Interface.readinput();
                            Type atype = Type.GetType("Realm." + add_input);
                            try
                            {
                                Item i = (Item)Activator.CreateInstance(atype);

                                if (Player.backpack.Count <= 10)
                                {
                                    Player.backpack.Add(i);
                                    Interface.type("Obtained '" + i.name + "'!");
                                }
                                else
                                {
                                    Interface.type("Not enough space.");
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                Interface.type("Invalid Item.");
                            }
                        }
                        else if (input == "p")
                        {
                            string p_input = Interface.readinput();
                            Type atype = Type.GetType("Realm." + p_input);
                            try
                            {
                                Item i = (Item)Activator.CreateInstance(atype);
                                Player.primary = i;
                            }
                            catch (ArgumentNullException)
                            {
                                Interface.type("Invalid Item.");
                            }
                        }
                        //else if (input == "t")
                        //{
                        //    string place_input = Interface.readinput();
                        //    Type ptype = Type.GetType("Realm." + place_input);
                        //    Place p = (Place)Activator.CreateInstance(ptype);
                        //}
                        else if (input == "l")
                        {
                            Main.Player.level = Convert.ToInt32(Interface.readinput());
                        }
                        else if (input == "x")
                        {
                            Main.Player.xp += Main.Player.xp_next;
                        }
                    }
                    else
                        currPlace.handleInput(command.KeyChar);
                    loop_number++;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}