using GameHub.Core;

namespace GameHub.Games.TicTacToe2D
{
    public class PlayerSettingsManager
    {
        private static PlayerSettings _cachedSettings;

        public static void SaveSettings(PlayerSettings settings)
        {
            if (settings.BoardSize < 3)
            {
                return;
            }
            if (settings.LengthToWin < 3)
            {
                return;
            }

            GameSettingsManager.SaveSettings("TicTacToe2D", settings);

            _cachedSettings = settings;
        }

        public static PlayerSettings LoadSettings()
        {
            return GameSettingsManager.LoadSettings<PlayerSettings>("TicTacToe2D");
        }

        public static PlayerSettings GetSettings()
        {
            if (_cachedSettings != null)
            {
                return _cachedSettings;  
            }

            PlayerSettings storedSettings = LoadSettings();

            if (storedSettings == null)
            {
                _cachedSettings = PlayerSettings.Alpha;
            }
            else { 
                _cachedSettings = storedSettings;
            }
            return _cachedSettings;
        }
    }
}
