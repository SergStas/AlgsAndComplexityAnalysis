using System;
using System.IO;

namespace ACALab2
{
    public static class FunctionalCheck
    {
        private const string BasicTestsPath = "instanceInit.txt";
        private const string InputCommandsPath = "input.txt";
        private const string InfixInputDirectoryPath = "infix";

        public static void CheckInfixConvert()
        {
            foreach (var path in Directory.EnumerateFiles(InfixInputDirectoryPath))
            {
                Console.WriteLine($"Case \"{path}\":");
                var converter = new InfixNotationConverter(path);
                Console.WriteLine($"Infix: \"{converter.InfixNotation}\"");
                Console.WriteLine("Postfix: \"" + converter.GetPostfixString(' ') + "\"\nVariables:");
                foreach (var (name, value) in converter.Variables)
                    Console.WriteLine($"\t{name} = {value};");
                Console.WriteLine();
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