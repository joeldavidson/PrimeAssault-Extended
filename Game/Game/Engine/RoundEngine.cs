﻿using System.Collections.Generic;
using System.Linq;

using PrimeAssault.ViewModels;
using PrimeAssault.Models;

namespace PrimeAssault.Engine
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : TurnEngine
    {
        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        /// <returns></returns>
        private bool ClearLists()
        {
            ItemPool.Clear();
            MonsterList.Clear();
            return true;
        }

        // Call to make a new set of monsters...
        public bool NewRound()
        {
            // End the existing round
            EndRound();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Update Score for the RoundCount
            BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones...
            * If you have crudi monsters, then pick from the list

            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            */
        /// </summary>
        /// <returns></returns>
        public int AddMonstersToRound()
        {
            int i;
            for (i = MonsterList.Count(); i < MaxNumberPartyMonsters; i++)
            {
                foreach (var data in MonsterIndexViewModel.Instance.Dataset)
                {
                    if (i < MaxNumberPartyMonsters)
                    {
                        MonsterList.Add(new PlayerInfoModel(data));
                    }
                    ++i;
                }
                break;
            }

            for (i = MonsterList.Count; i < MaxNumberPartyMonsters; i++)
            {
                var data = new MonsterModel { Attack = 10, CurrentHealth = 10 };
                // Help identify which Monster it is
                data.Name += " " + MonsterList.Count() + 1;
                MonsterList.Add(new PlayerInfoModel(data));
            }

            return MonsterList.Count();
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over
                RoundStateEnum = RoundEnum.PrimeAssaultOver;
                return RoundStateEnum;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            // Decide Who gets next turn
            // Remember who just went...
            PlayerCurrent = GetNextPlayerTurn(); //Turn order

            if (!BattleScore.AutoBattle && PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                TakeManualTurn(PlayerCurrent);
            }
            else
            {
                // Do the turn....
                TakeAutoTurn(PlayerCurrent); //Attack, targeting, damage
            }

            RoundStateEnum = RoundEnum.NextTurn;

            return RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerTurn()
        {
            // Recalculate Order
            OrderPlayerListByTurnOrder();

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before MonsterModel (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            // Work with the Class variable PlayerList
            PlayerList = MakePlayerList();

            PlayerList = PlayerList.OrderByDescending(a => a.GetSpeed())
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();

            return PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            PlayerList.Clear();

            // Remeber the Insert order, used for Sorting
            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            return PlayerList;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (PlayerList.Count == 0)
            {
                return null;
            }

            // No current player, so set the first one
            if (PlayerCurrent == null)
            {
                return PlayerList.FirstOrDefault();
            }

            // Find current player in the list
            var index = PlayerList.FindIndex(m => m.Guid.Equals(PlayerCurrent.Guid));

            // If at the end of the list, return the first element
            if (index == PlayerList.Count() - 1)
            {
                return PlayerList.FirstOrDefault();
            }

            // Return the next element
            return PlayerList[index + 1];
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public bool PickupItemsFromPool(PlayerInfoModel character)
        {
            // Have the character, walk the items in the pool, and decide if any are better than current one.

            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);

            return true;
        }

        public void PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            var thisLocation = setLocation;
            if (setLocation == ItemLocationEnum.RightFinger)
            {
                thisLocation = ItemLocationEnum.Finger;
            }

            if (setLocation == ItemLocationEnum.LeftFinger)
            {
                thisLocation = ItemLocationEnum.Finger;
            }

            var myList = ItemPool.Where(a => a.Location == thisLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return false;
            }

            var CharacterItem = character.GetItemByLocation(setLocation);
            if (CharacterItem == null)
            {
                SwapCharacterItem(character, setLocation, myList.FirstOrDefault());
                return true;
            }

            foreach (var PoolItem in myList)
            {
                if (PoolItem.Value > CharacterItem.Value)
                {
                    SwapCharacterItem(character, setLocation, PoolItem);
                    return true;
                }
            }

            return true;
        }


        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="PoolItem"></param>
        /// <returns></returns>
        public ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            // Put on the new ItemModel, which drops the one back to the pool
            var droppedItem = character.AddItem(setLocation, PoolItem.Id);

            // Add the PoolItem to the list of selected items
            BattleScore.ItemModelSelectList.Add(PoolItem);

            // Remove the ItemModel just put on from the pool
            ItemPool.Remove(PoolItem);

            if (droppedItem != null)
            {
                // Add the dropped ItemModel to the pool
                ItemPool.Add(droppedItem);
            }

            return droppedItem;
        }

        public bool TakeManualTurn(PlayerInfoModel Attacker)
        {
            //Must await input from a user which includes both Type of attack (move or normal) and enemy
            //Attack must be processed
            //continue as normal
            
            var result = Attack(Attacker);

            BattleScore.TurnCount++;

            return result;

        }
    }
}