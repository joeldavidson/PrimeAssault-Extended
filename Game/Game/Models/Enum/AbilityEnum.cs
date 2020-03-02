using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{

    /// <summary>
    /// Serves as ID for abilities. NOTE: These must be included sequentially! Otherwise the
    /// GetRandomAbility() funcion in Ability holder will not work
    /// </summary>
    public enum AbilityEnum
    {
        None = 0,
        CrocodileHunter = 1,
        XRayVision = 2,
        Generalist = 3,
        BigGameHunter = 4,
        Y2Killer = 5
    }
}
