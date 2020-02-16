using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        // The Battle Engine
        BattleEngine Engine = new BattleEngine();

        /// <summary>
        /// Return the Score Object
        /// </summary>
        /// <returns></returns>
        public ScoreModel GetScoreObject() { return Engine.BattleScore; }

        /// <summary>
        /// Run Auto Battle
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RunAutoBattle()
        {
            RoundEnum RoundCondition;

            Debug.WriteLine("Auto Battle Starting");

            // Auto Battle, does all the steps that a human would do.

            // Prepare for Battle

            // Picks 6 Characters
            var data = new CharacterModel();
            for (int i=0; i<6; i++) {
                Engine.PopulateCharacterList(data);
            }

            // Start Battle in AutoBattle mode
            Engine.StartBattle(true);

            // Fight Loop. Continue until Game is Over...
            do
            {
                Debug.WriteLine("Next Turn");

                // Do the turn...
                // If the round is over start a new one...
                RoundCondition = Engine.RoundNextTurn();

                if (RoundCondition == RoundEnum.NewRound)
                {
                    Engine.NewRound();
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != RoundEnum.GameOver);

            Debug.WriteLine("Game Over");

            // Wrap up
            Engine.EndBattle();

            return true;
        }
    }
}