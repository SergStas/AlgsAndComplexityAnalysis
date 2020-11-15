
using System.Text;
using static System.Console;

namespace ACALab4
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            new ConsoleInterface().Run();
            /*var tree = new RBTree();
            tree.Draw();
            for (var i = 0; i < 20; i++)
                tree.Add(i);
            tree.Draw();
            ReadLine();

            tree.Remove(6);
            tree.Draw();
            tree.Remove(4);
            tree.Draw();
            tree.Remove(5);
            tree.Draw();
            WriteLine(tree.FindMin());
            WriteLine(tree.FindMax());
            e(tree, 5);
            e(tree, 3);
            e(tree, 12);
            e(tree, 7);
            e(tree, 0);
            e(tree, 19);
            WriteLine();
            ReadLine();*/
        }
        static void e(RBTree tree, int k) => WriteLine(tree.FindNext(k) + " " + tree.FindPrev(k));
    }
}