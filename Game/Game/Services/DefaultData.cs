using PrimeAssault.Models;
using PrimeAssault.ViewModels;
using System.Collections.Generic;

namespace PrimeAssault.Services
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Evil Lazer",
                    Description = "Totally evil, totally bad.",
                    Range = 1,
                    Damage = 9,
                    attackValue = 2,
                    defenseValue = 2,
                    rangedDefenseValue = 2,
                    speedValue = 2,
                    attackMult = 2,
                    defenseMult = 2,
                    rangedDefenseMult = 2,
                    speedMult = 3,
                    ImageURI = "raygun.png",
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Monster Masher",
                    Description = "Its a graveyard smash!",
                    Range = 0,
                    Damage = 2,
                    attackValue = 4,
                    defenseValue = 0,
                    rangedDefenseValue = 0,
                    speedValue = 1,
                    attackMult = 2,
                    defenseMult = 1,
                    rangedDefenseMult = 1,
                    speedMult = 1,
                    ImageURI = "monster_masher.png",
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Mech Macerator",
                    Description = "Munch the metal!",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    attackValue = 4,
                    defenseValue = 2,
                    rangedDefenseValue = 1,
                    speedValue = 2,
                    attackMult = 2,
                    defenseMult = 2,
                    rangedDefenseMult = 1,
                    speedMult = 1,
                    ImageURI = "mechanical_macerator.png",
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed},
            };

            return datalist;
        }

        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Evil Harvey",
                    Description = "He's a lean mean killing machine!",
                    ImageURI = "evilHarvey.png",
                    Attack = 200,
                    Defense = 200,
                    Speed = 200,
                    RangedDefense = 200
                },

                new MonsterModel {
                    Name = "Goopy",
                    Description = "\"I don't think we're alone down here...\"",
                    ImageURI = "sewer_gator.png",
                },

                new MonsterModel {
                    Name = "Warehouse Guard",
                    Description = "One of the last organic creatures still on payroll",
                    ImageURI = "SilverRobot.png",
                },

                new MonsterModel {
                    Name = "Prime Security",
                    Description = "Originally designed to cut down on shipping costs...",
                    ImageURI = "shieldRobot.png",
                }
            };

            return datalist;
        }

        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var datalist = new List<CharacterModel>()

            {
                new CharacterModel {
                    Name = "Harvey",
                    Description = "A warrior of the people!",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    ImageURI = "mechanist_class.png",
                    JobClass = "Mechanist"
        },

                new CharacterModel {
                    Name = "Harvey's Dad",
                    Description = "Not as successful as his son.",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },
                new CharacterModel
                {
                    Name = "Optimus Primal",
                    Description = "Roll out, automatons!",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    ImageURI = "ringleader_class.png",
                    JobClass = "Ringleader",                   
                },
                new CharacterModel
                {
                    Name = "Toaster Oven",
                    Description = "Heats up buns, and other kitchen commodities.",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    ImageURI = "mechanic_class.png",
                    JobClass = "Mechanic",    
                },
                new CharacterModel
                {
                    Name = "正宗",
                    Description = "この悪阻らしい刀は天国を切れたと言われている",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    ImageURI = "brawler_class.png",
                    JobClass = "Brawler",
                },
                new CharacterModel
                { 
                    Name = "Blake",
                    Description = "He's okay I guess.",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    ImageURI = "hunter_class.png",
                    JobClass = "Hunter",
                }


    };

            return datalist;
        }
    }
}