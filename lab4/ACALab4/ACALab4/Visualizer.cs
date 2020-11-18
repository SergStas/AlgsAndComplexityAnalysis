using System;
using System.Collections.Generic;
using static System.Console;
using static System.ConsoleColor;

namespace ACALab4
{
    public partial class RedBlackTree
    {
        private static readonly char[] Borders = {'┗', '┣', '┃', '━'};
        private static int StartLine;

        private static readonly Dictionary<int, Tuple<ConsoleColor, ConsoleColor>> Coloring =
            new Dictionary<int, Tuple<ConsoleColor, ConsoleColor>>
            {
                {-1, (Black, White).ToTuple()},
                {0, (DarkGray, White).ToTuple()},
                {1, (White, Red).ToTuple()},
                {2, (White, Black).ToTuple()}
            };
        
        public void Draw(bool drawNils)
        {
            ClearConsole();
            if (_root != null)
            {
                var stack = new Stack<Tuple<Node, int, int, bool>>();
                stack.Push((_root, 0, StartLine, false).ToTuple());
                var depth = StartLine;
                while (stack.Count != 0)
                    DrawNode(stack, ref depth, drawNils);
            }
            ForegroundColor = Coloring[-1].Item1;
            BackgroundColor = Coloring[-1].Item2;
        }

        public static void SetDrawingStartingLine(int n) => StartLine = n;

        private static void ClearConsole()
        {
            BackgroundColor = Coloring[0].Item2;
            Clear();
            SetCursorPosition(0, StartLine);
        }

        private static void DrawNode(Stack<Tuple<Node, int, int, bool>> stack, ref int depth, bool drawNils)
        {
            var (node, x, y, last) = stack.Pop();
            if (node is null)
                return;
            while (y < depth)
                WriteTo(Borders[2], x - 1, y++, 0);
            if (x != 0)
                WriteTo(Borders[last ? 0 : 1], x - 1, y, 0);
            var content =  node.IsNil ? "NIL" : node.Key.ToString();
            WriteTo(x > 0 ? Borders[3].ToString() : "", x, depth, 0);
            WriteTo(content, x > 0 ? x + 1 : x, depth,  node.IsBlack ? 2 : 1);
            depth++;
            if (node.ChildrenCount == 0 && !drawNils) return;
            stack.Push((node.Left, x + (x > 0 ? 2 : 1), y + 2, true).ToTuple());
            stack.Push((node.Right, x + (x > 0 ? 2 : 1), y + 1, false).ToTuple());
        }

        private static void WriteTo(object value, int x, int y, int colorCode)
        {
            ForegroundColor = Coloring[colorCode].Item1;
            BackgroundColor = Coloring[colorCode].Item2;
            SetCursorPosition(x, y);
            Write(value);
        }
    }
}