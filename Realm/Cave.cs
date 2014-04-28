using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class Cave
    {
        public static void CaveLoop()
        {
            caveplace currPlace = new caveplace();
            Main.Player.applybonus();
            while (Main.Player.hp > 0)
            {
                Enemy enemy = new Enemy();
                Main.Player.levelup();
                currPlace = Map.cavemap[Map.CavePosition];
                if (Main.Player.hp > Main.Player.maxhp)
                    Main.Player.hp = Main.Player.maxhp;
                if (Combat.CheckBattle() && currPlace.getEnemyList() != null)
                {
                    enemy = currPlace.getEnemyList();
                    Combat.BattleLoop(enemy);
                }
                if (!Main.devmode)
                    Main.Player.applybonus();
                else
                    Main.Player.applydevbonus();
                if (!Main.devmode)
                {
                    Interface.type("-------------------------------------");
                    Interface.type(Main.Player.name + "(" + Main.Player.race + ")," + " Level " + Main.Player.level + " " + Main.Player.pclass + ":");
                    Interface.type("HP: " + Main.Player.hp + "/" + Main.Player.maxhp);
                    Interface.type("Attack: " + Main.Player.atk + " / Defense: " + Main.Player.def + " / Speed: " + Main.spdbuff + " / Intelligence: " + Main.Player.intl);
                    Interface.type("Mana: " + (1 + (Main.Player.intl / 10)));
                    Interface.type("Gold: " + Main.Player.g + " / Exp to Level: " + (Main.Player.xp_next - Main.Player.xp));
                    Interface.type("-------------------------------------");
                }
                if (!Main.devmode)
                    Interface.type(currPlace.Description);
                else
                    Interface.type(currPlace.ToString());

                ConsoleKeyInfo command = Interface.readkey();

                if (command.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else if (command.KeyChar == '-' && Main.devmode)
                {
                    string input = Interface.readinput();
                    if (input == "e")
                        End.Endgame();
                    else if (input == "c")
                    {
                        string combat_input = Interface.readinput();
                        Type etype = Type.GetType("Realm." + combat_input);
                        Enemy e = (Enemy)Activator.CreateInstance(etype);
                        Combat.BattleLoop(e);
                    }
                    else if (input == "n")
                        Main.Player.name = Interface.readinput();
                    else if (input == "a")
                    {
                        string add_input = Interface.readinput();
                        Type atype = Type.GetType("Realm." + add_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        if (Main.Player.backpack.Count <= 10)
                            Main.Player.backpack.Add(i);
                        else
                            Interface.type("Not enough space.");
                        Interface.type("Obtained '" + i.name + "'!");
                    }
                    else if (input == "p")
                    {
                        string p_input = Interface.readinput();
                        Type atype = Type.GetType("Realm." + p_input);
                        Item i = (Item)Activator.CreateInstance(atype);
                        Main.Player.primary = i;
                    }
                    else if (input == "t")
                    {
                        string place_input = Interface.readinput();
                        Type ptype = Type.GetType("Realm." + place_input);
                        Place p = (Place)Activator.CreateInstance(ptype);
                    }
                }
                else
                    currPlace.handleInput(command.KeyChar);
                Main.loop_number++;
            }
        }
    }
}
