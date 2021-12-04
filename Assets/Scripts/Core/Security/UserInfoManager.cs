using System;

namespace GameHub.Core.Security
{
    /// <summary>
    /// Class <c>UserInfoManager</c> provides the ability to load and save
    /// <c>UserInfo</c> that should persist across game sessions. For example,
    /// the user's first name, last name, username, id, and initials.
    /// </summary>
    /// <example>
    /// UserInfo userInfo = UserInfoManager.Instance.LoadUserInfo();
    /// settings = UserInfoManager.Instance.GetUserInfo();
    /// UserInfoManager.Instance.SaveUserInfo(settings);
    /// </example>
    public class UserInfoManager
    {
        /// <summary>
        /// Instance variable <c>_cachedUserInfo</c> stores the cached <c>UserInfo</c> 
        /// instance. 
        /// </summary>
        private UserInfo _cachedUserInfo;

        /// <summary>
        /// Instance variable <c>_instance</c> stores the <c>UserInfoManager</c>
        /// singleton instance.
        /// </summary>
        private static readonly Lazy<UserInfoManager> _instance =
            new Lazy<UserInfoManager>(() => new UserInfoManager());

        /// <summary>
        /// Method <c>Instance</c> returns the <c>UserInfoManager</c> singleton 
        /// instance.
        /// </summary>
        public static UserInfoManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// Private constructor to ensure the <c>UserInfoManager</c> cannot 
        /// be instantiated externally.
        /// </summary>
        private UserInfoManager()
        {
        }

        /// <summary>
        /// Method <c>SaveUserInfo</c> is used to serialize the <c>UserInfo</c> and
        /// write the associated data to the underlying data store, which may 
        /// be the file system or a database.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> represents the <c>UserInfo</c> to save / write to the 
        /// underylying storage mechanism.
        /// </param>
        public void SaveUserInfo(UserInfo userInfo)
        {
            GameSettingsManager.SaveSettings("UserSettings", userInfo);

            _cachedUserInfo = userInfo;
        }

        /// <summary>
        /// Method <c>LoadUserInfo</c> is used to retrieve the <c>UserInfo</c> associated
        /// with the current user directly from the underyling data store, bypassing
        /// any cached instance. Typically you should call <c>GeUserInfo</c> to speed
        /// up retrieval of the cached <c>UserInfo</c>.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>UserInfo</c> associated with the current user.
        /// </returns>
        public UserInfo LoadUserInfo()
        {
            return GameSettingsManager.LoadSettings<UserInfo>("UserSettings");
        }

        /// <summary>
        /// Method <c>GetUserInfo</c> is used to retrieve a) current cached <c>UserInfo</c>
        /// instance, persisted <c>UserInfo</c> instance if available in the underlying
        /// data store, or a generic <c>UserInfo</c> placeholder. 
        /// </summary>
        /// <returns>
        /// Returns instance of <c>UserInfo</c> associated with the current user.
        /// </returns>
        public UserInfo GetUserInfo()
        {
            if (_cachedUserInfo != null)
            {
                return _cachedUserInfo;
            }

            UserInfo storedSettings = LoadUserInfo();

            if (storedSettings == null)
            {
                _cachedUserInfo = new UserInfo("1", "JDoe", "John", "Doe");
            }
            else
            {
                _cachedUserInfo = storedSettings;
            }
            return _cachedUserInfo;
        }
    }
}