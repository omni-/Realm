﻿using System;
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

        public static int gbooks = 0;

        public static bool raven_dead = false;
        public static bool is_theif = false;
        public static bool wkingdead = false;

        public static Realm.Player.GamePlayer Player = new Realm.Player.GamePlayer();
        public static Globals globals = new Globals();

        public static bool devmode = false;
        public static bool hasmap = false;

        public static void Tutorial()
        {
            List<string> racelist = new List<string>{"Human", "Elf", "Rockman", "Giant", "Zephyr", "Shade"};
            List<string> classlist = new List<string> { "Warrior", "Paladin", "Mage", "Thief" };
            Formatting.type("Welcome, " + Player.name + ", to Realm.");
            Formatting.type("To do anything in Realm, simply press one of the listed commands.");
            Formatting.type("When in combat, select an availible move. All damage is randomized.");
            Formatting.type("While in the backpack, simply select a number corresponding to an item. You may swap this item in or out. Make sure to equip an item once you pick it up!");
            Formatting.type("At any specified time, you may press x, then y. This will cause you to commit suicide.");
            Formatting.type("At any specified time, you may press #. Doing so will save the game.");
            Formatting.type("In Realm, every player selects a race. Each race gives its own bonuses. You may choose from Human, Elf, Rockman, Giant, Zephyr, or Shade.");
            Formatting.type("Please enter a race. ");
            string race = Console.ReadLine();
            while (!racelist.Contains(race))
            {
                Formatting.type("Invalid. Please try again. ");
                race = Console.ReadLine();
            }
            Formatting.type("You have selected " + race + ".");
            Player.race = race;
            Formatting.type("Each player also has a class. You may choose from Warrior, Paladin, Mage, or Thief.");
            Formatting.type("Please enter a class. ");
            string pclass = Console.ReadLine();
            while (!classlist.Contains(pclass))
            {
                Formatting.type("Invalid. Please try again. ");
                pclass = Console.ReadLine();
            }
            Formatting.type("You have selected " + pclass + ".");
            Player.pclass = pclass;
            Formatting.type("You are now ready to play Realm. Good luck!");
            Formatting.type("Press any key to continue.");
            Console.ReadKey();
            Globals.PlayerPosition.x = 0;
            Globals.PlayerPosition.y = 5;
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
            if (Player.primary.Equals(new wood_staff()) && Player.secondary.Equals(new slwscreen()) && Player.armor.Equals(new sonictee()) && Player.accessory.Equals(new fmBP()))
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
                    if (Combat.CheckBattle() && currPlace.getEnemyList() != null)
                    {
                        enemy = currPlace.getEnemyList();
                        BattleLoop(enemy);
                    }
                }
                if (Player.primary.Equals(new phantasmal_claymore()) && Player.secondary.Equals(new spectral_bulwark()) && Player.armor.Equals(new illusory_plate()) && Player.accessory.Equals(new void_cloak()))
                    if (!Player.abilities.commandChars.Contains('*'))
                        Player.abilities.AddCommand(new Combat.EndtheIllusion("End the Illusion", '*'));
                if (devmode)
                    Formatting.type(Globals.PlayerPosition.x + " " + Globals.PlayerPosition.y);
                if (!devmode)
                    Player.applybonus();
                else
                    Player.applydevbonus();
                if (gbooks >= 3 && !Player.abilities.commandChars.Contains('@'))
                {
                    Formatting.type("Having read all of the Ramsay books, you are enlightened in the ways of Gordon Ramsay.");
                    Formatting.type("Learned 'Hell's Kitchen'!");
                    Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
                }
                if (!devmode)
                {
                    Formatting.type("-------------------------------------", 10);
                    Formatting.type(Player.name + "(" + Player.race + ")," + " Level " + Player.level + " " + Player.pclass + ":", 10);
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
                if (command.KeyChar == 'x')
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
                else if (command.KeyChar == '-' && devmode)
                {
                    string input = Console.ReadLine();
                    if (input == "e")
                        Endgame();
                    else if (input == "c")
                    {
                        string combat_input = Console.ReadLine();
                        Type etype = Type.GetType("Realm." + combat_input);
                        Enemy e = (Enemy)Activator.CreateInstance(etype);
                        BattleLoop(e);
                    }
                    else if (input == "n")
                        Player.name = Console.ReadLine();
                    else if (input == "a")
                    {
                        string add_input = Console.ReadLine();
                        Type atype = Type.GetType("Realm." + add_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        if (Player.backpack.Count <= 10)
                            Player.backpack.Add(i);
                        else
                            Formatting.type("Not enough space.");
                        Formatting.type("Obtained '" + i.name + "'!");
                    }
                    else if (input == "p")
                    {
                        string p_input = Console.ReadLine();
                        Type atype = Type.GetType("Realm." + p_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        Player.primary = i;
                    }
                    else if (input == "t")
                    {
                        string place_input = Console.ReadLine();
                        Type ptype = Type.GetType("Realm." + place_input);
                        Place p = (Place)Activator.CreateInstance(ptype);
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
            int mana = Player.level;
            Formatting.type("You have entered combat! Ready your weapons!");
            Formatting.type("Level " + enemy.level + " " + enemy.name + ":");
            Formatting.type("-------------------------", 10);
            Formatting.type("HP: " + enemy.hp);
            Formatting.type("Attack: " + enemy.atk);
            Formatting.type("Defense: " + enemy.def);
            Formatting.type("-------------------------", 10);
            bool is_turn = enemy.spd < Player.spd;
            while (enemy.hp >= 0)
            {
                Formatting.type("//////////////////////");
                Formatting.type("Enemy HP: " + enemy.hp);
                Formatting.type("Your HP: " + Player.hp);
                Formatting.type("//////////////////////");
                if (is_turn && !Player.stunned)
                {
                    if (Player.phased)
                        Player.phased = false;
                    Formatting.type("\r\nAVAILABLE MOVES:");
                    Formatting.type("=========================", 10);
                    if (Player.fire >= 3)
                        Player.on_fire = false;
                    if (Player.on_fire)
                    {
                        Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Player.hp -= dmg;
                        Formatting.type("You take " + " fire damage.");
                    }
                    if (Player.blinded)
                        Player.blinded = false;
                    if (Player.cursed)
                    {
                        Player.hp -= Combat.Dice.roll(1, 6);
                        Formatting.type("You are cursed!");
                    }
                    int i = 0;
                    foreach (Realm.Combat.Command c in Player.abilities.commands.Values)
                    {
                        string src = "||   " + c.cmdchar + ". " + c.name;
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
                    if (ch == 'm')
                    {
                        Formatting.type("You mimc the enemy's damage!");
                        enemy.hp -= enemydmg;
                    }
                    if (!Player.blinded)
                        Player.abilities.ExecuteCommand(ch, enemy);
                    else
                        Formatting.type("You are blind!");
                    int enemyhp = oldhp - enemy.hp;
                    Formatting.type("The enemy takes " + enemyhp + " damage!");
                    if (enemy.hp <= 0)
                    {
                        Formatting.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        return;
                    }
                    if (!Player.phased)
                        is_turn = false;
                }
                else if (Player.stunned)
                {
                    Formatting.type("You are stunned!");
                    Player.stunned = false;
                    if (Player.fire >= 3)
                        Player.on_fire = false;
                    if (Player.on_fire)
                    {
                        Player.fire++;
                        int dmg = Combat.Dice.roll(1, 3);
                        Player.hp -= dmg;
                        Formatting.type("You take " + " fire damage.");
                    }
                    if (Player.cursed)
                    {
                        Player.hp -= Combat.Dice.roll(1, 6);
                        Formatting.type("You are cursed!");
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
                        Formatting.type(enemy.name + " takes " + " fire damage.");
                    }
                    if (enemy.cursed)
                    {
                        Formatting.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                    }
                    if (enemy.trapped)
                    {
                        Formatting.type("Your trap has sprung!");
                        int dmg = (5 + (Player.level / 5) + (Player.intl / 3) + Combat.Dice.roll(1, 5));
                        enemy.hp -= dmg;
                        Formatting.type(enemy.name + " takes " + dmg + "damage!");
                    }
                    int oldhp = Player.hp;
                    string ability;
                    if (!Player.guarded && !enemy.blinded)
                    {
                        enemy.attack(out ability);
                        enemydmg = oldhp - Player.hp;
                        Formatting.type(enemy.name + " used " + ability);
                        Formatting.type("You take " + enemydmg + " damage!");
                    }
                    else
                    {
                       if (Player.blinded)
                            Formatting.type(enemy.name + "is blind!");
                        else if (Player.guarded)
                            Formatting.type("Safeguard prevented damage!");
                    }
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
                    if (enemy.cursed)
                    {
                        Formatting.type(enemy.name + " is cursed!");
                        enemy.hp -= Combat.Dice.roll(1, 6);
                    }
                    if (enemy.hp <= 0)
                    {
                        Formatting.type("Your have defeated " + enemy.name + "!");
                        enemy.droploot();
                        MainLoop();
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
                Formatting.type("Enter (y) to equip this item, (d) to destroy and anything else to go back.");
                char c = Console.ReadKey().KeyChar;
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
                            Player.primary = new Item();
                            Player.backpack.Remove(i);
                        }
                        else if (i.slot == 2)
                        {
                            Player.secondary = new Item();
                            Player.backpack.Remove(i);
                        }
                        else if (i.slot == 3)
                        {
                            Player.armor = new Item();
                            Player.backpack.Remove(i);
                        }
                        else if (i.slot == 4)
                        {
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
                Formatting.type("**********Current Equipment**********");
                if (!Player.primary.Equals(default(Item)))
                    Formatting.type("Primary: " + Player.primary.name);
                else
                    Formatting.type("Primary: None.");
                if (!Player.secondary.Equals(default(Item)))
                    Formatting.type("Secondary: " + Player.secondary.name);
                else
                    Formatting.type("Secondary: None.");
                if (!Player.armor.Equals(default(Item)))
                    Formatting.type("Armor: " + Player.armor.name);
                else
                    Formatting.type("Armor: None.");
                if (!Player.accessory.Equals(default(Item)))
                    Formatting.type("Accessory: " + Player.accessory.name);
                else
                    Formatting.type("Accessory: None.");
                Formatting.type("*************************************");
                int q = 1;
                Combat.CommandTable cmd = new Combat.CommandTable();
                foreach (Item i in Player.backpack)
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
            if (magiccounter >= 1)
                Formatting.type("You recognize the magic man from the alley in Central. It is he who stands before you.");
            Formatting.type("'I am Janus.' The mand before you says. You stand in front of the protetorate, Janus. He says '" + Player.name + ", have you realized that this world is an illusion?' You nod your head. 'You will result in the Realm's demise, you must be purged. Shimmering light gathers around him, and beams of blue light blast out of him.");
            Formatting.type("I regret to say, but we must fight.");
            BattleLoop(new finalboss());
            Formatting.type("Janus defeated, the world vanishes and you both are standing on glass in a blank world of black void.");
            Formatting.type("The protectorate kneels on the ground in front of you. 'Do you truly wish to end the illusion?', he says. You look at him without uttering a word. 'Very well, I must ask of you one last favor even though the realm is no more, do not let the realm vanish from within you. Everything around you goes black. (Press any key to continue)");
            Console.ReadKey();
            Console.Clear();
            Credits();
        }
        public static void Credits()
        {
            Formatting.type("A child awakes from his sleep and looks out the window feeling fulfilled as if a story has come to a close.");
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
                if (Player.backpack.Count <= 10)
                    Player.backpack.Add(i);
                else
                    Formatting.type("Not enough space.");
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