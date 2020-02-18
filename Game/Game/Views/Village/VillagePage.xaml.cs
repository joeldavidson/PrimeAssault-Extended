using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeAssault.Views
{
	/// <summary>
	/// The Main PrimeAssault Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VillagePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public VillagePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Monsters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void MonstersButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MonIndexPage());
		}

		/// <summary>
		/// Jump to the Characters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void CharactersButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CharIndexPage());
		}

		/// <summary>
		/// Jump to the Items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void ItemsButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ItemIndexPage());
		}

		/// <summary>
		/// Jump to the Scores
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void ScoresButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ScoreIndexPage());
		}
	}
}