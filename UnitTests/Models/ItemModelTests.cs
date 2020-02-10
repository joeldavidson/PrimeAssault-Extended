using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class ItemModelTests
    {
        [Test]
        public void ItemModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemModel();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemModel();

            // Reset

            // Assert 
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void ItemModel_Set_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemModel();
            result.Value = 6;

            // Reset

            // Assert 
            Assert.AreEqual(6,result.Value);
        }

        [Test]
        public void ItemModel_Update_Default_Should_Pass()
        {
            // Arrange
            var dataOriginal = new ItemModel();
            dataOriginal.Value = 1;
            
            var dataNew = new ItemModel();
            dataNew.Value = 2;

            // Act
            var result = dataOriginal.Update(dataNew);

            // Reset

            // Assert 
            Assert.AreEqual(2, dataOriginal.Value);
        }
    }
}