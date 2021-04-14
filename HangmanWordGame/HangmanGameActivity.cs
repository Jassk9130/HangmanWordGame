using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HangmanWordGame.Resources;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Stream = System.IO.Stream;


namespace HangmanWordGame
{
    [Activity(Label = "HangmanGameActivity")]
    public class HangmanGameActivity : Activity
    {
        //All Buttons A-Z
        private Button btnA;
        private Button btnB;
        private Button btnC;
        private Button btnD;
        private Button btnE;
        private Button btnF;
        private Button btnG;
        private Button btnH;
        private Button btnI;
        private Button btnJ;
        private Button btnK;
        private Button btnL;
        private Button btnM;
        private Button btnN;
        private Button btnO;
        private Button btnP;
        private Button btnQ;
        private Button btnR;
        private Button btnS;
        private Button btnT;
        private Button btnU;
        private Button btnV;
        private Button btnW;
        private Button btnX;
        private Button btnY;
        private Button btnZ;

        private TextView txtWordToGuess;
        private Button gamebtnMainMenu;
        private Button btnStartGame;
        private ImageView imgHangmanWordGame;
        private TextView txtCurrentScore;
        private TextView txtGuessesLeft;

        public static int Id;
        public static string PlayerName;
        public static int score;
        private string letter;
        private string rand;

        private int GuessesLeft = 8;

        private char[] wordToGuess;
        private char[] HiddenWord;

        private bool GuessedCorrect;

        private List<string> wordList = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainGame_layout);
            LoadWords();

            gamebtnMainMenu = FindViewById<Button>(Resource.Id.gamebtnMainMenu);
            gamebtnMainMenu.Click += gamebtnMainMenu_Click;

            btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);               // All text and buttons from MainGame_layout
            btnStartGame.Click += btnStartGame_Click;          

            txtCurrentScore = FindViewById<TextView>(Resource.Id.txtCurrentScore);
            txtCurrentScore.Text = score.ToString();

            txtGuessesLeft = FindViewById<TextView>(Resource.Id.txtGuessesLeft);
            txtGuessesLeft.Text = GuessesLeft.ToString();

            txtWordToGuess = FindViewById<TextView>(Resource.Id.txtWordToGuess);

            btnA = FindViewById<Button>(Resource.Id.btnA);  //btns A - Z
            btnB = FindViewById<Button>(Resource.Id.btnB);
            btnC = FindViewById<Button>(Resource.Id.btnC);
            btnD = FindViewById<Button>(Resource.Id.btnD);
            btnE = FindViewById<Button>(Resource.Id.btnE);
            btnF = FindViewById<Button>(Resource.Id.btnF);
            btnG = FindViewById<Button>(Resource.Id.btnG);
            btnH = FindViewById<Button>(Resource.Id.btnH);
            btnI = FindViewById<Button>(Resource.Id.btnI);
            btnJ = FindViewById<Button>(Resource.Id.btnJ);
            btnK = FindViewById<Button>(Resource.Id.btnK);
            btnL = FindViewById<Button>(Resource.Id.btnL);
            btnM = FindViewById<Button>(Resource.Id.btnM);
            btnN = FindViewById<Button>(Resource.Id.btnN);
            btnO = FindViewById<Button>(Resource.Id.btnO);
            btnP = FindViewById<Button>(Resource.Id.btnP);
            btnQ = FindViewById<Button>(Resource.Id.btnQ);
            btnR = FindViewById<Button>(Resource.Id.btnR);
            btnS = FindViewById<Button>(Resource.Id.btnS);
            btnT = FindViewById<Button>(Resource.Id.btnT);
            btnU = FindViewById<Button>(Resource.Id.btnU);
            btnV = FindViewById<Button>(Resource.Id.btnV);
            btnW = FindViewById<Button>(Resource.Id.btnW);
            btnX = FindViewById<Button>(Resource.Id.btnX);
            btnY = FindViewById<Button>(Resource.Id.btnY);
            btnZ = FindViewById<Button>(Resource.Id.btnZ);

            DisableButtons();  // Disable button function
            imgHangmanWordGame = FindViewById<ImageView>(Resource.Id.imgHangmanWordGame);
            DefaultImage();

            btnA.Click += Letter_Click;
            btnB.Click += Letter_Click;
            btnC.Click += Letter_Click;
            btnD.Click += Letter_Click;
            btnE.Click += Letter_Click;
            btnF.Click += Letter_Click;
            btnG.Click += Letter_Click;
            btnH.Click += Letter_Click;
            btnI.Click += Letter_Click;
            btnJ.Click += Letter_Click;
            btnK.Click += Letter_Click;
            btnL.Click += Letter_Click;
            btnM.Click += Letter_Click;
            btnN.Click += Letter_Click;
            btnO.Click += Letter_Click;
            btnP.Click += Letter_Click;
            btnQ.Click += Letter_Click;
            btnR.Click += Letter_Click;
            btnS.Click += Letter_Click;
            btnT.Click += Letter_Click;
            btnU.Click += Letter_Click;
            btnV.Click += Letter_Click;
            btnW.Click += Letter_Click;
            btnX.Click += Letter_Click;
            btnY.Click += Letter_Click;
            btnZ.Click += Letter_Click;
        }

        private void Letter_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            
            var clickedbutton = (Button)sender; //clicked btn        
            clickedbutton.Enabled = false;
           
            letter = clickedbutton.Text;
            letter = letter.ToUpper();
            
            for (int i = 0; i < HiddenWord.Length; i++)
            {
                // if the "letter" of the button clicked matches a letter of the word we are trying to guess
                if (letter == wordToGuess[i].ToString())
                {
                    // The position of the letter(i) in the word that is hidden(with underscores) is set.
                    HiddenWord[i] = letter.ToCharArray()[0];
                    txtWordToGuess.Text = string.Join(" ", HiddenWord);
                  
                    ScoringSystem();  // Run the "Scoring" method from below. Add score based upon the letter guessed.
                    ScoreUpdate();

                    GuessedCorrect = true;
                }

            }
            // If the GuessedCorrect condition is false, reduce the "GuessesLeft" by 1
            if (GuessedCorrect == false)
            {
                GuessesLeft = GuessesLeft - 1;

                GuessOver();
                GuessedWrongTextUpdate();
                ScoreUpdate();
            }
            else
            { // Set GuessedCorrect back to False for the next round
                GuessedCorrect = false;
            }

            // If the hidden word does not have underscores left(meaning it is a complete word), the game is won.
            if (!HiddenWord.Contains('_'))

            {
                GameWin();
            }
        }

        private void gamebtnMainMenu_Click(object sender, EventArgs e)  // Main Menu Button Click goes back to main activity page
        {
            StartActivity(typeof(MainActivity));
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            btnStartGame.Enabled = false;
            LoadNewRandomWord();                         //Load new words while disabling game and loading default img
            btnStartGame.Enabled = false;
            DefaultImage();
        }

        ///////////////////////////////*******FUNCTIONS************//////////////////////////////////////////

        private void LoadWords()                         //1. Function to Load words
        {
            Stream myStream = Assets.Open("Words.txt"); // Opens the words.textfile using streamReader to read the file
            using (StreamReader sr = new StreamReader(myStream))
            {
                string line;            
                while ((line = sr.ReadLine()) != null)  // while line is not empty, add words
                { 
                    wordList.Add(line);
                }
            }
        }

        private void LoadNewRandomWord()     //2. Function to load new Random Word from the List
        {
            ButtonEnable(); //  Enable the A-Z buttons
            GuessesLeft = 8; // Set the "guesses left" to 8

            Random randomGen = new Random();        //choose a random word from the words.txt
            rand = wordList[randomGen.Next(wordList.Count)];
            rand = rand.ToUpper();  //sets to uppercase
            wordToGuess = rand.ToArray(); //converts word to an array
 
            HiddenWord = new char[wordToGuess.Length];

            for (int i = 0; i < HiddenWord.Length; i++)
            {
                HiddenWord[i] = '_';  
                txtWordToGuess.Text = string.Join(" ", HiddenWord);
            }
        }

        private void DefaultImage()     //3. Default Image Function
        {
            imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang6);
        }

        private void GuessedWrongTextUpdate()  //4. Function if guessed the wrong letter and it tells the guesses left!
        {
            txtGuessesLeft.Text = GuessesLeft.ToString();
        }

        private void ScoreUpdate() //5. Function to show Score Update
        {
            txtCurrentScore.Text = score.ToString();
        }

        private void GuessOver() //6. GuessOver function
        {
            if (GuessesLeft == 0)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.gameover_lose);
                System.Threading.Thread.Sleep(2000);// wait time
                Toast.MakeText(this, " Sorry! You LOOSE ! Your Score : " + score, ToastLength.Short).Show(); //Toast = view class for message
                
                var sqlconn = new SqlConnection();
                sqlconn.UpdateScore(Id, PlayerName, score);
                System.Threading.Thread.Sleep(500);

                StartActivity(typeof(MainActivity));              
            }
            else if (GuessesLeft == 1)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang0);
            }
            else if (GuessesLeft == 2)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang1);
            }
            else if (GuessesLeft == 3)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang2);
            }
            else if (GuessesLeft == 4)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang3);
            }
            else if (GuessesLeft == 5)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang4);
            }
            else if (GuessesLeft == 6)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang5);
            }
            else if (GuessesLeft == 7)
            {
                imgHangmanWordGame.SetImageResource(Resource.Mipmap.hang6);
            }       
        }

        private void ScoringSystem()  //7. Score System function
        {
            switch (letter)
            {               
                case "B":
                case "C":
                case "D":               
                case "F":
                case "G":
                case "H":
                case "J":
                case "K":
                case "L":
                case "M":   // All letters 1 pt 
                case "N":
                case "P":
                case "Q":
                case "R":
                case "S":
                case "T":
                case "V":
                case "W":
                case "X":
                case "Y":
                case "Z":                   
                    score = score + 1;
                    break;

                case "A":
                case "E":  //All Wovels get 2 pts
                case "I":
                case "0":
                       score = score + 2;
                    break;             
            }
        }

        private void GameWin()
        {
            imgHangmanWordGame.SetImageResource(Resource.Mipmap.gameover_win);

            Toast.MakeText(this, " Yay ! Youu Win !", ToastLength.Short).Show();
            
            System.Threading.Thread.Sleep(500);
            DefaultImage();

            var SqlConn = new SqlConnection();
            SqlConn.UpdateScore(Id, PlayerName, score);          
            LoadNewRandomWord();
        }
        private void ButtonEnable()             //9. Function to enable all the buttons
        {
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnI.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            btnM.Enabled = true;
            btnN.Enabled = true;
            btnO.Enabled = true;
            btnP.Enabled = true;
            btnQ.Enabled = true;
            btnR.Enabled = true;
            btnS.Enabled = true;
            btnT.Enabled = true;
            btnU.Enabled = true;
            btnV.Enabled = true;
            btnW.Enabled = true;
            btnX.Enabled = true;
            btnY.Enabled = true;
            btnZ.Enabled = true;
            btnStartGame.Enabled = true;
        }

        private void DisableButtons()        //7. Function to Disable all the buttons.
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnG.Enabled = false;
            btnH.Enabled = false;
            btnI.Enabled = false;
            btnJ.Enabled = false;
            btnK.Enabled = false;
            btnL.Enabled = false;
            btnM.Enabled = false;
            btnN.Enabled = false;
            btnO.Enabled = false;
            btnP.Enabled = false;
            btnQ.Enabled = false;
            btnR.Enabled = false;
            btnS.Enabled = false;
            btnT.Enabled = false;
            btnU.Enabled = false;
            btnV.Enabled = false;
            btnW.Enabled = false;
            btnX.Enabled = false;
            btnY.Enabled = false;
            btnZ.Enabled = false;
        }
    }
}