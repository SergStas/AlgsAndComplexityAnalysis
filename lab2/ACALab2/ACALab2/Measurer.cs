using System.Diagnostics;

namespace ACALab2
{
    public static class Measurer
    {
        public static double MeasureCommandExecuting(string row, CustomStack<string> stack, int iterations)
        {
            var watch = new Stopwatch();
            CustomStack<string>.ExecuteCommand(row, stack, _ => { });
            watch.Start();
            for (var i = 0; i < iterations; i++)
                CustomStack<string>.ExecuteCommand(row, stack, _ => { });
            watch.Stop();
            return watch.ElapsedMilliseconds / (double) iterations;
        }
    }
}