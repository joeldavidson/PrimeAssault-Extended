using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    public class AbilityModel : BaseModel<AbilityModel>
    {
        public string Faction { get; set; } = "player"; //alternative is "sewerMonster" or "euphratesMech"
        //Name of the ability
        public double EffectValue { get; set; } = 0.0;
        //when this effect should activate
        public string TriggeredOn { get; set; } = "attack";

        //First effect should be used for The 
        public AbilityEffectEnum FirstEffect { get; set; } = AbilityEffectEnum.Nothing;

        public AbilityEffectEnum SecondEffect { get; set; } = AbilityEffectEnum.Nothing;

        public AbilityEffectEnum ThirdEffect {get; set;} = AbilityEffectEnum.Nothing;

        public AbilityModel()
        {
            Name = "None";
            Description = "None";
        }
    }
}