using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Main
    {
        public static int loop_number = 0;
        public static int game_state = 0;
        public static GamePlayer Player = new GamePlayer();

        public static void MainLoop()
        {
            game_state = 0;
            //if (loop_number >= 1)
            //{
            //    if (Combat.CheckBattle())
            //    {
                    BattleLoop();
            //    }
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
            while (enemy.hp >= 0)
            {
                if (is_turn)
                {
                    Formatting.type(enemy.name + ":");
                    Formatting.type("-------------------------", 10);
                    Formatting.type("Enemy HP: " + enemy.hp);
                    Formatting.type("-------------------------", 10);

                    Formatting.type("\r\nAVAILABLE MOVES:");
                    Formatting.type("=========================", 10);

                    foreach (TextAdventure.Combat.Command c in Main.Player.abilities.commands.Values)
                    {
                        string src = "||   " + c.cmdchar + ". " + c.name + "    ||";
                        Formatting.type(src, 10);
                    }
                    Formatting.type("=========================", 10);

                    int oldhp = enemy.hp;
                    char ch = Console.ReadKey().KeyChar;
                    Main.Player.abilities.ExecuteCommand(ch, enemy);
                    int enemyhp = oldhp - enemy.hp;
                    Formatting.type("The enemy takes " + enemyhp + " damage!");
                    is_turn = false;
                }
                if (enemy.hp <= 0)
                {
                    Formatting.type("Your have deafted " + enemy.name + "!");
                    MainLoop();
                }
                else if (!is_turn)
                {
                    Formatting.type(enemy.name + ":");
                    Formatting.type("-------------------------", 10);
                    Formatting.type("Your HP: " + Main.Player.hp);
                    Formatting.type("-------------------------", 10);
                    int oldhp = Main.Player.hp;
                    enemy.attack();
                    Formatting.type("You take " + (oldhp - Main.Player.hp) + "damage!");
                    is_turn = true;
                }
                if (Player.hp <= 0)
                {
                    End.IsDead = true;
                    End.GameOver();
                }
            }
        }
    }
}