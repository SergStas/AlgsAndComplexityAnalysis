using System;

namespace ACALab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new StringStack();
            s.Print();
            Console.WriteLine(s.IsEmpty);
            s.Push("132");
            s.Push("32");
            s.Push("cat");
            Console.WriteLine(s);
            Console.WriteLine(s.Count);
            Console.WriteLine(s.Top());
            s.Print();
            Console.WriteLine(s.IsEmpty);
            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());
            s.Print();
            Console.WriteLine(s.IsEmpty);
            try
            {
                s.Pop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + '\n');
            }
            var s0 = new StringStack("instanceInit.txt", true);
            Console.WriteLine(s0);
            Console.WriteLine(s0.Pop());
            Console.WriteLine(s0);
            var s1 = new StringStack(Console.ReadLine());
            Console.WriteLine(s1);
            Console.WriteLine(s1.Pop());
            Console.WriteLine(s1);
            Console.ReadLine();
        }
    }
}