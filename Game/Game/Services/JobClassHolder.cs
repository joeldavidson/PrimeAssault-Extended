using System;
using System.Collections.Generic;
using PrimeAssault.Models;

namespace PrimeAssault.Services
{
    public static class JobClassHolder
    {
        /// <summary>
        /// Single instance of job classes which should be listed for final version. Like a datastore but not.
        /// </summary>
        public static List<jobClassModel> JobClassList = new List<jobClassModel>()
        {
            new jobClassModel
            {
                HealthMult= .2,
                SpeedMult = -.1,
                DefenseMult = .3,
                RangedDefenseMult = -.3,
                AttackMult = .2,
                Description = "Soldiers have high health, and defense, and are more likely to get abilities that help with close-range combat.",
                ClassName = "Soldier",
                NextLevelMult = 1.0,
                ImageURI = "soldier_class.png",
            },
            new jobClassModel
            {
                HealthMult = -.3,
                SpeedMult = .3,
                DefenseMult = 0,
                RangedDefenseMult = .15,
                AttackMult = .15,
                Description = "The hunter has high Attack and Speed and its bonuses usually lend to killing sewer creatures from afar.",
                ClassName = "Hunter",//only done for formatting purposes, very hacky
                NextLevelMult = 1.0,
                ImageURI = "hunter_class.png",
            },
            new jobClassModel
            {
                HealthMult = .5,
                SpeedMult = -.5,
                DefenseMult = .15,
                RangedDefenseMult = .15,
                AttackMult = .05,
                Description = "The brawler is a beef-cake with high overall survivability, but no amazing offensive power.",
                ClassName = "Brawler",//only done for formatting purposes, very hacky
                NextLevelMult = 1.1,
                ImageURI = "brawler_class.png",
            },
            new jobClassModel
            {
                HealthMult = -.2,
                SpeedMult = .4,
                DefenseMult = -.2,
                RangedDefenseMult = .2,
                AttackMult = -.25,
                Description = "Mechanic has low overall stats, but all mechanic abilities allow for healing of teammates.",
                ClassName = "Mechanic",//only done for formatting purposes, very hacky
                NextLevelMult = 1.2,
                ImageURI = "mechanic_class.png",
            },
            new jobClassModel
            {
                HealthMult = 0,
                SpeedMult = 0,
                DefenseMult = .1,
                RangedDefenseMult = .1,
                AttackMult = .1,
                Description = "The mechanist has high ranged defense and attack, and its bonuses are good for killing Euphrates mechs.",
                ClassName = "Mechanist",//only done for formatting purposes, very hacky
                NextLevelMult = 1.1,
                ImageURI = "mechanist_class.png",
            },
             new jobClassModel
            {
                HealthMult = -.1,
                SpeedMult = -.1,
                DefenseMult = -.1,
                RangedDefenseMult = -.1,
                AttackMult = -.1,
                Description = "A ringleader has low base stats, but compensates for this through its ability to channel 10 rings to great possible effect.",
                ClassName = "Ringleader",
                NextLevelMult = 1.3,
                ImageURI = "ringleader_class.png",
            }
        };

        /// <summary>
        /// Ability model for getting a class
        /// </summary>
        public static jobClassModel GetClass(string ClassName)
        {
            //Predicate for seeing if class exists
            ClassName = ClassName.ToLower();
            Predicate<jobClassModel> nameFinder = (jobClassModel p) => { return p.ClassName.ToLower() == ClassName; };
            if (JobClassList.Exists(nameFinder))
                return (JobClassList.Find(nameFinder));
            return null;
        }
    }
}
