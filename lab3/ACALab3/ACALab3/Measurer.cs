using System;
using System.Diagnostics;

namespace ACALab3
{
    public static class Measurer
    {
        public static double Measure(Action act, int repetitions)
        {
            if (repetitions > 1)
                act();
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < repetitions; i++)
                act();
            watch.Stop();
            return watch.ElapsedMilliseconds / (double)repetitions;
        }
    }
}