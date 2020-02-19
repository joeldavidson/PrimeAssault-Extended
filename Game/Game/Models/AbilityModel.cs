using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    public class AbilityModel : BaseModel<AbilityModel>
    {
        //What kind of character can have the ability
        public string Faction { get; set; } = "player"; //alternative is "monster"
        //Name of the ability
        public double EffectValue { get; set; } = 0.0;
        //public string who it affects (self/enemy)
        public string CharacterAffected { get; set; } = "self";


        //stats to affect

        //Ability augments MaxHealth
        public bool AffectMaxHealth { get; set; } = false;
        //Ability augments Speed
        public bool AffectSpeed { get; set; } = false;
        //Ability augments Defense
        public bool AffectDefense { get; set; } = false;
        //Ability augments RangedDefense
        public bool AffectRangedDefense { get; set; } = false;
        //Ability augments DropRate
        public bool AffectDropRate { get; set; } = false;
        //Ability augments AffectHealing
        public bool AffectHealing { get; set; } = false;
        //Ability augments HealthRegen
        public bool AffectHealthRegen { get; set; } = false;
        //specific to only sewer creatures
        public bool SewerCreatureSpecific { get; set; } = false;
        //specific to only mechs
        public bool MechSpecific { get; set; } = false;

        public AbilityModel()
        {
            Name = "None";
            Description = "none";
        }
    }
}