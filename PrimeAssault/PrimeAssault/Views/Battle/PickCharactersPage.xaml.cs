using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeAssault.Views
{
	/// <summary>
	/// The Main PrimeAssault Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickCharactersPage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PickCharactersPage()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Battle
		/// 
		/// Its Modal because don't want user to come back...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void BattleButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
			await Navigation.PopAsync();
		}
	}
}