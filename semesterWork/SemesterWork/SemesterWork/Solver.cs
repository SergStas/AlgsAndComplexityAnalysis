using Graph;
using static System.Console;

namespace SemesterWork
{
    public class Solver
    {
        private readonly GraphGenerator _generator = new GraphGenerator();
        private CustomGraph _graph;

        public void Run()
        {
            _graph = _generator.GenerateGraph(true);
            OutputGraphDescription();
        }

        private void OutputGraphDescription()
        {
            WriteLine($"Nodes count - {_graph.NodesCount}\nNodes:");
            foreach (var node in _graph.Nodes())
            {
                WriteLine($"\t{node}:");
                foreach (var edge in node.Edges())
                    WriteLine($"\t\t{edge}");
            }
        }
    }
}