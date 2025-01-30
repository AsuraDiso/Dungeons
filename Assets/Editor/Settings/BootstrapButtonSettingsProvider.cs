using UnityEditor;
using UnityEngine.UIElements;

namespace Dungeons.Editor.Settings
{
    public class BootstrapButtonSettingsProvider : SettingsProvider
    {
        private SerializedObject _settings;

        private BootstrapButtonSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope) { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = BootstrapButtonSettings.GetSerializedSettings();
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(_settings.FindProperty(nameof(BootstrapButtonSettings.bootScenePath)));
            _settings.ApplyModifiedPropertiesWithoutUndo();
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new BootstrapButtonSettingsProvider("Project/BootScene");
        }
    }
}