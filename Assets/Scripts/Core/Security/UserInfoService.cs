using Zenject;

namespace GameHub.Core.Security
{
    /// <summary>
    /// Class <c>UserInfoService</c> provides the ability to load and save
    /// <c>UserInfo</c> that should persist across game sessions. For example,
    /// the user's first name, last name, username, id, and initials.
    /// </summary>
    public class UserInfoService : IUserInfoService
    {
        /// <summary>
        /// Instance variable <c>_gameSettingsService</c> is used to retrieve and 
        /// persist game settings.
        /// </summary>
        private IGameSettingsService _gameSettingsService;

        /// <summary>
        /// Constructor for <c>UserInfoService</c>
        /// </summary>
        /// <param name="gameSettingsService">
        /// <c>gameSettingsService</c> is used to retrieve and persist game settings
        /// and an underlying data store.
        /// </param>
        [Inject]
        public UserInfoService(IGameSettingsService gameSettingsService)
        {
            _gameSettingsService = gameSettingsService;
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
            _gameSettingsService.SaveSettings("UserSettings", userInfo);
        }

        /// <summary>
        /// Method <c>LoadUserInfo</c> is used to retrieve the <c>UserInfo</c> associated
        /// with the current user directly from the underyling data store, bypassing
        /// any cached instance.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>UserInfo</c> associated with the current user.
        /// </returns>
        public UserInfo GetUserInfo()
        {
            UserInfo userInfo = _gameSettingsService.GetSettings<UserInfo>("UserSettings");
            return (userInfo == null) ? new UserInfo("1", "jdoe", "John", "Doe") : userInfo;
        }
    }
}