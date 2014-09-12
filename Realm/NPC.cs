using System;
using System.Collections.Generic;

namespace Realm
{
    public class NPC
    {
        public string name, text;
        public int cmdp;
        public char[] commands;

        public NPC(string Name, string Text, char[] Commands)
        {
            name = Name;
            text = Text;
            commands = Commands;
        }

        public NPC()
        {
        }

        public virtual void Interact()
        {
            Interface.type("NPC: " + name, ConsoleColor.Cyan);
            Interface.type("==================", ConsoleColor.Yellow);
            Interface.type(text);
        }
    }

    public class Merchant : NPC
    {
        public string buy, leave;

        private Dictionary<char, Item> forsale = new Dictionary<char, Item>();

        /// <summary>
        /// A constructor for the merchant class. Use this to make a new merchant.
        /// </summary>
        /// <param name="MerchantName">The merchanrt's name.</param>
        /// <param name="GreetingText">The text that displays when the merchant greets you.</param>
        /// <param name="buyText">The text that displays when you purchase something from the merchant.</param>
        /// <param name="leaveText">The text that displays when you leave.</param>
        /// <param name="ItemsForSale">A dictionary which has unique char keys corresponding to items. These are the things you can buy from the merchant.</param>
        public Merchant(string MerchantName, string GreetingText, string buyText, string leaveText,
            Dictionary<char, Item> ItemsForSale)
        {
            name = MerchantName;
            text = GreetingText;
            forsale = ItemsForSale;
            buy = buyText;
            leave = leaveText;
        }

        private bool handleinput(char c)
        {
            if (!forsale.ContainsKey(c))
                return false;
            Player.Purchase(forsale[c].value, forsale[c]);
            return true;
        }

        public override void Interact()
        {
            base.Interact();
            Interface.type(
                "Press 'b' to go into the buy menu, 's' to go into the sell menu, or anything else to go back.");
            switch (Interface.readkey().KeyChar)
            {
                case 'b':
                    Interface.type("Press any key not listed to exit the buy menu.");
                    var loop = true;
                    while (loop)
                    {
                        foreach (var kv in forsale)
                            Interface.type(kv.Key + ". " + kv.Value.name + "(" + kv.Value.value + ")",
                                ConsoleColor.Green);
                        Interface.type("");
                        if (!handleinput(Interface.readkey().KeyChar))
                            loop = false;
                    }
                    break;
                case 's':
                    Interface.type("Index. Item name.....Item resale value. Press a non-listed key to go back.",
                        ConsoleColor.Yellow);
                    var indices = new List<int>();
                    var itr = 1;
                    foreach (var item in Main.Player.backpack)
                    {
                        Interface.type(itr + ". " + item.name + "....." + (item.value == 1 ? 1 : (int) (item.value*.6)));
                        indices.Add(itr);
                        itr++;
                    }
                    var input = Interface.readkey().KeyChar;
                    if (indices.Contains(Convert.ToInt32(input)))
                    {
                        Main.Player.g +=
                            (int)
                                (Main.Player.backpack[Convert.ToInt32(input)].value == 1
                                    ? 1
                                    : Main.Player.backpack[Convert.ToInt32(input)].value*.6);
                        Main.Player.backpack.Remove(Main.Player.backpack[Convert.ToInt32(input)]);
                    }
                    break;
                default:
                    Interface.type("Error: cmd not found.", ConsoleColor.Red);
                    break;
            }
        }
    }

    public class Innkeeper : NPC
    {
        public int price;
        public string stay, leave;

        public Innkeeper(string Name, string Text, string stayText, string leaveText)
        {
            price = 2 + (int) (Main.Player.level*1.5);
            name = Name;
            text = Text;
            stay = stayText;
            leave = leaveText;
        }

        public override void Interact()
        {
            base.Interact();
            Interface.type("Press 's' to stay at the inn for " + price + " gold, or anything else to go back.");
            switch (Interface.readkey().KeyChar)
            {
                case 's':
                    Player.Purchase(price);
                    Main.Player.hp = Main.Player.maxhp;
                    Interface.type("Your health has been restored.", ConsoleColor.Cyan);
                    if (!String.IsNullOrEmpty(stay))
                        Interface.type(stay);
                    Main.Player.last_inn[0] = Map.PlayerPosition.x;
                    Main.Player.last_inn[1] = Map.PlayerPosition.y;
                    break;
                default:
                    if (!String.IsNullOrEmpty(leave))
                        Interface.type(leave);
                    break;
            }
        }
    }
}