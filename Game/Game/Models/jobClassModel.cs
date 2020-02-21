using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    class jobClassModel : BaseModel<jobClassModel>
    {

        //The percentage values which will be ADDED with any item stat bonuses to the standard 1.0 total multiplier for any stats
        double HealthMult { get; set; } = 0;
        double SpeedMult { get; set; } = 0;
        double DefenseMult { get; set; } = 0;
        double RangedDefenseMult { get; set; } = 0;
        double AttackMult { get; set; } = 0;
        double NextLevelMult { get; set; } = 1.0;
        
        string className { get; set; } = "Standard";
        public jobClassModel()
        {
            Description = "This is not a real class";
            className = "Unassigned";
            ImageURI = "";
        }
    }
}
