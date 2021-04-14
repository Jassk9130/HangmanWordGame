using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HangmanWordGame.Resources;

namespace HangmanWordGame
{
    [Activity(Label = "HighScoreCardActivity")]
    public class HighScoreCardActivity : Activity
    {
        List<ScoreCard> myList;
        //public SqlConnection mySqlConnectionClass;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.HighScoreCard);

            SqlConnection mySqlConnectionClass = new SqlConnection();  //sql conn
            myList = mySqlConnectionClass.ViewAll();

            Button btnBackTomainmenu = FindViewById<Button>(Resource.Id.btnBackTomainmenu);
            btnBackTomainmenu.Click += BtnBackTomainmenu_Click;

            ListView HighScoresListView = FindViewById<ListView>(Resource.Id.HighScoresListView);  // Displays player names and high scores
            HighScoresListView.Adapter = new DataAdapterFile(this, myList);
        }

        private void BtnBackTomainmenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}