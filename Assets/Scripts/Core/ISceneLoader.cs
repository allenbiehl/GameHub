namespace GameHub.Core
{
    /// <summary>
    /// Interface <c>ISceneLoader</c> represents the base interface 
    /// for all <c>SceneLoader</c> implementations.
    /// </summary>
    public interface ISceneLoader
    {
        /// <summary>
        /// Method <c>Load</c> is used to load standard scenes defined
        /// in the <c>Scene</c> enumeration.
        /// </summary>
        /// <param name="scene">
        /// <c>scene</c> represents the standard scene to load
        /// </param>
        /// <c>transition</c> represents whether the intermediary loading 
        /// scene should be loaded before the target scene.
        /// </param>
        public void Load(SceneType scene, bool transition = false);

        /// <summary>
        /// Method <c>Load</c> is used to load any scene by its name.
        /// </summary>
        /// <param name="scene">
        /// <c>scene</c> represents the scene name to load.
        /// </param>
        /// <param name="transition">
        /// <c>transition</c> represents whether the intermediary loading 
        /// scene should be loaded before the target scene.
        /// </param>
        void Load(string scene, bool transition = false);

        /// <summary>
        /// Method <c>LoadMainMenu</c> is shortcut for loading the main menu.
        /// This is equivalent to <c>SceneLoader.Load(SceneLoader.Scene.MainMenu)</c>.
        /// </summary>
        /// <param name="scene">
        /// <c>scene</c> represents the standard scene to load
        /// </param>
        void LoadMainMenu();

        /// <summary>
        /// Method <c>LoaderCallback</c> is used by the <c>Loading</c>
        /// scene when loading is completed and is ready to switch to
        /// the target scene.
        /// </summary>
        void LoaderCallback();
    }
}
