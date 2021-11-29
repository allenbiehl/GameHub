using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    public class MinimaxComputerStrategy : IComputerStrategy
    {
        PlayerMove IComputerStrategy.CalculateMove(GameSeries gameSeries, GameState gameState)
        {
            GameBoardCell[,] sourceCells = gameState.GameBoard.Cells;
            int boardSize = gameState.GameBoard.Size;

            int[] cells = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    GameBoardCell sourceCell = sourceCells[row, column];

                    if (sourceCell.Claim == gameSeries.Player1)
                    {
                        cells[(row * boardSize) + column] = 1;
                    }
                    else if (sourceCell.Claim == gameSeries.Player2)
                    {
                        cells[(row * boardSize) + column] = 2;
                    }
                    else
                    {
                        cells[(row * boardSize) + column] = 0;
                    }
                }
            }

            Move move = Minimax(cells, 2);
            int moveRow = move.index / boardSize;
            int moveCol = move.index % boardSize;

            return new PlayerMove(gameSeries.Player2, moveRow, moveCol);
        }

        Move Minimax(int[] board, int playerId)
        {
            int[] available = Available(board);

            if (playerId == 2)
            {
                if (Winning(board, 1))
                {
                    Move move = new Move();
                    move.score = -10;
                    return move;
                }
            }
            else
            {
                if (Winning(board, 2))
                {
                    Move move = new Move();
                    move.score = 10;
                    return move;
                }
            }
            if (available.Length == 0)
            {
                Move move = new Move();
                move.score = 0;
                return move;
            }

            Move bestMove = new Move();

            if (playerId == 2)
            {
                var bestScore = -10000;

                for (var i = 0; i < available.Length; i++)
                {
                    int boardIdx = available[i];
                    Move move = new Move();
                    move.index = boardIdx;
                    board[boardIdx] = 2;
                    Move result = Minimax(board, 1);
                    move.score = result.score;
                    board[boardIdx] = 0;

                    if (result.score > bestScore)
                    {
                        bestScore = result.score;
                        bestMove = move;
                    }
                }
            }
            else
            {
                var bestScore = 10000;

                for (var i = 0; i < available.Length; i++)
                {
                    int boardIdx = available[i];
                    Move move = new Move();
                    move.index = boardIdx;
                    board[boardIdx] = 1;
                    Move result = Minimax(board, 2);
                    move.score = result.score;
                    board[boardIdx] = 0;

                    if (result.score < bestScore)
                    {
                        bestScore = move.score;
                        bestMove = move;
                    }
                }
            }
            return bestMove;
        }

        private int[] Available(int[] board)
        {
            List<int> available = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    available.Add(i);
                }
            }
            return available.ToArray();
        }

        private bool Winning(int[] board, int player)
        {
            if (
                (board[0] == player && board[1] == player && board[2] == player) ||
                (board[3] == player && board[4] == player && board[5] == player) ||
                (board[6] == player && board[7] == player && board[8] == player) ||
                (board[0] == player && board[3] == player && board[6] == player) ||
                (board[1] == player && board[4] == player && board[7] == player) ||
                (board[2] == player && board[5] == player && board[8] == player) ||
                (board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player)
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private struct Move
        {
            public int score;
            public int index;
        }
    }
}