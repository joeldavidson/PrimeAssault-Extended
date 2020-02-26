using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAssault.Models;
using PrimeAssault.ViewModels;
using System.ComponentModel;
using PrimeAssault.Helpers;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeAssault.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CharUpdatePage : ContentPage
    {
        // View Model for Item
        PlayerCharacterViewModel ViewModel;

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CharUpdatePage(PlayerCharacterViewModel data)
        {
            InitializeComponent();
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }
 
            BindingContext = this.ViewModel = data;
            UpdatePageBindingContext();
        }

        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var level = this.ViewModel.Data.Level;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            // This resets the Picker to -1 index, need to reset it back
            ViewModel.Data.Level = level;
            LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            ManageHealth();


            return true;
        }

        /// <summary>
        /// The Level selected from the list
        /// Need to recalculate Max Health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Level_Changed(object sender, EventArgs args)
        {
            // Change the Level
            ViewModel.Data.Level = LevelPicker.SelectedIndex + 1;

            ManageHealth();
        }


        /// <summary>
        /// Change the Level Picker
        /// </summary>
        public void ManageHealth()
        {
            // Roll for new HP
            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            // Show the Result
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.CurrentHealth = ViewModel.Data.MaxHealth;

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Returns to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
