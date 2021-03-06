﻿using System;
using System.Collections.Generic;

namespace ACALab6
{
    public class UserData
    {
        public string Id { get; set; }
        public string RegDate { get; private set; }
        public List<Genre> Genres { get; private set; }

        public static UserData RandomInstance() =>
            new UserData
            {
                Id = Generator.GenRndCharSeq(new Random().Next(3, 11)),
                RegDate = Generator.GenRndDate(),
                Genres = Generator.FillGenres()
            };
    }

    public enum Genre {VN, RPG, Strategy, Fighting, Simulator, Shooter}
}