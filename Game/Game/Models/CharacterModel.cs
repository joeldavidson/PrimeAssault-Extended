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
    }
}