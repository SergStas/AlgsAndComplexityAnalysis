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
            
            FirstPart.DoWork(100, false);
            // SecondPart.DoWork(10, false);
            
            watch.Stop();
            Console.WriteLine($"Elapsed time is {watch.Elapsed.ToString()}");
            Console.ReadLine();
        }
    }
}