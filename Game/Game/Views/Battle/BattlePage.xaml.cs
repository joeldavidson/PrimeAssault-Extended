using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrimeAssault.Models;
using System.Linq;
using PrimeAssault.ViewModels;
using PrimeAssault.Helpers;


namespace PrimeAssault.Views
{
	/// <summary>
	/// The Main PrimeAssault Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{

		// This uses the Instance so it can be shared with other Battle Pages as needed
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		#region PageHandelerVariables
		    // Hold the Selected Characters
		    public PickCharactersPage ModalPickCharactersPage;

		    // Hold the New Round Page where monsters are shown
		    public NewRoundPage ModalNewRoundPage;

		    // Hold Round Over Page
		    public RoundOverPage ModalRoundOverPage;

		    // Hold the Game Over Page where the Final Score is shown
		    public ScorePage ModalBattleGameOverPage;

		    // HTML Formatting for message output box
		    public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

            public PlayerInfoModel currentMonster = new PlayerInfoModel();

            public PlayerInfoModel currentAndroid = new PlayerInfoModel();

            int potions;

            public Image GoldArrow = new Image
            {
                Source = "GoldArrow.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 50,
                WidthRequest = 50,
                IsVisible = false,
            };

            public Image RedArrow = new Image
            {
                Source = "RedArrow.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 50,
                WidthRequest = 50,
                IsVisible = false,
            };

        #endregion PageHandelerVariables

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage ()
		{

			InitializeComponent ();

            BindingContext = EngineViewModel;

			// Clear the Screen

			// Start the Battle Engine
			EngineViewModel.Engine.StartBattle(false);

			// TODO: Replace this with actual determination after getting the real player and character list.
			// Set the Default Attacker and Defender
			EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).FirstOrDefault();
			EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).FirstOrDefault();

            

			ShowModalNewRoundPage();
            initializeAllMonsters();
            initializeAllCharacters();
            PlayerSelected(EngineViewModel.Engine.GetNextPlayerTurn());
            potions = EngineViewModel.Engine.MaxNumberPotions;
            PotionsLeft.Text = potions.ToString();
            Move1.Text = currentAndroid.Move1;
            Move2.Text = currentAndroid.Move2;

        }

        public void initializeAllMonsters()
        {
            int x = 0;
            int y = 0;
            int flip = 180;
            // Draw the Monsters
            foreach (var data in EngineViewModel.Engine.MonsterList)
            {
                StackLayout monster = CreatePlayerDisplayBox(data);
                if(y == 0)
                {
                    MonsterSelected(data, true);
                }
                if (y == 3)
                {
                    y++;
                }

                Grid.SetColumn(monster, y);

                if (y < 3)
                {
                    Grid.SetRow(monster, x++);

                }
                else
                {
                    monster.RotationY = flip;
                    data.flip = flip;
                    Grid.SetRow(monster, --x);
                }
                data.Y = y;
                data.X = x;
                MonsterListGrid.Children.Add(monster);
                y++;
            }
        }

        public void initializeAllCharacters()
        {
            // Add Players to Display
            int x = 2;
            int y = 0;
            int flip = 180;
            foreach (var data in EngineViewModel.Engine.CharacterList)
            {
                StackLayout character = CreatePlayerDisplayBox(data);

                //character.Children
                if (y == 3)
                {
                    y++;
                }

                Grid.SetColumn(character, y);
                //Grid.SetColumn(ArrowImage, y);
                if (y < 3)
                {
                    //  Grid.SetColumn(ArrowImage, x);
                    Grid.SetRow(character, x--);
                }
                else
                {
                    character.RotationY = flip;
                    Grid.SetRow(character, ++x);
                    data.flip = flip;
                }
                data.Y = y;
                data.X = x;
                if (data.X < 0)
                {
                    data.X = 0;
                }
                PartyListGrid.Children.Add(character);
                y++;
            }
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            var ClickableButton = true;
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new ImageButton
            {
                Source = data.ImageURI,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RotationY = 0,
            };



            if (data == null)
            {
                // Turn off click action
                ClickableButton = false;
            }

            // Put the Image Button and Text inside a layout
            if (data.PlayerType == PlayerTypeEnum.Character)
            {
                var PlayerStack = new StackLayout
                {
                    Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                    Padding = 0,
                    Spacing = 0,

                    Children = {
                        PlayerImage
                    },

                };
                if (ClickableButton)
                {
                    PlayerImage.Clicked += (sender, args) => PlayerSelected(data, true);
                    AttackButton.Clicked += (sender, args) => UnitAttacks(data, PlayerImage);
                    AttackButton.Clicked += (sender, args) => UnitGetsHit(data, PlayerImage);
                    AttackButton.Clicked += (sender, args) => UnitDies(data, PlayerImage);
                    AttackButton.Clicked += (sender, args) => slaveSelectToAttacker();
                    AttackButton.Clicked += (sender, args) => slaveSelectToDefender();
                    Move1.Clicked += (sender, args) => PlayerSelected(EngineViewModel.Engine.GetNextPlayerTurn(), true);
                }
                return PlayerStack;
            }
            else
            {
                var PlayerStack = new StackLayout
                {
                    Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                    Padding = 0,
                    Spacing = 0,

                    Children = {
                        PlayerImage
                    },

                };
                if (ClickableButton)
                {                        // Add a event to the user can click the item and see more
                    PlayerImage.Clicked += (sender, args) => MonsterSelected(data, true);
                    AttackButton.Clicked += (sender, args) => UnitAttacks(data, PlayerImage);
                    AttackButton.Clicked += (sender, args) => UnitGetsHit(data, PlayerImage);
                    AttackButton.Clicked += (sender, args) => UnitDies(data, PlayerImage);
                    AttackButton.Clicked += (sender, args) => slaveSelectToAttacker();
                    AttackButton.Clicked += (sender, args) => slaveSelectToDefender();
                }
                return PlayerStack;
            }
        }

        public void UnitDies(PlayerInfoModel data, ImageButton PlayerImage)
        {
            if (data.CurrentHealth < 1)
            {
                PlayerImage.RotateTo(90);
            }
        }

        public void UnitAttacks(PlayerInfoModel data, ImageButton PlayerImage)
        {
            if (data.lastToAttack)
            {
                rotateHit(data, PlayerImage);
                data.lastToAttack = false;
            }
        }


        public void UnitGetsHit(PlayerInfoModel data, ImageButton PlayerImage)
        {
            if (data.lastToGetHit)
            {
                rotateGetsHit(data, PlayerImage);
                data.lastToGetHit = false;
            }
        }

        public async void rotateHit(PlayerInfoModel data, ImageButton PlayerImage)
        {
            int tValY = 100;
            int tValX = 100;
            if (data.PlayerType == PlayerTypeEnum.Character)
            {
                tValY *= -1;
            }
            if (data.flip < 180)
            {
                tValX *= -1;
            }
            await PlayerImage.TranslateTo(data.X + tValX, data.Y + tValY, 500, Easing.SpringOut);
            await PlayerImage.TranslateTo(data.X, data.Y, 500, Easing.Linear);
        }

        public async void rotateGetsHit(PlayerInfoModel data, ImageButton PlayerImage)
        {
            int rVal = -20;
            if (data.flip > 0)
            {
                rVal *= -1;
            }
            await PlayerImage.RotateTo(rVal, 500, Easing.SpringOut);
            await PlayerImage.RotateTo(0, 500, Easing.CubicIn);
        }

        //Assigns the selected monsters stats to their respective labels
        public void ShowMonsterStats(PlayerInfoModel data)
        {
            
            MonsterATK.Text = data.Attack.ToString();
            MonsterDEF.Text = data.Defense.ToString();
            MonsterRDEF.Text = data.RangedDefense.ToString();
            MonsterSPD.Text = data.Speed.ToString();
            MonsterHEALTH.Text = data.CurrentHealth.ToString();
            MonsterMAXHEALTH.Text = data.MaxHealth.ToString();
            MonsterNAME.Text = data.Name;
        }

        //Assigns the selected monsters stats to their respective labels
        public void HideMonsterStats(PlayerInfoModel data)
        {

            MonsterATK.Text = "";
            MonsterDEF.Text = "";
            MonsterRDEF.Text = "";
            MonsterSPD.Text = "";
            MonsterHEALTH.Text = "";
            MonsterMAXHEALTH.Text = "";
            MonsterNAME.Text = ""; ;
        }

        //applies visual indicators of who is selected
        public bool MonsterSelected(PlayerInfoModel data, bool clicked = false)
        {
            if (clicked) 
            {
                deselectMonster();
            }

            currentMonster = data;
            RedArrow.IsVisible = true;
            ShowMonsterStats(data);
            Grid.SetRow(RedArrow, data.X);
            Grid.SetColumn(RedArrow, data.Y);
            MonsterListGrid.Children.Add(RedArrow);
            currentMonster.selected = true;
            return currentMonster.selected;
        }

        //removes visual indicators of who is selected
        public void deselectMonster()
        {
            if (RedArrow.IsVisible == true && (Grid.GetColumn(RedArrow) == currentMonster.Y))
            {
                RedArrow.IsVisible = false;
                HidePlayerStats(currentMonster);
                currentMonster.selected = false;
            }
        }


        //Assigns the selected Players stats to their respective labels
        public bool ShowPlayerStats(PlayerInfoModel data)
        {
            CharacterATK.Text = data.Attack.ToString();
            CharacterDEF.Text = data.Defense.ToString();
            CharacterRDEF.Text = data.RangedDefense.ToString();
            CharacterSPD.Text = data.Speed.ToString();
            CharacterHEALTH.Text = data.CurrentHealth.ToString();
            CharacterMAXHEALTH.Text = data.MaxHealth.ToString();
            CharacterNAME.Text = data.Name;
            return true;
        }

        //Assigns the selected Players stats to their respective labels
        public void HidePlayerStats(PlayerInfoModel data)
        {
            CharacterATK.Text = "";
            CharacterDEF.Text = "";
            CharacterRDEF.Text = "";
            CharacterSPD.Text = "";
            CharacterHEALTH.Text = "";
            CharacterMAXHEALTH.Text = "";
            CharacterNAME.Text = ""; ;
        }

        //applies visual indicators of who is selected
        public bool PlayerSelected(PlayerInfoModel data, bool clicked = false)
        {
            if (clicked)
            {
                deselectPlayer();
            }

            currentAndroid = data;
            GoldArrow.IsVisible = true;
            ShowPlayerStats(data);
            Grid.SetRow(GoldArrow, data.X);
            Grid.SetColumn(GoldArrow, data.Y);
            PartyListGrid.Children.Add(GoldArrow);
            currentAndroid.selected = true;
            return currentAndroid.selected;
        }


        //removes visual indicators of who is selected
        public void deselectPlayer()
        {
            if (GoldArrow.IsVisible == true && Grid.GetColumn(GoldArrow) == currentAndroid.Y)
            {
                GoldArrow.IsVisible = false;
                HidePlayerStats(currentAndroid);
                currentAndroid.selected = false;
            }
        }

        public async void PotionButton_Clicked(object sender, EventArgs e)
        {

            if (potions > 0)
            {
                if (currentAndroid.CurrentHealth == currentAndroid.MaxHealth)
                {
                    potions--;
                }
                else
                {
                    currentAndroid.CurrentHealth = currentAndroid.MaxHealth;
                    potions--;
                }
            }
            PotionsLeft.Text = potions.ToString();
        }

        //normal attack selected
        public async void normAttack_Clicked(object sender, EventArgs e)
        {
            Deselect_Clicked(sender, e);
            EngineViewModel.Engine.BattleMessagesModel.normATK = true;
            NormAttack.BorderColor = Color.Gold;
        }

        //move 1 selected
        public async void move1_Clicked(object sender, EventArgs e)
        {
            Deselect_Clicked(sender, e);
            EngineViewModel.Engine.BattleMessagesModel.move1ATK = true;
            Move1.Text = currentAndroid.Move1;
            Move1.BorderColor = Color.Gold;
        }


        //move 2 selected
        public async void move2_Clicked(object sender, EventArgs e)
        {
            Deselect_Clicked(sender, e);
            EngineViewModel.Engine.BattleMessagesModel.move2ATK = true;
            Move2.BorderColor = Color.Gold;
        }

        //move deselected
        public async void Deselect_Clicked(object sender, EventArgs e)
        {
            EngineViewModel.Engine.BattleMessagesModel.normATK = false;
            EngineViewModel.Engine.BattleMessagesModel.move1ATK = false;
            EngineViewModel.Engine.BattleMessagesModel.move2ATK = false;
            NormAttack.BorderColor = Color.Aqua;
            Move1.BorderColor = Color.Aqua;
            Move2.BorderColor = Color.Aqua;
        }

        public void slaveSelectToDefender() 
        {
            if(EngineViewModel.Engine.BattleMessagesModel.defendingUnit.PlayerType == PlayerTypeEnum.Character)
            {
                currentAndroid = EngineViewModel.Engine.BattleMessagesModel.defendingUnit;
                PlayerSelected(currentAndroid);
            }
            else
            {
                currentMonster = EngineViewModel.Engine.BattleMessagesModel.defendingUnit;
                MonsterSelected(currentMonster);
            }
        }

        public void slaveSelectToAttacker()
        {
            if (EngineViewModel.Engine.BattleMessagesModel.attackingUnit.PlayerType == PlayerTypeEnum.Character)
            {
                currentAndroid = EngineViewModel.Engine.BattleMessagesModel.attackingUnit;
                PlayerSelected(currentAndroid);
            }
            else
            {
                currentMonster = EngineViewModel.Engine.BattleMessagesModel.attackingUnit;
                MonsterSelected(currentMonster);
            }
        }
        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AttackButton_Clicked(object sender, EventArgs e)
		{
            
            bool enemyTurn = EngineViewModel.Engine.BattleMessagesModel.EnemyTurn;
            Move1.Text = currentAndroid.Move1;
            Move2.Text = currentAndroid.Move2;
            if (!currentAndroid.Alive)
            {
                deselectPlayer();
            }
            if ((!currentMonster.selected && !enemyTurn) || !currentMonster.Alive && !(EngineViewModel.Engine.MonsterList.Count < 1))
            { 
                await DisplayAlert("No target", "Select an Enemy to attack", "Continue", "Cancel");
            }
            else
            {
                // Redraw Game Board
                // Show who is Attack in Who

                // Hold the current state
                var RoundCondition = EngineViewModel.Engine.RoundNextTurn();

                if (RoundCondition == RoundEnum.NewRound)
                {
                    // Show the New Monster List, and Items Gained
                    MonsterListGrid.Children.Clear();
                    EngineViewModel.Engine.NewRound();
                    ShowModalNewRoundPage();
                    ShowModalRoundOverPage();
                    initializeAllMonsters();
                    Debug.WriteLine("Round Over");

                }

                // Check for Game Over
                if (RoundCondition == RoundEnum.PrimeAssaultOver)
                {

                    // Wrap up
                    EngineViewModel.Engine.EndBattle();

                    // Let the Player Know it is over

                    var Score = EngineViewModel.Engine.BattleScore.ScoreTotal;
                    MessagingCenter.Send(this, "AddData", Score);

                    var outputString = "Game Over! Score " + Score.ToString();
                    Debug.WriteLine(outputString);




                    // Clear the players from the center of the board


                    // Save the Score to the Score View Model, by sending a message to it.

                    ShowModalPageGameOver();
                    

                    return;
                }

                // Output the Game Board of What Happened, update UI Etc.

                // Output The Message that happened.
                BattleMessages.Text = string.Format("{0} \n {1}", EngineViewModel.Engine.BattleMessagesModel.TurnMessage, BattleMessages.Text);

                if (!string.IsNullOrEmpty(EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage))
                {
                    BattleMessages.Text = string.Format("{0} \n {1}", EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
                }

                // TODO: TEAM
                ///
                /// Now that the turn is over, need to change the attacker and defender 
                /// In the UI game, the player would choose who to attack
                /// The monster would auto pick
                ///
                /// For this example, just setting it back to the Charcter, so they can wack on the monster until it ends...
                ///

                // Get the turn, set the current player and attacker to match
                EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();

                if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
                {
                    // User would select who to attack

                    // for now just auto selecting
                    EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);

                    return;
                }

                // Monsters turn, so auto pick a Character to Attack
                EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
                
            }
            if (!EngineViewModel.Engine.BattleMessagesModel.EnemyTurn)
            {
                //deselectMonster();
            }
            Deselect_Clicked(sender, e);
        }
        #region PageHandelers

        /// <summary>
        /// Battle Over
        /// Battle Over button shows when all characters are dead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void RoundOverButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RoundOverPage());
        }


        /// <summary>
        /// Battle Over
        /// Battle Over button shows when all characters are dead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NewRoundButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Battle Over
        /// Battle Over button shows when all characters are dead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleOverButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void QuitButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

            if (answer)
            {
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnGameOverClicked(object sender, EventArgs args)
        {
            var Score = EngineViewModel.Engine.BattleScore.ScoreTotal;
            var outputString = "Battle Over! Score " + Score.ToString();
            Debug.WriteLine(outputString);

            ShowModalPageGameOver();

            // Back up to the Start of Battle
            await Navigation.PopToRootAsync();
        }

        public void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == ModalNewRoundPage)
            {
                ModalNewRoundPage = null;

                // remember to remove the event handler:
                App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == ModalPickCharactersPage)
            {
                ModalPickCharactersPage = null;

                // remember to remove the event handler:
                App.Current.ModalPopping -= HandleModalPopping;
            }
        }

        /// <summary>
        /// Show the Page for New Round
        /// 
        /// Upcomming Monsters
        /// 
        /// </summary>
        public async void ShowModalNewRoundPage()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            App.Current.ModalPopping += HandleModalPopping;
            ModalNewRoundPage = new NewRoundPage();
            await Navigation.PushModalAsync(ModalNewRoundPage);
        }

        /// <summary>
        /// Show the Round Over Page
        /// 
        /// Item Picker results
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            App.Current.ModalPopping += HandleModalPopping;
            ModalRoundOverPage = new RoundOverPage();
            await Navigation.PushModalAsync(ModalRoundOverPage);
        }

        /// <summary>
        /// Show the Select Characters Page
        /// 
        /// Let User Pick the Characters for the battle
        /// 
        /// </summary>
        public async void ShowModalPageCharcterSelect()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            App.Current.ModalPopping += HandleModalPopping;
            ModalPickCharactersPage = new PickCharactersPage();
            await Navigation.PushModalAsync(ModalPickCharactersPage);
        }

        /// <summary>
        /// Show the Game Over Page
        ///
        /// All Done
        /// 
        /// </summary>
        public async void ShowModalPageGameOver()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            App.Current.ModalPopping += HandleModalPopping;
            ModalBattleGameOverPage = new ScorePage();
            await Navigation.PushModalAsync(ModalBattleGameOverPage);

        }
        #endregion PageHandelers

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////                                        //////////////////////////////////////////
        /////////////////////////////////////////////////            Character Select            //////////////////////////////////////////
        /////////////////////////////////////////////////                                        //////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}