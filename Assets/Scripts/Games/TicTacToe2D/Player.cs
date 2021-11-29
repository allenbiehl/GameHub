using UnityEngine;
using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D
{
    public abstract class Player
    {
        public UserInfo UserInfo { get; private set; }

        public PlayerSettings Settings { get; private set; }

        public Player( UserInfo userInfo, PlayerSettings settings )
        {
            UserInfo = userInfo;
            Settings = settings;
        }
    }
}
