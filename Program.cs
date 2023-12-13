using System;
using System.Threading;

namespace NemLinha_Projeto
{
    class Program
    {
        private static LanguageManager _languageManager;
        static void Main()
        {
            // Load settings once and keep them in memory
            Settings settings = Settings.LoadSettings();
    
            // Create a LanguageManager with the default language from the settings
            _languageManager = new LanguageManager(settings.Language);
            
            Menu menu = new Menu();
            
            Console.CursorVisible = false;
            
            string[] menuOptions = { "Novo jogo", "Continuar",_languageManager.Translate("players"), _languageManager.Translate("options"), "Sair" };
            int selectedIndex = menu.ShowMenu(menuOptions,"Menu principal",0);

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
                    default:
                        Console.WriteLine($"ERRO: {menuOptions[selectedIndex]}");
                        Console.ReadKey();
                        break;
                        
                }
            }
        }

        static void DrawPlayerMenu()
        {
            Menu menu = new Menu();
            string[] pMenuOptions = { _languageManager.Translate("new_player"),
                _languageManager.Translate("list_all"),
                _languageManager.Translate("clear_all"),
                _languageManager.Translate("back") };
            int pSelectedIndex = menu.ShowMenu(pMenuOptions,_languageManager.Translate("players"),0);

            if (pSelectedIndex == pMenuOptions.Length - 1)
            {
                Main();
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
            Menu menu = new Menu();
            string[] menuOptions = { _languageManager.Translate("delete_player"),_languageManager.Translate("back") };
            
            int selectedIndex = menu.ShowMenu(menuOptions, $"{_languageManager.Translate("details_of")} {playerName}", 0, PlayerManager.DisplayPlayerInfo(playerName),false);

            switch (selectedIndex)
            {
                case 0:
                    // Apagar Jogador (Delete Player)
                    Console.WriteLine();
                    bool deleteConfirmation = menu.ConfirmAction(_languageManager.Translate("confirm_delete",playerName));
                    
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
            Menu menu = new Menu();
            string[] playerNames = PlayerManager.ListAllPlayerNames();

            string[] menuOptions = new string[playerNames.Length + 1];
            Array.Copy(playerNames, menuOptions, playerNames.Length);
            menuOptions[playerNames.Length] = _languageManager.Translate("back");

            int selectedIndex = menu.ShowMenu(menuOptions, _languageManager.Translate("players_list"), 0);

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
            Menu menu = new Menu();

            string[] optionsMenuOptions = { _languageManager.Translate("language"),
                _languageManager.Translate("color"), 
                _languageManager.Translate("back") };
            int selectedIndex = menu.ShowMenu(optionsMenuOptions, _languageManager.Translate("options"), 0);

            if (selectedIndex == optionsMenuOptions.Length - 1)
            {
                Main();
            }
            
            switch (selectedIndex)
            {
                case 0:
                    // User selected "Linguagem" (Language)
                    Console.Clear();
                    ShowLanguageOptions();
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

            string[] languageOptions = { "English", "Português", _languageManager.Translate("back") };
            int selectedIndex = new Menu().ShowMenu(languageOptions, _languageManager.Translate("choose_language"), 0);

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

                // Add more cases for additional languages

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
        
        public static LanguageManager LanguageManager
        {
            get { return _languageManager; }
        }
    }
}