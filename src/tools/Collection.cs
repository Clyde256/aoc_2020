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

        public static List<int> ToIntList(string text, char sep)
        {
            var tmp = text.Split(sep);
            
            var res =  new List<int>();

            foreach(var it in tmp)
            {
                var val = Convert.ToInt32(it);
                res.Add(val);
            }

            return res;
        }
    }
}