namespace GameHub.Core.Security
{
    /// <summary>
    /// Class <c>UserInfoManager</c> provides the ability to load <c>UserInfo</c> 
    /// associated with the current user as well as saving user information
    /// across gaming sessions.
    /// </summary>
    public class UserInfoManager
    {
        /// <summary>
        /// Instance variable <c>_cachedUserInfo</c> stores the cached <c>UserInfo</c> 
        /// instance. 
        /// </summary>
        private static UserInfo _cachedUserInfo;

        /// <summary>
        /// Method <c>SaveUserInfo</c> is used to serialize the <c>UserInfo</c> and
        /// write the associated data to the underlying storage mechanism, which may 
        /// be the file system or a database.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> represents the <c>UserInfo</c> to save / write to the 
        /// underylying storage mechanism.
        /// </param>
        public static void SaveUserInfo(UserInfo userInfo)
        {
            GameSettingsManager.SaveSettings("UserSettings", userInfo);

            _cachedUserInfo = userInfo;
        }

        /// <summary>
        /// Method <c>LoadUserInfo</c> is used to retrieve the <c>UserInfo</c> associated
        /// with the current user directly from the underyling storage mechanism, bypassing
        /// any cached reference. Typically you should call <c>GetUserInfo</c> to speed
        /// up retrieval of the cached <c>UserInfo</c>.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>UserInfo</c> associated with the current user.
        /// </returns>
        public static UserInfo LoadUserInfo()
        {
            return GameSettingsManager.LoadSettings<UserInfo>("UserSettings");
        }

        /// <summary>
        /// Method <c>GetUserInfo</c> is used to retrieve a) current cached <c>UserInfo</c>
        /// instance, persisted <c>UserInfo</c> instance if available in the underlying
        /// storage mechanism, or a generic <c>UserInfo</c> placeholder. 
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetUserInfo()
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
