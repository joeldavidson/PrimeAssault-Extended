using NUnit.Framework;

using PrimeAssault.Engine;
using PrimeAssault.Models;
using System.Threading.Tasks;
using PrimeAssault.Helpers;
using System.Linq;
using PrimeAssault.ViewModels;

namespace Scenario
{
    [TestFixture]
    public class AutoBattleEngineScenarioTests
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

        //////5 Unique Scenarios//////

        [Test]
        public void AutoBattleEngine_RunAutoBattle_Character_Levels_Should_Pass()
        {
            //Arrange

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 5000,
                                Level = 1,
                                CurrentHealth = 10,
                                ExperienceTotal = 299,
                                ExperiencePoints = 1,
                                Name = "Big Boi",
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayerMike);

            //Act
            Engine.RunAutoBattle();
            bool leveled = (CharacterPlayerMike.Level > 1);

            //Reset

            //Assert
            Assert.IsTrue(leveled);
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
        public async Task AutoBattleEngine_RunAutoBattle_Monsters_1_Should_Pass()
        {
            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayerMike);


            // Add Monsters

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            Engine.MaxNumberPartyMonsters = 1;

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Character_Level_Up_Should_Pass()
        {

            /* 
             * Test to force leveling up of a character during the battle
             * 
             * 1 Character, Experience set at next level mark
             * 
             * 6 Monsters
             * 
             * Character Should Level UP 1 level
             * 
             */

            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 1;

            CharacterIndexViewModel.Instance.Dataset.Clear();

            // To See Level UP happening, a character needs to be close to the next level
            var Character = new CharacterModel
            {
                ExperienceTotal = 300,    // Enough for next level
                Name = "Mike Level Example",
                Speed = 100,    // Go first
            };

            // Remember Start Level
            var StartLevel = Character.Level;

            var CharacterPlayer = new PlayerInfoModel(Character);

            Engine.CharacterList.Add(CharacterPlayer);


            // Add Monsters

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, Engine.BattleScore.CharacterAtDeathList.Contains("Mike Level Example"));
           // Assert.AreEqual(StartLevel+1, Engine.BattleScore.CharacterModelDeathList.Where(m=>m.Guid.Equals(Character.Guid)).First().Level);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_GameOver_Round_1_Should_Pass()
        {
            /* 
             * 
             * 1 Character, Speed slowest, only 1 HP
             * 
             * 6 Monsters
             * 
             * Should end in the first round
             * 
             */

            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 10,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayer);


            // Add Monsters

            Engine.MaxNumberPartyMonsters = 6;

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }


        //[Test]
        //public async Task AutoBattleEngine_RunAutoBattle_GameOver_Round_2_Should_Pass()
        //{
        //    /* 
        //     * 
        //     * 2 Character, Speed slowest, only 1 HP each
        //     * 
        //     * 2 Monsters
        //     * 
        //     * Should end in the first round
        //     * 
        //     */

        //    //Arrange

        //    // Add Characters

        //    Engine.MaxNumberPartyCharacters = 2;

        //    var CharacterPlayerMike = new PlayerInfoModel(
        //                    new CharacterModel
        //                    {
        //                        Speed = -1, // Will go last...
        //                        Level = 10,
        //                        CurrentHealth = 1,
        //                        ExperienceTotal = 1,
        //                        ExperienceRemaining = 1,
        //                        Name = "Mike",
        //                    });

        //    Engine.CharacterList.Add(CharacterPlayerMike);
        //    Engine.CharacterList.Add(CharacterPlayerMike);


        //    // Add Monsters

        //    Engine.MaxNumberPartyMonsters = 2;

        //    var MonsterPlayer = new PlayerInfoModel(
        //        new MonsterModel
        //        {
        //            Speed = 100, // Will go first...
        //            Level = 10,
        //            CurrentHealth = 1,
        //            ExperienceTotal = 1,
        //            ExperienceRemaining = 1,
        //        });

        //    Engine.MonsterList.Add(MonsterPlayer);
        //    Engine.MonsterList.Add(MonsterPlayer);

        //    //Act
        //    var result = await Engine.RunAutoBattle();

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(true, result);
        //}
    }
}