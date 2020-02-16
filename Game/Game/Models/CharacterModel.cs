
namespace Game.Models
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
            Name = "Elf";
            Description = "Happy Elf";
        }
    }
}