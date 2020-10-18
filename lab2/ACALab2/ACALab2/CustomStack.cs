using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACALab2
{
    public partial class CustomStack<T>
    {
        public bool IsEmpty => _content.Count == 0;
        public int Count => _content.Count;
        
        private readonly List<T> _content = new List<T>();

        private static readonly char[] Splitters = {' ', '\t', '\n'};
        
        public CustomStack(){}
        
        public CustomStack(string inputRow)
        {
            var content = ParseContent(inputRow);
            _content = content;
        }

        public CustomStack(string row, bool fromFile)
        {
            List<T> content;
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

        public void Push(T e) => _content.Add(e);

        public T Pop()
        {
            var result = Top();
            if (!IsEmpty)
                _content.RemoveAt(Count - 1);
            return result;
        }

        public T Top() => IsEmpty ? default : _content[Count - 1];

        public void Print() => Print(Console.WriteLine);

        public void Print(Action<string> output) => output(string.Join(' ', _content));

        private List<T> ParseContent(string row) => row.Split(Splitters).Select(e => (T)TryParse(e)).ToList();

        private static object TryParse(string e)
        {
            if (typeof(T) == typeof(string))
                return e;
            if (double.TryParse(e, out var d))
                return d;
            if (float.TryParse(e, out var f))
                return f;
            if (int.TryParse(e, out var i))
                return i;
            if (bool.TryParse(e, out var b))
                return b;
            if (char.TryParse(e, out var c))
                return c;
            throw new NotImplementedException();
        }

        public override string ToString() => $"StringStack[{Count}]: {string.Join(' ', _content)}";
    }
}