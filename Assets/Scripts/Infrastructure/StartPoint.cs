using Dungeons.Game.MapGeneration;
using Dungeons.Game.PlayerSystem;
using Dungeons.Game.Rooms;
using Dungeons.Services;
using UnityEngine;

namespace Dungeons.Infrastructure
{
    public class StartPoint : MonoBehaviour
    {
        [SerializeField] private Room _room;
        [SerializeField] private ConfigService _configService;

        private void Awake()
        {
            InitLocator();
            DontDestroyOnLoad(gameObject);
        }

        private void InitLocator()
        {
            Locator<ConfigService>.Instance = _configService;
            _configService.Init();

            var levelManager = new LevelSystem(_configService.GetRandomLevelPreset());
            var mapGen = new MapGenerator();
            var roomSpawner = new RoomSpawner(mapGen, _room);
            var playerSpawner = new PlayerSpawner();

            Locator<LevelSystem>.Instance = levelManager;
            Locator<MapGenerator>.Instance = mapGen;
            Locator<RoomSpawner>.Instance = roomSpawner;

            mapGen.Start();
            roomSpawner.SpawnRooms();

            var player = playerSpawner.Spawn(_configService.Player);
            Locator<Player>.Instance = player;

            roomSpawner.FillWithEnemies();
        }
    }
}