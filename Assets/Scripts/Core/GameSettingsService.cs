using System.IO;
using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// class <c>GameSettingsService</c> is responsible for json object serialization and 
    /// persistence. Use this class when you need to serialize and store object state and 
    /// then deserialize and restore restore the object state. 
    /// 
    /// Serialization Rules
    /// 
    /// - CAN serialize public non-static fields (of serializable types)
    /// - CAN serialize nonpublic non-static fields marked with the SerializeField attribute.
    /// - CANNOT serialize static fields.
    /// - CANNOT serialize properties.
    /// 
    /// Serialization Implementation
    /// 
    /// class Entry
    /// {
    ///     [SerializeField] 
    ///     private int _value = 0;
    ///     
    ///     public int Value
    ///     {
    ///         get { return _value; }
    ///         set { _value = value; }
    ///     }
    /// }
    /// </summary>
    /// <example>
    /// Entry settings = new Entry();
    /// GameSettingsService.Instance.SaveSettings("example", settings);
    /// settings = GameSettingsService.Instance.LoadeSettings<Entry>("example");
    /// </example>
    public class GameSettingsService : IGameSettingsService
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
        public void SaveSettings(string name, object settings)
        {
            if (name != null && settings != null)
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }
                string filePath = GetSettingsPath(name);
                string json = JsonUtility.ToJson(settings);
                File.WriteAllText(filePath, json);
            }
        }

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
        public T GetSettings<T>(string name)
        {
            string filePath = GetSettingsPath(name);

            if (name != null && File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(json);  
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Property <c>SettingsDirectory</c> represents the base settings directory 
        /// where the setting file will be stored.
        /// </summary>
        private string SettingsDirectory
        {
            get
            {
                string baseDataPath = Application.persistentDataPath;
                return $"{baseDataPath}/Settings";
            }
        }

        /// <summary>
        /// Method <c>GetSettingsPath</c> is used to retrieve the full file path 
        /// associated with the game setting name.
        /// </summary>
        /// <param name="name">
        /// <c>name</c> is the unique name of the game setting to retrieve.
        /// </param>
        /// <returns>
        /// Full game setting file path.
        /// </returns>
        private string GetSettingsPath(string name)
        {
            return $"{SettingsDirectory}/{name}.json";
        }
    }
}