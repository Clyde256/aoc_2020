using System.IO;
using System.Collections.Generic;

namespace AOC.Tools
{
    public class FileIO
    {
        public string FilePath { get; private set; }

        private FileIO(string filePath)
        {
            FilePath = filePath;
        }

        public static FileIO Create(string filePath)
        {
            var exist = File.Exists(filePath);
            
            if (!exist)
            {
                File.Create(filePath).Dispose();
                exist = File.Exists(filePath);
                if (!exist) return null;
            }

            return new FileIO(filePath);
        }

        public static FileIO CreateProjFilePath(string projFilePath)
        {
            var filePath = InputPath.ForExe(projFilePath);
            return Create(filePath);
        }

        public List<string> ReadAll()
        {
            var list = new List<string>();

            using StreamReader sr = File.OpenText(FilePath);
            
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }

            return list;
        }

        public List<List<string>> ReadAllGroups()
        {
            var list = new List<List<string>>();
            var group = new List<string>();

            using StreamReader sr = File.OpenText(FilePath);
            
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    var tmp = new List<string>();
                    tmp.AddRange(group);
                    list.Add(tmp);
                    group.Clear();
                    continue;
                }

                group.Add(line);
            }

            if (group.Count > 0)
            {
                list.Add(group);
            }

            return list;
        }

        public List<int> ReadAllInt()
        {
            var list = new List<int>();

            using StreamReader sr = File.OpenText(FilePath);
            
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                var val = System.Int32.Parse(line);
                list.Add(val);
            }

            return list;
        }
    }
}