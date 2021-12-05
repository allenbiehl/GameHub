using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.Event
{
    /// <summary>
    /// Class <c>PlayerWinEvent</c> is used to notify subscribers when a player wins
    /// the game. We attach the associated player and the winning cells which are passed
    /// to the subscribers for further processing.
    /// </summary>
    public class PlayerWinEvent : PlayerEvent
    {
        /// <summary>
        /// Property <c>WinCells</c> is the sequential game board cells the player 
        /// selected to win the game.
        /// </summary>
        public List<GameBoardCell> WinCells { get; private set; }

        /// <summary>
        /// Constructor for the <c>PlayerwinEvent</c>.
        /// </summary>
        /// <param name="player">
        /// <c>player</c> is the game player that claimed the open cell.
        /// </param>
        /// <param name="winCells">
        /// <c>winCells</c> is the sequential game board cells that player selected to 
        /// win the game.
        /// </param>
        public PlayerWinEvent( IPlayer player, List<GameBoardCell> winCells ) : base( player )
        {
            WinCells = winCells;
        }
    }
}
