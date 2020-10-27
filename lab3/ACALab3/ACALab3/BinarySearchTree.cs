using System;

namespace ACALab3
{
    public class BinarySearchTree<TTree> where TTree : IComparable
    {
        private TreeNode<TTree> _root;

        public void Add(TTree value)
        {
            var node = _root;
            while (node?.DirectionOf(value) != null)
                node = node.DirectionOf(value);
            if (node == null)
                _root = new TreeNode<TTree>(value);
            else if (value.CompareTo(node.Value) <= 0)
                node.AttachLeft(new TreeNode<TTree>(value));
            else
                node.AttachRight(new TreeNode<TTree>(value));
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
            
            while (node.DirectionOf(value).Value.CompareTo(value) != 0)
                node = node.DirectionOf(value);
            if (node.Left == null && node.Right == null)
                //node.
        }

        private void DetachNode(TreeNode<TTree> parent, TreeNode<TTree> node)
        {
            var isLeft = parent?.Value.CompareTo(node.Value) <= 0;
            var left = node.Left;
            var right = node.Right;
            if (isLeft)
                parent.DetachLeft();
            else
                parent?.DetachRight();
            if (left == null && right == null && parent == null)
                _root = null;
            else if (left == null ^ right == null)
                if (parent == null)
                    _root = left == null ? right : null;
                else if (left == null)
                    
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private class TreeNode<TNode> where TNode: IComparable
        {
            public TreeNode<TNode> Left { get; private set; }
            public TreeNode<TNode> Right { get; private set; }
            public TNode Value { get; }

            public TreeNode(TNode value) => Value = value;

            public void AttachLeft(TreeNode<TNode> value) => Left = value;
            public void AttachRight(TreeNode<TNode> value) => Right = value;
            public void DetachLeft() => Left = null;
            public void DetachRight() => Right = null;
            public TreeNode<TNode> DirectionOf(TNode value) => value.CompareTo(Value) <= 0 ? Left : Right;
        }
    }
}