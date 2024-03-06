using System;
using System.Collections.Generic;

namespace NemLinha_Projeto
{
    public class LoadGameMenu
    {
        public static Game DisplayMenu()
        {
            Console.WriteLine("Load Game Menu");
            Console.WriteLine("-----------------");

            // Display a list of saved games
            List<Game> savedGames = GameManager.LoadGames();
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear(); // Clear the console to refresh the display

                Console.WriteLine("Load Game Menu");
                Console.WriteLine("-----------------");

                // Display saved games with the selected game highlighted
                for (int i = 0; i < savedGames.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }

                    Console.WriteLine($"{i + 1}. Game ID: {savedGames[i].GameId}");

                    Console.ResetColor();
                }

                // Highlight the "Back to Main Menu" option if it is selected
                Console.ForegroundColor = (selectedIndex == savedGames.Count) ? ConsoleColor.Black : ConsoleColor.Gray;
                Console.BackgroundColor = (selectedIndex == savedGames.Count) ? ConsoleColor.Red : ConsoleColor.Black;

                Console.WriteLine("\nBack to Main Menu");

                Console.ResetColor();
                Console.Write("\nUse arrow keys to navigate, and press Enter to select: ");

                // Read user input
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Max(0, selectedIndex - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Min(savedGames.Count, selectedIndex + 1);
                        break;

                    case ConsoleKey.Enter:
                        if (selectedIndex == savedGames.Count)
                        {
                            // User selected to go back to the main menu
                            Console.WriteLine("\nReturning to Main Menu...");
                            return null; // or you can define a default behavior
                        }
                        else
                        {
                            // Load the selected game
                            Game selectedGame = savedGames[selectedIndex];
                            Console.WriteLine($"\nLoading Game ID: {selectedGame.GameId}...");

                            // Add your logic to update the loaded game if needed
                            // For example, you might want to update the LastModified property
                            selectedGame.LastModified = DateTime.Now;
                            GameManager.UpdateGame(selectedGame);

                            return selectedGame;
                        }

                    default:
                        break;
                }
            }
        }
    }
}
