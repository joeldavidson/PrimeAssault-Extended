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

        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;
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

            AddItemsToDisplay();

            return true;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            AddItemsToDisplay();

            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for :";
            PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location));

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        public void AddItemsToDisplay()
        {
            var GridChild = Equipment.Children;

            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.Head), 1, 0);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass), 1, 1);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand), 2, 1);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand), 0, 1);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger), 2, 2);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger), 0, 2);
            Equipment.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet), 1, 2);
        }

        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Defualt Image is the Plus
            var ImageSource = "";
            var ClickableButton = true;

            var data = ViewModel.Data.GetItemByLocation(location);
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

            
            if (ClickableButton)
            {
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(location);
            }


            // Add the Display Text for the item
            //Unused for now
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                   // ItemLabel
                },
            };
            return ItemStack;
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
        /*
        async void Head_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEquipIndexPage());
        }

        async void Necklace_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEquipIndexPage());
        }

        async void RightHand_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEquipIndexPage());
        }

        async void LeftHand_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEquipIndexPage());
        }
        async void Boots_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEquipIndexPage());
        }

        async void Ring1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemIndexPage());
        }

        async void Ring2_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ItemIndexPage());
        } */
    }
}