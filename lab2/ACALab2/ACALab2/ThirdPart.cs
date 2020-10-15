using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACALab2
{
    public static class ThirdPart
    {
        private const string MeasureInputCommandsPath = "measureInput.txt";
        private const string MeasureResultOutputPath = "measureResults.txt";
        private const char Splitter = '\t';
        private static readonly int[] CommandRepetitions = {10, 50, 100, 500, 1000};
        private static readonly string[] TestNames = {"PushFivePrint", "PushPopPrint", "PushCheckPrint"};
        
        public static void DoWork(int iterations, bool fileOutput)
        {
            var rows = File.ReadAllLines(MeasureInputCommandsPath);
            if (rows.Length != TestNames.Length)
                throw new ArgumentException("Tests' count and lines' count doesn't match");
            for (var i = 0; i < rows.Length; i++)
            {
                var results = new List<double>();
                Console.WriteLine($"Test \"{TestNames[i]}\":");
                foreach (var size in CommandRepetitions)
                {
                    var result = Measurer.MeasureCommandExecuting(rows[i], new StringStack(), iterations);
                    Console.WriteLine($"\tLength = {size}, time - {result} ms");
                    results.Add(result);
                }
                Console.WriteLine();
                if (fileOutput)
                    File.WriteAllLines(TestNames[i] + '_' + MeasureResultOutputPath,
                        CommandRepetitions.Zip(results).Select(tuple => tuple.First.ToString() + Splitter + tuple.Second));
            }
            Console.WriteLine("Done");
        }
    }
}