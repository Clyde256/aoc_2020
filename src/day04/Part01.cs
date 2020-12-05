using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AOC.Tools;

namespace AOC.Day04
{
    public static class Part01
    {
        public static void Run()
        {
            var path = InputPath.ForExe("./day04/input.txt");
            string text = System.IO.File.ReadAllText(path);
            var batch = text.Split(new string[] { System.Environment.NewLine + System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            batch = batch.Select(it => it.Replace(System.Environment.NewLine, " ")).ToArray();

            string pat_byr = @"(?=.*\bbyr:.+\b)";
            string pat_iyr = @"(?=.*\biyr:.+\b)";
            string pat_eyr = @"(?=.*\beyr:\w+\b)";
            string pat_hgt = @"(?=.*\bhgt:.+\b)";
            string pat_hcl = @"(?=.*\bhcl:.+\b)";
            string pat_ecl = @"(?=.*\becl:.+\b)";
            string pat_pid = @"(?=.*\bpid:.+\b)";

            Regex reg = new Regex(@"^" + pat_byr + pat_iyr + pat_eyr + pat_hgt + pat_hcl + pat_ecl + pat_pid + @".*$"); 

            var count = 0;            

            foreach(var it in batch)
            {
                var res = reg.Matches(it);

                if (res.Count > 0)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}