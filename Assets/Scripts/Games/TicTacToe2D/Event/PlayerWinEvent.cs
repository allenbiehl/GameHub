using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.Event
{
    public class PlayerWinEvent : PlayerEvent
    {
        public List<GameBoardCell> WinCells { get; private set; }

        public PlayerWinEvent( IPlayer player, List<GameBoardCell> winCells ) : base( player )
        {
            WinCells = winCells;
        }
    }
}
