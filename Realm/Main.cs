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

        public static bool raven_dead = false;
        public static bool is_theif = false;
        public static bool wkingdead = false;

        public static Realm.Player.GamePlayer Player = new Realm.Player.GamePlayer();
        public static Globals globals = new Globals();

        public static bool devmode = false;
        public static bool hasmap = false;

        public static string devstring = "__dev__";

        public static void Tutorial()
        {
            List<string> racelist = new List<string>{ "human", "elf", "rockman", "giant", "zephyr", "shade" };
            List<string> classlist = new List<string> { "warrior", "paladin", "mage", "thief" };
            Interface.type("Welcome, " + Player.name + ", to Realm.");
            Interface.type("To do anything in Realm, simply press one of the listed commands.");
            Interface.type("Make sure to visit every library! They offer many valuable abilities as well as experience.");
            Interface.type("When in combat, select an availible move. All damage is randomized. Mana is refilled after each fight.");
            Interface.type("While in the backpack, simply select a number corresponding to an item. You may swap this item in or out. Make sure to equip an item once you pick it up!");
            Interface.type("At any specified time, you may press x, then y. This will cause you to commit suicide.");
            Interface.type("At any specified time, you may press #. Doing so will save the game.");
            Interface.type("In Realm, every player selects a race. Each race gives its own bonuses. You may choose from Human, Elf, Rockman, Giant, Zephyr, or Shade.");
            Interface.type("Please enter a race. ");
            string race = Interface.readinput();
            while (!racelist.Contains(race))
            {
                Interface.type("Invalid. Please try again. ");
                race = Interface.readinput();
            }
            Player.race = race;
            Interface.type("You have selected " + Interface.ToUpperFirstLetter(Player.race) + ".");
            Interface.type("Each player also has a class. You may choose from Warrior, Paladin, Mage, or Thief.");
            Interface.type("Please enter a class. ");
            string pclass = Interface.readinput();
            while (!classlist.Contains(pclass))
            {
                Interface.type("Invalid. Please try again. ");
                pclass = Interface.readinput();
            }
            Player.pclass = pclass;
            Interface.type("You have selected " + Interface.ToUpperFirstLetter(Player.pclass) + ".");
            Interface.type("You are now ready to play Realm. Good luck!");
            Interface.type("Press any key to continue.");
            Interface.readkey();
            Globals.PlayerPosition.x = 0;
            Globals.PlayerPosition.y = 6;
            if (Player.race == "giant")
                Player.hp = 15;
            MainLoop();
        }
        public static void MainLoop()
        {
            game_state = 0;
            Place currPlace;
            if (devmode)
            {
                Player.primary = new phantasmal_claymore();
                Player.secondary = new spectral_bulwark();
                Player.armor = new illusory_plate();
                Player.accessory = new void_cloak();
                hasmap = true;
            }
            Player.applybonus();
            while (Player.hp > 0)
            {
                Enemy enemy = new Enemy();
                Player.levelup();
                currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                if (Player.hp > Player.maxhp)
                    Player.hp = Player.maxhp;
                if (loop_number >= 1)
                {
                    if (Combat.CheckBattle() && currPlace.getEnemyList() != null)
                    {
                        enemy = currPlace.getEnemyList();
                        BattleLoop(enemy);
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
                    Interface.type(Globals.PlayerPosition.x + " " + Globals.PlayerPosition.y);
                if (!devmode)
                    Player.applybonus();
                else
                    Player.applydevbonus();
                if (gbooks >= 3 && !Player.abilities.commandChars.Contains('@'))
                {
                    Interface.type("Having read all of the Ramsay books, you are enlightened in the ways of Gordon Ramsay.");
                    Interface.type("Learned 'Hell's Kitchen'!");
                    Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
                }
                if (!devmode)
                {
                    Interface.type("-------------------------------------");
                    Interface.type(Player.name + "(" + Interface.ToUpperFirstLetter(Player.race) + ")," + " Level " + Player.level + " " + Interface.ToUpperFirstLetter(Player.pclass) + ":");
                    Interface.type("HP: " + Player.hp + "/" + Player.maxhp);
                    Interface.type("Attack: " + Player.atk + " / Defense: " + Player.def + " / Speed: " + Player.spd + " / Intelligence: " + Player.intl);
                    Interface.type("Mana: " + (1 + (Player.intl / 10)));
                    Interface.type("Gold: " + Player.g + " / Exp to Level: " + (Player.xp_next - Player.xp));
                    Interface.type("-------------------------------------");
                }

                //currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                if (!devmode)
                    Interface.type(currPlace.Description);
                else
                    Interface.type(currPlace.ToString());
                char[] currcommands = currPlace.getAvailableCommands();
                Console.Write("\r\nYour current commands are x");
                foreach (char c in currcommands)
                {
                    Console.Write(", {0}", c);
                }
                Interface.type("");

                ConsoleKeyInfo command = Interface.readkey();
                if (command.KeyChar == 'x')
                {
                    Interface.type("\r\nAre you sure?");
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
                        Endgame();
                    else if (input == "c")
                    {
                        string combat_input = Interface.readinput();
                        Type etype = Type.GetType("Realm." + combat_input);
                        Enemy e = (Enemy)Activator.CreateInstance(etype);
                        BattleLoop(e);
                    }
                    else if (input == "n")
                        Player.name = Interface.readinput();
                    else if (input == "a")
                    {
                        string add_input = Interface.readinput();
                        Type atype = Type.GetType("Realm." + add_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        if (Player.backpack.Count <= 10)
                            Player.backpack.Add(i);
                        else
                            Interface.type("Not enough space.");
                        Interface.type("Obtained '" + i.name + "'!");
                    }
                    else if (input == "p")
                    {
                        string p_input = Interface.readinput();
                        Type atype = Type.GetType("Realm." + p_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        Player.primary = i;
                    }
                    else if (input == "t")
                    {
                        string place_input = Interface.readinput();
                        Type ptype = Type.GetType("Realm." + place_input);
                        Place p = (Place)Activator.CreateInstance(ptype);
                    }
                    else if (input == "l")
                    {
                        int level = Convert.ToInt32(Interface.readinput());
                    }
                }
                else
                    currPlace.handleInput(command.KeyChar);
                loop_number++;
            }
        }
        public static void BattleLoop(Enemy enemy)
        {
            game_state = 1;

            Player.applybonus();

            int enemydmg = 0;
            int mana = 1 + Player.intl / 10;
            Interface.type("You have entered combat! Ready your weapons!");
            Interface.type("Level " + enemy.level + " " + enemy.name + ":");
            Interface.type("-------------------------");
            Interface.type("HP: " + enemy.hp);
            Interface.type("Attack: " + enemy.atk);
            Interface.type("Defense: " + enemy.def);
            Interface.type("-------------------------");
            bool is_turn = enemy.spd < Player.spd;
            while (enemy.hp >= 0)
            {
                Interface.type("//////////////////////");
                Interface.type("Your HP: " + Player.hp);
                Interface.type("Your Mana: " + mana);
                Interface.type("----------------------");
                Interface.type("Enemy HP: " + enemy.hp);
                Interface.type("//////////////////////");
                if (is_turn && !Player.stunned)
                {
                    if (Player.phased)
                        Player.phased = false;
                    Interface.type("\r\nAVAILABLE MOVES:");
                    Interface.type("=========================");
                    int i = 0;
                    foreach (Realm.Combat.Command c in Player.abilities.commands.Values)
                    {
                        string src = "||   " + c.cmdchar + ". " + c.name;
                        Interface.type(src);
                        i++;
                    }
                    Interface.type("=========================");
                    Interface.type("");

                    if (Player.fire >= 3)
                        Player.on_fire = false;
                    if (Player.on_fire)
                    {
                        Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Player.hp -= dmg;
                        Interface.type("You take " + dmg + " fire damage.");
                    }
                    if (Player.blinded)
                        Player.blinded = false;
                    if (Player.cursed)
                    {
                        Player.hp -= Combat.Dice.roll(1, 6);
                        Interface.type("You are cursed!");
                    }

                    int oldhp = enemy.hp;
                    char ch = Interface.readkey().KeyChar;
                    while (!Player.abilities.commandChars.Contains(ch))
                    {
                        Interface.type("Invalid.");
                        Interface.type("");
                        ch = Interface.readkey().KeyChar;
                    }
                    while (ch != 'b' && mana <= 0)
                    {
                        Interface.type("Out of mana!");
                        Interface.type("");
                        ch = Interface.readkey().KeyChar;
                    }
                    if (ch != 'b')
                        mana--;
                    if (ch == 'm')
                    {
                        Interface.type("You mimc the enemy's damage!");
                        enemy.hp -= enemydmg;
                    }
                    if (!Player.blinded)
                        Player.abilities.ExecuteCommand(ch, enemy);
                    else
                    {
                        Interface.type("You are blind!");
                        if (Combat.Dice.roll(1, 10) == 1)
                        {
                            Interface.type("By some miracle, you manage to hit them!");
                            Player.abilities.ExecuteCommand(ch, enemy);
                            int  blindenemyhp = oldhp - enemy.hp;
                            Interface.type("The enemy takes " + blindenemyhp + " damage!");
                        }
                    }
                    int enemyhp = oldhp - enemy.hp;
                    Interface.type("The enemy takes " + enemyhp + " damage!");
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        Player.levelup();
                        return;
                    }
                    if (!Player.phased)
                        is_turn = false;
                }
                else if (Player.stunned)
                {
                    Interface.type("You are stunned!");
                    Player.stunned = false;
                    if (Player.fire >= 3)
                        Player.on_fire = false;
                    if (Player.on_fire)
                    {
                        Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Player.hp -= dmg;
                        Interface.type("You take " + dmg + " fire damage.");
                    }
                    if (Player.cursed)
                    {
                        Player.hp -= Combat.Dice.roll(1, 6);
                        Interface.type("You are cursed!");
                    }
                    is_turn = false;
                }

                else if (!is_turn && !enemy.stunned)
                {
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (Player.guard >= 2)
                        Player.guarded = false;
                    if (Player.guarded)
                        Player.guard++;
                    if (enemy.blinded)
                        enemy.blinded = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " fire damage.");
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!");
                            enemy.droploot();
                            Player.levelup();
                            return;
                        }
                    }
                    if (enemy.cursed)
                    {
                        Interface.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!");
                            enemy.droploot();
                            Player.levelup();
                            return;
                        }
                    }
                    if (enemy.trapped)
                    {
                        Interface.type("Your trap has sprung!");
                        int dmg = (Player.level / 5) + (Player.intl / 3) + Combat.Dice.roll(1, 5);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " damage!");
                        if (enemy.hp <= 0)
                        {
                            Interface.type("Your have defeated " + enemy.name + "!");
                            enemy.droploot();
                            Player.levelup();
                            return;
                        }
                    }
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        Player.levelup();
                        return;
                    }
                    int oldhp = Player.hp;
                    string ability;
                    if (!Player.guarded && !enemy.blinded)
                    {
                        enemy.attack(out ability);
                        enemydmg = oldhp - Player.hp;
                        Interface.type(enemy.name + " used " + ability);
                        Interface.type("You take " + enemydmg + " damage!");
                    }
                    else
                    {
                        if (Player.blinded)
                        {
                            Interface.type(enemy.name + "is blind!");
                            if (Combat.Dice.roll(1, 10) == 1)
                            {
                                Interface.type("By some miracle, " + enemy.name + " manages to hit you!");
                                enemy.attack(out ability);
                                enemydmg = oldhp - Player.hp;
                                Interface.type(enemy.name + " used " + ability);
                                Interface.type("You take " + enemydmg + " damage!");
                            }
                        }
                        else if (Player.guarded)
                            Interface.type("Safeguard prevented damage!");
                    }
                    if (Player.hp <= 0)
                        End.GameOver();
                    is_turn = true;
                }
                else if (enemy.stunned)
                {
                    Interface.type(enemy.name + " is stunned!");
                    enemy.stunned = false;
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Interface.type(enemy.name + " takes " + dmg + " fire damage.");
                    }
                    if (enemy.cursed)
                    {
                        Interface.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                    }
                    if (enemy.hp <= 0)
                    {
                        Interface.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        Player.levelup();
                        return;
                    }
                    is_turn = true;

                }
            }
        }
        public static void CaveLoop()
        {
            caveplace currPlace = new caveplace();
            Player.applybonus();
            while (Player.hp > 0)
            {
                Enemy enemy = new Enemy();
                Player.levelup();
                currPlace = Globals.cavemap[Globals.CavePosition];
                if (Player.hp > Player.maxhp)
                    Player.hp = Player.maxhp;
                if (Combat.CheckBattle() && currPlace.getEnemyList() != null)
                {
                    enemy = currPlace.getEnemyList();
                    BattleLoop(enemy);
                }
                if (!devmode)
                    Player.applybonus();
                else
                    Player.applydevbonus();
                if (!devmode)
                {
                    Interface.type("-------------------------------------");
                    Interface.type(Player.name + "(" + Player.race + ")," + " Level " + Player.level + " " + Player.pclass + ":");
                    Interface.type("HP: " + Player.hp + "/" + Player.maxhp);
                    Interface.type("Attack: " + Player.atk + " / Defense: " + Player.def + " / Speed: " + Player.spd + " / Intelligence: " + Player.intl);
                    Interface.type("Mana: " + (1 + (Player.intl / 10)));
                    Interface.type("Gold: " + Player.g + " / Exp to Level: " + (Player.xp_next - Player.xp));
                    Interface.type("-------------------------------------");
                }
                if (!devmode)
                    Interface.type(currPlace.Description);
                else
                    Interface.type(currPlace.ToString());
                char[] currcommands = currPlace.getAvailableCommands();
                Console.Write("\r\nYour current commands are x");
                foreach (char c in currcommands)
                {
                    Console.Write(", {0}", c);
                }
                Interface.type("");

                ConsoleKeyInfo command = Interface.readkey();
                if (command.KeyChar == 'x')
                {
                    Interface.type("\r\nAre you sure?");
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
                        Endgame();
                    else if (input == "c")
                    {
                        string combat_input = Interface.readinput();
                        Type etype = Type.GetType("Realm." + combat_input);
                        Enemy e = (Enemy)Activator.CreateInstance(etype);
                        BattleLoop(e);
                    }
                    else if (input == "n")
                        Player.name = Interface.readinput();
                    else if (input == "a")
                    {
                        string add_input = Interface.readinput();
                        Type atype = Type.GetType("Realm." + add_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        if (Player.backpack.Count <= 10)
                            Player.backpack.Add(i);
                        else
                            Interface.type("Not enough space.");
                        Interface.type("Obtained '" + i.name + "'!");
                    }
                    else if (input == "p")
                    {
                        string p_input = Interface.readinput();
                        Type atype = Type.GetType("Realm." + p_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        Player.primary = i;
                    }
                    else if (input == "t")
                    {
                        string place_input = Interface.readinput();
                        Type ptype = Type.GetType("Realm." + place_input);
                        Place p = (Place)Activator.CreateInstance(ptype);
                    }
                }
                else
                    currPlace.handleInput(command.KeyChar);
                loop_number++;
            }
        }
        public class backpackcommand : Combat.Command
        {
            public backpackcommand(string aname, char cmd): base(aname, cmd)
            {
            }
            public override bool Execute(object Data)
            {
                Item i = (Item)Data;
                Interface.type(i.name);
                Interface.type("Description: " + i.desc);
                Interface.type("Attack Buff: " + i.atkbuff + " / Defense Buff: " + i.defbuff + " / Speed Buff: " + i.spdbuff + " / Intelligence Buff: " + i.intlbuff);
                if (i.slot == 1)
                    Interface.type("Slot: Primary");
                else if (i.slot == 2)
                    Interface.type("Slot: Secondary");
                else if (i.slot == 3)
                    Interface.type("Slot: Armor");
                else if (i.slot == 4)
                    Interface.type("Slot: Accessory");
                Interface.type("Tier: " + i.tier);
                Interface.type("Enter (y) to equip this item, (d) to destroy and anything else to go back.");
                char c = Interface.readkey().KeyChar;
                switch (c)
                {
                    case 'y':
                        if (i.slot == 1)
                            Player.primary = i;
                        else if (i.slot == 2)
                            Player.secondary = i;
                        else if (i.slot == 3)
                            Player.armor = i;
                        else if (i.slot == 4)
                            Player.accessory = i;
                        break;
                    case 'd':
                        if (i.slot == 1)
                        {
                            if (Player.primary == i)
                                Player.primary = new Item();
                            Player.backpack.Remove(i);
                        }
                        else if (i.slot == 2)
                        {
                            if (Player.secondary == i)
                                Player.secondary = new Item();
                            Player.backpack.Remove(i);
                        }
                        else if (i.slot == 3)
                        {
                            if (Player.armor == i)
                                Player.armor = new Item();
                            Player.backpack.Remove(i);
                        }
                        else if (i.slot == 4)
                        {
                            if (Player.accessory == i)
                                Player.accessory = new Item();
                            Player.backpack.Remove(i);
                        }
                        break;
                    default:
                        return false;
                }
                return true;
            }
        }
        public static void BackpackLoop()
        {
            bool loopcontrol = true;
            while(loopcontrol)
            {
                Interface.type("**********Current Equipment**********");
                Interface.type("Primary: " + Player.primary.name);
                Interface.type("Secondary: " + Player.secondary.name);
                Interface.type("Armor: " + Player.armor.name);
                Interface.type("Accessory: " + Player.accessory.name);
                Interface.type("*************************************");
                int q = 1;
                Combat.CommandTable cmd = new Combat.CommandTable();
                foreach (Item i in Player.backpack)
                {
                    Interface.type((q - 1) + ". " + i.name);
                    backpackcommand bpcmd = new backpackcommand(i.name, (char)(q + 47));
                    cmd.AddCommand(bpcmd);
                    q++;
                }
                Interface.type("");
                char ch = Interface.readkey().KeyChar;
                if (!cmd.commandChars.Contains(ch))
                    break;
                //if (ch == '1')
                //    cmd.ExecuteCommand('1', 1);
                cmd.ExecuteCommand(ch, Player.backpack[(int)Char.GetNumericValue(ch)]);
            }
        }
        public static void Endgame()
        {
            if (magiccounter >= 1)
                Interface.type("You recognize the magic man from the alley in Central. It is he who stands before you.");
            Interface.type("'I am Janus.' The mand before you says. You stand in front of the protetorate, Janus. He says '" + Player.name + ", have you realized that this world is an illusion?' You nod your head. 'You will result in the Realm's demise, you must be purged. Shimmering light gathers around him, and beams of blue light blast out of him.");
            Interface.type("I regret to say, but we must fight.");
            BattleLoop(new finalboss());
            Interface.type("Janus defeated, the world vanishes and you both are standing on glass in a blank world of black void.");
            Interface.type("The protectorate kneels on the ground in front of you. 'Do you truly wish to end the illusion?', he says. You look at him without uttering a word. 'Very well, I must ask of you one last favor even though the realm is no more, do not let the realm vanish from within you. Everything around you goes black. (Press any key to continue)");
            Interface.readkey();
            Console.Clear();
            Credits();
        }
        public static void Credits()
        {
            Interface.type("A child awakes from his sleep and looks out the window feeling fulfilled as if a story has come to a close.");
            Interface.type("Press any key to continue.");
            Interface.readkey();
            Console.Clear();
            Interface.type("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Interface.type("----------------------Credits---------------------");
            Interface.type("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Interface.type("__________________________________________________");
            Interface.type("Executive Developer: Cooper Teixeira");
            Interface.type("__________________________________________________");
            Interface.type("Secondary Developer: Giorgio Lo");
            Interface.type("__________________________________________________");
            Interface.type("Executive Producer: Cooper Teixeira");
            Interface.type("__________________________________________________");
            Interface.type("Co-Producer: Giorgio Lo");
            Interface.type("__________________________________________________");
            Interface.type("Lead Content Designer: Giorgio Lo");
            Interface.type("__________________________________________________");
            Interface.type("Secondary Content Designer: Cooper Teixeira");
            Interface.type("__________________________________________________");
            Interface.type("Chief Tester: Rosemary Rogal");
            Interface.type("__________________________________________________");
            Interface.type("Lead Art Designer: Rosemary Rogal");
            Interface.type("__________________________________________________");
            Interface.type("Secondary Art Designer: Giorgio Lo");
            Interface.type("__________________________________________________");
            Interface.type("Secondary Art Designer: Cooper Teixeira");
            Interface.type("__________________________________________________");
            Interface.type("Concept Artist: Rosemary Rogal");
            Interface.type("__________________________________________________");
            Interface.type("Special thanks to:");
            Interface.type("- Steve Teixeira");
            Interface.type("- Ryan Teixeira");
            Interface.type("- Paul Pfenning");
            Interface.type("- Charlie Catino");
            Interface.type("- Bradley Lignoski");
            Interface.type("- Alexander Pfenning");
            Interface.type("- Ben Boyd");
            Interface.type("__________________________________________________");
            Interface.type("Copyright(c) 2013");
            Interface.type("Press any key to continue.");
            Interface.readkey();
            Environment.Exit(0);
        }
        public static void SammysAdventure()
        {

        }
        public static bool Purchase(int cost)
        {
            if (cost > Player.g)
            {
                Interface.type("You don't have enough gold.");
                return false;
            }
            else
            {
                Player.g -= cost;
                return true;
            }
        }
        public static bool Purchase(int cost, Item i)
        {
            if (cost <= Player.g)
            {
                Player.g -= cost;
                if (Player.backpack.Count <= 10)
                    Player.backpack.Add(i);
                else
                    Interface.type("Not enough space.");
                return true;
            }
            else
            {
                Interface.type("You don't have enough gold.");
                return false;
            }
        }
    }
}