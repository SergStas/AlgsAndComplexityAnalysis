using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACALab5
{
    public class TextProcessor
    {
        private readonly List<string> _tokens = new List<string>();
        private static readonly char[] Splitters = 
            {' ', ',', '.', ':', '-', '!', '?', ';', '\"', '(', ')', '#', '[', ']', '\t', '\n', '_', '`', '\''};

        public IEnumerable<string> ReadTokens(string path)
        {
            var content = File.ReadAllText(path);
            foreach (var e in content.Split(Splitters).Where(s => s.Length > 1)
                .Select(s => s.ToLower()))
                yield return e;
        }

        public void AddToDictionary(string path)
        {
            var content = File.ReadAllText(path);
            foreach (var e in content.Split(Splitters).Where(s => s.Length > 1)
                .Select(s => s.ToLower()).Distinct())
                if (!_tokens.Contains(e))
                    _tokens.Add(e);
        }

        public string GenerateText(int wordsCount)
        {
            var builder = new StringBuilder();
            var rnd = new Random();
            for (var i = 0; i < wordsCount; i++)
                builder.Append(_tokens[rnd.Next(0, _tokens.Count)] + (i == wordsCount - 1 ? "" : " "));
            return builder.ToString();
        }
    }
}