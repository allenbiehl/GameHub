using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.AI.Strategy;

namespace GameHub.Games.TicTacToe2D.AI
{
    /// <summary>
    /// Class <c>ExpertCommputerPlayer</c> represents an AI player that switches 
    /// strategies based on the game board size and potentially length to win. 
    /// This makes the game more excited as the human player can either tie 
    /// or lose when using the Minimax strategy, however the human player can
    /// win using an alternate strategy which the expert player employs.
    /// </summary>
    public class ExpertComputerPlayer : StrategyPlayer, IComputerPlayer
    {

        /// <summary>
        /// Constructor for the <c>ExpertComputerPlayer</c>.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> is the user information associated with the player.
        /// </param>
        /// <param name="settings">
        /// <c>settings</c> is the player game settings associated with the player.
        /// </param>
        public ExpertComputerPlayer( 
            UserInfo userinfo,
            PlayerSettings settings 
        ) : base( userinfo, settings, new ScaledComputerStrategy() )
        {
        }

        /// <summary>
        /// Default <c>ExpertComputerPlayer</c> implementation.
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