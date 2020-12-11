using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public void Add(string label) => _nodes.Add(new Node(label));

        public void Add(IEnumerable<string> labels)
        {
            foreach (var label in labels)
                Add(label);
        }

        public void Connect(string first, string second, double weight = 0) => FindNode(first)?.Connect(FindNode(second), weight);

        public void Connect(IEnumerable<Tuple<string, string, double>> edges)
        {
            foreach (var (first, second, weight) in edges)
                Connect(first, second, weight);
        }

        public Node FindNode(string label) => Nodes().FirstOrDefault(n => n.Label == label);

        public void Remove(string label) => FindNode(label)?.Remove();
    }
}