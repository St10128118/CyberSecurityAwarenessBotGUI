using System.Windows;

namespace CyberSecurityAwarenessBotGUI
{
    public partial class EditFactWindow : Window
    {
        public string Key { get; }
        public string Value => ValueText.Text;

        public EditFactWindow(string key, string value)
        {
            InitializeComponent();
            Key = key;
            KeyText.Text = key;
            ValueText.Text = value;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
