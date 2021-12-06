namespace GameHub.Games.TicTacToe2D
{ 
    /// <summary>
    /// Class <c>GameBoardCell</c> represents the individual game board cell contained
    /// on a virtual game board. The game board cell maintains 0 based coordinates for
    /// the row and cell as well as the player who claimed the cell. If the Claim is
    /// populated then the cell is claimed, otherwise the cell is available.
    /// </summary>
    public class GameBoardCell
    {
        /// <summary>
        /// Property <c>Claim</c> is used to store the player who claimed the game
        /// board cell. If the claim is undefined, then the cell is available.
        /// </summary>
        public IPlayer Claim { get; protected internal set; }

        /// <summary>
        /// Property <c>Row</c> is the 0 based coordinate of the game board row.
        /// </summary>
        public int Row { get; protected internal set; }

        /// <summary>
        /// Property <c>Column</c> is the 0 based coordinate of the game board column.
        /// </summary>
        public int Column { get; protected internal set; }

        /// <summary>
        /// Constructor for the <c>GameBoardCell</c>.
        /// </summary>
        /// <param name="row">
        /// <c>row</c> is the 0 based coordinate of the game board row.
        /// </param>
        /// <param name="column">
        /// <c>column</c> is the 0 base coordinate of the game board column.
        /// </param>
        /// <param name="player">
        /// <c>player</c> is the player who claimed the game board cell.
        /// </param>
        public GameBoardCell( int row, int column, IPlayer player = null )
        {
            Row = row;
            Column = column;
            Claim = player;
        }
    }
}
