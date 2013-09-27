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
                    hp = maxhp;
                    level++;
                    Interface.type("Congratulations! You have leveled up! You are now level " + level + ".");
                    if (xp_next < 0)
                        xp_overlap = Math.Abs(xp);
                    else
                        xp_overlap = 0;
                    xp = xp_overlap;
                    xp_next = (level >= 30 ? 62 + (level - 30) * 7 : (level >= 15 ? 17 + (level - 15) * 3 : 17));
                    if (xp >= xp_next)
                        levelup();
                }
            }
            public void applybonus()
            {
                if (race == "Giant")
                    maxhp = 12 + (level + 2);
                else
                    maxhp = 9 + level;
                def = 1 + (level / 2);
                atk = 1 + (level / 2);
                intl = 1 + (level / 2);
                spd = 1 + (level / 2);

                if (race == "Human")
                {
                    def += (1 + (level / 5));
                    atk += (1 + (level / 5));
                    spd += (1 + (level / 5));
                }
                else if (race == "Elf")
                    intl += (3 + (level / 5));
                else if (race == "Rockman")
                    def += (3 + (level / 5));
                else if (race == "Zephyr")
                    spd += (3 + (level / 5));
                else if (race == "Shade")
                    atk += (3 + (level / 5));

                if (!primary.Equals(default(Item)))
                {
                    def += primary.defbuff;
                    atk += primary.atkbuff;
                    intl += primary.intlbuff;
                    spd += primary.spdbuff;
                }
                if (level >= 5 && race == "Human" && !abilities.commands.ContainsKey('m'))
                {
                    abilities.AddCommand(new Combat.Mimic("Mimic", 'm'));
                    Interface.type("Learned Human ability Mimic!");
                }
                else if (level >= 5 && race == "Elf" && !abilities.commands.ContainsKey('l'))
                {
                    abilities.AddCommand(new Combat.LayTrap("Lay Trap", 'l'));
                    Interface.type("Learned Elf ability Lay Trap!");
                }
                else if (level >= 5 && race == "Rockman" && !abilities.commands.ContainsKey('g'))
                {
                    abilities.AddCommand(new Combat.Safeguard("Safeguard", 'g'));
                    Interface.type("Learned Rockman ability Safeguard!");
                }
                else if (level >= 5 && race == "Giant" && !abilities.commands.ContainsKey('r'))
                {
                    abilities.AddCommand(new Combat.Rage("Rage", 'r'));
                    Interface.type("Learned Giant ability Rage!");
                }
                else if (level >= 5 && race == "Zephyr" && !abilities.commands.ContainsKey('!'))
                {
                    abilities.AddCommand(new Combat.Lightspeed("Lightspeed", '!'));
                    Interface.type("Learned Zephyr ability Lightspeed!");
                }
                else if (level >= 5 && race == "Shade" && !abilities.commands.ContainsKey('n'))
                {
                    abilities.AddCommand(new Combat.Nightshade("Nightshade", 'n'));
                    Interface.type("Learned Shade ability Nightshade!");
                }

                if (pclass == "Warrior")
                    atk += (1 + (Main.Player.level / 5));
                if (pclass == "Paladin")
                    def += (1 + (Main.Player.level / 5));
                if (pclass == "Mage")
                    intl += (1 + (Main.Player.level / 5));
                if (pclass == "Thief")
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
    }
}
