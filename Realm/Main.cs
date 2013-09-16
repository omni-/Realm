using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public static class Main
    {
        public static int loop_number = 0;
        public static int game_state = 0;

        public static int forrestcounter = 0;
        public static int libcounter = 0;
        public static int centrallibcounter = 0;
        public static int ramseycounter = 0;
        public static int magiccounter = 0;
        public static int nlibcounter = 0;
        public static int townfolkcounter = 0;
        public static int nomadcounter = 0;

        public static int gbooks = 0;

        public static bool is_theif = false;

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
                if (devmode)
                    Formatting.type(Globals.PlayerPosition.x + " " + Globals.PlayerPosition.y);
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

            int mana = 2 + Player.level;
            Formatting.type("You have entered combat! Ready your weapons!");
            bool is_turn = enemy.spd < Player.spd;
            while (enemy.hp >= 0)
            {
                if (is_turn && !Player.stunned)
                {
                    if (Main.Player.is_phased)
                        Main.Player.is_phased = false;
                    Formatting.type(enemy.name + ":");
                    Formatting.type("-------------------------", 10);
                    Formatting.type("Enemy HP: " + enemy.hp);
                    Formatting.type("-------------------------", 10);

                    Formatting.type("\r\nAVAILABLE MOVES:");
                    Formatting.type("=========================", 10);
                    if (Main.Player.fire >= 3)
                        Main.Player.on_fire = false;
                    if (Main.Player.on_fire)
                    {
                        Main.Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Player.hp -= dmg;
                        Formatting.type("You take " + " fire damage.");
                    }
                    if (Main.Player.cursed)
                    {
                        Main.Player.hp -= Combat.Dice.roll(1, 6);
                        Formatting.type("You are cursed!");
                    }
                    int i = 0;
                    foreach (Realm.Combat.Command c in Main.Player.abilities.commands.Values)
                    {
                        string src = "||   " + c.cmdchar + ". " + c.name + "    ||";
                        Formatting.type(src, 10);
                        i++;
                    }
                    Formatting.type("=========================", 10);
                    Formatting.type("");

                    int oldhp = enemy.hp;
                    char ch = Console.ReadKey().KeyChar;
                    while (!Player.abilities.commandChars.Contains(ch))
                    {
                        Formatting.type("Invalid.");
                        Formatting.type("");
                        ch = Console.ReadKey().KeyChar;
                    }
                    while (ch != 'b' && mana <= 0)
                    {
                        Formatting.type("Out of mana!");
                        Formatting.type("");
                        ch = Console.ReadKey().KeyChar;
                    }
                    if (ch != 'b')
                        mana--;
                    Main.Player.abilities.ExecuteCommand(ch, enemy);
                    int enemyhp = oldhp - enemy.hp;
                    Formatting.type("The enemy takes " + enemyhp + " damage!");
                    if (enemy.hp <= 0)
                    {
                        Formatting.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        MainLoop();
                    }
                    if (!Main.Player.is_phased)
                        is_turn = false;
                }
                else if (Main.Player.stunned)
                {
                    Formatting.type("You are stunned!");
                    Main.Player.stunned = false;
                    if (Main.Player.fire >= 3)
                        Main.Player.on_fire = false;
                    if (Main.Player.on_fire)
                    {
                        Main.Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Player.hp -= dmg;
                        Formatting.type("You take " + " fire damage.");
                    }
                    if (Main.Player.cursed)
                    {
                        Main.Player.hp -= Combat.Dice.roll(1, 6);
                        Formatting.type("You are cursed!");
                    }
                    is_turn = false;
                }

                else if (!is_turn && !enemy.stunned)
                {
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Formatting.type(enemy.name + " takes " + " fire damage.");
                    }
                    if (enemy.is_cursed)
                    {
                        Formatting.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                    }
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
                else if (enemy.stunned)
                {
                    Formatting.type(enemy.name + " is stunned!");
                    enemy.stunned = false;
                    if (enemy.fire >= 3)
                        enemy.on_fire = false;
                    if (enemy.on_fire)
                    {
                        enemy.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        enemy.hp -= dmg;
                        Formatting.type(enemy.name + " takes " + " fire damage.");
                    }
                    if (enemy.is_cursed)
                    {
                        Formatting.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
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
        public static void Endgame()
        {
            Formatting.type("You stand in front of the protextorate, Janus. He says 'Have you realized that this world is a _____?' You nod your head and reach towards the book behind him. 'You will result in the Realm's demise, you must be wiped.");
            Formatting.type("The protectorate kneels on the ground in front of you. 'Do you truly wish to end this dream?', it says. You look at him without uttering a word. 'Very well, I must ask of you one last favor even though the realm is no more, do not let the realm vanish from within you. A child awakes from his sleep and looks out the window feeling fulfilled as if a story has come to a close")
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