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
                    "Consider using a password manager to generate and store strong passwords."
                }
            },
            { "phishing", new List<string>()
                {
                    "Be cautious of emails asking for urgent action.",
                    "Check the sender’s email address carefully.",
                    "Don’t click on suspicious links or attachments.",
                    "Verify requests by contacting the organization directly."
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
                    "Keep your operating system and applications patched.",
                    "Be cautious of free software that may bundle unwanted programs."
                }
            },
            { "wifi", new List<string>()
                {
                    "Avoid using public Wi-Fi for sensitive transactions.",
                    "Use a VPN when connecting to unsecured networks.",
                    "Change default router passwords.",
                    "Enable WPA3 or WPA2 encryption on your home Wi-Fi."
                }
            },
            { "windows updates", new List<string>()
                {
                    "Enable automatic updates for your operating system.",
                    "Regularly update browsers and plugins.",
                    "Patch vulnerabilities quickly to reduce risk.",
                    "Outdated software is a common attack vector."
                }
            },
            { "mobile security", new List<string>()
                {
                    "Install apps only from official stores.",
                    "Review app permissions before installing.",
                    "Enable device encryption and screen lock.",
                    "Keep your mobile OS updated."
                }
            }
        };

        public static bool HasTopic(string topic) => tips.ContainsKey(topic);

        public static void BrowseTips(string topic, string userName)
        {
            var topicTips = tips[topic];
            int index = 0;

            Console.WriteLine();
            Console.WriteLine($"CyberSec Bot: Okay {userName}, here are some tips about {topic}.");
            Console.WriteLine("Use N for next, P for previous, Q to quit tips.");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"Tip {index + 1}/{topicTips.Count}: {topicTips[index]}");
                Console.WriteLine();

                Console.Write("Command (N/P/Q): ");
                string? cmd = Console.ReadLine()?.ToLower();

                if (cmd == "n" && index < topicTips.Count - 1)
                    index++;
                else if (cmd == "p" && index > 0)
                    index--;
                else if (cmd == "q" || cmd == "quit")
                    break;
            }
            Console.WriteLine();
            Console.WriteLine($"CyberSec Bot: Done with {topic} tips. Back to interactive mode!");
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