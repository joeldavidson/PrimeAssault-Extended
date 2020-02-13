
namespace Game.Models
{
    public class PlayerInfoModel : BasePlayerModel<PlayerInfoModel>
    {
        
        public PlayerInfoModel(PlayerInfoModel data) 
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
        }

        public PlayerInfoModel(CharacterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
        }

        public PlayerInfoModel(MonsterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
        }
    }
}