using System.Collections.Generic;

namespace Game.Models
{
    /// <summary>
    /// Base Player that Characters and Monsters derive from
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePlayerModel<T> : BaseModel<T>
    {
        #region Attributes

        #region GameEngineAttributes
        // Guid of the original data it links back to the ID, used in Game Engine
        public string Guid;

        // alive status, !alive will be removed from the list
        public bool Alive = true;

        // The type of player, character comes before monster
        public PlayerTypeEnum PlayerType = PlayerTypeEnum.Unknown;

        // TurnOrder
        public int Order = 0;

        // Remember who was first into the list...
        public int ListOrder = 0;

        #endregion GameEngineAttributes

        #region PlayerAttributes

        // Total speed, including level and items
        public int Speed = 0;

        // Level of character or monster
        public int Level = 0;

        // The experience points the player has used in sorting ties...
        public int ExperiencePoints =0;

        // Current Health
        public int CurrentHealth = 0;

        // Max Health
        public int MaxHealth = 0;

        // Total Experience
        public int ExperienceTotal = 0;

        // The defense score, to be used for defending against attacks
        public int Defense { get; set; } = 0;

        // The Attack score to be used when attacking
        public int Attack { get; set; } = 0;

        #endregion PlayerAttributes

        #region Items
        // Item is a string referencing the database table
        public string Head { get; set; } = null;

        // Feet is a string referencing the database table
        public string Feet { get; set; } = null;

        // Necklasss is a string referencing the database table
        public string Necklass { get; set; } = null;

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; } = null;

        // Offhand is a string referencing the database table
        public string OffHand { get; set; } = null;

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; } = null;

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; } = null;
        #endregion Items
        #endregion Attributes

        #region Methods

        public BasePlayerModel()
        {
            Guid = Id;
        }

        public int GetAttack() { return 0; }
        public int GetDefense() { return 0; }
        public int GetSpeed() { return 0; }
        public int GetHealthCurrent() { return CurrentHealth; }
        public int GetHealthMax() { return MaxHealth; }
        public int GetDamageRollValue() { return 10; }


        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public bool TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return false;
            }

            CurrentHealth = CurrentHealth - damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                // Death...
                CauseDeath();
            }

            return true;
        }

        // Death
        // Alive turns to False
        public bool CauseDeath()
        {
            Alive = false;
            return Alive;
        }

        public List<ItemModel> DropAllItems() { return new List<ItemModel>(); }
        public string FormatOutput() { return ""; }
        public bool AddExperience(int newExperience) { return true; }
        public ItemModel AddItem(ItemLocationEnum itemlocation, string itemID) { return new ItemModel(); }
        public int CalculateExperienceEarned(int damage) { return 0; }
        public ItemModel GetItem(string itemString) { return new ItemModel(); }
        public ItemModel GetItemByLocation(ItemLocationEnum itemLocation)
        {
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    return GetItem(Head);

                case ItemLocationEnum.Necklass:
                    return GetItem(Necklass);

                case ItemLocationEnum.PrimaryHand:
                    return GetItem(PrimaryHand);

                case ItemLocationEnum.OffHand:
                    return GetItem(OffHand);

                case ItemLocationEnum.RightFinger:
                    return GetItem(RightFinger);

                case ItemLocationEnum.LeftFinger:
                    return GetItem(LeftFinger);

                case ItemLocationEnum.Feet:
                    return GetItem(Feet);

                default:
                    return null;
            }
        }
        #endregion Methods
    }
}