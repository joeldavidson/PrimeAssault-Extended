using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    /// <summary>
    /// Hold the Details for each Level
    /// </summary>
    class LevelDetailsModel
    {
        // The Level
        public int Level;
        
        // Experience points to achieve the level
        public int Experience;

        // Attack Bonus
        public int Attack;
        
        // Defense Bonus
        public int Defense;
        
        // Speed Bonus
        public int Speed;

        //Ranged Defense Bonus
        public int RangedDefense;

        /// <summary>
        /// Create a new level based on values passed in
        /// </summary>
        /// <param name="level"></param>
        /// <param name="experience"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="speed"></param>
        public LevelDetailsModel(int level, int experience, int attack, int defense, int rangedDefense, int speed)
        {
            Level = level;
            Experience = experience;
            Attack = attack;
            RangedDefense = rangedDefense;
            Defense = defense;
            Speed = speed;
        }
    }
}