using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeAssault.Models
{
    /// <summary>
    /// Basic structure of a move
    /// </summary>
    public class MoveModel : BaseModel<MoveModel>
    {
        //The size of the dice to be rolled by this attack which will be added to the normal attack stat
        public int Attack { get; set; } = 1;

        //Can be melee or ranged, melee attacks can only be used with a melee weapon in hand and ranged attacks can only be used with a ranged weapon
        public string Type { get; set; } = "melee";

        //determines who the attack targets, friend of foe (to facilitate healing)
        public string Target { get; set; } = "foe";

        public int Uses { get; set; } = 5;

        //constructs with default value for a move
        public MoveModel()
        {
            Name = "Hard Knuckles";
            Description = "An extra powerful punch.";
            ImageURI = "default_melee.png";
        }

        public MoveModel(MoveModel data)
        {
            Name =data.Name;
            Description = data.Description;
            ImageURI = data.ImageURI;
            Attack = data.Attack;
            Type = data.Type;
            Target = data.Target;
            Uses = data.Uses;
        }
    }
}
