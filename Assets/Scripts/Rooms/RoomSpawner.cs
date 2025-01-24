using System.Collections.Generic;
using MapGeneration;
using UnityEngine;

namespace Rooms
{
    public class RoomSpawner
    {
        private Room _roomPrefab;
        private Dictionary<Vector2, Room> _rooms = new();
        private MapGenerator _mapGenerator;

        public RoomSpawner(MapGenerator mapGen, Room roomPrefab)
        {
            _mapGenerator = mapGen;
            _roomPrefab = roomPrefab;
        }

        public void SpawnRooms()
        {
            List<RoomData> rooms = _mapGenerator.GetRoomData();

            foreach (var roomData in rooms)
            {
                InstantiateRoom(roomData);
            }

            _rooms[new Vector2(0, 0)].Enter();
        }

        private void InstantiateRoom(RoomData roomData)
        {
            var roomPosition = new Vector3(roomData.Position.x * RoomConstants.RoomWidth, 0, roomData.Position.y * RoomConstants.RoomDepth);
            var room = GameObject.Instantiate(_roomPrefab, roomPosition, Quaternion.identity);
            room.SetRoomSpawner(this);
            room.Instantiate(roomData);
            _rooms.Add(roomData.Position, room);
        }

        public Room GetRoomByCoords(Vector2 coords)
        {
            return _rooms.ContainsKey(coords) ? _rooms[coords] : null;
        }

        private void ClearRooms()
        {
            if (_rooms != null)
            {
                _rooms.Clear();
            }
        }
    }
}