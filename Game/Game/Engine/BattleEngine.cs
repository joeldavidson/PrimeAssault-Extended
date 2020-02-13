using System;
using System.Collections.Generic;
using System.Text;

using Game.Models;
using Game.ViewModels;

namespace Game.Engine
{
    public enum RoundEnum { Unknown = 0, NextTurn = 1, NewRound = 2, GameOver = 3, }

    public class CharacterModel : BaseModel<CharacterModel>
    {
        public bool Alive = true;
        public int Level = 0;
        public int ExperienceTotal = 0;

        // The speed of the character, impacts movement, and initiative
        public int Speed { get; set; }

        // The defense score, to be used for defending against attacks
        public int Defense { get; set; }

        // The Attack score to be used when attacking
        public int Attack { get; set; }

        // Current Health which is always at or below MaxHealth
        public int CurrentHealth { get; set; }

        // The highest value health can go
        public int MaxHealth { get; set; }

        // Item is a string referencing the database table
        public string Head { get; set; }

        // Feet is a string referencing the database table
        public string Feet { get; set; }

        // Necklasss is a string referencing the database table
        public string Necklass { get; set; }

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; }

        // Offhand is a string referencing the database table
        public string OffHand { get; set; }

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; }

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; }

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

        public List<ItemModel> DropAllItems(){return new List<ItemModel>();}
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
            }

            return null;
        }
    }

    public class MonsterModel : CharacterModel { }

    public class PlayerInfo : BaseModel<PlayerInfo>
    {
        // TurnOrder
        public int Order;
        
        // guid of the original data it links back to
        public string Guid;

        // alive status, !alive will be removed from the list
        public bool Alive;

        // Sorting Order is :  Speed, Level, ExperiencePoints, PlayerType, Name, ListOrder

        // Total speed, including level and items
        public int Speed;

        // Level of character or monster
        public int Level;

        // The experience points the player has used in sorting ties...
        public int ExperiencePoints;

        // The type of player, character comes before monster
        public PlayerTypeEnum PlayerType;

        // Finally if all of the above are the same, sort based on who was loaded first into the list...
        public int ListOrder;

        public int CurrentHealth;

        public int MaxHealth;

        // Need because of the instantiation below
        public PlayerInfo()
        {

        }

        // Take a character and add it to the Player
        public PlayerInfo(CharacterModel data)
        {
            PlayerType = PlayerTypeEnum.Character;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
            Guid = data.Id;
        }

        // Take a monster and add it to the player
        public PlayerInfo(MonsterModel data)
        {
            PlayerType = PlayerTypeEnum.Monster;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetHealthCurrent();
            MaxHealth = data.GetHealthMax();
            Guid = data.Id;
        }
    }


    /// <summary>
    /// Battle Engine for the Game
    /// </summary>
    public class BattleEngine : RoundEngine
    {
        public bool BattleRunning = false;

        public bool PopulateCharacterList()
        {
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());
            CharacterList.Add(new CharacterModel());

            return true;
        }

        //public bool PopulateMonsterList()
        //{
        //    MonsterList.Add(new CharacterModel());
        //    MonsterList.Add(new CharacterModel());
        //    MonsterList.Add(new CharacterModel());
        //    MonsterList.Add(new CharacterModel());
        //    MonsterList.Add(new CharacterModel());
        //    MonsterList.Add(new CharacterModel());

        //    return true;
        //}

        public bool StartBattle(bool isAutoBattle)
        {
            BattleScore.AutoBattle = isAutoBattle;

            BattleRunning = true;

            return true;
        }

        public bool EndBattle()
        {
            BattleRunning = false;

            return true;
        }

        //public bool NewRound()
        //{
        //    // Add Monsters to the Round
        //    MonsterList.Clear();
        //    PopulateMonsterList();

        //    // Increment the Round Number
        //    BattleScore.RoundCount++;

        //    return true;
        //}

        public RoundEnum NextTurn(bool killme)
        {
            if (killme)
            {
                CharacterList.RemoveAt(0);
            }
            else
            {
                MonsterList.RemoveAt(0);
            }

            if (MonsterList.Count == 0)
            {
                // Kill off a character, so the game will end...
                CharacterList.RemoveAt(0);

                return RoundEnum.NewRound;
            }

            if (CharacterList.Count == 0)
            {
                return RoundEnum.GameOver;
            }

            return RoundEnum.NextTurn;
        }
    }
}