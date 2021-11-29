using GameHub.Core.Event;

namespace GameHub.Games.TicTacToe2D.Event
{
    public class EventBus
    {
        public ActionNotification<GameEvent> NewSeriesEvents =
            new ActionNotification<GameEvent>();

        public ActionNotification<GameEvent> NewGameEvents =
            new ActionNotification<GameEvent>();

        public ActionNotification<GameEvent> TieGameEvents =
            new ActionNotification<GameEvent>();

        public ActionNotification<PlayerClaimEvent> PlayerClaimEvents =
            new ActionNotification<PlayerClaimEvent>();

        public ActionNotification<PlayerWinEvent> PlayerWinEvents =
            new ActionNotification<PlayerWinEvent>();

        public ActionNotification<BoardClickEvent> BoardClickEvents =
            new ActionNotification<BoardClickEvent>();
    }
}
