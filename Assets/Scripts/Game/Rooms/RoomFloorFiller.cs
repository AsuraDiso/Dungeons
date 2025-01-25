using Infrastructure;
using MapGeneration;
using Services;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class RoomFloorFiller : IRoomFiller
    {
        private readonly LevelSystem _levelSystem;

        public RoomFloorFiller()
        {
            _levelSystem = Locator<LevelSystem>.Instance;
        }

        public void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs)
        {
            var floor = new GameObject("Floor");
            floor.transform.SetPositionAndRotation(room.transform.position, Quaternion.identity);
            floor.transform.SetParent(room.transform);

            var tileScale = 2f;
            var floorDepth = 12;
            var floorLength = 20;
            for (var i = 0; i < floorLength * .5f; i++)
            for (var j = 0; j < floorDepth * .5f; j++)
            {
                var isDecor = Random.value < _levelSystem.CurrentLevelPreset.DecorChance;
                var floorPrefabs = isDecor
                    ? _levelSystem.CurrentLevelPreset.DecoratedFloorPrefabs
                    : _levelSystem.CurrentLevelPreset.FloorPrefabs;
                var position = new Vector3(i * tileScale - floorLength * .5f + tileScale * .5f, 0,
                    j * tileScale - floorDepth * .5f + tileScale * .5f);
                var floorPrefab = Object.Instantiate(
                    floorPrefabs[Random.Range(0, floorPrefabs.Count)],
                    floor.transform);
                floorPrefab.transform.SetPositionAndRotation(floor.transform.position + position,
                    floor.transform.rotation);
            }
        }

        public bool IsValid()
        {
            return true;
        }
    }
}