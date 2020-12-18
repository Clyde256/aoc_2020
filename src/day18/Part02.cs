using System;
using System.Collections;
using System.Linq;
using AOC.Tools;

namespace AOC.Day18
{
    public static class Part02
    {
        static long Solve(string str)
        {
            var dat = str.Split(" ");

            var val = new Stack();
            var ops = new Stack();

            for (var i = 0; i < dat.Count(); i++)
            {
                var it = dat[i];

                if (it == "+") 
                {
                    var tmp = (long)val.Pop() + Convert.ToInt64(dat[i+1]);
                    val.Push(tmp);
                    i++;
                    continue;
                }

                if (it == "*") continue;

                val.Push(Convert.ToInt64(it));
            }

            long res = 1;

            foreach(var it in val)
            {
                res *= (long)it;
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