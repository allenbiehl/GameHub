using System;
using System.IO;
using GameHub.Core;
using UnityEngine;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>PlayerSettingsManager</c> provides the ability to load and save
    /// <c>PlayerSettings</c> that should persist across game sessions. For example,
    /// the starting game board size used in the previous game session.
    /// </summary>
    /// <example>
    /// PlayerSettings settings = PlayerSettingsManager.Instance.LoadSettings();
    /// settings = PlayerSettingsManager.Instance.GetSettings();
    /// PlayerSettingsManager.Instance.SaveSettings(settings);
    /// </example>
    public class PlayerSettingsManager
    {
        /// <summary>
        /// Instance variable <c>_cachedSettings</c> stores the cached <c>PlayerSettings</c> 
        /// instance for faster retrieval than pulling from the underlying data store 
        /// implementation. 
        private PlayerSettings _cachedSettings;

        /// <summary>
        /// Instance variable <c>_instance</c> stores the <c>PlayerSettingsManager</c>
        /// singleton instance.
        /// </summary>
        private static readonly Lazy<PlayerSettingsManager> _instance = 
            new Lazy<PlayerSettingsManager>(() => new PlayerSettingsManager());

        /// <summary>
        /// Method <c>Instance</c> returns the <c>PlayerSettingsManager</c> singleton 
        /// instance.
        /// </summary>
        public static PlayerSettingsManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// Private constructor to ensure the <c>PlayerSettingsManager</c> cannot 
        /// be instantiated externally.
        /// </summary>
        private PlayerSettingsManager()
        {
        }

        /// <summary>
        /// Method <c>SaveSettings</c> is used to serialize the <c>PlayerSettings</c> and
        /// write the associated data to the underlying data store.
        /// </summary>
        /// <param name="userInfo">
        /// <c>settings</c> represents the <c>PlayerSettings</c> to save / write to the 
        /// underylying data store.
        /// </param>
        public void SaveSettings(PlayerSettings settings)
        {
            if (settings.BoardSize < 3)
            {
                return;
            }
            if (settings.LengthToWin < 3)
            {
                return;
            }

            GameSettingsManager.Instance.SaveSettings("TicTacToe2D", settings);

            _cachedSettings = settings;
        }

        /// <summary>
        /// Method <c>LoadSettings</c> is used to retrieve the <c>PlayerSettings</c> associated
        /// with the current user directly from the underyling data store, bypassing
        /// any cached instance. Typically you should call <c>GetSettings</c> to speed
        /// up retrieval of the cached <c>PlayerSettings</c>.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>PlayerSettings</c> associated with the current user.
        /// </returns>
        public PlayerSettings LoadSettings()
        {
            return GameSettingsManager.Instance.LoadSettings<PlayerSettings>("TicTacToe2D");
        }

        /// <summary>
        /// Method <c>GetSettings</c> is used to retrieve a) current cached <c>PlayerSettings</c>
        /// instance, persisted <c>PlayerSettings</c> instance if available in the underlying
        /// data store, or a generic <c>PlayerSettings</c> placeholder. 
        /// </summary>
        /// <returns></returns>
        public PlayerSettings GetSettings()
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