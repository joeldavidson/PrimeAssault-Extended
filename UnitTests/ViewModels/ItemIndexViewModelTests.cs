using NUnit.Framework;
using Game.ViewModels;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.Services;
using System.Threading.Tasks;
using Game.Models;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.ViewModels
{
    public class ItemIndexViewModelTests
    {
        ItemIndexViewModel ViewModel;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            // Activate the Datastore
            ScoreIndexViewModel.Instance.GetCurrentDataSource();
            ItemIndexViewModel.Instance.GetCurrentDataSource();

            ViewModel = ItemIndexViewModel.Instance;
        }

        /// <summary>
        /// Reset the data store
        /// </summary>
        public async Task ResetDataAsync()
        {
            await ViewModel.WipeDataListAsync();
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();
        }

        [Test]
        public async Task ItemIndexViewModel_Read_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = await ViewModel.ReadAsync("bogus");

            // Reset

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ItemIndexViewModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemIndexViewModel_SortDataSet_Default_Should_Pass()
        {
            // Arrange

            // Add items into the list Z ordered
            var dataList = new List<ItemModel>();
            dataList.Add(new ItemModel { Name = "z" });
            dataList.Add(new ItemModel { Name = "m" });
            dataList.Add(new ItemModel { Name = "a" });

            // Act
            var result = ViewModel.SortDataset(dataList);

            // Reset

            // Assert
            Assert.AreEqual("a", result[0].Name);
            Assert.AreEqual("m", result[1].Name);
            Assert.AreEqual("z", result[2].Name);
        }

        [Test]
        public async Task ItemIndexViewModel_CheckIfItemExists_Default_Should_Pass()
        {
            // Arrange

            // Add items into the list Z ordered
            var dataTest = new ItemModel { Name = "test" };
            await ViewModel.CreateAsync(dataTest);

            await ViewModel.CreateAsync(new ItemModel { Name = "z" });
            await ViewModel.CreateAsync(new ItemModel { Name = "m" });
            await ViewModel.CreateAsync(new ItemModel { Name = "a" });

            // Act
            var result = ViewModel.CheckIfItemExists(dataTest);

            // Reset
            await ResetDataAsync();

            // Assert
            Assert.AreEqual(dataTest.Id, result.Id);
        }

        [Test]
        public async Task ItemIndexViewModel_CheckIfItemExists_InValid_Missing_Should_Fail()
        {
            // Arrange

            // Add items into the list Z ordered
            var dataTest = new ItemModel { Name = "test" };
            // Don't add it to the list await ViewModel.CreateAsync(dataTest);

            await ViewModel.CreateAsync(new ItemModel { Name = "z" });
            await ViewModel.CreateAsync(new ItemModel { Name = "m" });
            await ViewModel.CreateAsync(new ItemModel { Name = "a" });

            // Act
            var result = ViewModel.CheckIfItemExists(dataTest);

            // Reset
            await ResetDataAsync();

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task ItemIndexViewModel_Message_Delete_Valid_Should_Pass()
        {
            // Arrange

            // Get the item to delete
            var first = ViewModel.Dataset.FirstOrDefault();

            // Make a Delete Page
            var myPage = new Game.Views.ItemDeletePage(true);

            // Act
            MessagingCenter.Send(myPage, "Delete", first);

            var data = await ViewModel.ReadAsync(first.Id);

            // Reset
            await ResetDataAsync();

            // Assert
            Assert.AreEqual(null, data); // Item is removed
        }

        [Test]
        public async Task ItemIndexViewModel_Message_Create_Valid_Should_Pass()
        {
            // Arrange

            // Make a new Item
            var data = new ItemModel();

            // Make a Delete Page
            var myPage = new Game.Views.ItemCreatePage(true);

            var countBefore = ViewModel.Dataset.Count();

            // Act
            MessagingCenter.Send(myPage, "Create", data);
            var countAfter = ViewModel.Dataset.Count();

            // Reset
            await ResetDataAsync();

            // Assert
            Assert.AreEqual(countBefore + 1, countAfter); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ItemIndexViewModel_Message_Update_Valid_Should_Pass()
        {
            // Arrange

            // Get the item to delete
            var first = ViewModel.Dataset.FirstOrDefault();
            first.Name = "test";

            // Make a Delete Page
            var myPage = new Game.Views.ItemUpdatePage(true);

            // Act
            MessagingCenter.Send(myPage, "Update", first);
            var result = await ViewModel.ReadAsync(first.Id);

            // Reset
            await ResetDataAsync();

            // Assert
            Assert.AreEqual("test", result.Name); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ItemIndexViewModel_Message_SetDataSource_Valid_Should_Pass()
        {
            // Arrange

            // Get the item to delete
            var data = 3000; // Non existing value

            // Make the page Page
            var myPage = new Game.Views.AboutPage(true);

            // Act
            MessagingCenter.Send(myPage, "SetDataSource", data);
            var result = ViewModel.GetCurrentDataSource();

            // Reset
            await ViewModel.SetDataSource(0);
            await ResetDataAsync();

            // Assert
            Assert.AreEqual(0, result); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ItemIndexViewModel_Message_WipeDataList_Valid_Should_Pass()
        {
            // Arrange

            // Make the page Page
            var myPage = new Game.Views.AboutPage(true);

            var data = new ItemModel();
            await ViewModel.CreateAsync(data);

            var countBefore = ViewModel.Dataset.Count();

            // Act
            MessagingCenter.Send(myPage, "WipeDataList", true);
            var countAfter = ViewModel.Dataset.Count();

            // Reset
            await ResetDataAsync();

            // Assert
            Assert.AreEqual(countBefore -1, countAfter); // Count of 0 for the load was skipped
        }
    }
}