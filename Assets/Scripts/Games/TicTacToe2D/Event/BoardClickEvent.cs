namespace GameHub.Games.TicTacToe2D.Event
{ 
    public class BoardClickEvent
    {
        public int Row { get; set; }    

        public int Column { get; private set; }

        public BoardClickEvent( int row, int column )
        {
            Row = row;
            Column = column;
        }
    }
}
