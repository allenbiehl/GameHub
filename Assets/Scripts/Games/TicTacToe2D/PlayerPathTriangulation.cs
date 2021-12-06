using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>PlayerPathTriangulation</c> is used to triangulate a winning move based 
    /// on the current player move as the vertext. Rather than scanning the entire game 
    /// board, this class checks a winning condition based on the major 4 win axis. Row,
    /// column, diaganol right and diaganol left.
    /// </summary>
    public class PlayerPathTriangulation
    {
        /// <summary>
        /// Method <c>FindPath</c> is used to determine whether the last move made by a
        /// player was a winning move based on the vertex, claimed cells and the required
        /// path win length. 
        /// </summary>
        /// <param name="boardCells">
        /// <c>boardCells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning move, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// If a winning move is determined, then we return the list of winning cells that 
        /// constitude the win, otherwise null is returned.
        /// </returns>
        public List<GameBoardCell> FindPath( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
        {
            List<GameBoardCell> results;

            if ((results = CheckRow(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            if ((results = CheckColumn(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            if ((results = CheckDiagonalRight(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            if ((results = CheckDiagonalLeft(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            return null;
        }

        /// <summary>
        /// Method <c>CheckRow</c> is used to check a win based on the row axis.
        /// </summary>
        /// <param name="boardCells">
        /// <c>boardCells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning moved, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Total number of sequential cells claimed by the player.
        /// </returns>
        private List<GameBoardCell> CheckRow( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
        {
            List<GameBoardCell> matches = new List<GameBoardCell>();
            int boardSize = boardCells.GetLength(0);
            int column = 0;

            while (column < boardSize && matches.Count < pathLength)
            {
                GameBoardCell boardCell = boardCells[playerMove.Row, column];

                if (boardCell.Claim == playerMove.Player)
                {
                    matches.Add(boardCell);
                }
                else
                {
                    matches.Clear();
                }
                column++;
            }
            return matches;
        }

        /// <summary>
        /// Method <c>CheckRow</c> is used to check a win based on the column axis.
        /// </summary>
        /// <param name="boardCells">
        /// <c>boardCells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning moved, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Total number of sequential cells claimed by the player.
        /// </returns>
        private List<GameBoardCell> CheckColumn( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
        {
            List<GameBoardCell> matches = new List<GameBoardCell>();
            int boardSize = boardCells.GetLength(0);
            int row = 0;

            while (row < boardSize && matches.Count < pathLength)
            {
                GameBoardCell boardCell = boardCells[row, playerMove.Column];

                if (boardCell.Claim == playerMove.Player)
                {
                    matches.Add(boardCell);
                }
                else
                {
                    matches.Clear();
                }
                row++;
            }
            return matches;
        }

        /// <summary>
        /// Method <c>CheckRow</c> is used to check a win based on the diagonal right axis.
        /// </summary>
        /// <param name="boardCells">
        /// <c>boardCells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning moved, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Total number of sequential cells claimed by the player.
        /// </returns>
        private List<GameBoardCell> CheckDiagonalRight( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
        {
            List<GameBoardCell> matches = new List<GameBoardCell>();
            int boardSize = boardCells.GetLength(0);

            // TODO Find better way to find starting cell
            int row = playerMove.Row;
            int column = playerMove.Column;

            while (row > 0 && column > 0)
            {
                row--;
                column--;
            }

            while (row < boardSize && column < boardSize && matches.Count < pathLength)
            {
                GameBoardCell boardCell = boardCells[row, column];

                if (boardCell.Claim == playerMove.Player)
                {
                    matches.Add(boardCell);
                }
                else
                {
                    matches.Clear();
                }
                row++;
                column++;
            }
            return matches;
        }

        /// <summary>
        /// Method <c>CheckRow</c> is used to check a win based on the diagonal left axis.
        /// </summary>
        /// <param name="boardCells">
        /// <c>boardCells</c> is the virtual board representation that includes all claimed
        /// and unclaimed cells which are required to determine a winning condition.
        /// </param>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the last move made by a player that is used as the origin
        /// or vertext to determine a winning moved, checking all four win axis.
        /// </param>
        /// <param name="pathLength">
        /// <c>pathLength</c> is the total sequential number of claimed cells required to win.
        /// </param>
        /// <returns>
        /// Total number of sequential cells claimed by the player.
        /// </returns>
        private List<GameBoardCell> CheckDiagonalLeft( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
        {
            List<GameBoardCell> matches = new List<GameBoardCell>();
            int boardSize = boardCells.GetLength(0);

            // TODO Find better way to find starting cell
            int row = playerMove.Row;
            int column = playerMove.Column;

            while (row > 0 && column < boardSize - 1)
            {
                row--;
                column++;
            }

            while (row < boardSize && column >= 0 && matches.Count < pathLength)
            {
                GameBoardCell boardCell = boardCells[row, column];

                if (boardCell.Claim == playerMove.Player)
                {
                    matches.Add(boardCell);
                }
                else
                {
                    matches.Clear();
                }
                row++;
                column--;
            }
            return matches;
        }
    }
}