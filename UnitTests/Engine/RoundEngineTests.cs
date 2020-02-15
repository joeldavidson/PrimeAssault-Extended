using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Game.Services;
using Game.ViewModels;

namespace UnitTests.Engine
{
    [TestFixture]
    public class RoundEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
            Engine.StartBattle(false);   // Clear the Engine
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void RoundEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Speed_Higher_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 20,
                CurrentHealth = 100,
                ExperiencePoints = 1000,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 1,
                Level = 1,
                CurrentHealth = 2,
                ExperiencePoints = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear(); 
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Level_Higher_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 20,
                CurrentHealth = 100,
                ExperiencePoints = 1000,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperiencePoints = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Experience_Higher_Should_Pass()
        {
            // Arrange

            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 100,
                ExperiencePoints = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperiencePoints = 1,
                Name = "C",
                ListOrder = 10,
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_ListOrder_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperiencePoints = 1,
                Name = "A",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperiencePoints = 1,
                Name = "A",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Name_A_Z_Should_Pass()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperiencePoints = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperiencePoints = 1,
                Name = "ZZ",
                ListOrder = 10
            };

            CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }
    }
}