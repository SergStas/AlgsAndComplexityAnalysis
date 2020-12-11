using System;
using System.IO;
using System.Linq;
using Graph;
using static System.String;

namespace SemesterWork
{
    public static class FileParser
    {
        private static readonly char[] BlankChars = {' ', '\t', '\n', '\r'};
        
        public static CustomGraph GetGraph(string path)
        {
            var formatted = Join("", File.ReadAllText(path).Where(c => !BlankChars.Contains(c)));
            var graph = new CustomGraph();
            graph.Add(formatted.Split(".")[0].Split(";"));
            graph.Connect(formatted.Split(".")[1].Split(";")
                .Select(s => (s.Split(",")[0], s.Split(",")[1], double.Parse(s.Split(",")[2])).ToTuple()));
            return graph;
        }
    }
}