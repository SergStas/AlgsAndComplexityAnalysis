using System;
using System.Collections;
using System.Collections.Generic;

namespace ACALab1
{
    public static class Generator
    {
        public static int[] RandomIntGenerator(int count)
        {
            var result = new List<int>();
            var random = new Random();
            for (var i = 0; i < count; i++)
                result.Add(random.Next());
            return result.ToArray();
        }

        public static int[] LinearIntSequence(int count) =>
            LinearIntSequence(count, false);

        public static int[] LinearIntSequence(int count, bool descending)
        {
            var result = new int[count];
            for (var i = 0; i < count; i++)
                result[i] = descending ? count - i - 1 : i;
            return result;
        }
    }
}