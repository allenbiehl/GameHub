using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.AI.Strategy;

namespace GameHub.Games.TicTacToe2D.AI
{
    /// <summary>
    /// Class <c>BeginnerComputerPlayer</c> represents an AI player that uses
    /// the most basic strategy and typically loses as all moves are random.
    /// </summary>
    public class BeginnerComputerPlayer : StrategyPlayer, IComputerPlayer
    {

        /// <summary>
        /// Constructor for the <c>BeginnerComputerPlayer</c>.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> is the user information associated with the player.
        /// </param>
        /// <param name="settings">
        /// <c>settings</c> is the player game settings associated with the player.
        /// </param>
        public BeginnerComputerPlayer(
            UserInfo userinfo,
            PlayerSettings settings
        ) : base(userinfo, settings, new RandomComputerStrategy())
        {
        }

        /// <summary>
        /// Default <c>BeginnerComputerPlayer</c> implementation.
        /// </summary>
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