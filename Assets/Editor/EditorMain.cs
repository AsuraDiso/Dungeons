using Dungeons.Editor.Settings;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Dungeons.Editor
{
    [InitializeOnLoad]
    public static class EditorMain
    {
        static EditorMain()
        {
            CustomEditorToolbar.Initialized += InitButtons;
            EditorApplication.playModeStateChanged += OnPlayModeStateChange;
        }
        
        private static void InitButtons()
        {
            CustomEditorToolbar.Initialized -= InitButtons;
            var button = new PlayFromBootstrapButton();
            CustomEditorToolbar.AddMiddle(button);
            button.SendToBack();
            button.Click += ButtonOnClick;
        }

        private static void ButtonOnClick()
        {
            var scenePath = BootstrapButtonSettings.GetOrCreateSettings().bootScenePath;
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            EditorApplication.EnterPlaymode();
        }

        private static void OnPlayModeStateChange(PlayModeStateChange obj)
        {
            if (obj is PlayModeStateChange.ExitingPlayMode)
            {
                EditorSceneManager.playModeStartScene = null;
            }
        }
    }
}