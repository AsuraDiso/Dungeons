using MapGeneration;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class RoomDecorationsFiller : IRoomFiller
    {
        public void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs)
        {
            var amount = Random.Range(1, 5);

            for (int i = 0; i < amount; i++)
            {
                var deco = roomConfigs.FloorDecorations[Random.Range(1, roomConfigs.FloorDecorations.Count)];
                var decoPrefab = GameObject.Instantiate(deco, room.transform, true);
                var position = new Vector3(Random.Range(-5f, 5f), -.005f, Random.Range(-3f, 3f));
                decoPrefab.transform.SetPositionAndRotation(room.transform.position+position, Quaternion.Euler(-90, 0, Random.Range(0, 360)));
                decoPrefab.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        public bool IsValid()
        {
            return true;    
        }
    }
}