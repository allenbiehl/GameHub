namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    class WinPathTriangulation
    {
        public bool Winning(int[,] cells, int playerId, int[] move, int pathLength)
        {
            if (
                checkRow(cells, playerId, move, pathLength) ||
                checkColumn(cells, playerId, move, pathLength) ||
                checkDiagonalRight(cells, playerId, move, pathLength) ||
                checkDiagonalLeft(cells, playerId, move, pathLength))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkRow(int[,] cells, int playerId, int[] move, int pathLength)
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

        private bool checkColumn(int[,] cells, int playerId, int[] move, int pathLength)
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

        private bool checkDiagonalRight(int[,] cells, int playerId, int[] move, int pathLength)
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

        private bool checkDiagonalLeft(int[,] cells, int playerId, int[] move, int pathLength)
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