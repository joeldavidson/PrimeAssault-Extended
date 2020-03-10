using System;
using System.Collections.Generic;
using System.Text;
using PrimeAssault.Helpers;
using PrimeAssault.Services;
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


        override public bool Update(MonsterModel data)
        {
            PlayerType = data.PlayerType;
            Name = data.Name;
            Description = data.Description;
            CurrentHealth = data.CurrentHealth;
            MaxHealth = data.MaxHealth;
            Speed = data.Speed;
            Attack = data.Attack;
            RangedDefense = data.RangedDefense;
            Defense = data.Defense;

            if (data.Level < 22 && data.Level > 0)
            {
                Level = data.Level;
            }
            else
            {
                Level = 1;
            }

            MonsterType = data.MonsterType;

            HealthMult = data.HealthMult;
            SpeedMult = data.SpeedMult;
            DefenseMult = data.DefenseMult;
            RangedDefenseMult = data.RangedDefense;
            AttackMult = data.AttackMult;

            ImageURI = data.ImageURI;

            Move1 = data.Move1;
            Move2 = data.Move2;
            Moves[0] = data.Moves[0];
            Moves[1] = data.Moves[1];
            Ability = data.Ability;

            X = data.X;
            Y = data.Y;

            return true;
        }

        //The monster (inherits from base) and its default monster stats
        public MonsterModel() : base()
        {
            PlayerType = PlayerTypeEnum.Monster;
            ImageURI = DEFAULT_URI;
            Name = RandomPlayerHelper.GetMonsterName();
            Description = RandomPlayerHelper.GetMonsterDescription();
            Attack = 7;
            Defense = 5;
            MaxHealth = 20;
            CurrentHealth = GetHealthMax();
            RangedDefense = 2;
            Speed = 2;
            ImageURI = "sewer_gator.png";

            X = 0;
            Y = 0;
        }

        public bool SetLevel(int targetLevel)
        {
            if (targetLevel > 0 && targetLevel <22)
            {
                Level = targetLevel;
            }
            return false;
        }


        //helper function which resets multipliers so that changing abilities does not stack previous bonuses with current ones
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
            return (LevelTableHelper.Instance.LevelDetailsList[Level].Experience);
        }

    }
}
