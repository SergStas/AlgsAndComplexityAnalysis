using System;

namespace ACALab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var e = new[] {"s", "as", "", "b", ""};
            StringSorter.QuickSort(e);
        }
    }
}