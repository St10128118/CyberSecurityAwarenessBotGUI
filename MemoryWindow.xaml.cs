using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CyberSecurityAwarenessBotGUI
{
    public partial class MemoryWindow : Window
    {
        private readonly ChatBotMemoryEngine _memoryEngine;

        public MemoryWindow(ChatBotMemoryEngine memoryEngine)
        {
            InitializeComponent();
            _memoryEngine = memoryEngine ?? throw new ArgumentNullException(nameof(memoryEngine));
            LoadFacts();
        }

        private void LoadFacts()
        {
            FactsList.Items.Clear();
            var facts = _memoryEngine.GetAllFacts();
            foreach (var kv in facts)
            {
                var item = new ListBoxItem { Content = $"{kv.Key} : {kv.Value}", Tag = kv.Key };
                FactsList.Items.Add(item);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (FactsList.SelectedItem is ListBoxItem li)
            {
                var key = li.Tag as string;
                var value = _memoryEngine.GetAllFacts().TryGetValue(key, out var v) ? v : string.Empty;
                var edit = new EditFactWindow(key, value);
                if (edit.ShowDialog() == true)
                {
                    _memoryEngine.UpdateFact(key, edit.Value);
                    LoadFacts();
                }
            }
            else
            {
                MessageBox.Show("Please select a fact to edit.", "Edit Fact", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (FactsList.SelectedItem is ListBoxItem li)
            {
                var key = li.Tag as string;
                var result = MessageBox.Show($"Delete the fact for '{key}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _memoryEngine.DeleteFact(key);
                    LoadFacts();
                }
            }
            else
            {
                MessageBox.Show("Please select a fact to delete.", "Delete Fact", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Clear all stored facts?", "Confirm Clear", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _memoryEngine.ClearMemory();
                LoadFacts();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
