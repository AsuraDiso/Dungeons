using System.Collections.Generic;
using Doors;
using MapGeneration;
using UnityEngine;

namespace Rooms
{
    public class RoomWallsAndDoorsFiller : IRoomFiller
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
                int wallLength = Mathf.Approximately(direction.x, 0) ? 20 : 12; 
                float wallOffset = 4; 
                float halfWallLength = wallLength*.5f; 

                var position = GetWallPosition(direction);
                var rotation = GetWallRotation(direction);

                for (int i = 0; i < wallLength; i+=4)
                {
                    var offset =
                        new Vector3(direction.y * i - halfWallLength * direction.y + direction.y * wallOffset * .5f, 0,
                            direction.x * i - halfWallLength * direction.x + direction.x * wallOffset * .5f);
                    var currentPosition = position + offset;
                    
                    var isDoorPlace = Mathf.Approximately(wallLength * 0.5f, i + wallOffset * 0.5f);
                    var doesHasDoor = roomData.NeighborsRelativePositions.Contains(direction);
                    if (isDoorPlace && doesHasDoor)
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
                    if (!(doesHasDoor && isDoorPlace))
                    { 
                        var wallPassage = GameObject.Instantiate(roomConfigs.WallPrefabs[Random.Range(0, roomConfigs.WallPrefabs.Count)], room.transform);
                        wallPassage.transform.SetPositionAndRotation(room.transform.position + currentPosition, rotation);
                    }
                }
            }
        }

        public bool IsValid()
        {
            return true;
        }
        private Vector3 GetWallPosition(Vector2 relativePosition)
        {
            return new Vector3(relativePosition.x * 10, 0, relativePosition.y * 6);
        }
        private Quaternion GetWallRotation(Vector2 relativePosition)
        {
            if (Mathf.Approximately(relativePosition.x, 1))
                return Quaternion.Euler(0, 90, 0);
            if (Mathf.Approximately(relativePosition.x, -1))
                return Quaternion.Euler(0, -90, 0);
            if (Mathf.Approximately(relativePosition.y, 1))
                return Quaternion.Euler(0, 180, 0);
            return Quaternion.Euler(0, 0, 0);
        }
    }
}
