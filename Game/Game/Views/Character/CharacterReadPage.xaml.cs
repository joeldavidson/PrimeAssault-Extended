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
        // View Model for Item
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

            AddItemsToDisplay();
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

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharDeletePage(new PlayerCharacterViewModel(viewModel.Data))));
            await Navigation.PopAsync();
        }

        public void AddItemsToDisplay()
        {
            var GridChild = Equipment.Children;

            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.Head), 1, 0);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass), 1, 1);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand),2, 1);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand),0,1);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger),2,2);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger),0,2);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet),1,2);
        }

        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Defualt Image is the Plus
            var ImageSource = "";
            //var ClickableButton = true;

            var data = viewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                // Show the Default Icon for the Location
                data = new ItemModel { Location = location, ImageURI = ImageSource };

                // Turn off click action
                //ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            /*
            if (ClickableButton)
            {
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }
            */
            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton
                }
            };

            return ItemStack;
        }
    }
}