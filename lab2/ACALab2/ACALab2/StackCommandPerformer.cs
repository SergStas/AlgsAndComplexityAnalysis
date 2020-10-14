using System;

namespace ACALab2
{
    public partial class StringStack
    {
        private static readonly Action<string> DefaultOutput = Console.WriteLine;
        
        public static void ExecuteCommand(string query)
        {
            var tokens = query.Split(Splitters);
            var stack = new StringStack();
            foreach (var token in tokens)
                ProcessToken(stack, token, DefaultOutput);
        }

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