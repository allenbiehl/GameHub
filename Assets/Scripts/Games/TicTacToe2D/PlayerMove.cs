namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>PlayerMove</c> provides a reference to a player and 
    /// the game board coordinates the player made during their turn.
    /// </summary>
    public class PlayerMove
    {
        /// <summary>
        /// Property <c>Player</c> is the player associated with the move.
        /// </summary>
        public IPlayer Player { get; private set; }

        /// <summary>
        /// Property <c>Row</c> is the 0 based coordinate associated with 
        /// the row.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Property <c>Row</c> is the 0 based coordinate associated with 
        /// the column.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Constructor for the <c>PlayerMove</c>.
        /// </summary>
        /// <param name="player">
        /// <c>player</c> is the player associated with the move.
        /// </param>
        /// <param name="row">
        /// <c>row</c> is the 0 based coordinate associated with 
        /// the row.
        /// </param>
        /// <param name="column">
        /// <c>column</c> is the 0 based coordinate associated with 
        /// the column.
        /// </param>
        public PlayerMove( IPlayer player, int row, int column )
        {
            Player = player;
            Row = row;
            Column = column;    
        }
    }
}
