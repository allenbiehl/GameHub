namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    public interface IComputerStrategy
    {
        PlayerMove CalculateMove( GameSeries gameSeries, GameState gameState );
    }
}