// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/27/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

namespace Realm
{
    public class Place
    {
        public bool is_npc_active;

        private Merchant m;

        protected virtual string GetDesc()
        {
            return "You are smack-dab in the middle of nowhere.";
        }

        public string Description
        {
            get { return GetDesc(); }
            set { }
        }

        public virtual Enemy getEnemyList()
        {
            var templist = new List<Enemy>();
            templist.Add(new Slime());
            if (Main.Player.level >= 3)
                templist.Add(new Goblin());
            if (Main.Player.level >= 5)
                templist.Add(new Bandit());
            if (Main.Player.level >= 10)
                templist.Add(new Drake());
            var randint = Main.rand.Next(1, templist.Count + 1);

            return templist[randint - 1];
        }

        public virtual char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            if (Main.Player.backpack.Count > 0)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            templist.Add('#');
            templist.Add('v');
            return templist.ToArray<char>();
        }

        public void handleInput(char input)
        {
            var inputHandled = false;
            while (!inputHandled)
            {
                inputHandled = _handleInput(input);
                if (!inputHandled)
                {
                    Interface.type("Invalid.");
                    Interface.type("");
                    input = Interface.readkey().KeyChar;
                }
            }
        }

        protected virtual bool _handleInput(char input)
        {
            switch (input)
            {
                case 'n':
                    if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                        Map.PlayerPosition.y += 1;
                    break;
                case 'v':
                    Interface.typeStats();
                    break;
                case 'e':
                    if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                        Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    if (Map.PlayerPosition.y > 0)
                        Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    if (Map.PlayerPosition.x > 0)
                        Map.PlayerPosition.x -= 1;
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'i':
                    if (is_npc_active)
                        m.Interact();
                    break;
                default:
                    return false;
            }
            return true;
        }

        public void CreateRandomNPC()
        {
            Description = "You run into a travelling merchant. He has a few pieces of gear for sale.";
            List<Item> tempitemlist = new List<Item>(), forsale = new List<Item>();
            tempitemlist.AddRange(Main.MainItemList.Where(i => i.tier >= 5));
            for (var i = 0; i < 4; i++)
                forsale.Add(tempitemlist[Main.rand.Next(0, tempitemlist.Count - 1)]);
            m = new Merchant(Map.GetNPCName(), "Hello. Have a look at my wares,", "The merchant thanks you.",
                "You leave.",
                new Dictionary<char, Item> {{'1', forsale[0]}, {'2', forsale[1]}, {'3', forsale[2]}, {'4', forsale[3]}});
        }
    }

    public class WKingdom : Place
    {
        protected override string GetDesc()
        {
            if (Main.wkingdead)
                return "There's nothing here. r to leave.";
            if (Main.wkingcounter >= 1) return "King: 'Back so soon? Do you want to fight now, coward?'";
            Main.wkingcounter++;
            return
                "King: \"Come, for I plan to give you the ultimate gift, eternal respite.\"\r\nYou're not sure why he has called you but you don't like it. The Western King approaches and unsheathes his blade emitting a strong aura of bloodlust. He seems to have powers far beyond anything you can imagine. Fight(f) or run(r)?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            return !Main.wkingdead ? new[] {'f', 'r', 'v', '#'} : new[] {'r', 'v', '#'};
        }

        protected override bool _handleInput(char input)
        {
            if (!Main.wkingdead)
            {
                switch (input)
                {
                    case 'v':
                        Interface.typeStats();
                        break;
                    case 'f':
                        Combat.BattleLoop(new WesternKing());
                        Interface.type("Somehow, you managed to beat the Western King. *ahem* Cheater *ahem*");
                        Main.wkingdead = true;
                        break;
                    case 'r':
                        Main.ach.Get("wking");
                        Interface.type("You escaped the Western King, but you're pretty damn lost now.");
                        Map.PlayerPosition.y -= 2;
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
                        Interface.type("You leave.");
                        Map.PlayerPosition.y -= 1;
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
            return
                "You find yourself in a forest, that bizarrely appears to wrap itself around you, like a fun house mirror. You are more lost than that time you were on a road trip and your phone died so you had no GPS. Do you want to travel east(e), north(n), backpack(b), or search the area(z)?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('z');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'z':
                    if (Main.forrestcounter == 0)
                    {
                        Interface.type(
                            "You decide to look around. You find a trail leading to a clearing. Once in the  clearing, you see a suit of cardboard armor held together with duct tape, a refrigerator box, and a cardboad tube. Pick them up? Your current commands are y, n");
                        Interface.type("");
                        var tempinput = Interface.readkey().KeyChar;
                        switch (tempinput)
                        {
                            case 'y':
                                Interface.type(
                                    "Amid the -trash- gear on the ground, you find a tile with the letter 'G' on it.");
                                if (Main.Player.backpack.Count <= 7)
                                {
                                    Main.Player.backpack.Add(new cardboard_armor());
                                    Interface.type("Obtained 'Cardboard Armor'!", ConsoleColor.Green);
                                    Main.Player.backpack.Add(new cardboard_sword());
                                    Interface.type("Obtained 'Cardboard Shield'!", ConsoleColor.Green);
                                    Main.Player.backpack.Add(new cardboard_shield());
                                    Interface.type("Obtained 'Cardboard Shield'!", ConsoleColor.Green);
                                    Main.ach.Get("cardboard");
                                    Main.forrestcounter++;
                                }
                                else
                                    Interface.type("Not enough space!");
                                break;
                            case 'n':
                                Interface.type("\r\nLoser.");
                                break;
                        }
                    }
                    else
                    {
                        Interface.type("You've already been here!");
                    }
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You come across a river town centered around a massive well full of magically enriched electrolyte water. And the good stuff. Do you want to go north(n), south(s), east(e), west(w), backpack(b), or visit the town(t)";
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('t');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 't':
                    Interface.type("There is an inn(i) and an arms dealer(a). Or you can investigate the well(w).");
                    Interface.type("");
                    var tempinput = Interface.readkey().KeyChar;
                    switch (tempinput)
                    {
                        case 'i':
                            var ik = new Innkeeper("Joseph", "Greetings. Would you like to stay at the inn?",
                                "Your health has been fully restored, although you suspect you have lice.",
                                "You leave the grimy inn.");
                            ik.Interact();
                            break;
                        case 'a':
                            var m = new Merchant("Caelan",
                                "\"Hello. I have for sale a high quality ring and a staff of the highest tier.\" You decide he isn't the most trustworthy man.",
                                "The price tag on the staff has an 'o' burned into it.",
                                "You decide the man is far too sleazy.",
                                new Dictionary<char, Item> {{'1', new plastic_ring()}, {'2', new wood_staff()}});
                            m.Interact();
                            break;
                        case 'w':
                            Interface.type("Do you want to look inside the well?(y/n)");
                            var ___tempinput = Interface.readkey().KeyChar;
                            switch (___tempinput)
                            {
                                case 'y':
                                    Interface.type("You fall in and drown.");
                                    End.GameOver();
                                    break;
                                case 'n':
                                    Interface.type("You leave.");
                                    break;
                            }
                            break;
                    }
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You arrive at a seaside port bustling with couriers and merchants. Do you want to go to the arms dealer(a), the library(l), or inn(i)?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'i':
                    var ik = new Innkeeper("Johnny", "\"Greetings. Welcome to the inn.\"",
                        "Your back hurts from the prison mattress you slept on, but at least you're rested. You see the letter 'd' in the coffe grounds from your espresso as you leave.",
                        "You decide not to sleep here");
                    ik.Interact();
                    break;
                case 'a':
                    var m = new Merchant("Hans",
                        "The merchant greets you in a pleasant German accent. He's unfortunately mostly out of stock.",
                        "He thanks you graciously as you struggle to understand him.",
                        "You decide there's nothing here for you.",
                        new Dictionary<char, Item> {{'1', new iron_lance()}, {'2', new iron_rapier()}});
                    m.Interact();
                    break;
                case 'l':
                    if (Main.libcounter == 0)
                    {
                        Interface.type(
                            "You see a massive building with columns the size of a house. This is obivously the town's main attraction. Nerds are streaming in and out like a river. You try to go inside, but you're stopped at the door. The enterance fee is 3 g. Pay? (y/n)");
                        var ___tempinput = Interface.readkey().KeyChar;
                        switch (___tempinput)
                        {
                            case 'y':
                                if (Player.Purchase(3))
                                {
                                    Main.Player.xp += 10;
                                    Interface.type(
                                        "You enter the library. Before you lays a vast emporium of knowledge. You scratch your butt, then look for some comic books or something. 3 books catch your eye. 'The Wizard's Lexicon'(a), 'The Warrior's Code'(b), and 'Codex Pallatinus'(c). Which do you read?");
                                    switch (Interface.readkey().KeyChar)
                                    {
                                        case 'a':
                                            Interface.type(
                                                "You read of the maegi of old. As you flip through, something catches your eye. You see what looks to be ancient writing, but you somehow understand it.");
                                            Main.Player.abilities.AddCommand(new Combat.EnergyOverload(
                                                "Energy Overload", 'e'));
                                            break;
                                        case 'b':
                                            Interface.type(
                                                "You pore over the pages, and see a diagram of an ancient technique, lost to the ages.");
                                            Main.Player.abilities.AddCommand(new Combat.BladeDash("Blade Dash", 'd'));
                                            break;
                                        case 'c':
                                            Interface.type(
                                                "You squint your eyes to see the tiny text. This tome convinces you of the existence of Lord Luxferre, the Bringer of Light. The book teaches you the importance of protecting others.");
                                            Main.Player.abilities.AddCommand(new Combat.HolySmite("Holy Smite", 'h'));
                                            break;
                                    }
                                }
                                break;
                            case 'n':
                                Interface.type("You leave.");
                                break;
                        }
                        Main.libcounter++;
                    }
                    else
                        Interface.type("The library is closed.");
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You arrive at a small town situated between two valleys. Do you want to visit the town(t) or head to the inn(i)?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('i');
            templist.Add('t');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'i':
                    var ik = new Innkeeper("Olaf", "THe innkeeper gruffly greets you and offers his cheapest room.",
                        "The mattress smelled of mildew, and now you do too.",
                        "You are taken aback by his taciturn attitude and leave.");
                    ik.Interact();
                    break;
                case 't':
                    Interface.type(
                        "You visit the town. You may choose to visit with the townsfolk(t), or head to the artificer(a).");
                    switch (Interface.readkey().KeyChar)
                    {
                        case 'a':
                            var m = new Merchant("Ji",
                                "You see a stooping old man selling magically charged rings. He offers you one.",
                                "He smiles weakly and thanks you.", "He looks a little downtrodden.",
                                new Dictionary<char, Item> {{'1', new iron_band()}});
                            m.Interact();
                            break;
                        case 't':
                            Interface.type(
                                "You talk to a villager. He muses about the fact that sometimes, reality doesn't feel real at all. Puzzled by his comment, you walk away.");
                            Interface.type(
                                "A paper flaps out of his cloak as he walks away. On it is nothing but the letter 'w'.");
                            break;
                        default:
                            break;
                    }
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You find yourself at the foot of a mountain. There is a village not far off. Do you wish to go there?(y to enter)";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('y');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'y':
                    Interface.type("You see a library(l) and a weapon shop(w). Which do you wish to enter?");
                    switch (Interface.readkey().KeyChar)
                    {
                        case 'l':
                            if (Main.ramsaycounter == 0)
                            {
                                Main.Player.xp += 10;
                                Interface.type(
                                    "There are three books. Do you wish to read Climbing Safety (c), Solomon's Answer (s), or Gordon Ramsay: A Biology (g)");
                                switch (Interface.readkey().KeyChar)
                                {
                                    case 'c':
                                        Interface.type(
                                            "Climbing mountains requires absolute safety. Rule #1: Don't Fall(.....this book seems pretty thick. Do you wish to continue reading?(y/n))");
                                        switch (Interface.readkey().KeyChar)
                                        {
                                            case 'y':
                                                Interface.type(
                                                    "As you silently become informed about safety, you notice that a segment of the wall is opening itself up to your far right. Do you wish to enter?(y/n)");
                                                switch (Interface.readkey().KeyChar)
                                                {
                                                    case 'y':
                                                        Interface.type(
                                                            "You try to enter, but the door requires a password.");
                                                        if (Interface.readinput() == "don't fall")
                                                        {
                                                            Interface.type(
                                                                "You open the door to find a dark room with a suspicious figure conducting suspicious rituals. The man looks flustered and says 'Nice day isn't it?'. As you wonder what anyone would be doing in such a dark room, the man edges his way to the entrance and dashes outside. He forgot to take his book with him. Do you wish to take it? (y/n) ");
                                                            switch (Interface.readkey().KeyChar)
                                                            {
                                                                case 'y':
                                                                    Main.Player.abilities.AddCommand(
                                                                        new Combat.ConsumeSoul("Consume Soul", 'u'));
                                                                    break;
                                                                case 'n':
                                                                    Interface.type(
                                                                        "You remember that you are an exemplary member of society and that you will by no means touch another's belongings without their consent. You leave the room like the good man you are.");
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                            Interface.type("Password incorrect.");
                                                        break;
                                                    case 'n':
                                                        Interface.type(
                                                            "You did not see anything out of the ordinary. You have never seen anything out of the ordinary. As you leave you makes sure to shut the door behind you because you did not see anything out of the ordinary.");
                                                        break;
                                                }
                                                break;
                                            case 'n':
                                                Interface.type(
                                                    "You believe that you already know enough about safety. Put the book back in it's spot in the bookshelf?(y/n)");
                                                switch (Interface.readkey().KeyChar)
                                                {
                                                    case 'y':
                                                        Interface.type(
                                                            "You insert the book back in it's righteous position. You feel good about doing a good deed.");
                                                        break;
                                                    case 'n':
                                                        Main.Player.hp -= 2;
                                                        Interface.type(
                                                            "You are trash. You are the pondscum of society. Repent and pay with your life. You take 2 damage.",
                                                            ConsoleColor.Red);
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                    case 's':
                                        Interface.type(
                                            "You don't know how to read this language, however you do find a crumpled up map of the realm in the back of the book.");
                                        Interface.type("Obtained 'Map'!", ConsoleColor.Green);
                                        Main.hasmap = true;
                                        break;
                                    case 'g':
                                        Main.intlbuff += 1;
                                        Interface.type(
                                            "You are touched by the art of cooking. Being forged in the flame of cooking, your ability to think up vicious insults has improved. Your intelligence has improved a little",
                                            ConsoleColor.Cyan);
                                        Main.gbooks++;
                                        break;
                                }
                                Main.ramsaycounter++;
                                break;
                            }
                            Interface.type(
                                "The library is closed, but you find a signed version of Gordon Ramsay's book.");
                            break;
                        case 'w':
                            Interface.type(
                                "You realize that you don't speak the same language as the shopkeeper. You take all of his peppermint candy and leave.");
                            Interface.type(
                                "Before you toss it on the floor like the scumbag you are, you notice one of the candy wrappers has an apostrophe on the inside.");
                            break;
                    }
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You have arrived at the gate of a castle. There are many people passing through the gate. You see the royal library(l), a smithy's guild(g). Where do you wish to go?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('l');
            templist.Add('g');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'l':
                    if (Main.nlibcounter == 0)
                    {
                        Main.Player.xp += 10;
                        Interface.type(
                            "In the royal library you find 'Crescent Path'(c), 'Tale of Sariel'(t), 'History of The Realm'(h), and Gordon Ramsay: A Geology(g). Which do you wish to read?");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'c':
                                Interface.type(
                                    " The book reads 'In the land of the central sands.......(This book is incredibly long. If you wish to complete this book, it may cost you.");
                                switch (Interface.readkey().KeyChar)
                                {
                                    case 'y':
                                        Main.Player.hp -= 1;
                                        Main.spdbuff += 3;
                                        Interface.type("You are exhausted from finishing the book, ", ConsoleColor.Red);
                                        Interface.typeOnSameLine(
                                            "but you feel like your speed has increased a little.", ConsoleColor.Cyan);
                                        break;
                                    case 'n':
                                        Interface.type(
                                            "You decide that completing this book is not worth the time. Do you wish to put the book back in it's original spot on the bookshelf?(y/n)");
                                        switch (Interface.readkey().KeyChar)
                                        {
                                            case 'y':
                                                Interface.type(
                                                    "You place the book back in it's original position. You daydream about a perfect society where all of mankind would put books back in bookshelves.");
                                                break;
                                            case 'n':

                                                Interface.type("Are you the trash of society?");
                                                switch (Interface.readkey().KeyChar)
                                                {
                                                    case 'y':
                                                        Main.Player.hp -= 3;
                                                        Main.Player.reputation -= 100;
                                                        Interface.type("You lose 3 hp. Dirtbag.", ConsoleColor.Red);
                                                        break;
                                                    case 'n':
                                                        Interface.type("You place the book back in the bookshelf.");
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case 't':
                                Interface.type(
                                    "You read of the exploits of the ancient hero Sariel, and his philosophies of protecting others.");
                                Main.Player.abilities.AddCommand(new Combat.Dawnstrike("Dawnstrike", 't'));
                                break;
                            case 'h':
                                Interface.type(
                                    "You read 5000 pages on the history of the realm. You're not sure why you did that, but you feel smarter.",
                                    ConsoleColor.Cyan);
                                Main.intlbuff += 2;
                                break;
                            case 'g':
                                Interface.type(
                                    "You learn of the layers of granite and basalt on Planet Ramsay. You feel tougher.",
                                    ConsoleColor.Cyan);
                                Main.defbuff += 1;
                                Main.gbooks++;
                                break;
                        }
                        Main.nlibcounter++;
                    }
                    else
                    {
                        Interface.type("The library is closed.");
                    }
                    break;
                case 'g':
                    var m = new Merchant("Smith's Guild",
                        "The Smith's Guild has their cumulative project for sale. It's high quality, but very expensive.",
                        "They high five each other, happy that someone appreciates their work.",
                        "You decide your money is more important than protection and move on.",
                        new Dictionary<char, Item> {{'1', new bt_plate()}});
                    m.Interact();
                    break;
                case 'c':
                    break;
                case 'p':
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You step through the gates of the city, and witness the largest coalescence of humanity you've seen in you entire life. There are hundereds of great wonders in this behemoth of a city. You may visit the library(l), the weaponsmith(a), the inn(i), the magic shop(q), or the town's great monument(o)";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
            templist.Add('q');
            templist.Add('o');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;

                case 'l':
                    if (Main.centrallibcounter == 0)
                    {
                        Main.Player.xp += 10;
                        Interface.type(
                            "This massive building is a monument to human knowledge. You feel dwarfed by its towering presence.");
                        Interface.type(
                            "You arrive at a shelf that draws your attention. There are six books. The first one is a musty tome entitled Alcwyn's Legacy(a). The second one is A Guide to Theivery(g), the third Sacrificial Rite(s). The fourth book is The Void(v). The fifth book is Samael's Game(j). The final book's title ramsay: A Mathematics(r). Which do you read?");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'a':
                                Interface.type("You become enlightened in the ways of the elder wizard Alcywn.");
                                Main.Player.abilities.AddCommand(new Combat.Curse("Curse", 'c'));
                                break;
                            case 'g':
                                Interface.type(
                                    "Now skilled in the art of stealing, you gain 10% more gold. You also feel faster.",
                                    ConsoleColor.Cyan);
                                Main.is_theif = true;
                                Main.spdbuff += 2;
                                break;
                            case 's':
                                Interface.type("You become skilled in the art of sacrifice.");
                                Main.Player.abilities.AddCommand(new Combat.Sacrifice("Sacrifice", 's'));
                                break;
                            case 'v':
                                Interface.type("You learn of the Void. You can phase in and out of reality.");
                                Main.Player.abilities.AddCommand(new Combat.Phase("Phase", 'p'));
                                break;
                            case 'r':
                                Interface.type("You become educated on the mathematics of cooking.", ConsoleColor.Cyan);
                                Main.intlbuff += 1;
                                Main.gbooks++;
                                break;
                            case 'j':
                                Interface.type("You open the book and find dice inside. Do you wish to roll?(y/n)");
                                switch (Interface.readkey().KeyChar)
                                {
                                    case 'y':
                                        var abilchance = Dice.roll(1, 6);
                                        if (abilchance == 6)
                                        {
                                            Interface.type("You feel as if something incredible has happened.");
                                            Main.Player.abilities.AddCommand(new Combat.Gamble("Gamble", '$'));
                                        }
                                        else
                                        {
                                            Interface.type("Wow you roll a" + abilchance);
                                            Main.Player.hp -= abilchance;
                                            Interface.type("You lose" + abilchance + "hp", ConsoleColor.Red);
                                            Interface.type("You decide to never gamble again.");
                                        }
                                        break;

                                    case 'n':
                                        Interface.type("You feel threatened by the dice.");
                                        break;
                                }
                                Main.centrallibcounter++;
                                break;
                        }
                        Main.centrallibcounter++;
                    }
                    else
                    {
                        Interface.type("The library is closed.");
                    }
                    break;
                case 'a':
                    var m = new Merchant("Reginald",
                        "You approach a building with a sigil bearing crossed swords. You suspect this is the weaponsmith. You enter, and he has loads of firepower for sale.",
                        "You are happy with your purchase.", "You leave.",
                        new Dictionary<char, Item>
                        {
                            {'1', new bt_longsword()},
                            {'2', new iron_buckler()},
                            {'3', new iron_mail()},
                            {'4', new iron_rapier()}
                        });
                    m.Interact();
                    break;
                case 'q':
                    if (Main.magiccounter == 0)
                    {
                        Interface.type("You arrive at a shifty magic dealer in a back alley. He says, 'Hey! ");
                        Interface.typeOnSameLine(Main.Player.name, ConsoleColor.White);
                        Interface.typeOnSameLine(
                            "! I can sell you this Blood Amulet(b, 50), a new ability(a, 50), or secret (s, 50). You game? You're not sure how he knows your name, but it freaks you out. (b, a, s)");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'b':
                                Player.Purchase(50, new blood_amulet());
                                break;
                            case 'a':
                                if (Player.Purchase(50))
                                {
                                    Interface.type(
                                        "He holds out his hand, and reality appears to bend around it. Kind of like the Degauss button on monitors from the 90's.");
                                    Main.Player.abilities.AddCommand(new Combat.VorpalBlades("Vorpal Blades", 'v'));
                                }
                                break;
                            case 's':
                                if (Player.Purchase(50))
                                    Interface.type(
                                        "\"I have a secret to tell you. Everything you know is wrong.\" He holds out his hand, and reality appears to bend around it. Kind of like the Degauss button on monitors from the 90's. \"See this?\" he says. 'This is what's known as the Flux. Everything is from the Flux, and controlled by the Flux. Learn to control it, and you control reality. Oh, and those letters you keep seeing will be useful\". he dissapears througha shimmering portal and leaves you there mystified.");
                                break;
                        }
                        Main.magiccounter++;
                        break;
                    }
                    Interface.type("You look for the magic man, but he's gone.");
                    break;
                case 'b':
                    Backpack.BackpackLoop();
                    break;
                case 'i':
                    var ik = new Innkeeper("Humphrey",
                        "The inn is called 'Donaldius Trumpe'. Humphrey's expression seems to state that he has disadain for plebs.",
                        "You feel refreshed, however the bedsheets smelled like cold blooded capitalism and weasely politicians.",
                        "You leave the posh hotel");
                    ik.Interact();
                    break;
                case 'o':
                    Interface.type(
                        "You visit the city's monument, which is a tourist attraction for the entire Realm. People are everywhere. You elbow your way through the crowd to get a better look. The monument is a massive sword, stabbed into the ground as if placed there by a giant god. Blue light is pulsing up it like a giant conduit. You spot a secret door in the blade of the sword. Enter (y/n)");
                    switch (Interface.readkey().KeyChar)
                    {
                        case 'y':
                            Interface.type("The door requires a password.");
                            if (Interface.readinput(false) == "god's will")
                                End.Endgame();
                            else
                                Interface.type("The door remains shut.");
                            break;
                        case 'n':
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
            return
                "You step through the gates of the southernmost kingdom in the land. The gates are still scarred from the Invasion. The library is a smoking ruin. You may go to the arms dealer(a), the residential district(r) or the inn(i)";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('l');
            templist.Add('i');
            templist.Add('q');
            templist.Add('o');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'a':
                    var m = new Merchant("Rex", "The arms dealer has a small stand, but his wares are valuable.",
                        "He grins an you can't help but smile with him.", "You leave.",
                        new Dictionary<char, Item>
                        {
                            {'1', new ds_amulet()},
                            {'2', new ds_kite()},
                            {'3', new ds_kris()},
                            {'4', new ds_scale()}
                        });
                    m.Interact();
                    break;
                case 'b':
                    Backpack.BackpackLoop();
                    break;
                case 'i':
                    var ik = new Innkeeper("Chandler",
                        "The inn is half burned down, but there are still beds avaiable. The inkeeper seems to have burn scars on his face.",
                        "You stay the night and all your clothes are covered in ash.", "You decide not to risk it.");
                    ik.Interact();
                    break;
                case 'r':
                    if (Main.townfolkcounter == 0)
                    {
                        Main.Player.xp += 10;
                        Interface.type(
                            "You visit the house of the jobless former librarian. He says there is something very strange about that sword monument in Central. He says you can never go anywhere unarmed. He teaches you a new ability.");
                        Main.Player.abilities.AddCommand(new Combat.Incinerate("Incinerate", 'a'));
                        Interface.type("As you're leaving, you notice the letter 'l' burned into the doorknob.");
                        Main.townfolkcounter++;
                    }
                    else
                        Interface.type("You look for the librarian, but he's not there.");
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
            return
                "You walk into a ghost town, obviously built for a port before the ocean receeded. You may visit the inn(i) or the arms dealer(a).";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('i');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'a':
                    var m = new Merchant("Ernest", "A very old man is selling some very high end wares.",
                        "You see a glint of greed in the old man's eye.", "You leave",
                        new Dictionary<char, Item> {{'1', new bt_battleaxe()}, {'2', new bt_greatsword()}});
                    m.Interact();
                    break;
                case 'b':
                    Backpack.BackpackLoop();
                    break;
                case 'i':
                    var ik = new Innkeeper("John", "It's a normal-ass inn.",
                        "You sleep the night, and wake up feeling like you slept the night.",
                        "You don't sleep in the inn.");
                    ik.Interact();
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
            return
                "You happen upon a travelling band of merchants wearing turbans. They have unimaginably valuable wares for sale. You may talk to the Sage Kaiser(k) or the man with the gear(g)";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('k');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                        Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                        Map.PlayerPosition.x += 1;
                    break;
                case 'w':
                    if (Map.PlayerPosition.x > 0)
                        Map.PlayerPosition.x -= 1;
                    break;
                case 's':
                    if (Map.PlayerPosition.y > 0)
                        Map.PlayerPosition.y -= 1;
                    break;
                case 'g':
                    var m = new Merchant("Toothless man", "You walk up to the toothless man holding wares.",
                        "The toothless man reverently hands you the artifact", "You decide it's too pricey.",
                        new Dictionary<char, Item>
                        {
                            {'1', new illusory_plate()},
                            {'2', new spectral_bulwark()},
                            {'3', new void_cloak()}
                        });
                    m.Interact();
                    break;
                case 'k':
                    if (Main.nomadcounter == 0)
                    {
                        Interface.type(
                            "You visit their Elder King, or Sage Kaiser, as they call him. He offers to teach you an ability for 200 gold. (y/n)");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'y':
                                if (Player.Purchase(200))
                                {
                                    Interface.type(
                                        "You say yes, and he holds up hand, and some strange runes on his hand begin to glow.");
                                    Main.Player.abilities.AddCommand(new Combat.Heavensplitter("Heavensplitter", 'z'));
                                    Interface.type("The old man also hands you a rune with the letter 's' inscribed.");
                                    Main.nomadcounter++;
                                }
                                break;
                        }
                    }
                    else
                        Interface.type(
                            "He already taught you the strongest ability he knows. He has nothing more to offer.");
                    break;
                case 'b':
                    Backpack.BackpackLoop();
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
            return
                "You approach the gates of the East Kingdom. You see an arms dealer(a) and an inn(i). Where do you go?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('i');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'i':
                    var ik = new Innkeeper("Archibald",
                        "\"Welcome to the Ritz Hotel\". You've always wanted to stay in one of these.",
                        "You steal all the soap from the shower.", "You leave the posh hotel");
                    ik.Interact();
                    break;
                case 'a':
                    var m = new Merchant("Sarah",
                        "You visit the arms dealer and it's being manned by a 6 year old girl missing her front teeth.",
                        "She grins and hands you the gear.", "You leave.",
                        new Dictionary<char, Item> {{'1', new sb_gauntlet()}, {'2', new sb_saber()}});
                    m.Interact();
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
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
            return
                "You are in a small village at the base of a waterfall. You see an arms dealer(a) and an inn(i). Where do you want to go?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('#');
            templist.Add('a');
            templist.Add('i');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'a':
                    var m = new Merchant("Greg",
                        "A balding middle aged man is running this shop. He is selling fairly valuable wares.",
                        "You buy the gear.", "You leave.",
                        new Dictionary<char, Item> {{'1', new sb_chain()}, {'2', new sb_shield()}});
                    m.Interact();
                    break;
                case 'i':
                    var ik = new Innkeeper("Johannes",
                        "This hotel is obviously designed for rich people, but there is a clear lack of customers.",
                        "As you're leaving the hotel, you notice a 'Romney 2012' sign. The letter 'i' is carved over Mitt's face",
                        "You can't take the republicanism in this place");
                    ik.Interact();
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
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
            return
                "You are at the foot of a black citadel, do you wish to climb the stairs and commence the reconquest of Ravenkeep(c) or leave(l)?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            templist.Add('v');
            templist.Add('#');
            templist.Add('c');
            templist.Add('l');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                case 'm':
                    if (Main.hasmap)
                        Interface.drawmap();
                    break;
                case 'c':
                    if (!Main.raven_dead)
                    {
                        Interface.type(
                            "You climb 18 flights of obsidian stairs. You're kind of huffing an puffing at this point, but you stand before the mad king who rules RavenKeep.");
                        Interface.type("Challenge him, or run? (f/r).");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'f':
                                Interface.type(
                                    "You challenge the mad king, and he stands from his obsidian throne, raven-feathered cloak swirling. He laughs a deep booming laugh and draws a wicked looking blade.");
                                Combat.BattleLoop(new RavenKing());
                                Main.ach.Get("raven");
                                if (Main.Player.backpack.Count <= 10)
                                {
                                    Main.Player.backpack.Add(new phantasmal_claymore());
                                    Interface.type("Obtained 'Phantasmal Claymore'!", ConsoleColor.Green);
                                }
                                else
                                    Interface.type("Not enough space.");
                                Interface.type(
                                    "The king falls to the ground, defeated. You pick up his onyx longsword form the ground, and in your hand it changes to a shimmering blue claymore.");
                                Interface.type(
                                    "A blue portal opens up with the glowing letter 'l' above it. You yell 'Jeronimo!' and jump through.");
                                Map.PlayerPosition.x = 3;
                                Map.PlayerPosition.y = 3;
                                break;
                            case 'r':
                                Interface.type("You run back down the stairs like this sissy cRAVEN you are.");
                                break;
                        }
                    }
                    else
                        Interface.type("There's no one here.");
                    break;
                case 'l':
                    Interface.type("Coward.");
                    Map.PlayerPosition.x -= 1;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class Coaltown : Place
    {
        protected override string GetDesc()
        {
            return
                "You find yourself in a town obviously centered around a massive coal mine. You may visit the inn(i), arms dealer(a), or the coal mines(c)";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('a');
            templist.Add('i');
            templist.Add('c');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'a':
                    var m = new Merchant("Edwin",
                        "You visit the arms dealer. He is old, and the wrinkles in his face are blackened with coal dust. As this town is the only source of the precious black mineral in the all of Realm, these ex-miners are very rich. For sale before you are imported wares from the distant land of Avira.",
                        "He smiles gratefully.", "You are far too plebian to afford his wares.",
                        new Dictionary<char, Item> {{'1', new a_amulet()}, {'2', new a_mail()}, {'3', new a_staff()}});
                    m.Interact();
                    break;
                case 'i':
                    var ik = new Innkeeper("Johnson", "Do you want to stay at the coaltown motel?",
                        "Everything in the town, now including you, is coated in a layer of fine black dust, but at least your hp has been restored.",
                        "You decide not to risk the lung cancer.");
                    ik.Interact();
                    break;
                case 'c':
                    if (Main.minecounter == 0)
                    {
                        Interface.type(
                            "This coalmine is abundant with miners and minecarts, carrying the precious black resource back to the surface. You try to swipe a coal nugget from a passing minecart, as the stuff is worth double it's weight in gold, but a burly miner swats your hand. With his pickeaxe. Ow. Do you want to travel deeper into the mine? (y/n)");
                        var roll = Dice.roll(1, 10);
                        if (roll == 1)
                        {
                            Interface.type("The mine collapses and you die.");
                            End.GameOver();
                        }
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'y':
                                Interface.type(
                                    "As you go deeper and deeper into the mines, the caves get darker and darker. In front of you, the entire rail collapses. You must make a split second decision. Jump(j), or fall(f)?");
                                switch (Interface.readkey().KeyChar)
                                {
                                    case 'j':
                                        Interface.type(
                                            "You try to jump for your life, but you hit your head on an i-beam and die.");
                                        End.GameOver();
                                        break;
                                    case 'f':
                                        Interface.type(
                                            "You try to ride the wave of steel and stone falling down hundreds of feet. Probably not the best of your ideas, you reflect as you fall. You hit the ground, and everything goes black.");
                                        Interface.type("Press any key to continue.", ConsoleColor.White);
                                        Interface.readkey();
                                        Console.Clear();
                                        if (Dice.roll(1, 2) == 1)
                                        {
                                            Interface.type("You find yourself in a mysterious glowing cave.");
                                            Cave.CaveLoop();
                                        }
                                        else
                                        {
                                            Interface.type(
                                                "You wake up. At least you're not dead. But everything on you hurts. Your eyes adjust to your surroundings, and you find yourself in the ruins of an ancient library, but everything is burned. Everything save one book. Pick it up? (y/n)");
                                            switch (Interface.readkey().KeyChar)
                                            {
                                                case 'y':
                                                    Main.Player.backpack.Add(new tome());
                                                    Interface.type("You pick up the book.");
                                                    if (!Main.Player.abilities.commands.ContainsKey('p'))
                                                        Main.Player.abilities.AddCommand(
                                                            new Combat.ForcePulse("Force Pulse", 'f'));

                                                    Interface.type(
                                                        "It takes a few hours, but you climb your way out of the mine. You surface looking like a chimney sweep.");
                                                    break;
                                                case 'n':
                                                    Interface.type(
                                                        "It takes a few hours, but you climb your way out of the mine, leaving the book behind.");
                                                    break;
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 'n':
                                Interface.type("You leave.");
                                break;
                            default:
                                break;
                        }
                        Main.minecounter++;
                    }
                    else
                        Interface.type("The mine collapsed!");
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class FrozenFjords : Place
    {
        protected override string GetDesc()
        {
            return
                "Around you, the air becomes bitter cold. Snow begins to fall. You come upon a glacier. As you are about to turn around and head back, you spot a castle of ice, almost hidden in the snow. You enter the kingdom of frost, and you see a library(l), a weaponsmith(a), and an inn(i). Where do you go?";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('a');
            templist.Add('i');
            templist.Add('l');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'a':
                    var m = new Merchant("Leif", "A young man with a frosty beard and frostier gear greets you.",
                        "Jesus christ this is so cold oh god I don't want it anymore help me oh god it's so cold",
                        "His head moves, but you're not sure if it's a nod of farewell or a shiver",
                        new Dictionary<char, Item>
                        {
                            {'1', new ice_amulet()},
                            {'2', new ice_dagger()},
                            {'3', new ice_shield()},
                            {'4', new swifites()}
                        });
                    m.Interact();
                    break;
                case 'i':
                    var ik = new Innkeeper("Bjergsen", "Do you wish to stay at the Iceborn Inn?",
                        "You are well rested, but you have frostbite in three toes.", "It's too damn cold.");
                    ik.Interact();
                    break;
                case 'l':
                    if (Main.frozencounter == 0)
                    {
                        Interface.type(
                            "You arrive at a library made of ice, and although it's cold as dead kittens, you decide to go in. 3 books catch your eye. Cold (c), Frost(f), or Ice(i).");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'c':
                                Interface.type(
                                    "You pick up the book entitled Cold. You read of the century long winter from the days of old. It kind of scares you, but you feel smarter and faster.",
                                    ConsoleColor.Cyan);
                                Main.intlbuff += 2;
                                Main.spdbuff += 2;
                                break;
                            case 'f':
                                Interface.type(
                                    "You pick of the book entitled Frost. You learn of the event that froze over this once-warm town. It terrifies you, but you feel stronger and tougher.",
                                    ConsoleColor.Cyan);
                                Main.atkbuff += 2;
                                Main.defbuff += 2;
                                break;
                            case 'i':
                                Interface.type(
                                    "You pick up the book entitled Ice. You learn of the ancient sorcer who was able to cast ice spells by drawing the heat out of the air, and the water from the ground.");
                                Main.Player.abilities.AddCommand(new Combat.IceChains("Ice Chains", 'i'));
                                break;
                            default:
                                break;
                        }
                        Main.Player.xp += 10;
                        Main.frozencounter++;
                    }
                    else
                        Interface.type("The doors are frozen shut.");
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class MagicCity : Place
    {
        protected override string GetDesc()
        {
            return
                "You arrive at the fabled City of Magic, although now that you're here, it's a lot less it's cracked up to be. People are pulling rabbits out of hats here, not blasting dragons with fireballs. You do, however, find a bit of the town that's a Wizard's Guild, although not very good wizards reside there. You may go to their magic dealer(a), their inn(i), or their library(l).";
        }

        public override Enemy getEnemyList()
        {
            return null;
        }

        public override char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count >= 1)
                templist.Add('b');
            if (Main.hasmap)
                templist.Add('m');
            if (Map.PlayerPosition.x > 0)
                templist.Add('w');
            if (Map.PlayerPosition.x < Map.map.GetUpperBound(0))
                templist.Add('e');
            if (Map.PlayerPosition.y > 0)
                templist.Add('s');
            if (Map.PlayerPosition.y < Map.map.GetUpperBound(1))
                templist.Add('n');
            templist.Add('v');
            templist.Add('a');
            templist.Add('i');
            templist.Add('l');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'e':
                    Map.PlayerPosition.x += 1;
                    break;
                case 'n':
                    Map.PlayerPosition.y += 1;
                    break;
                case 's':
                    Map.PlayerPosition.y -= 1;
                    break;
                case 'w':
                    Map.PlayerPosition.x -= 1;
                    break;
                case 'a':
                    var m = new Merchant("Harold",
                        "A young man with one of those stereotypical black and white 'wands' is running the shop.",
                        "He grins a conman's grin.", "You are disgusted with him and leave",
                        new Dictionary<char, Item>
                        {
                            {'1', new m_amulet()},
                            {'2', new m_robes()},
                            {'3', new m_staff()},
                            {'4', new m_tome()}
                        });
                    m.Interact();
                    break;
                case 'i':
                    var ik = new Innkeeper("Dave Ironmeadow", "Do you want to stay at the Rabbit-Inn-Hat?",
                        "Your health has been restored, but when you tried to pull a sock from your suitcase, and infinite chain of rainbow hankies tied together stopped you from actually getting a sock. So now you only have one.",
                        "You almost pass out from all of the phony magic because of flashbacks to bad birthday parties.");
                    ik.Interact();
                    break;
                case 'l':
                    if (Main.noobcounter == 0)
                    {
                        Interface.type(
                            "You arrive at a library which looks more like Half-Price books than an actual library. You may read Dissapearance(d), the Art of Illusion(i), or Magical Lasers(m).");
                        switch (Interface.readkey().KeyChar)
                        {
                            case 'd':
                                Interface.type(
                                    "You learn about how the first mages learned to make themselves dissapear.");
                                Main.Player.abilities.AddCommand(new Combat.NowYouSeeMe("Now You See Me", 'y'));
                                break;
                            case 'i':
                                Interface.type("You read about how the first wizards learnt to decieve people's minds.");
                                Main.Player.abilities.AddCommand(new Combat.Illusion("Illusion", '?'));
                                break;
                            case 'm':
                                Interface.type("You learn of how to make cool lasers with magic.");
                                Main.Player.abilities.AddCommand(new Combat.PewPewPew("Pew Pew Pew", 'w'));
                                break;
                            default:
                                break;
                        }
                        Main.Player.xp += 10;
                        Main.noobcounter++;
                    }
                    else
                        Interface.type("The library is cloed.");
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class caveplace
    {
        private Random rand;
        public List<Enemy> enemylist = new List<Enemy>();

        protected virtual string GetDesc()
        {
            return "It's really dark here. Press f to move forward.";
        }

        public string Description
        {
            get { return GetDesc(); }
        }

        public virtual Enemy getEnemyList()
        {
            var templist = new List<Enemy>();
            templist.Add(new Slime());
            if (Main.Player.level >= 3)
                templist.Add(new cavebat());
            if (Main.Player.level >= 5)
                templist.Add(new cavespider());
            var randint = rand.Next(1, templist.Count + 1);

            return templist[randint - 1];
        }

        public virtual char[] getAvailableCommands()
        {
            var templist = new List<char>();
            if (Main.Player.backpack.Count > 0)
                templist.Add('b');
            templist.Add('v');
            templist.Add('f');
            templist.Add('#');
            return templist.ToArray<char>();
        }

        public virtual void handleInput(char input)
        {
            var inputHandled = false;
            while (!inputHandled)
            {
                inputHandled = _handleInput(input);
                if (!inputHandled)
                {
                    Interface.type("Invalid.");
                    Interface.type("");
                    input = Interface.readkey().KeyChar;
                }
            }
        }

        protected virtual bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'f':
                    if (Map.CavePosition < Map.cavemap.GetUpperBound(0))
                        Map.CavePosition += 1;
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }

        public caveplace()
        {
            rand = new Random();
        }
    }

    public class crystal : caveplace
    {
        protected override string GetDesc()
        {
            return
                "You arrive in an opening in the tunnel, and you are surrounded by beautiful crystals. You may attempt to harvest them(h), or press on(f).";
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'h':
                    if (Dice.roll(1, 2) == 1)
                    {
                        Main.Player.hp -= 2;
                        Interface.type("You cut yourself trying to harvest the crystals and give up.", ConsoleColor.Red);
                    }
                    else
                    {
                        if (!Main.hasFarmedCrystal)
                        {
                            Interface.type(
                            "As you harvest them, the crystals glow, and energy seeps through your veins and into your brain. Your vision becomes very bright, and you feel enlightened.",
                            ConsoleColor.Cyan);
                            Main.intlbuff += 3;
                        }
                        else
                        {
                            Interface.type("You throw up from mana poisoning.", ConsoleColor.Red);
                            Main.Player.hp -= 5;
                        }
                    }
                    break;
                case 'f':
                    if (Map.CavePosition < Map.cavemap.GetUpperBound(0))
                        Map.CavePosition += 1;
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class mushrooms : caveplace
    {
        protected override string GetDesc()
        {
            return
                "The tunnel widens, and the chamber you are in is aglow with weird mushrooms. You may attempt to consume them(c), or press on(f).";
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'c':
                    var roll = Dice.roll(1, 4);
                    if (roll == 1)
                    {
                        Main.Player.hp -= 5;
                        Interface.type("Bad trip! You puke blood everywhere and take heavy damage.", ConsoleColor.Red);
                        if (Main.Player.hp <= 0)
                            End.GameOver();
                    }
                    else if (roll == 2)
                    {
                        Interface.type("You feel like you just drank 6 espressi, and you're bouncing off the walls.",
                            ConsoleColor.Cyan);
                        Main.spdbuff += 3;
                    }
                    else if (roll == 3)
                    {
                        Interface.type("Nothing happens, except you're less hungry now.");
                        Main.Player.hp += 1;
                    }
                    else if (roll == 4)
                    {
                        Interface.type(
                            "The mushroom you just ate was a hypersteroid. You grow taller, your muscles bigger, and all in less than a minute. It's kind of painful though.",
                            ConsoleColor.Cyan);
                        Main.Player.maxhp += 2;
                        Main.atkbuff += 1;
                    }
                    break;
                case 'f':
                    if (Map.CavePosition < Map.cavemap.GetUpperBound(0))
                        Map.CavePosition += 1;
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class spring : caveplace
    {
        protected override string GetDesc()
        {
            return
                "In your journey through the cave, you come across a spring of water. You can drink it(d), or press on(f).";
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'd':
                    var roll = Dice.roll(1, 2);
                    if (roll == 1)
                    {
                        Main.Player.hp -= 7;
                        Interface.type("The water isn't actually water. It's arsenic.", ConsoleColor.Red);
                        if (Main.Player.hp <= 0)
                            End.GameOver();
                    }
                    else
                    {
                        Interface.type(
                            "This is the most delicous water you have ever tasted. Your health has been fully restored.");
                        Main.Player.hp = Main.Player.maxhp;
                    }
                    break;
                case 'f':
                    if (Map.CavePosition < Map.cavemap.GetUpperBound(0))
                        Map.CavePosition += 1;
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class dragon : caveplace
    {
        protected override string GetDesc()
        {
            return
                "You happen upon a sleeping dragon guarding a mountain of gold. You see an exit behind it. You try to kill the dragon and take the gold(k), or you can try to sneak around it(s)..";
        }

        protected override bool _handleInput(char input)
        {
            switch (input)
            {
                case 'v':
                    Interface.typeStats();
                    break;
                case 'k':
                    Combat.BattleLoop(new Dragon());
                    Interface.type(
                        "You escape the cave! You find youself in a sewer, and when you finally pull yourself out, you're in Central.");
                    Map.PlayerPosition.y = 3;
                    Map.PlayerPosition.x = 3;
                    Main.MainLoop();
                    break;
                case 's':
                    if (Dice.roll(1, 10) == 1)
                    {
                        Combat.BattleLoop(new Dragon());
                        Main.ach.Get("dragon");
                    }
                    else
                        Interface.type("You successfully snuck past the dragon!");
                    Interface.type(
                        "You escape the cave! You find youself in a sewer, and when you finally pull yourself out, you're in Central.");
                    Map.PlayerPosition.y = 3;
                    Map.PlayerPosition.x = 3;
                    //Main.MainLoop();
                    break;
                case 'b':
                    if (Main.Player.backpack.Count > 0)
                        Backpack.BackpackLoop();
                    break;
                case '#':
                    Save.SaveGame();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}