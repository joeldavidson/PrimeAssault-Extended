﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
        {
            MessagingCenter.Subscribe<AboutPage, int>(this, "Zombification", async (obj, data) =>
            {
                SetZombieApocalypse(data);
            });

            #endregion Constructor
        }

        public bool SetZombieApocalypse(int input)
        {
            if (input == 0)
            {
                ZombieApocalypse = false;
            }
            else
            {
                ZombieApocalypse = true;
            }
            return ZombieApocalypse;
        }

    }
}