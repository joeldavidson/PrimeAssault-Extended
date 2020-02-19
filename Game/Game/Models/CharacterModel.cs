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
            Name = "Boomer";
            Description = "The day just doesn't end!";
            Attack = 100;
            ImageURI = "soldier_class.png";
            Moves[0] = MoveHolder.getMove("Crackshot");
            Moves[1] = MoveHolder.getMove("Crackshot");

        }
    }
}