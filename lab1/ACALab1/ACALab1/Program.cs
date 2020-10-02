using System;
using System.Diagnostics;

namespace ACALab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();
            //FirstPart.DoWork(1000, true);
            SecondPart.DoWork(1000, true);
            watch.Stop();
            Console.WriteLine($"Elapsed time is {watch.Elapsed.ToString()}");
            Console.ReadLine();
        }
    }
}