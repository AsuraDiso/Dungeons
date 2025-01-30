using System;
using Dungeons.Game.MapGeneration;
using Dungeons.Game.PlayerSystem;
using Dungeons.Game.Rooms;
using Dungeons.Infrastructure;
using Dungeons.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Game.UI.MiniMap
{
    public class UIMiniMap : MonoBehaviour
    {
        private float _mapScaler = 2f; 
        private Player _player; 
        [SerializeField] private UIMiniMapRoom _image; 
        [SerializeField] private RectTransform _mapContainer; 
        [SerializeField] private RectTransform _mapCutter; 
        [SerializeField] private Vector2 _regularSize; 
        [SerializeField] private Vector2 _zoomedSize; 

        private void Awake()
        {
            var mapGen = Locator<MapGenerator>.Instance; 
            CreateRoomLayout(mapGen);
            _player = Locator<Player>.Instance;
        }

        private void Update()
        {
            if (_player.Controller.TabPressed)
            {
                _mapCutter.sizeDelta = _zoomedSize;
            } else
            {
                _mapCutter.sizeDelta = _regularSize;
            }
        }

        public void Move(Vector2 position)
        {
            
        }
        private void CreateRoomLayout(MapGenerator mapGen)
        {
            var rooms = mapGen.GetRoomData();

            foreach (var roomData in rooms)
                InstantiateRoom(roomData);
        }

        private void InstantiateRoom(RoomData roomData)
        {
            var roomSpawner = Locator<RoomSpawner>.Instance;
                   
            var roomPosition = new Vector2(roomData.Position.x * RoomConstants.RoomWidth * _mapScaler,
                roomData.Position.y * RoomConstants.RoomDepth * _mapScaler);

            var room = roomSpawner.GetRoomByCoords(roomData.Position);
            
            var uiRoom = Instantiate(_image, _mapContainer);
            uiRoom.RectTransform.anchoredPosition = roomPosition;
            uiRoom.SetRoomSize(RoomConstants.RoomWidth * _mapScaler, RoomConstants.RoomDepth * _mapScaler);
            uiRoom.SetRoomTypeIcon(roomData.Type);
            uiRoom.ConnectRoom(room);
            room.Enter += () => { OnRoomChanged(roomData.Position); };
        }

        private void OnRoomChanged(Vector2 position)
        {
            _mapContainer.anchoredPosition = position * -new Vector2(RoomConstants.RoomWidth * _mapScaler, RoomConstants.RoomDepth * _mapScaler);
        }
    }
}