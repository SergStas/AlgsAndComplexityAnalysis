using System;
using System.Collections.Generic;
using static System.Console;

namespace ACALab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 1000000;
            var table = new HashTable(size);
            var count = new Random().Next(0, size + 1);
            WriteLine("Generating...");
            var users = new List<UserData>();
            for (var i = 0; i < count; i++)
                users.Add(UserData.RandomInstance());
            WriteLine("Pushing...");
            var keys = new List<int>();
            for (var i = 0; i < count; i++)
                keys.Add(table.Add(users[i]));
            WriteLine($"Done, press enter to print out all the {count} objects");
            ReadLine();
            for (var i = 0; i < count; i++)
                WriteLine(table.Get(keys[i]).Id);
            ReadLine();
        }
    }
}