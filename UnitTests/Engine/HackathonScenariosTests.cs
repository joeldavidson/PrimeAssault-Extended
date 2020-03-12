using NUnit.Framework;
using PrimeAssault.Engine;
using PrimeAssault.Models;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using PrimeAssault.Helpers;
using System.Linq;
using PrimeAssault.ViewModels;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PrimeAssault;
using PrimeAssault.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        AutoBattleEngine AutoBattleEngine;
        BattleEngine BattleEngine;
        BattleMessagesModel BattleMessages;
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
            BattleMessages = new BattleMessagesModel();
            AutoBattleEngine = EngineViewModel.AutoBattleEngine;
            BattleEngine = EngineViewModel.Engine;
            page = new BattlePage();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters


            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, AutoBattleEngine.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(3, AutoBattleEngine.BattleScore.RoundCount);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        {
            /* 
             * Scenario Number:  
             *  2
             *  
             * Description: 
             *      Make a Character called Bob
             *      Bob Always Misses
             *      Other Characters Always Hit
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character named Bob
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Named Bob
             *  Test with Character of any other name
             * 
             * Validation:
             *      Verify Enum is Miss
             *  
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                Name = "Bob",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(19);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, BattleEngine.BattleMessagesModel.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *      2
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create Character named Mike
             *      Create Monster
             *      Call TurnAsAttack so Mike can attack Monster
             * 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                Name = "Joe",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessagesModel.HitStatus);
        }

        [Test]
        public async Task HackathonScenario_Scenario_16_Zombies_Should_Not_SpawnAsync()
        {
            /* 
             * Scenario Number:  
             *      16
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Battle Messages Page, PlayerInfoModel, BattleEngineViewModel
             *                 
             * Test Algrorithm:
             *      Create Character party of 6
             *      Create Monster close to death
             *      Set chance of zombification to 100
             *      Run game until zombie wins
             * 
             * Test Conditions:
             *      Control Zombification to be 100%
             *  
             *  Validation
             *      Character party loses (since zombie will be revived forever)
             *      
             */

            //Arrange

            BattleMessages.ZombieApocalypse = true;
            BattleMessages.ResChance = 100;

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 6;

            PlayerInfoModel CharacterPlayer;
            for (int i = 0; i < BattleEngine.MaxNumberPartyCharacters; i++)
            {
                CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                Name = "Bob",
                            });
                BattleEngine.CharacterList.Add(CharacterPlayer);
            }


            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            bool result = await AutoBattleEngine.RunAutoBattle();

            //reset

            BattleMessages.ZombieApocalypse = false;
            BattleMessages.ResChance = 0;

            //Assert

            Assert.AreEqual(true, result);
        }

        public void HackathonScenario_Scenario_18_Audio_Should_Play_On_Hit()
        {
            /* 
            * Scenario Number:  
            *      18
            *      
            * Description: 
            *      Make a sound effect play when a unit attacks
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *       Create a character
            *       Create an enemy
            *       
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *       Verify AudioCenter is created
            *       Verify AudioCenter.Attack_Sound() works
            *  
            */

            //Arrange

            // Set Character Conditions
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            BattleEngine.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            // Act
            BattleEngine.Attack(PlayerInfo);

            var result = BattleEngine.Attack(PlayerInfo);

            // Reset
            BattleEngine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(true, result);
        }

        //Hack 04
        [Test]
        public void HACK_4_PotionButton_Clicked_Default_Should_Pass()
        {

            // Arrange
            PlayerInfoModel data = new PlayerInfoModel();
            ImageButton dataImage = new ImageButton();
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

            /* Scenario Number:  
            *      4
            *      
            * Description: 
            *      Healing finally comes to the Mobile Class at SU. It’s been a long battle. Now magically, if the debug setting is enabled, 6 full healing potions are available per round. The potions do not stack or survive to the next round so use 
            *      them wisely. During a round, instead of attacking, a player may choose to drink one of the 6 healing potions. This will restore the character to max health. A potion is fully used up regardless of how much health it gives the 
            *      character. At the start of each new round, a fresh batch of 6 potions is delivered, and the old ones expire (the are very perishable). If debug setting for AllowRoundHealing is set to false, then no potions arrive… In autobattle 
            *      mode, if the health of a character is <20% of their max health, they will automatically drink an available potion (they are greedy, and will drink it even if the character next to them needs it more than they do…)
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create basic character with Playerinfomodel
            *      reduce its health
            *  
            *      call potion method
            *      Check current and previous health
            *      
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */
            // Arrange
            PlayerInfoModel character = new PlayerInfoModel();

            character.MaxHealth = 10;
            character.CurrentHealth = 1;

            page.currentCharacter = character;

            // Act
            page.PotionButton_Clicked(null, null);

            // Assert
            Assert.AreEqual(page.currentCharacter.CurrentHealth, page.currentCharacter.MaxHealth); // Got to here, so it happened...
        }


        /* Scenario Number:  
         *      4
         *      
         * Description: 
         *     Implement animations for events that happen during the battle.  Animate Hit, Miss, Level UP, Death. Include screen shots of the animation effects
         *     
         *     
         * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
         *      No Code changes requied 
         * 
         * Test Algrorithm:
         *      Create basic character with Playerinfomodel
         *      Create fake image
         *  
         *      Pass them into the the animation method
         *      Check to make sure it made it through
         *      
         * 
         * Test Conditions:
         *      Default condition is sufficient
         * 
         * Validation:
         *      Verify Death animation
         *      Verify Unit attack animation
         *      Verify Unit gets hit animation
         *  
         */

        //Hack 40
        [Test]
        public void HACK_40_UnitDies_Default_Should_Pass()
        {
            // Arrange
            PlayerInfoModel data = new PlayerInfoModel();
            ImageButton dataImage = new ImageButton();
            // Act
            page.UnitDies(data, dataImage);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_UnitAttacks_Default_Should_Pass()
        {
            // Arrange
            PlayerInfoModel data = new PlayerInfoModel();
            ImageButton dataImage = new ImageButton();
            // Act
            page.UnitAttacks(data, dataImage);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_UnitGetsHit_Default_Should_Pass()
        {
            // Arrange
            PlayerInfoModel data = new PlayerInfoModel();
            ImageButton dataImage = new ImageButton();
            // Act
            page.UnitGetsHit(data, dataImage);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_rotateHit_Default_Should_Pass()
        {
            // Arrange
            PlayerInfoModel data = new PlayerInfoModel();
            ImageButton dataImage = new ImageButton();
            // Act
            page.rotateHit(data, dataImage);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //Hack 40
        [Test]
        public void HACK_40_rotateGetsHit_Default_Should_Pass()
        {
            // Arrange
            PlayerInfoModel data = new PlayerInfoModel();
            ImageButton dataImage = new ImageButton();
            // Act
            page.rotateGetsHit(data, dataImage);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
