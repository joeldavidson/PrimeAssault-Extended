﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    //Enemie
    public class MonsterModel : BasePlayerModel<MonsterModel>
    {
        //current xp give equation
        //(BASE_XP + (LVL_MULT * level) ^ LVL_EXP) = (1 + (2 * level) ^ 2)
        const int BASE_XP = 1;
        const int LVL_MULT = 2;
        const int LVL_EXP = 2;

        //Default monster image
        const string DEFAULT_URI = "sewer_gator.png";
        
        //Item that is dropped by the monster
        ItemModel Drop = new ItemModel(); //should be retrieved at random?
        
        //Type of monster the player is facing
        public string type { get; set; } = "sewer creature";

        //The monster (inherits from base)
        public MonsterModel() : base()
        {
            ImageURI = DEFAULT_URI;
            Name = "";
            Description = "";
            Attack = 5;
            Defense = 5;
            CurrentHealth = 20;
            MaxHealth = 20;
            RangedDefense = 2;
            Speed = 2;
            ImageURI = "sewer_gator.png";
            Move1 = "Crackshot";
            Move2 = "Iron Grip";
        }

        //Levels up the Monster if they are ready
        bool LevelUp() 
        {
            Level++;
            increaseStats();
            return true;
        }

        //Basic level up stat increases, temporary, will eventually change when classes are better hammered out
        void increaseStats() 
        {
            CurrentHealth += 5;
            Defense += 2;
            RangedDefense += 2;
            Speed += 2;
            Attack += 2;
        }

        //provides multipliers for stats that are increased with every level
        void equalizeMultipliers()
        {
            HealthMult = 1;
            SpeedMult = 1;
            DefenseMult = 1;
            RangedDefenseMult = 1;
            AttackMult = 1;
        }

        //experience given by the monster (NEEDS TO BE REDONE TO FOLLOW GAME RULES)
        public int dropExp()
        {
            return (BASE_XP + (LVL_MULT * Level) ^ LVL_EXP);
        }

    }
}
