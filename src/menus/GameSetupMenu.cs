using System;
using System.Collections.Generic;

namespace NemLinha_Projeto
{
    public class GameSetupMenu
    {
        private List<Player> selectedPlayers = new List<Player>();
        private int currentPlayerIndex = 0;

        private int numberOfPlayers = 2;
        private int boardHeight = 9; // Default board height
        private int boardWidth = 9;  // Default board width
        private int winningSequence = 1; // Default winning sequence
        private int specialPieceSize=0; // Default special peice size
        private int specialPiecesPerPlayer = 0; //Default special piece quantity

        public Game StartGameSetup()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Game Setup!");

            Game newGame = ConfigureGame();
            if (newGame == null)  return null;
            
            // Display final setup
            Console.Clear();
            Console.WriteLine("Game Setup Complete:");
            Console.WriteLine($"Board Size: {boardHeight}x{boardWidth}");
            Console.WriteLine($"Winning Sequence: {newGame.WinningSequence}");
            Console.WriteLine($"Special Piece per Player: {newGame.SpecialPiecePerPlayer}");
            Console.WriteLine($"Special Piece Size: {newGame.SpecialPieceSize}");
            Console.WriteLine("Selected Players:");

            foreach (var player in selectedPlayers)
            {
                Console.WriteLine(player.Name);
            }

            // Save the game setup
            GameManager.AddGame(newGame);

            // Continue with the game or perform additional setup logic as needed
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return newGame;
        }

        private Game ConfigureGame()
        {
            Console.Clear();
            Console.WriteLine("Use arrow keys to navigate, press Enter to select or modify options:");
            string[] options = UpdateOptions();

            int selectedIndex = 0;

            ConsoleKeyInfo key;
            do
            {
                Console.Clear();

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (options.Length - 1 == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        if (options.Length - 2 == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                    }

                    Console.WriteLine(options[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex<0)
                    {
                        selectedIndex = options.Length-1;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex>options.Length - 1)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    HandleOptionSelection(selectedIndex);

                    // Reset special piece size if winning sequence is changed
                    if (selectedIndex == 2)
                    {
                        specialPieceSize = 0; // Reset to default value or prompt for a new value
                    }

                    // Ensure winning sequence, special piece size, and board size are within the new board size limits
                    if (selectedIndex == 1)
                    {
                        winningSequence = Math.Min(winningSequence, Math.Min(boardHeight, boardWidth));
                        specialPieceSize = Math.Min(specialPieceSize, Math.Min(winningSequence, Math.Min(boardHeight, boardWidth)));
                    }

                    // Clear selected players if the number of players is decreased
                    if (selectedIndex == 0 && selectedPlayers.Count > numberOfPlayers)
                    {
                        selectedPlayers.Clear();
                    }

                    if (selectedIndex == options.Length - 2)
                    {
                        // Check if at least 2 players are selected before allowing "Continue"
                        if (selectedPlayers.Count != numberOfPlayers)
                        {
                            Console.WriteLine("Must select all the players that will play first");
                            Console.ReadKey();
                        }
                        else
                        {
                            // If 2 or more players are selected, exit the loop
                            break;
                        }
                    }

                    if (selectedIndex == options.Length - 1)
                    {
                        Program.DrawMainMenu();
                    }

                    options = UpdateOptions();
                }

            } while (true);
            
            selectedPlayers.Sort((p1, p2) => String.CompareOrdinal(p1.Name, p2.Name));

            List<GamePlayer> playersData = new List<GamePlayer>();
            
            foreach (var player in selectedPlayers)
            {
                playersData.Add(new GamePlayer(player, specialPiecesPerPlayer));
            }
            // Create a new Game object with the setup information
            Game newGame = new Game(0, numberOfPlayers, winningSequence, specialPiecesPerPlayer,specialPieceSize, playersData, boardHeight, boardWidth, DateTime.Now);

            return newGame;
        }

        private void HandleOptionSelection(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    Console.Write("\nEnter the number of players (should be greater than 2): ");
                    //numberOfPlayers = GetIntInput(minValue: 2); Removed becouse current only 2 players can play
                    break;

                case 1:
                    GetBoardSize();
                    break;

                case 2:
                    Console.Write("\nEnter the winning sequence: ");
                    winningSequence = GetIntInput(minValue: 1);
                    // Ensure winning sequence is not greater than board size
                    winningSequence = Math.Min(winningSequence, Math.Min(boardHeight, boardWidth));
                    break;
                
                case 3:
                    Console.Write("\nEnter the special pieces quantity per player: ");
                    specialPiecesPerPlayer = GetIntInput(minValue: 0);
                    break;

                case 4:
                    Console.Write("\nEnter the special piece size: ");
                    specialPieceSize = GetIntInput(minValue: 1);
                    // Ensure special piece size is not greater than winning sequence and not greater than board size
                    specialPieceSize = Math.Min(specialPieceSize, Math.Min(winningSequence, Math.Min(boardHeight, boardWidth)));
                    break;

                case 5:
                    if (numberOfPlayers >= 2)
                    {
                        if (!SelectPlayers())
                        {
                            Console.Clear();
                            Program.DrawPlayerMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nPlease set the number of players to be greater than 2 first.");
                        Console.ReadKey();
                    }
                    break;
            }
        }

        private void GetBoardSize()
        {
            Console.Write("Enter the board height: ");
            boardHeight = GetIntInput(minValue: 9); // Minimum board height is 9

            Console.Write("Enter the board width: ");
            boardWidth = GetIntInput(minValue: 9); // Minimum board width is 9
        }

        private string[] UpdateOptions()
        {
            return new string[] {
                $"Number of Players: {numberOfPlayers}",
                $"Board Size: {boardHeight}x{boardWidth}",
                $"Winning Sequence: {winningSequence}",
                $"Special Piece quantity per player: {specialPiecesPerPlayer}",
                $"Special Piece Size: {specialPieceSize}",
                "Select Players",
                "\nContinue -->",
                "\n\nExit"
            };
        }

        private bool SelectPlayers()
        {
            Console.Clear();
            // Check if there are any players registered
            if (PlayerManager.GetNumberOfPlayers() == 0)
            {
                Console.WriteLine("No players registered. Please register players before setting up the game.");
                Console.ReadKey();
                return false;
            }else
            if(PlayerManager.GetNumberOfPlayers()<numberOfPlayers)
            {
                Console.WriteLine("Not enouth players registered. Please register more players before setting up the game.");
                Console.ReadKey();
                return false;
            }
            
            Console.WriteLine("Use arrow keys to navigate, press Enter to select or deselect options.");
            
            int selectedCount = 0;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                Console.WriteLine("Select Players:");

                string[] playerNames = PlayerManager.ListAllPlayerNames();

                for (int i = 0; i < playerNames.Length; i++)
                {
                    if (i == currentPlayerIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(playerNames[i]);

                    Console.ResetColor();

                    if (selectedPlayers.Exists(p => p.Name == playerNames[i]))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" (Selected)");
                        Console.ResetColor();
                    }

                    Console.WriteLine();
                }

                Console.WriteLine($"\nCurrent Selection: {selectedPlayers.Count}/{numberOfPlayers}");
                Console.WriteLine("Selected Players:");

                for (int i = 0; i < selectedPlayers.Count; i++)
                {
                    Console.Write(selectedPlayers[i].Name);

                    Console.ResetColor();

                    Console.Write(" ");
                }

                Console.WriteLine("\nPress Enter to toggle selection. Press Esc to continue.");

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && currentPlayerIndex > 0)
                {
                    currentPlayerIndex--;
                }
                else if (key.Key == ConsoleKey.DownArrow && currentPlayerIndex < playerNames.Length - 1)
                {
                    currentPlayerIndex++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (selectedPlayers.Exists(p => p.Name == playerNames[currentPlayerIndex]))
                    {
                        // Deselect the player if already selected
                        selectedPlayers.Remove(selectedPlayers.Find(p =>
                            p.Name == playerNames[currentPlayerIndex]));
                        selectedCount--;
                    }
                    else
                    {
                        if (selectedPlayers.Count < numberOfPlayers)
                        {
                            // Check if the player is not already selected
                            if (selectedCount < numberOfPlayers)
                            {
                                selectedPlayers.Add(new Player(playerNames[currentPlayerIndex], 0, 0));
                                selectedCount++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Max players reached");
                        }
                    }
                }

            } while (key.Key != ConsoleKey.Escape && selectedCount < numberOfPlayers);

            return true;
        }

        private int GetIntInput(int minValue)
        {
            int result;
            string input = Console.ReadLine();

            while (!int.TryParse(input, out result) || result < minValue)
            {
                Console.Write($"Invalid input. Please enter a valid number greater than or equal to {minValue}: ");
                input = Console.ReadLine();
            }
            return result;
        }
    }
}
