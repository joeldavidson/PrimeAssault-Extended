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
    public partial class MonIndexPage : ContentPage
    {
        // The view model, used for data binding
        readonly MonIndexViewModel ViewModel = MonIndexViewModel.Instance;
        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public MonIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }
        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnMonSelected(object sender, SelectedItemChangedEventArgs args)
        {
            MonsterModel Mon = args.SelectedItem as MonsterModel;
            if (Mon == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new MonReadPage(new MonsterViewModel(Mon)));

            // Manually deselect item.
            MonListView.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddMon_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new NavigationPage(new MonCreatePage(new MonsterViewModel())));
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
    }
}