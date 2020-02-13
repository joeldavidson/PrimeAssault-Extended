using System.Collections.Generic;
using Game.Models;

namespace Game.Models
{
    public class CharacterModel : BasePlayerModel<CharacterModel>
    {
        public CharacterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
        }
    }

    public class MonsterModel : BasePlayerModel<MonsterModel>
    {
        public MonsterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
        }
    }

    public class PlayerInfoModel : BasePlayerModel<PlayerInfoModel>
    { 
    }
}