using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACALab2
{
    public partial class StringStack
    {
        public bool IsEmpty => _content.Count == 0;
        public int Count => _content.Count;
        
        private readonly List<string> _content = new List<string>();

        private static readonly char[] Splitters = {' ', '\t', '\n'};
        
        public StringStack(){}
        
        public StringStack(string inputRow)
        {
            var content = ParseContent(inputRow);
            _content = content;
        }

        public StringStack(string row, bool fromFile)
        {
            List<string> content;
            if (fromFile)
            {
                var raw = File.ReadAllText(row).ToCharArray().ToList();
                raw.RemoveAll(s => s == '\r');
                content = ParseContent(string.Join("", raw));
            }
            else
                content =  ParseContent(row);
            _content = content;
        }

        public void Push(string e) => _content.Add(e);

        public string Pop()
        {
            var result = Top();
            if (!IsEmpty)
                _content.RemoveAt(Count - 1);
            return result;
        }

        public string Top() => IsEmpty ? null : _content[Count - 1];

        public void Print() => Print(Console.WriteLine);

        public void Print(Action<string> output) => output(string.Join(' ', _content));

        private static List<string> ParseContent(string row) => row.Split(Splitters).ToList();

        public override string ToString() => $"StringStack[{Count}]: {string.Join(' ', _content)}";
    }
}