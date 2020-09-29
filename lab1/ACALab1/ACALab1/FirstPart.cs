using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ACALab1
{
    public static class FirstPart
    {
        private const char LINE_SPLITTER = '\n';
        private const char TOKEN_SPLITTER = '\t'; 
        private static readonly int[] _sizes = new[]
        {
            100, 250, 500, 750, 1000, 1250, 2500, 5000, 7500, 10000
        };
        
        public static void DoWork(int iterations, bool fileLog)
        {
            Console.WriteLine($"Repetitions count: {iterations}");
            ProcessAlg(c => Max(c as IEnumerable<int>), "Max", iterations, fileLog);
            ProcessAlg(c => Count(c as IEnumerable<int>), "Count", iterations, fileLog);
            ProcessAlg(c => ArrayBubbleSort(c as IEnumerable<int>), "BubbleSort", iterations, fileLog);
        }
        
        public static T Max<T>(IEnumerable<T> collection) where T : IComparable
        {
            try
            {
                var result = collection.First();
                foreach (var e in collection)
                    if (e.CompareTo(result) > 0)
                        result = e;
                return result;
            }
            catch { return default; }
        }

        public static int Count<T>(IEnumerable<T> collection)
        {
            var result = 0;
            if (collection is null) 
                return result;
            foreach (var _ in collection)
                result++;
            return result;
        }

        public static bool ArrayBubbleSort<T>(IEnumerable<T> collection) where T : IComparable
        {
            var arr = collection?.ToArray();
            if (arr is null)
                return false;
            for (var i = 0; i < arr.Length; i++)
            for (var j = 0; j < arr.Length; j++)
                if (arr[i].CompareTo(arr[j]) > 0)
                {
                    var temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            return true;
        }

        private static void ProcessAlg<TRes>(Func<IEnumerable, TRes> alg, string name, int iterations, bool fileLog)
        {
            Console.WriteLine($"Algorithm: {name}");
            var results = new List<double>();
            foreach (var size in _sizes)
            {
                var time = Measurer.MeasureOnRandomCollections(alg, Generator.IntListGenerator, size, iterations);
                results.Add(time);
                ConsoleOutput(size, time);
            }
            if (fileLog)
                WriteToFile(_sizes, results.ToArray(), name);
        }

        private static void ConsoleOutput(int size, double result) =>
            Console.WriteLine($"\tLength: {size}, time(ms): {result}");

        private static void WriteToFile(int[] sizes, double[] results, string algName)
        {
            var builder = new StringBuilder();
            if (sizes.Length != results.Length)
                throw new ArgumentException();
            for (var i = 0; i < sizes.Length; i++)
                builder.Append($"{sizes[i]}{TOKEN_SPLITTER}{results[i]}{LINE_SPLITTER}");
            File.WriteAllText(algName + ".txt", builder.ToString());
            Console.WriteLine($"Logged to {algName}.txt successfully");
        }
    }
}