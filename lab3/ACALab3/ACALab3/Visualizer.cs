using System;
using System.Collections.Generic;
using static System.Console;

namespace ACALab3
{
    public partial class BinarySearchTree<T>
    {
        private static readonly char[] borders = {'┗', '┣', '┃', '━'};
        public void Draw()
        {
            WriteLine();
            var start = CursorTop;
            if (_root == null)
                return;
            var stack = new Stack<Tuple<TreeNode<T>, int, int, bool>>();
            stack.Push((_root, 0, start, false).ToTuple());
            var depth = start;
            while (stack.Count != 0)
                DrawNode(stack, ref depth);
            WriteLine();
        }

        private static void DrawNode(Stack<Tuple<TreeNode<T>, int, int, bool>> stack, ref int depth)
        {
            var (node, x, y, last) = stack.Pop();
            while (y < depth)
                WriteTo(borders[2], x - 1, y++);
            if (x != 0)
                WriteTo(borders[last ? 0 : 1], x - 1, y);
            var content = node?.Value.ToString() ?? "";
            WriteTo((x > 0 ? borders[3].ToString() : "") + content, x, depth);
            depth++;
            if (node == null || node.ChildrenCount == 0)
                return;
            stack.Push((node.Left, x + (x > 0 ? 2 : 1), y + 2, true).ToTuple());
            stack.Push((node.Right, x + (x > 0 ? 2 : 1), y + 1, false).ToTuple());
        }

        private static void WriteTo(object value, int x, int y)
        {
            SetCursorPosition(x, y);
            Write(value);
        }
    }
}