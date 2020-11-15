using System.Text;
using static System.Console;

namespace ACALab4
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            var tree = new RBTree();
            tree.Draw();
            for (var i = 0; i < 20; i++)
            {
                tree.Add(i);
                tree.Draw();
            }
            ReadLine();
        }
    }
}