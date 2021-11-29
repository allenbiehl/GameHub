
using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D
{ 
    public class GameBoard
    {
        public GameBoardCell[,] Cells { get; private set; }

        public int Size { 
            get {
                return Cells.GetLength(0);    
            } 
        }

        public List<GameBoardCell> AvailableCells
        {
            get
            {
                List<GameBoardCell> available = new List<GameBoardCell>();
                GameBoardCell[,] source = Cells;

                for (int row = 0; row < Size; row++)
                {
                    for (int column = 0; column < Size; column++)
                    {
                        GameBoardCell cell = source[row, column];

                        if (cell.Claim == null)
                        {
                            available.Add(cell);
                        }
                    }
                }
                return available;
            }
        }

        public GameBoard( int size )
        { 
            Cells = new GameBoardCell[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Cells[row, column] = new GameBoardCell(row, column);
                }
            }
        }
    }
}
