namespace GameHub.Games.TicTacToe2D
{
    public class PlayerMove
    {
        public IPlayer Player { get; private set; }

        public int Row { get; private set; }

        public int Column { get; private set; }

        public PlayerMove( IPlayer player, int row, int column )
        {
            Player = player;
            Row = row;
            Column = column;    
        }
    }
}
