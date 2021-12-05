namespace GameHub.Core.Security
{
    /// <summary>
    /// Interface <c>IUserInfoService</c> represents the base interface 
    /// for all <c>UserInfoService</c> implementations.
    /// </summary>
    public interface IUserInfoService
    {
        /// <summary>
        /// Method <c>SaveUserInfo</c> is used to serialize the <c>UserInfo</c> and
        /// write the associated data to the underlying data store, which may 
        /// be the file system or a database.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> represents the <c>UserInfo</c> to save / write to the 
        /// underylying storage mechanism.
        /// </param>
        public void SaveUserInfo(UserInfo userInfo);

        /// <summary>
        /// Method <c>LoadUserInfo</c> is used to retrieve the <c>UserInfo</c> associated
        /// with the current user directly from the underyling data store, bypassing
        /// any cached instance.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>UserInfo</c> associated with the current user.
        /// </returns>
        UserInfo GetUserInfo();
    }
}
