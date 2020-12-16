using System;
using System.Linq;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day16
{
    public static class Part02
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

        static bool IsValid(List<int> ticker, List<Item> items)
        {
            if (ticker.Count != items.Count) return false;

            for (var i = 0; i < ticker.Count; i++)
            {
                if (!items[i].IsValid(ticker[i])) return false;
            }

            return true;
        }

        static List<List<Item>> Valid(List<int> ticket, List<Item> items)
        {
            var valid = new List<List<Item>>();

            foreach(var val in ticket)
            {
                var validItems  = new List<Item>();

                foreach(var it in items)
                {
                    if (!it.IsValid(val)) continue;
                    validItems.Add(it);
                }

                valid.Add(validItems);
            }

            return valid;
        }

        static void ReduceInvalid(List<int> ticket, ref List<List<Item>> itemsComb)
        {
            for(var i = 0; i < ticket.Count; i++)
            {
                var val = ticket[i];
                var comb = itemsComb[i];

                var tmp = comb.Count;
                var vld = 0;

                for(var j = comb.Count - 1; j >= 0; j--)
                {
                    if (comb[j].IsValid(val))
                    {
                        vld++;
                        continue;
                    }
                    
                    comb.RemoveAt(j);
                }

                if (vld <= 0 && comb.Count > 0)
                {
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        static bool Check(List<int> ticket, List<List<Item>> variants)
        {
            for (var i = 0; i < ticket.Count; i++)
            {       
                bool valid = false;

                foreach(var it in variants[i])
                {
                    if (it.IsValid(ticket[i])) 
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid) return false;
            }

            return true;
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day16/input.txt");
            var data = reader.ReadAll();

            List<Item> items;
            List<int> myTicket;
            List<List<int>> nearbyTickets;
            Parse(data, out items, out myTicket, out nearbyTickets);

            for(var i = nearbyTickets.Count - 1; i >= 0; i--)
            {
                var ticket = nearbyTickets[i];
                var invalid = InvalidSum(items, ticket);
                if (invalid > 0) nearbyTickets.Remove(ticket);
            }

            var variants = Valid(myTicket, items);

            Console.WriteLine();
        }
    }
}