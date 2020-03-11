
namespace PrimeAssault.Models
{
    /// <summary>
    /// Player for the game.
    /// 
    /// Either Monster or Character
    /// 
    /// Constructor Player to Player used a T in Round
    /// </summary>
    public class PlayerInfoModel : BasePlayerModel<PlayerInfoModel>
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlayerInfoModel(){}
       

        /// <summary>
        /// Create PlayerInfoModel from character
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(CharacterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperienceTotal = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();

            Attack = data.Attack;
            Defense = data.Defense;
            RangedDefense = data.RangedDefense;
            Speed = data.Speed;
            MaxHealth = data.GetHealthMax();
            CurrentHealth = data.GetHealthCurrent();

            AttackMult = data.AttackMult;
            DefenseMult = data.DefenseMult;
            RangedDefenseMult = data.RangedDefenseMult;
            SpeedMult = data.SpeedMult;
            HealthMult = data.HealthMult;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            PrimaryHand = data.PrimaryHand;
            OffHand = data.OffHand;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            lastToAttack = data.lastToAttack;
            lastToGetHit = data.lastToGetHit;

            X = data.X;
            Y = data.Y;

            flip = data.flip;
        }

        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(PlayerInfoModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperienceTotal = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();

            Attack = data.Attack;
            Defense = data.Defense;
            RangedDefense = data.RangedDefense;
            Speed = data.Speed;
            MaxHealth = data.GetHealthMax();
            CurrentHealth = data.GetHealthCurrent();

            AttackMult = data.AttackMult;
            DefenseMult = data.DefenseMult;
            RangedDefenseMult = data.RangedDefenseMult;
            SpeedMult = data.SpeedMult;
            HealthMult = data.HealthMult;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            PrimaryHand = data.PrimaryHand;
            OffHand = data.OffHand;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            lastToAttack = data.lastToAttack;
            lastToGetHit = data.lastToGetHit;


            X = data.X;
            Y = data.Y;

            flip = data.flip;
        }

        /// <summary>
        /// Crate PlayerInfoModel from Monster
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(MonsterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperienceTotal = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();

            Attack = data.Attack;
            Defense = data.Defense;
            RangedDefense = data.RangedDefense;
            Speed = data.Speed;
            MaxHealth = data.GetHealthMax();
            CurrentHealth = data.GetHealthCurrent();

            AttackMult = data.AttackMult;
            DefenseMult = data.DefenseMult;
            RangedDefenseMult = data.RangedDefenseMult;
            SpeedMult = data.SpeedMult;
            HealthMult = data.HealthMult;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            lastToAttack = data.lastToAttack;
            lastToGetHit = data.lastToGetHit;


            X = data.X;
            Y = data.Y;

            flip = data.flip;
        }
        /*
        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();

            if (PlayerType == PlayerTypeEnum.Character)
            {
                myReturn += " , Total Experience : " + ExperienceTotal;
                myReturn += " , Damage : " + GetDamageTotalString;
                myReturn += " , Attack :" + GetAttackTotal;
                myReturn += " , Defense :" + GetDefenseTotal;
                myReturn += " , Speed :" + GetSpeedTotal;
            }

            myReturn += " , Items : " + ItemSlotsFormatOutput();

            return myReturn;
        }
        */
    }
}