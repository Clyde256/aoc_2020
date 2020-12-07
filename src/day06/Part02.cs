using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day06
{
    public static class Part02
    {
        public static int QuestionCount(List<string> data)
        {
            if (data.Count <= 0) return 0;

            var dic = new Dictionary<char, int>();
            bool fisrt = true;

            foreach(var it in data)
            {
                if (fisrt)
                {
                    foreach(var chr in it)
                    {
                        if (dic.ContainsKey(chr)) continue;
                        dic[chr] = 1;
                    }
                }
                else
                {
                    var tmp = dic.ToList();

                    foreach(var chr in tmp)
                    {
                        var pos = it.IndexOf(chr.Key);
                        if (pos >= 0) continue;
                        dic.Remove(chr.Key);
                    }
                }

                fisrt = false;
            }

            return dic.Count();
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day06/input.txt");
            var list = reader.ReadAllGroups();
            
            var res = 0;

            foreach(var it in list)
            {
                res += QuestionCount(it);
            }

            Console.WriteLine(res);
        }
    }
}