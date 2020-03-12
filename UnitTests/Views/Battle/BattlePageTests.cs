using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrimeAssault;
using PrimeAssault.Views;
using PrimeAssault.ViewModels;
using PrimeAssault.Models;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;

namespace UnitTests.Views
{
    [TestFixture]
    public class BattlePageTests
    {
        App app;
        BattlePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new BattlePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void BattlePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattlePage_AttackButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AttackButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_RoundOverButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.RoundOverButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_NewRoundButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.NewRoundButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_BattleOverButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.BattleOverButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ExitButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ExitButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_QuitButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.QuitButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////HACKATHONS UNIT TESTS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Hack 04
        [Test]
        public void HACK_4_PotionButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.PotionButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 04
        [Test]
        public void HACK_4_Potion_Not_Given_To_Unit_Should_Pass()
        {
            // Arrange
            PlayerInfoModel character = new PlayerInfoModel();

            int health = character.CurrentHealth - 1;

            page.currentCharacter = character;

            // Act
            //page.PotionButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.AreNotEqual(page.currentCharacter.CurrentHealth, page.currentCharacter.MaxHealth); // Got to here, so it happened...
        }

        //Hack 04
        [Test]
        public void HACK_4_Potion_Given_To_Unit_Should_Pass()
        {
            // Arrange
            PlayerInfoModel character = new PlayerInfoModel();

            int health = character.CurrentHealth - 1;

            page.currentCharacter = character;

            // Act
            page.PotionButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.AreEqual(page.currentCharacter.CurrentHealth, page.currentCharacter.MaxHealth); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_UnitDies_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.UnitDies(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_UnitAttacks_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.UnitAttacks(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_UnitGetsHit_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.UnitGetsHit(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_rotateHit_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.rotateHit(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_rotateGetsHit_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.rotateGetsHit(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}