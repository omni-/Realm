using System;
using System.Collections.Generic;

namespace Realm
{
    public class Player
    {
        public class GamePlayer
        {
            public int hp, maxhp, spd, atk, intl, def, g, level,xp, xp_next, fire, guard, reputation;

            public string pclass, race, name;

            public Item primary = new Item(), secondary = new Item(), armor = new Item(), accessory = new Item();

            public List<Item> backpack;

            public bool on_fire = false, cursed = false, stunned = false, guarded = false, blinded = false, phased = false;

            public Combat.CommandTable abilities;

            public int[] last_inn = new int[2];

            public void levelup()
            {
                int xp_overlap;
                xp_next = level >= 10 ? 62 + (level - 10) * 7 : (level >= 5 ? 17 + (level - 5) * 3 : 17);
                if (xp >= xp_next)
                {
                    level++;
                    hp = maxhp;
                    Interface.type("Congratulations! You have leveled up! You are now level " + level + ".", true);
                    if (xp > xp_next)
                        xp_overlap = Math.Abs(xp - xp_next);
                    else
                        xp_overlap = 0;
                    xp = xp_overlap;
                    xp_next = (level >= 10 ? 62 + (level - 10) * 7 : (level >= 5 ? 17 + (level - 5) * 3 : 17));
                    if (xp >= xp_next)
                        levelup();
                }
            }
            public void applybonus()
            {
                atk = 1;
                def = 1;
                spd = 1;
                intl = 1;
                if (race == "giant" || race == "drake")
                {
                    maxhp = 12 + (level + 2);
                    atk = (1 + (level / 3));
                }
                else
                    maxhp = 9 + level;
                if (race == "human")
                {
                    def = (1 + (level / 3));
                    atk = (2 + (level / 3));
                    spd = (1 + (level / 3));
                }
                else if (race == "elf" || race == "slime")
                {
                    intl = (3 + (level / 2));
                    spd = (1 + (level / 3));
                }
                else if (race == "rockman")
                {
                    def = (3 + (level / 2));
                    maxhp = 10 + level;
                }
                else if (race == "zephyr" || race == "goblin")
                {
                    spd = (3 + (level / 2));
                    intl = (1 + (level / 3));
                }
                else if (race == "shade" || race == "bandit")
                {
                    atk = (3 + (level / 2));
                    spd = (1 + (level / 3));
                }
                atk += Main.atkbuff;
                def += Main.defbuff;
                spd += Main.spdbuff;
                intl += Main.intlbuff;

                if (!primary.Equals(default(Item)))
                {
                    def += primary.defbuff;
                    atk += primary.atkbuff;
                    intl += primary.intlbuff;
                    spd += primary.spdbuff;
                }
                if (level >= 5 && race == "human" && !abilities.commands.ContainsKey('m'))
                    abilities.AddCommand(new Combat.Mimic("Mimic", 'm'));
                else if (level >= 5 && race == "elf" && !abilities.commands.ContainsKey('l'))
                    abilities.AddCommand(new Combat.Heal("Heal", 'l'));
                else if (level >= 5 && race == "rockman" && !abilities.commands.ContainsKey('g'))
                    abilities.AddCommand(new Combat.Safeguard("Safeguard", 'g'));
                else if (level >= 5 && race == "giant" && !abilities.commands.ContainsKey('r'))
                    abilities.AddCommand(new Combat.Rage("Rage", 'r'));
                else if (level >= 5 && race == "zephyr" && !abilities.commands.ContainsKey('!'))
                    abilities.AddCommand(new Combat.Lightspeed("Lightspeed", '!'));
                else if (level >= 5 && race == "shade" && !abilities.commands.ContainsKey('n'))
                    abilities.AddCommand(new Combat.Nightshade("Nightshade", 'n'));
                else if (level >= 5 && race == "slime" && !abilities.commands.ContainsKey('m'))
                {
                    //abilities.AddCommand();
                }
                if (pclass == "warrior")
                    atk += (1 + (Main.Player.level / 5));
                if (pclass == "paladin")
                    def += (1 + (Main.Player.level / 5));
                if (pclass == "mage")
                    intl += (1 + (Main.Player.level / 5));
                if (pclass == "thief")
                    spd += (1 + (Main.Player.level / 5));

                if (!secondary.Equals(default(Item)))
                {
                    def += secondary.defbuff;
                    atk += secondary.atkbuff;
                    intl += secondary.intlbuff;
                    spd += secondary.spdbuff;
                }

                if (!armor.Equals(default(Item)))
                {
                    def += armor.defbuff;
                    atk += armor.atkbuff;
                    intl += armor.intlbuff;
                    spd += armor.spdbuff;
                }
                if (!accessory.Equals(default(Item)))
                {
                    def += accessory.defbuff;
                    atk += accessory.atkbuff;
                    intl += accessory.intlbuff;
                    spd += accessory.spdbuff;
                }
            }
            public void applydevbonus()
            {
                atk = 1000;
                def = 1000;
                intl = 1000;
                spd = 1000;
                maxhp = 1000;
                g = 1000;
                level = 1000;
                if (!primary.Equals(default(Item)))
                {
                    def += primary.defbuff;
                    atk += primary.atkbuff;
                    intl += primary.intlbuff;
                    spd += primary.spdbuff;
                }

                if (!secondary.Equals(default(Item)))
                {
                    def += secondary.defbuff;
                    atk += secondary.atkbuff;
                    intl += secondary.intlbuff;
                    spd += secondary.spdbuff;
                }

                if (!armor.Equals(default(Item)))
                {
                    def += armor.defbuff;
                    atk += armor.atkbuff;
                    intl += armor.intlbuff;
                    spd += armor.spdbuff;
                }
                if (!accessory.Equals(default(Item)))
                {
                    def += accessory.defbuff;
                    atk += accessory.atkbuff;
                    intl += accessory.intlbuff;
                    spd += accessory.spdbuff;
                }
                hp = maxhp;
            }
            public GamePlayer()
            {
                maxhp = 11;
                hp = 11;
                level = 1;
                xp = 0;
                g = 15;
                def = 1;
                intl = 1;
                backpack = new List<Item>();
                abilities = new Combat.CommandTable();
                abilities.AddCommand(new Combat.BasicAttack("Basic Attack", 'b'));
            }
        }
        public static bool Purchase(int cost)
        {
            if (cost > Main.Player.g)
            {
                Interface.type("You don't have enough gold.");
                return false;
            }
            else
            {
                Main.Player.g -= cost;
                Main.ach.Get("itembuy");
                return true;
            }
        }
        public static bool Purchase(int cost, Item i)
        {
            if (cost <= Main.Player.g)
            {
                Main.Player.g -= cost;
                if (Main.Player.backpack.Count <= 10)
                {
                    Main.Player.backpack.Add(i);
                    Interface.type("Obtained '" + i.name + "'!", ConsoleColor.Green);
                }
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
        public static List<Item> getCorrectlyTieredItems()
        {
            var Items = new List<Item>();
            foreach(var i in Main.MainItemList)
                if (i.tier <= (Main.Player.level / 5))
                    Items.Add(i);
            return Items;
        }
    }
}
