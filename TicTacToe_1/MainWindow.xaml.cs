using System.Windows;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }






        #endregion

        #region PrivateMembers
        /// <summary>
        /// Array to hold results
        /// </summary>
        private MarkType[] Results;

        /// <summary>
        /// True for P1 - X, False P2 - O
        /// </summary>
 
        private bool Player1Turn;

        /// <summary>
        /// Has game ended? 
        /// </summary>
        private bool GameEnded;



        #endregion

        #region NewGame
        /// <summary>
        /// New Game
        /// </summary>
        public void NewGame()
        {
            //create blank array
            Results = new MarkType[9];

            //make sure all current state is free (at start)
            for(var i = 0; i < Results.Length; i++)
            {
                Results[i] = MarkType.Free;
            }

            //make sure p1 is current
            Player1Turn = true;

            ///
            /// Iterate every button on grid and clear text
            ///

            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });

            ///
            /// make sure no reset
            ///
            GameEnded = false;

        }
        #endregion

        #region ButtonClickEventHandler

        /// <summary>
        /// Button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event of click</param>


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // start new game on click
            if (GameEnded)
            {
                NewGame();

                return;
            }

            
            /// cast the sender to button
            
            var button = (Button)sender;

            /// find button position

            var column = Grid.GetColumn(button);

            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            /// do nothing if notfree

            if(Results[index]!= MarkType.Free)
            {
                return;
            }

            /// set cell value based on player turn

            Results[index] = Player1Turn ? MarkType.Cross : MarkType.Naught;

            button.Content = Player1Turn ? "X" : "O";

            //change player 2 red

            if (!Player1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Invert player turn
            Player1Turn ^= true;
            
            //Check end conditions
            CheckForWinner();
        }
        #endregion

        ///
        /// Check for winner
        ///

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins

            // Check for horizontal wins
            //
            //  - Row 0
            //
            if (Results[0] != MarkType.Free && (Results[0] & Results[1] & Results[2]) == Results[0])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_0_0.Background = Button_1_0.Background = Button_2_0.Background = Brushes.Green;

                //output winner
               
            }
            //
            //  - Row 1
            //
            if (Results[3] != MarkType.Free && (Results[3] & Results[4] & Results[5]) == Results[3])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_0_1.Background = Button_1_1.Background = Button_2_1.Background = Brushes.Green;
            }
            //
            //  - Row 2
            //
            if (Results[6] != MarkType.Free && (Results[6] & Results[7] & Results[8]) == Results[6])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_0_2.Background = Button_1_2.Background = Button_2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vertical Wins

            // Check for vertical wins
            //
            //  - Column 0
            //
            if (Results[0] != MarkType.Free && (Results[0] & Results[3] & Results[6]) == Results[0])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_0_0.Background = Button_0_1.Background = Button_0_2.Background = Brushes.Green;
            }
            //
            //  - Column 1
            //
            if (Results[1] != MarkType.Free && (Results[1] & Results[4] & Results[7]) == Results[1])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_1_0.Background = Button_1_1.Background = Button_1_2.Background = Brushes.Green;
            }
            //
            //  - Column 2
            //
            if (Results[2] != MarkType.Free && (Results[2] & Results[5] & Results[8]) == Results[2])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_2_0.Background = Button_2_1.Background = Button_2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins

            // Check for diagonal wins
            //
            //  - Top Left Bottom Right
            //
            if (Results[0] != MarkType.Free && (Results[0] & Results[4] & Results[8]) == Results[0])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_0_0.Background = Button_1_1.Background = Button_2_2.Background = Brushes.Green;
            }
            //
            //  - Top Right Bottom Left
            //
            if (Results[2] != MarkType.Free && (Results[2] & Results[4] & Results[6]) == Results[2])
            {
                // Game ends
                GameEnded = true;

                // Highlight winning cells in green
                Button_2_0.Background = Button_1_1.Background = Button_0_2.Background = Brushes.Green;
            }

            #endregion

            #region No Winners

            // Check for no winner and full board
            if (!Results.Any(f => f == MarkType.Free))
            {
                // Game ended
                GameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }

            #endregion
        }
    }
}
