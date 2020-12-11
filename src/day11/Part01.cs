using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day11
{
    public static class Part01
    {
        static void Occupied(Map2D map, int y, int x, ref int seat, ref int occp)
        {
            var around = new List<(int, int)>();
            around.Add((y - 1, x - 1));
            around.Add((y - 1, x));
            around.Add((y - 1, x + 1));
            around.Add((y, x - 1));
            around.Add((y, x + 1));
            around.Add((y + 1, x + 1));
            around.Add((y + 1, x));
            around.Add((y + 1, x - 1));

            foreach(var it in around)
            {
                var tmp = ' ';
                if (!map.TryGetValue(it.Item1, it.Item2, ref tmp)) continue;

                if (tmp == '.') continue;
                
                seat++;

                if (tmp == '#') occp++;
            }
        }

        static void Boarding(Map2D seats)
        {
            var clone = seats.Clone();

            for (var y = 0; y < clone.Rows; y++)
            {
                for (var x = 0; x < clone.Cols; x++)
                {
                    var state = clone.Value(y, x);
                    if (state == '.') continue;

                    var seat = 0;
                    var occp = 0;
                    Occupied(clone, y, x, ref seat, ref occp);

                    if (state == 'L' && occp == 0) seats.SetValue(y, x, '#');
                    if (state == '#' && occp >= 4) seats.SetValue(y, x, 'L');
                }
            }
        }

        static int OccupiedCount(Map2D map)
        {
            var count = 0;

            for (var y = 0; y < map.Rows; y++)
            {
                for (var x = 0; x < map.Cols; x++)
                {
                    if (map.Value(y, x) == '#') count++;
                }
            }

            return count;
        }

        public static void Run()
        {
            var filePath = InputPath.ForExe("./day11/input.txt");
            var map = new Map2D();
            map.Load(filePath);

            Map2D prev = null;
            var cnt = 0;

            while(true)
            {
                cnt++;
                Boarding(map);

                if (prev == null)
                {
                    prev = map.Clone();
                    continue;
                }

                if (prev.Equale(map)) break;
                prev = map.Clone();
            }

            Console.WriteLine(cnt);
            map.Print();
            Console.WriteLine(OccupiedCount(map));
        }
    }
}