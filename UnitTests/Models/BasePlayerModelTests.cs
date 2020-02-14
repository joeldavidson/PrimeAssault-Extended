using NUnit.Framework;

using Game.Models;

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

        [Test]
        public void BasePlayerModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new BasePlayerModel<CharacterModel>();

            // Reset

            // Assert
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(Game.Services.ItemService.DefaultImageURI, result.ImageURI);
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
            Assert.AreEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);
        }

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
    }
}