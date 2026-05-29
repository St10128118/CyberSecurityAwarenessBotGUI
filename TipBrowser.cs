using System;
using System.Collections.Generic;

namespace CybersecurityAwarenessBot
{
    public static class TipBrowser
    {
        private static Dictionary<string, List<string>> tips = new Dictionary<string, List<string>>()
        {
            { "passwords", new List<string>()
                {
                    "Use at least 12 characters in your password.",
                    "Mix uppercase, lowercase, numbers, and symbols.",
                    "Never reuse the same password across multiple accounts.",
                    "A password manager can make strong passwords much easier to manage.",
                    "Try passphrases instead of short passwords — they're safer and easier to remember.",
                    "Enable multi-factor authentication whenever possible."
                }
            },
            { "phishing", new List<string>()
                {
                    "Be cautious of emails asking for passwords or urgent action.",
                    "Check the sender’s email address carefully.",
                    "Don’t click on suspicious links or attachments.",
                    "Verify requests by contacting the organization directly.",
                    "Phishing emails often create urgency to trick people into acting fast.",
                    "Check the sender address carefully — attackers often imitate trusted companies.",

                }
            },
            { "safe browsing", new List<string>()
                {
                    "Limit the personal information you share online.",
                    "Review privacy settings regularly.",
                    "Be cautious of friend requests from strangers.",
                    "Think before posting — once online, it’s hard to remove."
                }
            },
            { "malware", new List<string>()
                {
                    "Install antivirus software and keep it updated.",
                    "Avoid downloading files from untrusted sources.",
                    "Malware often spreads through suspicious attachments or unsafe downloads.",
                    "Regularly back up important data to recover from potential infections.",
                    "Keep your operating system and applications patched.",
                    "Be cautious of free software that may bundle unwanted programs."
                }
            },
            { "wifi", new List<string>()
                {
                    "Avoid using public Wi-Fi for sensitive transactions.",
                    "Use a VPN when connecting to unsecured networks.",
                    "Change default router passwords.",
                    "Enable WPA3 or WPA2 encryption on your home Wi-Fi.",
                    "Public Wi-Fi can be a hotspot for attackers — use a VPN to encrypt your connection.",
                    "Always change default passwords on your router to prevent unauthorized access."
                }
            },
            { "windows updates", new List<string>()
                {
                    "Enable automatic updates for your operating system.",
                    "Regularly update browsers and plugins.",
                    "Patch vulnerabilities quickly to reduce risk.",
                    "Outdated software is a common attack vector.",
                    "Enable automatic updates to ensure you get the latest security patches.",
                    "Keep all your software updated — attackers often target known vulnerabilities in outdated programs."
                }
            },
            { "mobile security", new List<string>()
                {
                    "Install apps only from official stores.",
                    "Review app permissions before installing.",
                    "Enable device encryption and screen lock.",
                    "Keep your mobile OS updated.",
                    "Be cautious of apps that request excessive permissions.",
                    "Use a strong screen lock and enable encryption on your mobile device to protect your data."
                }
            }
        };

        public static bool HasTopic(string topic) => tips.ContainsKey(topic);

        public static string BrowseTips(string topic, string userName)
        {
            if (!tips.ContainsKey(topic))
            {
                return $"Sorry {userName}, I don't seems to have anytips on that topic. " +
                    $"Please choose any topic from this list: {string.Join(", ", tips.Keys)}.";
            }
            var topicTips = tips[topic];
            string tip = $"Tip 1/{topicTips.Count}: {topicTips[0]}\n";
            for (int i = 1; i < topicTips.Count; i++)
            {
                tip += $"Tip {i + 1}/{topicTips.Count}: {topicTips[i]}\n";
            }
            tip += $"This is all you need to know regarding the topic of {topic}.\n";
            tip += $"Feel free to ask about another topic or ask me anything else, {userName}!";
            return tip;
        }

        internal static List<string> GetTipCategories()
        {
            // Create a list to hold the categories
            var categories = new List<string>();
            foreach (var category in tips.Keys)
            {
                categories.Add(category);
            }
            return categories;
        }
    }
}