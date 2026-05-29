using System.Configuration;
using System.Data;
using System.Windows;

namespace CyberSecurityAwarenessBotGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Show splash screen first
            var splash = new SplashScreen();
            splash.Show();

            // Keep it visible for a bit
            await Task.Delay(3000);

            // Open main app window
            var startUpWindow = new Start_Up();
            MainWindow = startUpWindow;
            startUpWindow.Show();

            // Close splash
            splash.Close();
        }
    }

}
