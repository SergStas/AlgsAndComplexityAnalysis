using System;
using System.Collections.Generic;
using System.Linq;
using static System.Double;

namespace Graph
{
    public class CustomGraph
    {
        public int NodesCount => _nodes.Count;
        
        private readonly List<Node> _nodes = new List<Node>();

        public IEnumerable<Node> Nodes()
        {
            foreach (var node in _nodes)
                yield return node;
        }

        public Node GetNodeById(int id) => id < NodesCount && id >= 0 ? _nodes[id] : null;
        
        public void Add(string label) => _nodes.Add(new Node(label));

        public void Add(IEnumerable<string> labels)
        {
            foreach (var label in labels)
                Add(label);
        }

        public bool TryConnect(string first, string second, double weight = 0)
        {
            Node from = FindNode(first), to = FindNode(second);
            if (from is null || to is null || first == second || from.IsConnected(to))
                return false;
            Connect(first, second, weight);
            return true;
        }

        public void Connect(string first, string second, double weight = 0) => FindNode(first)?.Connect(FindNode(second), weight);

        public void Connect(IEnumerable<Tuple<string, string, double>> edges)
        {
            foreach (var (first, second, weight) in edges)
                Connect(first, second, weight);
        }

        public Node FindNode(string label) => Nodes().FirstOrDefault(n => n.Label == label);

        public void Remove(string label) => FindNode(label)?.Remove();

        public void Rename(string oldLabel, string newLabel) => FindNode(oldLabel).Rename(newLabel);

        public double CalculateLength(int[] indexes)
        {
            var result = (indexes is null || indexes.Length < 2) ? NaN : 0.0;
            for (var i = 1; i < indexes.Length; i++)
                result += GetNodeById(indexes[i]).Edges().FirstOrDefault(e => e.Contains(GetNodeById(indexes[i - 1])))?.Weight ?? NaN;
            if (IsNaN(result))
                result = MaxValue;
            return result;
        }

        public double CalculateLength(IEnumerable<string> labels) => CalculateLength(labels
            .Select(l => _nodes.IndexOf(_nodes.Find(n => n.Label == l))).ToArray());
    }
}