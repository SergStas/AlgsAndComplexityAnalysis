using System;
using System.Linq;

namespace Graph
{
    public class GraphGenerator
    {
        private const int DefaultWeightMinValue = 50;
        private const int DefaultWeightMaxValue = 1500;
        private const int DefaultNodesMinCount = 4;
        private const int DefaultNodesMaxCount = 20;
        private const string DefaultLabelFormat = "Node#{0}";
        
        private readonly Random _rnd;

        public CustomGraph GenerateGraph(bool taskPattern)
        {
            var result = new CustomGraph();
            var count = _rnd.Next(DefaultNodesMinCount, DefaultNodesMaxCount + 1);
            AddNodes(result, count);
            var powers = new int[count].Select(_ => _rnd.Next(1, count - 1)).ToArray();
            if (taskPattern) powers[0] = count - 1;
            AddEdges(result, powers);
            return result;
        }

        private void AddEdges(CustomGraph result, int[] powers)
        {
            for (var i=0;i<result.NodesCount;i++)
                for (var j=0;j<powers[i];j++)
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
    }
}