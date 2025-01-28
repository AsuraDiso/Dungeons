using Dungeons.Game.MapGeneration;
using Dungeons.Game.PlayerSystem;
using Dungeons.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dungeons.Services
{
    public class LevelSystem
    {
        private readonly ConfigService _configs;

        public LevelSystem(LevelPreset levelPreset, int currentLevel = 1)
        {
            _configs = Locator<ConfigService>.Instance;
            CurrentLevelPreset = levelPreset;
            CurrentLevel = currentLevel;
        }

        public LevelPreset CurrentLevelPreset { get; private set; }
        public int CurrentLevel { get; private set; }

        public void ChangeLevel()
        {
            CurrentLevel++;
            CurrentLevelPreset = _configs.GetRandomLevelPreset(CurrentLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log($"Current level: {CurrentLevel}");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/Game");
        }
    }
}