using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                        PlayerManager.ClearAllPlayers();
                        break;
                    case 2:
                        DrawPlayerMenu();
                        
                        
                        string addPlayerResult1 = PlayerManager.AddPlayer("Player1");
                        string addPlayerResult2 = PlayerManager.AddPlayer("Player2");

                        Console.WriteLine(addPlayerResult1);
                        Console.WriteLine(addPlayerResult2);

                        // Example: Update player's statistics using the method in the PlayerManager class
                        string updateResult1 = PlayerManager.UpdatePlayerStats("Player1", player =>
                        {
                            player.GamesPlayed += 10;
                            player.Victories += 5;
                        });

                        string updateResult2 = PlayerManager.UpdatePlayerStats("Player2", player =>
                        {
                            player.GamesPlayed += 15;
                            player.Victories += 8;
                        });

                        Console.WriteLine(updateResult1);
                        Console.WriteLine(updateResult2);

                        // Example: Display updated player information
                        List<Player> players = PlayerManager.LoadPlayers();
                        foreach (var player in players)
                        {
                            player.DisplayPlayerInfo();
                            Console.WriteLine();
                        }

                        Console.ReadKey();
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
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine($"ERRO: {pMenuOptions[pSelectedIndex]}");
                    break;
            }
        }

        static void AddPlayerForm()
        {
            Console.Clear();
            string username = GetValidUsername();
            Console.WriteLine($"Nome: {username}");
            Console.ReadLine();
        }
        
        static string GetValidUsername()
        {
            string input;

            do
            {
                Console.Write("Digite o username: ");
                input = Console.ReadLine();

            } while (!IsValidInput(input));

            // Extrair o nome de usuário do input
            string username = input.Substring("Nome:".Length).Trim();

            return username;
        }

        static bool IsValidInput(string input)
        {
            string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return parts.Length == 2 && parts[0].Equals("Nome:", StringComparison.OrdinalIgnoreCase) && IsValidUsername(parts[1]);
        }

        static bool IsValidUsername(string username)
        {
            return !string.IsNullOrEmpty(username) &&
                   username.Length <= 21 &&
                   System.Text.RegularExpressions.Regex.IsMatch(username, "^[a-zA-Z0-9]+$");
        }

        static void CloseProgram()
        {
            Console.Clear();
            Console.WriteLine("Closing the program. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
    }
}