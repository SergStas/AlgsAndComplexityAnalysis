using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Node
    {
        public string Label { get; }
        public int Power => _edges.Count;
        
        private readonly List<Edge> _edges = new List<Edge>();

        public Node(string label) => Label = label;

        public IEnumerable<Edge> Edges()
        {
            foreach (var edge in _edges)
                yield return edge;
        }
        
        public IEnumerable<Node> IncidentNodes()
        {
            foreach (var edge in _edges)
                yield return edge.SecondNode(this);
        }

        public Edge Connect(Node node, double weight = 0)
        {
            var edge = new Edge(this, node, weight);
            _edges.Add(edge);
            node._edges.Add(edge);
            return edge;
        }

        public void Detach(Node node)
        {
            var edge = Edges().First(e => e.Contains(node));
            _edges.Remove(edge);
            edge.SecondNode(this)._edges.Remove(edge);
        }

        public void Remove()
        {
            foreach (var node in IncidentNodes())
                Detach(node);
        }

        public override string ToString() => $"{Label}: {Power}";
    }
}