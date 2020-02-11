using NUnit.Framework;

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
        public async Task AutoBattleEngine_RunAutoBattle_Default_CharactersDie_Should_Pass()
        {
            // Arrange

            // Act
            Engine.FlagWhoDies = true;
            var result = await Engine.RunAutoBattle();

            // Reset

            // Assert
            Assert.AreEqual(true,result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Default_MonstersDie_Should_Pass()
        {
            // Arrange

            // Act
            Engine.FlagWhoDies = false;
            var result = await Engine.RunAutoBattle();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AutoBattleEngine_GetScoreValue_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.GetScoreValue();

            // Reset

            // Assert
            Assert.AreEqual(0, result);
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

        [Test]
        public void AutoBattleEngine_GetRoundsValue_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.GetRoundsValue();

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}