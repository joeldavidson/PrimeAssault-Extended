﻿using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;

namespace UnitTests.Engine
{
    [TestFixture]
    public class AutoBattleEngineTests
    {
        AutoBattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new AutoBattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_RunAutoBattle_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.RunAutoBattle();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_GetScoreObject_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.GetScoreObject();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}