using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ACALab1
{
    public static class Measurer
    {
        public static double MeasureOnSpecifiedCollection<TEl, TRet>(Func<IEnumerable<TEl>, TRet> algorithm,
            IEnumerable<TEl> collection)
        {
            algorithm(new TEl[0]);
            return Measure(() => algorithm(collection)) / 1000;
        }
        
        public static double MeasureOnRandomCollections<TEl, TRet>(Func<IEnumerable<TEl>, TRet> algorithm, 
            Func<int, IEnumerable<TEl>> generator, int length, int iterations)
        {
            var collections = GenerateCollections(generator, length, iterations);
            algorithm(generator(length));
            return Measure(() =>
                { foreach (var collection in collections) algorithm(collection); }) / 1000 / iterations;
        }

        private static TEl[][] GenerateCollections<TEl>(Func<int, IEnumerable<TEl>> generator, int length, int iterations)
        /*  Сложность операции генерации коллекций - не меньше O(N)
            время, затраченное на генерацию, в рассчет не идет        */ 
        {
            var result = new TEl[][iterations];
            for (var i = 0; i < iterations; i++)
                result[i] = generator(length).ToArray();
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