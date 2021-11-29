using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    public class RandomComputerStrategy : IComputerStrategy
    {
        PlayerMove IComputerStrategy.CalculateMove( GameSeries gameSeries, GameState gameState )
        {
            List<GameBoardCell> availableCells = gameState.GameBoard.AvailableCells;
            GameBoardCell cell = availableCells[new System.Random().Next(0, availableCells.Count - 1)];
            return new PlayerMove(gameSeries.Player2, cell.Row, cell.Column);
        }
    }
}