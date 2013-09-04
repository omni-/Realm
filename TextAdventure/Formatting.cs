using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Formatting
    {
        //public static string format(string src)
        //{
        //    int size = src.Length;

        //    int n = size / 80;
        //    for (int i = 0; i < n; i++)
        //    {
        //        int endIndex = n * i - 1;
        //        int startIndex = endIndex - 80;
        //        StringBuilder tempstr = new StringBuilder(new string(src.Substring(startIndex, (i * n - 1))));
        //    }
        //}
        public static string format(string src)
        {
            return (string)(src.Substring(0,80) + "\n" + format(src.Substring(80)));
        }
    }
}
