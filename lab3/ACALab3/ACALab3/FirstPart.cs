using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACALab3
{
    public static class FirstPart
    {
        private static readonly int[] Sizes = {25, 1000, 5000, 10000};
        private static readonly int[] Ranges = {50, 10000, 500};
        private const string TreeTestsResultsPath = "result_tree_";
        private const string ListTestsResultsPath = "result_sort_";

        public static void DoWork(bool fileOutput)
        {
            Console.WriteLine("Tree tests:");
            PerformMeasures(fileOutput, true);
            Console.ReadLine();
            Console.WriteLine("List tests:");
            PerformMeasures(fileOutput, false);
        }
        
        private static void PerformMeasures(bool fileOutput, bool tree)
        {
            var testsParams = Sizes.Skip(1).SelectMany(size => Ranges.Skip(1).Select((range) => (size, range).ToTuple()));
            if (fileOutput)
                foreach (var range in Ranges)
                    if (File.Exists($"{(tree ? TreeTestsResultsPath : ListTestsResultsPath)}{range}.txt"))
                        File.Delete($"{(tree ? TreeTestsResultsPath : ListTestsResultsPath)}{range}.txt");
            var i = 0;
            if (tree)
                ProcessTest(Ranges[0], Sizes[0], i++, 1, fileOutput, true, true);
            foreach (var (range, size) in testsParams)
                ProcessTest(size, range, i++, 100, fileOutput, false, tree);
            Console.WriteLine("Done");
        }

        private static void ProcessTest(int range, int size, int testNumber, int enumeratingRepetitions, bool fileOutput, bool consoleOutput, bool tree)
        {
            Console.WriteLine($"Test #{testNumber}: collection type - {(tree ? "tree" : "list")}, count = {size}, range = [1; {range}]");
            var (fill, enumerate) =
                tree ? MeasureTreeTest(range, size, enumeratingRepetitions, consoleOutput)
                    : MeasureListTest(range, size);
            Console.WriteLine($"\tElapsed time - {fill + enumerate}ms (filling - {fill}ms, enumerating - {enumerate}ms");
            if (fileOutput)
                File.AppendAllText($"{(tree ? TreeTestsResultsPath : ListTestsResultsPath)}{range}.txt", $"{size}\t{fill + enumerate}\n");
            Console.WriteLine();
            if (!consoleOutput)
                return;
            Console.WriteLine("Test completed, press Enter to continue");
            Console.ReadLine();
        }

        private static Tuple<double, double> MeasureTreeTest(int range, int count, int enumeratingRepetitions, bool writeToConsole)
        {
            var tree = new BinarySearchTree<int>();
            tree.Add(0);
            tree.Remove(0);
            var rnd = new Random();
            var filling = Measurer.Measure(() =>
            {
                for (var i = 0; i < count; i++)
                    tree.Add(rnd.Next(0, range) + 1);
            }, 1);
            if (writeToConsole)
                tree.Draw();
            var enumerating = Measurer.Measure(() =>
            {
                foreach (var e in tree)
                    if (writeToConsole)
                        Console.Write(e + " ");
                if (writeToConsole)
                    Console.WriteLine();
            }, enumeratingRepetitions);
            return (filling, enumerating).ToTuple();
        }

        private static Tuple<double, double> MeasureListTest(int range, int count)
        {
            var list = new List<int>();
            list.Add(0);
            list.Remove(0);
            var rnd = new Random();
            var filling = Measurer.Measure(() =>
            {
                for (var i = 0; i < count; i++)
                    list.Add(rnd.Next(0, range) + 1);
            }, 1);
            var enumerating = Measurer.Measure(() =>
            {
                foreach (var _ in list){}
            }, 100);
            return (filling, enumerating).ToTuple();
        }
    }
}