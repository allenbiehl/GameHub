using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    public class VertexComputerStrategy : IComputerStrategy
    {
        private WinPathTriangulation _winTriangulation = new WinPathTriangulation();

        private int total;

        PlayerMove IComputerStrategy.CalculateMove(GameSeries gameSeries, GameState gameState)
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

        private PlayerMove GetInitialMove(GameSeries gameSeries, GameState gameState)
        {
            List<GameBoardCell> availableCells = gameState.GameBoard.AvailableCells;
            GameBoardCell cell = availableCells[new System.Random().Next(0, availableCells.Count - 1)];
            return new PlayerMove(gameSeries.Player2, cell.Row, cell.Column);
        }

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

        private class ScoreCard
        {
            public List<ScoreCardDirection> Directions { get; set; }

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

            public ScoreCard( int[] leftOffset, int[] rightOffset )
            {
                List<ScoreCardDirection> directions = new List<ScoreCardDirection>();
                Directions = new List<ScoreCardDirection>();
                Directions.Add(new ScoreCardDirection(leftOffset));
                Directions.Add(new ScoreCardDirection(rightOffset));
            }
        }

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
