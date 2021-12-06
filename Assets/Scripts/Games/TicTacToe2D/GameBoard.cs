
using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D
{ 
    /// <summary>
    /// Class <c>GameBoard</c> represents the virtual tic tac toe game board. 
    /// It stores a two dimensional list of game board cells and their claimed 
    /// stated, whether the cell is claimed and populated with a player or empty,
    /// meaning the cell is available.
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// Property <c>Cells</c> is a two dimensional array of game board cells.
        /// </summary>
        public GameBoardCell[,] Cells { get; private set; }

        /// <summary>
        /// Property <c>Size</c> is the dimension of the board. i.e. 3 cells x 3 cells.
        /// </summary>
        public int Size { 
            get {
                return Cells.GetLength(0);    
            } 
        }

        /// <summary>
        /// Property <c>AvailableCells</c> returns the total number of game board cells
        /// that have not been claimed and are available for selection.
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

        /// <summary>
        /// Constructor for <c>GameBoard</c>.
        /// </summary>
        /// <param name="size">
        /// <c>size</c> is the total number of cells.
        /// </param>
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
