using Game.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        #region ScoreInformation
        public int GetScoreValue() { return 0; }
        public ScoreModel GetScoreObject() { return Engine.BattleScore; }
        public int GetRoundsValue() { return 0; }
        #endregion ScoreInformation

        /*
    #region RunAutoBattle
    public async Task<bool> RunAutoBattle()
    {

        // Auto Battle, does all the steps that a human would do.

        // Prepare for Battle

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
*/

        /*
        #region RunAutoBattlePrepare
        public async Task<bool> RunAutoBattle()
        {
            // Auto Battle, does all the steps that a human would do.

            // Prepare for Battle
            var Engine = new BattleEngine();

            // Picks 6 Characters
            Engine.PopulateCharacterList();

            // Start Battle in AutoBattle mode
            Engine.StartBattle(true);

            // Initialize the Rounds
            Engine.NewRound();

            // Fight

            // Fight Loop. Continue until Game is Over...
            bool GameContinueCondition = false;
            do
            {
                // Do the turn...
                // If the round is over start a new one...

            } while (GameContinueCondition);

            // Wrap up
            Engine.EndBattle();

            return true;
        }
        #endregion RunAutoBattlePrepare
    */

        #region RunAutoBattleFight

        public bool FlagWhoDies = false;
        BattleEngine Engine = new BattleEngine();

        public async Task<bool> RunAutoBattle()
        {
            BattleEngine.RoundEnum RoundCondition;

            Debug.WriteLine("Auto Battle Starting");

            // Auto Battle, does all the steps that a human would do.

            // Prepare for Battle

            // Picks 6 Characters
            Engine.PopulateCharacterList();

            // Start Battle in AutoBattle mode
            Engine.StartBattle(true);

            // Fight

            // Populate the Round
            Engine.NewRound();

            // Fight Loop. Continue until Game is Over...
            do
            {
                Debug.WriteLine("Next Turn");

                // Do the turn...
                // If the round is over start a new one...
                RoundCondition = Engine.NextTurn(FlagWhoDies);

                if (RoundCondition == BattleEngine.RoundEnum.NewRound)
                {
                    Engine.NewRound();
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != BattleEngine.RoundEnum.GameOver);

            Debug.WriteLine("Game Over");

            // Wrap up
            Engine.EndBattle();

            return true;
        }
        #endregion RunAutoBattleFight
    }
}