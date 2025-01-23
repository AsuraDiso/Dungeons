using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGeneration
{
    public enum RoomType
    {
        Start,
        Regular,
        Reward,
        Shop,
        Secret,
        Boss,
    }
    public struct RoomData
    {
        public RoomType Type { get; set; }
        public Vector2 Position { get; set; }
        public List<Vector2> NeighborsRelativePositions { get; set; }
    }

    public class MapGenerator
    {
        private bool _started;
        private List<RoomData> _rooms = new(); 
        private int[] _floorPlan;
        private int _floorPlanCount;
        private Queue<int> _cellQueue = new();
        private List<int> _endRooms = new();
        private int _bossl;
        private int _seed;

        private int _mapWidth;
        private int _mapHeight;
        private int _minRooms;
        private int _maxRooms;

        public MapGenerator(int mapWidth = 10, int mapHeight = 9, int minRooms = 7, int maxRooms = 15)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _minRooms = minRooms;
            _maxRooms = maxRooms;

            _floorPlan = new int[_mapWidth * _mapHeight];  
        }
        public void SetSeed(string seed)
        {
            if (seed == null)
                _seed = (int)(DateTime.UtcNow.Ticks % int.MaxValue);
            else
                _seed = GetNumericSeed(seed);
            Random.InitState(_seed);
        }
        public void Start()
        {
            StartGame();
        
            for (int i = 0; i < 10; i++)
            {
                Update();
            }

            PlaceSpecial();
        }

        private static int GetNumericSeed(string seed)
        {
            var hash = 0;
            foreach (char c in seed)
            {
                hash = (hash << 5) - hash + c; 
                hash %= int.MaxValue;   
            }
            return hash;
        }
        
        private void StartGame()
        {
            _started = true;

            Array.Clear(_floorPlan, 0, _floorPlan.Length);
            _floorPlanCount = 0;
            _cellQueue.Clear();
            _endRooms.Clear();
            _rooms.Clear();  

            Visit(_mapWidth * _mapHeight / 2); 
        }

        private void Update()
        {
            if (_started)
            {
                if (_cellQueue.Count > 0)
                {
                    int i = _cellQueue.Dequeue();
                    int x = i % _mapWidth;
                    bool created = false;

                    if (x > 1) created |= Visit(i - 1);
                    if (x < _mapWidth - 1) created |= Visit(i + 1);
                    if (i > _mapWidth) created |= Visit(i - _mapWidth);
                    if (i < (_mapWidth * (_mapHeight - 1))) created |= Visit(i + _mapWidth);

                    if (!created)
                    {
                        _endRooms.Add(i); 
                    }
                }
            }
        }

        private void PlaceSpecial()
        {
            if (_floorPlanCount < _minRooms || _endRooms.Count <= 0) 
            {
                Start(); 
                return;
            }

            _bossl = _endRooms[^1];  
            _endRooms.RemoveAt(_endRooms.Count - 1);
            CreateRoom(_bossl, RoomType.Boss);  
    
            int rewardl = PopRandomEndRoom();
            if (rewardl == -1)
            {
                Start();
                return;
            }
            CreateRoom(rewardl, RoomType.Reward);

            int coinl = PopRandomEndRoom();
            if (coinl == -1)
            {
                Start();
                return;
            }
            CreateRoom(coinl, RoomType.Shop);

            int secretl = PickSecretRoom();
            if (secretl == -1)
            {
                Start();
                return;
            }
            CreateRoom(secretl, RoomType.Secret);

            UpdateRoomNeighbors();  
        }

        private void CreateRoom(int i, RoomType roomType)
        {
            if (i == -1) return;
    
            int x = i % _mapWidth;
            int y = (i - x) / _mapWidth;
            Vector2 roomPosition = new Vector2(x - _mapWidth / 2, y - _mapHeight / 2);
    
            for (int j = 0; j < _rooms.Count; j++)
            {
                if (_rooms[j].Position == roomPosition)
                {
                    _rooms.RemoveAt(j);
                    break;
                }
            }
    
            List<Vector2> neighbors = new();
            if (y < _mapHeight - 1 && _floorPlan[i + _mapWidth] == 1) neighbors.Add(new Vector2(0, 1)); 
            if (x > 0 && _floorPlan[i - 1] == 1) neighbors.Add(new Vector2(-1, 0)); 
            if (x < _mapWidth - 1 && _floorPlan[i + 1] == 1) neighbors.Add(new Vector2(1, 0)); 
            if (y > 0 && _floorPlan[i - _mapWidth] == 1) neighbors.Add(new Vector2(0, -1)); 

            RoomData roomData = new RoomData
            {
                Type = roomType,
                Position = roomPosition,
                NeighborsRelativePositions = neighbors
            };

            _rooms.Add(roomData);  
        }


        private void UpdateRoomNeighbors()
        {
            for (int i = 0; i < _rooms.Count; i++)
            {
                var room = _rooms[i]; 
                List<Vector2> neighbors = new List<Vector2>();

                if (IsNeighbor(room.Position + new Vector2(0, 1))) neighbors.Add(new Vector2(0, 1));  
                if (IsNeighbor(room.Position + new Vector2(0, -1))) neighbors.Add(new Vector2(0, -1));
                if (IsNeighbor(room.Position + new Vector2(1, 0))) neighbors.Add(new Vector2(1, 0));  
                if (IsNeighbor(room.Position + new Vector2(-1, 0))) neighbors.Add(new Vector2(-1, 0)); 

                room.NeighborsRelativePositions = neighbors;

                _rooms[i] = room;
            }
        }

        private bool IsNeighbor(Vector2 position)
        {
            foreach (var room in _rooms)
            {
                if (room.Position == position)
                {
                    return true;
                }
            }
            return false;
        }

        private int PopRandomEndRoom()
        {
            if (_endRooms.Count == 0)
                return -1;

            int index = Random.Range(0, _endRooms.Count);
            int room = _endRooms[index];
            _endRooms.RemoveAt(index);
            return room;
        }

        private int PickSecretRoom()
        {
            for (int e = 0; e < 900; e++)
            {
                int x = Random.Range(1, _mapWidth - 1);
                int y = Random.Range(2, _mapHeight - 1);
                int i = y * _mapWidth + x;

                if (_floorPlan[i] == 1)
                    continue;

                if (_bossl == i - 1 || _bossl == i + 1 || _bossl == i + _mapWidth || _bossl == i - _mapWidth)
                    continue;

                if (NeighbourCount(i) >= 3)
                    return i;

                if (e > 300 && NeighbourCount(i) >= 2)
                    return i;

                if (e > 600 && NeighbourCount(i) >= 1)
                    return i;
            }

            return -1;
        }

        private int NeighbourCount(int i)
        {
            int count = 0;
            if (i - _mapWidth >= 0) count += _floorPlan[i - _mapWidth];
            if (i - 1 >= 0) count += _floorPlan[i - 1];
            if (i + 1 < _mapWidth * _mapHeight) count += _floorPlan[i + 1];
            if (i + _mapWidth < _mapWidth * _mapHeight) count += _floorPlan[i + _mapWidth];
            return count;
        }

        private bool Visit(int i)
        {
            if (_floorPlan[i] == 1)
                return false;

            int neighbours = NeighbourCount(i);

            if (neighbours > 2)
                return false;

            if (_floorPlanCount >= _maxRooms)
                return false;

            if (Random.value < 0.5f && i != _mapWidth * _mapHeight / 2)  
                return false;

            _cellQueue.Enqueue(i);
            _floorPlan[i] = 1;
            _floorPlanCount++;

            CreateRoom(i, RoomType.Regular); 
            return true;
        }

        public List<RoomData> GetRoomData()
        {
            return _rooms;
        }
    }
}
