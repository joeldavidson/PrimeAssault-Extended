namespace Game.Models
{
    public class MonsterModel : BasePlayerModel<MonsterModel>
    {
        public MonsterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
            Guid = Id;
        }
    }
}