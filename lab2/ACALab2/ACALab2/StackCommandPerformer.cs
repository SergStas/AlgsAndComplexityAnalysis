using System;

namespace ACALab2
{
    public partial class StringStack
    {
        private static readonly Action<string> DefaultOutput = Console.WriteLine;
        
        public static void ExecuteCommand(string query, StringStack stack, Action<string> output)
        {
            var tokens = query.Split(Splitters);
            foreach (var token in tokens)
                ProcessToken(stack, token, output);
        }

        public static void ExecuteCommand(string query, StringStack stack) => ExecuteCommand(query, stack, DefaultOutput);
        
        private static void ProcessToken(StringStack stack, string token, Action<string> output)
        {
            var value = token.Length == 1 ? null : token.Substring(2);
            var command = int.Parse(token[0].ToString());
            switch (command)
            {
                case 1:
                    stack.Push(value);
                    break;
                case 2:
                    output(stack.Pop());
                    break;
                case 3:
                    output(stack.Top());
                    break;
                case 4:
                    output(stack.IsEmpty.ToString());
                    break;
                case 5:
                    stack.Print(output);
                    break;
            }
        }
    }
}