using System;

namespace Realm
{
    public static class Dice
    {
        public static int roll(int numdice, int numsides)
        {
            var total = 0;
            var rand = new Random();
            for (var i = 0; i < numdice; i++)
            {
                total += rand.Next(1, Math.Abs(numsides + 1));
            }
            return total;
        }

        public static bool misschance(int spd)
        {
            var misschance = roll(1, 101 + (spd*3));
            if (misschance == 1)
            {
                Interface.type("Missed!", ConsoleColor.White);
                return true;
            }
            return false;
        }
    }
}