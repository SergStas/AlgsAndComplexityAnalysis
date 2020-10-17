using System.Collections.Generic;
using System.Linq;

namespace ACALab2
{
    public static class InfixNotationConverter
    {
        private static readonly char[] Splitters = {' ', '\t', '\n'};

        private static readonly Dictionary<string, char> OperationsAliases = new Dictionary<string, char>
        {
            { "sin", '#' },
            { "cos", '$' },
            { "ln", '%' },
            { "sqrt", '&' }
        };

        private static readonly Dictionary<char, int> Operators = new Dictionary<char, int>
        {
            {'+', 0}, {'-', 0},
            {'*', 1}, {':', 1},
            {'^', 2},
            {'#', 3}, {'$', 3}, {'%', 3}, {'&', 3}
        };

        private static List<string> Buffer;
        
        public static string ConvertToPostfix(string infixNotation)
        {
            Buffer = new List<string>();
            var formatted = FormatInfix(infixNotation);
            return formatted;
        }

        private static string FormatInfix(string infix)
        {
            var noSplitters = infix.ToCharArray().Select(char.ToLower).ToList();
            foreach (var splitter in Splitters)
                noSplitters.RemoveAll(c => c == splitter);
            var result = string.Join("", noSplitters);
            foreach (var (key, value) in OperationsAliases)
                result = result.Replace(key, value.ToString());
            return result;
        }

        private static void ProcessInfix()
        {
            
        }

        private static void ParseUnit(string line, int start, int end)
        {
            
        }
    }
}