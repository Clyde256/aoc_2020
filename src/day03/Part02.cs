using System;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day03
{
    public static class Part02
    {
        public static void Run()
        {
            var map = Day03.Part01.LoadMap();

            var slopes = new List<(int, int)>{ (1, 1), (1, 3), (1, 5), (1, 7), (2, 1) };

            long res = 1;

            foreach (var it in slopes) 
            {
                var cnt = Day03.Part01.CountTree(map, it);
                res *= cnt;
                Console.WriteLine(cnt);
            }

            Console.WriteLine(res);
        }
    }
}