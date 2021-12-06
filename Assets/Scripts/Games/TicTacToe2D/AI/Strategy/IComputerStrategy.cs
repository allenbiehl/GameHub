namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Interface <c>IComputerStrategy</c> represents the base strategy interface 
    /// for all strategies. A player can use one strategy or multiple strategies
    /// based on a number of factors.
    /// </summary>
    public interface IComputerStrategy
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
        PlayerMove CalculateMove( GameSeries gameSeries, GameState gameState );
    }
}