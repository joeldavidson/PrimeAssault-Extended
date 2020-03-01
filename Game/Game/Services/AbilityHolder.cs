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
        /// List of abilities
        /// </summary>
        public static List<AbilityModel> AbilityList = new List<AbilityModel>()
        {
            new AbilityModel()
            {
                Name = "Crocodile Hunter",
                Description = "You know your way around them beasties! Crikey! +30% damage against sewer creatures..",
                FirstEffectValue = .3,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Advantage,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                SecondEffect = AbilityEffectEnum.SewerCreatureSpecific

            },
            new AbilityModel()
            {
                Name = "X-Ray vision",
                Description = "Whether up close or far away, you can find the hidden chinks in Euphrates armor. +30% damage against all mechs.",
                FirstEffectValue = .3,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Advantage,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                SecondEffect = AbilityEffectEnum.SewerCreatureSpecific
            }
        };

        /// <summary>
        /// Ability model for getting move
        /// </summary>
        public static AbilityModel GetAbility(string name)
        {
            //Predicate for seeing if move exists
            Predicate<AbilityModel> nameFinder = (AbilityModel p) => { return p.Name == name; };
            if (AbilityList.Exists(nameFinder))
            {
                return (AbilityList.Find(nameFinder));
            }
            return null;
        }
    }
}
