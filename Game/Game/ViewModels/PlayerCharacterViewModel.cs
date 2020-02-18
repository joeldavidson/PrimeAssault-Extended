using System;
using System.Collections.Generic;
using System.Text;
using PrimeAssault.Models;

namespace PrimeAssault.ViewModels
{
    public class PlayerCharacterViewModel : BaseViewModel<CharacterModel>
    {
        public CharacterModel Data { get; set; }

        public PlayerCharacterViewModel(CharacterModel data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
