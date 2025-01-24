using MapGeneration;
using PlayerSystem;
using Rooms;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Room _room;
    [SerializeField] private Player _playerPrefab;
    private void Awake()
    {
        InitLocator();
        DontDestroyOnLoad(gameObject);
    }

    private void InitLocator()
    {
        var levelManager = new LevelManager();
        var mapGen = new MapGenerator();
        var roomSpawner = new RoomSpawner(mapGen, _room);
        var playerSpawner = new PlayerSpawner();

        Locator<LevelManager>.Instance = levelManager;
        Locator<MapGenerator>.Instance = mapGen;
        Locator<RoomSpawner>.Instance = roomSpawner;
        
        // mapGen.SetSeed("YOP235ER4");
        mapGen.Start();
        roomSpawner.SpawnRooms();

        var player = playerSpawner.Spawn(_playerPrefab);
        Locator<Player>.Instance = player;
    }
}
