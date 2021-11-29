using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// [System.Serializable]
    /// public class Settings
    /// {
    ///   public List<string> list = new List<string>();
    /// }
    /// 
    /// Settings settings = new Settings();
    /// GameSettingsManager.SaveSettings("example", settings);
    /// 
    /// settings = GameSettingsManager.LoadeSettings("example");
    /// </example>
    public static class GameSettingsManager
    {

        public static void SaveSettings(string name, object settings)
        {
            if (name != null && settings != null)
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }
                string settingsPath = getSettingsPath(name);
                XmlSerializer serializer = new XmlSerializer(settings.GetType());
                FileStream stream = new FileStream(settingsPath, FileMode.Create);
                serializer.Serialize(stream, settings);
                stream.Close();

            }
        }

        public static T LoadSettings<T>(string name)
        {
            string settingsPath = getSettingsPath(name);

            if (name != null && File.Exists(settingsPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                FileStream stream = new FileStream(settingsPath, FileMode.Open);
                object settings = serializer.Deserialize(stream) as object;
                return (T) Convert.ChangeType(settings, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        private static string SettingsDirectory
        {
            get
            {
                string baseDataPath = Application.persistentDataPath;
                return $"{baseDataPath}/Settings";
            }
        }

        private static string getSettingsPath(string name)
        {
            return $"{SettingsDirectory}/{name}.xml";
        }
    }
}