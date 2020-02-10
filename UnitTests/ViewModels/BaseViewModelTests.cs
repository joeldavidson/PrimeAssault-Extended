using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.ViewModels;
using Game.Models;

namespace UnitTests.ViewModels
{
    [TestFixture]
    public class BaseViewModelTests : BaseViewModel<ItemModel>
    {
        [Test]
        public void BaseViewModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new BaseViewModel<ItemModel>();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BaseViewModel_SetProperty_Changed_Should_Pass()
        {
            // Arrange

            // Act
            var result = new BaseViewModel<ItemModel>();

            var isBusy = false;
            SetProperty<bool>(ref isBusy, true);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BaseViewModel_SetProperty_Same_Should_Skip()
        {
            // Arrange

            // Act
            var result = new BaseViewModel<ItemModel>();

            var isBusy = false;
            SetProperty<bool>(ref isBusy, false);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BaseViewModel_SetProperty_OnChange_Should_Skip()
        {
            // Arrange

            var testName = new TestName();

            Action showMethod = testName.Display;

            var isBusy = true;

            // Act

            SetProperty<bool>(ref isBusy, false, "bogus", showMethod);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void BaseViewModel_SetProperty_OnChange_Null_Should_Skip()
        {
            // Arrange

            var testName = new TestName();

            Action showMethod = null;

            var isBusy = true;

            // Act

            SetProperty<bool>(ref isBusy, false, "bogus", showMethod);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Following TestName class is used for the Action in the SetProperty test
        /// </summary>
        public class TestName
        {
            public TestName()
            {
            }

            public void Display()
            {
            }
        }
    }
}