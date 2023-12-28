using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NemLinha_Projeto
{
    class Program
    {
        private static LanguageManager _languageManager;
        private const string GameVersion = "V0.22b";
        public const string GameTitle="\n \u2588\u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2557   \u2588\u2588\u2557\u2588\u2588\u2588\u2557   \u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557    \u2588\u2588\u2557  \u2588\u2588\u2557\n\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u2588\u2588\u2554\u2550\u2550\u2550\u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u255a\u2550\u2550\u2588\u2588\u2554\u2550\u2550\u255d    \u2588\u2588\u2551  \u2588\u2588\u2551\n\u2588\u2588\u2551     \u2588\u2588\u2551   \u2588\u2588\u2551\u2588\u2588\u2554\u2588\u2588\u2557 \u2588\u2588\u2551\u2588\u2588\u2554\u2588\u2588\u2557 \u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551        \u2588\u2588\u2551       \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2551\n\u2588\u2588\u2551     \u2588\u2588\u2551   \u2588\u2588\u2551\u2588\u2588\u2551\u255a\u2588\u2588\u2557\u2588\u2588\u2551\u2588\u2588\u2551\u255a\u2588\u2588\u2557\u2588\u2588\u2551\u2588\u2588\u2554\u2550\u2550\u255d  \u2588\u2588\u2551        \u2588\u2588\u2551       \u255a\u2550\u2550\u2550\u2550\u2588\u2588\u2551\n\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2554\u255d\u2588\u2588\u2551 \u255a\u2588\u2588\u2588\u2588\u2551\u2588\u2588\u2551 \u255a\u2588\u2588\u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2557   \u2588\u2588\u2551            \u2588\u2588\u2551\n \u255a\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u255d  \u255a\u2550\u2550\u2550\u255d\u255a\u2550\u255d  \u255a\u2550\u2550\u2550\u255d\u255a\u2550\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u2550\u2550\u2550\u2550\u255d   \u255a\u2550\u255d            \u255a\u2550\u255d\n"+GameVersion+"                                                                          \n"; 
        private static SfxPlayerManager _sFxPlayerManager;
        private static MusicPlayerManager _musicPlayerManager;
        private static VolumeBarMenu _volumeBarMenu;
        private static Settings _settings;
        private static Menus _menus;
        private static bool _debugModeEnabled;
        
        public static LanguageManager LanguageManager => _languageManager;
        public static SfxPlayerManager SfXPlayerManager => _sFxPlayerManager;

        public static MusicPlayerManager MusicPlayerManager => _musicPlayerManager;
        
        public static Settings Settings => _settings;
        
        static void Main()
        {
            Console.Title = "Connect 4";
            
            List<Func<Task>> tasks = new List<Func<Task>>
            {
                () => ExecuteTask("Loading settings", () => { _settings = Settings.LoadSettings();
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Setting SFX player", () => { _sFxPlayerManager = new SfxPlayerManager(_settings.SfxVolume);
                        return Task.CompletedTask;
                }),
                () => ExecuteTask("Setting music player", () => { _musicPlayerManager = new MusicPlayerManager(_settings.MusicVolume);
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Caching the sounds", () => { _musicPlayerManager.AddMusic("MainMenu", "https://cdn.discordapp.com/attachments/938557421218066462/1189316969342709891/space-invaders-classic-arcade-game-116826.mp3?ex=659db8bd&is=658b43bd&hm=824b39c08a3a1e035bd5f7d9c7e5da542d14042a8def2dca4ec8baefad88c4dd&");
                    _musicPlayerManager.AddMusic("Credits", "https://cdn.discordapp.com/attachments/1049087575249399849/1187825231797178448/Credits.wav?ex=65984b73&is=6585d673&hm=84b7bfee09493bf050da1b87d071345d69f54b4e389411d662d1201514e5ff54&");
                    _sFxPlayerManager.AddSfx("NavigationMenu", "https://cdn.discordapp.com/attachments/1137832862406688800/1188886682708222012/Retro5.mp3?ex=659c2801&is=6589b301&hm=9e3cf4ff431d16bfec13f994c6f0052ae74c6e831077ff8fb576dfda6f060ab4&");
                    _sFxPlayerManager.AddSfx("SelectMenu", "https://cdn.discordapp.com/attachments/1137832862406688800/1188886683018596403/Retro7.mp3?ex=659c2801&is=6589b301&hm=4a3cbca381da2b573ba5ed2ee843192b01d24eb78f39c2dae1d6c5b4d82ca9a6&");
                    _sFxPlayerManager.AddSfx("BackMenu", "https://cdn.discordapp.com/attachments/1189786476653854760/1189786569024999444/Retro4.mp3?ex=659f6e17&is=658cf917&hm=2f71de70f3c63b53971997c5bf4d0e7d4dfd6af503fb591634ac245ba22d33bc&");
                    _sFxPlayerManager.AddSfx("SuccessMenu", "https://cdn.discordapp.com/attachments/1189786476653854760/1189786785253957662/Retro9.mp3?ex=659f6e4a&is=658cf94a&hm=b30e35cdc13677e2e0362c1738e69127ca4225ceb66de4f5a76557850a7055ac&");
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Getting \"MainMenu\" music", () => { _musicPlayerManager.DownloadMusic("MainMenu");
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Getting \"Credits\" music", () => { _musicPlayerManager.DownloadMusic("Credits");
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Getting \"NavigationMenu\" sound", () => { _sFxPlayerManager.DownloadSfx("NavigationMenu");
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Getting \"SelectMenu\" sound", () => { _sFxPlayerManager.DownloadSfx("SelectMenu");
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Getting \"BackMenu\" sound", () => { _sFxPlayerManager.DownloadSfx("BackMenu");
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Generating menus", () => { _menus = new Menus();
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Generating more menus", () => { _volumeBarMenu = new VolumeBarMenu();
                    return Task.CompletedTask;
                }),
                () => ExecuteTask("Setting the language", () => { _languageManager = new LanguageManager(_settings.Language);
                    return Task.CompletedTask;
                })
                
            };

            ConsoleLoader loader = new ConsoleLoader();
            loader.ExecuteTasks(tasks).Wait(); // Blocking call in a non-async main
            
            Console.WriteLine(LanguageManager.Translate("loading_complete"));
            Console.WriteLine(LanguageManager.Translate("press_any_key_to_continue"));
            Console.ReadKey();
            DrawMainMenu();
        }

        static void DrawMainMenu()
        {
            _musicPlayerManager.StopMusic();
            _musicPlayerManager.PlayMusic("MainMenu");
            Console.CursorVisible = false;
            
            string[] menuOptions = { LanguageManager.Translate("new_game"),
                LanguageManager.Translate("continue"),
                LanguageManager.Translate("players"),
                LanguageManager.Translate("options"),
                LanguageManager.Translate("credits"),
                LanguageManager.Translate("exit") };
            int selectedIndex = _menus.ShowMenu(menuOptions,LanguageManager.Translate("main_menu"),0,_debugModeEnabled?$"Current music volume: {_settings.MusicVolume}\nCurrent SFX volume: {_settings.SfxVolume}":null,true,-1,GameTitle);

            if (selectedIndex == menuOptions.Length - 1)
            {
                CloseProgram();
            }
            else
            {
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0:
                        int[,] gameGrid = { 
                            {1,2,3,4,1}, 
                            {6,7,2,1,0},
                            {6,1,8,9,0},
                            {6,1,8,9,0},
                            {1,2,1,2,5}, 
                            {6,1,8,9,0},
                            {6,1,8,9,0},
                            {6,1,8,9,0},
                            {6,1,8,9,0},
                            {1,2,3,4,5} };
                        Grid uIgrid = new Grid();
                        Console.Clear();
                        uIgrid.DisplayGrid(10,5,gameGrid);
                        Console.ReadKey();
                        break;
                    case 1:
                        break;
                    case 2:
                        DrawPlayerMenu();
                        break;
                    case 3:
                        DrawOptionsMenu();
                        break;
                    case 4 :
                        CreditsMenu();
                        break;
                    default:
                        Console.WriteLine($"ERRO: {menuOptions[selectedIndex]}");
                        Console.ReadKey();
                        break;
                        
                }
            }
        }

        static void DrawPlayerMenu()
        {
            string[] pMenuOptions = { _languageManager.Translate("new_player"),
                _languageManager.Translate("list_all"),
                _languageManager.Translate("clear_all"),
                _languageManager.Translate("back") };
            int pSelectedIndex = _menus.ShowMenu(pMenuOptions,_languageManager.Translate("players"),0);

            if (pSelectedIndex == pMenuOptions.Length - 1)
            {
                DrawMainMenu();
            }

            switch (pSelectedIndex)
            {
                case 0:
                    AddPlayerForm();
                    DrawPlayerMenu();
                    break;
                case 1:
                    ListPlayersMenu();
                    break;
                case 2:
                    PlayerManager.ClearAllPlayers();
                    Console.ReadKey();
                    DrawPlayerMenu();
                    break;
                default:
                    Console.WriteLine($"ERRO: {pMenuOptions[pSelectedIndex]}");
                    break;
            }
        }
        
        static void PlayerDetailsMenu(string playerName)
        {
            string[] menuOptions = { _languageManager.Translate("delete_player"),_languageManager.Translate("back") };
            
            int selectedIndex = _menus.ShowMenu(menuOptions, $"{_languageManager.Translate("details_of")} {playerName}", 0, PlayerManager.DisplayPlayerInfo(playerName),false);

            switch (selectedIndex)
            {
                case 0:
                    // Apagar Jogador (Delete Player)
                    Console.WriteLine();
                    bool deleteConfirmation = _menus.ConfirmAction(_languageManager.Translate("confirm_delete",playerName));
                    
                    if (deleteConfirmation)
                    {
                        string deleteResult = PlayerManager.DeletePlayer(playerName);
                        Console.WriteLine(deleteResult);
                        ListPlayersMenu();
                    }
                    else
                    {
                        // If the deletion is not confirmed, go back to the player details menu
                        PlayerDetailsMenu(playerName);
                    }
                    break;
                case 1:
                    // Voltar (Go back)
                    ListPlayersMenu();
                    break;
            }
        }

        static void ListPlayersMenu()
        {
            string[] playerNames = PlayerManager.ListAllPlayerNames();

            string[] menuOptions = new string[playerNames.Length + 1];
            Array.Copy(playerNames, menuOptions, playerNames.Length);
            menuOptions[playerNames.Length] = _languageManager.Translate("back");

            int selectedIndex = _menus.ShowMenu(menuOptions, _languageManager.Translate("players_list"), 0);

            if (selectedIndex == playerNames.Length)
            {
                DrawPlayerMenu();
            }
            else
            {
                PlayerDetailsMenu(playerNames[selectedIndex]);
            }
        }

        static void AddPlayerForm()
        {
            Console.Clear();
            string username = GetValidUsername();
            bool addPlayerResult = PlayerManager.AddPlayer(username);

            if (addPlayerResult)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            
            Console.WriteLine(PlayerManager.AddPlayerStringOut(addPlayerResult, username,LanguageManager));
            Console.ResetColor();
            
            if (addPlayerResult == false)
            {
                Thread.Sleep(1050);
                AddPlayerForm();
            }
            Thread.Sleep(1050);
        }
        
        static string GetValidUsername()
        {
            string username;

            do
            {
                Console.Write(_languageManager.Translate("enter_username"));
                username = Console.ReadLine();

                if (!IsValidUsername(username))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(_languageManager.Translate("invalid_username"));
                    Console.ResetColor();
                }

            } while (!IsValidUsername(username));

            return username;
        }

        static bool IsValidUsername(string username)
        {
            // Check if the username is between 1 and 21 characters
            return !string.IsNullOrEmpty(username) &&
                   !HasSpecialCharacters(username) &&
                   username.Length <= 21;
        }

        static bool HasSpecialCharacters(string input)
        {
            // This method to define what special characters are not allowed
            // For simplicity, I'm considering only letters and numbers to be valid
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true; // Special character found
                }
            }
            return false; // No special characters found
        }

        static void CloseProgram()
        {
            Console.Clear();
            Console.WriteLine(_languageManager.Translate("closing_program"));
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        private static void DrawOptionsMenu()
        {
            string[] optionsMenuOptions = { _languageManager.Translate("language"),
                _languageManager.Translate("sound"), 
                _languageManager.Translate("back") };
            int selectedIndex = _menus.ShowMenu(optionsMenuOptions, _languageManager.Translate("options"), 0);

            if (selectedIndex == optionsMenuOptions.Length - 1)
            {
                DrawMainMenu();
            }
            
            switch (selectedIndex)
            {
                case 0:
                    ShowLanguageOptions();
                    break;

                case 1:
                    //ShowColorOptions();
                    _volumeBarMenu.DrawSoundMenu();
                    _settings.SaveSettings();
                    DrawOptionsMenu();
                    break;

                default:
                    Console.WriteLine($"ERRO: {optionsMenuOptions[selectedIndex]}");
                    Console.ReadKey();
                    break;
            }
        }
        
        private static void ShowLanguageOptions()
        {
            string[] languageOptions = { "English", "Português","Español","Italiano","Deutsch","Français", _languageManager.Translate("back") };
            int selectedIndex = new Menus().ShowMenu(languageOptions, _languageManager.Translate("choose_language"), 0,null,true,LanguageManager.GetLanguageIndex(_settings.Language));

            if (selectedIndex == languageOptions.Length - 1)
            {
                DrawOptionsMenu();
            }
            
            // Update the language based on the user's selection
            switch (selectedIndex)
            {
                case 0:
                    _settings.Language = "en";
                    break;

                case 1:
                    _settings.Language = "pt";
                    break;
                
                case 2:
                    _settings.Language = "es";
                    break;
                
                case 3:
                    _settings.Language = "it";
                    break;
                
                case 4:
                    _settings.Language = "de";
                    break;
                
                case 5:
                    _settings.Language = "fr";
                    break;

                default:
                    Console.WriteLine($"ERRO: {languageOptions[selectedIndex]}");
                    Console.ReadKey();
                    break;
            }

            // Save the updated settings
            _settings.SaveSettings();

            // Clear the cached settings to force a reload
            Settings.ClearCachedSettings();

            // Change the language in the LanguageManager
            LanguageManager.ChangeLanguage(_settings.Language);
            
            Console.WriteLine(LanguageManager.Translate("press_any_key_to_continue"));
            Console.ReadKey();
            DrawOptionsMenu();
        }

        static void CreditsMenu()
        {
            _musicPlayerManager.StopMusic();
            _musicPlayerManager.PlayMusic("Credits");
            
            Console.WriteLine(GameTitle);
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{' ',33}CREDITS{' ',33}║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════╝\n");

            Console.ResetColor();
            Console.WriteLine("Developed by:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Mário Santos");
            Console.WriteLine("Cláudio Ferreira");
            Console.WriteLine("Tomás Ferreira");

            Console.ResetColor();
            Console.WriteLine("\nSpecial Thanks to:");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Team Members");

            Console.ResetColor();
            Console.WriteLine("\nLibraries and Frameworks:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(".NET 8 Core");
            Console.WriteLine(".NET framework 3.5");
            Console.WriteLine("ConsoleGUI Library");
            Console.WriteLine("JSON Library");

            Console.ResetColor();
            Console.WriteLine("\nArtwork and Design:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ASCII Art by Mário Santos");

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine(LanguageManager.Translate("press_any_key_to_continue"));
            Console.ReadKey();
            DrawMainMenu();
        }
        
        public static async Task ExecuteTask(string taskName, Func<Task> taskAction)
        {
            Console.WriteLine(taskName);
            await taskAction.Invoke();
        }

        public static bool DebugMode
        {
            get { return _debugModeEnabled; }
            set { _debugModeEnabled = value; }
        }
    }
}