using System;
using System.Linq;

namespace NemLinha_Projeto
{
    public class Grid
    {
        private string _grid;
        private string _topLine = "";
        private string _midGridDivision = "";
        private string _botLine = "";

        public void DisplayGrid(int rows, int columns, int[,] gameGrid, int currentPlayer, int player1PiecesLeft, int player2PiecesLeft,int pieceLenght,int pieceColumnToShow = -1)
        {
            Console.Clear(); // Clear the console to refresh the display
            // Generate and display the player information boxes side by side
            DisplayPlayerInfoBox("Player 1 (O)","Player 2 (X)", player1PiecesLeft,player2PiecesLeft, currentPlayer == 1);

            // Add space above the grid for showing a game piece in the specified column
            if (pieceColumnToShow >= 0 && pieceColumnToShow < columns)
            {
                string piece = currentPlayer == 1 ? "  O " : "  X ";
                string header = "";
                Console.ForegroundColor = currentPlayer == 2 ? ConsoleColor.DarkRed : ConsoleColor.DarkBlue;
                for (int i = 0; i < pieceLenght; i++)
                {
                    header += piece;
                }
                Console.WriteLine(new string(' ', 4*pieceColumnToShow) + $"{header}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
            }
            
            // Generate and display the grid
            if (_grid is null)
            {
                GenerateGridBoundaries(columns, 3);
                this._grid = GenerateGrid(rows, columns, gameGrid, 3);
            }
            else
            {
                this._grid = GenerateGrid(rows, columns, gameGrid, 3);
            }

            Console.WriteLine(_grid);
        }

        private void GenerateGridBoundaries(int columns, int cellWidth = 5)
        {
            string horizontalLine = new string('─', cellWidth);

            // Top Line of Grid
            _topLine += "\u250c";
            for (int c = 0; c < columns; c++)
            {
                _topLine += horizontalLine;
                if (c + 1 == columns)
                {
                    _topLine += $"\u2510";
                }
                else
                {
                    _topLine += $"\u252c";
                }
            }
            _topLine += "\n";

            // Mid Line of Grid
            _midGridDivision += $"\u251c";
            for (int c = 0; c < columns; c++)
            {
                _midGridDivision += horizontalLine;
                if (c + 1 == columns)
                {
                    _midGridDivision += $"\u2524";
                }
                else
                {
                    _midGridDivision += $"\u253c";
                }
            }
            _midGridDivision += "\n";

            // Bot Line of Grid
            _botLine += $"\u2514";
            for (int c = 0; c < columns; c++)
            {
                _botLine += horizontalLine;
                if (c + 1 == columns)
                {
                    _botLine += $"\u2518";
                }
                else
                {
                    _botLine += $"\u2534";
                }
            }
        }

        public string GenerateGrid(int rows, int columns, int[,] gameGrid, int cellHeight = 5)
        {
            string grid = "";

            string verticalLine = new string(' ', cellHeight);

            grid += this.Lines(verticalLine, rows, columns, gameGrid);

            return grid;
        }

        private string Lines(string verticalLine, int rows, int columns, int[,] gameGrid = null)
        {
            string lineRows = "";
            lineRows += _topLine;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (gameGrid != null)
                    {
                        string cellContent = $"│{verticalLine}";

                        if (gameGrid[r, c] == 1)
                        {
                            cellContent = $"│ O ";
                        }
                        else if (gameGrid[r, c] == 2)
                        {
                            cellContent = $"│ X ";
                        }

                        lineRows += cellContent;
                    }
                }
                lineRows += "│\n";

                if (r < rows - 1)
                {
                    lineRows += _midGridDivision;
                }
                else
                {
                    lineRows += _botLine;
                }

            }

            return lineRows;
        }

        private void DisplayPlayerInfoBox(string player1Name,string player2Name, int player1piecesLeft, int player2piecesLeft, bool isCurrentPlayer)
        {
            Console.BackgroundColor = isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("┌──────────────────┐"); // Added space between player boxes
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("     ");
            Console.BackgroundColor = !isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write($"┌──────────────────┐\n"); // Start of the second player box
            
            Console.BackgroundColor = isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write($"│ {player1Name, -16} │"); // Added space between player boxes
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("     ");
            Console.BackgroundColor = !isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write($"│ {player2Name, -16} │\n"); // Start of the second player box
            
            Console.BackgroundColor = isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write($"│ Pieces Left: {player1piecesLeft, 2}  │"); // Added space between player boxes
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("     ");
            Console.BackgroundColor = !isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write($"│ Pieces Left: {player2piecesLeft, 2}  │\n"); // Start of the second player box
            
            Console.BackgroundColor = isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write("└──────────────────┘"); // Added space between player boxes
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("     ");
            Console.BackgroundColor = !isCurrentPlayer ? ConsoleColor.DarkCyan : ConsoleColor.Black;
            Console.Write($"└──────────────────┘\n"); // End of the second player box

            Console.ResetColor();
            Console.WriteLine();
        }

    }
}
