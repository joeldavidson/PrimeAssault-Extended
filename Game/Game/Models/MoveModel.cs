using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    public class MoveModel : BaseModel<MoveModel>
    {
        public int attack { get; set; } = 1;

        public string type { get; set; } = "melee";

        public MoveModel()
        {
            Name = "Hard Knuckles";
            Description = "An extra powerful punch.";
            ImageURI = "default_melee.png";
        }
    }
}
