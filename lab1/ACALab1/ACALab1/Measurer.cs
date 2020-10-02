using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ACALab1
{
    public static class Measurer
    {
        public static double MeasureOnSpecifiedCollection<TEl, TRes>(Func<IEnumerable<TEl>, TRes> algorithm,
            IEnumerable<TEl> collection)
        {
            var e = algorithm(new TEl[0]);
            return Measure(() => algorithm(collection));
        }
        
        public static double MeasureOnGeneratedCollection<TEl, TRes>(Func<IEnumerable<TEl>, TRes> algorithm, 
            Func<int, IEnumerable<TEl>> generator, int length, int iterations)
        {
            var collections = GenerateCollections(generator, length, iterations);
            var e = algorithm(generator(length));
            return Measure(() =>
                { foreach (var collection in collections) algorithm(collection); }) / (double)iterations;
        }

        private static List<List<TEl>> GenerateCollections<TEl>(Func<int, IEnumerable<TEl>> generator, int length, int iterations)
        /*  Сложность операции генерации коллекций - не меньше O(N)
            время, затраченное на генерацию, в рассчет не идет        */ 
        {
            var result = new List<List<TEl>>();
            for (var i = 0; i < iterations; i++)
                result.Add(generator(length).ToList());
            return result;
        }

        private static long Measure(Action act)
        {
            var watch = new Stopwatch();
            watch.Start();
            act();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}