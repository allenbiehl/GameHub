namespace GameHub.Core.Security
{
    public class UserInfoManager
    {
        private static UserInfo _cachedUserInfo;


        public static void SaveUserInfo(UserInfo userInfo)
        {
            GameSettingsManager.SaveSettings("UserSettings", userInfo);

            _cachedUserInfo = userInfo;
        }

        public static UserInfo LoadUserInfo()
        {
            return GameSettingsManager.LoadSettings<UserInfo>("UserSettings");
        }

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
