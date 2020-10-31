using System;

namespace ACALab3
{
    public class BinarySearchTree<TTree> where TTree : IComparable
    {
        private TreeNode<TTree> _root;

        public void Add(TTree value) => Add(new TreeNode<TTree>(value), _root);
        
        private void Add(TreeNode<TTree> newNode, TreeNode<TTree> rootNode)
        {
            var node = rootNode;
            while (node?.DirectionOf(newNode.Value) != null)
                node = node.DirectionOf(newNode.Value);
            if (node == null)
                _root = newNode;
            else
                node.Attach(newNode, newNode.Value.CompareTo(node.Value) <= 0);
        }

        public bool Contains(TTree value)
        {
            var node = _root;
            while (node != null)
                if (node.Value.CompareTo(value) == 0)
                    return true;
                else
                    node = node.DirectionOf(value);
            return false;
        }

        public void Remove(TTree value)
        {
            if (!Contains(value))
                return;
            var node = _root;
            if (node.Value.CompareTo(value) == 0)
                DetachNode(null, node);
            else 
                while (node.DirectionOf(value).Value.CompareTo(value) != 0)
                    node = node.DirectionOf(value);
            DetachNode(node, node.DirectionOf(value));
        }

        private void DetachNode(TreeNode<TTree> parent, TreeNode<TTree> node)
        {
            if (parent == null)
                DetachRoot();
            if (node == null || parent == null)
                return;
            var isLeft = parent.Left == node;
            parent.Detach(isLeft);
            if (node.ChildrenCount == 1)
                parent.Attach(node.Left ?? node.Right, isLeft);
            if (node.ChildrenCount < 2)
                return;
            parent.Attach(node.Right, isLeft);
            Add(node.Left, node.Right);
        }

        private void DetachRoot()
        {
            var root = _root;
            _root = null;
            if (root.ChildrenCount == 1)
                _root = root.Left ?? root.Right;
            if (root.ChildrenCount < 2)
                return;
            _root = root.Right;
            Add(root.Left, root.Right);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        private class TreeNode<TNode> where TNode: IComparable
        {
            public TreeNode<TNode> Left { get; private set; }
            public TreeNode<TNode> Right { get; private set; }
            public TNode Value { get; }

            public int ChildrenCount => Left == null ? Right == null ? 0 : 1 : Right == null ? 1 : 2;

            public TreeNode(TNode value) => Value = value;

            public void Attach(TreeNode<TNode> node, bool left)
            {
                if (left)
                    Left = node;
                else
                    Right = node;
            }

            public void Detach(bool left)
            {
                if (left)
                    Left = null;
                else
                    Right = null;
            }

            public TreeNode<TNode> DirectionOf(TNode value) => value.CompareTo(Value) <= 0 ? Left : Right;
        }
    }
}