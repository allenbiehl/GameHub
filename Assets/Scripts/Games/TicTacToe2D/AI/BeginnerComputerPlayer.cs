using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.AI.Strategy;

namespace GameHub.Games.TicTacToe2D.AI
{
    public class BeginnerComputerPlayer : StrategyPlayer, IComputerPlayer
    {
        public BeginnerComputerPlayer(
            UserInfo userinfo,
            PlayerSettings settings
        ) : base(userinfo, settings, new RandomComputerStrategy())
        {
        }

        public static IPlayer Default
        {
            get
            {
                return new BeginnerComputerPlayer(
                    new UserInfo("0", "AI", "Artificial", "Intelligence"),
                    PlayerSettings.Omega
                );
            }
        }
    }
}