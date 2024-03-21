using System.Windows;

namespace X0_IoanOprea
{
    public partial class GameSettingsWindow : Window
    {
        public GameSettingsWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            string playerName = txtPlayerName.Text;
            char playerSign = rbX.IsChecked == true ? 'X' : '0';

            GameWindow gameWindow = new GameWindow(playerName, playerSign);
            gameWindow.ShowDialog();
            Close();
        }
    }
}
