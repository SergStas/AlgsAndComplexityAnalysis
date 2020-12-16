using System;
using System.Collections.Generic;

namespace ACALab6
{
    public class UserData
    {
        public string Id { get; private set; }
        public string RegDate { get; private set; }
        public List<Genre> Genres { get; private set; }

        public static UserData RandomInstance() =>
            new UserData
            {
                Id = Generator.GenRndCharSeq(new Random().Next(3, 11)),
                RegDate = Generator.GenRndDate(),
                Genres = Generator.FillGenres()
            };

        public int GetExtraHash()
        {
            var seed = 967;
            unchecked
            {
                var hash = RegDate.GetHashCode() ^ seed * Id.GetHashCode();
                foreach (var genre in Genres)
                    hash = hash * seed + genre.GetHashCode();
                return hash;
            }
        }
    }

    public enum Genre {VN, RPG, Strategy, Fighting, Simulator, Shooter}
}