using GameHub.Core.Event;

namespace GameHub.Games.TicTacToe2D.Event
{
    /// <summary>
    /// Class <c>EventBus</c> is used by the <c>GameManager</c> to manage event
    /// subsribers. The <c>EventBus</c> provides communication channels for
    /// components to notify other components when a game event occurs. This
    /// model is highly scalable and the interaction between the publisher
    /// and subscriber is completely decoupled.
    /// </summary>
    public class EventBus
    {
        /// <summary>
        /// Property <c>NewSeriesEvents</c> is used to notify subscribers
        /// when a new game series started.
        /// </summary>
        public ActionNotification<GameEvent> NewSeriesEvents { get; } 
            = new ActionNotification<GameEvent>();

        /// <summary>
        /// Property <c>NewGameEvents</c> is used to notify subscribers
        /// when a new game started.
        /// </summary>
        public ActionNotification<GameEvent> NewGameEvents { get; }
            = new ActionNotification<GameEvent>();

        /// <summary>
        /// Property <c>TieGameEvents</c> is used to notify subscribers
        /// when the game completes and there is a tie.
        /// </summary>
        public ActionNotification<GameEvent> TieGameEvents { get; }
            = new ActionNotification<GameEvent>();

        /// <summary>
        /// Property <c>PlayerClaimEvents</c> is used to notify subscribers
        /// when a player made a move and claimed an open cell on the board.
        /// </summary>
        public ActionNotification<PlayerClaimEvent> PlayerClaimEvents { get; }
            = new ActionNotification<PlayerClaimEvent>();

        /// <summary>
        /// Property <c>PlayerWinEvents</c> is used to notify subscribers
        /// when the game completes and one player won.
        /// </summary>
        public ActionNotification<PlayerWinEvent> PlayerWinEvents { get; }
            = new ActionNotification<PlayerWinEvent>();

        /// <summary>
        /// Property <c>BoardClickEvents</c> is used to notify subscribers
        /// when the player clicked on a cell on the board.
        /// </summary>
        public ActionNotification<BoardClickEvent> BoardClickEvents { get; }
            = new ActionNotification<BoardClickEvent>();
    }
}
