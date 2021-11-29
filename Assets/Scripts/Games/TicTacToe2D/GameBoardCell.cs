namespace GameHub.Games.TicTacToe2D
{ 
    public class GameBoardCell
    {
        public IPlayer Claim { get; protected internal set; }

        public int Row { get; protected internal set; }

        public int Column { get; protected internal set; }  

        public GameBoardCell( int row, int column, IPlayer player = null )
        {
            Row = row;
            Column = column;
            Claim = player;
        }
    }
}
