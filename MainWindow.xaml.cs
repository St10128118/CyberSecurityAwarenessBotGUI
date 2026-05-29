using CybersecurityAwarenessBot;
using CyberSecurityAwarenessBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.ComponentModel;
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
        private ChatBotMemoryEngine _memoryEngine;
        // history: list of conversations; each conversation is a list of lines like "User: ..." or "Bot: ..."
        private List<List<string>> conversationHistory = new List<List<string>>();
        private List<string> currentConversation = new List<string>();
        private readonly string historyFilePath;

        public MainWindow(string _username)
        {
            userName = _username;
            InitializeComponent();
            WindowState = WindowState.Maximized;

            // history file path in same app data folder used by memory engine
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = Path.Combine(appData, "CyberSecurityAwarenessBotGUI");
            Directory.CreateDirectory(folder);
            historyFilePath = Path.Combine(folder, "history.json");

            // subscribe to closing to persist conversation history
            this.Closing += MainWindow_Closing;

            // load persisted conversation history (if any)
            LoadConversationHistory();

            // initialize memory engine
            _memoryEngine = new ChatBotMemoryEngine();

            // Initial welcome message by the bot using the Actions class method AddMessage.
            var welcome = $"Hello! {userName}! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.";
            Actions.AddMessage(welcome, false, MessagesList);
            // record into current conversation history
            AddMessageToHistory(welcome, false);

            // populate ConversationList with keys from TipBrowser's dictionary of tips
            PopulateConversationList();
        }

        private void MemoryButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new MemoryWindow(_memoryEngine);
            win.Owner = this;
            win.ShowDialog();
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
            AddMessageToHistory(text, true);
            InputTextBox.Clear();

            // simulate processing delay
            await Actions.SimulateProcessingDelayAsync();

            // get bot reply (GenerateBotReply now also handles exit confirmation)
            var botReply = GenerateBotReply(text);
            if (!string.IsNullOrEmpty(botReply))
            {
                Actions.AddMessage(botReply, false, MessagesList);
                AddMessageToHistory(botReply, false);
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
            // normalize input by removing common punctuation (question marks, periods, commas, etc.) so
            // intent matching is not affected by trailing punctuation.
            var cleaned = CleanInput(userMessage);

            // let memory engine handle memory commands first (remember/recall)
            var memResp = _memoryEngine.GetResponse(cleaned);
            if (!string.IsNullOrEmpty(memResp))
                return memResp;

            if (IsExitCommand(cleaned))
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
                    return BotResponses.GetBotResponse("__stay_reassurance__", userName);
                }
            }

            if (TipBrowser.HasTopic(cleaned))
            {
                return TipBrowser.BrowseTips(cleaned, userName);
            }
            else
            {
                return BotResponses.GetBotResponse(cleaned, userName);
            }
        }

        // Remove common punctuation characters that shouldn't affect intent matching.
        // Keep characters used by parsing such as ':' and '=' and apostrophes.
        private string CleanInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input ?? string.Empty;

            var charsToRemove = new[] { '?', '.', '!', ',', ';', '"', '“', '”', '—', '–', '(', ')', '[', ']', '/' };
            var sb = new System.Text.StringBuilder(input.Length);
            foreach (var ch in input)
            {
                if (charsToRemove.Contains(ch))
                    continue;
                sb.Append(ch);
            }
            return sb.ToString();
        }

        private void ConversationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ConversationList.SelectedItem;
            if (selectedItem is string selectedText && !string.IsNullOrEmpty(selectedText))
            {
                // we are just going to add the selected category as a user message and then generate a bot reply based on that category
                Actions.AddMessage(selectedText, true, MessagesList);
                AddMessageToHistory(selectedText, true);
                var botReply = GenerateBotReply(selectedText);
                if (!string.IsNullOrEmpty(botReply))
                {
                    Actions.AddMessage(botReply, false, MessagesList);
                    AddMessageToHistory(botReply, false);
                }
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
                var msg = "Glad you want to hear some more...";
                Actions.AddMessage(msg, false, MessagesList);
                AddMessageToHistory(msg, false);
            }
        }

        private void NewConversationButton_Click(object sender, RoutedEventArgs e)
        {
            // Archive current conversation if it has content
            if (currentConversation != null && currentConversation.Count > 0)
            {
                // create a copy and store
                var copy = new List<string>(currentConversation);
                conversationHistory.Add(copy);
                var title = $"Conversation {conversationHistory.Count} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                HistoryList.Items.Add(title);
                // persist updated history
                SaveConversationHistory();
            }

            // Clear UI fields for new conversation
            MessagesList.Items.Clear();
            InputTextBox.Clear();

            // reset current conversation
            currentConversation = new List<string>();

            // Prompt the user with guidance for starting a new conversation and record it
            var prompt = "Please choose a topic from the sidebar or type any of the available topics to begin.";
            Actions.AddMessage(prompt, false, MessagesList);
            AddMessageToHistory(prompt, false);

            // Give focus to the input box for convenience
            InputTextBox.Focus();
        }

        private void AddMessageToHistory(string text, bool isUser)
        {
            if (currentConversation == null)
                currentConversation = new List<string>();

            var prefix = isUser ? "User: " : "Bot: ";
            currentConversation.Add(prefix + text);
        }

        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIndex = HistoryList.SelectedIndex;
            if (selectedIndex < 0 || selectedIndex >= conversationHistory.Count)
                return;

            // display the selected historical conversation
            MessagesList.Items.Clear();
            var convo = conversationHistory[selectedIndex];
            foreach (var line in convo)
            {
                if (line.StartsWith("User: "))
                {
                    var msg = line.Substring("User: ".Length);
                    Actions.AddMessage(msg, true, MessagesList);
                }
                else if (line.StartsWith("Bot: "))
                {
                    var msg = line.Substring("Bot: ".Length);
                    Actions.AddMessage(msg, false, MessagesList);
                }
                else
                {
                    Actions.AddMessage(line, false, MessagesList);
                }
            }
        }

        private void SaveConversationHistory()
        {
            try
            {
                var toSave = conversationHistory.Select(conv => conv.ToArray()).ToArray();
                var json = JsonSerializer.Serialize(toSave, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(historyFilePath, json);
            }
            catch
            {
                // ignore save failures for now
            }
        }

        private void LoadConversationHistory()
        {
            try
            {
                if (!File.Exists(historyFilePath))
                    return;

                var json = File.ReadAllText(historyFilePath);
                var loaded = JsonSerializer.Deserialize<string[][]>(json);
                if (loaded == null)
                    return;

                conversationHistory.Clear();
                HistoryList.Items.Clear();
                int i = 0;
                foreach (var conv in loaded)
                {
                    var list = conv.ToList();
                    conversationHistory.Add(list);
                    i++;
                    var title = $"Conversation {i} - loaded";
                    HistoryList.Items.Add(title);
                }
            }
            catch
            {
                // ignore load failures
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                // archive current conversation if non-empty
                if (currentConversation != null && currentConversation.Count > 0)
                {
                    conversationHistory.Add(new List<string>(currentConversation));
                    // add a title entry for the saved conversation
                    var title = $"Conversation {conversationHistory.Count} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    if (HistoryList != null)
                        HistoryList.Items.Add(title);
                }

                SaveConversationHistory();
            }
            catch
            {
                // ignore
            }
        }
    }
}
