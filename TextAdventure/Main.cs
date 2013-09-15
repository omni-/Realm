using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Main
    {
        public static int loop_number = 0;
        public static int game_state = 0;
        public static int forrestcounter = 0;
        public static int libcounter = 0;
        public static int centrallibcounter = 0;
        public static int ramseycounter = 0;
        public static int gbooks = 0;
        public static bool is_theif = false;
        public static bool is_phased = false;
        public static GamePlayer Player = new GamePlayer();
        public static Globals globals = new Globals();
        public static bool devmode = false;
        public static bool hasmap = false;

        public static void Tutorial()
        {
            Formatting.type("Welcome, " + Player.name + ", to Realm.");
            Formatting.type("To do anything in Realm, simply press one of the listed commands.");
            Formatting.type("When in combat, select an availible move. All damage is randomized.");
            Formatting.type("While in the backpack, simply select a number corresponding to an item. You may swap this item in or out. Make sure to equip an item once you pick it up!");
            Formatting.type("At any specified time, you may press x, then y. This will cause you to commit suicide.");
            Formatting.type("You are now ready to play Realm. Good luck!");
            Formatting.type("Press any key to continue.");
            Console.ReadKey();
            MainLoop();
        }
        public static void MainLoop()
        {
            game_state = 0;
            Place currPlace;
            if (devmode)
            {
                Player.primary = globals.phantasmal_claymore;
                Player.secondary = globals.spectral_bulwark;
                Player.armor = globals.illusory_plate;
                Player.accessory = globals.void_cloak;
                hasmap = true;
            }
            if (Player.primary.Equals(globals.phantasmal_claymore) && Player.secondary.Equals(globals.spectral_bulwark) && Player.armor.Equals(globals.illusory_plate) && Player.accessory.Equals(globals.void_cloak))
                if(!Player.abilities.commandChars.Contains('*'))
                    Player.abilities.AddCommand(new Combat.EndtheIllusion("End the Illusion", '*'));
            if (Player.primary.Equals(globals.wood_staff) && Player.secondary.Equals(globals.slwscreen) && Player.armor.Equals(globals.sonictee) && Player.accessory.Equals(globals.fmBP))
            {
                if (!Player.abilities.commandChars.Contains('/'))
                    Player.abilities.AddCommand(new Combat.ArrowsofLies("Arrows of Lies", '/'));
            }
            while (!End.IsDead)
            {
                Enemy enemy = new Enemy();
                Player.levelup();
                //if (devmode)
                //{
                //    Formatting.type("X: " + Globals.PlayerPosition.x + " Y: " + Globals.PlayerPosition.y);
                //    Formatting.type("Enter an x coord: ");
                //    Globals.PlayerPosition.x = Console.ReadKey().KeyChar;
                //    Formatting.type("Enter a y coord: ");
                //    Globals.PlayerPosition.y = Console.ReadKey().KeyChar;
                //}              
                currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                if (Player.hp > Player.maxhp)
                    Player.hp = Player.maxhp;
                if (loop_number >= 1)
                {
                    if (Combat.CheckBattle())
                    {
                        enemy = currPlace.getEnemyList();
                        BattleLoop(enemy);
                    }
                }
                if (!devmode)
                    Main.Player.applybonus();
                else
                    Main.Player.applydevbonus();
                if (!devmode)
                {
                    Formatting.type("-------------------------------------", 10);
                    Formatting.type("Level: " + Player.level, 10);
                    Formatting.type("HP: " + Player.hp + "/" + Player.maxhp, 10);
                    Formatting.type("Defense: " + Player.def, 10);
                    Formatting.type("Attack: " + Player.atk, 10);
                    Formatting.type("Speed: " + Player.spd, 10);
                    Formatting.type("Intelligence: " + Player.intl, 10);
                    Formatting.type("Gold: " + Player.g, 10);
                    Formatting.type("Exp to Level: " + (Player.xp_next - Player.xp));
                    Formatting.type("-------------------------------------", 10);
                }

                //currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                if (!devmode)
                    Formatting.type(currPlace.Description, 10);
                else
                    Formatting.type(currPlace.ToString());
                char[] currcommands = currPlace.getAvailableCommands();
                Console.Write("\r\nYour current commands are x");
                foreach (char c in currcommands)
                {
                    Console.Write(", {0}", c);
                }
                Formatting.type("");

                ConsoleKeyInfo command = Console.ReadKey();
                if (command.Key == ConsoleKey.X)
                {
                    Formatting.type("\r\nAre you sure?");
                    char surecommand = Console.ReadKey().KeyChar;
                    if (surecommand == 'y')
                    {
                        End.IsDead = true;
                        End.GameOver();
                    }
                }
                else if (command.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else
                    currPlace.handleInput(command.KeyChar);
                loop_number++;
            }
        }

        public static void BattleLoop(Enemy enemy)
        {
            game_state = 1;

            Player.applybonus();

            Formatting.type("You have entered combat! Ready your weapons!");
            bool is_turn = enemy.spd < Player.spd;
            while (enemy.hp >= 0)
            {
                if (is_turn)
                {
                    if (is_phased)
                        is_phased = false;
                    Formatting.type(enemy.name + ":");
                    Formatting.type("-------------------------", 10);
                    Formatting.type("Enemy HP: " + enemy.hp);
                    Formatting.type("-------------------------", 10);

                    Formatting.type("\r\nAVAILABLE MOVES:");
                    Formatting.type("=========================", 10);
                    int i = 0;
                    foreach (TextAdventure.Combat.Command c in Main.Player.abilities.commands.Values)
                    {
                        //Main.Player.abilities.commands.Keys[i] = (char)(i + 49);
                        string src = "||   " + c.cmdchar + ". " + c.name + "    ||";
                        Formatting.type(src, 10);
                        i++;
                    }
                    Formatting.type("=========================", 10);
                    Formatting.type("");

                    int oldhp = enemy.hp;
                    char ch = Console.ReadKey().KeyChar;
                    Main.Player.abilities.ExecuteCommand(ch, enemy);
                    int enemyhp = oldhp - enemy.hp;
                    Formatting.type("The enemy takes " + enemyhp + " damage!");
                    if (enemy.hp <= 0)
                    {
                        Formatting.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        MainLoop();
                    }
                    if (!is_phased)
                        is_turn = false;
                }
                else if (!is_turn)
                {
                    if (enemy.is_cursed)
                        enemy.hp -= 2;
                    int oldhp = Main.Player.hp;
                    string ability;
                    enemy.attack(out ability);
                    Formatting.type(enemy.name + " used " + ability);
                    Formatting.type("You take " + (oldhp - Main.Player.hp) + " damage!");
                    Formatting.type("-------------------------", 10);
                    Formatting.type("Your HP: " + Main.Player.hp);
                    Formatting.type("-------------------------", 10);
                    if (Player.hp <= 0)
                    {
                        End.IsDead = true;
                        End.GameOver();
                    }
                    is_turn = true;
                }
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
                Formatting.type(i.name);
                Formatting.type("Description: " + i.desc);
                Formatting.type("Attack Buff: " + i.atkbuff);
                Formatting.type("Defense Buff: " + i.defbuff);
                Formatting.type("Speed Buff: " + i.spdbuff);
                Formatting.type("Intelligence Buff: " + i.intlbuff);
                Formatting.type("Enter (y) to equip this item, (n) to dequip and anything else to go back.");
                char c = Console.ReadKey().KeyChar;
                switch (c)
                {
                    case 'y':
                        if (i.slot == 1)
                            Main.Player.primary = i;
                        else if (i.slot == 2)
                            Main.Player.secondary = i;
                        else if (i.slot == 3)
                            Main.Player.armor = i;
                        else if (i.slot == 4)
                            Main.Player.accessory = i;
                        break;
                    case 'n':
                        if (i.slot == 1)
                            Main.Player.primary = new Item();
                        else if (i.slot == 2)
                            Main.Player.secondary = new Item();
                        else if (i.slot == 3)
                            Main.Player.armor = new Item();
                        else if (i.slot == 4)
                            Main.Player.accessory = new Item();
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
                Formatting.type("**********Current Equipment**********");
                if (!Main.Player.primary.Equals(default(Item)))
                    Formatting.type("Primary: " + Player.primary.name);
                else
                    Formatting.type("Primary: None.");
                if (!Main.Player.secondary.Equals(default(Item)))
                    Formatting.type("Secondary: " + Player.secondary.name);
                else
                    Formatting.type("Secondary: None.");
                if (!Main.Player.armor.Equals(default(Item)))
                    Formatting.type("Armor: " + Player.armor.name);
                else
                    Formatting.type("Armor: None.");
                if (!Main.Player.accessory.Equals(default(Item)))
                    Formatting.type("Accessory: " + Player.accessory.name);
                else
                    Formatting.type("Accessory: None.");
                Formatting.type("*************************************");
                int q = 1;
                Combat.CommandTable cmd = new Combat.CommandTable();
                foreach (Item i in Main.Player.backpack)
                {
                    Formatting.type((q - 1) + ". " + i.name);
                    backpackcommand bpcmd = new backpackcommand(i.name, (char)(q + 47));
                    cmd.AddCommand(bpcmd);
                    q++;
                }
                Formatting.type("");
                char ch = Console.ReadKey().KeyChar;
                if (!cmd.commandChars.Contains(ch))
                    break;
                //if (ch == '1')
                //    cmd.ExecuteCommand('1', 1);
                cmd.ExecuteCommand(ch, Player.backpack[(int)Char.GetNumericValue(ch)]);
            }
        }
        public static void SammysAdventure()
        {

        }
        public static bool Purchase(int cost)
        {
            if (cost > Player.g)
            {
                Formatting.type("You don't have enough gold.");
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
                Player.backpack.Add(i);
                return true;
            }
            else
            {
                Formatting.type("You don't have enough gold.");
                return false;
            }
        }
    }
}