using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    public class jobClassModel : BaseModel<jobClassModel>
    {

        //The percentage values which will be ADDED with any item bonuses to the standard 1.0 total multiplier for any stats
        public double HealthMult { get; set; } = 0;
        public double SpeedMult { get; set; } = 0;
        public double DefenseMult { get; set; } = 0;
        public double RangedDefenseMult { get; set; } = 0;
        public double AttackMult { get; set; } = 0;
        public double NextLevelMult { get; set; } = 1.0;

        //Name of each class eg "soldier" or "mechanic"
        public string ClassName { get; set; } = "Standard";

        //Default constructor with the "Null" values
        public jobClassModel()
        {
            Description = "This is not a real class";
            ClassName = "Unassigned";
            ImageURI = "";
        }
    }
}
