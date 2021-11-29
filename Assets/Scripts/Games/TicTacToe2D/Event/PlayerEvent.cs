namespace GameHub.Games.TicTacToe2D.Event
{
    public class PlayerEvent : GameEvent
    {
        public IPlayer Player { get; set; }

        public PlayerEvent( IPlayer player )
        {
            Player = player;
        }
    }
}