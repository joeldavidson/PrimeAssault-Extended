using System.ComponentModel;
using Xamarin.Forms;

using PrimeAssault.Models;
using PrimeAssault.ViewModels;
using System;

namespace PrimeAssault.Views
{
    [DesignTimeVisible(false)]
    public partial class CharReadPage : ContentPage
    {

        PlayerCharacterViewModel viewModel;

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public CharReadPage(PlayerCharacterViewModel data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharUpdatePage(new PlayerCharacterViewModel(viewModel.Data))));
            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharDeletePage(new PlayerCharacterViewModel(viewModel.Data))));
            await Navigation.PopAsync();
        }
    }
}