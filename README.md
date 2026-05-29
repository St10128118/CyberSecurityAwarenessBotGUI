
# Cyber Security Awareness Bot (WPF)

A compact, local WPF chatbot that teaches practical cybersecurity habits through short, friendly advice. Designed to be easy to run, inspect and extend.

Highlights
- Targets .NET 8 (WPF) and keeps XAML designer compatibility in mind.
- Simple chat UI with conversational bubbles, Enter-to-send, and a toggleable sidebar.
- Local memory engine: teach the bot facts (e.g. `remember my city is London`) and recall them later.
- Conversation history persisted per user for later review.
- Topic-focused responses (phishing, passwords, MFA, malware, browsing, Wi?Fi, updates, mobile security).

Quick start
1. Open the solution in Visual Studio or run `dotnet run` from the project directory.
2. Enter a username on startup (press Enter to submit).
3. Chat naturally. Examples:
   - `remember my city is Boston`
   - `what is my city`
   - `How do I spot phishing emails?`

Data & storage
- Memory and conversation history are stored under `%APPDATA%/CyberSecurityAwarenessBotGUI/` as JSON files (safe for local development).

Extending the bot
- Responses live in `BotResponses.cs` and are easy to expand or replace with an API call to a remote service or LLM.
- The memory engine (`ChatBotMemoryEngine.cs`) exposes methods to list, edit, and delete stored facts.

License & notes
- Lightweight demo for learning and prototyping. Adapt and extend the code for production use with proper security and privacy controls.

