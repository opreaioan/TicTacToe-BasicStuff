using System;
using System.Windows;

namespace X0_IoanOprea
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            GameSettingsWindow settingsWindow = new GameSettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Student: Oprea Ioan\nE-mail: ioan-g.oprea@student.unitbv.ro\nData: {DateTime.Now}\nDescriere: A simple X0 game.", "Despre");
        }
    }
}
