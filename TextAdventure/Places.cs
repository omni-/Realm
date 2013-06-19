using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Places
    {
        public static void CastleInit()
        {
            Console.WriteLine("");
            Console.WriteLine("King: 'Come, my champion, for slaying the elder dragon, Tyrone, I will give you the ultimate gift, eternal respite.'");
            Console.WriteLine("");
            Console.WriteLine("The Western King approaches and unsheathes his blade emitting a strong aura of  bloodlust. Fight or Run? [f to fight, r to run]");

            string input = parse.InputCleanse(new string[] {"f", "r"});
            if (input == "f")
            {
                Console.WriteLine("The King gives you swift respite. He decapitates your sorry ass");
                End.GameOver();
            }
            else
            {
                Console.WriteLine("You have lived.");
            }
        }
    }
}
