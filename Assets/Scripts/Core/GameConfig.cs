using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// Class <c>GameConfig</c> stores game configuration attributes.
    /// </summary>
    public class GameConfig
    {
        /// <summary>
        /// Instance property <c>Name</c> stores the game name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Instance property <c>Scene</c> stores the game scene name.
        /// </summary>
        public string Scene { get; set; }

        /// <summary>
        /// Instance property <c>Icon</c> stores the game icon.
        /// </summary>
        public Sprite Icon { get; set; }

        /// <summary>
        /// Constructor initializes the new <c>GameConfig</c>.
        /// </summary>
        /// <param name="name">
        /// <c>name</c> is the game name
        /// </param>
        /// <param name="scene">
        /// <c>scene</c> is the game scene
        /// </param>
        /// /// <param name="icon">
        /// <c>icon</c> is the game icon
        /// </param>
        public GameConfig(string name, string scene, Sprite icon)
        {
            Name = name;
            Scene = scene;
            Icon = icon;
        }
    }
}