﻿using NUnit.Framework;

using PrimeAssault.Models;
using PrimeAssault.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    public class BasePlayerModelTests
    {
        [Test]
        public void BasePlayerModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new BasePlayerModel<CharacterModel>();

            // Reset

            // Assert
            Assert.AreEqual("This is an Item", result.Name);
        }

        /*
        [Test]
        public void BasePlayerModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new BasePlayerModel<CharacterModel>();

            // Reset

            // Assert
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(PrimeAssault.Services.ItemService.DefaultImageURI, result.ImageURI);
            Assert.AreEqual(PlayerTypeEnum.Unknown, result.PlayerType);
            Assert.AreEqual(true, result.Alive);
            Assert.AreEqual(0, result.Order);
            Assert.AreEqual(result.Id, result.Guid);
            Assert.AreEqual(0, result.ListOrder);
            Assert.AreEqual(0, result.Speed);
            Assert.AreEqual(0, result.Level);
            Assert.AreEqual(0, result.ExperiencePoints);
            Assert.AreEqual(0, result.CurrentHealth);
            Assert.AreEqual(0, result.MaxHealth);
            Assert.AreEqual(0, result.ExperienceTotal);
            Assert.AreEqual(0, result.Defense);
            Assert.AreEqual(0, result.Attack);
            Assert.AreEqual(null, result.Head);
            Assert.AreEqual(null, result.Feet);
            Assert.AreEqual(null, result.Necklass);
            Assert.AreNotEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);
        }
        */

        [Test]
        public void BasePlayerModel_Set_Get_Default_Should_Pass()
        {
            // Arrange
            var result = new BasePlayerModel<CharacterModel>();

            // Act
            result.Id = "bogus";
            result.ImageURI = "uri";
            result.PlayerType = PlayerTypeEnum.Monster;
            result.Alive = false;
            result.Order = 100;
            result.Guid = "guid";
            result.ListOrder = 200;
            result.Speed = 300;
            result.Level = 400;
            result.ExperiencePoints = 500;
            result.CurrentHealth = 600;
            result.MaxHealth = 700;
            result.ExperienceTotal = 800;
            result.Defense = 900;
            result.Attack = 123;
            result.Head = "head";
            result.Feet = "feet";
            result.Necklass = "necklass";
            result.PrimaryHand = "primaryhand";
            result.OffHand = "offhand";
            result.RightFinger ="rightfinger";
            result.LeftFinger = "leftfinger";

            // Reset

            // Assert
            Assert.AreEqual("bogus", result.Id);
            Assert.AreEqual("uri", result.ImageURI);
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
            Assert.AreEqual(false, result.Alive);
            Assert.AreEqual(100, result.Order);
            Assert.AreEqual("guid", result.Guid);
            Assert.AreEqual(200, result.ListOrder);
            Assert.AreEqual(300, result.Speed);
            Assert.AreEqual(400, result.Level);
            Assert.AreEqual(500, result.ExperiencePoints);
            Assert.AreEqual(600, result.CurrentHealth);
            Assert.AreEqual(700, result.MaxHealth);
            Assert.AreEqual(800, result.ExperienceTotal);
            Assert.AreEqual(900, result.Defense);
            Assert.AreEqual(123, result.Attack);
            Assert.AreEqual("head", result.Head);
            Assert.AreEqual("feet", result.Feet);
            Assert.AreEqual("necklass", result.Necklass);
            Assert.AreEqual("primaryhand", result.PrimaryHand);
            Assert.AreEqual("offhand", result.OffHand);
            Assert.AreEqual("rightfinger", result.RightFinger);
            Assert.AreEqual("leftfinger", result.LeftFinger);
        }

        [Test]
        public void BasePlayerModel_Update_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.Update(null);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BasePlayerModel_GetAttack_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetAttack();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void BasePlayerModel_GetDefense_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetDefense();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void BasePlayerModel_GetSpeed_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetSpeed();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void BasePlayerModel_GetHealthCurrent_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetHealthCurrent();

            // Reset

            // Assert
            Assert.AreEqual(21, result);
        }

        [Test]
        public void BasePlayerModel_GetHealthMax_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetHealthMax();

            // Reset

            // Assert
            Assert.AreEqual(21, result);
        }

        [Test]
        public void BasePlayerModel_TakeDamage_Valid_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.AugmentHealth(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BasePlayerModel_TakeDamage_InValid_Should_Fail()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.AugmentHealth(0);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void BasePlayerModel_CauseDeath_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.CauseDeath();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void BasePlayerModel_FormatOutput_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.FormatOutput();

            // Reset

            // Assert
            Assert.AreEqual("A level " + data.Level + " " + data.Name, result);
        }

        [Test]
        public void BasePlayerModel_AddExperience_Default_Should_Fail()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.AddExperience(0);

            // Reset

            // Assert
            Assert.AreNotEqual(true, result);
        }

        [Test]
        public void BasePlayerModel_CalculateExperienceEarned_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.CalculateExperienceEarned(0);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void BasePlayerModel_GetItem_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItem("test");

            // Reset

            // Assert
            Assert.AreEqual(null,result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_Head_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_Feet_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_Necklass_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Necklass);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_PrimaryHand_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.PrimaryHand);

            // Reset

            // Assert
            Assert.AreEqual(null,result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_OffHand_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.OffHand);

            // Reset

            // Assert
            Assert.AreEqual(null,result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_RightFinger_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.RightFinger);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_LeftFinger_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.LeftFinger);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_GetItemByLocation_Unknown_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Unknown);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_DropAllItems_Default_Should_Pass()
        {
            var item = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Arrange
            var data = new BasePlayerModel<CharacterModel>
            {
                Head = item.Id,
                Necklass = item.Id,
                PrimaryHand = item.Id,
                OffHand = item.Id,
                RightFinger = item.Id,
                LeftFinger = item.Id,
                Feet = item.Id,
            };

            // Act
            var result = data.DropAllItems();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BasePlayerModel_AddItem_Unknown_Should_Fail()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();

            // Act
            var result = data.AddItem(ItemLocationEnum.Unknown, "bogus");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_AddItem_Default_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();
            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Act

            // Add the second item, this will return the first item as the one replaced which is null
            var result = data.AddItem(ItemLocationEnum.Head, itemOld.Id);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void BasePlayerModel_AddItem_Default_Replace_Should_Pass()
        {
            // Arrange
            var data = new BasePlayerModel<CharacterModel>();
            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, itemOld.Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.AddItem(ItemLocationEnum.Head, itemNew.Id);

            // Reset

            // Assert
            Assert.AreEqual(itemOld.Id, result.Id);
        }


        [Test]
        public async Task BasePlayerModel_GetItemBonus_Default_Attack_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            PrimeAssault.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id="head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });

            
            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new BasePlayerModel<CharacterModel>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetItemBonus(AttributeEnum.Attack);

            // Reset

            // Assert
            Assert.AreEqual(7654321, result);
        }
    }
}