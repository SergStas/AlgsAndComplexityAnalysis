using System;

namespace Graph
{
    public class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public double Weight { get; }
        

        public Edge(Node first, Node second, double weight = 0)
        {
            From = first;
            To = second;
            Weight = weight;
        }

        public Node SecondNode(Node node) => node.Equals(From) ? From : To;

        public bool Contains(Node node) => From.Equals(node) || To.Equals(node);
        
        public override string ToString() => $"{From.Label}-{To.Label} ({Weight})";
    }
}