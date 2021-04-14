using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using SQLite;

namespace HangmanWordGame
{
    [Activity(Label = "HangmanWordGame", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
                
            SetContentView(Resource.Layout.activity_main);

           Button btnStartNewGameScreen = FindViewById<Button>(Resource.Id.btnStartNewGameScreen); // btn Start New Game
            btnStartNewGameScreen.Click += BtnStartNewGameScreen_Click;

            Button btnHighScore = FindViewById<Button>(Resource.Id.btnHighScore);  // btn High Score
            btnHighScore.Click += btnHighScore_Click;

            Button btnEditDatabase = FindViewById<Button>(Resource.Id.btnEditDatabase); // btn Edit Database
            btnEditDatabase.Click += btnEditDatabase_Click;

            Button btnQuitGame = FindViewById<Button>(Resource.Id.btnQuitGame); // Btn Quit
            btnQuitGame.Click += btnQuitGame_Click;
        }

        private void BtnStartNewGameScreen_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(PlayerProfile));
        }

        private void btnHighScore_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HighScoreCardActivity));
        }

        private void btnEditDatabase_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditDatabase));
        }

        private void btnQuitGame_Click(object sender, EventArgs e) // Quits the Game 
        {
            System.Environment.Exit(0);
        }
    }
}