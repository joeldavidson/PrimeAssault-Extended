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

namespace PrimeAssault.ViewModels
{
    public class BattleEngineViewModel
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel instance;
        private static readonly object syncRoot = new Object();        
        // Track if the system needs refreshing
        public bool _needsRefresh;

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new BattleEngineViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton


        /// <summary>
        /// The Battle Engine
        /// </summary>
        public Engine.BattleEngine Engine = new Engine.BattleEngine();

        /// <summary>
        /// Auto Battle Engine (used for scneario testing)
        /// </summary>
        public Engine.AutoBattleEngine AutoBattleEngine = new Engine.AutoBattleEngine();

        // Hold the Proposed List of Characters for the Battle to Use
        public ObservableCollection<CharacterModel> PartyCharacterList { get; set; } = new ObservableCollection<CharacterModel>();

        // Hold the View Model to the CharacterIndexViewModel
        public CharacterIndexViewModel DatabaseCharacterViewModel = CharacterIndexViewModel.Instance;

        // Have the Database Character List point to the Character View Model List
        public ObservableCollection<CharacterModel> DatabaseCharacterList { get; set; } = CharacterIndexViewModel.Instance.Dataset;

        public bool ZombieApocalypse { get; set; } = false;

        public int ChanceOfRes { get; set; } = 0;
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
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
            ChanceOfRes = input;
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Sets the need to refresh 
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

    }
}