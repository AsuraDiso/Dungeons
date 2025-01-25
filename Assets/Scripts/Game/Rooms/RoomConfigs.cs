using System.Collections.Generic;
using UnityEngine;

namespace Rooms
{
    [CreateAssetMenu(menuName = "Rooms/RoomConfigs")]
    public class RoomConfigs : ScriptableObject
    {
        [SerializeField] private GameObject _doorPrefab;
        [SerializeField] private List<GameObject> _wallPrefabs;
        [SerializeField] private List<GameObject> _floorTilePrefabs;

        public GameObject DoorPrefab => _doorPrefab;
        public List<GameObject> WallPrefabs => _wallPrefabs;
        public List<GameObject> FloorTilePrefabs => _floorTilePrefabs;
    }
}