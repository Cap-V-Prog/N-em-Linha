using System;

namespace NemLinha_Projeto
{
    public class VolumeBarMenu
    {

        public void DrawSoundMenu()
        {
            Console.CursorVisible = false;
            DrawMenu();

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        ChangeActiveOption(true); // Cycle through options
                        break;

                    case ConsoleKey.DownArrow:
                        ChangeActiveOption(false); // Cycle through options
                        break;

                    case ConsoleKey.LeftArrow:
                        ChangeVolume(true); // Decrease volume
                        Program.SfXPlayerManager.PlaySoundEffect("NavigationMenu");
                        break;

                    case ConsoleKey.RightArrow:
                        ChangeVolume(false); // Increase volume
                        Program.SfXPlayerManager.PlaySoundEffect("NavigationMenu");
                        break;
                }
                
                DrawMenu();

            } while (keyInfo.Key != ConsoleKey.Escape);
        }
        
        static int _activeOption = 0; // 0 for music, 1 for SFX
        
        static void DrawMenu()
        {
            Console.Clear();

            Console.WriteLine("=== Volume Menu ===");
            DrawVolumeBar("MUSIC", Program.Settings.MusicVolume, _activeOption == 0);
            DrawVolumeBar("SFX", Program.Settings.SfxVolume, _activeOption == 1);
            Console.WriteLine("Use Arrow Keys to navigate. Press Esc to exit.");
        }

        static void DrawVolumeBar(string label, float volume, bool isActive)
        {
            if (isActive)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            int percentage = (int)Math.Round(volume * 100);
            Console.WriteLine($"{label.PadLeft(8)} [{GetVolumeBar(volume)}] {percentage}%");
            Console.ResetColor();
        }

        static void ChangeVolume(bool decrease)
        {
            float activeVolume = (_activeOption == 0) ? Program.Settings.MusicVolume : Program.Settings.SfxVolume;
            activeVolume = decrease ? Math.Max(activeVolume - 0.05f, 0f) : Math.Min(activeVolume + 0.05f, 1f);

            // Round to 2 decimal places to avoid precision issues
            activeVolume = (float)Math.Round(activeVolume, 2);
            
            if (_activeOption == 0)
            {
                Program.Settings.MusicVolume = activeVolume;
                Program.MusicPlayerManager.SetBackgroundMusicVolume(Program.Settings.MusicVolume);
            }
            else
            {
                Program.Settings.SfxVolume = activeVolume;
                Program.SfXPlayerManager.SetSFXVolume(Program.Settings.SfxVolume);
            }
        }

        static void ChangeActiveOption(bool up)
        {
            _activeOption = up ? (_activeOption + 1) % 2 : (_activeOption - 1 + 2) % 2;
        }

        static string GetVolumeBar(float volume)
        {
            int barLength = (int)(volume * 50); // Adjust the length based on your preference
            int emptyLength = 50 - barLength; // Remaining empty characters

            string bar = new string('\u25a0', barLength);
            string empty = new string('-', emptyLength);

            return $"{bar}{empty}";
        }
    }
}