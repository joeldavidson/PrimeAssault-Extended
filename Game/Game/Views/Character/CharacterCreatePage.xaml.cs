using System;
using PrimeAssault.Helpers;
using PrimeAssault.Models;
using PrimeAssault.ViewModels;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }
            this.ViewModel.Data.Level = 1;

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
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
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.CurrentHealth = ViewModel.Data.MaxHealth;
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