using System;

namespace ACALab2
{
    public class Operator
    {
        public string Notation { get; }
        public int Priority { get; }
        public bool IsBinary { get; }
        public char Alias { get; }

        private readonly Func<double, double, double> _bi;
        private readonly Func<double, double> _uno;

        public Operator(string notation, char alias, int priority, Func<double, double, double> bi) : this(notation, alias, priority)
        {
            IsBinary = true;
            _bi = bi;
        }
        
        public Operator(string notation, char alias, int priority, Func<double, double> uno) : this(notation, alias, priority)
        {
            IsBinary = false;
            _uno = uno;
        }

        private Operator(string notation, char alias, int priority)
        {
            Notation = notation;
            Alias = alias;
            Priority = priority;
        }

        public double Execute(double first, double second)
        {
            if (!IsBinary)
                throw new ArgumentException();
            return _bi(first, second);
        }

        public double Execute(double first)
        {
            if (IsBinary)
                throw new ArgumentException();
            return _uno(first);
        }

        public override string ToString() => Notation;
    }
}