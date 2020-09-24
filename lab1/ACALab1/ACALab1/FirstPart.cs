using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ACALab1
{
    public static class FirstPart
    {
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

        public static void ArrayBubbleSort<T>(T[] collection) where T : IComparable
        {
            if (collection is null)
                return;
            for (var i = 0; i < collection.Length - 1; i++)
            for (var j = i + 1; j < collection.Length; j++)
                if (collection[i].CompareTo(collection[j]) > 0)
                {
                    var temp = collection[i];
                    collection[i] = collection[j];
                    collection[j] = temp;
                }
        }
    }
}