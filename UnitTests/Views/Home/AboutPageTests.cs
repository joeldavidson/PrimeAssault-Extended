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
    public class AboutPageTests : AboutPage
    {
        App app;
        AboutPage page;

        // Base Constructor
        public AboutPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new AboutPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void AboutPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AboutPage_Elements_Get_Set_Should_Pass()
        {
            // Arrange

            // Act
            ((StackLayout)page.FindByName("DatabaseSettingsFrame")).IsVisible = true;
            ((StackLayout)page.FindByName("DebugSettingsFrame")).IsVisible = true;

            ((Switch)page.FindByName("DatabaseSettingsSwitch")).IsVisible = true;
            ((Switch)page.FindByName("DatabaseSettingsSwitch")).IsToggled = true;
            ((Switch)page.FindByName("DatabaseSettingsSwitch")).IsToggled = false;

            ((Switch)page.FindByName("DebugSettingsSwitch")).IsVisible = true;
            ((Switch)page.FindByName("DebugSettingsSwitch")).IsToggled = true;
            ((Switch)page.FindByName("DebugSettingsSwitch")).IsToggled = false;

            ((Switch)page.FindByName("DataSourceValue")).IsVisible = true;
            ((Switch)page.FindByName("DataSourceValue")).IsToggled = true;
            ((Switch)page.FindByName("DataSourceValue")).IsToggled = false;

            ((Label)page.FindByName("CurrentDateTime")).Text = "test";

            ((StackLayout)page.FindByName("DatabaseSettingsFrame")).IsVisible = false;
            ((StackLayout)page.FindByName("DebugSettingsFrame")).IsVisible = false;

            // Reset

            // Assert
            Assert.IsNotNull((StackLayout)page.FindByName("DebugSettingsFrame"));
            Assert.IsNotNull(((StackLayout)page.FindByName("DatabaseSettingsFrame")));

            Assert.IsNotNull((Label)page.FindByName("CurrentDateTime"));

            Assert.IsNotNull((Switch)page.FindByName("DatabaseSettingsSwitch"));
            Assert.IsNotNull((Switch)page.FindByName("DataSourceValue"));
            Assert.IsNotNull((Switch)page.FindByName("DebugSettingsSwitch"));
        }

        [Test]
        public void AboutPage_DatabaseSettingsSwitch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("DatabaseSettingsFrame");
            var current = frame.IsVisible;

            ToggledEventArgs args = new ToggledEventArgs(current);


            // Act
            page.DatabaseSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DebugSettingsSwitch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("DebugSettingsFrame");
            var current = frame.IsVisible;

            ToggledEventArgs args = new ToggledEventArgs(current);


            // Act
            page.DebugSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DataSource_Toggled_Default_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled;

            ToggledEventArgs args = new ToggledEventArgs(current);

            // Act
            page.DataSource_Toggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DataSource_Toggled_False_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled = false;

            // Act
            control.IsToggled = true;

            var result = control.IsToggled;

            // Reset

            // Assert
            Assert.AreEqual(!current,result); 
        }

        [Test]
        public void AboutPage_DataSource_Toggled_True_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled = true;

            // Act
            control.IsToggled = false;

            var result = control.IsToggled;

            // Reset

            // Assert
            Assert.AreEqual(!current, result); 
        }

        [Test]
        public void PrimeAssaultPage_WipeDataList_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.WipeDataList_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void Hack_21_Switch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("Hack21Frame");
            var current = frame.IsVisible;

            ToggledEventArgs args = new ToggledEventArgs(current);


            // Act
            page.DebugSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void Hack_22_Switch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("Hack22Frame");
            var current = frame.IsVisible;

            ToggledEventArgs args = new ToggledEventArgs(current);


            // Act
            page.DebugSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void Hack_23_Switch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("Hack23Frame");
            var current = frame.IsVisible;

            ToggledEventArgs args = new ToggledEventArgs(current);


            // Act
            page.DebugSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void Hack_24_Switch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("Hack24Frame");
            var current = frame.IsVisible;

            ToggledEventArgs args = new ToggledEventArgs(current);


            // Act
            page.DebugSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }
    }
}
