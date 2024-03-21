using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace X0_IoanOprea
{
    public partial class GameWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (X) or player 2's turn (0)
        /// </summary>
        private bool mPlayer1Turn;


        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion
        private string playerName;
        private char playerSign;

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameWindow(string name, char sign)
        {
            InitializeComponent();
            playerName = name;
            playerSign = sign;
            NewGame();
        }
        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {

            // Create a new blank array of free cells
            mResults = new MarkType[9];

            // Set all cells to free
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.LightSlateGray;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            // Find the index of the button
            var index = column + (row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on the human player's sign (X or 0)
            mResults[index] = MarkType.Cross;

            // Set button text to the human player's sign
            button.Content = playerSign.ToString();

            // Change 0 to red
            if (playerSign == '0')
                button.Foreground = Brushes.Red;

            // Check for a winner after human player's move
            CheckForWinner();

            // If the game has ended, return
            if (mGameEnded)
                return;

            // Computer's turn
            // Generate a random move for the computer
            var random = new Random();
            int computerMove;
            do
            {
                computerMove = random.Next(0, 9); // Generate random move index
            } while (mResults[computerMove] != MarkType.Free); // Keep generating until an empty cell is found

            // Set the cell value based on the computer's sign (opposite of human player's sign)
            mResults[computerMove] = MarkType.Zero;

            // Find the button corresponding to the computer's move
            var computerButton = Container.Children.OfType<Button>().ElementAt(computerMove);

            // Set button text to the computer's sign
            computerButton.Content = playerSign == 'X' ? "0" : "X";

            // Change 0 to red if player's sign is X
            if (playerSign == 'X')
                computerButton.Foreground = Brushes.Red;

            // Check for a winner after computer's move
            CheckForWinner();
        }



        /// <summary>
        /// Check if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            string message = string.Empty;

            // Check for horizontal wins
            //
            //  - Row 0
            //  
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

                message = mResults[0] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }
            //
            //  - Row 1
            //  
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

                message = mResults[3] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }
            //
            //  - Row 2
            //  
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

                message = mResults[6] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }

            // Check for vertical wins
            //
            //  - Col 0
            //  
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

                message = mResults[0] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }

            //
            //  - Col 1
            //  
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

                message = mResults[1] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }

            //
            //  - Col 2
            //  
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

                message = mResults[2] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }

            //
            //  - Diagonal 1
            //  
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

                message = mResults[0] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }

            //
            //  - Diagonal 2
            //  
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

                message = mResults[2] == MarkType.Cross ? $"{playerName}, ai câștigat!" : $"{playerName}, ai pierdut!";
            }

            // Check for no winner and full board
            if ((!mGameEnded) && (!mResults.Any(result => result == MarkType.Free)))
            {
                // Game ended
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

                message = "Egalitate";
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
            }
        }

    }
}
