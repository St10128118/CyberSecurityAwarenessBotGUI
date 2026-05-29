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
                    $"Welcome back, {userName}! Need cybersecurity help or just chatting?",
                    $"Good to see you, {userName}! Ask me anything about staying safe online.",
                    $"Hi {userName} — I'm here when you're ready to learn about passwords, phishing and more."
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
                    $"I'm good, {userName}! Always here to help protect against online risks.",
                    $"Thanks for asking, {userName}! I'm ready to help — what would you like to know?"
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
                    $"I’m here to help keep your digital life safer, {userName}.",
                    $"I can provide tips, examples, and practical steps to improve your security posture."
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

            // Phishing
            if (ContainsAny(normalized, "phish", "phishing", "spear", "scam", "email"))
            {
                return GetRandomResponse(new[]
                {
                    $"Phishing often looks urgent and asks you to click links or provide credentials. Verify sender addresses and never rush.",
                    $"If an email asks for sensitive info, don't reply. Contact the company via an official channel. Hover over links to inspect them first.",
                    $"Spear-phishing targets specific people. Look for mismatched domains, spelling mistakes, and requests that bypass normal processes.",
                    $"Check the sender's email domain carefully; attackers often use lookalike domains with small differences.",
                    $"Don't enable macros or run attachments unless you trust the sender and can verify the file independently.",
                    $"When in doubt, verify via a different channel (call the company's official number) before taking action."
                });
            }

            // Passwords
            if (ContainsAny(normalized, "password", "passphrase", "manager", "credential"))
            {
                return GetRandomResponse(new[]
                {
                    $"Use a password manager to generate and store unique passwords for each site. Avoid reused passwords.",
                    $"Prefer long passphrases (3+ random words) or 12+ character passwords with mixed characters when you must.",
                    $"Enable multi-factor authentication wherever possible — it significantly reduces account takeover risk.",
                    $"Change passwords immediately if you suspect they were exposed and revoke sessions where possible.",
                    $"Use the password manager's built-in breach check to find reused or leaked credentials.",
                    $"Avoid storing passwords in plain text or in notes — a manager secures them with encryption."
                });
            }

            // Multi-factor authentication
            if (ContainsAny(normalized, "mfa", "multi-factor", "2fa", "two-factor", "authenticator"))
            {
                return GetRandomResponse(new[]
                {
                    $"MFA (2FA) adds a second step — SMS is better than nothing but authenticator apps or hardware keys are stronger.",
                    $"Use an authenticator app (e.g., Authy, Google Authenticator) or a hardware security key for high-value accounts.",
                    $"If a site supports backup codes, store them safely offline. Don’t share MFA codes with anyone.",
                    $"Prefer push-based MFA or hardware tokens for the best balance of security and usability.",
                    $"Register multiple MFA methods where supported so you have recovery options if one device is lost.",
                    $"Treat MFA prompts with suspicion if you didn't initiate a login — it could indicate an account attack."
                });
            }

            // Malware
            if (ContainsAny(normalized, "malware", "virus", "trojan", "ransomware", "infect"))
            {
                return GetRandomResponse(new[]
                {
                    $"Keep your OS and applications up to date, use reputable antivirus tools, and be cautious of unknown downloads.",
                    $"Ransomware often spreads through attachments or malicious installers. Back up important data offline and patch frequently.",
                    $"If you suspect infection, disconnect from networks and follow your incident response plan or seek expert help.",
                    $"Use least-privilege accounts and avoid running unknown executables as administrator.",
                    $"Regular backups (offline or immutable) reduce the impact of ransomware and let you recover safely.",
                    $"Educate users about suspicious attachments and provide clear reporting channels for potential incidents."
                });
            }

            // Safe browsing
            if (ContainsAny(normalized, "browse", "website", "safe browsing", "url", "link"))
            {
                return GetRandomResponse(new[]
                {
                    $"Check site certificates (padlock) and prefer sites with HTTPS. Avoid downloading from unknown or file-sharing sites.",
                    $"If a site seems off, search for reviews or contact the company directly. Don’t enter credentials on unfamiliar pages.",
                    $"Use browser security features and keep extensions to a minimum — each extension is a potential attack surface.",
                    $"Keep your browser up to date and enable features like phishing protection where available.",
                    $"Avoid pasting sensitive information into pages you reached from unknown links.",
                    $"Consider using isolated browser profiles for high-risk activities (banking, shopping) to reduce cross-site risks."
                });
            }

            // Wi-Fi and network
            if (ContainsAny(normalized, "wifi", "wi-fi", "network", "hotspot", "router"))
            {
                return GetRandomResponse(new[]
                {
                    $"Avoid using public Wi‑Fi for sensitive tasks. Use a trusted VPN when on unknown networks.",
                    $"Change default router passwords, keep firmware updated, and use WPA3/WPA2 with a strong passphrase.",
                    $"Guest networks isolate IoT devices from your primary network — use them to reduce risk.",
                    $"Disable WPS and remote admin on routers to reduce exposure to brute-force attacks.",
                    $"Use network segmentation where possible and monitor for unknown devices on your network.",
                    $"If you need to share Wi‑Fi, create a separate guest SSID with limited privileges."
                });
            }

            // Updates & patching
            if (ContainsAny(normalized, "update", "patch", "vulnerability", "upgrade"))
            {
                return GetRandomResponse(new[]
                {
                    $"Regularly install OS and app updates — many breaches exploit known vulnerabilities that were already patched.",
                    $"Enable automatic updates where practical and schedule maintenance windows for critical systems.",
                    $"Inventory your software so you know what needs patching; unsupported software should be replaced.",
                    $"Test patches in a staging environment before wide deployment when possible to avoid regressions.",
                    $"Subscribe to vendor security alerts for critical products so you can respond quickly to updates.",
                    $"Prioritize patching by risk — address public exploits and internet-facing systems first."
                });
            }

            // Mobile security
            if (ContainsAny(normalized, "mobile", "phone", "app", "android", "ios", "sms"))
            {
                return GetRandomResponse(new[]
                {
                    $"Install apps only from official stores, review permissions, and keep your mobile OS updated.",
                    $"Beware of SMS-based phishing (smishing). Don’t follow unknown links sent by text or install unexpected apps.",
                    $"Use a screen lock, biometric or strong PIN, and enable Find My Device features in case of loss.",
                    $"Encrypt backups and be cautious when using public USB charging ports (use a power-only cable).",
                    $"Keep Bluetooth discoverability off when not pairing and unpair devices you no longer use.",
                    $"Review app permissions periodically and uninstall apps you don't use to reduce exposure."
                });
            }

            // Special reassurance responses when user decides to stay after an exit prompt
            if (normalized == "__stay_reassurance__")
            {
                return GetRandomResponse(new[]
                {
                    $"Phew — I'm glad you decided to stay, {userName}! I've got more cybersecurity tips and a few jokes ready.",
                    $"Fantastic — nice to know you're not leaving yet, {userName}. Let's keep your digital life secure!",
                    $"Woohoo! Staying is the safe choice. Stick around, {userName} — I promise helpful tips and minimal dad jokes.",
                    $"Yay, {userName}! I'm thrilled you stayed. Ask me about phishing, passwords, or how to avoid suspicious links.",
                    $"Great choice, {userName} — I'm ready with more tips, tricks, and cyber-safe puns. What would you like to learn next?"
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
