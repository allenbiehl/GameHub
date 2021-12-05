namespace GameHub.Games.TicTacToe2D.Event
{ 
    /// <summary>
    /// Class <c>BoardClickEvent</c> represents a game listener event and 
    /// is passed to registered subscribers.
    /// </summary>
    public class BoardClickEvent
    {
        /// <summary>
        /// Property <c>Row</c> is the game board cell row that the player 
        /// clicked on.
        /// </summary>
        public int Row { get; set; }    

        /// <summary>
        /// Property <c>Column</c> is the game board cell column that the 
        /// player clicked on.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Constructor for the <c>BoardClickEvent</c>.
        /// </summary>
        /// <param name="row">
        /// <c>row</c> is the game board cell row that the player clicked on.
        /// </param>
        /// <param name="column">
        /// <c>column</c> is the game board cell column that the player clicked on.
        /// </param>
        public BoardClickEvent( int row, int column )
        {
            Row = row;
            Column = column;
        }
    }
}
