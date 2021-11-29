using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.AI.Strategy;

namespace GameHub.Games.TicTacToe2D.AI
{
    public class ExpertComputerPlayer : StrategyPlayer, IComputerPlayer
    {
        public ExpertComputerPlayer( 
            UserInfo userinfo,
            PlayerSettings settings 
        ) : base( userinfo, settings, new ScaledComputerStrategy() )
        {
        }

        public static IPlayer Default 
        {
            get
            {
                return new ExpertComputerPlayer(
                    new UserInfo("0", "AI", "Artificial", "Intelligence"),
                    PlayerSettings.Omega
                );
            }
        }
    }
}