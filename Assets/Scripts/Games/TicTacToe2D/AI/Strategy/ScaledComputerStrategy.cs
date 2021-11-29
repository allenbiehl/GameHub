using System.Collections.Generic;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    public class ScaledComputerStrategy : IComputerStrategy
    {
        Dictionary<int, IComputerStrategy> _scaledStrategies;

        public ScaledComputerStrategy()
        {
            _scaledStrategies = new Dictionary<int, IComputerStrategy>();
            _scaledStrategies.Add(0, new VertexComputerStrategy());
            _scaledStrategies.Add(3, new MinimaxComputerStrategy());
        }

        PlayerMove IComputerStrategy.CalculateMove(GameSeries gameSeries, GameState gameState)
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
