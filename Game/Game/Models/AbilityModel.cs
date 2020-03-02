using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    /// <summary>
    /// Abilities can be held by both monsters and characters, and each kind 
    /// </summary>
    public class AbilityModel : BaseModel<AbilityModel>
    {
        //Name of the faction that can hold the ability
        public string Faction { get; set; } = "player"; //alternative is "sewerMonster" or "euphratesMech"
       
        //when this effect should activate
        public AbilityTriggerEnum TriggeredOn { get; set; } = AbilityTriggerEnum.None;

        public MonsterTypesEnum Advantage { get; set; } = MonsterTypesEnum.Nothing;

        public AbilityEnum AbilityID { get; set; } = AbilityEnum.None;
        
        //the bool which determines whether or not the character's ability is being used to calculate stats
        public bool IsActive { get; set; } = false;

        //First effect of ability
        public AbilityEffectEnum FirstEffect { get; set; } = AbilityEffectEnum.Nothing;
        //How much the Effect mutates a value by
        public double FirstEffectValue { get; set; } = 0.0;

        //Second effect of the ability
        public AbilityEffectEnum SecondEffect { get; set; } = AbilityEffectEnum.Nothing;
        //How much the Effect mutates a value by
        public double SecondEffectValue { get; set; } = 0.0;

        //Third effect the ability may have
        public AbilityEffectEnum ThirdEffect {get; set;} = AbilityEffectEnum.Nothing;
        //How much the Effect mutates a value by
        public double ThirdEffectValue { get; set; } = 0.0;

        public AbilityModel()
        {
            Name = "None";
            Description = "None";
        }

        public bool ActivateAbility()
        {
            IsActive = true;
            return IsActive;
        }

        public bool DeactivateAbility()
        {
            IsActive = false;
            return IsActive;
        }

        public bool IsAdvantaged(MonsterTypesEnum OpponentType)
        {
            if (Advantage == OpponentType)
            {
                return true;
            }
            return false;
        }
    }
}