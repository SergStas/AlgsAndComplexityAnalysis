using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static ACALab1.Common;
using static ACALab1.Generator;

namespace ACALab1
{
    public static class SecondPart
    {
        private static readonly int[] _sizes = {100, 1000, 10000};
        
        public static void DoWork(int iterations, bool fileLog)
        {
            Console.WriteLine($"Repetitions count: {iterations}");
            PerformAllLaunches(
                c => Count(c as IEnumerable<int>),
                    c => (c as IEnumerable<int>)?.Count(),
                new Func<int, IEnumerable<int>>[]{ RandomIntGenerator, RandomIntGenerator, RandomIntGenerator },
                "Count",
                iterations,
                fileLog
            );
            Console.WriteLine($"Repetitions count: {iterations}");
            PerformAllLaunches(
                c => InsertSort(c as IEnumerable<int>),
                c => { (c as IEnumerable<int>)?.ToList().Sort(); return true; },
                new Func<int, IEnumerable<int>>[]
                { 
                    LinearIntSequence, 
                    RandomIntGenerator, 
                    c => LinearIntSequence(c, true)
                },
                "InsertSort",
                iterations,
                fileLog
            );
            Console.WriteLine("PT2: Done");
        }

        private static void PerformAllLaunches<TEl, TRes>(Func<IEnumerable, TRes> custom, Func<IEnumerable, TRes> system, 
            Func<int, IEnumerable<TEl>>[] generators, string name, int iterations, bool fileLog)
        {
            Console.WriteLine($"Algorithm: {name}\nRealization - custom:");
            for (var i = 0; i < 3; i++)
            {
                var type = i switch
                {
                    0 => "best",
                    1 => "normal",
                    _ => "worst"
                };
                Console.WriteLine("Input type - " + type);
                ProcessAlgorithm(custom, generators[i], name + "_custom_" + type, iterations, fileLog);
            }
            Console.WriteLine($"Realization - system:");
            for (var i = 0; i < 3; i++)
            {
                var type = i switch
                {
                    0 => "best",
                    1 => "normal",
                    _ => "worst"
                };
                Console.WriteLine("Input type - " + type);
                ProcessAlgorithm(system, generators[i], name + "_system_" + type, iterations, fileLog);
            }
            Console.WriteLine();
        }

        private static void ProcessAlgorithm<TEl, TRes>(Func<IEnumerable, TRes> alg, Func<int, IEnumerable<TEl>> generator, 
            string name, int iterations, bool fileLog)
        {
            var results = new List<double>();
            foreach (var size in _sizes)
            {
                var time = Measurer.MeasureOnGeneratedCollection(alg, generator, size, iterations);
                results.Add(time);
                ConsoleOutput(size, time);
            }
            if (fileLog)
                WriteToFile(_sizes, results.ToArray(), name);
        }
        
        private static int Count<T>(IEnumerable<T> arr)
        {
            var result = 0;
            if (arr is null) 
                return result;
            foreach (var _ in arr)
                result++;
            return result;
        }

        private static bool InsertSort<T>(IEnumerable<T> collection) where T : IComparable
        {
            var arr = collection?.ToArray();
            if (arr is null || arr.Length < 2)
                return false;
            for (int i = 1, j = i; i < arr.Length; i++, j = i)
                while (j > 0 && arr[j].CompareTo(arr[j - 1]) < 0)
                {
                    var temp = arr[j];
                    arr[j] = arr[j - 1];
                    arr[j - 1] = temp;
                    j--;
                }
            return true;
        }
    }
}