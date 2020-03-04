using PrimeAssault.Services;
using PrimeAssault.Helpers;
namespace PrimeAssault.Models
{
    /// <summary>
    /// Characters
    /// 
    /// Derive from BasePlayerModel
    /// </summary>
    public class CharacterModel : BasePlayerModel<CharacterModel>
    {

        //The class of the character in question
        public string JobClass { get; set; }


        /// <summary>
        /// Default character
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public CharacterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
            Name = RandomPlayerHelper.GetCharacterName(); ;
            Description = RandomPlayerHelper.GetCharacterDescription();
            Attack = 5;
            Defense = 5;
            CurrentHealth = 20;
            MaxHealth = 20;
            RangedDefense = 2;
            Speed = 2;
            SetJobClass("Soldier");
        }

        public CharacterModel(CharacterModel data)
        {
            Update(data);
        }

        override public bool Update(CharacterModel data)
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

            if (data.Level < 21 && data.Level > 0)
            {
                Level = data.Level;
            }
            else
            {
                Level = 1;
            }

            HealthMult = data.HealthMult;
            SpeedMult = data.SpeedMult;
            DefenseMult = data.DefenseMult;
            RangedDefenseMult = data.RangedDefense;
            AttackMult = data.AttackMult;

            //uniue to character class
            JobClass = data.JobClass;
            SetJobClass(data.JobClass);

            Move1 = data.Move1;
            Move2 = data.Move2;
            Moves[0] = data.Moves[0];
            Moves[1] = data.Moves[1];
            Ability = data.Ability;

            return true;
        }

        public bool SetJobClass(string InClass)
        {
            jobClassModel Current = JobClassHolder.GetClass(InClass.ToLower());
            ResetMultipliers();
            HealthMult += Current.HealthMult;
            SpeedMult += Current.SpeedMult;
            DefenseMult += Current.DefenseMult;
            RangedDefenseMult += Current.RangedDefenseMult;
            AttackMult += Current.AttackMult;
            Description = Current.Description;
            JobClass = InClass; //only done for formatting purposes, very hacky
            NextLevelMult = Current.NextLevelMult;
            ImageURI = Current.ImageURI;

            Ability = AbilityHolder.GetRandomAbility();

            return true;
        }

        public override bool LevelUp()
        {
            if (Level < LevelTableHelper.MaxLevel)
            {
                if (LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience <= ExperienceTotal)
                {
                    ++Level;
                    return true;
                }
            }
            return false;
        }
        public int LevelUpToValue(int Value)
        {
            // Adjust the experience to the min for that level.
            // That will trigger level up to happen

            if (Value < 0)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value <= Level)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value > LevelTableHelper.MaxLevel)
            {
                Value = LevelTableHelper.MaxLevel;
            }

            AddExperience(LevelTableHelper.Instance.LevelDetailsList[Value].Experience + 1);

            return Level;
        }
    }
}