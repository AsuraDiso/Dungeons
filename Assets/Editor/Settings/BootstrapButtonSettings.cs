using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Dungeons.Editor.Settings
{
    public class BootstrapButtonSettings : ScriptableObject
    {
        public const string SettingsPath = "Assets/Editor/Settings/BootstrapButtonSettings.asset";

        public string bootScenePath;

        public static BootstrapButtonSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<BootstrapButtonSettings>(SettingsPath);
            if (settings == null)
            {
                settings = CreateInstance<BootstrapButtonSettings>();
                settings.bootScenePath = GetFirstScene();
                AssetDatabase.CreateAsset(settings, SettingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        private static string GetFirstScene()
        {
            return EditorBuildSettings.scenes.FirstOrDefault(scene => scene.enabled)?.path;
        }

        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
}