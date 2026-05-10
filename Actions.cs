using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot
{
    public static class Actions
    {
        public static void PrintWelcomeBanner()
        {
            // ASCII Art Welcome Banner
            Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine("||                                                                           ||");
            Console.WriteLine("||   ____         _                  ____                 ____          _    ||");
            Console.WriteLine("||  / ___| _   _ | |__    ___  _ __ / ___|   ___   ___   | __ )   ___  | |_  ||");
            Console.WriteLine("|| | |    | | | || '_ \\  / _ \\| '__|\\___ \\  / _ \\ / __|  |  _ \\  / _ \\ | __| ||");
            Console.WriteLine("|| | |___ | |_| || |_) ||  __/| |    ___) ||  __/| (__   | |_) || (_) || |_  ||");
            Console.WriteLine("||  \\____| \\__, ||_.__/  \\___||_|   |____/  \\___| \\___|  |____/  \\___/  \\__| ||");
            Console.WriteLine("||         |___/                                                             ||");
            Console.WriteLine("||                                                                           ||");
            Console.WriteLine("||                         Cybersecurity Awareness Bot                       ||");
            Console.WriteLine("||                                                                           ||");
            Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine();
        }

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
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        }

        public static void BreakLine()
        {
            Console.WriteLine("\n\n\t*******************************************************************\n\n");
        }
    }
}
