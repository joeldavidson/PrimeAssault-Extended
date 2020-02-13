using System.Collections.Generic;
using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Holds the Data Structures for the Battle Engine
    /// </summary>
    public class BaseEngine
    {
        #region Properties

        // Holds the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessagesModel BattleMessagesModel = new BattleMessagesModel();

        // The Pool of items collected during the round as turns happen
        public List<ItemModel> ItemPool = new List<ItemModel>();

        // List of Monsters
        public List<MonsterModel> MonsterList = new List<MonsterModel>();

        // List of Characters
        public List<CharacterModel> CharacterList = new List<CharacterModel>();

        // Current Player who is the attacker
        public PlayerInfoModel CurrentAttacker;

        // Current Player who is the Defender
        public PlayerInfoModel CurrentDefender;

        // Hold the list of players (MonsterModel, and character by guid), and order by speed
        public List<PlayerInfoModel> PlayerList;

        // Player currently engaged
        public PlayerInfoModel PlayerCurrent;

        public RoundEnum RoundStateEnum = RoundEnum.Unknown;

        public const int MaxNumberPartyPlayers = 6;
       #endregion Properties
    }
}