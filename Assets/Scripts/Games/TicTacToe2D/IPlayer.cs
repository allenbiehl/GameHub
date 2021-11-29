using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D
{
    public interface IPlayer
    {
        UserInfo UserInfo { get; }

        PlayerSettings Settings { get; }

        void Play( GameManager gameManager, GameSeries gameSeries, GameState gameState );
    }
}