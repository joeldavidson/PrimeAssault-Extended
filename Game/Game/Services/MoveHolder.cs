using System;
using System.Collections.Generic;
using PrimeAssault.Models;

namespace PrimeAssault.Services
{
    public static class MoveHolder
    {
        public static List<MoveModel> MoveList = new List<MoveModel>()
            {
                new MoveModel {
                    Name = "Crackshot",
                    Description = "Years of plinking in state-of-the-art simulations have given you formidable ranged ability.",
                    Attack = 5,
                    Type = "ranged",
                    Uses = 10
                    },
                new MoveModel
                {
                    Name = "Iron Grip",
                    Description = "You have trained your hands from birth and now they squeeze things... fiercely.",
                    Attack = 5,
                    Type = "melee",
                    Uses = 10
                },
                new MoveModel
                {
                    Name = "Snipe",
                    Description = "Take a deep breath, squeeze, and release. Does 10 ranged damage.",
                    Attack = 10,
                    Type = "ranged",
                    Uses = 5
                },
                new MoveModel
                {
                    Name = "Loaded Charge",
                    Description = "You pack a few big boy bullets. Good on ya. Does 15 ranged damage.",
                    Attack = 20,
                    Type = "ranged",
                    Uses = 3
                },
                new MoveModel
                {
                    Name = "Flying Fist",
                    Description = "Now those knuckles bruise big! Does 10 melee damage.",
                    Attack = 10,
                    Type = "melee",
                    Uses = 5
                },
                new MoveModel
                {
                    Name = "Brick Kick",
                    Description = "How can you walk with feet that hard? Does 20 melee damage.",
                    Attack = 20,
                    Type = "melee",
                    Uses = 3
                }

            };

        public static MoveModel GetMove(string name)
        {
            //Predicated for seeing if move exists
            Predicate<MoveModel> nameFinder = (MoveModel p) => { return p.Name == name; };
            if (MoveList.Exists(nameFinder))
            {
                return (MoveList.Find(nameFinder));
            }
            return null;
        }
    }
}
