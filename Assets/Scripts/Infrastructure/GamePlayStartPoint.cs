using Dungeons.Game.MapGeneration;
using Dungeons.Game.PlayerSystem;
using Dungeons.Services;
using UnityEngine;

namespace Dungeons.Infrastructure
{
    public class GamePlayStartPoint : MonoBehaviour
    {
        private void Awake()
        {
            var startPoint = Locator<StartPoint>.Instance;
            var mapGen = new MapGenerator();
            var roomSpawner = new RoomSpawner(mapGen, startPoint.Room);
            var playerSpawner = new PlayerSpawner();
    
            Locator<MapGenerator>.Instance = mapGen;
            Locator<RoomSpawner>.Instance = roomSpawner;

            mapGen.Start();
            roomSpawner.SpawnRooms();

            var player = playerSpawner.Spawn(startPoint.ConfigService.Player);
            Locator<Player>.Instance = player;

            roomSpawner.FillWithEnemies();
        }

        private void OnDestroy()
        {
            Locator<MapGenerator>.Instance = null;
            Locator<RoomSpawner>.Instance = null;
            Locator<PlayerSpawner>.Instance = null;
            Locator<Player>.Instance = null;
        }
    }
}