using System.Collections.Generic;
using MapGeneration;
using UnityEngine;

namespace Rooms
{
    public class RoomSpawner : MonoBehaviour
    {
        [SerializeField] private Room _roomPrefab;
        private Dictionary<Vector2, Room> _rooms = new();
        private MapGenerator mapGenerator = new();

        public void GenerateMap(string seed = null)
        {
            mapGenerator.SetSeed(seed);
            mapGenerator.Start();
            List<RoomData> rooms = mapGenerator.GetRoomData();

            foreach (var roomData in rooms)
            {
                InstantiateRoom(roomData);
            }

            _rooms[new Vector2(0, 0)].Enter();
        }

        private void InstantiateRoom(RoomData roomData)
        {
            var roomPosition = new Vector3(roomData.Position.x * RoomConstants.RoomWidth, 0, roomData.Position.y * RoomConstants.RoomDepth);
            var room = Instantiate(_roomPrefab, roomPosition, Quaternion.identity);
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