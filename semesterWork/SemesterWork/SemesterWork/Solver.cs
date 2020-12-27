using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Graph;
using static System.Console;

namespace SemesterWork
{
    public class Solver
    {
        private readonly GraphGenerator _generator = new GraphGenerator();
        private CustomGraph _graph;
        private readonly Dictionary<string, double> _orders = new Dictionary<string, double>();

        public void Run()
        {
            var watch = new Stopwatch();
            Write("Input count of nodes: ");
            var count = -1;
            while (count == -1)
                try{ count = int.Parse(ReadLine());}
                catch{Write("Incorrect input, try again: ");}
            _graph = _generator.GenerateGraph(count, true);
            FillOrders(_generator.GenerateOrdersSequence(_graph));
            OutputGraphDescription();
            WriteLine("Press enter to start calculations");
            ReadLine();
            
            WriteLine("Calculations has started...");
            watch.Start();

            var solution = new TaskProcessor(_graph, _orders, false).Solve();
            WriteLine();
            watch.Stop();
            
            WriteLine($"==========================\nSolution found in {Math.Round(watch.ElapsedMilliseconds / 1000.0, 2)}s:");
            
            TaskProcessor.OutputNewResult(solution, _graph.CalculateLength(solution), true);
        }

        private void FillOrders(IEnumerable<(string, double)> orders)
        {
            foreach (var (label, mass) in orders) 
                _orders.Add(label, mass);
        }

        private void OutputGraphDescription()
        {
            WriteLine("\nGraph created:");
            WriteLine($"Nodes count - {_graph.NodesCount}\nNodes:");
            foreach (var node in _graph.Nodes())
            {
                WriteLine($"\t{node}:");
                foreach (var edge in node.Edges())
                    WriteLine($"\t\t{edge}");
            }

            WriteLine("Orders:");
            foreach (var (key, value) in _orders) 
                WriteLine($"\t{key}: {value}");
        }

        private class TaskProcessor
        {
            private readonly CustomGraph _graph;
            private readonly Dictionary<string, double> _orders;
            private readonly string _storageLabel;
            private int _recursionCounter;
            private readonly bool _outputResults;

            public TaskProcessor(CustomGraph graph, Dictionary<string, double> dict, bool outputResults)
            {
                _outputResults = outputResults;
                _graph = graph;
                _orders = dict;
                _storageLabel = _graph.GetNodeById(0).Label;
            }

            public List<string> Solve()
            {
                var result = new List<string>();
                DoRecursion(new List<string>{_storageLabel}, ref result, 0);
                return result;
            }

            private void DoRecursion(List<string> currentRoute, ref List<string> bestRoute, double mass)
            {
                _recursionCounter++;
                if (_recursionCounter % 50000 == 0)
                    WriteLine($"Recursive call #{_recursionCounter}");
                var prevState = currentRoute.ToList();
                var prevMass = mass;
                if (_graph.NodesCount == currentRoute.Distinct().Count())
                {
                    currentRoute.Add(_storageLabel);
                    if (!(_graph.CalculateLength(currentRoute) < _graph.CalculateLength(bestRoute))) return;
                    bestRoute = currentRoute.ToList();
                    if (_outputResults)
                        OutputNewResult(bestRoute, _graph.CalculateLength(bestRoute), false);
                    return;
                }
                var lastNode = _graph.FindNode(currentRoute[^1]);
                var neighbours = lastNode.IncidentNodes()
                    .Where(n => !currentRoute.Contains(n.Label))
                    .Append(_graph.FindNode(_storageLabel)).ToList();
                if (lastNode.Label == _storageLabel)
                    neighbours.Remove(_graph.FindNode(_storageLabel));
                foreach (var incidentNode in neighbours)
                {
                    if (_storageLabel != incidentNode.Label && mass + _orders[incidentNode.Label] > 5)
                    {
                        mass = 0;
                        currentRoute.Add(_storageLabel);
                    }
                    currentRoute.Add(incidentNode.Label);
                    mass = _storageLabel == incidentNode.Label ? 0 : mass + _orders[incidentNode.Label];
                    if (_graph.CalculateLength(currentRoute) < _graph.CalculateLength(bestRoute))
                        DoRecursion(currentRoute, ref bestRoute, mass);
                    currentRoute = prevState.ToList();
                    mass = prevMass;
                }
            }

            public static void OutputNewResult(List<string> labels, double length, bool final) => 
                WriteLine($"\t{(final ? "Final solution" : "New best solution found")}: \n\t-{string.Join("\n\t-", labels)} ({length})");
        }
    }
}