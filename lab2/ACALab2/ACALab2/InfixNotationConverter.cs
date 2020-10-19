using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACALab2
{
    public class InfixNotationConverter
    {
        public string InfixNotation { get; private set; }
        public CustomStack<string> Tokens { get; } = new CustomStack<string>();
        public Dictionary<string, double> Variables { get; } = new Dictionary<string, double>();
        
        private static List<char> Alphabet => Enumerable.Range('a', 'z').Select(i => (char) i).ToList();

        private static List<char> Operators => BasicOperators.FullCollection.Select(op => op.Alias).ToList();
        
        private readonly CustomStack<Operator> _operators = new CustomStack<Operator>();
        private string[] _varsValues;
        
        private static readonly char[] Splitters = {' ', '\t', '\n'};

        public InfixNotationConverter(string path)
        {
            ReadInput(path);
            ConvertToPostfix();
            FillVarsDict();
        }
        
        public string GetPostfixString(char splitter) =>
            string.Join(splitter, Tokens.ToString().Split(' ').Skip(1));

        private void ReadInput(string path)
        {
            var lines = File.ReadAllLines(path);
            InfixNotation = lines[0];
            _varsValues = lines.Skip(1).ToArray();
        }

        private void FillVarsDict()
        {
            foreach (var row in _varsValues.Where(s => !string.IsNullOrEmpty(s)))
                try {Variables[row.Split('=')[0]] = double.Parse(row.Split('=')[1]);}
                catch (Exception e) {throw new FormatException($"Invalid format in row {row}:\n {e.Message}");}
        }

        private void ConvertToPostfix()
        {
            var formatted = GetFormattedInfix();
            ProcessInfix(formatted);
        }

        private string GetFormattedInfix()
        {
            var noSplitters = InfixNotation.ToCharArray().Select(char.ToLower).ToList();
            foreach (var splitter in Splitters)
                noSplitters.RemoveAll(c => c == splitter);
            var result = string.Join("", noSplitters);
            foreach (var op in BasicOperators.FullCollection)
                result = result.Replace(op.Notation.ToLower(), op.Alias.ToString());
            return result;
        }

        private void ProcessInfix(string formatted)
        {
            var isOperand = false;
            var operand = "";
            for (var i = 0; i < formatted.Length; i++)
            {
                var curChar = formatted[i];
                if (char.IsDigit(curChar) || Alphabet.Contains(curChar) || curChar == ',' || curChar == '.' ||
                    curChar == '-' && (i == 0 || Operators.Contains(formatted[i - 1])))
                {
                    operand += curChar;
                    isOperand = true;
                }
                else if (Operators.Contains(curChar))
                    ProcessOperator(ref operand, ref isOperand, curChar);
                else 
                    throw new FormatException();
            }
            if (isOperand)
                ProcessEndOfToken(ref operand, ref isOperand);
            while (!_operators.IsEmpty)
                Tokens.Push(_operators.Pop().Notation);
        }

        private void ProcessOperator(ref string operand, ref bool isOperand, char curChar)
        {
            var op = BasicOperators.FullCollection.First(o => o.Alias == curChar);
            if (isOperand)
                ProcessEndOfToken(ref operand, ref isOperand);
            if (op.Alias == ')')
                ProcessParentheses();
            else
            {
                while (op.Alias != '(' && op.IsBinary && !_operators.IsEmpty && _operators.Top().Priority >= op.Priority)
                    Tokens.Push(_operators.Pop().Notation);
                _operators.Push(op);
            }
        }

        private void ProcessParentheses()
        {
            while (_operators.Top().Alias != '(')
                Tokens.Push(_operators.Pop().Notation);
            _operators.Pop();
        }

        private void ProcessEndOfToken(ref string operand, ref bool isOperand)
        {
            if (!double.TryParse(operand, out _) && !operand.ToCharArray().All(Alphabet.Contains))
                throw new Exception($"Unresolved token {operand}");
            if (operand.ToCharArray().All(Alphabet.Contains))
                Variables[operand] = 0;
            Tokens.Push(operand);
            isOperand = false;
            operand = "";
        }
    }
}