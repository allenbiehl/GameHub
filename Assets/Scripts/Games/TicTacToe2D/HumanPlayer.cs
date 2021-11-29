using UnityEngine;
using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D
{
    public class HumanPlayer : Player, IPlayer
    {
        public HumanPlayer( UserInfo userinfo, PlayerSettings settings ) : base(userinfo, settings)
        {
        }

        void IPlayer.Play( GameManager gameManager, GameSeries gameSeries, GameState game )
        {
        }
    }
}
