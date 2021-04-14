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
    [Activity(Label = "EditDatabase")]
    public class EditDatabase : Activity
    {
        List<ScoreCard> myList;
        private Button btnEditDbBackToMainMenu;
        private Button btnDeleteEntry;
        private Spinner spinnerEditDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EditDatabase);

            SqlConnection mySqlConnectionClass = new SqlConnection(); // Sql Connection
            myList = mySqlConnectionClass.ViewAll();

            btnEditDbBackToMainMenu = FindViewById<Button>(Resource.Id.btnEditDbBackToMainMenu); // 1.button back to main menu
            btnEditDbBackToMainMenu.Click += btnEditDbBackToMainMenu_Click;

            btnDeleteEntry = FindViewById<Button>(Resource.Id.btnDeleteEntry); // 2. Button Delete
            btnDeleteEntry.Click += btnDeleteEntry_Click;
            btnDeleteEntry.Enabled = false;

            spinnerEditDB = FindViewById<Spinner>(Resource.Id.spinnereditDB);
            Resources.DataAdapterFile dtadptr = new Resources.DataAdapterFile(this, myList);   // 3. Spinner dropdown

            spinnerEditDB.Adapter = dtadptr;

            spinnerEditDB.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerEditDB_ItemSelected); 

        }

        private void btnEditDbBackToMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            var sqlConn = new SqlConnection();
            sqlConn.DeletePlayer(HangmanGameActivity.Id);
            myList = sqlConn.ViewAll();

            var dtadptr = new Resources.DataAdapterFile(this, myList);
            spinnerEditDB.Adapter = dtadptr;
        }

        private void spinnerEditDB_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            HangmanGameActivity.Id = this.myList.ElementAt(e.Position).Id;
            btnDeleteEntry.Enabled = true;
        }    
    }
}