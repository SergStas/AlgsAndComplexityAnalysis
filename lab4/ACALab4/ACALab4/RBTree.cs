namespace ACALab4
{
    public partial class RBTree
    {
        private Node _root;
        
        public bool Add(int key)
        {
            if (Find(key) != null)
                return false;
            var node = _root;
            while (_root != null && !node.DirectionOf(key).IsNil)
                node = node.DirectionOf(key);
            var result = new Node(key, node) {IsBlack = _root is null, IsRoot = _root is null};
            if (_root == null) _root = result;
            else node.InsertChild(result);
            result.BalanceAfterAdding();
            if (!_root.IsRoot) _root = result.Root;
            return true;
        }

        public bool Remove(int key) => Remove(Find(key));

        private bool Remove(Node node)
        {
            if (node is null) return false;
            if (node.ChildrenCount == 2)
            {
                var prev = Find((int)FindPrev(node.Key));
                node.SwapKeys(prev);
                Remove(prev);
            }
            else if (node.IsBlack) RemoveBlack(node);
            else node.Parent.Detach(node.Key);
            return true;
        }

        private static void RemoveBlack(Node node)
        {
            if (node.ChildrenCount == 1)
            {
                var child = node.Left.IsNil ? node.Right : node.Left;
                node.SwapKeys(child);
                node.Detach(child.Key);
            }
            else
            {
                node.BalanceAfterRemoving();
                node.Parent.Detach(node.Key);
            }
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

        private static int? FindBorderKey(bool min, Node root)
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
            public int Key { get; private set; }
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

            public void SwapKeys(Node node)
            {
                var t = Key;
                Key = node.Key;
                node.Key = t;
            }

            public void SwapColors(Node node)
            {
                var t = node.IsBlack;
                node.IsBlack = IsBlack;
                IsBlack = t;
            }

            public Node DirectionOf(int key) => key <= Key ? Left : Right;

            public void InsertChild(Node node) => InsertChild(node, node.Key <= Key);

            private void InsertChild(Node node, bool left)
            {
                if (left)
                    Left = node;
                else
                    Right = node;
                node.Parent = this;
                node.IsLeft = left;
            }

            public void BalanceAfterAdding()
            {
                if (Parent?.Parent is null || IsBlack)
                    return;
                if (!Uncle.IsBlack)
                {
                    Parent.IsBlack = Uncle.IsBlack = true;
                    Parent.Parent.IsBlack = Parent.Parent.IsRoot;
                    Parent.Parent.BalanceAfterAdding();
                    return;
                }
                if (IsLeft != Parent.IsLeft)
                {
                    SmallRotate(IsLeft, this, Parent);
                    LargeRotate(IsLeft, this, Parent);
                }
                else
                    LargeRotate(IsLeft, Parent, Parent.Parent);
                Parent?.SwapColors(IsLeft ? Parent.Right : Parent.Left); // FIXME
            }

            public void BalanceAfterRemoving()
            {
                Node p = Parent, c = IsLeft ? p.Right : p.Left, ln = c.Left, rn = c.Right;
                if (!p.IsBlack && c.IsBlack && ln.IsBlack && rn.IsBlack)
                    p.SwapColors(c);
                else if (!p.IsBlack && c.IsBlack && (IsLeft ? rn.IsBlack : ln.IsBlack))
                {
                    SmallRotate(!IsLeft, c, p);
                    p.SwapColors(c);
                    if (!IsLeft)
                        c.Left.IsBlack = true;
                    else
                        c.Right.IsBlack = true;
                }
                else if (p.IsBlack && !c.IsBlack && (IsLeft ? ln.Left.IsBlack && ln.Right.IsBlack : rn.Left.IsBlack && rn.Right.IsBlack))
                {
                    SmallRotate(!IsLeft, c, p);
                    c.SwapColors(IsLeft ? c.Left : c.Right);
                }
                else if (p.IsBlack && !c.IsBlack && (IsLeft ? !ln.Right.IsBlack : !rn.Left.IsBlack))
                {
                    SmallRotate(IsLeft, IsLeft ? ln : rn, c);
                    SmallRotate(!IsLeft, IsLeft ? ln : rn, p);
                    if (!IsLeft)
                        rn.Left.IsBlack = true;
                    else
                        ln.Right.IsBlack = true;
                }
                else if (p.IsBlack && c.IsBlack && !c.IsNil && (IsLeft ? !ln.IsBlack : !rn.IsBlack))
                {
                    SmallRotate(IsLeft, IsLeft ? ln : rn, c);
                    SmallRotate(!IsLeft, IsLeft ? ln : rn, p);
                    if (!IsLeft)
                        rn.IsBlack = true;
                    else
                        ln.IsBlack = true;
                }
                else
                {
                    c.IsBlack = true;
                    p.BalanceAfterRemoving();
                }
            }

            private void Detach(Node node)
            {
                if (!node.IsNil)
                    Detach(node.Key);
            }
            
            public void Detach(int key)
            {
                DirectionOf(key).Parent = null;
                if (key <= Key)
                    Left = new Node(this);
                else
                    Right= new Node(this);
            }

            private static void LargeRotate(bool right, Node a, Node b)
            {
                var p = b.Parent;
                var c = right ? a.Right : a.Left;
                a.Detach(c);
                b.InsertChild(c, right);
                a.InsertChild(b);
                p?.InsertChild(a);
                a.Parent = p;
                a.IsRoot = b.IsRoot;
                b.IsRoot = false;
            }

            private static void SmallRotate(bool right, Node a, Node b)
            {
                var p = b.Parent;
                var c = right ? a.Right : a.Left;
                a.Detach(c);
                b.InsertChild(c, right);
                a.InsertChild(b);
                p?.InsertChild(a);
                a.Parent = p;
                a.IsRoot = b.IsRoot;
                b.IsRoot = false;
            }

            public override string ToString() =>
                IsNil ? "NIL" : $"{(IsBlack ? 'B' : 'R')}{Key}: {(Left.IsNil ? "NIL" : (Left.IsBlack ? "B" : "R") + Left.Key)}" + 
                $", {(Right.IsNil ? "NIL" : (Right.IsBlack ? "B" : "R") + Right.Key.ToString())}";
        }
    }
}