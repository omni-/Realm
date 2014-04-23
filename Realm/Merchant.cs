using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class Merchant
    {
        public string name, text;
        public Dictionary<char, Item> forsale = new Dictionary<char, Item>();
        public Dictionary<Item, int> prices = new Dictionary<Item, int>();
        char[] commands;
        public Merchant(string MerchantName, string GreetingText, Dictionary<char, Item> ItemsForSale, char[] Commands)
        {
            name = MerchantName;
            text = GreetingText;
            forsale = ItemsForSale;
            commands = Commands;
        }
        private bool handleinput(char c)
        {
            if (!commands.Contains(c))
                return false;
            else
            {
                Player.Purchase(prices[forsale[c]], forsale[c]);
                return true;
            }
        }
        public void Interact()
        {
            Interface.type("Merchant: " + name, ConsoleColor.Cyan);
            Interface.type("==================", ConsoleColor.Yellow);
            Interface.type(text, ConsoleColor.Cyan);
            Interface.type("Press 'b' to go into the buy menu, 's' to go into the sell menu, or anything else to go back.");
            switch(Interface.readkey().KeyChar)
            {
                case 'b':
                    bool loop = true;
                    while (loop)
                    {
                        foreach (KeyValuePair<char, Item> kv in forsale)
                            Interface.type(kv.Key + ". " + kv.Value + "(" + prices[kv.Value] + ")");
                        handleinput(Interface.readkey().KeyChar);
                    }
                    break;
                case 's':
                    break;
                default:
                    break;
            }
        }
    }
}
