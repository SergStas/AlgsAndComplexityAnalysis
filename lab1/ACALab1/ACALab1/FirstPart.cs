using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static ACALab1.Common;

namespace ACALab1
{
    public static class FirstPart
    { 
        private static readonly int[] _sizes =
        {
            200000
        };
        
        public static void DoWork(int iterations, bool fileLog)
        {
            Console.WriteLine($"Repetitions count: {iterations}");
            ProcessAlg(c => Max(c as IEnumerable<int>), "Max", iterations, fileLog);
            ProcessAlg(c => Count(c as IEnumerable<int>), "Count", iterations, fileLog);
            ProcessAlg(c => BubbleSort(c as IEnumerable<int>), "BubbleSort", iterations, fileLog);
            Console.WriteLine("PT1: Done");
        }

        private static T Max<T>(IEnumerable<T> collection) where T : IComparable
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

        private static int Count<T>(IEnumerable<T> collection)
        {
            var result = 0;
            if (collection is null) 
                return result;
            foreach (var _ in collection)
                result++;
            return result;
        }

        private static bool BubbleSort<T>(IEnumerable<T> collection) where T : IComparable
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
                var time = Measurer.MeasureOnGeneratedCollection(alg, Generator.RandomIntGenerator, size, iterations);
                results.Add(time);
                ConsoleOutput(size, time);
            }
            if (fileLog)
                WriteToFile(_sizes, results.ToArray(), name);
        }
    }
}