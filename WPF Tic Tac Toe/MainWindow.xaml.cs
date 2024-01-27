using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int playerTurn = 1;
        public int[,] gameState = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        public bool gameEnd = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameEnd)
            {
                var button = sender as Button;
                var code = button.Tag;
                button.Content = playerTurn == 1 ? "X" : "O";
                int position = Int32.Parse(code.ToString());
                MarkPosition(button, position);
                gameEnd = CheckWinner();
                if (gameEnd)
                {
                    MainText.Text = "Player " + playerTurn + " Won";
                    return;
                }
                if (CheckDraw(gameState))
                {
                    MainText.Text = "No Winner";
                    return;
                }
                ChangeTurn();
            }

        }
        private void MarkPosition(Button button, int position)
        {

            if (playerTurn == 1)
            {
                button.Background = Brushes.LightGreen;
                button.Foreground = Brushes.Blue;
            }
            else
            {
                button.Background = Brushes.LightGreen;
                button.Foreground = Brushes.Red;
            }
            int row = (position - 1) / 3;
            int col = (position - 1) % 3;

            gameState[row, col] = playerTurn;
        }
        private void ChangeTurn()
        {
            _ = playerTurn == 1 ? playerTurn = 2 : playerTurn = 1;
            MainText.Text = "Player Turn: " + playerTurn;
        }
        private bool CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                // Check rows
                if (AreEqual(gameState[i, 0], gameState[i, 1], gameState[i, 2]) && gameState[i, 0] != 0)
                    return true;

                // Check columns
                if (AreEqual(gameState[0, i], gameState[1, i], gameState[2, i]) && gameState[0, i] != 0)
                    return true;
            }

            // Check diagonals
            if ((AreEqual(gameState[0, 0], gameState[1, 1], gameState[2, 2]) || AreEqual(gameState[0, 2], gameState[1, 1], gameState[2, 0])) && gameState[1, 1] != 0)
                return true;



            return false;
        }

        private bool CheckDraw(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool AreEqual(int a, int b, int c)
        {
            return a == b && b == c;
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            int[,] resetMatrix = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            playerTurn = 1;
            gameState = resetMatrix;
            MainText.Text = "Player Turn: " + playerTurn;
            gameEnd = false;

            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (Button button in buttons)
            {
                button.Content = "";
            }
        }
    }
}