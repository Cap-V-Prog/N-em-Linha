using System;
using System.Threading;

namespace NemLinha_Projeto
{
    class Program
    {
        static void Main()
        {
            Menu menu = new Menu();
            
            Console.CursorVisible = false;
            
            string[] menuOptions = { "Novo jogo", "Continuar","Jogadores", "Opções", "Sair" };
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
            string[] pMenuOptions = { "Novo Jogador", "Listar todos","Apagar todos", "Voltar" };
            int pSelectedIndex = menu.ShowMenu(pMenuOptions,"Jogadores",0);

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
            string[] menuOptions = { "Apagar Jogador","Voltar" };

            int selectedIndex = menu.ShowMenu(menuOptions, $"Detalhes de {playerName}", 0,PlayerManager.DisplayPlayerInfo(playerName),false);

            switch (selectedIndex)
            {
                case 0:
                    // Apagar Jogador (Delete Player)
                    bool deleteConfirmation = menu.ConfirmAction($"Tem certeza que deseja apagar o jogador {playerName}?");

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
            menuOptions[playerNames.Length] = "Voltar";

            int selectedIndex = menu.ShowMenu(menuOptions, "Lista de jogadores", 0);

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
            
            Console.WriteLine(PlayerManager.AddPlayerStringOut(addPlayerResult,username));
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
                Console.Write("Introduza o nome de utilizador:");
                username = Console.ReadLine();

                if (!IsValidUsername(username))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entrada inválida! Nomes de usuário não podem conter caracteres especiais ou espaços e devem ter entre 1 e 21 caracteres. Tente novamente.");
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
            Console.WriteLine("Encerrando o programa. Pressione qualquer tecla para sair.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
    }
}