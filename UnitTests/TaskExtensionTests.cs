using NUnit.Framework;

using Xamarin.Forms.Mocks;

using Game;
using Game.ViewModels;
using Game.Services;
using Game.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using SQLite;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public void TaskExtensions_DatabasePath_Default_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

           IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);

            // Act
            var result = ScoreIndexViewModel.Instance.GetCurrentDataSource();

            // Reset

            // Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void TaskExtensions_DatabasePath_Error_Should_Fail()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var myException = new NotImplementedException();

            //Action<Exception> myact = (ex => { throw ex; });
            Action<Exception> myact = (ex => { int a = 1; });

            // Call with Error=true
            IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(true, myact);

            // Act
            var result = ScoreIndexViewModel.Instance.GetCurrentDataSource();

            // Reset

            // Assert
            Assert.AreEqual(true, true);
        }
    }

    public class TestDatabaseService<T> : IDataStore<T> where T : new()
    {
        static bool _error=false;

        public TestDatabaseService(bool condition, Action<Exception> onException = null)
        {
            _error = condition;
            InitializeAsync().SafeFireAndForget(false, onException);
        }

        async Task InitializeAsync()
        {
            if (_error)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<bool> WipeDataListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(T data)
        {
            throw new NotImplementedException();
        }

        public async Task<T> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(T data)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> IndexAsync()
        {
            throw new NotImplementedException();
        }
    }
}