using System;
using System.Threading;
using System.Threading.Tasks;


namespace NemLinha_Projeto
{
    class Program
    {
        private static LanguageManager _languageManager;
        private const string GameVersion = "V0.17b";
        private const string GameTitle="\n \u2588\u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2557   \u2588\u2588\u2557\u2588\u2588\u2588\u2557   \u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557    \u2588\u2588\u2557  \u2588\u2588\u2557\n\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u2588\u2588\u2554\u2550\u2550\u2550\u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u255a\u2550\u2550\u2588\u2588\u2554\u2550\u2550\u255d    \u2588\u2588\u2551  \u2588\u2588\u2551\n\u2588\u2588\u2551     \u2588\u2588\u2551   \u2588\u2588\u2551\u2588\u2588\u2554\u2588\u2588\u2557 \u2588\u2588\u2551\u2588\u2588\u2554\u2588\u2588\u2557 \u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551        \u2588\u2588\u2551       \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2551\n\u2588\u2588\u2551     \u2588\u2588\u2551   \u2588\u2588\u2551\u2588\u2588\u2551\u255a\u2588\u2588\u2557\u2588\u2588\u2551\u2588\u2588\u2551\u255a\u2588\u2588\u2557\u2588\u2588\u2551\u2588\u2588\u2554\u2550\u2550\u255d  \u2588\u2588\u2551        \u2588\u2588\u2551       \u255a\u2550\u2550\u2550\u2550\u2588\u2588\u2551\n\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2554\u255d\u2588\u2588\u2551 \u255a\u2588\u2588\u2588\u2588\u2551\u2588\u2588\u2551 \u255a\u2588\u2588\u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2557   \u2588\u2588\u2551            \u2588\u2588\u2551\n \u255a\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u255d  \u255a\u2550\u2550\u2550\u255d\u255a\u2550\u255d  \u255a\u2550\u2550\u2550\u255d\u255a\u2550\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u2550\u2550\u2550\u2550\u255d   \u255a\u2550\u255d            \u255a\u2550\u255d\n"+GameVersion+"                                                                          \n"; 
        static BackgroundMusicPlayer musicPlayer = new BackgroundMusicPlayer();
        static ConsoleLoader loader = new ConsoleLoader();
        private static Settings settings;
        private static Menus menus;
        static void Main()
        {
            Console.Title = "Connect 4";
            
            musicPlayer.AddMusic("Credits", "https://cdn.discordapp.com/attachments/1049087575249399849/1187825231797178448/Credits.wav?ex=65984b73&is=6585d673&hm=84b7bfee09493bf050da1b87d071345d69f54b4e389411d662d1201514e5ff54&");
            musicPlayer.AddMusic("MainMenu", "https://cdn.discordapp.com/attachments/1137832862406688800/1187884890457382943/titanium-170190.mp3?ex=65988303&is=65860e03&hm=c6ac3a4454c471fd84f87c2d33ba8ad86dc63b475272f403f141185da8a3bd42&");
            
            LoadingScreen();
            Console.WriteLine(LanguageManager.Translate("loading_complete"));
            Console.WriteLine(LanguageManager.Translate("press_any_key_to_continue"));
            Console.ReadKey();
            DrawMainMenu();
        }

        static void DrawMainMenu()
        {
            Console.CursorVisible = false;
            
            string[] menuOptions = { LanguageManager.Translate("new_game"),
                LanguageManager.Translate("continue"),
                LanguageManager.Translate("players"),
                LanguageManager.Translate("options"),
                LanguageManager.Translate("credits"),
                LanguageManager.Translate("exit") };
            int selectedIndex = menus.ShowMenu(menuOptions,LanguageManager.Translate("main_menu"),0,null,true,-1,GameTitle);

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
                        OptionsMenu();
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
            Menus menus = new Menus();
            string[] pMenuOptions = { _languageManager.Translate("new_player"),
                _languageManager.Translate("list_all"),
                _languageManager.Translate("clear_all"),
                _languageManager.Translate("back") };
            int pSelectedIndex = menus.ShowMenu(pMenuOptions,_languageManager.Translate("players"),0);

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
            Menus menus = new Menus();
            string[] menuOptions = { _languageManager.Translate("delete_player"),_languageManager.Translate("back") };
            
            int selectedIndex = menus.ShowMenu(menuOptions, $"{_languageManager.Translate("details_of")} {playerName}", 0, PlayerManager.DisplayPlayerInfo(playerName),false);

            switch (selectedIndex)
            {
                case 0:
                    // Apagar Jogador (Delete Player)
                    Console.WriteLine();
                    bool deleteConfirmation = menus.ConfirmAction(_languageManager.Translate("confirm_delete",playerName));
                    
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
            Menus menus = new Menus();
            string[] playerNames = PlayerManager.ListAllPlayerNames();

            string[] menuOptions = new string[playerNames.Length + 1];
            Array.Copy(playerNames, menuOptions, playerNames.Length);
            menuOptions[playerNames.Length] = _languageManager.Translate("back");

            int selectedIndex = menus.ShowMenu(menuOptions, _languageManager.Translate("players_list"), 0);

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
        
        private static void OptionsMenu()
        {
            Menus menus = new Menus();

            string[] optionsMenuOptions = { _languageManager.Translate("language"),
                _languageManager.Translate("color"), 
                _languageManager.Translate("back") };
            int selectedIndex = menus.ShowMenu(optionsMenuOptions, _languageManager.Translate("options"), 0);

            if (selectedIndex == optionsMenuOptions.Length - 1)
            {
                DrawMainMenu();
            }
            
            switch (selectedIndex)
            {
                case 0:
                    // User selected "Linguagem" (Language)
                    Console.Clear();
                    ShowLanguageOptions();
                    Console.WriteLine(LanguageManager.Translate("press_any_key_to_continue"));
                    Console.ReadKey();
                    OptionsMenu();
                    break;

                case 1:
                    // User selected "Cor" (Color)
                    Console.Clear();
                    //ShowColorOptions();
                    break;

                default:
                    Console.WriteLine($"ERRO: {optionsMenuOptions[selectedIndex]}");
                    Console.ReadKey();
                    break;
            }
        }
        
        private static void ShowLanguageOptions()
        {
            // Load settings once and keep them in memory
            Settings settings = Settings.LoadSettings();

            string[] languageOptions = { "English", "Português","Español","Italiano","Deutsch","Français", _languageManager.Translate("back") };
            int selectedIndex = new Menus().ShowMenu(languageOptions, _languageManager.Translate("choose_language"), 0,null,true,LanguageManager.GetLanguageIndex(settings.Language));

            if (selectedIndex == languageOptions.Length - 1)
            {
                OptionsMenu();
            }
            
            // Update the language based on the user's selection
            switch (selectedIndex)
            {
                case 0:
                    settings.Language = "en";
                    break;

                case 1:
                    settings.Language = "pt";
                    break;
                
                case 2:
                    settings.Language = "es";
                    break;
                
                case 3:
                    settings.Language = "it";
                    break;
                
                case 4:
                    settings.Language = "de";
                    break;
                
                case 5:
                    settings.Language = "fr";
                    break;

                default:
                    Console.WriteLine($"ERRO: {languageOptions[selectedIndex]}");
                    Console.ReadKey();
                    break;
            }

            // Save the updated settings
            settings.SaveSettings();

            // Clear the cached settings to force a reload
            Settings.ClearCachedSettings();

            // Change the language in the LanguageManager
            LanguageManager.ChangeLanguage(settings.Language);
        }

        static void CreditsMenu()
        {
            musicPlayer.PlayMusic("Credits");
            
            Console.Clear();
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
            musicPlayer.StopMusic();
            DrawMainMenu();
        }
        
        static void LoadingScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(GameTitle);
            Console.ResetColor();
            loader.ShowLoader("", 5,0);
            musicPlayer.DownloadMusicAsync("MainMenu");
            loader.ShowLoader("", 5,1);
            musicPlayer.DownloadMusicAsync("Credits");
            loader.ShowLoader("", 5,2);
            settings = Settings.LoadSettings();
            loader.ShowLoader("", 5,3);
            menus = new Menus();
            loader.ShowLoader("", 5,4);
            _languageManager = new LanguageManager(settings.Language);
            loader.ShowLoader("", 5,5);
        }

        
        public static LanguageManager LanguageManager
        {
            get { return _languageManager; }
        }
    }
}