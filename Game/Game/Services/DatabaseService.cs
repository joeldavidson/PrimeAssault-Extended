using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;
using Game.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game.Services
{
    /// <summary>
    /// Database Services
    /// Will write to the local data store
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DatabaseService<T> : IDataStore<T> where T : new()
    {
        /// <summary>
        /// Set the class to load on demand
        /// Saves app boot time
        /// </summary>
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

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
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(T)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        // Set Needs Init to False, so toggles to true 
        public bool NeedsInitialization = true;

        /// <summary>
        /// First time toggled, returns true.
        /// </summary>
        /// <returns></returns>
        public bool GetNeedsInitializationAsync()
        {
            if (NeedsInitialization == true)
            {
                // Toggle State
                NeedsInitialization = false;
                return true;
            }

            return NeedsInitialization;
        }

        /// <summary>
        /// Wipe Data List
        /// Drop the tables and create new ones
        /// </summary>
        public async Task<bool> WipeDataListAsync()
        {
            try
            {
                NeedsInitialization = true;

                await Database.DropTableAsync<T>().ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(T)).ConfigureAwait(false); 
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error WipeData" + e.Message);
            }

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new record for the data passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(T data)
        {
            try
            {
                var result = await Database.InsertAsync(data).ConfigureAwait(false);
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

            try
            {
                var dataList = await IndexAsync().ConfigureAwait(false);

                data = dataList.Where((T arg) => ((BaseModel<T>)(object)arg).Id.Equals(id)).FirstOrDefault();

                //data = await Database.Table<T>().Where((T arg) => ((BaseModel<T>)(object)arg).Id.Equals(id)).FirstOrDefaultAsync();
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
            var myRead = await ReadAsync(((BaseModel<T>)(object)data).Id).ConfigureAwait(false); 
            if (myRead == null)
            {
                return false;
            }

            int result = 0;
            try
            {
                result = await Database.UpdateAsync(data).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Create Failed " + e.Message);
                return (result == 0);
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
            var data = await ReadAsync(id).ConfigureAwait(false);
            if (data == null)
            {
                return false;
            }

            int result;
            try
            {
                result = await Database.DeleteAsync(data).ConfigureAwait(false);
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
                result = await Database.Table<T>().ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Create Failed " + e.Message);
                return null;
            }

            return result;
        }
    }
}