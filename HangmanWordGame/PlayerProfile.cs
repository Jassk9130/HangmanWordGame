using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HangmanWordGame.Resources;


namespace HangmanWordGame
{
    [Activity(Label = "PlayerProfile")]
    public class PlayerProfile : Activity
    {
        private Spinner SelectPlayerSpinner;
        public TextView txtEnterPlayerName;
        List<ScoreCard> myList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddPlayerProfile);

            SqlConnection mySqlConnectionClass = new SqlConnection();
            myList = mySqlConnectionClass.ViewAll();

            SelectPlayerSpinner = FindViewById<Spinner>(Resource.Id.selectPlayerSpinner); //Palyer dropdown
            DataAdapterFile dtadptr = new Resources.DataAdapterFile(this, myList);

            SelectPlayerSpinner.Adapter = dtadptr;

            SelectPlayerSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerDropdown_ItemSelected);

            txtEnterPlayerName = FindViewById<TextView>(Resource.Id.txtEnterName); //Enter Player name textview

            Button btnAddPlayer = FindViewById<Button>(Resource.Id.btnAddProfile); //button Add Player Profile
            btnAddPlayer.Click += btnAddPlayer_Click;

            Button btnGobackToMainMenu = FindViewById<Button>(Resource.Id.btnGobackToMainMenu);

            Button btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame); //button Start Game
            btnStartGame.Click += btnStartGame_Click;

            btnGobackToMainMenu.Click += btnGobackToMainMenu_Click;  //button back to Main Menu
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)//Button_click Add Player
        {
            
            if (txtEnterPlayerName.Text.Length > 0)   // If word length is > 0 do this else display msg
            {
                
                HangmanGameActivity.PlayerName = txtEnterPlayerName.Text.ToString();
                HangmanGameActivity.score = 0;  // Start score @ 0.

                var sqlConn = new SqlConnection();               
                sqlConn.InsertNewPlayer(HangmanGameActivity.PlayerName, HangmanGameActivity.score); // Inserts the Players name and score 

                myList = sqlConn.ViewAll();  // update the list

                var dtadptr = new Resources.DataAdapterFile(this, myList);   //DataAdapterFile.cs
                SelectPlayerSpinner.Adapter = dtadptr;  //displays the updated list on the spinner

                Toast.MakeText(this, "Add Sucessful!", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Please Enter A Valid Name!", ToastLength.Short).Show(); // Toast(view) ie: displays message
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e) //button_click Start Game starts Hangman Game activity
        {
            StartActivity(typeof(HangmanGameActivity));
        }
        private void btnGobackToMainMenu_Click(object sender, EventArgs e) //button_click back to main menu starts Main activity
        {
            StartActivity(typeof(MainActivity));
        }


        private void SpinnerDropdown_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)  // Spinner/dropdown function
        {
            Spinner spinner = (Spinner)sender;  // Player Name and their score is collected from here 

            HangmanGameActivity.Id = this.myList.ElementAt(e.Position).Id;
            HangmanGameActivity.PlayerName = this.myList.ElementAt(e.Position).Name;
            HangmanGameActivity.score = this.myList.ElementAt(e.Position).Score;
        }
    }
}