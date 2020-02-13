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

    public class PlayerInfo : BaseModel<PlayerInfo>
    {
        // TurnOrder
        public int Order;

        // guid of the original data it links back to
        public string Guid;

        // alive status, !alive will be removed from the list
        public bool Alive;

        // Sorting Order is :  Speed, Level, ExperiencePoints, PlayerType, Name, ListOrder

        // Total speed, including level and items
        public int Speed;

        // Level of character or monster
        public int Level;

        // The experience points the player has used in sorting ties...
        public int ExperiencePoints;

        // The type of player, character comes before monster
        public PlayerTypeEnum PlayerType;

        // Finally if all of the above are the same, sort based on who was loaded first into the list...
        public int ListOrder;

        // Current Health
        public int CurrentHealth;

        // Max Health
        public int MaxHealth;

        // Need because of the instantiation below
        public PlayerInfo()
        {

        }

        // Take a character and add it to the Player
        public PlayerInfo(CharacterModel data)
        {
            PlayerType = PlayerTypeEnum.Character;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
            Guid = data.Id;
        }

        // Take a monster and add it to the player
        public PlayerInfo(MonsterModel data)
        {
            PlayerType = PlayerTypeEnum.Monster;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
            Guid = data.Id;
        }
    }
}