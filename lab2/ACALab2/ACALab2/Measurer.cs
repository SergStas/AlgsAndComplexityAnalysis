using System.Diagnostics;

namespace ACALab2
{
    public static class Measurer
    {
        public static double MeasureCommandExecuting(string row, StringStack stack, int iterations)
        {
            var watch = new Stopwatch();
            StringStack.ExecuteCommand(row, stack, _ => { });
            watch.Start();
            for (var i = 0; i < iterations; i++)
                StringStack.ExecuteCommand(row, stack, _ => { });
            watch.Stop();
            return watch.ElapsedMilliseconds / (double) iterations;
        }
    }
}