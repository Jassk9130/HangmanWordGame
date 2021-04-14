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


namespace HangmanWordGame.Resources
{

    
    class DataAdapterFile : BaseAdapter<ScoreCard> // 
    {
        private readonly Activity context;
        private readonly List<ScoreCard> allItems;

        public DataAdapterFile(Activity context, List<ScoreCard> items)
        {
            allItems = items;
            this.context = context;
        }

        public override int Count
        {
            get { return allItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ScoreCard this[int position]
        {
            get { return allItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var row = convertView;

            if (row == null)     //Layout Inflator = Instantiates a layout XML file into its corresponding View objects
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.ListViewRow, null, false);
            }

            // Set the textViewRowName.Text which is in the ListViewRow layout to Players Name

            TextView txtRowName = row.FindViewById<TextView>(Resource.Id.txtrowName);
            txtRowName.Text = allItems[position].Name;

            // Set the textViewRowName.Text in the  ListViewRow to the Players score
            TextView txtRowScore = row.FindViewById<TextView>(Resource.Id.txtrowScore);
            txtRowScore.Text = allItems[position].Score.ToString();

            return row;


        }
    }
}