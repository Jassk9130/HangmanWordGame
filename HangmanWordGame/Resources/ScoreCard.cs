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
using SQLite;


namespace HangmanWordGame.Resources
{
    class ScoreCard
    {
        [PrimaryKey, AutoIncrement]    //Column("_Id")]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int Score { get; set; }

        public override string ToString()
        {
            return string.Format("[ScoreCard: Id={0}, Name={1}, Score={2}]", Id, Name, Score);
        }
    }
}