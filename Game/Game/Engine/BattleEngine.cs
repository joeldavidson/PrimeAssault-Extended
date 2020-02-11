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
        public enum RoundEnum {Unknown = 0,NextTurn = 1,NewRound = 2,GameOver = 3,}

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

        public RoundEnum NextTurn(bool killme)
        {
            if (killme)
            {
                CharacterList.RemoveAt(0);
            }
            else
            {
                MonsterList.RemoveAt(0);
            }

            if (MonsterList.Count == 0)
            {
                // Kill off a character, so the game will end...
                CharacterList.RemoveAt(0);

                return RoundEnum.NewRound;
            }

            if (CharacterList.Count == 0)
            {
                return RoundEnum.GameOver;
            }

            return RoundEnum.NextTurn;
        }
    }
}