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
                    Location = ItemLocationEnum.OffHand,
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
                    ImageURI = "sewer_gator.png",
                    Attack = 200,
                    Defense = 200,
                    Speed = 200,
                    RangedDefense = 200
                },

                new MonsterModel {
                    Name = "Goopy",
                    Description = "Gloop glop he's bot.",
                    ImageURI = "sewer_gator.png",
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

                }
            };

            return datalist;
        }
    }
}