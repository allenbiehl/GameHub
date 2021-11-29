using UnityEngine;
using GameHub.Core;

namespace GameHub.Games.TicTacToe2D
{
    [System.Serializable]
    public class PlayerSettings
    {
        public static PlayerSettings Alpha
        {
            get
            {
                PlayerSettings settings = new PlayerSettings();
                settings.Color = ColorCode.Blue;
                return settings;
            }
        }

        public static PlayerSettings Omega
        {
            get
            {
                PlayerSettings settings = new PlayerSettings();
                settings.Color = ColorCode.Orange;
                return settings;
            }
        }

        public Color Color { get; set; } = ColorCode.Blue;

        public string Opponent { get; set; } = 
            GameHub.Games.TicTacToe2D.Opponent.AIExpert;

        public int BoardSize { get; set; } = 3;

        public int LengthToWin { get; set; } = 3;

        public PlayerSettings()
        {
        }
    }
}
