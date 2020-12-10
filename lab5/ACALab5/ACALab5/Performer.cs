using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace ACALab5
{
    public static class Performer
    {
        private static readonly TextProcessor Processor = new TextProcessor();
        private const string InputPath = "hobbit.txt";
        private const string OutputPath = "result.txt";
        private static readonly int[] Sizes = {/*100, 500, 1000, 2000, 5000, */10000};
        
        public static void DoWork(int iterations, bool fileOutput)
        {
            //PerformSorting();
            //ReadLine();
            PerformMeasures(iterations, fileOutput);
        }

        private static void PerformSorting()
        {
            SortText(StringSorter.QuickSort, "QuickSort", false);
            ReadLine();
            SortText(StringSorter.BubbleSort, "BubbleSort", true);
        }

        private static void SortText(Action<string[]> sortAlg, string algName, bool printWords)
        {
            WriteLine($"{algName}:");
            var tokens = Processor.ReadTokens(InputPath).ToArray();
            WriteLine($"Tokens count - {tokens.Length}\nSorting has begun");
            var elapsed = Measurer.Measure(sortAlg, () => tokens, 1, _ => { });
            WriteLine($"Sorted successfully, elapsed time - {elapsed}s\nPress enter to continue");
            ReadLine();
            if (printWords)
                foreach (var group in tokens.GroupBy(s => s))
                    WriteLine(group.Key + " - " + group.Count() + " times");
            WriteLine();
        }

        private static void PerformMeasures(int iterations, bool fileOutput)
        {
            Processor.AddToDictionary(InputPath);
            //MeasureOnGenerated(StringSorter.QuickSort, "QuickSort", iterations, fileOutput);
            ReadLine();
            MeasureOnGenerated(StringSorter.BubbleSort, "BubbleSort", iterations, fileOutput);
        }

        private static void MeasureOnGenerated(Action<string[]> alg, string algName, int iterations, bool fileOutput)
        {
            var results = new Dictionary<int, double>();
            WriteLine($"{algName}:");
            foreach (var size in Sizes)
            {
                WriteLine($"\tSize - {size}");
                var elapsed = Measurer.Measure(alg, 
                    () => Processor.GenerateText(size).Split(' '),
                    iterations,
                    s => WriteLine($"\t\t[MEASURER {DateTime.Now.ToLongTimeString()}]: {s}"));
                WriteLine($"\tElapsed time - {(int)(elapsed*100000)/100000.0}s");
                results.Add(size, elapsed);
            }
            if (fileOutput)
                File.WriteAllText(algName.ToLower() + "_" + OutputPath, 
                string.Join('\n', results.Select( pair => pair.Key + "\t" + (int)(pair.Value*100000)/100000.0)));
            WriteLine("\tDone");
        }
    }
}