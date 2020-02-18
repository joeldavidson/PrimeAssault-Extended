using PrimeAssault.Models;
using PrimeAssault.Views;
using System;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using PrimeAssault.Services;

namespace PrimeAssault.ViewModels
{

    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class MonIndexViewModel : BaseViewModel<MonsterModel>
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile MonIndexViewModel instance;
        private static readonly object syncRoot = new Object();

        public static MonIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MonIndexViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public MonIndexViewModel()
        {
            Title = "Monsters";

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<MonCreatePage, MonsterModel>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data as MonsterModel);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<MonUpdatePage, MonsterModel>(this, "Update", async (obj, data) =>
            {
                // Have the item update itself
                data.Update(data);

                await UpdateAsync(data as MonsterModel);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<MonDeletePage, MonsterModel>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data as MonsterModel);
            });

            // Register the Set Data Source Message
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", async (obj, data) =>
            {
                await SetDataSource(data);
            });

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<AboutPage, bool>(this, "WipeDataList", async (obj, data) =>
            {
                await WipeDataListAsync();
            });

            #endregion Messages
        }

        #endregion Constructor

        #region DataOperations_CRUDi

        /// <summary>
        /// Returns the item passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override MonsterModel CheckIfExists(MonsterModel data)
        {
            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item...

            var myList = Dataset.Where(a =>
                                        a.Name == data.Name &&
                                        a.Description == data.Description &&
                                        a.Attack == data.Attack &&
                                        a.AttackMult == data.AttackMult &&
                                        a.Defense == data.Defense &&
                                        a.DefenseMult == data.DefenseMult &&
                                        a.RangedDefense == data.RangedDefense&&
                                        a.RangedDefenseMult == data.RangedDefenseMult &&
                                        a.Speed == data.Speed &&
                                        a.SpeedMult == data.SpeedMult &&
                                        a.Ability == data.Ability
                                        )
                                        .FirstOrDefault();

            if (myList == null)
            {
                // it's not a match, return false;
                return null;
            }

            return myList;
        }

        /// <summary>
        /// Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override List<MonsterModel> GetDefaultData()
        {
            return DefaultData.LoadData(new MonsterModel());
        }

        #endregion DataOperations_CRUDi

        #region SortDataSet

        /// <summary>
        /// The Sort Order for the ItemModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<MonsterModel> SortDataset(List<MonsterModel> dataset)
        {
            return dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ToList();
        }

        #endregion SortDataSet

        /// <summary>
        /// Takes an item string ID and looks it up and returns the item
        /// This is because the Items on a character are stores as strings of the GUID.  That way it can be saved to the DB.
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public MonsterModel GetMonster(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Item myData = DataStore.GetAsync_Item(ItemID).GetAwaiter().GetResult();
            MonsterModel myData = Dataset.Where(a => a.Id.Equals(id)).FirstOrDefault();
            if (myData == null)
            {
                return null;
            }

            return myData;
        }
    }
}
