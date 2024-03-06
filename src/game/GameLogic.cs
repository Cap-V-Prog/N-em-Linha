using System;

namespace NemLinha_Projeto
{
    public class GameLogic
    {
        public static bool CheckForWin(int[,] board, int winningSequence, int currentPlayer)
        {
            return CheckHorizontal(board, winningSequence, currentPlayer) ||
                   CheckVertical(board, winningSequence, currentPlayer) ||
                   CheckDiagonal(board, winningSequence, currentPlayer);
        }
        
        private static bool CheckVertical(int[,] board, int winningSequence, int currentPlayer, Func<int, int, int> positionSelector)
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);
            
            for (int j = 0; j < columns; j++)
            {
                int count = 0;

                for (int i = 0; i < rows; i++)
                {
                    if (positionSelector(i, j) == currentPlayer)
                    {
                        count++;

                        if (count == winningSequence)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            return false;
        }

        private static bool CheckLine(int[,] board, int winningSequence, int currentPlayer, Func<int, int, int> positionSelector)
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                int count = 0;

                for (int j = 0; j < columns; j++)
                {
                    if (positionSelector(i, j) == currentPlayer)
                    {
                        count++;

                        if (count == winningSequence)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            return false;
        }

        private static bool CheckHorizontal(int[,] board, int winningSequence, int currentPlayer)
        {
            return CheckLine(board, winningSequence, currentPlayer, (i, j) => board[i, j]);
        }

        private static bool CheckVertical(int[,] board, int winningSequence, int currentPlayer)
        {
            return CheckVertical(board, winningSequence, currentPlayer, (i, j) => board[i, j]);
        }

        private static bool CheckDiagonal(int[,] board, int winningSequence, int currentPlayer)
        {
            return CheckMainDiagonal(board, winningSequence, currentPlayer) || CheckAntiDiagonal(board, winningSequence, currentPlayer);
        }

        private static bool CheckDiagonalLine(int[,] board, int winningSequence, int currentPlayer, Func<int, int, int> positionSelector)
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);
            
            for (int i = 0; i <= rows - winningSequence; i++)
            {
                for (int j = 0; j <= columns - winningSequence; j++)
                {
                    int count = 0;

                    for (int k = 0; k < winningSequence; k++)
                    {
                        if (positionSelector(i + k, j + k) == currentPlayer)
                        {
                            count++;

                            if (count == winningSequence)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return false;
        }

        private static bool CheckMainDiagonal(int[,] board, int winningSequence, int currentPlayer)
        {
            return CheckDiagonalLine(board, winningSequence, currentPlayer, (i, j) => board[i, j]);
        }

        private static bool CheckAntiDiagonal(int[,] board, int winningSequence, int currentPlayer)
        {
            return CheckDiagonalLine(board, winningSequence, currentPlayer, (i, j) => board[i, board.GetLength(1) - 1 - j]);
        }
        
        public static bool DropPiece(int[,] board, int column, int playerPiece, int pieceLength)
        {
            if (CanDropPiece(board, column, pieceLength))
            {
                int endColumn = Math.Max(column + (pieceLength - 1), 0);

                for (int j = column; j <= endColumn; j++)
                {
                    int emptyRow = FindLowestEmptyRow(board, j);

                    if (emptyRow != -1)
                    {
                        board[emptyRow, j] = playerPiece;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cannot drop piece. Either column is full or there's not enough space.");
                        Console.ReadKey();
                        return false;
                    }
                }

                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Cannot drop piece. Either column is full or there's not enough space.");
            Console.ReadKey();
            return false;
        }
        
        public static bool CanDropPiece(int[,] board, int column, int pieceLength)
        {
            bool isfull = false;
            int endColumn = Math.Max(column + (pieceLength - 1), 0);

            for (int j = column; j <= endColumn; j++)
            {
                if (!IsColumnFull(board, j))
                {
                    isfull = true;
                }
                else
                {
                    isfull = false;
                    break;
                }
            }

            return isfull;
        }

        private static bool IsColumnFull(int[,] board, int column)
        {
            int rows = board.GetLength(0);

            for (int i = 0; i < rows; i++)
            {
                if (board[i, column] == 0)
                {
                    return false;
                }
            }

            return true; // Column is full
        }

        private static int FindLowestEmptyRow(int[,] board, int column)
        {
            int rows = board.GetLength(0);

            for (int i = rows - 1; i >= 0; i--)
            {
                if (board[i, column] == 0)
                {
                    return i;
                }
            }

            return -1; // Column is full
        }
        
        public static bool IsBoardFull(int[,] board)
        {
            int columns = board.GetLength(1);

            for (int j = 0; j < columns; j++)
            {
                if (!IsColumnFull(board, j))
                {
                    return false; // At least one column is not full
                }
            }

            return true; // All columns are full
        }
    }
}
