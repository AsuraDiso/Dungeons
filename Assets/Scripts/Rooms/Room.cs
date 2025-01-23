using System.Collections.Generic;
using MapGeneration;
using UnityEngine;

namespace Rooms
{
    public static class RoomConstants
    {
        public const float RoomWidth = 20f;
        public const float RoomDepth = 12f;
        public const float CameraHeight = 15f;
        public const float CameraDepth = -7f;
        public const float PlayerOffsetX = 9f;
        public const float PlayerOffsetZ = 5f;
    }

    public class Room : MonoBehaviour
    {
        public Vector2 Position { get; private set; }
        public RoomType Type { get; private set; } 
        private List<IRoomFiller> _fillers = new();
        [SerializeField] private RoomConfigs _roomConfigs;
        private RoomSpawner _roomSpawner;
        private Camera _currentCamera;

        private void Awake()
        {
            _currentCamera = Camera.main;
        }

        public void SetRoomSpawner(RoomSpawner roomSpawner)
        {
            _roomSpawner = roomSpawner;
        }

        public void Instantiate(RoomData roomData)
        {
            gameObject.SetActive(false);

            Type = roomData.Position == new Vector2(0, 0) ? RoomType.Start : roomData.Type;

            Position = roomData.Position;
            name = $"Room: {Type}, ({Position.x},{Position.y})";

            _fillers.Add(new RoomDoorsFiller());
            _fillers.Add(new RoomDecorationsFiller());
            _fillers.Add(new RoomLightningFiller());

            if (Type == RoomType.Regular)
            {
                _fillers.Add(new RoomMobsFiller());
            }

            foreach (IRoomFiller filler in _fillers)
            {
                if (filler.IsValid())
                {
                    filler.Fill(this, roomData, _roomConfigs);
                }
            }
        }

        public void TryEnterAnotherRoom(GameObject other, Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                var nextRoomCoords = Position + direction;

                if (_roomSpawner != null)
                {
                    Room nextRoom = _roomSpawner.GetRoomByCoords(nextRoomCoords);
                    if (nextRoom != null)
                    {
                        Exit();
                        nextRoom.Enter(other, direction);
                    }
                }
            }
        }

        public void Enter(GameObject other = default, Vector2 direction = default)
        {
            gameObject.SetActive(true);
            if (other != null)
            {
                other.transform.position = transform.position + new Vector3(-direction.x * RoomConstants.PlayerOffsetX, 0f, -direction.y * RoomConstants.PlayerOffsetZ);
            }
            _currentCamera.transform.position = transform.position + new Vector3(0, RoomConstants.CameraHeight, RoomConstants.CameraDepth);
        }

        public void Exit()
        {
            gameObject.SetActive(false);
        }
    }
}
