using System;

namespace NemLinha_Projeto
{
    public class GameScreen
    {
        private static void GameEnd(int currentPlayer,Game currentGame,bool Draw=false)
        {
            if (Draw)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The board is full. It's a draw!");
                Console.ReadKey();
                Console.ResetColor();
                
                foreach (var gamePlayer in currentGame.PlayersData)
                {
                    Action<Player> updateAction = player =>
                    {
                        // Update the player's information
                        player.GamesPlayed++;
                    };
                    Console.WriteLine(PlayerManager.UpdatePlayerStats(gamePlayer.Player.Name, updateAction, Program.LanguageManager));
                }
                
                GameManager.ResetBoard(currentGame.GameId);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The player {currentGame.PlayersData[currentPlayer-1].Player.Name} won the game");
                Console.ResetColor();
                GameManager.ResetBoard(currentGame.GameId);
                Console.ReadKey();
                
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                
                //Update winner player
                string playerNameToUpdate = currentGame.PlayersData[currentPlayer-1].Player.Name;
                Action<Player> updateAction = player =>
                {
                    // Update the player's information
                    player.GamesPlayed++;
                    player.Victories++;
                };
                string resultMessage = PlayerManager.UpdatePlayerStats(playerNameToUpdate, updateAction, Program.LanguageManager);
                Console.WriteLine(resultMessage);
                
                //Update other player
                playerNameToUpdate = currentGame.PlayersData[(currentPlayer == 1 ? 2 : 1)-1].Player.Name;
                updateAction = player =>
                {
                    // Update the player's information
                    player.GamesPlayed++;
                };
                resultMessage = PlayerManager.UpdatePlayerStats(playerNameToUpdate, updateAction, Program.LanguageManager);
                Console.WriteLine(resultMessage);
                Console.ResetColor();
            }
        }
        public static void DrawGameScreen(Game currentGame)
        {
            Grid uIgrid = new Grid();
            int currentColumn = 0;
            int currentPlayer = 1;
            int pieceleght = 1;
            bool onGame = true;

            while (onGame)
            {
                Console.Clear();
                uIgrid.DisplayGrid(currentGame.BoardHeight,currentGame.BoardWidth,currentGame.Board,currentPlayer,currentGame.PlayersData[0].SpecialPiecesCount,currentGame.PlayersData[1].SpecialPiecesCount,pieceleght,currentColumn);
                if (GameLogic.IsBoardFull(currentGame.Board))
                {
                    GameEnd(currentPlayer, currentGame, true);
                    onGame = false;
                    break;
                }
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentColumn - 1 >= 0) currentColumn--;
                        break;
                    case ConsoleKey.RightArrow:

                        if (currentColumn + pieceleght < currentGame.BoardWidth) currentColumn++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentGame.SpecialPieceSize > 0 && currentGame.SpecialPiecePerPlayer > 0)
                        {
                            if (pieceleght == 1)
                            {
                                pieceleght = currentGame.SpecialPieceSize;

                                if (currentColumn + pieceleght > currentGame.BoardWidth)
                                    currentColumn = currentGame.BoardWidth - pieceleght;
                            }
                            else
                            {
                                pieceleght = 1;
                            }
                        }

                        break;
                    case ConsoleKey.Enter:
                        if (pieceleght != 1)
                            if ((currentPlayer == 1
                                    ? currentGame.PlayersData[0].SpecialPiecesCount
                                    : currentGame.PlayersData[1].SpecialPiecesCount) < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("No special pieces left!");
                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            }

                        if (GameLogic.DropPiece(currentGame.Board, currentColumn, currentPlayer, pieceleght))
                        {
                            GameManager.UpdateGame(currentGame);
                            uIgrid.DisplayGrid(currentGame.BoardHeight, currentGame.BoardWidth, currentGame.Board,
                                currentPlayer, currentGame.PlayersData[0].SpecialPiecesCount,
                                currentGame.PlayersData[1].SpecialPiecesCount, pieceleght, currentColumn);
                            if (GameLogic.CheckForWin(currentGame.Board, currentGame.WinningSequence, currentPlayer))
                            {
                                GameEnd(currentPlayer,currentGame);
                                onGame = false;
                                break;
                            }

                            if (currentPlayer == 1)
                            {
                                if (pieceleght != 1) currentGame.PlayersData[0].SpecialPiecesCount--;
                                currentPlayer = 2;
                            }
                            else
                            {
                                if (pieceleght != 1) currentGame.PlayersData[1].SpecialPiecesCount--;
                                currentPlayer = 1;
                            }

                            pieceleght = 1;
                        }

                        break;
                }            
            }
                                                    
        }
    }
}