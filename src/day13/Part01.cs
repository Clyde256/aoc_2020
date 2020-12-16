using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day13
{
    public static class Part01
    {
        static void Data(out int timestamp, out List<int> buses)
        {
            var reader = FileIO.CreateProjFilePath("./day13/input.txt");
            var data = reader.ReadAll();
            
            timestamp = Convert.ToInt32(data[0]);
            
            var strBus = data[1].Split(",");
            buses = new List<int>();

            foreach(var it in strBus)
            {
                if (it == "x") continue;
                buses.Add(Convert.ToInt32(it));
            }
        }

        static void NearestTime(int time, int bus, ref int nearTime, ref int dist)
        {
            double mul = time / (double)bus;
            int index = (int)Math.Ceiling(mul);
            nearTime = index * bus;
            dist = nearTime - time;
        }

        public static void Run()
        {
            int timestamp;
            List<int> buses;
            Data(out timestamp, out buses);

            var minDist = Int32.MaxValue;
            var minBus = -1;
            var minTime = -1;

            foreach(var it in buses)
            {
                var near = 0;
                var dist = 0;
                NearestTime(timestamp, it, ref near, ref dist);

                if (dist > minDist) continue;

                minDist = dist;
                minBus = it;
                minTime = near;
            }

            Console.WriteLine(string.Format("BUS[{0}]; TIME[{1}]", minBus, minTime));
            Console.WriteLine(minDist * minBus);
        }
    }
}