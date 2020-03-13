using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using PrimeAssault.Models;
using PrimeAssault.ViewModels;

namespace PrimeAssault.Engine
{
    class ManualBattleEngine : BattleEngine
    {
        /// <returns></returns>
        public ScoreModel GetScoreObject() { return BattleScore; }

        public async Task<bool> RunAutoBattle()
        {
            RoundEnum RoundCondition;

            Debug.WriteLine("Battle begins!");

            // Auto Battle, does all the steps that a human would do.

            // Prepare for Battle

            CreateCharacterParty();

            // Start Battle in AutoBattle mode
            StartBattle(false);

            // Fight Loop. Continue until Game is Over...
            do
            {
                Debug.WriteLine("Next Turn");

                // Do the turn...
                // If the round is over start a new one...
                RoundCondition = RoundNextTurn(); //What guides decisionmaking for turn order and targeting and attack

                if (RoundCondition == RoundEnum.NewRound)
                {
                    NewRound();
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != RoundEnum.PrimeAssaultOver);

            Debug.WriteLine("Game Over");

            // Wrap up
            EndBattle();

            return true;
        }

        /// <summary>
        /// Create Characters for Party
        /// </summary>
        private bool CreateCharacterParty()
        {
            // Picks 6 Characters

            // To use your own characters, populate the List before calling RunAutoBattle

            // Will first pull from existing characters
            for (int i = CharacterList.Count(); i < MaxNumberPartyCharacters; i++)
            {
                foreach (var data in CharacterIndexViewModel.Instance.Dataset)
                {
                    PopulateCharacterList(data);
                }
                break;
            }

            //If there are not enough will add random ones
            for (int i = CharacterList.Count(); i < MaxNumberPartyCharacters; i++)
            {
                PopulateCharacterList(Helpers.RandomPlayerHelper.GetRandomCharacter(20));
            }

            return true;
        }
    }
}
