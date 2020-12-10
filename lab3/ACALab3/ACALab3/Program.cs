using System;
using System.Text;
using static System.Console;

namespace ACALab3
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            /*FirstPart.DoWork(false);
            ReadLine();*/
            /*
            SecondPart.DoWork();
            ReadLine();
            CheckBasicFunctional();
            ReadLine();*/
            var e = new BinarySearchTree<int>();
            var f = new int[] {1, 6, 3, 2, 6, 8, 3};
            for (var i = 0; i < f.Length; i++)
            {
                e.Add(f[i]);
                Clear();
                e.Draw();
                ReadKey();
            }
        }

        private static void CheckBasicFunctional()
        {
            var tree = new BinarySearchTree<int>();
            tree.Draw();
            for (var i = 0; i < 20; i += 4)
            {
                tree.Add(i);
                tree.Draw();
                WriteLine(tree.CalculateDepth());
            }
            for (var i = -1; i < 20; i += 4)
            {
                tree.Add(i);
                tree.Draw();
                WriteLine(tree.CalculateDepth());
            }
            foreach (var e in tree)
                Write(e + " ");
            tree.Draw();
            WriteLine(tree.CalculateDepth());
            WriteLine();
            WriteLine(tree.Contains(-4));
            WriteLine(tree.Contains(4));
            WriteLine(tree.Contains(5));
            tree.Remove(11);
            tree.Draw();
            WriteLine(tree.CalculateDepth());
            tree.Remove(8);
            tree.Draw();
            WriteLine(tree.CalculateDepth());
            tree.Remove(16);
            tree.Draw();
            WriteLine(tree.CalculateDepth());
            tree.Remove(0);
            tree.Draw();
            WriteLine(tree.CalculateDepth());
            foreach (var e in tree)
                Write(e + " ");
            WriteLine();
            WriteLine();
            WriteLine();
            WriteLine();
            var tree0 = new BinarySearchTree<int>();
            for (var i = 0; i < 25; i++)
                tree0.Add(new Random().Next(0, 50));
            tree0.Draw();
            WriteLine(tree0.CalculateDepth());
            foreach (var e in tree0)
                Write(e + " ");
        }
    }
}