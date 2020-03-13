using PrimeAssault.ViewModels;
using System.Collections.Generic;
using PrimeAssault.Helpers;
using System;
using PrimeAssault.Services;

namespace PrimeAssault.Models
{
    /// <summary>
    /// Base Player that Characters and Monsters derive from
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePlayerModel<T> : BaseModel<T>
    {


        #region Attributes

        #region PrimeAssaultEngineAttributes

        public int GetDamageLevelBonus { get { return Convert.ToInt32(Math.Ceiling(Level * .25)); } }

        //Size of character's move set
        const int NUM_MOVES = 2;

        // Guid of the original data it links back to the ID, used in PrimeAssault Engine
        public string Guid;

        // alive status, !alive will be removed from the list
        public bool Alive = true;

        // The type of player, character comes before monster
        public PlayerTypeEnum PlayerType = PlayerTypeEnum.Unknown;

        // TurnOrder
        public int Order = 0;

        // Remember who was first into the list...
        public int ListOrder = 0;

        #endregion PrimeAssaultEngineAttributes

        #region PlayerAttributes

        // Total speed, including level and items
        public int Speed { get; set; } = 0;

        // Level of character or monster
        public int Level { get; set; } = 1;

        // The experience points the player has used in sorting ties... ---ASK QUESTION: DON'T UNDERSTAND IMPORTANCE OF EXPERIENCE POINTS VS TOTAL EXPERIENCE
        public int ExperiencePoints { get; set; } = 0;

        // The Experience available to given up
        public int ExperienceRemaining { get; set; }

        // Current Health
        public int CurrentHealth { get; set; } = 20;

        // Max Health
        public int MaxHealth { get; set; } = 20;

        // Total Experience
        public int ExperienceTotal { get; set; }= 0;

        // The defense score, to be used for defending against physical attacks
        public int Defense { get; set; } = 0;

        // The Attack score to be used when attacking
        public int Attack { get; set; } = 0;

        // The score to be used for defending against ranged attacks
        public int RangedDefense { get; set; } = 0;
       
        //Multiplier value of current base MaxHealth
        public double HealthMult { get; set; } = 1.0;

        //Multiplier value of current base Speed
        public double SpeedMult { get; set; } = 1.0;

        //Multiplier value of current base Defense
        public double DefenseMult { get; set; } = 1.0;

        //Multiplier value of current base RangedDefense
        public double RangedDefenseMult { get; set; } = 1.0;

        //Multiplier value of current base Attack
        public double AttackMult { get; set; } = 1.0;

        //Multiplier for calculating how much EXP is necessary for the next leve
        public double NextLevelMult { get; set; } = 1.0;

        //Ability name of character
        public AbilityModel Ability { get; set; } = new AbilityModel();

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;


        //In the event that the BasePlayerModel is a monster, gives its type
        public MonsterTypesEnum MonsterType { get; set; } = MonsterTypesEnum.Nothing;

        #region moves
        //First move that a character can use in combat
        public string Move1 { get; set; } = "";

        //second move that a character can use in combat
        public string Move2 { get; set; } = "";

        //Array of moves for each character
        public MoveModel[] Moves = new MoveModel[NUM_MOVES];

        #endregion moves

        #endregion PlayerAttributes

        #region Items
        // ItemModel is a string referencing the database table
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
            ImageURI = "soldier_class.png";
            Move1 = "Crackshot";
            Move2 = "Iron Grip";
            Moves[0] = new MoveModel(MoveHolder.GetMove("Crackshot"));
            Moves[1] = new MoveModel(MoveHolder.GetMove("Iron Grip"));
            CurrentHealth = GetHealthMax();
        }

        /// <summary>
        /// Rturn the Calculated Attack of a character
        /// </summary>
        /// <returns></returns>
        public int GetAttack() {
            if (Ability.IsActive)
            {
                ProcessAbility();
            }

            var myReturn = Attack;

            // Get Bonus from Level
            myReturn += LevelTableHelper.Instance.LevelDetailsList[Level].Attack;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Attack);

            return myReturn;
        }

        /// <summary>
        /// Return the Calculated ranged defense of a character
        /// </summary>
        /// <returns></returns>
        public int GetRangedDefense() {
            var myReturn = RangedDefense;

            // Get Bonus from Level
            myReturn += LevelTableHelper.Instance.LevelDetailsList[Level].RangedDefense;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.RangedDefense);

            return myReturn;
        }

        /// <summary>
        /// Return the Calculated Defense
        /// </summary>
        /// <returns></returns>
        public int GetDefense()
        {
            var myReturn = Defense;

            // Get Bonus from Level
            myReturn += LevelTableHelper.Instance.LevelDetailsList[Level].Defense;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Defense);

            return myReturn;
        }

        public int GetHealthCurrent() { return CurrentHealth; }

        /// <summary>
        /// Rturn the Calculated maxHealth of a character
        /// </summary>
        /// <returns></returns>
        public int GetHealthMax() {

            var myReturn = MaxHealth;

            // Get Bonus from Level
            myReturn += LevelTableHelper.Instance.LevelDetailsList[Level].Health;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.MaxHealth);

            return myReturn;
        }

        /// <summary>
        /// Rturn the Calculated Speed
        /// </summary>
        /// <returns></returns>
        public int GetSpeed()
        {

            // Base value
            var myReturn = Speed;

            // Get Bonus from Level
            myReturn += LevelTableHelper.Instance.LevelDetailsList[Level].Speed;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Speed);

            return myReturn;
        }

        /// <summary>
        /// Applies the effects of any ability to the desired stats
        /// </summary>
        /// <returns></returns>
        public bool ProcessAbility()
        {
            if (Ability.FirstEffect == AbilityEffectEnum.Nothing && Ability.SecondEffect == AbilityEffectEnum.Nothing && Ability.ThirdEffect == AbilityEffectEnum.Nothing)
            {
                return false;
            }

            ActivateEffect(Ability.FirstEffect, Ability.FirstEffectValue);
            ActivateEffect(Ability.SecondEffect, Ability.SecondEffectValue);
            ActivateEffect(Ability.ThirdEffect, Ability.ThirdEffectValue);

            return true;
        }

        public bool ActivateEffect(AbilityEffectEnum Effect, double Value)
        {
            if (Effect == AbilityEffectEnum.Nothing)
            {
                return false;
            }
            if (Effect == AbilityEffectEnum.AffectAttack)
            {
                AttackMult += Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectDefense)
            {
                DefenseMult += Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectMaxHealth)
            {
                HealthMult += Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectRangedDefense)
            {
                RangedDefenseMult += Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectSpeed)
            {
                SpeedMult += Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectHealthRegen)
            {
                AugmentHealth((int)Math.Ceiling(-MaxHealth * Value));
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectHealing) //Should act the same as enhancing attack power, since healing is based off of the attack stat
            {
                AttackMult += Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deactivates the effects of an active ability, if the ability affects status of a character.
        /// </summary>
        /// <returns></returns>
        public bool DeactivateAbility()
        {
            if (!Ability.IsActive)
            {
                return true;
            }
            DeactivateEffect(Ability.FirstEffect, Ability.FirstEffectValue);
            DeactivateEffect(Ability.SecondEffect, Ability.SecondEffectValue);
            DeactivateEffect(Ability.ThirdEffect, Ability.ThirdEffectValue);

            Ability.DeactivateAbility();

            return true;
        }

        /// <summary>
        /// Resets multipliers to a pre-effect state.
        /// </summary>
        /// <param name="Effect"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool DeactivateEffect(AbilityEffectEnum Effect, double Value)
        {
            if (Effect == AbilityEffectEnum.Nothing)
            {
                return false;
            }
            if (Effect == AbilityEffectEnum.AffectAttack)
            {
                AttackMult -= Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectDefense)
            {
                DefenseMult -= Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectMaxHealth)
            {
                HealthMult -= Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectRangedDefense)
            {
                RangedDefenseMult -= Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectSpeed)
            {
                SpeedMult -= Value;
                return true;
            }
            if (Effect == AbilityEffectEnum.AffectHealing) //Should act the same as enhancing attack power, since healing is based off of the attack stat
            {
                AttackMult -= Value;
                return true;
            }
            return false;
        }

        public int GetDamageRollValue() 
        {
            var myReturn = 0;

            var myItem = ItemIndexViewModel.Instance.GetItem(PrimaryHand);
            if (myItem != null)
            {
                // Dice of the weapon.  So sword of Damage 10 is d10
                myReturn += DiceHelper.RollDice(1, myItem.Damage);
            }

            // Add in the Level as extra damage per game rules
            myReturn += GetDamageLevelBonus;

            return myReturn;
        }

        //Information available in the event of a monster being updated


        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public bool AugmentHealth(int damage)
        {
            if (damage == 0)
            {
                return false;
            }
           
            //For healing, negative damage is treated as gained health
            CurrentHealth = CurrentHealth - damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                // Death...
                CauseDeath();
            }
            else if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
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

        public string FormatOutput() { return "A level " + Level + " " + Name; }

        public bool AddExperience(int newExperience) 
        {
            // Don't allow going lower in experience
            if (newExperience < 0)
            {
                return false;
            }

            // Increment the Experience
            ExperienceTotal += newExperience;

            // Can't level UP if at max.
            if (Level >= LevelTableHelper.MaxLevel)
            {
                return false;
            }

            // Then check for Level UP
            // If experience is higher than the experience at the next level, level up is OK.
            if (ExperienceTotal >= LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience)
            {
                return LevelUp();
            }
            return false;
        }

        virtual public bool LevelUp()
        {
            return false;
        }

        public int CalculateExperienceEarned(int damage) {
            if (damage < 1)
            {
                return 0;
            }

            int remainingHealth = Math.Max(CurrentHealth - damage, 0); // Go to 0 is OK...
            double rawPercent = (double)remainingHealth / (double)CurrentHealth;
            double deltaPercent = 1 - rawPercent;
            var pointsAllocate = (int)Math.Floor(ExperienceRemaining * deltaPercent);

            // Catch rounding of low values, and force to 1.
            if (pointsAllocate < 1)
            {
                pointsAllocate = 1;
            }

            // Take away the points from remaining experience
            ExperienceRemaining -= pointsAllocate;
            if (ExperienceRemaining < 0)
            {
                pointsAllocate = 0;
            }

            return pointsAllocate;
        }

        //Used to access first move in character
        public MoveModel GetFirstMove(int index) { return Moves[0]; }
        //Used to access second move in character
        public MoveModel GetSecondtMove(int index) { return Moves[1]; }


        //Resets all stat multipliers to their flat 1 values
        public void ResetMultipliers()
        {
            HealthMult = 1;
            SpeedMult = 1;
            DefenseMult = 1;
            RangedDefenseMult = 1;
            AttackMult = 1;
        }

        #region Items
        // Get the Item at a known string location (head, foot etc.)
        public ItemModel GetItem(string itemString)
        {
            return ItemIndexViewModel.Instance.GetItem(itemString);
        }

        // Drop All Items
        // Return a list of items for the pool of items
        public List<ItemModel> DropAllItems()
        {
            var myReturn = new List<ItemModel>();

            // Drop all Items
            ItemModel myItem;

            myItem = RemoveItem(ItemLocationEnum.Head);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Necklass);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.PrimaryHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.OffHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.RightFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.LeftFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Feet);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            return myReturn;
        }

        // Remove ItemModel from a set location
        // Does this by adding a new ItemModel of Null to the location
        // This will return the previous ItemModel, and put null in its place
        // Returns the ItemModel that was at the location
        // Nulls out the location
        public ItemModel RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        // Get the ItemModel at a known string location (head, foot etc.)
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
            }

            return null;
        }

        // Add ItemModel
        // Looks up the ItemModel
        // Puts the ItemModel ID as a string in the location slot
        // If ItemModel is null, then puts null in the slot
        // Returns the ItemModel that was in the location
        public ItemModel AddItem(ItemLocationEnum itemlocation, string itemID)
        {
            ItemModel myReturn;

            switch (itemlocation)
            {
                case ItemLocationEnum.Feet:
                    myReturn = GetItem(Feet);
                    Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    myReturn = GetItem(Head);
                    Head = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    myReturn = GetItem(Necklass);
                    Necklass = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    myReturn = GetItem(PrimaryHand);
                    PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    myReturn = GetItem(OffHand);
                    OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    myReturn = GetItem(RightFinger);
                    RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    myReturn = GetItem(LeftFinger);
                    LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }

            return myReturn;
        }

        public string GetItemURI(ItemLocationEnum itemLocation)
        {

            ItemModel myItem;
            string itemURI;

            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    myItem = GetItem(Head);
                    itemURI = myItem.ImageURI;
                    return itemURI;

                case ItemLocationEnum.Necklass:
                    myItem = GetItem(Necklass);
                    itemURI = myItem.ImageURI;
                    return itemURI;
                   
                case ItemLocationEnum.PrimaryHand:
                    myItem = GetItem(PrimaryHand);
                    itemURI = myItem.ImageURI;
                    return itemURI;

                case ItemLocationEnum.OffHand:
                    myItem = GetItem(OffHand);
                    itemURI = myItem.ImageURI;
                    return itemURI;

                case ItemLocationEnum.RightFinger:
                    myItem = GetItem(RightFinger);
                    itemURI = myItem.ImageURI;
                    return itemURI;

                case ItemLocationEnum.LeftFinger:
                    myItem = GetItem(LeftFinger);
                    itemURI = myItem.ImageURI;
                    return itemURI;

                case ItemLocationEnum.Feet:
                    myItem = GetItem(Feet);
                    itemURI = myItem.ImageURI;
                    return itemURI;
            }

            return null;
        }

        // Walk all the Items on the Character.
        // Add together all Items that modify the Attribute Enum Passed in
        // Return the sum
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            ItemModel myItem;

            myItem = GetItem(Head);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Necklass);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(PrimaryHand);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(OffHand);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(RightFinger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(LeftFinger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Feet);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            return myReturn;
        }

        #endregion Items

        #endregion Methods
    }
}