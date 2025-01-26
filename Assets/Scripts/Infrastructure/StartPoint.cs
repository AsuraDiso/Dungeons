using Dungeons.Game.MapGeneration;
using Dungeons.Game.PlayerSystem;
using Dungeons.Game.Rooms;
using Dungeons.Services;
using PlayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dungeons.Infrastructure
{
    public class StartPoint : MonoBehaviour
    {
        [field: SerializeField] public Room Room { get; private set; }
        [field: SerializeField] public ConfigService ConfigService { get; private set; }

        private void Awake()
        {
            InitLocator();
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("Scenes/Game");
        }

        private void InitLocator()
        {
            Locator<StartPoint>.Instance = this;
            Locator<ConfigService>.Instance = ConfigService;

            Locator<LevelSystem>.Instance = new LevelSystem(ConfigService.GetRandomLevelPreset());
            ConfigService.Init();
        }
    }
}