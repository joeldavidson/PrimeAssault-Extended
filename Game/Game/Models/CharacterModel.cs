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
            RangedDefense = 2;
            Speed = 2;
            ImageURI = "soldier_class.png";
            Moves[0] = MoveHolder.getMove("Crackshot");
            Moves[1] = MoveHolder.getMove("Iron Grip");
        }
    }
}