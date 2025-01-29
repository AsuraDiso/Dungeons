using Dungeons.Game.MapGeneration;
using Dungeons.Game.Rooms;
using Dungeons.Infrastructure;
using UnityEngine;

namespace Dungeons.Game.UI.MiniMap
{
    public class UIMiniMap : MonoBehaviour
    {
        private float _mapScaler = 1f; 
        [SerializeField] private RectTransform _image; 
        [SerializeField] private RectTransform _mapContainer; 

        private void Awake()
        {
            var mapGen = Locator<MapGenerator>.Instance; 
            CreateRoomLayout(mapGen);
        }

        private void CreateRoomLayout(MapGenerator mapGen)
        {
            var rooms = mapGen.GetRoomData();

            foreach (var roomData in rooms)
                InstantiateRoom(roomData);
        }

        private void InstantiateRoom(RoomData roomData)
        {
            var roomPosition = new Vector2(
                roomData.Position.x * RoomConstants.RoomWidth * _mapScaler,
                roomData.Position.y * RoomConstants.RoomDepth * _mapScaler
            );

            var room = Instantiate(_image, _mapContainer);
            room.anchoredPosition = roomPosition;
        }
    }
}