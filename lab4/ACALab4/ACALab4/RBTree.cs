using System.Runtime.InteropServices.WindowsRuntime;

namespace ACALab4
{
    public class RBTree
    {
        private Node _root;

        public void Add(int key)
        {
            
        }

        public Node Find(int key)
        {
            var node = _root;
            while (node != null && !node.IsNil)
                if (node.Key == key)
                    return node;
                else
                    node = key <= node.Key ? node.Left : node.Right;
            return null;
        }

        public int? FindMin() => FindBorderKey(true);
        public int? FindMax() => FindBorderKey(false); 

        private int? FindBorderKey(bool min)
        {
            var node = _root;
            while (node != null && (min ? !node.Left.IsNil : !node.Right.IsNil))
                node = min ? node.Left : node.Right;
            return node?.Key;
        }

        public int? FindPrev(int key) => FindNeighbour(key, false);
        public int? FindNext(int key) => FindNeighbour(key, true);

        private int? FindNeighbour(int key, bool next)
        {
            var node = Find(key);
            if (node is null)
                return null;
            var lastIteration = false;
            if (next ? !node.Right.IsNil : !node.Left.IsNil)
                return next ? node.Right.Key : node.Left.Key;
            while (node.Parent != null && !lastIteration)
            {
                lastIteration = next == node.IsLeft;
                node = node.Parent;
            }
            return node?.Key;
        }


















        public class Node
        {
            public int Key { get; set; }
            public bool IsBlack { get; set; }
            public bool IsNil { get; set; }
            public Node Parent { get; set; }
            public bool IsLeft { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            

            public Node Uncle => Parent is null ? null : Parent.IsLeft ? Parent.Parent?.Right : Parent.Parent?.Left;
            
            public Node(int key, Node parent, bool isLeft)
            {
                Key = key;
                Parent = parent;
                Left = new Node(default, this, true) {IsNil = true, IsBlack = true};
                Right = new Node(default, this, false) {IsNil = true, IsBlack = true};
                IsLeft = isLeft;
            }
        }
    }
}