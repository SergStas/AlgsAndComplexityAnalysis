using System;
using System.Collections.Generic;

namespace ACALab3
{
    public static class SecondPart
    {
        private const int Count = 50;
        private const int Range = 25;
        
        public static void DoWork()
        {
            CheckDepth();
            Console.ReadLine();
            CheckRemove();
        }

        private static void CheckDepth()
        {
            var tree = new BinarySearchTree<int>();
            var rnd = new Random();
            for (var i = 0; i < Count; i++)
                tree.Add(rnd.Next(0, Range) + 1);
            tree.Draw();
            foreach (var e in tree)
                Console.Write(e + " ");
            Console.WriteLine("\nMax depth of tree - " + tree.CalculateDepth());
            Console.WriteLine();
        }

        private static void CheckRemove()
        {
            var tree = new BinarySearchTree<int>();
            var rnd = new Random();
            for (var i = 0; i < Count; i++)
                tree.Add(rnd.Next(0, Range) + 1);
            tree.Draw();
            var list = new List<int>();
            foreach (var e in tree)
                list.Add(e);
            foreach (var e in tree)
                Console.Write(e + " ");
            Console.Write("\nInput value to remove: ");
            var s = Console.ReadLine();
            if (!int.TryParse(s, out var n))
                Console.WriteLine("Incorrect value");
            else if (!tree.Contains(n))
                Console.WriteLine("Tree doesn't contains value " + n);
            else
                while (tree.Contains(n))
                    tree.Remove(n);
            if (int.TryParse(s, out _) && list.Contains(n))
                tree.Draw();
        }
    }
}