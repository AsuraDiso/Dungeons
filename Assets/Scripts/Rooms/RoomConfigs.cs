using System.Collections.Generic;
using UnityEngine;

namespace Rooms
{
    [CreateAssetMenu(menuName = "Rooms/RoomConfigs")]
    public class RoomConfigs : ScriptableObject
    {
        [SerializeField] private GameObject _doorPrefab;
        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private List<GameObject> _floorDecorPrefabs;

        public GameObject DoorPrefab => _doorPrefab;
        public GameObject WallPrefab => _wallPrefab;
        public List<GameObject> FloorDecorations => _floorDecorPrefabs;
    }
}