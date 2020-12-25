using System;
using System.Collections.Generic;
using static System.Console;

namespace ACALab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 1000;
            var table = new HashTable(size);
            var count = new Random().Next(0, size);
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
            var cnt = 0;
            for (var i = 0; i < count; i++)
            {
                var e = table.Find(users[i].Id);
                cnt += e.Id == users[i].Id && users[i].RegDate == e.RegDate ? 1 : 0;
                BackgroundColor = e.Id == users[i].Id && users[i].RegDate == e.RegDate ? 
                    ConsoleColor.Green : ConsoleColor.Red; 
                WriteLine($"{e.Id} - {e.RegDate} \t\t\t\t| {users[i].Id} {users[i].RegDate}");
            }

            BackgroundColor = ConsoleColor.Black;
            WriteLine($"{cnt}/{count} ({cnt*100.0/count}%)");
            ReadLine();
        }
    }
}