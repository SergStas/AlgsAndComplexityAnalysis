using System;
using System.Collections;
using System.Collections.Generic;

namespace ACALab1
{
    public static class Generator
    {
        public static List<int> IntListGenerator(int count)
        {
            var result = new List<int>();
            var random = new Random();
            for (var i = 0; i < count; i++)
                result.Add(random.Next());
            return result;
        }
    }
}