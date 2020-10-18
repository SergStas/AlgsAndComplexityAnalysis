using System.Collections.Generic;
using System.Linq;

namespace ACALab2
{
    public class InfixNotationConverter
    {
        public string InfixNotation { get; }
        
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
        
        public InfixNotationConverter(string infix) => InfixNotation = infix;
        
        public string ConvertToPostfix()
        {
            var formatted = GetFormattedInfix();
            return formatted;
        }

        private string GetFormattedInfix()
        {
            var noSplitters = InfixNotation.ToCharArray().Select(char.ToLower).ToList();
            foreach (var splitter in Splitters)
                noSplitters.RemoveAll(c => c == splitter);
            var result = string.Join("", noSplitters);
            foreach (var (key, value) in OperationsAliases)
                result = result.Replace(key, value.ToString());
            return result;
        }

        private void ProcessInfix()
        {
            for (var i = 0; i < InfixNotation.Length; i++)
            {
                
            }
        }
    }
}