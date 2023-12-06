using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                        PlayerManager.ClearAllPlayers();
                        break;
                    case 2:
                        DrawPlayerMenu();

                        // Example: Update player's statistics using the method in the PlayerManager class
                        /*string updateResult1 = PlayerManager.UpdatePlayerStats("Player1", player =>
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

                        Console.ReadKey();*/
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
            Thread.Sleep(550);
        }
        
        static string GetValidUsername()
        {
            string username;

            do
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine();

                if (!IsValidUsername(username))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Usernames must not contain special characters or spaces and should be 1 to 21 characters long. Please try again.");
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
            Console.WriteLine("Closing the program. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
    }
}