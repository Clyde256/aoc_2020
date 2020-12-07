using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day07
{
    public static class Part02
    {
        public static Dictionary<string, List<(String, int)>> GetDictionary(List<string> data)
        {
            var dic = new Dictionary<string, List<(string, int)>>();

            foreach(var it in data)
            {
                var tmp = it.Split(" contain ");

                var key = tmp[0];
                key = key.Replace(" bags", "");
                key = key.Replace(" bag", "");

                var lst = tmp[1].Split(", ");
                var keys = new List<(string, int)>();

                for (var i = 0; i < lst.Count(); i++)
                {
                    var txt = lst[i];
                    txt = txt.TrimEnd('.');
                    txt = txt.Replace(" bags", "");
                    txt = txt.Replace(" bag", "");

                    if (lst[i] == "no other bags")
                    {
                        // keys.Add((txt, 0));
                    }
                    else
                    {
                        var pos = txt.IndexOf(' ');

                        var cnt = 0;
                        var num = txt.Substring(0, pos);
                        Int32.TryParse(num, out cnt);

                        txt = txt.Substring(pos + 1);
                        if (txt != "other")
                        {
                            keys.Add((txt, cnt));
                        }
                    }
                }

                dic.Add(key, keys);
            }

            return dic;
        }

        public static int CountColors(Dictionary<string, List<(String, int)>> dic, string color)
        {
            var items = dic[color];

            int count = 0;

            foreach(var it in items)
            {
                var cnt = CountColors(dic, it.Item1);
                count += it.Item2;
                count += it.Item2 * cnt;
            }

            return count;
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day07/input.txt");
            var list = reader.ReadAll();
            
            var init = "shiny gold";
            var dic = GetDictionary(list);
            var count = CountColors(dic, init);
            Console.WriteLine(count);
        }
    }
}