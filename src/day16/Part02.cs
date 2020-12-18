using System;
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

        public class Ticket
        {
            public List<int> Values;

            public static Ticket Create(string str)
            {
                var res = new Ticket();
                res.Values = Collections.ToIntList(str, ',');
                return res;
            }

            public void Print()
            {
                var str = "";
                foreach(var it in Values)
                {
                    str += it.ToString() + ", ";
                }

                str.TrimEnd(' ');
                str.TrimEnd(',');

                Console.WriteLine(str);
            }

            public long InvalidSum(List<Item> items)
            {
                long invalid = 0;

                foreach(var val in Values)
                {
                    bool isValid = false;

                    foreach(var it in items)
                    {
                        if (!it.IsValid(val)) continue;
                        isValid = true;
                        break;
                    }

                    if (isValid) continue;
                    invalid += val;
                }

                return invalid;
            }

            public int Count { get { return Values.Count; } }
        }

        static void Parse(List<string> data, out List<Item> items, out List<Ticket> tickets)
        {
            items = new List<Item>();
            tickets = new List<Ticket>();

            while(data.Count > 0)
            {
                var row = data[0];
                data.RemoveAt(0);

                if (row == "") break;

                var item = Item.Create(row);

                items.Add(item);
            }

            while(data.Count > 0)
            {
                var row = data[0];
                data.RemoveAt(0);

                if (row == "") break;
                if (row.Contains(':')) continue;

                var tck = Ticket.Create(row);
                tickets.Add(tck);
            }

            while(data.Count > 0)
            {
                var row = data[0];
                data.RemoveAt(0);

                if (row == "") break;
                if (row.Contains(':')) continue;

                var tck = Ticket.Create(row);
                tickets.Add(tck);
            }
        }

        static void ValidTickets(out List<Item> items, out List<Ticket> tickets)
        {
            var reader = FileIO.CreateProjFilePath("./day16/input.txt");
            var data = reader.ReadAll();

            Parse(data, out items, out tickets);

            var valid = new List<Ticket>();
            valid.Add(tickets[0]); // My Ticket
            tickets.RemoveAt(0);

            long invalidSum = 0;

            foreach(var tic in tickets)
            {
                var inv = tic.InvalidSum(items);
                if (inv == 0) valid.Add(tic);
                invalidSum += inv;
            }

            tickets = valid;

            Console.WriteLine(invalidSum);
        }

        static List<List<Item>> Variants(List<Item> items, int count)
        {
            var list = new List<List<Item>>();

            for (var i = 0; i < count; i++)
            {
                var tmp = new List<Item>();

                foreach (var it in items)
                {
                    tmp.Add(it);
                }

                list.Add(tmp);
            }

            return list;
        }

        static void Reduce(ref List<Item> options, int pos, List<Ticket> tickets)
        {
            for (var i = options.Count - 1; i >= 0; i--)
            {
                var it = options[i];
                bool isValid = true;

                foreach(var tic in tickets)
                {
                    var value = tic.Values[pos];
                    if (it.IsValid(value)) continue;
                    Console.WriteLine(value.ToString() + " " + it.Name);
                    isValid = false;
                    break;
                }

                if (isValid) continue;

                options.RemoveAt(i);
            }
        }

        static bool SubReduce(ref List<List<Item>> variants, ref Dictionary<string, int> result)
        {
            try
            {
                var idx = variants.FindIndex(x => x.Count == 1);
                var single = variants[idx][0];
                result.Add(single.Name, idx);
                variants[idx].Clear();

                var removed = 0;

                foreach(var lst in variants)
                {
                    if (lst.Count == 1 && lst[0].Name == single.Name) continue;
                    removed += lst.RemoveAll(x => x.Name == single.Name);
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static void Run()
        {
            List<Item> items;
            List<Ticket> tickets;
            ValidTickets(out items, out tickets);

            var myTicket = tickets[0];

            List<List<Item>> variants = Variants(items, tickets[0].Count);


            for (var i = 0; i < tickets[0].Count; i++)
            {
                var tmp = variants[i];
                Reduce(ref tmp, i, tickets);
                Console.WriteLine();
            }

            var res = new Dictionary<string, int>();

            while(SubReduce(ref variants, ref res))
            {
            }

            long result = 1;

            foreach(var it in res)
            {
                if (!it.Key.Contains("departure")) continue;
                var item = items[it.Value];
                Console.WriteLine(it.Value);
                result *=  myTicket.Values[it.Value];
            }

            Console.WriteLine(result);
        }
    }
}