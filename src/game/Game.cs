using System;
using System.Collections.Generic;

namespace NemLinha_Projeto
{
    public class Game
    {
        public int GameId { get; set; }
        public int NumberOfPlayers { get; set; }
        public int WinningSequence { get; set; }
        
        public int SpecialPiecePerPlayer { get; set; }
        public int SpecialPieceSize { get; set; }
        
        public int currentPlaying { get; set; }
        
        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }
        public int[,] Board { get; set; }
        public DateTime LastModified { get; set; }
        
        public List<GamePlayer> PlayersData{ get; set; }

        public Game(int gameId,int numberOfPlayers,int winningSequence,int specialPiecePerPlayer,int specialPieceSize,List<GamePlayer> playersData,int boardHeight,int boardWidth,DateTime timeStamp)
        {
            GameId = gameId;
            NumberOfPlayers = numberOfPlayers;
            WinningSequence = winningSequence;
            SpecialPiecePerPlayer = specialPiecePerPlayer;
            SpecialPieceSize = specialPieceSize;
            BoardHeight = boardHeight;
            BoardWidth = boardWidth;
            PlayersData = playersData;
            // Initialize the 3D array (board) with dimensions BoardHeight x BoardWidth
            Board = new int[boardHeight, boardWidth];
            currentPlaying = 1;
            LastModified = timeStamp;
        }
        
        public void ResetBoard()
        {
            // Initialize the 2D array (board) with dimensions BoardHeight x BoardWidth
            Board = new int[BoardHeight, BoardWidth];
            foreach (var player in PlayersData)
            {
                player.SpecialPiecesCount = SpecialPiecePerPlayer;
            }
            currentPlaying = 1;
        }
    }
    
    public class GamePlayer
    {
        public Player Player { get; set; }
        public int SpecialPiecesCount { get; set; }

        public GamePlayer(Player player,int specialpieces)
        {
            Player = player;
            SpecialPiecesCount = specialpieces;
        }
    }
}
