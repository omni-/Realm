using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Main
    {
        public static Player player = new Player();
        public static void MainLoop()
        {
            if (Combat.CheckBattle())
            {
                BattleLoop();
            }
            Place currPlace;
            while (!End.IsDead)
            {
                player.hp = 10;
                player.def = 0;
                player.atk = 1;
                player.intl = 0;
                player.spd = 1;

                foreach (Item i in player.backpack)
                {
                    player.def += i.defbuff;
                    player.atk += i.atkbuff;
                    player.intl += i.intlbuff;
                    player.spd += i.spdbuff;
                }

                Console.WriteLine("\r\n");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("HP: " + player.hp);
                Console.WriteLine("Defense: "+ player.def);
                Console.WriteLine("Attack: " + player.atk);
                Console.WriteLine("Speed: " + player.spd);
                Console.WriteLine("Intelligence: " + player.intl);
                Console.WriteLine("-------------------------------------");

                currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                Console.WriteLine(currPlace.Description);

                char[] currcommands = currPlace.getAvailableCommands();
                Console.Write("\n Your current commands are x");
                foreach (char c in currcommands)
                {
                    Console.Write(", {0}", c);
                }
                Console.WriteLine("");

                ConsoleKeyInfo command = Console.ReadKey();
                if (command.Key == ConsoleKey.X)
                {
                    Console.WriteLine("\n Are you sure?");
                    char surecommand = Console.ReadKey().KeyChar;
                    if (surecommand == 'y')
                    {
                        End.IsDead = true;
                        End.GameOver();
                    }
                }
                else if (command.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else
                    currPlace.handleInput(command.KeyChar);
            }
        }

        public static void BattleLoop()
        {
            bool is_turn = false;
            Console.WriteLine("You've been ambushed! You ready your weapons.");
            Place currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
            List<Enemy> enemylist = currPlace.getEnemyList();
            Random rand = new Random();
            int randint = rand.Next(1, enemylist.Count);
            Enemy enemy = enemylist[randint];
            if (enemy.spd < player.spd)
                is_turn = true;
            while (enemy.hp != 0)
            {
                if (is_turn)
                {

                }
            }
        }
    }
}
//gota figte let's go guys!