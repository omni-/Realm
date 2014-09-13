using System;

namespace Realm
{
    public static class Cave
    {
        public static void CaveLoop()
        {
            caveplace currPlace;
            Main.Player.applybonus();
            while (Main.Player.hp > 0)
            {
                Main.Player.levelup();
                currPlace = Map.cavemap[Map.CavePosition];
                if (Main.Player.hp > Main.Player.maxhp)
                    Main.Player.hp = Main.Player.maxhp;
                if (Combat.CheckBattle() && currPlace.getEnemyList() != null)
                {
                    Combat.BattleLoop(currPlace.getEnemyList());
                }
                if (!Main.devmode)
                    Main.Player.applybonus();
                else
                    Main.Player.applydevbonus();
                if (!Main.devmode)
                {
                    Interface.type("-------------------------------------");
                    Interface.type(Main.Player.name + "(" + Main.Player.race + ")," + " Level " + Main.Player.level +
                                   " " + Main.Player.pclass + ":");
                    Interface.type("HP: " + Main.Player.hp + "/" + Main.Player.maxhp);
                    Interface.type("Attack: " + Main.Player.atk + " / Defense: " + Main.Player.def + " / Speed: " +
                                   Main.spdbuff + " / Intelligence: " + Main.Player.intl);
                    Interface.type("Mana: " + (1 + (Main.Player.intl/10)));
                    Interface.type("Gold: " + Main.Player.g + " / Exp to Level: " +
                                   (Main.Player.xp_next - Main.Player.xp));
                    Interface.type("-------------------------------------");
                }
                Interface.type(!Main.devmode ? currPlace.Description : currPlace.ToString());

                var command = Interface.readkey();

                if (command.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else if (command.KeyChar == '-' && Main.devmode)
                {
                    var input = Interface.readinput();
                    if (input == "e")
                        End.Endgame();
                    else if (input == "c")
                    {
                        var combat_input = Interface.readinput();
                        var etype = Type.GetType("Realm." + combat_input);
                        var e = (Enemy) Activator.CreateInstance(etype);
                        Combat.BattleLoop(e);
                    }
                    else if (input == "n")
                        Main.Player.name = Interface.readinput();
                    else if (input == "a")
                    {
                        var add_input = Interface.readinput();
                        var atype = Type.GetType("Realm." + add_input);
                        var i = (Item) Activator.CreateInstance(atype);
                        if (Main.Player.backpack.Count <= 10)
                            Main.Player.backpack.Add(i);
                        else
                            Interface.type("Not enough space.");
                        Interface.type("Obtained '" + i.name + "'!");
                    }
                    else if (input == "p")
                    {
                        var p_input = Interface.readinput();
                        var atype = Type.GetType("Realm." + p_input);
                        var i = (Item) Activator.CreateInstance(atype);
                        Main.Player.primary = i;
                    }
                    else if (input == "t")
                    {
                        var place_input = Interface.readinput();
                        var ptype = Type.GetType("Realm." + place_input);
                        var p = (Place) Activator.CreateInstance(ptype);
                    }
                }
                else
                    currPlace.handleInput(command.KeyChar);
                Main.loop_number++;
            }
        }
    }
}