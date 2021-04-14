using System;
using System.Collections.Generic;
using SQLite;
using HangmanWordGame.Resources;
using System.IO;

namespace HangmanWordGame
{
    class SqlConnection
    {
        private string dbpath { get; set; }
        private SQLiteConnection db { get; set; }

        public SqlConnection()
        {
            //Path String for Database File
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "HangmanWordGameDB.sqlite");

            //Set up the Db connection
            db = new SQLiteConnection(dbPath);

            db.CreateTable<ScoreCard>(); // Table of Records
        }

        public List<ScoreCard> ViewAll()   // Gets Stored Score from Scorecard.cs file
        {
            try
            {
                return db.Query<ScoreCard>("select *  from ScoreCard  ORDER BY Score DESC");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }

        public string UpdateScore(int id, string name, int score) // Updates the Score in db
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "HangmanWordGameDB.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new ScoreCard();

                item.Id = id;
                item.Name = name;
                item.Score = score;

                db.Update(item);
                return "Record Updated..!";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;

            }

        }

        public string InsertNewPlayer(string name, int score) // Adds New Palyer to db
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "HangmanWordGameDB.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new ScoreCard();
                item.Name = name;
                item.Score = score;

                db.Insert(item);
                return " Sucessfully added to the database !";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }

        }

        public string DeletePlayer(int id)       // Deletes Player info from the db.
        {

            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "HangmanWordGameDB.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new ScoreCard();
                item.Id = id;


                db.Delete(item);
                return "Sucessfully deleted from the database !";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }


        }
    }
}


        

    
