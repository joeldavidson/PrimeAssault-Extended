using System;
using System.ComponentModel;
using Xamarin.Forms;

using PrimeAssault.Models;
using PrimeAssault.ViewModels;

namespace PrimeAssault.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// Index Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CharIndexPage : ContentPage
    {
        // The view model, used for data binding
        readonly CharacterIndexViewModel ViewModel = CharacterIndexViewModel.Instance;

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public CharIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }
        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnCharSelected(object sender, SelectedItemChangedEventArgs args)
        {
            CharacterModel Char = args.SelectedItem as CharacterModel;
            if (Char == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new CharReadPage(new PlayerCharacterViewModel(Char)));

            // Manually deselect item.
            CharListView.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddChar_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new NavigationPage(new CharCreatePage(new PlayerCharacterViewModel())));
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }

        async void Attack_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Character Attack", "The Attack stat helps determine how much damage the unit will deal! The higher the better!", "Dismiss");
        }

        async void Defense_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Character Defense", "The Defense stat helps determine how much damage the unit will take! The higher the better!", "Dismiss");
        }

        async void RangedDefense_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Character Ranged Defense", "The Ranged Defense stat helps determine if the unit will get hit! The higher the better!", "Dismiss");
        }

        async void Speed_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Character Speed", "The Speed stat helps determine if the unit will get hit! The higher the better!", "Dismiss");
        }

        async void Head_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }

        async void Torso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }

        async void RightHand_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }

        async void LeftHand_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }
        async void Boots_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }

        async void Ring1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }

        async void Ring2_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new NavigationPage(new ItemIndexPage()));
        }
    }
}