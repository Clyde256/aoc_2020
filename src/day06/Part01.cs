using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day06
{
    public static class Part01
    {
        public static int QuestionCount(List<string> data)
        {
            var dic = new HashSet<char>();

            foreach(var it in data)
            {
                for (char chr = 'a'; chr <= 'z'; chr++)
                {
                    var pos = it.IndexOf(chr);
                    if (pos < 0) continue;
                    
                    if (dic.Contains(chr)) continue;
                    dic.Add(chr);
                } 
            }

            return dic.Count;
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