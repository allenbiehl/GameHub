namespace GameHub.Games.TicTacToe2D.Event
{
    /// Class <c>PlayerEvent</c> represents the base player game event used 
    /// by the event notification sytem. If you just need to notify
    /// subscribers that an event occurred associated with a particular player, 
    /// then you can use this generic event instance. If however you need to pass 
    /// additional information associated with the event to the subscribers then 
    /// you should implement a custom event implementation specific to your need.
    public class PlayerEvent : GameEvent
    {
        /// <summary>
        /// Property <c>IPlayer</c> is the player implementation associated with 
        /// the event that occurred.
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// Constructor for the <c>PlayerEvent</c>.
        /// </summary>
        /// <param name="player">
        /// <c>player</c> is the game player that claimed the open cell.
        /// </param>
        public PlayerEvent( IPlayer player )
        {
            Player = player;
        }
    }
}