using System.Collections.Generic;

namespace PrimeAssault.Models
{
    /// <summary>
    /// Helper to manage the Level Table Data
    /// </summary>
    class LevelTableHelper
    {
        #region Singleton
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static LevelTableHelper _instance;

        public static LevelTableHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelTableHelper();
                }
                return _instance;
            }
        }

        #endregion Singleton

        // Level Max
        public const int MaxLevel = 20;

        // List of all the levels
        public List<LevelDetailsModel> LevelDetailsList { get; set; }

        /// <summary>
        /// Constructor calls to clear the data
        /// </summary>
        public LevelTableHelper()
        {
            ClearAndLoadDatTable();
        }

        /// <summary>
        /// Clear the data and relaod it
        /// </summary>
        public void ClearAndLoadDatTable()
        {
            LevelDetailsList = new List<LevelDetailsModel>();
            LoadLevelData();
        }

        /// <summary>
        /// Load the data for each level
        /// </summary>
        public void LoadLevelData()
        {
            // Init the level list, going to index into it like an array, so making 0 be a null value.  That way Level can be Array Index.
            LevelDetailsList.Add(new LevelDetailsModel(0,0, 0, 0, 0, 0, 0));

            // Character Level Chart...

            // Sequence is Level,Experience,Attack,Ranged Defense,Speed

            LevelDetailsList.Add(new LevelDetailsModel(1, 0, 1, 1, 1, 1, 1));
            LevelDetailsList.Add(new LevelDetailsModel(2, 300, 2, 1, 2, 2, 1));
            LevelDetailsList.Add(new LevelDetailsModel(3, 900, 3, 2, 3, 3, 1));
            LevelDetailsList.Add(new LevelDetailsModel(4, 2700, 4, 2, 3, 3, 1));
            LevelDetailsList.Add(new LevelDetailsModel(5, 6500, 5, 2, 4, 4, 2));
            LevelDetailsList.Add(new LevelDetailsModel(6, 14000, 6, 3, 4, 4, 2));
            LevelDetailsList.Add(new LevelDetailsModel(7, 23000, 7, 3, 5, 5, 2));
            LevelDetailsList.Add(new LevelDetailsModel(8, 34000, 8, 3, 5, 5, 2));
            LevelDetailsList.Add(new LevelDetailsModel(9, 48000, 9, 3, 5, 5, 2));
            LevelDetailsList.Add(new LevelDetailsModel(10, 64000, 10, 4, 6, 6, 3));
            LevelDetailsList.Add(new LevelDetailsModel(11, 85000, 4, 11, 6, 6, 3));
            LevelDetailsList.Add(new LevelDetailsModel(12, 100000, 4, 12, 6, 6, 3));
            LevelDetailsList.Add(new LevelDetailsModel(13, 120000, 4, 13, 7, 7, 3));
            LevelDetailsList.Add(new LevelDetailsModel(14, 140000, 5, 14, 7, 7, 3));
            LevelDetailsList.Add(new LevelDetailsModel(15, 165000, 5, 15, 7, 7, 4));
            LevelDetailsList.Add(new LevelDetailsModel(16, 195000, 5, 16, 8, 8, 4));
            LevelDetailsList.Add(new LevelDetailsModel(17, 225000, 5, 17, 8, 8, 4));
            LevelDetailsList.Add(new LevelDetailsModel(18, 265000, 6, 18, 8, 8, 4));
            LevelDetailsList.Add(new LevelDetailsModel(19, 305000, 7, 19, 9, 9, 4));
            LevelDetailsList.Add(new LevelDetailsModel(20, 355000, 8, 20, 10, 10, 5));

            // Level 21, is for Monster Experience points...
            LevelDetailsList.Add(new LevelDetailsModel(21, 400000, 0, 0, 0, 0, 0));
        }
    }
}