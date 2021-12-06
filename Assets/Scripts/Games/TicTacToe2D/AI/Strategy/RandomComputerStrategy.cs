using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Class <c>RandomComputerStrategy</c> represents a random selection based on 
    /// the available cells. If you want a somewhat challenging game, don't use this
    /// strategy as its almost guaranteed to lose, especially on larger than 3 x 3 boards.
    /// This is a good strategy for basic testing and game functionality.
    /// </summary>
    public class RandomComputerStrategy : IComputerStrategy
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
        public PlayerMove CalculateMove( GameSeries gameSeries, GameState gameState )
        {
            List<GameBoardCell> availableCells = gameState.GameBoard.AvailableCells;
            GameBoardCell cell = availableCells[new System.Random().Next(0, availableCells.Count - 1)];
            return new PlayerMove(gameSeries.Player2, cell.Row, cell.Column);
        }
    }
}