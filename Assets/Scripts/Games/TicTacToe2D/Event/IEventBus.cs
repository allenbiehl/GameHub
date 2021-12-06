using GameHub.Core.Event;

namespace GameHub.Games.TicTacToe2D.Event
{
    /// <summary>
    /// Interface <c>IEventBus</c> is the public api for the event bus implementation.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Property <c>NewSeriesEvents</c> is used to notify subscribers
        /// when a new game series started.
        /// </summary>
        ActionNotification<GameEvent> NewSeriesEvents { get; }

        /// <summary>
        /// Property <c>NewGameEvents</c> is used to notify subscribers
        /// when a new game started.
        /// </summary>
        ActionNotification<GameEvent> NewGameEvents { get; }

        /// <summary>
        /// Property <c>TieGameEvents</c> is used to notify subscribers
        /// when the game completes and there is a tie.
        /// </summary>
        ActionNotification<GameEvent> TieGameEvents { get; }

        /// <summary>
        /// Property <c>PlayerClaimEvents</c> is used to notify subscribers
        /// when a player made a move and claimed an open cell on the board.
        /// </summary>
        ActionNotification<PlayerClaimEvent> PlayerClaimEvents { get; }

        /// <summary>
        /// Property <c>PlayerWinEvents</c> is used to notify subscribers
        /// when the game completes and one player won.
        /// </summary>
        ActionNotification<PlayerWinEvent> PlayerWinEvents { get; }

        /// <summary>
        /// Property <c>BoardClickEvents</c> is used to notify subscribers
        /// when the player clicked on a cell on the board.
        /// </summary>
        ActionNotification<BoardClickEvent> BoardClickEvents { get; }
    }
}
