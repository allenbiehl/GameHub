using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Class <c>MinimaxComputerStrategy</c> represents the unbeatable strategy used
    /// for 3 x 3 board sizes. Due to the nature of this algorithm which plays the entire
    /// game before making a move, the strategy does not scale well and an alternate 
    /// strategy should be used for board sizes greater than 3 x 3.
    /// </summary>
    public class MinimaxComputerStrategy : IComputerStrategy
    {
        /// <summary>
        /// Method <c>CalculateMove</c> is called by the AI player to make the best
        /// move based on the selected strategy.
        /// </summary>
        /// <param name="gameSeries">
        /// <c>gameSeries</c>  is the current series being played between both opponents.
        /// The <c>gameSeries</c> includes a history of game play and can be used by the
        /// AI to calculate moves based on predictable human strategies.
        /// </param>
        /// <param name="gameState">
        /// <c>gameState</c> is the current game being played.
        /// </param>
        /// <returns>
        /// Best <c>PlayerMove</c> made by the computer strategy implementation.
        /// </returns>
        public PlayerMove CalculateMove(GameSeries gameSeries, GameState gameState)
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

        /// <summary>
        /// Method <c>Minimax</c> is used to select the best move possible based 
        /// on the avialable game board cells.
        /// </summary>
        /// <param name="board">
        /// <c>board</c> is a one dimensional array of game cells.
        /// </param>
        /// <param name="playerId">
        /// <c>playerId</c> is the player who made the last move and we check 
        /// for a win.
        /// </param>
        /// <returns>
        /// <c>Move</c> is the best player move available.
        /// </returns>
        private Move Minimax(int[] board, int playerId)
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

        /// <summary>
        /// Method <c>Available</c> returns an array that contains 
        /// all available cells.
        /// </summary>
        /// <param name="board">
        /// <c>board</c> is the array of all game board cells.
        /// </param>
        /// <returns>
        /// Array of available game board cells.
        /// </returns>
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

        /// <summary>
        /// Method <c>Winning</c> calculates whether the player won the game based on 
        /// the winning strategies.
        /// </summary>
        /// <param name="board">
        /// <c>board</c> is the array of all game board cells.
        /// </param>
        /// <param name="player">
        /// <c>player</c> is the current player to evaluate for a win.
        /// </param>
        /// <returns>
        /// Boolean value whether the player won.
        /// </returns>
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

        /// <summary>
        /// Struct <c>Move</c> is used to store the move score and coordinate location.
        /// </summary>
        private struct Move
        {
            /// <summary>
            /// Instance variable <c>score</c> is the weighted value of the move that 
            /// is compared against other moves.
            /// </summary>
            public int score;

            /// <summary>
            /// Instance variable <c>index</c> is the 0 based coordinate of the game 
            /// board cell.
            /// </summary>
            public int index;
        }
    }
}