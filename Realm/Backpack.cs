// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/30/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Realm
{
    public class BP : Collection<Item>
    {
        public BP(Item i)
        {
            Add(i);
        }

        public BP()
        {
        }

        protected override void InsertItem(int index, Item item)
        {
            if (Count < 10)
            {
                base.InsertItem(index, item);
                Interface.type("Obtained " + item.name + "!", ConsoleColor.Green);
            }
            else
            {
                Interface.type(
                    "Your backpack is full. Please enter the index of an item to drop, or anything else to go back.",
                    ConsoleColor.Red);
                for (int j = 0; j < Count; j++)
                {
                    Interface.type(j + ". " + this[j].name);
                }
                var k = 0;
                var result = Int32.TryParse(Interface.readkey().KeyChar.ToString(), out k);
                if (result && Count - 1 >= k && k >= 0)
                {
                    base.InsertItem(IndexOf(this[k]), item);
                    Interface.type("Obtained " + item.name + "!", ConsoleColor.Green);
                    Remove(this[k]);
                }
                else
                {
                    Interface.type("You decide to ignore the item and move on.");
                }
            }
        }
    }

    public class Backpack
    {
        private class backpackcommand : Combat.Command
        {
            public backpackcommand(string aname, char cmd)
                : base(aname, cmd)
            {
            }

            public override bool Execute(object Data)
            {
                var i = (Item) Data;
                Interface.type(i.name);
                Interface.type("Description: " + i.desc, ConsoleColor.Green);
                Interface.type(
                    "Attack Buff: " + i.atkbuff + " / Defense Buff: " + i.defbuff + " / Speed Buff: " + i.spdbuff +
                    " / Intelligence Buff: " + i.intlbuff, ConsoleColor.Green);
                switch (i.slot)
                {
                    case 1:
                        Interface.type("Slot: Primary", ConsoleColor.Green);
                        break;
                    case 2:
                        Interface.type("Slot: Secondary", ConsoleColor.Green);
                        break;
                    case 3:
                        Interface.type("Slot: Armor", ConsoleColor.Green);
                        break;
                    case 4:
                        Interface.type("Slot: Accessory", ConsoleColor.Green);
                        break;
                }
                Interface.type("Tier: ", ConsoleColor.Green);
                Interface.typeOnSameLine(i.tier.ToString(),
                    (i.tier == 0
                        ? ConsoleColor.Gray
                        : i.tier == 1
                            ? ConsoleColor.White
                            : i.tier == 2
                                ? ConsoleColor.Blue
                                : i.tier == 3
                                    ? ConsoleColor.Yellow
                                    : i.tier == 4
                                        ? ConsoleColor.Red
                                        : i.tier == 5
                                            ? ConsoleColor.Magenta
                                            : i.tier == 6 ? ConsoleColor.Cyan : ConsoleColor.DarkMagenta));
                Interface.type("Enter (y) to equip this item, (d) to destroy and anything else to go back.",
                    ConsoleColor.Green);
                var c = Interface.readkey().KeyChar;
                switch (c)
                {
                    case 'y':
                        switch (i.slot)
                        {
                            case 1:
                                Main.Player.primary = i;
                                break;
                            case 2:
                                Main.Player.secondary = i;
                                break;
                            case 3:
                                Main.Player.armor = i;
                                break;
                            case 4:
                                Main.Player.accessory = i;
                                break;
                        }
                        break;
                    case 'd':
                        switch (i.slot)
                        {
                            case 1:
                                if (Main.Player.primary == i)
                                    Main.Player.primary = new Item();
                                Main.Player.backpack.Remove(i);
                                break;
                            case 2:
                                if (Main.Player.secondary == i)
                                    Main.Player.secondary = new Item();
                                Main.Player.backpack.Remove(i);
                                break;
                            case 3:
                                if (Main.Player.armor == i)
                                    Main.Player.armor = new Item();
                                Main.Player.backpack.Remove(i);
                                break;
                            case 4:
                                if (Main.Player.accessory == i)
                                    Main.Player.accessory = new Item();
                                Main.Player.backpack.Remove(i);
                                break;
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
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Interface.type("----------Current Equipment----------");
                Interface.type("Primary: " + Main.Player.primary.name);
                Interface.type("Secondary: " + Main.Player.secondary.name);
                Interface.type("Armor: " + Main.Player.armor.name);
                Interface.type("Accessory: " + Main.Player.accessory.name);
                Interface.type("-------------------------------------");
                var q = 1;
                var cmd = new Combat.CommandTable();
                foreach (var i in Main.Player.backpack)
                {
                    Interface.type((q - 1) + ". " + i.name);
                    var bpcmd = new backpackcommand(i.name, (char) (q + 47));
                    cmd.AddCommand(bpcmd);
                    q++;
                }
                Interface.type("");
                var ch = Interface.readkey().KeyChar;
                if (!cmd.commandChars.Contains(ch))
                    break;
                cmd.ExecuteCommand(ch, Main.Player.backpack[(int) Char.GetNumericValue(ch)]);
            }
            Console.ResetColor();
        }
    }
}