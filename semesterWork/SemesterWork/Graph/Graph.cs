using System.Collections.Generic;

namespace Graph
{
    public class Graph
    {
        public int NodesCount => _nodes.Count;
        
        private readonly List<Node> _nodes = new List<Node>();

        public IEnumerable<Node> Nodes()
        {
            foreach (var node in _nodes)
                yield return node;
        }
    }
}