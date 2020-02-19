using PrimeAssault.Models;
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
                    Name = "Evil Lazar",
                    Description = "Totally evil, totally bad.",
                    Range = 0,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Monster Masher",
                    Description = "Dastardly attack potential!",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Mech Macerator",
                    Description = "Munch the metal!",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
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
            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Harvey",
                    Description = "A warrior of the people!",
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