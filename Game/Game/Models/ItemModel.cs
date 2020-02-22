using PrimeAssault.Services;
using PrimeAssault.Helpers;

namespace PrimeAssault.Models
{
    /// <summary>
    /// Item for the PrimeAssault
    /// 
    /// The Items that a character can use, a Monster may drop, or may be randomly available.
    /// The items are stored in the DB, and during game time a random item is selected.
    /// The system supports CRUDi operatoins on the items
    /// When in test mode, a test set of items is loaded
    /// When in run mode the items from from the database
    /// When in online mode, the items come from an api call to a webservice
    /// 
    /// When characters or monsters die, they drop items into the Items Pool for the Battle
    /// 
    /// </summary>
    public class ItemModel : BaseModel<ItemModel>
    {
        // Range of the item, swords are 1, hats/rings are 0, bows are >1
        public int Range { get; set; } = 0;

        // The Damage the Item can do if it is used as a weapon in the primary hand
        public int Damage { get; set; } = 0;

        // Enum of the different attributes that the item modifies, Items can only modify one item
        public AttributeEnum Attribute { get; set; } = AttributeEnum.Unknown;

        // Where the Item goes on the character.  Head, Foot etc.
        public ItemLocationEnum Location { get; set; } = ItemLocationEnum.Unknown;

        // The int value of an item when being sold
        public int Value { get; set; } = 0;
        //the string value
        public string valueString { get; set; } = "0g";
        public int attackValue { get; set; } = 0;
        //what kind of defense bonus the item can add
        public int defenseValue { get; set; } = 0;
        //what kind of reanged defense bonus the item can add
        public int rangedDefenseValue { get; set; } = 0;
        //How much speed the item adds
        public int speedValue { get; set; } = 0;
        //How much health the item adds
        public int healthValue { get; set; } = 0;
        //What the attack multiplier for the item is
        public double attackMult { get; set; } = 0;
        //What the defense multiplier for the item has
        public double defenseMult { get; set; } = 0;
        //What the ranged defense multiplier for the item has
        public double rangedDefenseMult { get; set; } = 0;
        //What the speed multiplier for the item has
        public double speedMult { get; set; } = 0;
        //What the health multiplier for the item has
        public double healthMult { get; set; } = 0;
        //// Count of how many
        //public int Count { get; set; } = 1;

        //// Tracks if the item is a consumable or not
        //public bool IsConsumable { get; set; } = false;

        //// The Category of the itme
        //public int Category { get; set; } = 0;

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public ItemModel()
        {
            ImageURI = ItemService.DefaultImageURI;
        }

        /// <summary>
        /// Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public ItemModel(ItemModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        override public bool Update(ItemModel data)
        {
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            attackValue = data.attackValue;
            defenseValue = data.defenseValue;
            rangedDefenseValue = data.rangedDefenseValue;
            speedValue = data.speedValue;
            healthValue = data.healthValue;

            attackMult = data.attackMult;
            defenseMult = data.defenseMult;
            rangedDefenseMult = data.rangedDefenseMult;
            speedMult = data.speedMult;
            healthMult = data.healthMult;

            Value = GetValue();
            valueString = Value + "g";
            return true;
        }

        //Gets the cash value of an item. This is the amount the item will be sold for. 
        int GetValue()
        {
            return attackValue + defenseValue + rangedDefenseValue + speedValue + healthValue;
        }
        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description + " for " +
                            Location.ToString() + " with " +
                            Attribute.ToString() +
                            "+" + Value + " , " +
                            "Damage : " + Damage + " , " +
                            "Range : " + Range;

            return myReturn.Trim();
        }

        /// <summary>
        /// Updates the Item to be closer to what the Level would want
        /// </summary>
        /// <param name="level"></param>
        public int ScaleLevel(int level)
        {
            if (DiceHelper.ForceRollsToNotRandom)
            {
                // Use the level as the value
                Value = level;
            }

            // Roll a dice of up to the Level
            Value = DiceHelper.RollDice(1, level);

            return Value;
        }
    }
}