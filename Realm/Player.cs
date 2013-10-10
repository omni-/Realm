using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class Player
    {
        public class GamePlayer
        {
            public int hp;
            public int maxhp;
            public int spd;
            public int atk;
            public int intl;
            public int def;
            public string pclass;
            public string race;
            public string name;
            public Item primary = new Item();
            public Item secondary = new Item();
            public Item armor = new Item();
            public Item accessory = new Item();
            public List<Item> backpack;
            public int g;
            public int level;
            public int xp;
            public int xp_next;
            public bool on_fire = false;
            public bool cursed = false;
            public bool stunned = false;
            public bool guarded = false;
            public bool blinded = false;
            public bool phased = false;
            public int fire;
            public int guard;
            public Realm.Combat.CommandTable abilities;
            public void levelup()
            {
                int xp_overlap;
                xp_next = level >= 20 ? 62 + (level - 20) * 7 : (level >= 10 ? 17 + (level - 10) * 3 : 17);
                if (xp >= xp_next)
                {
                    level++;
                    hp = maxhp;
                    Interface.type("Congratulations! You have leveled up! You are now level " + level + ".", true);
                    if (xp_next < 0)
                        xp_overlap = Math.Abs(xp_next);
                    else
                        xp_overlap = 0;
                    xp = xp_overlap;
                    xp_next = (level >= 20 ? 62 + (level - 30) * 7 : (level >= 10 ? 17 + (level - 10) * 3 : 17));
                    if (xp >= xp_next)
                        levelup();
                }
            }
            public void applybonus()
            {
                atk = 1;
                def = 1;
                spd = 1;
                intl = 0;
                if (race == "giant")
                    maxhp = 12 + (level + 2);
                else
                    maxhp = 9 + level;
                if (race == "human")
                {
                    def = (1 + (level / 5));
                    atk = (1 + (level / 5));
                    spd = (1 + (level / 5));
                }
                else if (race == "elf")
                    intl = (3 + (level / 2));
                else if (race == "rockman")
                    def = (3 + (level / 2));
                else if (race == "zephyr")
                    spd = (3 + (level / 2));
                else if (race == "shade")
                    atk = (3 + (level / 2));
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
                {
                    abilities.AddCommand(new Combat.Mimic("Mimic", 'm'));
                    Interface.type("Learned human ability Mimic!", ConsoleColor.Cyan);
                }
                else if (level >= 5 && race == "elf" && !abilities.commands.ContainsKey('l'))
                {
                    abilities.AddCommand(new Combat.LayTrap("Lay Trap", 'l'));
                    Interface.type("Learned Elf ability Lay Trap!", ConsoleColor.Cyan);
                }
                else if (level >= 5 && race == "rockman" && !abilities.commands.ContainsKey('g'))
                {
                    abilities.AddCommand(new Combat.Safeguard("Safeguard", 'g'));
                    Interface.type("Learned rockman ability Safeguard!", ConsoleColor.Cyan);
                }
                else if (level >= 5 && race == "giant" && !abilities.commands.ContainsKey('r'))
                {
                    abilities.AddCommand(new Combat.Rage("Rage", 'r'));
                    Interface.type("Learned Giant ability Rage!", ConsoleColor.Cyan);
                }
                else if (level >= 5 && race == "zephyr" && !abilities.commands.ContainsKey('!'))
                {
                    abilities.AddCommand(new Combat.Lightspeed("Lightspeed", '!'));
                    Interface.type("Learned zephyr ability Lightspeed!", ConsoleColor.Cyan);
                }
                else if (level >= 5 && race == "shade" && !abilities.commands.ContainsKey('n'))
                {
                    abilities.AddCommand(new Combat.Nightshade("Nightshade", 'n'));
                    Interface.type("Learned shade ability Nightshade!", ConsoleColor.Cyan);
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
                abilities = new Realm.Combat.CommandTable();
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
                return true;
            }
        }
        public static bool Purchase(int cost, Item i)
        {
            if (cost <= Main.Player.g)
            {
                Main.Player.g -= cost;
                if (Main.Player.backpack.Count <= 10)
                    Main.Player.backpack.Add(i);
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
