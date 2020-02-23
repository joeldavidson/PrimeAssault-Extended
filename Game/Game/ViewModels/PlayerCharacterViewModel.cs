using System;
using System.Collections.Generic;
using System.Text;
using PrimeAssault.Models;

namespace PrimeAssault.ViewModels
{
    public class PlayerCharacterViewModel : BaseViewModel<CharacterModel>
    {
        /// <summary>
        /// The Character Model
        /// </summary>
        public CharacterModel Data { get; set; }

        /// <summary>
        /// Constructor takes an existing character and sets
        /// The Title for the page to match the text of data
        /// The Data to be the passed in data
        /// </summary>
        /// <param name="data"></param>
        public PlayerCharacterViewModel(CharacterModel data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
