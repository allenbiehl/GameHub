namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Class <c>WinPathTriangulation</c> is used to triangulate a winning move based 
    /// on the current player move as the vertext. Rather than scanning the entire game 
    /// board, this class checks a winning condition based on the major 4 win axis. Row,
    /// column, diaganol right and diaganol left.
    /// 
    /// This is somewhat of a duplicate of the <c>PlayerPathTriangulation</c> class,
    /// however it is designed for speed and numeric comparison as opposed to object
    /// comparison.
    /// </summary>
    class WinPathTriangulation
    {
        /// <summary>
        /// Method <c>Winning</c> is used to determine whether the last move made by a
        /// player was a winning move based on the vertex, claimed cells and the required
        /// path win length. 
        /// </summary>
        /// <param name="cells">
        /// <c>cells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerId">
        /// <c>playerId</c> is the current player to evaluate for a win.
        /// </param>
        /// <param name="move">
        /// <c>move</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning move, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Boolean value whether the player won.
        /// </returns>
        public bool Winning(int[,] cells, int playerId, int[] move, int pathLength)
        {
            if (
                CheckRow(cells, playerId, move, pathLength) ||
                CheckColumn(cells, playerId, move, pathLength) ||
                CheckDiagonalRight(cells, playerId, move, pathLength) ||
                CheckDiagonalLeft(cells, playerId, move, pathLength))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method <c>CheckRow</c> is used to check a win based on the row axis.
        /// </summary>
        /// <param name="cells">
        /// <c>cells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerId">
        /// <c>playerId</c> is the current player to evaluate for a win.
        /// </param>
        /// <param name="move">
        /// <c>move</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning move, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Boolean value whether the player won.
        /// </returns>
        private bool CheckRow(int[,] cells, int playerId, int[] move, int pathLength)
        {
            int boardSize = cells.GetLength(0);
            int matches = 0;
            int column = 0;

            while (column < boardSize && matches < pathLength)
            {
                if (cells[move[0], column] == playerId)
                {
                    matches++;
                }
                else
                {
                    matches = 0;
                }
                column++;
            }
            return matches == pathLength;
        }

        /// <summary>
        /// Method <c>CheckColumn</c> is used to check a win based on the column axis.
        /// </summary>
        /// <param name="cells">
        /// <c>cells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerId">
        /// <c>playerId</c> is the current player to evaluate for a win.
        /// </param>
        /// <param name="move">
        /// <c>move</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning move, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Boolean value whether the player won.
        /// </returns>
        private bool CheckColumn(int[,] cells, int playerId, int[] move, int pathLength)
        {
            int boardSize = cells.GetLength(0);
            int matches = 0;
            int row = 0;

            while (row < boardSize && matches < pathLength)
            {
                if (cells[row, move[1]] == playerId)
                {
                    matches++;
                }
                else
                {
                    matches = 0;
                }
                row++;
            }
            return matches == pathLength;
        }

        /// <summary>
        /// Method <c>CheckDiagonalRight</c> is used to check a win based on the diagonal right axis.
        /// </summary>
        /// <param name="cells">
        /// <c>cells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerId">
        /// <c>playerId</c> is the current player to evaluate for a win.
        /// </param>
        /// <param name="move">
        /// <c>move</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning move, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Boolean value whether the player won.
        /// </returns>
        private bool CheckDiagonalRight(int[,] cells, int playerId, int[] move, int pathLength)
        {
            int matches = 0;
            int boardSize = cells.GetLength(0);

            // TODO Find better way to find starting cell
            int row = move[0];
            int column = move[1];

            while (row > 0 && column > 0)
            {
                row--;
                column--;
            }

            while (row < boardSize && column < boardSize && matches < pathLength)
            {
                if (cells[row, column] == playerId)
                {
                    matches++;
                }
                else
                {
                    matches = 0;
                }
                row++;
                column++;
            }
            return matches == pathLength;
        }

        /// <summary>
        /// Method <c>CheckDiagonalLeft</c> is used to check a win based on the diagonal left axis.
        /// </summary>
        /// <param name="cells">
        /// <c>cells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerId">
        /// <c>playerId</c> is the current player to evaluate for a win.
        /// </param>
        /// <param name="move">
        /// <c>move</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning move, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Boolean value whether the player won.
        /// </returns>
        private bool CheckDiagonalLeft(int[,] cells, int playerId, int[] move, int pathLength)
        {
            int matches = 0;
            int boardSize = cells.GetLength(0);

            // TODO Find better way to find starting cell
            int row = move[0];
            int column = move[1];

            while (row > 0 && column < boardSize - 1)
            {
                row--;
                column++;
            }

            while (row < boardSize && column >= 0 && matches < pathLength)
            {
                if (cells[row, column] == playerId)
                {
                    matches++;
                }
                else
                {
                    matches = 0;
                }
                row++;
                column--;
            }
            return matches == pathLength;
        }
    }
}