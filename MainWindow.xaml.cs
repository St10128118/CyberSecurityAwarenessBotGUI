using CybersecurityAwarenessBot;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityAwarenessBotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _sidebarWidth = 240;

        public MainWindow()
        {
            InitializeComponent();

            // Example welcome message
            AddMessage("Hello — I'm your Cyber Security Awareness Bot. Ask me anything about security best practices.", false);

            // populate ConversationList with keys from TipBrowser's dictionary of tips
            PopulateConversationList();
        }

        private void SidebarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftPanel.Visibility == Visibility.Visible && LeftColumn.Width.Value > 0)
            {
                // hide
                _sidebarWidth = LeftColumn.Width.Value;
                LeftColumn.Width = new GridLength(0);
                LeftPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                // show
                LeftColumn.Width = new GridLength(_sidebarWidth);
                LeftPanel.Visibility = Visibility.Visible;
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await SubmitMessageAsync();
        }

        private async void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                e.Handled = true; // prevent newline
                await SubmitMessageAsync();
            }
        }

        private async Task SubmitMessageAsync()
        {
            var text = InputTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(text))
                return;

            // add user message
            AddMessage(text, true);
            InputTextBox.Clear();

            // small delay to simulate processing
            await Task.Delay(400);

            // placeholder bot reply - replace with real bot integration
            var botReply = GenerateBotReply(text);
            AddMessage(botReply, false);
        }

        private void AddMessage(string text, bool isUser)
        {
            // create a simple bubble: Border containing a TextBlock
            var tb = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Color.FromRgb(17, 24, 39)),
                MaxWidth = 520,
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left

            };

            var border = new Border
            {
                Child = tb,
                Padding = new Thickness(10),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(4),
                Background = isUser ? new SolidColorBrush(Color.FromRgb(220, 248, 198)) : new SolidColorBrush(Color.FromRgb(241, 245, 249)),
            };
            MessagesList.Items.Add(border);
            ScrollToEnd();
        }

        private void ScrollToEnd()
        {
            if (MessagesList.Items.Count == 0)
                return;

            var last = MessagesList.Items[MessagesList.Items.Count - 1];
            MessagesList.Dispatcher.InvokeAsync(() => MessagesList.ScrollIntoView(last));
        }

        // Populate the ConversationList ListBox with the keys of the tips dictionary found on TipBrowser.
        private void PopulateConversationList()
        {
            ConversationList.Items.Clear();
            var categories = TipBrowser.GetTipCategories() as List<string>;
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    ConversationList.Items.Add(category);
                }
            }
        }

        // Very simple placeholder response generator; replace with your bot or service call.
        private string GenerateBotReply(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return "Could you rephrase that?";

            var lower = userMessage.ToLowerInvariant();
            if (lower.Contains("phish") || lower.Contains("email"))
                return "Phishing indicators include urgent requests, unknown senders, and suspicious links. Don't click links—hover to inspect them and verify sender.";
            if (lower.Contains("password"))
                return "Use a passphrase or a password manager. Enable multi-factor authentication wherever possible.";
            if (lower.Contains("help") || lower.Contains("how"))
                return "Tell me what you're trying to do and I'll guide you step-by-step.";
            return "Thanks — I received that. For detailed guidance you can ask about phishing, passwords, MFA, or secure browsing.";
        }
    }
}
