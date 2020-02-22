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
                    attack = 5,
                    type = "ranged"
                    },
                new MoveModel
                {
                    Name = "Iron Grip",
                    Description = "You have trained your hands from birth and now they squeeze things... fiercely.",
                    attack = 5,
                    type = "melee"
                }


            };

        public static MoveModel GetMove(string name)
        {
            //Predicated for seeing if move exists
            Predicate<MoveModel> nameFinder = (MoveModel p) => { return p.Name == name; };
            if (MoveList.Exists(nameFinder))
                return (MoveList.Find(nameFinder));
            return null;
        }
    }
}
