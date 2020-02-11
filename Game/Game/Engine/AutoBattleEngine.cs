using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// AutoBattle Engine
    /// 
    /// Runs the engine simulation with no user interaction
    /// 
    /// </summary>
    public class AutoBattleEngine
    {
        #region Algrorithm
        // Prepare for Battle
        // Pick 6 Characters
        // Initialize the Battle
        // Start a Round

        // Fight Loop
        // Loop in the round each turn
        // If Round is over, Start New Round
        // Check end state of round/game

        // Wrap up
        // Get Score
        // Save Score
        // Output Score
        #endregion Algrorithm

        #region RunAutoBattle
        public bool RunAutoBattle()
        {

            // Auto Battle, does all the steps that a human would do.

            // Perpare for Battle

            // Picks 6 Characters
            // Start
            // Initialize the Rounds

            // Fight

            // Fight Loop. Continue until Game is Over...
            bool GameContinueCondition = false;
            do
            {
                // Do the turn...
                // If the round is over start a new one...
            } while (GameContinueCondition);

            // Wrap up
            return true;
        }
        #endregion RunAutoBattle

        #region ScoreInformation
        public int GetScoreValue() { return 0; }
        public ScoreModel GetScoreObject() { return new ScoreModel(); }
        public int GetRoundsValue() { return 0; }
        public string GetResultsOutput() { return "done"; }
        #endregion ScoreInformation
    }
}