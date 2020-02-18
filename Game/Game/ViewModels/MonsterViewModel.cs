using System;
using System.Collections.Generic;
using System.Text;
using PrimeAssault.Models;

namespace PrimeAssault.ViewModels
{
    public class MonsterViewModel : BaseViewModel<MonsterModel>
    {
        //The Monster model
        public MonsterModel Data { get; set; }

        /// <summary>
        /// Constructor takes an existing Monster and sets
        /// The Title for the page to match the text of data
        /// The Data to be the passed in data
        /// </summary>
        /// <param name="data"></param>
        public MonsterViewModel(MonsterModel data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
