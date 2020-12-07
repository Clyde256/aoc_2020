using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day07
{
    public static class Part01
    {
        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day07/input.txt");
            var list = reader.ReadAllGroups();
        }
    }
}