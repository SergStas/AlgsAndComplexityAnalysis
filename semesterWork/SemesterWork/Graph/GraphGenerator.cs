using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class GraphGenerator
    {
        private const int DefaultWeightMinValue = 50;
        private const int DefaultWeightMaxValue = 1500;
        private const int DefaultNodesMinCount = 4;
        private const int DefaultNodesMaxCount = 9;
        private const double OrderMinMass = 0.3;
        private const double OrderMaxMass = 3;
        private const string DefaultLabelFormat = "Node#{0}";
        
        private readonly Random _rnd;

        public CustomGraph GenerateGraph(bool taskPattern)
        {
            var result = new CustomGraph();
            var count = _rnd.Next(DefaultNodesMinCount, DefaultNodesMaxCount + 1);
            AddNodes(result, count);
            var powers = new int[count].Select(_ => _rnd.Next(1, count - 1)).ToArray();
            AddEdges(result, powers, taskPattern);
            if (taskPattern) result.GetNodeById(0).Rename("Storage");
            return result;
        }

        private void AddEdges(CustomGraph result, int[] powers, bool taskPattern)
        {
            if (taskPattern)
                for (var i = 1; i < powers.Length; i++)
                    result.Connect(result.GetNodeById(0).Label,
                        result.GetNodeById(i).Label,
                        _rnd.Next(DefaultWeightMinValue, DefaultWeightMaxValue - 1));
            for (var i = 0; i < result.NodesCount; i++)
            for (var j = 0; j < powers[i]; j++)
                result.TryConnect(result.GetNodeById(i).Label,
                    result.GetNodeById(_rnd.Next(0, powers.Length)).Label,
                    _rnd.Next(DefaultWeightMinValue, DefaultWeightMaxValue - 1));
        }

        private void AddNodes(CustomGraph result, int count)
        {
            for (var i = 0; i < count; i++)
                result.Add(string.Format(DefaultLabelFormat, i));
        }

        public GraphGenerator()
        {
            _rnd = new Random();
        }

        public List<(string, double)> GenerateOrdersSequence(CustomGraph graph) => 
            graph.Nodes().Skip(1).Select(node => (node.Label, Math.Round(_rnd.NextDouble() * (OrderMaxMass - OrderMinMass) + OrderMinMass, 2))).ToList();
    }
}