using System;
using System.Collections;
using System.Collections.Generic;

namespace ACALab3
{
    public partial class BinarySearchTree<T> : IEnumerable<T> where T : IComparable
    {
        private TreeNode<T> _root;

        public void Add(T value) => Add(new TreeNode<T>(value), _root);
        
        private void Add(TreeNode<T> newNode, TreeNode<T> rootNode)
        {
            var node = rootNode;
            while (node?.DirectionOf(newNode.Value) != null)
                node = node.DirectionOf(newNode.Value);
            if (node == null)
                _root = newNode;
            else
                node.Attach(newNode, newNode.Value.CompareTo(node.Value) <= 0);
        }

        public bool Contains(T value)
        {
            var node = _root;
            while (node != null)
                if (node.Value.CompareTo(value) == 0)
                    return true;
                else
                    node = node.DirectionOf(value);
            return false;
        }

        public void Remove(T value)
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

        private void DetachNode(TreeNode<T> parent, TreeNode<T> node)
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

        public int CalculateDepth()
        {
            if (_root == null)
                return 0;
            var stack = new Stack<Tuple<TreeNode<T>, int>>();
            var max = 0;
            FillStack(stack, (_root, 1).ToTuple(),
                s => (s.Peek().Item1.Left, s.Peek().Item2 + 1).ToTuple(),
                s => s.Peek().Item1.Left != null);
            while (stack.Count != 0)
                MeasureDepthOfNodeFromStack(stack, ref max);
            return max;
        }

        private static void MeasureDepthOfNodeFromStack(Stack<Tuple<TreeNode<T>, int>> stack, ref int max)
        {
            var (node, depth) = stack.Pop();
            if (depth > max)
                max = depth;
            if (node.Right != null)
                FillStack(stack, (node.Right, depth + 1).ToTuple(),
                    s => (s.Peek().Item1.Left, s.Peek().Item2 + 1).ToTuple(),
                    s => s.Peek().Item1.Left != null);
        }

        private IEnumerable<T> Enumerate()
        {
            var stack = new Stack<TreeNode<T>>();
            FillStack(stack, _root,
                s => s.Peek().Left,
                s => s.Peek().Left != null);
            while (stack.Count != 0)
                foreach (var e in EnumerateNodeFromStack(stack))
                    yield return e;
        }

        private static IEnumerable<T> EnumerateNodeFromStack(Stack<TreeNode<T>> stack)
        {
            var node = stack.Pop();
            yield return node.Value;
            if (node.Right != null)
                FillStack(stack, node.Right,
                    s => s.Peek().Left,
                    s => s.Peek().Left != null);
        } 

        private static void FillStack<TContent>(Stack<TContent> stack, TContent startItem, 
            Func<Stack<TContent>, TContent> newItem, 
            Func<Stack<TContent>, bool> predicate)
        {
            stack.Push(startItem);
            while (predicate(stack))
                stack.Push(newItem(stack));
        }

        public IEnumerator<T> GetEnumerator() => Enumerate().GetEnumerator();

        public override string ToString() =>
            $"BinarySearchTree<{typeof(T)}>{(_root == null ? " (empty)" : $": [{_root}]")}";

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

            public override string ToString() =>
                ChildrenCount switch
                {
                    0 => Value.ToString(),
                    1 => (Left == null ? $"{Value}[{Right}]" : $"[{Left}]{Value}"),
                    _ => $"[{Left}]{Value}[{Right}]"
                };
        }
    }
}