using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D
{
    public class GameState
    { 
        public GameBoard GameBoard { get; private set; }

        public GameStatus Status { get; protected internal set; }

        public IPlayer InitialPlayer { get; private set; }

        public IPlayer WinningPlayer { get; protected internal set; }

        public IPlayer CurrentPlayer { get; protected internal set; }

        public int LengthToWin { get; private set; }

        public List<PlayerMove> PlayerMoves { get; set; }

        public GameState( GameBoard gameBoard, IPlayer initialPlayer, int lengthToWin )
        {
            GameBoard = gameBoard;  
            InitialPlayer = initialPlayer;
            CurrentPlayer = initialPlayer;
            Status = GameStatus.Initialized;
            LengthToWin = lengthToWin;
            PlayerMoves = new List<PlayerMove>();
        }
    }
}
