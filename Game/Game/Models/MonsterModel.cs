namespace Game.Models
{
    public class MonsterModel : BasePlayerModel<MonsterModel>
    {
        public MonsterModel()
        {
            PlayerType = PlayerTypeEnum.Monster;
            Guid = Id;
        }
    }
}