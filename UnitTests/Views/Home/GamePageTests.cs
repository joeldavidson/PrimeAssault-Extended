﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrimeAssault;
using PrimeAssault.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;

namespace UnitTests.Views
{
    [TestFixture]
    public class PrimeAssaultPageTests
    {
        App app;
        PrimeAssaultPage page;

        // Base Constructor
        //public PrimeAssaultPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new PrimeAssaultPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void PrimeAssaultPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PrimeAssaultPage_DungeonButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.DungeonButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PrimeAssaultPage_VillageButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.SafeHouseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PrimeAssaultPage_AutobattleButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AutobattleButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}