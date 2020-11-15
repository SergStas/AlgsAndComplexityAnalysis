using System.Diagnostics.CodeAnalysis;

namespace ACALab4
{
    public partial class RBTree
    {
        private Node _root;
        
        public void Add(int key)
        {
            var node = _root;
            while (node != null && !node.DirectionOf(key).IsNil)
                node = node.DirectionOf(key);
            var result = new Node(key, node) {IsBlack = _root is null, IsRoot = _root is null};
            if (node == null)
                _root = result;
            else
                node.InsertChild(result);
            result.Balance();
            if (!_root.IsRoot)
                _root = result.Root;
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

        public int? FindMin() => FindBorderKey(true, _root);
        public int? FindMax() => FindBorderKey(false, _root); 

        private int? FindBorderKey(bool min, Node root)
        {
            var node = root;
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
                return next ? FindBorderKey(true, node.Right) : FindBorderKey(false, node.Left);
            while (node.Parent != null && !lastIteration)
            {
                lastIteration = next == node.IsLeft;
                node = node.Parent;
            }
            return lastIteration ? node?.Key : null;
        }

        public class Node
        {
            public int Key { get; }
            public bool IsBlack { get; set; }
            public bool IsNil { get; }
            public Node Parent { get; set; }
            public bool IsLeft { get; set; }
            public bool IsRoot { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node Root => IsRoot ? this : Parent.Root;
            public int ChildrenCount => IsNil ? 0 : Left.IsNil ? Right.IsNil ? 0 : 1 : Right.IsNil ? 1 : 2;
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

            public void InsertChild(Node node) => InsertChild(node, node.Key <= Key);

            private void InsertChild(Node node, bool left)
            {
                if (left)
                    Left = node;
                else
                    Right = node;
                node.IsLeft = left;
            }

            public void Balance()
            {
                if (Parent?.Parent is null || IsBlack)
                    return;
                if (!Uncle.IsBlack)
                {
                    Parent.IsBlack = Uncle.IsBlack = true;
                    Parent.Parent.IsBlack = Parent.Parent.IsRoot;
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
                g.InsertChild(right ? p.Right : p.Left, right);
                p.InsertChild(g);
                g.Parent?.InsertChild(p);
                p.Parent = g.Parent;
                g.Parent = p;
                g.IsBlack = false;
                p.IsBlack = true;
                p.IsRoot = g.IsRoot;
                g.IsRoot = false;
            }

            public override string ToString() =>
                IsNil ? "NIL" : $"{(IsBlack ? 'B' : 'R')}{Key}: {(Left.IsNil ? "NIL" : (Left.IsBlack ? "B" : "R") + Left.Key)}" + 
                $", {(Right.IsNil ? "NIL" : (Right.IsBlack ? "B" : "R") + Right.Key.ToString())}";
        }
    }
}