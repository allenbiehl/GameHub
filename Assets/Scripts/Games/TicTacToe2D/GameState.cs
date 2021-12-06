using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>GameState</c> represents the current game being played or
    /// and game that was completed. 
    /// </summary>
    public class GameState
    { 
        /// <summary>
        /// Property <c>GameBoard</c> is the virtual game board associated
        /// with this game.
        /// </summary>
        public GameBoard GameBoard { get; private set; }

        /// <summary>
        /// Property <c>Status</c> is the current game status, whether the
        /// game is started or completed.
        /// </summary>
        public GameStatus Status { get; protected internal set; }

        /// <summary>
        /// Property <c>InitialPlayer</c> is the offensive player who made
        /// or will make the first move for the game. This is used in 
        /// subsequent games to determine the next offensive player.
        /// </summary>
        public IPlayer InitialPlayer { get; private set; }

        /// <summary>
        /// Property <c>WinningPlayer</c> is the player who won the game
        /// and claimed victory.
        /// </summary>
        public IPlayer WinningPlayer { get; protected internal set; }

        /// <summary>
        /// Property <c>CurrentPlayer</c> is the player who has the current
        /// turn and needs to make a selection based on the current available
        /// game board cells.
        /// </summary>
        public IPlayer CurrentPlayer { get; protected internal set; }

        /// <summary>
        /// Property <c>LengthToWin</c> is the total number of sequential cell
        /// claimed by a user required to win. Default is 3.
        /// </summary>
        public int LengthToWin { get; private set; }

        /// <summary>
        /// Property <c>PlayerMoves</c> contains a list of all historical plays 
        /// made in succession by both players. 
        /// </summary>
        public List<PlayerMove> PlayerMoves { get; set; }

        /// <summary>
        /// Constructor for <c>GameState</c>.
        /// </summary>
        /// <param name="gameBoard">
        /// <c>gameBoard</c> is the virtual game board associated with this game.
        /// </param>
        /// <param name="initialPlayer">
        /// <c>initialPlayer</c> is the offsensive player for this game. This changes
        /// each subsequent game played between the two players in the same session.
        /// </param>
        /// <param name="lengthToWin">
        /// <c>lengthToWin</c> is the total number of sequential cells required to win.
        /// </param>
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
