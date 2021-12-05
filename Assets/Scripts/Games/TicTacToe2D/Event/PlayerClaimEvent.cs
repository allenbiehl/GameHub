namespace GameHub.Games.TicTacToe2D.Event
{
    /// <summary>
    /// Class <c>PlayerClaimEvent</c> is used to notify subscribers when a player selects 
    /// an open game board cell and claims it. We attach the associated <c>PlayerMove</c>
    /// that the player made and pass the event to the subscribers for further processing.
    /// </summary>
    public class PlayerClaimEvent : PlayerEvent
    {
        /// <summary>
        /// Property <c>PlayerMove</c> is the move made by the player.
        /// </summary>
        public PlayerMove PlayerMove { get; protected internal set; }

        /// <summary>
        /// Constructor for <c>PlayerClaimEvent</c> which provides the associated 
        /// <c>IPlayer</c> and <c>PlayerMove</c> that was made.
        /// </summary>
        /// <param name="player">
        /// <c>player</c> is the game player that claimed the open cell.
        /// </param>
        /// <param name="playerMove">
        /// <c>playerMOve</c> is the move that occurred by the player.
        /// </param>
        public PlayerClaimEvent( IPlayer player, PlayerMove playerMove ) : base( player )   
        {
            PlayerMove = playerMove;
        }
    }
}
