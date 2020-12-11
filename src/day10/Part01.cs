using System;
using System.Linq;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day10
{
    public static class Part01
    {
        static void Count(List<int> data)
        {
            data.Sort();

            var dic = new Dictionary<int, int>();

            for (var i = 0; i < data.Count; i++)
            {
                int dif = 0;

                if (i == 0)
                {
                    dif = data[i] - 0;
                }
                else
                {
                    dif = data[i] - data[i-1];
                }

                if (dic.ContainsKey(dif)) dic[dif] = dic[dif] + 1;
                else dic.Add(dif, 1);

                if (i == data.Count - 1)
                {
                    dic[3] +=  1;
                }
            }

            Console.WriteLine("1: " + dic[1]);
            Console.WriteLine("2: " + dic[2]);
            Console.WriteLine("3: " + dic[3]);
            Console.WriteLine("RES: " + dic[3] * dic[1]);
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day10/input.txt");
            var data = reader.ReadAllInt();
            Count(data);
        }
    }
}