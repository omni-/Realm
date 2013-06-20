using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Main
    {
        public static void MainLoop()
        {
            Place currPlace;
            while (!End.IsDead)
            {
                
                currPlace = Globals.map[Globals.PlayerPosition.x, Globals.PlayerPosition.y];
                Console.WriteLine(currPlace.Description);
                char[] currcommands = currPlace.getAvailableCommands();
                foreach (char c in currcommands)
                {
                    Console.WriteLine("Your current commands available are {0}.", c);
                }
                char command = Console.ReadKey().KeyChar;
                if (command == 'x')
                {
                    End.IsDead = true;
                    End.GameOver();
                }
                else
                    currPlace.handleInput(command);
            }
        }
    }
}
