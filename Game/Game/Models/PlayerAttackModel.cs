using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    class PlayerAttackModel
    {
        public PlayerInfoModel Target;
        public PlayerInfoModel Attacker;
        public MoveEnum Move;

        public PlayerAttackModel()
        {
            Target = null;
            Attacker = null;
            Move = MoveEnum.None;
        }
    }
}
