using System;
using System.Collections.Generic;

namespace NemLinha_Projeto
{
    public class Game
    {
        public int GameId { get; set; }
        public int NumberOfPlayers { get; set; }
        public int WinningSequence { get; set; }
        public int SpecialPieceSize { get; set; }
        
        public int BoardHeight { get; set; }
        
        public int BoardWidth { get; set; }
        public List<Player> SelectedPlayerNames { get; set; }
        public int[,] Board { get; set; }
        public DateTime LastModified { get; set; }

        public Game(int gameId,int numberOfPlayers,int winningSequence,int specialPieceSize,List<Player> selectedPlayerNames,int boardHeight,int boardWidth,DateTime timeStamp)
        {
            GameId = gameId;
            NumberOfPlayers = numberOfPlayers;
            WinningSequence = winningSequence;
            SpecialPieceSize = specialPieceSize;
            BoardHeight = boardHeight;
            BoardWidth = boardWidth;
            SelectedPlayerNames = selectedPlayerNames;

            // Initialize the 3D array (board) with dimensions BoardHeight x BoardWidth
            Board = new int[boardHeight, boardWidth];
            LastModified = timeStamp;
        }
    }
}
