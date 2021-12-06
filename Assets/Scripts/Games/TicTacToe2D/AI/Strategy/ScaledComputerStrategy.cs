using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Class <c>ScaledComputerStrategy</c> is an AI strategy that changes based on 
    /// the game board size. Due to the intensive nature of the <c>MiniMax</c> 
    /// computer strategy, we can only use it on 3 x 3 boards. However the scaled 
    /// strategy is designed to register different strategies based on the specified
    /// board size. There is not limit to the number of strategies you can use.
    /// In additiona, a strategy can also delegate to other strategies internally,
    /// howeve the <c>ScaledComputerStrategy</c> is specifically geared towards 
    /// choosing the best strategy based on the board size.
    /// </summary>
    public class ScaledComputerStrategy : IComputerStrategy
    {
        /// <summary>
        /// Instance variable <c>_scaledStrategies</c> that stores the game board
        /// size as a key. If the board size is undefined, then we use the default
        /// <c>VertextComputerStrategy</c>.
        /// </summary>
        private Dictionary<int, IComputerStrategy> _scaledStrategies;

        /// <summary>
        /// Constructor for the <c>ScaledComputerStrategy</c>.
        /// </summary>
        public ScaledComputerStrategy()
        {
            _scaledStrategies = new Dictionary<int, IComputerStrategy>();
            _scaledStrategies.Add(0, new VertexComputerStrategy());
            _scaledStrategies.Add(3, new MinimaxComputerStrategy());
        }

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
            int boardSize = gameState.GameBoard.Size;
            IComputerStrategy strategy =
                _scaledStrategies.ContainsKey(boardSize)
                ? _scaledStrategies[boardSize]
                : _scaledStrategies[0];
            return strategy.CalculateMove(gameSeries, gameState);
        }
    }
}
