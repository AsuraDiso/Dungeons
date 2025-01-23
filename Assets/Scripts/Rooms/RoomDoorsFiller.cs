using System.Collections.Generic;
using Doors;
using MapGeneration;
using UnityEngine;

namespace Rooms
{
    public class RoomDoorsFiller : IRoomFiller
    {
        public void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs)
        {
            List<Vector2> directions = new()
            {
                new Vector2(-1, 0),  
                new Vector2(1, 0),   
                new Vector2(0, 1),   
                new Vector2(0, -1)   
            };

            foreach (var direction in directions)
            {
                var position = GetDoorPosition(direction);
                var rotation = GetDoorRotation(direction);

                if (!roomData.NeighborsRelativePositions.Contains(direction))
                {
                    var wall = GameObject.Instantiate(roomConfigs.WallPrefab, room.transform);
                    wall.transform.SetPositionAndRotation(room.transform.position + position, rotation);
                }
                else
                {
                    var doorObject = GameObject.Instantiate(roomConfigs.DoorPrefab, room.transform);
                    doorObject.transform.SetPositionAndRotation(room.transform.position + position, rotation);

                    var door = doorObject.GetComponent<Door>();
                    if (door != null)
                    {
                        door.Direction = direction;
                        door.ConnectedRoom = room;
                    }
                }
            }
        }

        public bool IsValid()
        {
            return true;
        }

        private Vector3 GetDoorPosition(Vector2 relativePosition)
        {
            return new Vector3(relativePosition.x * 10, 0, relativePosition.y * 6);
        }

        private Quaternion GetDoorRotation(Vector2 relativePosition)
        {
            if (Mathf.Approximately(relativePosition.x, 1))
                return Quaternion.Euler(-90, 0, -90);
            if (Mathf.Approximately(relativePosition.x, -1))
                return Quaternion.Euler(-90, 0, 90);
            if (Mathf.Approximately(relativePosition.y, 1))
                return Quaternion.Euler(-90, 0, -180);
            return Quaternion.Euler(-90, 0, 0);
        }
    }
}
