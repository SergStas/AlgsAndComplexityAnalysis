using System.Diagnostics;

namespace ACALab2
{
    public static class Measurer
    {
        public static double MeasureCommandExecuting<T>(string row, CustomStack<T> stack, int iterations)
        {
            var watch = new Stopwatch();
            CustomStack<T>.ExecuteCommand(row, stack, _ => { });
            watch.Start();
            for (var i = 0; i < iterations; i++)
                CustomStack<T>.ExecuteCommand(row, stack, _ => { });
            watch.Stop();
            return watch.ElapsedMilliseconds / (double) iterations;
        }
    }
}