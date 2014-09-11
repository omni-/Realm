using System;
using System.Collections.Generic;
using System.Linq;

namespace Realm
{
    public static class Main
    {
        public static int loop_number = 0, slimecounter = 0, goblincounter = 0, banditcounter = 0, drakecounter = 0, wkingcounter = 0, slibcounter = 0, forrestcounter = 0, libcounter = 0, centrallibcounter = 0, ramsaycounter = 0, magiccounter = 0, nlibcounter = 0, townfolkcounter = 0, nomadcounter = 0, minecounter = 0, frozencounter = 0, noobcounter = 0, gbooks = 0, intlbuff = 0, defbuff = 0, atkbuff = 0, spdbuff = 0;

        public static bool raven_dead = false, is_theif = false, wkingdead = false, is_typing = false, devmode = false, hasmap = false, achievements_disabled = false;

        public static Achievement ach = new Achievement();
        public static Player.GamePlayer Player = new Player.GamePlayer();
        public static Map globals = new Map();

        public static Random rand = new Random();

        public static string version = "Version Number - v1.8.4.5";

        public static Dictionary<string, bool> achieve = new Dictionary<string, bool>();

        public static List<Item> MainItemList = new List<Item> {
new cardboard_armor(), new cardboard_shield(), new cardboard_sword(), new iron_band(), new iron_buckler(), new iron_lance(), new iron_mail(), new iron_rapier(), new wood_armor(), new wood_plank(), new wood_staff(), new fmBP(), new sonictee(), new slwscreen(), new plastic_ring(), new m_amulet(), new m_robes(), new m_staff(), new m_tome(), new bt_battleaxe(), new bt_greatsword(), new bt_longsword(), new bt_plate(), new blood_amulet(), new swifites(), new ice_amulet(), new ice_dagger(), new ice_shield(), new p_mail(), new p_shield(), new p_shortsword(), new goldcloth_cloak(), new a_amulet(), new a_mail(), new a_staff(), new tome(), new ds_amulet(), new ds_kite(), new ds_kris(), new ds_scale(), new sb_saber(), new sb_chain(), new sb_gauntlet(), new sb_shield()
        };

        public static void Tutorial()
        {
            List<string> racelist = new List<string> { "human", "elf", "rockman", "giant", "zephyr", "shade" }, classlist = new List<string> { "warrior", "paladin", "mage", "thief" }, secret = new List<string>();
            if (achieve["100slimes"])
                secret.Add("Slime");
            if (achieve["100goblins"])
                secret.Add("Goblin");
            if (achieve["100bandits"])
                secret.Add("Bandit");
            if (achieve["100drakes"])
                secret.Add("Drake");
            is_typing = true;
            Interface.type("Welcome, ");
            Interface.typeOnSameLine(Player.name, ConsoleColor.White);
            Interface.typeOnSameLine(", to Realm.");
            if (achieve["finalboss"])
                Player.backpack.Add(new nightbringer());
            if (!devmode)
            {
                Interface.type("Skip the tutorial? (y/n)");
                if (Interface.readkey().KeyChar == 'n')
                {
                    Interface.type("To do anything in Realm, simply press one of the listed commands.");
                    Interface.type("If at any time, you wish to you view you stats, press 'v'");
                    Interface.type("Make sure to visit every library! They offer many valuable abilities as well as experience.");
                    Interface.type("When in combat, select an availible move. All damage is randomized. Mana is refilled after each fight.");
                    Interface.type("While in the backpack, simply select a number corresponding to an item. You may swap this item in or out. Make sure to equip an item once you pick it up!");
                    Interface.type("At any specified time, you may press #. Doing so will save the game.");
                }
                Interface.type("In Realm, every player selects a race. Each race gives its own bonuses. You may choose from Human, Elf, Rockman, Giant, Zephyr, or Shade.");
                if (achieve["100slimes"] || achieve["100goblins"] || achieve["100bandits"] || achieve["100drakes"])
                {
                    Interface.type("Secret Races: ", ConsoleColor.Yellow);
                    for (var i = 0; i < secret.Count; i++)
                    {
                        if (i > 1)
                            Interface.typeOnSameLine(", ", ConsoleColor.Yellow);
                        Interface.type(secret[i], ConsoleColor.Yellow);
                    }
                    racelist.AddRange(secret);
                }
                Interface.type("Please enter a race. ");
                is_typing = false;
                var race = Interface.readinput();
                while (!racelist.Contains(race))
                {
                    Interface.type("Invalid. Please try again. ");
                    race = Interface.readinput();
                }
                Player.race = race;
                Interface.type("You have selected " + Player.race.ToUpperFirstLetter() + ".", ConsoleColor.Magenta);
                Interface.type("Each player also has a class. You may choose from Warrior, Paladin, Mage, or Thief.");
                Interface.type("Please enter a class. ");
                var pclass = Interface.readinput();
                while (!classlist.Contains(pclass))
                {
                    Interface.type("Invalid. Please try again. ");
                    pclass = Interface.readinput();
                }
                Player.pclass = pclass;
                Interface.type("You have selected " + Player.pclass.ToUpperFirstLetter() + ".", ConsoleColor.Magenta);
                Interface.type("You are now ready to play Realm. Good luck!");
                Interface.type("Press any key to continue.", ConsoleColor.White);
                Interface.readkey();
                Map.PlayerPosition.x = 0;
                Map.PlayerPosition.y = 6;
                if (Player.race == "giant")
                    Player.hp = 15;
            }
            MainLoop();
        }
        public static void MainLoop()
        {
            while (Player.hp > 0)
            {
                if (slimecounter == 100)
                    ach.Get("100slimes");
                if (goblincounter == 100)
                    ach.Get("100goblins");
                if (banditcounter == 100)
                    ach.Get("100bandits");
                if (drakecounter == 100)
                    ach.Get("100drakes");

                var xy = Map.CoordinatesOf(typeof(Nomad));
                Map.map[xy.Item1, xy.Item2] = new Place();
                var nextNomad = Map.getRandomBlankTile();
                Map.map[nextNomad.Item1, nextNomad.Item2] = new Nomad();

                foreach (var p in Map.map)
                    if (p.GetType() == typeof(Place))
                        p.is_npc_active = false;
                var randomTile = Map.getRandomBlankTile();
                if (rand.NextDouble() <= .1)
                    Map.map[randomTile.Item1, randomTile.Item2].is_npc_active = true;

                if (devmode)
                {
                    Interface.type("Dev Powers!", ConsoleColor.DarkMagenta);
                    Player.primary = new phantasmal_claymore();
                    Player.secondary = new spectral_bulwark();
                    Player.armor = new illusory_plate();
                    Player.accessory = new void_cloak();
                    hasmap = true;
                }
                Player.applybonus();
                var enemy = new Enemy();
                Player.levelup();
                Place currPlace = Map.map[Map.PlayerPosition.x, Map.PlayerPosition.y];
                if (Player.hp > Player.maxhp)
                    Player.hp = Player.maxhp;
                if (loop_number >= 1)
                {
                    if (Combat.CheckBattle() && currPlace.getEnemyList() != null && !devmode)
                    {
                        enemy = currPlace.getEnemyList();
                        Combat.BattleLoop(enemy);
                    }
                }
                Item pc = new phantasmal_claymore();
                Item sb = new spectral_bulwark();
                Item ip = new illusory_plate();
                Item vc = new void_cloak();
                if ((Player.backpack.Contains(pc) && Player.backpack.Contains(sb) && Player.backpack.Contains(ip) && Player.backpack.Contains(vc)) || devmode)
                {
                    if (!Player.abilities.commandChars.Contains('*'))
                        Player.abilities.AddCommand(new Combat.EndtheIllusion("End the Illusion", '*'));
                    ach.Get("set");
                }
                if (devmode)
                    Interface.type(Map.PlayerPosition.x + " " + Map.PlayerPosition.y);
                if (!devmode)
                    Player.applybonus();
                else
                    Player.applydevbonus();
                if (gbooks >= 3 && !Player.abilities.commandChars.Contains('@'))
                {
                    Interface.type("Having read all of the Ramsay books, you are enlightened in the ways of Gordon Ramsay.");
                    Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
                }
                if (!devmode)
                    Interface.type(currPlace.Description);
                else
                    Interface.type(currPlace.ToString());
                var currcommands = currPlace.getAvailableCommands();
                Interface.typeOnSameLine("\r\nYour current commands are x", ConsoleColor.Cyan);
                foreach (var c in currcommands)
                {
                    Interface.typeOnSameLine(", " + c, ConsoleColor.Cyan);
                }
                Interface.type("");

                var command = Interface.readkey();

                if (command.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else if (command.KeyChar == '-' && devmode)
                {
                    var input = Interface.readinput();
                    if (input == "end")
                        End.Endgame();
                    else if (input == "combat")
                    {
                        var combat_input = Interface.readinput();
                        var etype = Type.GetType("Realm." + combat_input);
                        try
                        {
                            var e = (Enemy)Activator.CreateInstance(etype);
                            Combat.BattleLoop(e);
                        }
                        catch (ArgumentNullException)
                        {
                            Interface.type("Invalid Enemy.");
                        }
                    }
                    else if (input == "name")
                        Player.name = Interface.readinput();
                    else if (input == "additem")
                    {
                        var add_input = Interface.readinput();
                        var atype = Type.GetType("Realm." + add_input);
                        try
                        {
                            var i = (Item)Activator.CreateInstance(atype);

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

                    else if (input == "droploot")
                    {
                        if (rand.NextDouble() <= .1d)
                        {
                            Player.backpack.Add(MainItemList[rand.Next(0, MainItemList.Count - 1)]);
                            Interface.type("Obtained " + MainItemList[rand.Next(0, MainItemList.Count - 1)].name + "!", ConsoleColor.Green);
                        }
                        else
                        {
                            Interface.type("Nothing dropped.");
                        }
                    }
                    else if (input == "teleport")
                    {
                        var xcoord = Int32.Parse(Interface.readinput());
                        var ycoord = Int32.Parse(Interface.readinput());
                        Map.PlayerPosition.x = xcoord;
                        Map.PlayerPosition.y = ycoord;
                    }
                    else if (input == "level")
                    {
                        Player.level = Convert.ToInt32(Interface.readinput());
                    }
                    else if (input == "levelup")
                    {
                        Player.xp += Player.xp_next;
                    }
                    else if (input == "getach")
                    {
                        ach.Get(Interface.readinput());
                    }
                    else if (input == "suicide")
                        End.GameOver();
                }
                else
                    currPlace.handleInput(command.KeyChar);
                loop_number++;
            }
        }
    }
}
