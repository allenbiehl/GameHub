using UnityEngine;
using GameHub.Core;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>PlayerSettings</c> stores all TicTacToe2D game settings, which includes
    /// color, current opponent, board size, and length to win.
    /// </summary>
    [System.Serializable]
    public class PlayerSettings
    {
        /// <summary>
        /// Instance variable <c>_color</c> stores the player's color code. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private Color _color = ColorCode.Blue;

        /// <summary>
        /// Instance variable <c>_opponent</c> stores the player's last opponent. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private string _opponent = GameHub.Games.TicTacToe2D.Opponent.AIExpert;

        /// <summary>
        /// Instance variable <c>_boardSize</c> stores the selected board size. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private int _boardSize = 3;

        /// <summary>
        /// Instance variable <c>_lengthToWin</c> stores the total number of sequential 
        /// claimed spaces by a single player needed to win. Default for tic-tac-toe is 3;
        /// We store the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private int _lengthToWin = 3;

        /// <summary>
        /// Instance property <c>Color</c> stores the players's color code.
        /// </summary>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// Instance property <c>LastName</c> stores the player's last opponent.
        /// </summary>
        public string Opponent
        {
            get
            {
                return _opponent;
            }
            set
            {
                _opponent = value;
            }
        }

        /// <summary>
        /// Instance property <c>LastName</c> stores the player's selected board size.
        /// </summary>
        public int BoardSize
        {
            get
            {
                return _boardSize;
            }
            set
            {
                _boardSize = value;
            }
        }

        /// <summary>
        /// Instance property <c>LastName</c> stores the total number of sequential 
        /// claimed spaces by a single player needed to win.
        /// </summary>
        public int LengthToWin
        {
            get
            {
                return _lengthToWin;
            }
            set
            {
                _lengthToWin = value;
            }
        }

        /// <summary>
        /// <c>Alpha</c> represents the default primary player settings. i.e. Player 1.
        /// </summary>
        public static PlayerSettings Alpha
        {
            get
            {
                PlayerSettings settings = new PlayerSettings();
                settings.Color = ColorCode.Blue;
                return settings;
            }
        }

        /// <summary>
        /// <c>Omegaha</c> represents the default opponent player settings. i.e. Player 2.
        /// </summary>
        public static PlayerSettings Omega
        {
            get
            {
                PlayerSettings settings = new PlayerSettings();
                settings.Color = ColorCode.Orange;
                return settings;
            }
        }

        /// <summary>
        /// Empty constructor required for serialization.
        /// </summary>
        public PlayerSettings()
        {
        }
    }
}
