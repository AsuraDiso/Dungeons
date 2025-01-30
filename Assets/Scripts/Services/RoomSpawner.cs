using System.Collections.Generic;
using Dungeons.Game.MapGeneration;
using Dungeons.Game.Rooms;
using UnityEngine;

namespace Dungeons.Services
{
    public class RoomSpawner
    {
        private readonly MapGenerator _mapGenerator;
        private readonly Room _roomPrefab;
        private readonly Dictionary<Vector2, Room> _rooms = new();

        public RoomSpawner(MapGenerator mapGen, Room roomPrefab)
        {
            _mapGenerator = mapGen;
            _roomPrefab = roomPrefab;
        }

        public void SpawnRooms()
        {
            ClearRooms();
            var rooms = _mapGenerator.GetRoomData();

            foreach (var roomData in rooms) InstantiateRoom(roomData);

            _rooms[new Vector2(0, 0)].EnterRoom();
        }

        private void InstantiateRoom(RoomData roomData)
        {
            var roomPosition = new Vector3(roomData.Position.x * RoomConstants.RoomWidth, 0,
                roomData.Position.y * RoomConstants.RoomDepth);
            var room = Object.Instantiate(_roomPrefab, roomPosition, Quaternion.identity);
            room.Instantiate(roomData);
            _rooms.Add(roomData.Position, room);
        }

        public void FillWithEnemies()
        {
            foreach (var room in _rooms) room.Value.FillWithEnemies();
        }


        public Room GetRoomByCoords(Vector2 coords) => _rooms.ContainsKey(coords) ? _rooms[coords] : null;
        private void ClearRooms()
        {
            foreach (var room in _rooms) Object.Destroy(room.Value.gameObject);
            _rooms?.Clear();
        }
    }
}