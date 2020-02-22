using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAssault.Models;
using PrimeAssault.ViewModels;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeAssault.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// Create Char
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CharCreatePage : ContentPage
    {
        // The Char to create
        PlayerCharacterViewModel ViewModel { get; set; }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharCreatePage(PlayerCharacterViewModel data)
        {
            InitializeComponent();

            data.Data = new CharacterModel();

            BindingContext = this.ViewModel = data;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}