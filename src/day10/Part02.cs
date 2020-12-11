using System;
using System.Collections.Generic;
using AOC.Tools;
namespace AOC.Day10
{
    public static class Part02
    {
        static void Print(SortedDictionary<int, int> dic) { foreach (var it in dic) { Console.WriteLine(it); } }
        static void Calculate(List<int> data)
        {
            data.Sort(); data.Insert(0, 0); data.Add(data[data.Count - 1] + 3); var map = new SortedDictionary<int, int>();
            var cnt = 0;
            for (var i = 0; i < data.Count; i++)
            {
                if (i + 1 >= data.Count) break;
                var dif = data[i + 1] - data[i]; if (dif == 1) { cnt++; } else { if (map.ContainsKey(cnt)) map[cnt] += 1; else map.Add(cnt, 1); cnt = 0; }
            }

            map.Remove(0); map.Remove(1); Print(map);
            long res = 1;
            foreach (var item in map) { switch (item.Key) { case 2: res *= (long)Math.Pow(2, item.Value); break; case 3: res *= (long)Math.Pow(4, item.Value); break; case 4: res *= (long)Math.Pow(7, item.Value); break; } }
            Console.WriteLine(res);
        }

        public static void Run() { var reader = FileIO.CreateProjFilePath("./day10/input.txt"); var list = reader.ReadAllInt(); Calculate(list); }
    }
}