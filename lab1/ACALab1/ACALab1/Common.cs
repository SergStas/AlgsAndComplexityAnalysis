using System;
using System.IO;
using System.Text;

namespace ACALab1
{
    public static class Common
    {
        private const char LINE_SPLITTER = '\n';
        private const char TOKEN_SPLITTER = '\t';
        
        public static void ConsoleOutput(int size, double result) =>
            Console.WriteLine($"\tLength: {size}, time(ms): {result}");

        public static void WriteToFile(int[] sizes, double[] results, string algName)
        {
            var builder = new StringBuilder();
            if (sizes.Length != results.Length)
                throw new ArgumentException();
            for (var i = 0; i < sizes.Length; i++)
                builder.Append($"{sizes[i]}{TOKEN_SPLITTER}{results[i]}{LINE_SPLITTER}");
            File.WriteAllText(algName + ".txt", builder.ToString());
            Console.WriteLine($"Logged to {algName}.txt successfully");
        }
        
    }
}