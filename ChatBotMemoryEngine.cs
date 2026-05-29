using System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CyberSecurityAwarenessBotGUI
{
    public class ChatBotMemoryEngine
    {
        private readonly Dictionary<string, string> memory = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly string memoryFile;
        private readonly object fileLock = new();

        public ChatBotMemoryEngine()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = Path.Combine(appData, "CyberSecurityAwarenessBotGUI");
            Directory.CreateDirectory(folder);
            memoryFile = Path.Combine(folder, "memory.json");

            LoadMemory();
        }

        // Returns a response string if the memory engine handled the input, otherwise null
        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var trimmed = input.Trim();

            // Try to parse 'remember' commands with a flexible regex
            // Examples supported:
            //  - remember my city is Johannesburg
            //  - remember that company = Contoso
            //  - remember favorite color: blue
            var rememberPattern = new Regex(@"^remember(?: that)?(?: my)?\s+(.+?)\s*(?:is|=|:)\s*(.+)$", RegexOptions.IgnoreCase);
            var m = rememberPattern.Match(trimmed);
            if (m.Success)
            {
                var rawKey = m.Groups[1].Value;
                var rawValue = m.Groups[2].Value;
                var key = NormalizeKey(rawKey);
                var value = rawValue.Trim();
                UpdateFact(key, value);
                return $"Okay — I'll remember that your {rawKey.Trim()} is {value}.";
            }

            // Other common variations: "remember X Y" (poorly formed) - ignore

            // Recall phrases
            var recallPattern1 = new Regex(@"^(?:what is my|what's my)\s+(.+)$", RegexOptions.IgnoreCase);
            var recallPattern2 = new Regex(@"^do you remember(?: my)?\s+(.+)$", RegexOptions.IgnoreCase);

            var r1 = recallPattern1.Match(trimmed);
            if (r1.Success)
            {
                var key = NormalizeKey(r1.Groups[1].Value);
                return Recall(key);
            }

            var r2 = recallPattern2.Match(trimmed);
            if (r2.Success)
            {
                var key = NormalizeKey(r2.Groups[1].Value);
                return Recall(key);
            }

            // Not handled by memory engine
            return null;
        }

        public Dictionary<string, string> GetAllFacts()
        {
            lock (fileLock)
            {
                return memory.ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool DeleteFact(string key)
        {
            key = NormalizeKey(key);
            lock (fileLock)
            {
                var removed = memory.Remove(key);
                if (removed)
                    SaveMemory();
                return removed;
            }
        }

        public void ClearMemory()
        {
            lock (fileLock)
            {
                memory.Clear();
                SaveMemory();
            }
        }

        public void UpdateFact(string key, string value)
        {
            key = NormalizeKey(key);
            lock (fileLock)
            {
                memory[key] = value;
                SaveMemory();
            }
        }

        private string Recall(string key)
        {
            key = NormalizeKey(key);
            lock (fileLock)
            {
                if (memory.TryGetValue(key, out var value))
                {
                    return $"Yes — you told me your {key} is {value}.";
                }
            }
            return $"I don't remember your {key} yet.";
        }

        private void SaveMemory()
        {
            try
            {
                lock (fileLock)
                {
                    var json = JsonSerializer.Serialize(memory, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(memoryFile, json);
                }
            }
            catch
            {
                // ignore write errors for now — do not throw from memory engine
            }
        }

        private void LoadMemory()
        {
            try
            {
                lock (fileLock)
                {
                    if (File.Exists(memoryFile))
                    {
                        var json = File.ReadAllText(memoryFile);
                        var loaded = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                        if (loaded != null)
                        {
                            memory.Clear();
                            foreach (var kv in loaded)
                                memory[kv.Key] = kv.Value;
                        }
                    }
                }
            }
            catch
            {
                // ignore parse errors, start with empty memory
            }
        }

        private static string NormalizeKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            // trim and remove possessives and extra words like 'my'
            var k = key.Trim().ToLowerInvariant();
            k = Regex.Replace(k, "^my\\s+", "");
            k = k.Replace("'s", "");
            return k.Trim();
        }
    }

}
