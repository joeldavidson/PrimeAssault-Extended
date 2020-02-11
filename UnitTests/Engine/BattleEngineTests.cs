using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using System.Linq;

namespace UnitTests.Engine
{
    [TestFixture]
    public class BattleEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void BattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleEngine_StartBattle_AutoModel_True_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.StartBattle(true);

            // Reset

            // Assert
            Assert.AreEqual(true,result);
            Assert.AreEqual(true, Engine.BattleScore.AutoBattle);
        }

        [Test]
        public void BattleEngine_EndBattle_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.EndBattle();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_NewRound_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.NewRound();

            // Reset

            // Assert
            Assert.AreEqual(true,result);
        }

        [Test]
        public void BattleEngine_PopulateCharacterList_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.PopulateCharacterList();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(6, Engine.CharacterList.Count());
        }

        [Test]
        public void BattleEngine_PopulateMonsterList_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.PopulateMonsterList();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(6, Engine.MonsterList.Count());
        }

        #region KillMeTests
        [Test]
        public void BattleEngine_NextTurn_True_Default_Should_Pass()
        {
            // Arrange
            Engine.PopulateCharacterList();
            Engine.NewRound();
            var countBefore = Engine.CharacterList.Count();

            // Act

            var result = Engine.NextTurn(true);
            var countAfter = Engine.CharacterList.Count();

            // Reset

            // Assert
            Assert.AreEqual(BattleEngine.RoundEnum.NextTurn, result);
            Assert.AreEqual(countBefore-1, countAfter);
        }

        [Test]
        public void BattleEngine_NextTurn_True_Loop_Should_Pass()
        {
            // Arrange
            Engine.PopulateCharacterList();
            Engine.NewRound();
            var countBefore = Engine.CharacterList.Count();

            // Remove First 5
            Engine.NextTurn(true);
            Engine.NextTurn(true);
            Engine.NextTurn(true);
            Engine.NextTurn(true);
            Engine.NextTurn(true);

            // Act
            var result = Engine.NextTurn(true);
            var countAfter = Engine.CharacterList.Count();

            // Reset

            // Assert
            Assert.AreEqual(BattleEngine.RoundEnum.GameOver, result);
            Assert.AreEqual(0, countAfter);
        }

        [Test]
        public void BattleEngine_NextTurn_False_Default_Should_Pass()
        {
            // Arrange
            Engine.PopulateCharacterList();
            Engine.PopulateMonsterList();
            var countBefore = Engine.MonsterList.Count();

            // Act

            var result = Engine.NextTurn(false);
            var countAfter = Engine.MonsterList.Count();

            // Reset

            // Assert
            Assert.AreEqual(BattleEngine.RoundEnum.NextTurn, result);
            Assert.AreEqual(countBefore - 1, countAfter);
        }

        [Test]
        public void BattleEngine_NextTurn_False_Loop_Should_Pass()
        {
            // Arrange
            Engine.PopulateCharacterList();
            Engine.PopulateMonsterList();
            var countBefore = Engine.MonsterList.Count();

            // Remove First 5
            Engine.NextTurn(false);
            Engine.NextTurn(false);
            Engine.NextTurn(false);
            Engine.NextTurn(false);
            Engine.NextTurn(false);

            // Act
            var result = Engine.NextTurn(false);
            var countAfter = Engine.MonsterList.Count();

            // Reset

            // Assert
            Assert.AreEqual(BattleEngine.RoundEnum.NewRound, result);
            Assert.AreEqual(0, countAfter);
        }
        #endregion KillMeTests
    }
}