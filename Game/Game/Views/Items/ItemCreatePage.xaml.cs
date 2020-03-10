using PrimeAssault.Models;
using PrimeAssault.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeAssault.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<ItemModel> ViewModel = new GenericViewModel<ItemModel>();


        // Empty Constructor for UTs
        public ItemCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ItemCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new ItemModel();

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Create";

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
            //pick.SelectedItem = ViewModel.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Stepper for changing the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}