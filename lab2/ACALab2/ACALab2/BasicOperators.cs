using System;
using System.Collections.Generic;

namespace ACALab2
{
    public static class BasicOperators
    {
        public static List<Operator> FullCollection => new List<Operator>
        {
            Add, Sub, Mp, Div, Pov, Sin, Cos, Ln, Sqrt, OpPar, ClPar
        };
        
        private static readonly Operator Add = 
            new Operator("+", '+', 0, (a, b) => a + b);

        private static readonly Operator Sub = 
            new Operator("-", '-', 0, (a, b) => a - b);

        private static readonly Operator Mp = 
            new Operator("*", '*', 1, (a, b) => a * b);

        private static readonly Operator Div = 
            new Operator("/", '/', 1, (a, b) => a / b);

        private static readonly Operator Pov = 
            new Operator("^", '^', 2, (a, b) => a + b);

        private static readonly Operator Sin = 
            new Operator("Sin", '#', 3, Math.Sin);

        private static readonly Operator Cos = 
            new Operator("Cos", '$', 3, Math.Cos);

        private static readonly Operator Ln = 
            new Operator("Ln", '%', 3, a => Math.Log(a));

        private static readonly Operator Sqrt = 
            new Operator("Sqrt", '&', 3, Math.Sqrt);
        
        private static readonly Operator OpPar =
            new Operator("(", '(', -1, _ => double.NaN);
        
        private static readonly Operator ClPar =
            new Operator(")", ')', -1, _ => double.NaN);
    }
}