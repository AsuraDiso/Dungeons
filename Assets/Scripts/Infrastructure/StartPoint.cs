using Infrastructure;
using MapGeneration;
using PlayerSystem;
using Rooms;
using Services;
using UnityEngine;
using UnityEngine.Serialization;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Room _room;

    [FormerlySerializedAs("_configManager")] [SerializeField]
    private ConfigService _configService;

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