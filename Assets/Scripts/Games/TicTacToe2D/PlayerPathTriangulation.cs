using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D
{
    public class PlayerPathTriangulation
    {
        public List<GameBoardCell> FindPath( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
        {
            List<GameBoardCell> results;

            if ((results = checkRow(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            if ((results = checkColumn(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            if ((results = checkDiagonalRight(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            if ((results = checkDiagonalLeft(boardCells, playerMove, pathLength)).Count == pathLength)
            {
                return results;
            }
            return null;
        }

        private List<GameBoardCell> checkRow( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
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

        private List<GameBoardCell> checkColumn( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
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

        private List<GameBoardCell> checkDiagonalRight( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
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

        private List<GameBoardCell> checkDiagonalLeft( GameBoardCell[,] boardCells, PlayerMove playerMove, int pathLength )
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