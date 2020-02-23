using PrimeAssault.Services;
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
        public string JobClass { get; set; } = "Soldier";


        /// <summary>
        /// Default character
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public CharacterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
            Guid = Id;
            Name = "";
            Description = "";
            Attack = 5;
            Defense = 5;
            CurrentHealth = 20;
            MaxHealth = 20;
            RangedDefense = 2;
            Speed = 2;
            ImageURI = "soldier_class.png";
            Moves[0] = MoveHolder.GetMove("Crackshot");
            Moves[1] = MoveHolder.GetMove("Iron Grip");
            Move1 = "Crackshot";
            Move2 = "Iron Grip";
        }

        bool setClass()//ugly awful class, not maintanable, needs work. So sorry to everyone, just trying to get class up on its feet. Hardcode galore.
        {
            if (JobClass == "Soldier") //helper class with default values for each class
            {
                ResetMultipliers();
                HealthMult += .2;
                SpeedMult += -.1;
                DefenseMult += .3;
                RangedDefenseMult += -.3;
                AttackMult += .2;
                Description = "Soldiers have high health, and defense, and are more likely to get abilities that help with close-range combat.";
                JobClass = "Soldier"; //only done for formatting purposes, very hacky
                NextLevelMult = 1.0;
                ImageURI = "soldier_class.png";
                return true;
            }
            if (JobClass == "Hunter")
            {
                ResetMultipliers();
                HealthMult += -.3;
                SpeedMult += .3;
                DefenseMult += 0;
                RangedDefenseMult += .15;
                AttackMult += .15;
                Description = "The hunter has high Attack, and Speed and its bonuses usually lend to killing sewer creatures from afar.";
                JobClass = "Hunter";//only done for formatting purposes, very hacky
                NextLevelMult = 1.0;
                ImageURI = "hunter_class.png";
                return true;
            }
            if (JobClass == "Brawler")
            {
                ResetMultipliers();
                HealthMult += .5;
                SpeedMult += -.5;
                DefenseMult += .15;
                RangedDefenseMult += .15;
                AttackMult += .05;
                Description = "The brawler is a beef-cake with high overall survivability, but no amazing offensive power.";
                JobClass = "Brawler";//only done for formatting purposes, very hacky
                NextLevelMult = 1.1;
                ImageURI = "brawler_class.png";
                return true;
            }
            if (JobClass == "Mechanic")
            {
                ResetMultipliers();
                HealthMult += -.2;
                SpeedMult += .4;
                DefenseMult += -.2;
                RangedDefenseMult += .2;
                AttackMult += -.25;
                Description = "Mechanic has low overall stats, but all mechanic abilities allow for healing of teammates.";
                JobClass = "Mechanic";//only done for formatting purposes, very hacky
                NextLevelMult = 1.2;
                ImageURI = "mechanic_class.png";
                return true;
            }
            if (JobClass == "Mechanist")
            {
                ResetMultipliers();
                HealthMult += 0;
                SpeedMult += 0;
                DefenseMult += .1;
                RangedDefenseMult += .1;
                AttackMult += .1;
                Description = "The mechanist has high ranged defense and attack, and its bonuses are good for killing Euphrates mechs.";
                JobClass = "Mechanist";//only done for formatting purposes, very hacky
                NextLevelMult = 1.1;
                ImageURI = "mechanist_class.png";
                return true;
            }
            if (JobClass == "Ringleader")
            {
                ResetMultipliers();
                HealthMult += -.1;
                SpeedMult += -.1;
                DefenseMult += -.1;
                RangedDefenseMult += -.1;
                AttackMult += -.1;
                Description = "A ringleader has low base stats, but compensates for this through its ability to channel 10 rings to great possible effect.";
                JobClass = "Ringleader";
                NextLevelMult = 1.3;
                ImageURI = "ringleader_class.png";
                return true;
            }
            return false;
        }
    }
}