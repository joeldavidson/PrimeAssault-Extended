using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    public class PlayerAttackModel
    {
        // The target of the attack
        public PlayerInfoModel Target;

        // the character making the attack
        public PlayerInfoModel Attacker;

        //The what move the character used
        public MoveEnum Move;

        public PlayerAttackModel()
        {
            Target = null;
            Attacker = null;
            Move = MoveEnum.None;
        }
    }
}
