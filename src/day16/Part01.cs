using System;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day16
{
    public static class Part01
    {
        public class Range
        {
            public int Min;
            public int Max;

            public Range(int min, int max)
            {
                Min = min;
                Max = max;
            }

            public bool IsIn(int value)
            {
                if (value < Min) return false;
                if (value > Max) return false;
                return true;
            }

            public static Range Create(string text)
            {
                var data = text.Split('-');
                if (data.Length != 2) return null;
                var min = Convert.ToInt32(data[0]);
                var max = Convert.ToInt32(data[1]);
                var range = new Range(min, max);
                return range;
            }
        }

        public class Item
        {
            public string Name;

            public Range RangeA;
            public Range RangeB;

            public bool IsValid(int value)
            {
                if (RangeA.IsIn(value)) return true;
                if (RangeB.IsIn(value)) return true;
                return false;
            }

            public static Item Create(string text)
            {
                var tmpA = text.Split(':', StringSplitOptions.RemoveEmptyEntries);
                if (tmpA.Length != 2) return null;

                var tmpB = tmpA[1].Split("or", StringSplitOptions.RemoveEmptyEntries);
                if (tmpB.Length != 2) return null;

                var item = new Item();
                item.Name = tmpA[0];
                item.RangeA = Range.Create(tmpB[0]);
                item.RangeB = Range.Create(tmpB[1]);

                return item;
            }
        }

        static void Parse(List<string> data, out List<Item> items, out List<int> myTicket, out List<List<int>> nearbyTickets)
        {
            items = new List<Item>();

            while(data.Count > 0)
            {
                var row = data[0];
                data.RemoveAt(0);

                if (row == "") break;

                var item = Item.Create(row);

                items.Add(item);
            }

            myTicket = new List<int>();

            while(data.Count > 0)
            {
                var row = data[0];
                data.RemoveAt(0);

                if (row == "") break;
                if (row.Contains(':')) continue;

                myTicket = Collections.ToIntList(row, ',');
            }

            nearbyTickets = new List<List<int>>();

            while(data.Count > 0)
            {
                var row = data[0];
                data.RemoveAt(0);

                if (row == "") break;
                if (row.Contains(':')) continue;

                var ticket = Collections.ToIntList(row, ',');
                nearbyTickets.Add(ticket);
            }
        }

        static int InvalidSum(List<Item> items, List<int> values)
        {
            var invalidSum = 0;

            foreach(var val in values)
            {
                var validCount = 0;

                foreach(var it in items)
                {
                    if (!it.IsValid(val)) continue;
                    validCount++;
                }

                if (validCount <= 0)
                {
                    invalidSum += val;
                }
            }

            return invalidSum;
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day16/input.txt");
            var data = reader.ReadAll();

            List<Item> items;
            List<int> myTicket;
            List<List<int>> nearbyTickets;
            Parse(data, out items, out myTicket, out nearbyTickets);

            var invalidSum = 0;

            foreach(var tck in nearbyTickets)
            {
                invalidSum += InvalidSum(items, tck);
            }

            Console.WriteLine(invalidSum);
        }
    }
}