using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AOC.Tools;

namespace AOC.Day04
{
    public static class Part02
    {
        public static void Run()
        {
            var path = InputPath.ForExe("./day04/input.txt");
            string text = System.IO.File.ReadAllText(path);
            var batch = text.Split(new string[] { System.Environment.NewLine + System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            batch = batch.Select(it => it.Replace(System.Environment.NewLine, " ")).ToArray();

            string pat_byr = @"(?=.*\bbyr:(19[2-8][0-9]|199[0-9]|200[0-2])\b)";
            string pat_iyr = @"(?=.*\biyr:(201[0-9]|2020)\b)";
            string pat_eyr = @"(?=.*\beyr:(202[0-9]|2030)\b)";
            string pat_hgt = @"(?=.*\bhgt:(((1[5-8][0-9]|19[0-3])cm)|((59|6[0-9]|7[0-6])in))\b)";
            string pat_hcl = @"(?=.*\bhcl:#([0-9]|[a-f]){6}\b)";
            string pat_ecl = @"(?=.*\becl:(amb|blu|brn|gry|grn|hzl|oth)\b)";
            string pat_pid = @"(?=.*\bpid:([0-9]{9})\b)";

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