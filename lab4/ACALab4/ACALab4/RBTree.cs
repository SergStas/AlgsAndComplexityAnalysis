using System.Diagnostics.CodeAnalysis;

namespace ACALab4
{
    public class RBTree
    {
        private Node _root;
        
        public void Add(int key)
        {
            var node = _root;
            while (node != null && !node.DirectionOf(key).IsNil)
                node = node.DirectionOf(key);
            var result = new Node(key, node) {IsBlack = _root is null};
            if (node == null)
                _root = result;
            else
                node.InsertChild(result);
            result.Balance();
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

            private Node Uncle => Parent is null ? null : Parent.IsLeft ? Parent.Parent?.Right : Parent.Parent?.Left;
            
            public Node(int key, Node parent)
            {
                Key = key;
                Parent = parent;
                Left = new Node(this) {IsLeft = true};
                Right = new Node(this);
                IsLeft = key <= Parent?.Key;
            }

            private Node(Node parent)
            {
                IsNil = IsBlack = true;
                Parent = parent;
            }

            public Node DirectionOf(int key) => key <= Key ? Left : Right;

            public void InsertChild(Node node)
            {
                if (node.Key <= Key)
                    Left = node;
                else
                    Right = node;
                node.IsLeft = node.Key <= Key;
            }

            public void Balance()
            {
                if (Parent?.Parent is null || IsBlack)
                    return;
                if (!Uncle.IsBlack)
                {
                    Parent.IsBlack = Uncle.IsBlack = true;
                    Parent.Parent.IsBlack = false;
                    Parent.Parent.Balance();
                    return;
                }
                if (IsLeft != Parent.IsLeft)
                {
                    var g = Parent.Parent;
                    g.InsertChild(this);
                    InsertChild(Parent);
                    Parent.Parent = this;
                    Parent = g;
                }
                Rotate(IsLeft);
            }

            private void Rotate(bool right)
            {
                var g = Parent.Parent;
                var p = Parent;
                g.InsertChild(right ? p.Right : p.Left);
                p.InsertChild(g);
                g.IsBlack = false;
                p.IsBlack = true;
            }
        }
    }
}