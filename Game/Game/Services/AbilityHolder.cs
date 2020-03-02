using System;
using System.Collections.Generic;
using PrimeAssault.Models;
using PrimeAssault.Helpers;

namespace PrimeAssault.Services
{
    /// <summary>
    /// Class for Ability holder
    /// </summary>
    public static class AbilityHolder
    {
        //Number of different possible abilities, update if more are added. Reflect changes in Ability Enum
        public static int ABILITY_COUNT = 5;
        /// <summary>
        /// List of abilities
        /// </summary>
        public static List<AbilityModel> AbilityList = new List<AbilityModel>()
        {
            new AbilityModel()
            {
                Name = "Crocodile Hunter",
                Description = "You know your way around them beasties! Crikey! +30% damage against sewer creatures.",
                FirstEffectValue = .3,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Advantage,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                Advantage = MonsterTypesEnum.SewerCreature,
                AbilityID = AbilityEnum.CrocodileHunter
            },
            new AbilityModel()
            {
                Name = "X-Ray vision",
                Description = "Whether up close or far away, you can find the hidden chinks in Euphrates armor. +30% damage against all mechs.",
                FirstEffectValue = .3,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Advantage,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                Advantage = MonsterTypesEnum.Mech,
                AbilityID = AbilityEnum.XRayVision
            },
            new AbilityModel()
            {
                Name = "Generalist",
                Description = "They all say you don’t really have a niche but, darn it, that is your niche! Prove the others wrong! +15% damage to all enemies.",
                FirstEffectValue = .15,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Attack,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                AbilityID = AbilityEnum.Generalist
            },
            new AbilityModel()
            {
                Name = "Big Game Hunter",
                Description = "Your elephant gun has bested every beast known to man! Well, in the simulations at least… +60% ranged damage against sewer creatures.",
                FirstEffectValue = .6,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Advantage,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                Advantage = MonsterTypesEnum.SewerCreature,
                AbilityID = AbilityEnum.BigGameHunter
            },
            new AbilityModel()
            {
                Name = "Y2Killer",
                Description = "Crunch those mechs man! Euphrates never stood a chance. +60 physical damage against mechs.",
                FirstEffectValue = .6,
                Faction = "player",
                TriggeredOn = AbilityTriggerEnum.Advantage,
                FirstEffect = AbilityEffectEnum.AffectAttack,
                Advantage = MonsterTypesEnum.Mech,
                AbilityID = AbilityEnum.Y2Killer
            }
        };

        /// <summary>
        /// Ability model for getting ability by name
        /// </summary>
        public static AbilityModel GetAbility(AbilityEnum name)
        {
            //Predicate for seeing if move exists
            Predicate<AbilityModel> nameFinder = (AbilityModel p) => { return p.AbilityID == name; };
            if (AbilityList.Exists(nameFinder))
            {
                return (AbilityList.Find(nameFinder));
            }
            return null;
        }

        //Gets a random ability from the list.
        public static AbilityModel GetRandomAbility()
        {
            return GetAbility((AbilityEnum)DiceHelper.RollDice(1, ABILITY_COUNT));
        }
    }
}
