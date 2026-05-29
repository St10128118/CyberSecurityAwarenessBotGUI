using CybersecurityAwarenessBot;
using CyberSecurityAwarenessBot;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberSecurityAwarenessBotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _sidebarWidth = 240;
        private string userName;

        public MainWindow(string _username)
        {
            userName = _username;
            InitializeComponent();
            WindowState = WindowState.Maximized;

            // Initial welcome message by the bot using the Actions class method AddMessage.
            Actions.AddMessage($"Hello! {userName}! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.", false, MessagesList);

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
            Actions.AddMessage(text, true, MessagesList);
            InputTextBox.Clear();

            // simulate processing delay
            await Actions.SimulateProcessingDelayAsync();

            // get bot reply (GenerateBotReply now also handles exit confirmation)
            var botReply = GenerateBotReply(text);
            if (!string.IsNullOrEmpty(botReply))
            {
                Actions.AddMessage(botReply, false, MessagesList);
            }
        }

        private bool IsExitCommand(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            var lower = text.ToLowerInvariant();
            // common farewell/exit phrases
            var exitPhrases = new[] { "exit", "quit", "goodbye", "good bye", "bye", "bye bye", "see you later", "see you", "farewell" };

            return exitPhrases.Any(p => lower.Contains(p));
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

        // Bot reply generation method - for now it checks if the user message matches a tip category and if so, it calls the TipBrowser.BrowseTips method to start browsing tips for that category.
        // Otherwise, it uses the BotResponses.GetBotResponse method to get a response based on the user input.
        private string GenerateBotReply(string userMessage)
        {
            if (IsExitCommand(userMessage))
            {
                var result = MessageBox.Show(
                    "Are you sure you want to exit?",
                    "Confirm Exit",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Application.Current?.Shutdown();
                    return null;
                }
                else
                {
                    // humorous reassurance from bot when user decides to stay
                    return "Phew — I'm glad you decided to stay! I've got more cybersecurity tips and witty remarks ready.";
                }
            }

            if (TipBrowser.HasTopic(userMessage))
            {
                return TipBrowser.BrowseTips(userMessage, userName);
            }
            else
            {
                return BotResponses.GetBotResponse(userMessage, userName);
            }
        }

        private void ConversationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ConversationList.SelectedItem;
            if (selectedItem is string selectedText && !string.IsNullOrEmpty(selectedText))
            {
                // we are just going to add the selected category as a user message and then generate a bot reply based on that category
                Actions.AddMessage(selectedText, true, MessagesList);
                var botReply = GenerateBotReply(selectedText);
                Actions.AddMessage(botReply, false, MessagesList);
            }
        }



        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Ask user to confirm exit
            var result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Close the application
                Application.Current?.Shutdown();
            }
            else
            {
                // User chose not to exit - bot adds a reassuring message to the conversation list
                Actions.AddMessage("Glad you want to hear some more...", false, MessagesList);
            }
        }

        private void NewConversationButton_Click(object sender, RoutedEventArgs e)
        {
            // a) Clear UI fields
            MessagesList.Items.Clear();
            InputTextBox.Clear();

            // b) Prompt the user with guidance for starting a new conversation
            Actions.AddMessage("Please choose a topic from the sidebar or type any of the available topics to begin.", false, MessagesList);

            // c) Give focus to the input box for convenience
            InputTextBox.Focus();
        }
    }
}
