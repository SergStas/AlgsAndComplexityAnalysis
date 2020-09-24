using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ACALab1
{
    public static class Measurer
    {
        public static double MeasureCollectionAlg<TEl, TRet>(Func<IEnumerable<TEl>, TRet> alg, IEnumerable<TEl> collection, int count)
        {
            var watch = new Stopwatch();
            alg(collection);
            watch.Start();
            for (var i = 0; i < count; i++)
                alg(collection);
            watch.Stop();
            return watch.ElapsedMilliseconds / 1000 / count;
        }
    }
}