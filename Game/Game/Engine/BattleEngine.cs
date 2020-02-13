namespace Game.Engine
{
    /// <summary>
    /// Battle Engine for the Game
    /// </summary>
    public class BattleEngine : RoundEngine
    {
        public bool BattleRunning = false;

        public bool PopulateCharacterList(CharacterModel data)
        {
            CharacterList.Add(data);

            return true;
        }

        public bool StartBattle(bool isAutoBattle)
        {
            BattleScore.AutoBattle = isAutoBattle;

            BattleRunning = true;

            NewRound();

            return true;
        }

        public bool EndBattle()
        {
            BattleRunning = false;

            return true;
        }
    }
}