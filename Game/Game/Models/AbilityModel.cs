using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    public class AbilityModel : BaseModel<AbilityModel>
    {
        //What kind of character can have the ability
        public string faction { get; set; } = "player"; //alternative is "monster"
        //Name of the ability
        public double effectValue { get; set; } = 0.0;

        public AbilityModel()
        {
            Name = "None";
            Description = "You can spot those shiny scales even when they lay covered in sewer grease. +10% chance of getting unique drops from sewer creatures.";
        }
    }
}