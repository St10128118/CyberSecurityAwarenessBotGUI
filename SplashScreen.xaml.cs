using CyberSecurityAwarenessBot;
using System.Windows;

namespace CyberSecurityAwarenessBotGUI
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            // ensure the window opens centered and above other windows
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
        }
    }
}
