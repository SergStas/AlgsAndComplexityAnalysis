using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ACALab5
{
    public static class Measurer
    {
        public static double Measure(Action<string[]> alg, Func<string[]> generator, int repetitions, Action<string> log)
        {
            var watch = new Stopwatch();
            alg(generator());
            var collections = new List<string[]>();
            log("Generating collections...");
            log("Collections generated successfully");
            for (var i = 0; i < repetitions; i++)
                collections.Add(generator());
            log("Measuring has begun");
            watch.Start();
            for (var i = 0; i < repetitions; i++)
                alg(collections[i]);
            watch.Stop();
            log("Measuring finished");
            return watch.ElapsedMilliseconds / 1000.0 / repetitions;
        }
    }
}