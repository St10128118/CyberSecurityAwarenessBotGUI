using CyberSecurityAwarenessBot;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;

namespace CyberSecurityAwarenessBotGUI
{
    /// <summary>
    /// Interaction logic for Start_Up.xaml
    /// </summary>
    public partial class Start_Up : Window
    {
        public Start_Up()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            // Play WAV greeting audio for greeting
            Actions.PlayVoiceOrSound("greeting_audio.wav");
        }

        private void proceed(object sender, RoutedEventArgs e)
        {
            home_grid.Visibility = Visibility.Hidden;
            username_grid.Visibility = Visibility.Visible;

            // Play WAV greeting audio for greeting
            Actions.PlayVoiceOrSound("greeting.wav");
        }

        private void submit_name(object sender, RoutedEventArgs e)
        {
            string username = usernames_input.Text.Trim();
            if (string.IsNullOrWhiteSpace(username) || !Regex.IsMatch(username, @"^[a-zA-Z\s'-]{2,50}$"))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }
            MainWindow mainWindow = new MainWindow(username);
            mainWindow.Show();
            this.Close();
        }
    }
}
