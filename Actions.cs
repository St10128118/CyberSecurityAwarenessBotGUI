using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberSecurityAwarenessBot
{
    public static class Actions
    {

        public static void PlayVoiceOrSound(string soundLocation)
        {
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    SoundPlayer player = new(soundLocation);
                    player.Load();
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void AddMessage(string text, bool isUser, ListBox MessagesList)
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
            ScrollToEnd(MessagesList);
        }

        private static void ScrollToEnd(ListBox MessagesList)
        {
            if (MessagesList.Items.Count == 0)
                return;

            var last = MessagesList.Items[MessagesList.Items.Count - 1];
            MessagesList.Dispatcher.InvokeAsync(() => MessagesList.ScrollIntoView(last));
        }

        public static async Task SimulateProcessingDelayAsync()
        {
            // small delay to simulate processing
            Random rand = new Random();

            // Define your set of delays in milliseconds
            int[] delays = { 400, 600, 700, 900 };

            // Pick a random index from the delays array
            int randomIndex = rand.Next(delays.Length);

            // Get the number at that index
            int randomNumber = delays[randomIndex];
            await Task.Delay(randomNumber); // and then delay
        }

        public static void BreakLine()
        {
            Console.WriteLine("\n\n\t*******************************************************************\n\n");
        }
    }
}
