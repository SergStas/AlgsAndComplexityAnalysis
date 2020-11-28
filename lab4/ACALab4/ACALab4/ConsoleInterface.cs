using System;
using System.Collections.Generic;
using static System.Console;

namespace ACALab4
{
    public class ConsoleInterface
    {
        private RedBlackTree _tree = new RedBlackTree();
        private static readonly string[] Messages =
        {
            "RBTree initialized successfully. Input -help to get commands info",
            "Operation '{0}' done successfully",
            "Invalid command '{0}', Input -help to get commands info",
            "Key '{0}' not found",
            "Use in format 'code [arg]'. Codes: 1-Add, 2-Remove, 3-Find, 4-Min, 5-Max, 6-FindNext, 7-FindPrev",
            "Command '{0}' requires argument. Use in form 'code arg'",
            "Unable to resolve arg in command '{0}'",
            "Command done successfully with result '{0}'",
            "Command '{0}' returned no result",
            "Key '{0}' is already added",
            "Cleared successfully",
            "NIL-drawing mode switched to '{0}'",
            "Tree reset"
        };
        private CommandState _state = CommandState.Init;
        private const int BordersLength = 100;
        private bool _drawNils;

        private static readonly Dictionary<int, Func<RedBlackTree, int, Tuple<bool, bool, int>>> Commands = new Dictionary<int, Func<RedBlackTree, int, Tuple<bool, bool, int>>> 
        {
            {1, (tree, key) => (tree.Add(key), false, 0).ToTuple()},
            {2, (tree, key) => (tree.Remove(key), false, 0).ToTuple()},
            {3, (tree, key) => (true, true, tree.Find(key).Key).ToTuple()},
            {4, (tree, _) => (tree.FindMin() != null, true, tree.FindMin() ?? 0).ToTuple()},
            {5, (tree, _) => (tree.FindMax() != null, true, tree.FindMax() ?? 0).ToTuple()},
            {6, (tree, key) => (tree.FindNext(key) != null, true, tree.FindNext(key) ?? 0).ToTuple()},
            {7, (tree, key) => (tree.FindPrev(key) != null, true, tree.FindPrev(key) ?? 0).ToTuple()}
        };

        public void Run()
        {
            PresetTree();
            RedBlackTree.SetDrawingStartingLine(4);
            var command = "";
            while (command != "-quit" && command != "-q")
            {
                Redraw(command);
                command = ReadLine();
                ProcessCommand(command, out command);
            }
        }

        private void ProcessCommand(string command, out string argument)
        {
            argument = command;
            switch (command)
            {
                case "-r": case "-reset":
                    _state = CommandState.Reset;
                    PresetTree();
                    return;
                case "-help": case "-h":
                    _state = CommandState.Help;
                    return;
                case "-quit": case "-q": return;
                case "-c": case "-clear":
                    _tree = new RedBlackTree();
                    _state = CommandState.Cleared;
                    return;
                case "-s": case "-switch_nils":
                    _drawNils = !_drawNils;
                    _state = CommandState.Switched;
                    argument = _drawNils.ToString();
                    return;
            }
            var parts = command.Split(" ");
            if (command == "" || !int.TryParse(parts[0], out var code) || code < 1 || code > 7 || parts.Length > 2)
                _state = CommandState.InvalidCommand;
            else if (code != 4 && code != 5 && parts.Length < 2)
                _state = CommandState.ArgumentRequired;
            else if (parts.Length == 2 && !int.TryParse(parts[1], out var arg))
            {
                _state = CommandState.UnresolvedArgument;
                argument = parts[1];
            }
            else
            {
                arg = parts.Length == 2 ? int.Parse(parts[1]) : 0;
                var (success, hasResult, result) = Commands[code](_tree, arg);
                if (success && !hasResult)
                    _state = CommandState.Ok;
                else switch (code)
                {
                    case 1 when !success:
                        _state = CommandState.Duplicate;
                        argument = parts[1];
                        break;
                    case 2 when !success:
                        _state = CommandState.KeyNotFound;
                        argument = parts[1];
                        break;
                    default:
                        if (success)
                        {
                            _state = CommandState.Result;
                            argument = result.ToString();
                        }
                        else
                            _state = CommandState.NoResult;
                        break;
                }
            }
        }

        private void Redraw(string command)
        {
            _tree.Draw(_drawNils);
            SetCursorPosition(0, 1);
            Write(new string('=', BordersLength));
            SetCursorPosition(0, 3);
            Write(new string('=', BordersLength));
            SetCursorPosition(0, 0);
            Write(Messages[(int)_state], command);
            SetCursorPosition(0, 2);
        }

        private void PresetTree()
        {
            /*_tree.Add(3);
            _tree.Add(2);
            _tree.Add(1);*/
            //_tree.Add(0);
            _tree = new RedBlackTree();
            for (var i = 0; i < 20; i++)
                _tree.Add(i);//new Random().Next(1, 101));
        }
        
        private enum CommandState
        {
            Init, Ok, InvalidCommand, KeyNotFound, Help, ArgumentRequired, UnresolvedArgument, 
            Result, NoResult, Duplicate, Cleared, Switched, Reset
        }
    }
}