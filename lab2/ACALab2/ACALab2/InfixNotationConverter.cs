using System;
using System.Collections.Generic;
using System.Linq;

namespace ACALab2
{
    public class InfixNotationConverter
    {
        public string InfixNotation { get; }
        
        private readonly CustomStack<string> _tokens = new CustomStack<string>();
        private readonly CustomStack<Operator> _operators = new CustomStack<Operator>();
        
        private static readonly char[] Splitters = {' ', '\t', '\n'};
        private static List<char> Alphabet => Enumerable.Range('a', 'z').Select(i => (char) i).ToList();

        private static List<char> Operators => BasicOperators.FullCollection.Select(op => op.Alias).ToList();
        
        public InfixNotationConverter(string infix) => InfixNotation = infix;
        
        public string ConvertToPostfix()
        {
            var formatted = GetFormattedInfix();
            ProcessInfix(formatted);
            return _tokens.ToString();
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
                {
                    var op = BasicOperators.FullCollection.First(o => o.Alias == curChar);
                    if (isOperand)
                        ProcessEndOfToken(ref operand, ref isOperand);
                    if (op.Alias == ')')
                        ProcessParentheses();
                    else
                    {
                        while (op.Alias != '(' && op.IsBinary && !_operators.IsEmpty && _operators.Top().Priority >= op.Priority)
                            _tokens.Push(_operators.Pop().Notation);
                        _operators.Push(op);
                    }
                }
                else 
                    throw new FormatException();
            }
            if (isOperand)
                ProcessEndOfToken(ref operand, ref isOperand);
            while (!_operators.IsEmpty)
                _tokens.Push(_operators.Pop().Notation);
        }

        private void ProcessParentheses()
        {
            while (_operators.Top().Alias != '(')
                _tokens.Push(_operators.Pop().Notation);
            _operators.Pop();
        }

        private void ProcessEndOfToken(ref string operand, ref bool isOperand)
        {
            if (double.TryParse(operand, out _))
                _tokens.Push(operand);
            else
                throw new NotImplementedException();
            isOperand = false;
            operand = "";
        }
    }
}