using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot
{
    public static class BotResponses
    {
        private static readonly Random random = new();

        public static string GetBotResponse(string input, string userName)
        {
            string normalized = input.Trim().ToLower();

            if (ContainsAny(normalized, "hello", "hi", "hey", "good morning", "good evening"))
            {
                return GetRandomResponse(new[]
                {
                    $"Hey {userName}! 👋 How can I help today?",
                    $"Hi {userName}! Ready to level up your cyber awareness?",
                    $"Hello {userName}! What can I help you with today?",
                    $"Hey there, {userName}! Hope you're having a secure day 😄",
                    $"Welcome back, {userName}! Need cybersecurity help or just chatting?"
                });
            }

            // How are you
            if ((normalized.Contains("how") && normalized.Contains("you")) ||
                normalized.Contains("how are you"))
            {
                return GetRandomResponse(new[]
                {
                    $"I'm doing great, {userName}! Staying alert for cyber threats 😎",
                    $"All systems secure and running smoothly, {userName}!",
                    $"Doing awesome, thanks for asking {userName}! Staying cyber-safe never sleeps.",
                    $"Feeling digital and secure today, {userName} 😄",
                    $"I'm good, {userName}! Always here to help protect against online risks."
                });
            }

            // Purpose
            if (ContainsAny(normalized, "purpose", "what do you do", "who are you", "what are you"))
            {
                return GetRandomResponse(new[]
                {
                    $"My purpose is to help you stay safe online, {userName}.",
                    $"I'm your cybersecurity awareness assistant, here to help you browse smarter.",
                    $"I help users learn about phishing, passwords, malware, and staying secure online.",
                    $"Think of me as your cyber safety companion 😄",
                    $"I’m here to help keep your digital life safer, {userName}."
                });
            }

            // Help
            if (ContainsAny(normalized, "help", "what can i ask", "commands", "topics"))
            {
                return GetRandomResponse(new[]
                {
                    "You can ask about passwords, phishing, malware, safe browsing, Wi-Fi safety, updates, and mobile security.",
                    "Try asking about phishing scams, password tips, malware, or online safety.",
                    "Need ideas? Ask about cyber threats, suspicious emails, safe websites, or account security.",
                    "I can help with cybersecurity basics or we can just chat casually 😄"
                });
            }

            return $"I didn't quite understand that. Could you rephrase, {userName}?\n";
        }
        private static bool ContainsAny(string input, params string[] keywords)
        {
            return keywords.Any(input.Contains);
        }

        private static string GetRandomResponse(string[] responses)
        {
            return responses[random.Next(responses.Length)];
        }
    }

}
