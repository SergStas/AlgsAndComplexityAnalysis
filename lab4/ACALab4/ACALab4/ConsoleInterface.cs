﻿using System;
using System.Collections.Generic;
using static System.Console;

namespace ACALab4
{
    public class ConsoleInterface
    {
        private readonly RBTree _tree = new RBTree();
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
            "Command '{0}' returned no result"
        };
        private CommandState _state = CommandState.Init;
        private const int BordersLength = 100;

        private static readonly Dictionary<int, Func<RBTree, int, Tuple<bool, bool, int>>> Commands = new Dictionary<int, Func<RBTree, int, Tuple<bool, bool, int>>> 
        {
            {1, (tree, key) => {tree.Add(key); return (true, false, 0).ToTuple();}},
            {2, (tree, key) => (tree.Remove(key), false, 0).ToTuple()},
            {3, (tree, key) => (true, true, tree.Find(key).Key).ToTuple()},
            {4, (tree, _) => (tree.FindMin() != null, true, tree.FindMin() ?? 0).ToTuple()},
            {5, (tree, _) => (tree.FindMax() != null, true, tree.FindMax() ?? 0).ToTuple()},
            {6, (tree, key) => (tree.FindNext(key) != null, true, tree.FindNext(key) ?? 0).ToTuple()},
            {7, (tree, key) => (tree.FindPrev(key) != null, true, tree.FindPrev(key) ?? 0).ToTuple()},
        };

        public void Run()
        {
            PresetTree();
            RBTree.SetDrawingStartingLine(4);
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
            if (command == "-help" || command == "-h")
                _state = CommandState.Help;
            if (command == "-quit" || command == "-q" || command == "-help" || command == "-h")
                return;
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
                else if (code == 2 && !success)
                {
                    _state = CommandState.KeyNotFound;
                    argument = parts[1];
                }
                else if (success)
                {
                    _state = CommandState.Result;
                    argument = result.ToString();
                }
                else
                    _state = CommandState.NoResult;
            }
        }

        private void Redraw(string command)
        {
            _tree.Draw();
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
            for (var i = 0; i < 20; i++)
                _tree.Add(i);
        }
        
        private enum CommandState
        {
            Init, Ok, InvalidCommand, KeyNotFound, Help, ArgumentRequired, UnresolvedArgument, Result, NoResult
        }
    }
}