using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    public class StrategyPlayer : Player, IPlayer
    {
        protected IComputerStrategy Strategy { get; set; }

        public StrategyPlayer(
            UserInfo userinfo,
            PlayerSettings settings,
            IComputerStrategy strategy = null
        ) : base(userinfo, settings)
        {
            Strategy = strategy;
        }

        void IPlayer.Play(GameManager gameManager, GameSeries gameSeries, GameState gameState)
        {
            if (Strategy != null)
            {
                //var watch = new System.Diagnostics.Stopwatch();
                //watch.Start();

                PlayerMove move = this.Strategy.CalculateMove(gameSeries, gameState);

                //watch.Stop();
                //Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");

                gameManager.MakePlay(move);
            }
        }
    }

}
