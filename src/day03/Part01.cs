using System;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day03
{
    public static class Part01
    {
        public static int CountTree(Map2D map, (int, int) slope)
        {
            var pos = (0, 0);
            var treeCnt = 0;

            while (true)
            {
                if (pos.Item1 >= map.Rows) break;
                var val = map.Value(pos.Item1, pos.Item2);
                //Console.WriteLine(string.Format("[{0}, {1}] {2}", pos.Item1, pos.Item2, val));
                if (val == '#') treeCnt++;
                pos.Item1 += slope.Item1;
                pos.Item2 += slope.Item2;
            }

            return treeCnt;
        }

        public static Map2D LoadMap()
        {
            var filePath = InputPath.ForExe("./day03/input.txt");
            var map = new Map2D();
            map.Load(filePath);
            map.Print();
            
            return map;
        }

        public static void Run()
        {
            var map = LoadMap();
            var count = CountTree(map, (1, 3));
            Console.WriteLine(count);
        }
    }
}