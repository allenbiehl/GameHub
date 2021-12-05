namespace GameHub.Core
{
    /// <summary>
    /// Interface <c>IGameSettingsService</c> represents the base interface 
    /// for all <c>GameSettingsService</c> implementations.
    /// </summary>
    public interface IGameSettingsService
    {
        /// <summary>
        /// Method <c>SaveSettings</c> is used to persist settings associated
        /// with a distinc name (key) to the underlying data store.
        /// </summary>
        /// <param name="name">
        /// <c>name</c> is the unique name of the game setting.
        /// </param>
        /// <param name="settings">
        /// <c>settings</c> is the object that will be persisted.
        /// </param>
        void SaveSettings(string name, object settings);

        /// <summary>
        /// Method <c>GetSettings</c> is used to retrieve persisted settings 
        /// from the underlying data store using the distinct setting name.
        /// </summary>
        /// <typeparam name="T">
        /// <c>T</c> is the object type that will be deserialized.
        /// </typeparam>
        /// <param name="name">
        /// <c>name</c> is the unique name of the game setting to retrieve.
        /// </param>
        /// <returns>
        /// Deserialized game setting object.
        /// </returns>
        T GetSettings<T>(string name);
    }
}
