using System;
using System.Collections.Generic;

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
                        Console.WriteLine($"You selected: {menuOptions[selectedIndex]}");
                        Console.ReadKey();
                        break;
                        
                }
            }
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