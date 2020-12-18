using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day18
{
    public static class Part01
    {
        static long Solve(string str)
        {
            var dat = str.Split(" ");
            
            long res = 0;
            
            for (var i = 0; i < dat.Count(); i++)
            {
                var it = dat[i];

                if (it == "+") 
                {
                    res += Convert.ToInt64(dat[i+1]);
                    i++;
                    continue;
                }

                if (it == "*")
                {
                    res *= Convert.ToInt64(dat[i+1]);
                    i++;
                    continue;
                }

                res = Convert.ToInt64(it);
            }

            return res;
        }

        static bool Calc(ref String expr)
        {
            var ir = expr.IndexOf(')');
            if (ir < 0) return false;

            var il = expr.LastIndexOf('(', ir);
            if (il < 0) return false;
            var len = ir - il + 1;

            var one = expr.Substring(il+1, ir-1 - il);
            var res = Solve(one);

            expr = expr.Remove(il, len);
            expr = expr.Insert(il, res.ToString());

            return true;
        }

        static long Result(string expr)
        {
            while(true)
            {
                if (!Calc(ref expr))
                {
                    return Solve(expr);
                }
            }

            throw new NotImplementedException();
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day18/input.txt");
            var data = reader.ReadAll();

            long res = 0;

            foreach(var it in data)
            {
                res += Result(it);
            }

            Console.WriteLine(res);
        }
    }
}