using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Class <c>VertextComputerStrategy</c> is an experimental strategy design to 
    /// address the scale limitations of the <c>Minimax</c> strategy.
    /// </summary>
    public class VertexComputerStrategy : IComputerStrategy
    {
        /// <summary>
        /// Instance variable <c>_winTriangulation</c> is used to triangulate a win 
        /// based on a board cell.
        /// </summary>
        private WinPathTriangulation _winTriangulation = new WinPathTriangulation();

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
            if (gameState.PlayerMoves.Count == 0)
            {
                return GetInitialMove(gameSeries, gameState);
            }
            else
            {
                return GetVertexMove(gameSeries, gameState);
            }
        }

        /// <summary>
        /// Method <c>GetInitialMove</c> is used when the AI player is the offensive player
        /// and they need to make the first game board move.
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
        private PlayerMove GetInitialMove(GameSeries gameSeries, GameState gameState)
        {
            List<GameBoardCell> availableCells = gameState.GameBoard.AvailableCells;
            GameBoardCell cell = availableCells[new System.Random().Next(0, availableCells.Count - 1)];
            return new PlayerMove(gameSeries.Player2, cell.Row, cell.Column);
        }

        /// <summary>
        /// Method <c>GetVertexMove</c> is used on subsequent moves as this algorithm is 
        /// based on the players last move and is defensive in nature.
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
        private PlayerMove GetVertexMove(GameSeries gameSeries, GameState gameState)
        {
            PlayerMove player1Move = gameState.PlayerMoves.Last();
            GameBoardCell[,] sourceCells = gameState.GameBoard.Cells;
            int boardSize = sourceCells.GetLength(0);
            int[,] cells = new int[boardSize, boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    GameBoardCell sourceCell = sourceCells[row, column];

                    if (sourceCell.Claim == gameSeries.Player1)
                    {
                        cells[row, column] = 1;
                    }
                    else if (sourceCell.Claim == gameSeries.Player2)
                    {
                        cells[row, column] = 2;
                    }
                    else
                    {
                        cells[row, column] = 0;
                    }
                }
            }

            int[] move = GetBestMove(cells, new int[]{player1Move.Row, player1Move.Column}, 1);

            return new PlayerMove(gameSeries.Player2, move[0], move[1]);
        }

        /// <summary>
        /// Method <c>GetBestMove</c> is used to get the best available move based on the 
        /// player vertex. This algorith checks all four axis and determines the weight 
        /// of each axis. 
        /// </summary>
        /// <param name="cells">
        /// <c>cells</c> is a two dimensional array of all game board cells.
        /// </param>
        /// <param name="vertex">
        /// <c>vertext</c> is the coordinates of the last play made by the specified player.
        /// </param>
        /// <param name="player">
        /// <c>player</c> is the player to target against.
        /// </param>
        /// <returns>
        /// Coordinates of the best game board cell move available.
        /// </returns>
        private int[] GetBestMove( int[,] cells, int[] vertex, int player )
        {
            int boardSize = cells.GetLength(0);

            ScoreCard[] scorecards = new ScoreCard[] {
                new ScoreCard(OffsetDirection.East, OffsetDirection.West),
                new ScoreCard(OffsetDirection.North, OffsetDirection.South),
                new ScoreCard(OffsetDirection.NorthWest, OffsetDirection.SouthEast),
                new ScoreCard(OffsetDirection.SouthWest, OffsetDirection.NorthEast)
            };

            foreach(ScoreCard scorecard in scorecards)
            {
                foreach (ScoreCardDirection direction in scorecard.Directions)
                {
                    int distance = 1;
                    int[] offset = direction.Offset;
                    int row = vertex[0] + (offset[0] * distance);
                    int column = vertex[1] + (offset[1] * distance);
                    bool capture = true;

                    while (row >= 0 && row < boardSize && column >= 0 && column < boardSize && capture)
                    {
                        int value = cells[row, column];

                        if (value == player)
                        {
                            direction.Value += 10;
                        }
                        else if (value == 0)
                        {
                            direction.Value += 5;
                            direction.FirstAvailableCell = new int[] { row, column };
                            capture = false;
                        }
                        else
                        {
                            capture = false;
                        }
                        distance++;
                        row = vertex[0] + (offset[0] * distance);
                        column = vertex[1] + (offset[1] * distance);
                    }
                }
            }

            int[] bestMove = null;
            float bestValue = -1;

            foreach (ScoreCard scorecard in scorecards)
            {
                int[] newCell = scorecard.BestAvailableCell;
                float newValue = scorecard.Value;

                if (newValue > bestValue && newCell != null)
                {
                    bestValue = newValue;
                    bestMove = newCell;
                }
            }

            // Couldn't figure out the next position based on the vertex algorith
            // Find a random available spot
            if (bestMove == null)
            {
                List<int[]> availableCells = AvailableCells(cells);
                int[] cell = availableCells[new System.Random().Next(0, availableCells.Count - 1)];
                bestMove = new int[] { cell[0], cell[1] };
            }

            return bestMove;
        }

        /// <summary>
        /// Method <c>AvailableCells</c> is used to filter all available cells from the game board.
        /// </summary>
        /// <param name="board">
        /// <c>board</c> is the 2 dimensional array of game board cell coordinates.
        /// </param>
        /// <returns>
        /// List of available game board cells.
        /// </returns>
        private List<int[]> AvailableCells(int[,] board)
        {
            List<int[]> available = new List<int[]>();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(0); col++)
                {
                    if (board[row, col] == 0)
                    {
                        available.Add(new int[] { row, col });
                    }
                }
            }
            return available;
        }

        /// <summary>
        /// Class <c>ScoreCard</c> represents the score card associated with a specific axis, 
        /// whether row, column, diagonal right and diagonal left. Each axis is comprised of
        /// left and right parts. i.e. row = east and west.
        /// </summary>
        private class ScoreCard
        {
            /// <summary>
            /// Property <c>Direction</c> is a collection of directions that comprise the axis.
            /// </summary>
            public List<ScoreCardDirection> Directions { get; set; }

            /// <summary>
            /// Property <c>Value</c> represents the combined value of both directions.
            /// </summary>
            public float Value 
            { 
                get
                {
                    float value = 0;
                    foreach(ScoreCardDirection direction in Directions)
                    {
                        value += direction.Value;
                    }
                    return value;
                }
            }

            /// <summary>
            /// Property <c>BestAvailableCell</c> is the best calculated as compared to each 
            /// direction.
            /// </summary>
            public int[] BestAvailableCell
            {
                get
                {
                    int[] bestCell = null;
                    float bestScore = -1;

                    foreach (ScoreCardDirection direction in Directions)
                    {
                        if (direction.FirstAvailableCell != null && direction.Value >= bestScore)
                        {
                            bestCell = direction.FirstAvailableCell;
                            bestScore = direction.Value;
                        }
                    }
                    return bestCell;
                }
            }

            /// <summary>
            /// Constructor for the <c>ScoreCard</c>.
            /// </summary>
            /// <param name="leftOffset">
            /// <c>leftOffset</c> is the left axis direction. i.e. west.
            /// </param>
            /// <param name="rightOffset">
            /// <c>rightOffset</c> is the right axis direction. i.e. east.
            /// </param>
            public ScoreCard( int[] leftOffset, int[] rightOffset )
            {
                List<ScoreCardDirection> directions = new List<ScoreCardDirection>();
                Directions = new List<ScoreCardDirection>();
                Directions.Add(new ScoreCardDirection(leftOffset));
                Directions.Add(new ScoreCardDirection(rightOffset));
            }
        }

        /// <summary>
        /// Class <c>ScoreCardDirection</c> represents the individual score and cells 
        /// associated with a specific direction. Each direction is used to determine 
        /// the most likely attack axis and then choose the most likely attack direction 
        /// based on the vertex.
        /// </summary>
        private class ScoreCardDirection
        {
            public int[] Offset { get; set; }

            public float Value { get; set; } = 0;

            public List<int[]> Cells { get; }

            public int[] FirstAvailableCell { get; set; }   

            public ScoreCardDirection( int[] offset )
            {
                Cells = new List<int[]>();
                Offset = offset;
            }
        }

        /// <summary>
        /// Class <c>OffsetDirection</c> represents the offset coordinates that represent 
        /// a direction from the vertex.
        /// </summary>
        public struct OffsetDirection
        {
            public static int[] North = new int[] { -1, 0 };

            public static int[] NorthEast = new int[] { -1, 1 };

            public static int[] East = new int[] { 0, 1 };

            public static int[] SouthEast = new int[] { 1, 1 };

            public static int[] South = new int[] { 1, 0 };

            public static int[] SouthWest = new int[] { 1, -1 };
             
            public static int[] West = new int[] { 0, -1 };

            public static int[] NorthWest = new int[] { -1, -1 };
        }
    }
}
