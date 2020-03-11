using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

using PrimeAssault.Engine;
using PrimeAssault.Models;
using PrimeAssault.Views;
using PrimeAssault.Services;
using PrimeAssault.ViewModels;

namespace PrimeAssault.Engine
{
    /// <summary>
    /// Battle Engine for the PrimeAssault
    /// </summary>
    public class BattleEngine : RoundEngine
    {
        // Track if the Battle is Running or Not
        public bool BattleRunning = false;

        // For Hack 16
        public bool ZombieApocalypse =false;
        public int ResChance = 0;

        #region Constructor

        public BattleEngine()
        {
            MessagingCenter.Subscribe<AboutPage, int>(this, "Zombification", async (obj, data) =>
            {
                await SetZombieApocalypse(data);
            });

            MessagingCenter.Subscribe<AboutPage, int>(this, "ZombificationChance", async (obj, data) =>
            {
                await SetResChance(data);
            });


            #endregion Constructor
        }

        async public Task<bool> SetZombieApocalypse(int input)
        {
            if (input == 0)
            {
                ZombieApocalypse = false;
            }
            else
            {
                ZombieApocalypse = true;
            }
            return await Task.FromResult(true);
        }
        async public Task<bool> SetResChance(int input)
        {
            ResChance = input;
            return await Task.FromResult(true);
        }
        /// <summary>
        /// Add the charcter to the character list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool PopulateCharacterList(CharacterModel data)
        {
            
            CharacterList.Add(new PlayerInfoModel(data));

            return true;
        }

        /// <summary>
        /// Start the Battle
        /// </summary>
        /// <param name="isAutoBattle"></param>
        /// <returns></returns>
        public bool StartBattle(bool isAutoBattle)
        {
            // Reset the Score so it is fresh
            BattleScore = new ScoreModel
            {
                AutoBattle = isAutoBattle
            };

            BattleMessagesModel.ZombieApocalypse = ZombieApocalypse;
            BattleMessagesModel.ResChance = ResChance;
            BattleRunning = true;

            NewRound();

            return true;
        }

        /// <summary>
        /// End the Battle
        /// </summary>
        /// <returns></returns>
        public bool EndBattle()
        {
            BattleRunning = false;

            return true;
        }
    }
}