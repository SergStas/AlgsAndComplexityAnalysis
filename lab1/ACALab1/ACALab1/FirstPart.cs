using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ACALab1
{
    public static class FirstPart
    {
        private static readonly int[] _sizes = new[]
        {
            100, 250, 500, 750, 1000, 1250, 2500, 5000, 7500, 10000
        };
        
        public static void DoWork(int iterations)
        {
            Console.WriteLine($"Repetitions count: {iterations}");
            ProcessAlg(c => Max(c as IEnumerable<int>), "Max", iterations);
            ProcessAlg(c => Count(c as IEnumerable<int>), "Count", iterations);
            ProcessAlg(c => ArrayBubbleSort(c as IEnumerable<int>), "BubbleSort", iterations);
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
            var arr = collection.ToArray();
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

        private static void ProcessAlg<TRes>(Func<IEnumerable, TRes> alg, string name, int iterations)
        {
            Console.WriteLine($"Algorithm: {name}");
            foreach (var size in _sizes)
            {
                var time = Measurer.MeasureOnRandomCollections(Max, Generators.IntListGenerator, size, iterations);
                ConsoleOutput(size, time);
            }
        }

        private static void ConsoleOutput(int size, double result) =>
            Console.WriteLine($"\tLength: {size}, time(ms): {result}");
    }
}