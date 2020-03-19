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
            MaxHealth = 20;
            RangedDefense = 2;
            Speed = 2;
            SetJobClass("Soldier");
            lastToAttack = false;
            lastToGetHit = false;
            flip = 0;
            X = 0;
            Y = 0;
            ExperienceTotal = 0;
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;
            selected = false;
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
            MaxHealth = data.MaxHealth;
            CurrentHealth = data.CurrentHealth;
            Speed = data.Speed;
            Attack = data.Attack;
            RangedDefense = data.RangedDefense;
            Defense = data.Defense;


            ExperienceTotal = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
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

            lastToAttack = data.lastToAttack;
            lastToGetHit = data.lastToGetHit;
            flip = data.flip;

            X = data.X;
            Y = data.Y;
            selected = data.selected;
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
            // Walk the Level Table descending order
            // Stop when experience is >= experience in the table
            for (var i = LevelTableHelper.Instance.LevelDetailsList.Count - 1; i > 0; i--)
            {
                // Check the Level
                // If the Level is > Experience for the Index, increment the Level.
                if (LevelTableHelper.Instance.LevelDetailsList[i].Experience <= ExperienceTotal)
                {
                    var NewLevel = LevelTableHelper.Instance.LevelDetailsList[i].Level;

                    // When leveling up, the current health is adjusted up by an offset of the MaxHealth, rather than full restore
                    var OldCurrentHealth = CurrentHealth;
                    var OldMaxHealth = MaxHealth;

                    // Set new Health
                    // New health, is d10 of the new level.  So leveling up 1 level is 1 d10, leveling up 2 levels is 2 d10.
                    var NewHealthAddition = DiceHelper.RollDice(NewLevel - Level, 10);

                    // Increment the Max health
                    MaxHealth += NewHealthAddition;

                    // Calculate new current health
                    // old max was 10, current health 8, new max is 15 so (15-(10-8)) = current health
                    CurrentHealth = (MaxHealth - (OldMaxHealth - OldCurrentHealth));

                    // Set the new level
                    Level = NewLevel;

                    // Done, exit
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