﻿using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;
using PrimeAssault.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PrimeAssault.Services
{
    /// <summary>
    /// Database Services
    /// Will write to the local data store
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DatabaseService<T> : IDataStore<T> where T : new()
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile DatabaseService<T> instance;
        private static readonly object syncRoot = new Object();

        public static DatabaseService<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseService<T>();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        /// <summary>
        /// Set the class to load on demand
        /// Saves app boot time
        /// </summary>
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return GetDataConnection();
        });

        /// <summary>
        /// Connection method for sql
        /// </summary>
        public static SQLiteAsyncConnection GetDataConnection()
        {
            if (TestMode)
            {
                return new SQLiteAsyncConnection(":memory:", Constants.Flags);
            }

            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        /// <summary>
        /// Bool for testing
        /// </summary>
        public static bool TestMode = false;
        /// <summary>
        /// Force exception on number set to default of -1
        /// </summary>
        public int ForceExceptionOnNumber = -1;

        // Lazy Connection
        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        // Track if Initialized or Not
        public static bool initialized = false;

        // Set Needs Init to False, so toggles to true 
        public bool NeedsInitialization = true;

        // Semaphore to track transactions
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(initialCount: 1);

        /// <summary>
        /// Constructor
        /// All the database to start up
        /// </summary>
        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        /// <summary>
        /// Create the Table if it does not exist
        /// </summary>
        /// <returns></returns>
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                initialized = true;

                // Check if the Data Table Already exists
                if (Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
                {
                    return;
                }

                await Database.CreateTablesAsync(CreateFlags.None, typeof(T));
            }
        }

        /// <summary>
        /// First time toggled, returns true.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetNeedsInitializationAsync()
        {
            if (NeedsInitialization == true)
            {
                // Toggle State
                NeedsInitialization = false;
                return await Task.FromResult(true);
            }

            return await Task.FromResult(NeedsInitialization);
        }

        /// <summary>
        /// Wipe Data List
        /// Drop the tables and create new ones
        /// </summary>
        public async Task<bool> WipeDataListAsync()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                GetForceExceptionCount();

                NeedsInitialization = true;

                await Database.DropTableAsync<T>();
                await Database.CreateTablesAsync(CreateFlags.None, typeof(T));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error WipeData" + e.Message);
                return await Task.FromResult(false);
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Create a new record for the data passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            try
            {
                GetForceExceptionCount();

                var result = await Database.InsertAsync(data);
                return (result == 1);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Create Failed " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Return the record for the ID passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> ReadAsync(string id)
        {
            T data;

            if (string.IsNullOrEmpty(id))
            {
                return default(T);
            }

            try
            {
                GetForceExceptionCount();

                var dataList = await IndexAsync();

                data = dataList.Where((T arg) => ((BaseModel<T>)(object)arg).Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Read Failed " + e.Message);
                data = default(T);
            }

            return data;
        }

        /// <summary>
        /// Update the record passed in if it exists
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            var myRead = await ReadAsync(((BaseModel<T>)(object)data).Id);
            if (myRead == null)
            {
                return false;
            }

            int result = 0;
            try
            {
                GetForceExceptionCount();

                result = await Database.UpdateAsync(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Create Failed " + e.Message);
                return await Task.FromResult(false);
            }

            return (result == 1);
        }

        /// <summary>
        /// Delete the record of the ID passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            var data = await ReadAsync(id);
            if (data == null)
            {
                return false;
            }

            int result;
            try
            {
                GetForceExceptionCount();

                result = await Database.DeleteAsync(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Delete Failed " + e.Message);
                return false;
            }

            return (result == 1);
        }

        /// <summary>
        /// Return all records in the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> IndexAsync()
        {
            List<T> result;
            try
            {
                GetForceExceptionCount();

                result = await Database.Table<T>().ToListAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Create Failed " + e.Message);
                return null;
            }

            return result;
        }

        /// <summary>
        /// Keeps track of the Forced execption Count
        /// </summary>
        /// <returns></returns>
        public int GetForceExceptionCount()
        {
            if (ForceExceptionOnNumber > 0)
            {
                if (ForceExceptionOnNumber == 1)
                {
                    throw new NotImplementedException();
                }

                ForceExceptionOnNumber--;
            }

            return ForceExceptionOnNumber;
        }
    }
}