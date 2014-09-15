// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/30/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

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
                currPlace.handleInput(command.KeyChar);
                Main.loop_number++;
            }
        }
    }
}