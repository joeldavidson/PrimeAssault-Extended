using System;
using System.Collections.Generic;
using PrimeAssault.Models;

namespace PrimeAssault.Services
{
    /// <summary>
    /// Class for Ability holder
    /// </summary>
    public static class AbilityHolder
    {
        /// <summary>
        /// Class for character ability holder
        /// </summary>
        public static class CharacterAbilityHolder
        {
            /// <summary>
            /// List of abilities
            /// </summary>
            public static List<AbilityModel> AbilityList = new List<AbilityModel>()
            {
                 new AbilityModel
                 {
                    Name = "Crocodile Hunter",
                    Description = "You know your way around them beasties! Crikey! +30% damage against sewer creatures..",
                    EffectValue = .3,
                    Faction = "player",
                    CharacterAffected = "self",

                 },
                new AbilityModel
                {
                    Name = "X-Ray vision",
                    Description = "Whether up close or far away, you can find the hidden chinks in Euphrates armor. +30% damage against all mechs.",
                    EffectValue = .3,
                    Faction = "player",
                }
            };

            /// <summary>
            /// Ability model for getting move
            /// </summary>
            public static AbilityModel getMove(string name)
            {
                //Predicate for seeing if move exists
                Predicate<AbilityModel> nameFinder = (AbilityModel p) => { return p.Name == name; };
                if (AbilityList.Exists(nameFinder))
                    return (AbilityList.Find(nameFinder));
                return null;
            }
        }
    }
}
