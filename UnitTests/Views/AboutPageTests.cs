using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class AboutPageTests
    {
        [Test]
        public void AboutPage_Constructor_Default_Should_Pass()
        {
            // Arrange
            // Initilize Xamarin Forms
            MockForms.Init();

            var app = new App();

            // Act
            var result = new AboutPage();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
