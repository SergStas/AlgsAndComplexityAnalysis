using static System.Console;

namespace SemesterWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = FileParser.GetGraph("graphInput.txt");
            ReadLine();
        }
    }
}