using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using System.IO;

namespace PrimeAssault.Views
{
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        // Constructor for UnitTests
        public AboutPage(bool UnitTest){}

        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();


            // Hide the Debug Settings
            DatabaseSettingsFrame.IsVisible = false;

            // Turn off the Settings Frame
            DebugSettingsFrame.IsVisible = false;

            // Set to the curent date and time
            CurrentDateTime.Text = DateTime.Now.ToString("MM/dd/yy hh:mm:ss");


            var stream = GetStreamFromFile("feelgood.mp3");
            player.Load(stream);
        }



        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("PrimeAssault." + filename);
            return stream;

        }

        /// <summary>
        /// Show or hide the Database Frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DatabaseSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Database Section
            DatabaseSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DebugSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
           // Show or hide the Debug Settings
           DebugSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Hack_16_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Debug Settings
            Hack16Frame.IsVisible = (e.Value);

            if (Hack16Switch.IsToggled == true)
            {
                MessagingCenter.Send(this, "Zombification", 1);
            }
            else
            {
                MessagingCenter.Send(this, "Zombification", 0);
            }
        }

        public void PercentageChance_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs args)
        {
            Percentage_Chance.Value = Math.Round(args.NewValue);
            double value = args.NewValue;
            Percentage_Label.Text = String.Format("Percent chance of zombification: %{0}", value);
        }



        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Hack_21_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Debug Settings
            Hack21Frame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Hack_22_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Debug Settings
            Hack22Frame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Hack_23_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Debug Settings
            Hack23Frame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Hack_24_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Debug Settings
            Hack24Frame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Data Source Toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataSource_Toggled(object sender, EventArgs e)
        {
            // Flip the settings
            if (DataSourceValue.IsToggled == true)
            {
                MessagingCenter.Send(this, "SetDataSource", 1);
            }
            else
            {
                MessagingCenter.Send(this, "SetDataSource", 0);
            }
        }

        /// <summary>
        /// Button to delete the data store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void WipeDataList_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Data", "Are you sure you want to delete all data?", "Yes", "No");

            if (answer)
            {
                MessagingCenter.Send(this, "WipeDataList", true);
            }
        }
        

        private void Stop_Clicked(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void Pause_Clicked(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            player.Play();
        }

    }
}