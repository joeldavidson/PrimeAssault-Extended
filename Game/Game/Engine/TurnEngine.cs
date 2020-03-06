using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using PrimeAssault.Services;
using PrimeAssault.Models;
using PrimeAssault.Helpers;
using PrimeAssault.ViewModels;

namespace PrimeAssault.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// </summary>
   
    public class TurnEngine : BaseEngine
    {
        //variable which determines likelihood that an "AI" will use a move on any given turn.
        public static int PROBABILITY_OF_MOVE = 3; 

        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            var result = Attack(Attacker);

            BattleScore.TurnCount++;

            return result;
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(PlayerInfoModel Attacker)
        {
            // For Attack, Choose Who
            var Target = AttackChoice(Attacker);

            if (Target == null)
            {
                return false;
            }


            // Do Attack
            TurnAsAttack(Attacker, Target);

            CurrentAttacker = new PlayerInfoModel(Attacker);
            CurrentDefender = new PlayerInfoModel(Target);

            return true;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            switch (data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();

                case PlayerTypeEnum.Character:
                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectCharacterToAttack()
        {
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // Select first in the list
            var Defender = CharacterList
                .Where(m => m.Alive)
                .OrderBy(m => m.GetHealthCurrent())
                .OrderBy(m => m.GetAttack())
                .OrderBy(m => m.ListOrder).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectMonsterToAttack()
        {
            if (MonsterList == null)
            {
                return null;
            }

            if (MonsterList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 
            var Defender = MonsterList
                .Where(m => m.Alive)
                .OrderBy(m => m.GetHealthCurrent())
                .OrderBy(m => m.GetAttack()).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target) //Kind of monolothic, consider decomposing
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            BattleMessagesModel.TurnMessage = string.Empty;
            BattleMessagesModel.TurnMessageSpecial = string.Empty;
            BattleMessagesModel.AttackStatus = string.Empty;
            BattleMessagesModel.MoveStatus = string.Empty;
            BattleMessagesModel.DamageAmount = string.Empty;

            // Remember Current Player
            BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            // Choose who to attack

            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            //checks for if move can potentially be used...
            MoveModel moveUsed = null;
            if (Attacker.Moves[0].Uses != 0 || Attacker.Moves[1].Uses != 0) //Assumes attackers always have two moves
            {
                //uses move...
                moveUsed = UseMove(Attacker);
            }

            //need to check for any active attack/defense abilities...
            if (Attacker.Ability.TriggeredOn == AbilityTriggerEnum.Attack)
            {
                Attacker.ProcessAbility();
            }
            else if (Attacker.Ability.TriggeredOn  == AbilityTriggerEnum.Advantage)
            {
                if (Attacker.Ability.IsAdvantaged(Target.MonsterType))
                {
                    Attacker.ProcessAbility();
                }
            }
            // Set Attack
            var AttackScore = Attacker.Level + Attacker.GetAttack();

            //if a move was used, add the attack of the move to the normal attack score
            if (moveUsed != null)
            {
                BattleMessagesModel.MoveStatus = (" with " + moveUsed.Name);
                AttackScore += moveUsed.Attack;
            }

            //need to check for any active attack/defense abilities...
            if (Target.Ability.TriggeredOn == AbilityTriggerEnum.Defense)
            {
                Target.ProcessAbility();
            }

            bool Melee = false;

            //need to check what damage type it is to see what defense stat is used (ranged or melee)
            if (Target.GetItemByLocation(ItemLocationEnum.PrimaryHand) == null || Target.GetItemByLocation(ItemLocationEnum.PrimaryHand).Range != 0)
            {
                Melee = true;
            }

            //Set defense
            int DefenseScore;
            if (Melee)
            {
                DefenseScore = Target.GetDefense() + Target.Level;
            }
            else
            {
                DefenseScore = Target.GetRangedDefense() + Target.Level;
            }

            BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            switch (BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.Hit:
                    // It's a Hit
                    //Calculate Damage
                    int damage = Attacker.GetDamageRollValue();
                    BattleMessagesModel.DamageAmount = (damage.ToString());

                    Target.AugmentHealth(damage);
                    BattleMessagesModel.CurrentHealth = Target.CurrentHealth;
                    BattleMessagesModel.DamageAmount = " for " + BattleMessagesModel.DamageAmount + " damage,";
                    BattleMessagesModel.TurnMessageSpecial = BattleMessagesModel.GetCurrentHealthMessage();

                    RemoveIfDead(Target);
                    break;
            }

            Attacker.DeactivateAbility();
            Target.DeactivateAbility();
            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.MoveStatus + BattleMessagesModel.DamageAmount + BattleMessagesModel.TurnMessageSpecial;
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// Function which determines if a move should be used, and then what move should be used. It also keeps track of uses of each move. Overall needs reworking. First attempt was not tenable or easy to understand. Too many ifs.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public MoveModel UseMove(PlayerInfoModel Attacker)
        {
            //If a character has a move that can be used, there is a PROBABILITY_OF_MOVE*10% chance they will use it
            int decision = DiceHelper.RollDice(1, 10);
            if (decision >= PROBABILITY_OF_MOVE)
            {
                return null;
            }
            MoveModel RetMove = null;
            //Test to see if attack must be ranged and do subsequent checks to determine if any valid moves exist
            if (Attacker.GetItemByLocation(ItemLocationEnum.PrimaryHand) != null && Attacker.GetItemByLocation(ItemLocationEnum.PrimaryHand).Range != 0)
            {
                //If both moves are viable moves
                if (Attacker.Moves[0].Type == "ranged" && Attacker.Moves[1].Type == "ranged")
                {
                    //returns a random Move from index 0 or 1 based on the random dice roll, but also checks to see if randomly selected move has no uses left
                    RetMove = Attacker.Moves[decision % 2];
                    if (RetMove.Uses == 0)
                    {
                        Attacker.Moves[(decision + 1 % 2)].Uses--;
                        RetMove = Attacker.Moves[(decision + 1) % 2];
                    }
                    Attacker.Moves[decision % 2].Uses--;
                }
                //if only move 0 is a valid move
                else if (Attacker.Moves[0].Type == "ranged" && Attacker.Moves[1].Type == "melee")
                {
                    RetMove = Attacker.Moves[0];
                    if (RetMove.Uses != 0)
                    {
                        Attacker.Moves[0].Uses--;
                    }
                    else
                    {
                        RetMove = null;
                    }
                }
                //if only move 1 is a valid move
                else if (Attacker.Moves[0].Type == "melee" && Attacker.Moves[1].Type == "ranged")
                {
                    RetMove = Attacker.Moves[1];
                    if (RetMove.Uses != 0)
                    {
                        Attacker.Moves[1].Uses--;
                    }
                    else
                    {
                        RetMove = null;
                    }
                }
            }

            //if the weapon in hand is melee or unarmed and subsequent checks to see if any valid move exists
            else
            {
                if (Attacker.Moves[0].Type == "melee" && Attacker.Moves[1].Type == "melee")
                {
                    //returns a random Move from index 0 or 1 based on the random dice roll, but also checks to see if randomly selected move has no uses left
                    RetMove = Attacker.Moves[decision % 2];
                    if (RetMove.Uses == 0)
                    {
                        Attacker.Moves[(decision + 1 % 2)].Uses--;
                        RetMove = Attacker.Moves[(decision + 1) % 2];
                    }
                    Attacker.Moves[decision % 2].Uses--;
                }
                //if only move 0 is a valid move
                else if (Attacker.Moves[0].Type == "melee" && Attacker.Moves[1].Type == "ranged")
                {
                    RetMove = Attacker.Moves[0];
                    if (RetMove.Uses != 0)
                    {
                        Attacker.Moves[0].Uses--;
                    }
                    else
                    {
                        RetMove = null;
                    }
                }
                //if only move 1 is a valid move
                else if (Attacker.Moves[0].Type == "ranged" && Attacker.Moves[1].Type == "melee")
                {
                    RetMove = Attacker.Moves[1];
                    if (RetMove.Uses != 0)
                    {
                        Attacker.Moves[1].Uses--;
                    }
                    else
                    {
                        RetMove = null;
                    }
                }
            }
            return RetMove;
        }

        /// <summary>
        /// If Dead process Targed Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                TargetDied(Target);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public bool TargetDied(PlayerInfoModel Target)
        {
            // Mark Status in output
            BattleMessagesModel.TurnMessageSpecial = " and causes death. ";

            // Remove target from list...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    CharacterList.Remove(Target);

                    // Add the MonsterModel to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    DropItems(Target);

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    MonsterList.Remove(Target);

                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    DropItems(Target);

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = " Items Dropped : ";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + " , ";
            }

            ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            BattleMessagesModel.TurnMessageSpecial += DroppedMessage;

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";

                // Force Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                return BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";

                // Force Hit
                BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
                return BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";
                
                // Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.DamageAmount = "0";
                return BattleMessagesModel.HitStatus;
            }

            BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // You decide how to drop monster items, level, etc.

            // The Number drop can be Up to the Round Count, but may be less.  
            // Negative results in nothing dropped
            var NumberToDrop = (DiceHelper.RollDice(1, round+1)-1);

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                result.Add(data);
            }

            return result;
        }
    }
}