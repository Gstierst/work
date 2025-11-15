using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StierstorferGA2
{
    public partial class Form1 : Form
    {
        //variables that will be used by following functions
        string[] words = null;
        string wordToBeGuessed = " ";
        int attemptsCounter = 0;

        /// <summary>
        /// Intializes components of the winForm? loads the textfile cotaining word to guess into the string array 
        /// and then calls a function to select a random word
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //store words from test file in the array of strings
            words = File.ReadAllLines("wordList.txt");
            SelectRandomWord();
        }

        /// <summary>
        /// Picks a random word from the list of words that were loaded from file
        /// </summary>
        private void SelectRandomWord()
        {
            Random randomWordFromList = new Random();
            //picks random words from the list based on the length of the string array 
            wordToBeGuessed = words[randomWordFromList.Next(words.Length)];
        }

        /// <summary>
        /// Iterates through each of the 5 textboxes then checks if its empty and then returns true or false
        /// </summary>
        /// <returns></returns false if not all text boxes are full, returns true if all textboxes are full>
        private bool TextBoxesFilled()
        {
            //iterate through each textbox
            for(int i =  1; i <= 5; i++)
            {
                //creates instance of the current text box
                TextBox currentTextBox = Controls["textBox" + i] as TextBox;
                if (currentTextBox.Text == "")
                {
                    return false;
                }
            }
            //if all textboxes are not empty return true
            return true;
        }

        /// <summary>
        /// Works just like the first TextBoxesFilled func, but has a change for the I variable that 
        /// allows it to work on later lines after the first guess
        /// </summary>
        /// <returns></returns false if not all text boxes are full, returns true if all textboxes are full>
        private bool TextBoxesFilledAfterFirstGuess()
        {
            //iterate through each textbox
            for (int i = 1; i <= 5; i++)
            {
                //changes the I variable to make sure that the instances of the current text box work for the later lines
                int newI = i + (5 * attemptsCounter);
                //creates instance of the current text box
                TextBox currentTextBox = Controls["textBox" + newI] as TextBox;
                if (currentTextBox.Text == "")
                {
                    return false;
                }
            }
            //if all textboxes are not empty return true
            return true;
        }

        /// <summary>
        /// Calls function to see if the textboxes are filled, if not filled prompts message,
        /// if filled it saves each character to a string for comparision with the word given.
        /// This comparison determines what color the text box should be changed to.
        /// </summary>
        private void CompareGuess()
        {
            if (TextBoxesFilled())
            {
                string userGuess = "";

                //iterate through each textbox
                for (int i = 1; i <= 5; i++)
                {
                    TextBox currentTextBox = Controls["textBox" + i] as TextBox;
                    //checks if current text box is not empty
                    if (currentTextBox.Text != "")
                    {
                        //adds character to the string holding the users guest
                        userGuess += currentTextBox.Text;
                    }
                }

                //loops through length of word 
                for (int i = 0; i < 5; i++)
                {
                    TextBox currentTextBox = Controls["textBox" + (i + 1)] as TextBox;
                    if(currentTextBox.Text != "")
                    {
                        //checks if character at specific interation is the same
                        if (wordToBeGuessed[i] == userGuess[i])
                        {
                            //change color of textbox if character is in same spot
                            currentTextBox.BackColor = Color.Green;
                        }
                        //checks if current character is within the word being guessed
                        else if (wordToBeGuessed.Contains(userGuess[i]))
                        {
                            //change color if character exist in word but not right position
                            currentTextBox.BackColor = Color.Gold;
                        }
                        // if the character is not in the word
                        else
                        {
                            //if character isnt in word at all
                            currentTextBox.BackColor = Color.Gray;
                        }
                    }
                }

                WinCondition(userGuess);
            }
            else
            {
                MessageBox.Show("You need 5 characters\n");
            }
        }

        /// <summary>
        /// Does the same as CheckGuess by has a change that increases the i value by 
        /// 5 times the attempts acounter to adjust for the addition lines it should read after.
        /// </summary>
        private void CompareGuessAfterFirstGuess()
        {
            if (TextBoxesFilledAfterFirstGuess())
            {
                string userGuess = "";

                //iterate through each textbox
                for (int i = 1; i <= 5; i++)
                {
                    int newI = i + (5 * attemptsCounter);
                    TextBox currentTextBox = Controls["textBox" + newI] as TextBox;
                    //checks if current text box is empty
                    if (currentTextBox.Text != "")
                    {
                        //adds character to the string holding the users guest
                        userGuess += currentTextBox.Text;
                    }
                }

                //loops through length of word 
                for (int i = 0; i < 5; i++)
                {
                    int newI = i + (5 * attemptsCounter);
                    TextBox currentTextBox = Controls["textBox" + (newI + 1)] as TextBox;
                    if (currentTextBox.Text != "")
                    {
                        //checks if character at specific interation is the same
                        if (wordToBeGuessed[i] == userGuess[i])
                        {
                            currentTextBox.BackColor = Color.Green;
                        }
                        //checks if current character is within the word being guessed
                        else if (wordToBeGuessed.Contains(userGuess[i]))
                        {
                            currentTextBox.BackColor = Color.Gold;
                        }
                        // if the character is not in the word
                        else
                        {
                            currentTextBox.BackColor = Color.White;
                        }
                    }
                }

                WinCondition(userGuess);
            }
            else
            {
                MessageBox.Show("You need 5 characters\n");
            }
        }


        /// <summary>
        /// Determines if the users guess is the same as the word being guess, 
        /// if so it prompts that you have guessed the word. If not, it increases the users attempts and 
        /// moves to the second line of text boxes. If attempts reach 6, then it prompts what the word was. 
        /// </summary>
        /// <param name="guess"> The string containing what the user entered in the text box</param>
        private void WinCondition(string guess)
        {
            if (guess == wordToBeGuessed)
            {
                MessageBox.Show("You have guessed the word!\n");
            }
            else
            {
                attemptsCounter++;
                if (attemptsCounter == 6)
                {
                    MessageBox.Show("The word was: " + wordToBeGuessed);
                }
            }
        }

        //name of game
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Runs the check guess function to determine whether or not the guess is the word
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (attemptsCounter == 0)
            {
                CompareGuess();
            }
            else
            {
                CompareGuessAfterFirstGuess();
            }
    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
