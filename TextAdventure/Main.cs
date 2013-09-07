using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Main
    {
        public static int loop_number = 0;
        public static int game_state = 0;
        public static void MainLoop()
        {
            game_state = 0;
            //if (Player.abilities.Count == 0)
            //    Player.abilities.AddCommand("BasicAttack");
            //if (loop_number >= 1)
            //{
                //if (Combat.CheckBattle())
                //{
                    BattleLoop();
                //}
            //}
            Place currPlace;
            while (!End.IsDead)
            {
                Player.hp = 10;
                Player.def = 0;
                Player.atk = 1;
                Player.intl = 0;
                Player.spd = 1;

                foreach (Item i in Player.backpack)
                {
                    Player.def += i.defbuff;
                    Player.atk += i.atkbuff;
                    Player.intl += i.intlbuff;
                    Player.spd += i.spdbuff;
                }

                Formatting.type("-------------------------------------", 10);
                Formatting.type("HP: " + Player.hp);
                Formatting.type("Defense: "+ Player.def);
                Formatting.type("Attack: " + Player.atk);
                Formatting.type("Speed: " + Player.spd);
                Formatting.type("Intelligence: " + Player.intl);
                Formatting.type("-------------------------------------", 10);

                currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                Formatting.type(currPlace.Description);

                char[] currcommands = currPlace.getAvailableCommands();
                Console.Write("\r\nYour current commands are x");
                foreach (char c in currcommands)
                {
                    Console.Write(", {0}", c);
                }
                Formatting.type("");

                ConsoleKeyInfo command = Console.ReadKey();
                if (command.Key == ConsoleKey.X)
                {
                    Formatting.type("\r\nAre you sure?");
                    char surecommand = Console.ReadKey().KeyChar;
                    if (surecommand == 'y')
                    {
                        End.IsDead = true;
                        End.GameOver();
                    }
                }
                else if (command.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else
                    currPlace.handleInput(command.KeyChar);
                loop_number++;
            }
        }

        public static void BattleLoop()
        {
            game_state = 1;

            Player.hp = 10;
            Player.def = 0;
            Player.atk = 1;
            Player.intl = 0;
            Player.spd = 1;

            foreach (Item i in Player.backpack)
            {
                Player.def += i.defbuff;
                Player.atk += i.atkbuff;
                Player.intl += i.intlbuff;
                Player.spd += i.spdbuff;
            }

            Formatting.type("You've been ambushed! You ready your weapons.");
            Place currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
            List<Enemy> enemylist = currPlace.getEnemyList();
            Random rand = new Random();
            int randint = rand.Next(0, enemylist.Count);
            Enemy enemy = enemylist[randint];
            bool is_turn = enemy.spd < Player.spd;
            while (enemy.hp != 0)
            {
                if (is_turn)
                {
                    TextAdventure.Combat.CommandTable cmdtable = Combat.getBattleCommands();
                    Formatting.type("\r\nAVAILABLE MOVES:");
                    Formatting.type("=========================", 10);
                    /* make a function that returns a list of strings that are the availible commands */
                    foreach (TextAdventure.Combat.Command c in Player.abilities)
                    {
                        string src = "||   " + i + ". " + Player.abilities[i] + "    ||";
                        Formatting.type(src, 10);
                    }
                    Formatting.type("=========================", 10);
                    Formatting.type("\r\n", 0);
                    Console.ReadKey();
                }
            }
        }
    }
}
//gota figte let's go guys!