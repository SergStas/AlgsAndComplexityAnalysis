using System;
using System.IO;

namespace ACALab2
{
    public static class FunctionalCheck
    {
        private const string BasicTestsPath = "instanceInit.txt";
        private const string InputCommandsPath = "input.txt";

        private static readonly string[] InfixExpressions =
        {
            "1 + 2",
            "1 + 2 * 3",
            "2 / 2 ^ 3",
            "sqrt 12 + 4 - ln cos 45 + 3 * sin 2 ^ 5",
            "1 + 2 * 3 / 4 - 1 + 2 + Ln 3 ^ 6"
        };

        public static void CheckInfixConvert()
        {
            foreach (var infix in InfixExpressions)
            {
                Console.WriteLine($"Case \"{infix}\":");
                Console.WriteLine(new InfixNotationConverter(infix).ConvertToPostfix() + '\n');
            }
        }

        public static void CheckInputQueries()
        {
            var lines = File.ReadAllLines(InputCommandsPath);
            for (var i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"Launch#{i + 1}, command line - \"{lines[i]}\"");
                CustomStack<string>.ExecuteCommand(lines[i], new CustomStack<string>());
                Console.WriteLine("\n==============================");
            }
        }

        public static void CheckBasicFunctional()
        {
            var s = new CustomStack<string>();
            s.Print();
            Console.WriteLine(s);
            Console.WriteLine(s.IsEmpty);
            s.Push("132");
            s.Push("32");
            s.Push("cat");
            Console.WriteLine(s);
            Console.WriteLine(s.Count);
            Console.WriteLine(s.Top());
            s.Print();
            var output = "";
            s.Print(str => output += str);
            Console.WriteLine(output);
            Console.WriteLine(s.IsEmpty);
            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());
            s.Print();
            Console.WriteLine(s.IsEmpty);
            s.Pop();
            var s0 = new CustomStack<string>(BasicTestsPath, true);
            Console.WriteLine(s0);
            Console.WriteLine(s0.Pop());
            Console.WriteLine(s0);
            var s1 = new CustomStack<string>(Console.ReadLine());
            Console.WriteLine(s1);
            Console.WriteLine(s1.Pop());
            Console.WriteLine(s1);
        }
        
    }
}