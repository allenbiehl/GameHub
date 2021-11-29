namespace GameHub.Games.TicTacToe2D.Event
{
    public class PlayerClaimEvent : PlayerEvent
    {
        public PlayerMove PlayerMove { get; protected internal set; }

        public PlayerClaimEvent( IPlayer player, PlayerMove playerMove ) : base( player )   
        {
            PlayerMove = playerMove;
        }
    }
}
