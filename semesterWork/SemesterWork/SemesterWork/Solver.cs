using System;
using System.Collections.Generic;
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
            _graph = _generator.GenerateGraph(true);
            FillOrders(_generator.GenerateOrdersSequence(_graph));
            OutputGraphDescription();

            var solution = new TaskProcessor(_graph, _orders).Solve();
            WriteLine();
            foreach (var i in solution) 
                WriteLine(i);
        }

        private void FillOrders(IEnumerable<(string, double)> orders)
        {
            foreach (var (label, mass) in orders) 
                _orders.Add(label, mass);
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

            foreach (var order in _orders) 
                WriteLine($"{order.Key}: {order.Value}");
        }

        private class TaskProcessor
        {
            private readonly CustomGraph _graph;
            private readonly Dictionary<string, double> _orders;
            private readonly string _storageLabel;

            public TaskProcessor(CustomGraph graph, Dictionary<string, double> dict)
            {
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
                var prevState = currentRoute.ToList();
                var prevMass = mass;
                if (_graph.NodesCount == currentRoute.Distinct().Count())
                {
                    currentRoute.Add(_storageLabel);
                    if (!(_graph.CalculateLength(currentRoute) < _graph.CalculateLength(bestRoute))) return;
                    bestRoute = currentRoute.ToList();
                    OutputNewResult(bestRoute, _graph.CalculateLength(bestRoute));
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

            private static void OutputNewResult(List<string> labels, double length)
            {
                WriteLine($"\tNew best route: {string.Join(" ", labels)} ({length})");
            } 
        }
    }
}