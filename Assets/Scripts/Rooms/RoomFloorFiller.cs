using MapGeneration;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class RoomFloorFiller : IRoomFiller
    {
        public void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs)
        {
            var floor = new GameObject("Floor");
            floor.transform.SetPositionAndRotation(room.transform.position, Quaternion.identity);
            floor.transform.SetParent(room.transform);
            
            var tileScale = 2f;
            int floorDepth = 12;
            int floorLength = 20;
            for (int i = 0; i < floorLength*.5f; i++)
            {
                for (int j = 0; j < floorDepth*.5f; j++)
                {
                    var position = new Vector3(i * tileScale - floorLength*.5f + tileScale*.5f, 0, j * tileScale - floorDepth*.5f + tileScale*.5f);
                    var floorPrefab = GameObject.Instantiate(
                        roomConfigs.FloorTilePrefabs[Random.Range(0, roomConfigs.FloorTilePrefabs.Count)],
                        floor.transform);
                    floorPrefab.transform.SetPositionAndRotation(floor.transform.position + position,
                        floor.transform.rotation);
                }
            }
        }

        public bool IsValid()
        {
            return true;    
        }
    }
}