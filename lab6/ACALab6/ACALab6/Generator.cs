using System;
using System.Collections.Generic;
using System.Text;

namespace ACALab6
{
    public static class Generator
    {
        public static string GenRndCharSeq(int length)
        {
            var rnd = new Random();
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
                sb.Append((char)(rnd.Next(0, 26) + 'a'));
            return sb.ToString();
        }

        public static string GenRndDate() =>
            $"{new Random().Next(0,2021)}.{new Random().Next(0,13)}.{new Random().Next(0,32)}";

        public static List<Genre> FillGenres()
        {
            var genres = new List<Genre>();
            var rnd = new Random();
            for (var i = 0; i < 5; i++)
                if (rnd.Next(0, 2) == 0)
                    genres.Add((Genre) i);
            return genres;
        }
    }
}