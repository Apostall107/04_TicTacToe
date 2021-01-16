using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _04_TicTacToe
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constr
        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }
        #endregion


        #region private Members

        /// <summary>
        /// Holds the current results of cells in the actice game
        /// </summary>
        MarkType[] _Results;
        bool _Player1Turn;
        bool _GameEnded;
        #endregion

        private void NewGame()
        {
            _Results = new MarkType[9];

            for (int i = 0; i < _Results.Length; i++)
            {
                _Results[i] = MarkType.Free;
            }


            _Player1Turn = true;

            Container.Children.Cast<Button>().ToList()//clearing game fields
                .ForEach(button =>
                {

                    button.Content = string.Empty;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Red;//set default colour of mark

                });

            _GameEnded = false;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (_GameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;


            //
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);//get index for list

            //no actions on click
            if (_Results[index] != MarkType.Free)
                return;

            _Results[index] = _Player1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = _Player1Turn ? "X" : "0";


            CheckForWinner();

            PlayerSwitch(button);
            
            



        }

        private void PlayerSwitch(Button button)
        {
            _Player1Turn = !_Player1Turn;

            if (_Player1Turn)
                button.Foreground = Brushes.Red;
            else
                button.Foreground = Brushes.Blue;
        }

        private void CheckForWinner()
        {
            #region Horizontal Wins

            // Check for horizontal wins
            //
            //  - Row 0
            //
            if (_Results[0] != MarkType.Free && (_Results[0] & _Results[1] & _Results[2]) == _Results[0])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            //  - Row 1
            //
            if (_Results[3] != MarkType.Free && (_Results[3] & _Results[4] & _Results[5]) == _Results[3])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            //  - Row 2
            //
            if (_Results[6] != MarkType.Free && (_Results[6] & _Results[7] & _Results[8]) == _Results[6])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vertical Wins

            // Check for vertical wins
            //
            //  - Column 0
            //
            if (_Results[0] != MarkType.Free && (_Results[0] & _Results[3] & _Results[6]) == _Results[0])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            //  - Column 1
            //
            if (_Results[1] != MarkType.Free && (_Results[1] & _Results[4] & _Results[7]) == _Results[1])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            //  - Column 2
            //
            if (_Results[2] != MarkType.Free && (_Results[2] & _Results[5] & _Results[8]) == _Results[2])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins

            // Check for diagonal wins
            //
            //  - Top Left Bottom Right
            //
            if (_Results[0] != MarkType.Free && (_Results[0] & _Results[4] & _Results[8]) == _Results[0])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //
            //  - Top Right Bottom Left
            //
            if (_Results[2] != MarkType.Free && (_Results[2] & _Results[4] & _Results[6]) == _Results[2])
            {
                // Game ends
                _GameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No Winners

            // Check for no winner and full board
            if (!_Results.Any(f => f == MarkType.Free))
            {
                _GameEnded = true;
               
                Container.Children.Cast<Button>().ToList()
                    .ForEach(button =>
                {
                    button.Background = Brushes.Yellow;
                });

            }

            #endregion
        }


    }





}
