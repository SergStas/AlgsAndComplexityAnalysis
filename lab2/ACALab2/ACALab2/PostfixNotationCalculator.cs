using System;
using System.Linq;

namespace ACALab2
{
    public class PostfixNotationCalculator
    {
        private readonly InfixNotationConverter _converter;
        private readonly CustomStack<double> _values = new CustomStack<double>();

        public PostfixNotationCalculator(InfixNotationConverter converter) => _converter = converter;

        public double Calculate(Action<string> log)
        {
            foreach (var token in _converter.Tokens)
            {
                if (_converter.Variables.ContainsKey(token))
                    _values.Push(_converter.Variables[token]);
                else if (double.TryParse(token, out var value))
                    _values.Push(value);
                else
                {
                    var op = BasicOperators.FullCollection.First(o => o.Notation == token);
                    if (op.IsBinary)
                    {
                        var first = _values.Pop();
                        _values.Push(op.Execute(_values.Pop(), first, log));
                    }
                    else
                        _values.Push(op.Execute(_values.Pop(), log));
                }
            }
            return _values.Pop();
        }
    }
}