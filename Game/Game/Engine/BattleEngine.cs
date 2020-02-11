using System;
using System.Collections.Generic;
using System.Text;

using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Battle Engine for the Game
    /// </summary>
    public class BattleEngine
    {
        // Temprorary hold Character until refactored
        public class CharacterModel : BaseModel<CharacterModel>{}

        // List of Characters
        public List<CharacterModel> CharacterList = new List<CharacterModel>();

        // List of Monsters
        public List<CharacterModel> MonsterList = new List<CharacterModel>();

        public ScoreModel BattleScore = new ScoreModel();

        public bool BattleRunning = false;

        public bool PopulateCharacterList()
        {
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            
            return true;
        }

        public bool PopulateMonsterList()
        {
            MonsterList.Add(new CharacterModel());
            MonsterList.Add(new CharacterModel());
            MonsterList.Add(new CharacterModel());
            MonsterList.Add(new CharacterModel());
            MonsterList.Add(new CharacterModel());
            MonsterList.Add(new CharacterModel());

            return true;
        }

        public bool StartBattle(bool isAutoBattle)
        {
            BattleScore.AutoBattle = isAutoBattle;
            
            BattleRunning = true;

            return true;
        }

        public bool EndBattle()
        {
            BattleRunning = false;

            return true;
        }

        public bool NewRound()
        {
            // Add Monsters to the Round
            MonsterList.Clear();
            PopulateMonsterList();

            // Increment the Round Number
            BattleScore.RoundCount++;

            return true;
        }
    }
}