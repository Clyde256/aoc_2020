using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day15
{
    public static class Part01
    {
        static void Insert(ref Dictionary<int, List<int>> dic, int value, int turn)
        {
            if (dic.ContainsKey(value))
            {
                dic[value].Add(turn);
            }
            else
            {
                dic.Add(value, new List<int>() { turn });
            }
        }

        static void Turn(ref Dictionary<int, List<int>> dic, ref int lastNum, int turn)
        {
            var item = dic[lastNum];

            if (item.Count() == 1)
            {
                lastNum = 0;
                Insert(ref dic, lastNum, turn);
            }
            else
            {
                lastNum = item.Last() - item[item.Count - 2];
                Insert(ref dic, lastNum, turn);
            }
        }

        static void Pexeso(List<int> data, int endTurn)
        {
            var lastNum = -1;

            var dic = new Dictionary<int, List<int>>();

            for (var it = 0; it < data.Count; it++)
            {
                dic.Add(data[it], new List<int>() { it + 1 });
                lastNum = data[it];
            }

            int i = dic.Count + 1;

            while (true)
            {
                Turn(ref dic, ref lastNum, i);

                if (i == endTurn)
                {
                    Console.WriteLine(lastNum);
                    return;
                }

                i++;
            }
        }

        public static void Run()
        {
            // Pexeso(new List<int>() { 0,3,1,6,7,5 }, 2020);       // Part 1
            Pexeso(new List<int>() { 0, 3, 1, 6, 7, 5 }, 30000000); // Part 2
        }
    }
}