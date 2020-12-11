using System;
using System.Collections.Generic;

namespace AOC.Tools
{
    public static class Collections
    {
        public static void Print<T>(IEnumerable<T> col)
        {
            foreach (var item in col)
                Console.WriteLine(item); // Replace this with your version of printing
        }
    }
}