using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot
{
    public static class BotResponses
    {
        public static string GetBotResponse(string input, string userName)
        {
            switch (input)
            {
                case "how are you?":
                    return $"\nI'm doing great, {userName}! Always scanning for cyber threats and keeping awareness high.\n";
                case "what is your purpose?":
                    return $"\nMy purpose is to help you, {userName}, stay safe online by sharing cybersecurity awareness tips.\n";
                case "what can i ask you?":
                    return $"\nYou can ask me about cybersecurity basics like phishing, strong passwords, safe browsing, or just chat casually!\n";
                case "help":
                    return $"\n Choose from: passwords, phishing, safe browsing, malware, wifi, windows updates, and mobile security\n";
                default:
                    return $"I didn't quite understand that. Could you rephrase, {userName}?\n";
            }
        }
    }

}
